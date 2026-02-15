using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using SalesSolution.Web.Models;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.DataManager;
using Library.DAL.SInventory_DAL;
using Library.DAO.SInventory_Entities;
using Library.BLL.SInventory_BLL;

public partial class SInventory_UI_ProductTarget : System.Web.UI.Page
{
    DataTable aDataTable = new DataTable();
    ProductTargetBLL aProductTargetBLL = new ProductTargetBLL();
    ProductTargetDAL aProductTargetDAL = new ProductTargetDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDownList();
            LoadActiveProductList();
            targetIdHiddenField.Value = Request.QueryString["ID"];
            LoadPTInfo(targetIdHiddenField.Value);
            SetDefaultValue();
            

        }
    }

    private void LoadActiveProductList()
    {
        aDataTable = aProductTargetBLL.LoadProductTarget();
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();
    }

    private void LoadDropDownList()
    {
        aProductTargetDAL.LoadExistingCategory(ddlSearchCategory);
    }

    protected void gv_DocumentUpload_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;

        if ((gv.ShowHeader == true && gv.Rows.Count > 0)
            || (gv.ShowHeaderWhenEmpty == true))
        {
            //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    private void LoadPTInfo(string targetId)
    {

        
        if (targetId != "")
        {
            aDataTable = aProductTargetDAL.LoadProductTargetEdit(targetId);

            txtTargetCategory.Text = aDataTable.Rows[0]["TargetCategory"].ToString();
            txtTotalTarget.Text = aDataTable.Rows[0]["TotalTargetByTp"].ToString();
            txtTotalTargetWithVAt.Text = aDataTable.Rows[0]["TotalTargetByTpVat"].ToString();

            loadGridView.DataSource = aDataTable;
            loadGridView.DataBind();

            for (int i = 0; i < aDataTable.Rows.Count; i++)
            {
                TextBox Quantity = (TextBox)loadGridView.Rows[i].FindControl("TargetQty");
                TextBox TargetValue = (TextBox)loadGridView.Rows[i].FindControl("TargetValue");
                TextBox TargetWithVAT = (TextBox)loadGridView.Rows[i].FindControl("TargetWithVAT");

                if (aDataTable.Rows[i]["TargetQty"].ToString() != "")
                {
                    Quantity.Text = aDataTable.Rows[i]["TargetQty"].ToString();
                    TargetValue.Text = aDataTable.Rows[i]["TargetValue"].ToString();
                    TargetWithVAT.Text = aDataTable.Rows[i]["TargetWithVAT"].ToString();
                }
                else
                {
                    Quantity.Text = "";
                    TargetValue.Text = "";
                    TargetWithVAT.Text = "";
                }

            }

          
        }
        else
        {
            //aDataTable = aProductTargetBLL.LoadProductTarget();
            //loadGridView.DataSource = aDataTable;
            //loadGridView.DataBind();
        }
        
    }

    protected void codeTextBox_TextChanged(object sender, EventArgs e)
    {
        TextBox TextBox = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)TextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        Label unitPrice = (Label)loadGridView.Rows[rowindex].FindControl("UnitPrice");
        TextBox Quantity = (TextBox)loadGridView.Rows[rowindex].FindControl("TargetQty");
        TextBox TargetValue = (TextBox)loadGridView.Rows[rowindex].FindControl("TargetValue");
        Label VATAmountPerUnit = (Label)loadGridView.Rows[rowindex].FindControl("VATAmountPerUnit");
        TextBox TargetWithVAT = (TextBox)loadGridView.Rows[rowindex].FindControl("TargetWithVAT");

        
        decimal sumForVAt = 0;
        decimal sum = 0;

        if (unitPrice.Text != "" && VATAmountPerUnit.Text != "")
        {
            sum = Convert.ToDecimal(Quantity.Text.Trim()) * Convert.ToDecimal(unitPrice.Text);
            TargetValue.Text = String.Format("{0:0.00}", sum);
            sumForVAt = (Convert.ToDecimal(unitPrice.Text) + Convert.ToDecimal(VATAmountPerUnit.Text)) * Convert.ToDecimal(Quantity.Text);
            TargetWithVAT.Text = String.Format("{0:0.00}", sumForVAt);
        }

        TotalTargetValue();
        //string productCode = productCodeTextBox.Text.Trim();
        //GetProduct(rowindex, productCode);
    }
    protected void SetDefaultValue()
    {
        
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            TextBox Quantity = (TextBox)loadGridView.Rows[i].FindControl("TargetQty");
            TextBox TargetValue = (TextBox)loadGridView.Rows[i].FindControl("TargetValue");
            TextBox TargetWithVAT = (TextBox)loadGridView.Rows[i].FindControl("TargetWithVAT");
            Label unitPrice = (Label)loadGridView.Rows[i].FindControl("UnitPrice");
            Label VATAmountPerUnit = (Label)loadGridView.Rows[i].FindControl("VATAmountPerUnit");

            if (Quantity.Text == "")
            {
                Quantity.Text = "0";
                TargetValue.Text = "0";
                TargetWithVAT.Text = "0";
            }
            if (unitPrice.Text == "" && VATAmountPerUnit.Text=="")
            {
                unitPrice.Text = "0";
                VATAmountPerUnit.Text = "0";
            }
        }
        
    }
    protected void TotalTargetValue()
    {
        decimal sum = 0;
        decimal SumWithVAt = 0;
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            TextBox TargetValue = (TextBox)loadGridView.Rows[i].FindControl("TargetValue");
            TextBox TargetWithVAT = (TextBox)loadGridView.Rows[i].FindControl("TargetWithVAT");
            
            if (TargetValue.Text != "")
            {
                sum += Convert.ToDecimal(TargetValue.Text);
                SumWithVAt += Convert.ToDecimal(TargetWithVAT.Text);
            }
        }
        txtTotalTarget.Text = sum.ToString();
        txtTotalTargetWithVAt.Text = SumWithVAt.ToString();
        
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       // if (e.CommandName == "EditData")
       // {
       //     Session["DAEdit"] = e.CommandArgument.ToString();
       //     Response.Redirect("DASetup.aspx");
       // }

    }
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {

        if (Validation())
        {

            // Master Bind

            TargetCategoryMasterDAO aTargetCategoryMasterDAO = new TargetCategoryMasterDAO();


            aTargetCategoryMasterDAO.TargetId = targetIdHiddenField.Value != "" ? Convert.ToInt32(targetIdHiddenField.Value) : 0;
            aTargetCategoryMasterDAO.TargetCategory = txtTargetCategory.Text;
            aTargetCategoryMasterDAO.TotalTargetByTp = Convert.ToDecimal(txtTotalTarget.Text);
            aTargetCategoryMasterDAO.TotalTargetByTpVat = Convert.ToDecimal(txtTotalTargetWithVAt.Text);

            if (targetIdHiddenField.Value != "")
            {
                aTargetCategoryMasterDAO.UpdatedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
            }
            else
            {
                aTargetCategoryMasterDAO.EntryBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
            }


            // Detail Bind

            TargetCategoryDetailsDAO aDetailsDAO;
            List<TargetCategoryDetailsDAO> aList = new List<TargetCategoryDetailsDAO>();


            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                aDetailsDAO = new TargetCategoryDetailsDAO();

                Label ProductCode = (Label)loadGridView.Rows[i].FindControl("ProductCode");
                TextBox Quantity = (TextBox)loadGridView.Rows[i].FindControl("TargetQty");
                Label unitPrice = (Label)loadGridView.Rows[i].FindControl("UnitPrice");
                Label VATAmountPerUnit = (Label)loadGridView.Rows[i].FindControl("VATAmountPerUnit");
                TextBox TargetValue = (TextBox)loadGridView.Rows[i].FindControl("TargetValue");
                TextBox TargetWithVAT = (TextBox)loadGridView.Rows[i].FindControl("TargetWithVAT");

                aDetailsDAO.ProductCode = ProductCode.Text.Trim();
                aDetailsDAO.TargetQty = Convert.ToDecimal(Quantity.Text);
                aDetailsDAO.TpPerPack = Convert.ToDecimal(unitPrice.Text);
                aDetailsDAO.VatPerPack = Convert.ToDecimal(VATAmountPerUnit.Text);
                aDetailsDAO.TargetValueByTp = Convert.ToDecimal(TargetValue.Text);
                aDetailsDAO.TargetValueByTpVat = Convert.ToDecimal(TargetWithVAT.Text);

                aList.Add(aDetailsDAO);
            }

            // Save Operation

            ResultInfo res = aProductTargetDAL.SaveProductTarget(aTargetCategoryMasterDAO, aList);

            if (res.isSuccess)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation Successful !" + "','Success','ProductTarget.aspx');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist !" + "','Faild');", true);
            }
        }    
    }
    private bool Validation()
    {
        aProductTargetDAL.HasTargetCategory(txtTargetCategory.Text.Trim());
        if (txtTargetCategory.Text == "")
        {
            showMessageBox("Please Input a Name for Product target!!!!");
            return false;
        }
        if (targetIdHiddenField.Value=="")
        {
            if (aProductTargetDAL.HasTargetCategory(txtTargetCategory.Text))
            {
            showMessageBox("Please Input a Unique Name for Product target!!!!");
            return false;
            }
        }

      //  for (int i = 0; i < loadGridView.Rows.Count; i++)
      //  {
      //      TextBox categoryTextBox = (TextBox)loadGridView.Rows[i].Cells[1].FindControl("targetCategoryTextBox");
      //
      //      if (categoryTextBox.Text == "")
      //      {
      //          ShowMessageBox("Please Select Target Category !!!");
      //          return false;
      //      }
      //
      //  }

        return true;

    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
       Response.Redirect("ProductTarget.aspx");
    }

    protected void ddlSearchCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchCategory.SelectedValue != "")
        {
            targetIdHiddenField.Value = ddlSearchCategory.SelectedValue;

            aDataTable = aProductTargetDAL.LoadProductTargetEdit(ddlSearchCategory.SelectedValue);

            if (aDataTable.Rows.Count > 0)
            {
                txtTargetCategory.Text = aDataTable.Rows[0]["TargetCategory"].ToString();
                txtTotalTarget.Text = aDataTable.Rows[0]["TotalTargetByTp"].ToString();
                txtTotalTargetWithVAt.Text = aDataTable.Rows[0]["TotalTargetByTpVat"].ToString();

                loadGridView.DataSource = aDataTable;
                loadGridView.DataBind();

                for (int i = 0; i < aDataTable.Rows.Count; i++)
                {
                    TextBox Quantity = (TextBox) loadGridView.Rows[i].FindControl("TargetQty");
                    TextBox TargetValue = (TextBox) loadGridView.Rows[i].FindControl("TargetValue");
                    TextBox TargetWithVAT = (TextBox) loadGridView.Rows[i].FindControl("TargetWithVAT");

                    if (aDataTable.Rows[i]["TargetQty"].ToString() != "")
                    {
                        Quantity.Text = aDataTable.Rows[i]["TargetQty"].ToString();
                        TargetValue.Text = aDataTable.Rows[i]["TargetValue"].ToString();
                        TargetWithVAT.Text = aDataTable.Rows[i]["TargetWithVAT"].ToString();
                    }
                    else
                    {
                        Quantity.Text = "";
                        TargetValue.Text = "";
                        TargetWithVAT.Text = "";
                    }

                }
            }
        }
    }
}