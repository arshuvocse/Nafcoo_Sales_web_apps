using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.DoctorModule_DAL;
using Newtonsoft.Json;
using SalesSolution.Web.Models;

public partial class DoctorModule_UI_ExpenseType : System.Web.UI.Page
{
    //private static SetupDAL _setupDAL
    //;
    static SetupDAL _setupDAL=new SetupDAL();
        string DtlId = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable aDataTable = new DataTable();
            aDataTable.Columns.Add("ExpenseTypDetailsId");
            aDataTable.Columns.Add("FieldName");
            aDataTable.Columns.Add("IsRequied");
            gv_DA.DataSource = aDataTable;
            gv_DA.DataBind();

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                btnUpdate.Visible = true;

                id_mastetID.Value = Request.QueryString["id"];
               GetOneRecord(id_mastetID.Value);
            }
            else
            {
                btnSave.Visible = true;
            }
        }

    }
    private void GetOneRecord(string Id)
    {
        try
        {
            using (DataTable dt = _setupDAL.GetEditDataForId(Convert.ToInt32(Id)))
            {

                

                TxtName.Text = dt.Rows[0]["ExpenseTypeName"].ToString();
                hfForMe.Value = dt.Rows[0]["ExpenseTypDetailsIdStr"].ToString();

                try
                {
                  if(Convert.ToBoolean(dt.Rows[0]["ImageRequired"].ToString()) == true)
                    {
                        rbImgType.Items[0].Selected = true;
                    }
                    else
                    {
                        rbImgType.Items[1].Selected = true;

                    }
                }
                catch(Exception ex)
                {

                }
               
                try
                {
                    customSwitch1.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                }
                catch (Exception ex)
                {
                    customSwitch1.Checked = false;
                }





            }


            using (DataTable dtDetail = _setupDAL.Get_ExpenseTypeDetailsByExpenseId(Convert.ToInt32(Id)))
            {
                gv_DA.DataSource = dtDetail;
                gv_DA.DataBind();

            }


            



        }
        catch (Exception ex) { }
    }
    
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    
    


     

    protected void addButtonDA_Click(object sender, EventArgs e)
    {
        IsRequired.CssClass = "form-select form-select-sm mb-3 mySelect2";
        txtFieldName.CssClass = "form-control form-control-sm  mb-3";

        if (txtFieldName.Text != "")
        {
            Add();

        }
        else
        {
            txtFieldName.ToolTip = "please fill out this field";
            txtFieldName.CssClass = "form-control form-control-sm  mb-3";
            txtFieldName.Focus();

        }

    }

    public void Add()
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("ExpenseTypDetailsId");
        aDataTable.Columns.Add("FieldName");
        aDataTable.Columns.Add("IsRequied");


        DataRow dataRow = null;
        for (int i = 0; i < gv_DA.Rows.Count; i++)
        {
            HiddenField hfExpenseTypDetailsId = ((HiddenField)gv_DA.Rows[i].Cells[1].FindControl("hfExpenseTypDetailsId"));
            Label lbl_FieldName = ((Label)gv_DA.Rows[i].Cells[1].FindControl("lbl_FieldName"));
            Label lbl_IsRequied = ((Label)gv_DA.Rows[i].Cells[1].FindControl("lbl_IsRequied"));
            dataRow = aDataTable.NewRow();
            dataRow["ExpenseTypDetailsId"] = hfExpenseTypDetailsId.Value;
            dataRow["FieldName"] = lbl_FieldName.Text;
            dataRow["IsRequied"] = lbl_IsRequied.Text;


            aDataTable.Rows.Add(dataRow);
        }
        dataRow = aDataTable.NewRow();
        dataRow["ExpenseTypDetailsId"] = "0";
        dataRow["FieldName"] = txtFieldName.Text;
        dataRow["IsRequied"] = IsRequired.SelectedValue;


        aDataTable.Rows.Add(dataRow);
        gv_DA.DataSource = aDataTable;
        gv_DA.DataBind();
        txtFieldName.Text = string.Empty;
        IsRequired.SelectedIndex = 0;

    }

    
    public void Remove(int row)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("ExpenseTypDetailsId");
        aDataTable.Columns.Add("FieldName");
        aDataTable.Columns.Add("IsRequied");

        int count = 0;
        DataRow dataRow = null;
        for (int i = 0; i < gv_DA.Rows.Count; i++)
        {
            HiddenField hfExpenseTypDetailsId = ((HiddenField)gv_DA.Rows[i].Cells[1].FindControl("hfExpenseTypDetailsId"));
            DataTable dt = new DataTable();
            if (hfExpenseTypDetailsId.Value == "0")
            {
                
            }
            else
            {
                dt = _setupDAL.checkFroDelete(string.IsNullOrEmpty(hfExpenseTypDetailsId.Value) ? (int?)null : int.Parse(hfExpenseTypDetailsId.Value));
            }
           
            
                if (dt.Rows.Count == 0)
                {
                    if (i != row)
            {

                
                    if (dt.Rows.Count == 0)
                    {
                        Label lbl_FieldName = ((Label)gv_DA.Rows[i].Cells[1].FindControl("lbl_FieldName"));
                        Label lbl_IsRequied = ((Label)gv_DA.Rows[i].Cells[1].FindControl("lbl_IsRequied"));
                        dataRow = aDataTable.NewRow();
                        dataRow["ExpenseTypDetailsId"] = hfExpenseTypDetailsId.Value;
                        dataRow["FieldName"] = lbl_FieldName.Text;
                        dataRow["IsRequied"] = lbl_IsRequied.Text;

                        

                        aDataTable.Rows.Add(dataRow);
                    }
             
                }
                else
                {
                    if (hfExpenseTypDetailsId.Value != "0")
                    {
                        ResultInfo Res = _setupDAL.delExpensDetls(hfExpenseTypDetailsId.Value);
                    }
                }
                }
                else
                {
                    count++;
                    showMessageBox("Can not be deleted!");
                }
             
        }
       

        if (count == 0)
        {

            gv_DA.DataSource = aDataTable;
            gv_DA.DataBind();
        }

    }

    protected void deleteImageButton_Click(object sender, EventArgs e)
    {
        LinkButton ImageButton = (LinkButton)sender;
        GridViewRow currentRow = (GridViewRow)ImageButton.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        Remove(rowindex);
    }

    public bool Validation()
    {


        TxtName.CssClass = "form-control form-control-sm";
         


        if (TxtName.Text == "")
        {
            TxtName.ToolTip = "please fill out this field";
            TxtName.CssClass = "form-control form-control-sm is-invalid";
            TxtName.Focus();
            return false;
        }


        


        if (gv_DA.Rows.Count == 0)
        {
            showMessageBox("please Add to List One Row");

            return false;
        }


        

        return true;
    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (Validation())
        {

            List<ExpenseTypeDetails> MarketList = new List<ExpenseTypeDetails>();


            for (int i = 0; i < gv_DA.Rows.Count; i++)
            {
                HiddenField hfExpenseTypDetailsId = ((HiddenField)gv_DA.Rows[i].Cells[1].FindControl("hfExpenseTypDetailsId"));
                Label lbl_FieldName = ((Label)gv_DA.Rows[i].Cells[1].FindControl("lbl_FieldName"));
                Label lbl_IsRequied = ((Label)gv_DA.Rows[i].Cells[1].FindControl("lbl_IsRequied"));



                ExpenseTypeDetails _DAO = new ExpenseTypeDetails();
                 
 
                _DAO.ExpenseTypDetailsId = string.IsNullOrEmpty(hfExpenseTypDetailsId.Value) ? (int?)null : int.Parse(hfExpenseTypDetailsId.Value);
                _DAO.FieldName = string.IsNullOrEmpty(lbl_FieldName.Text) ? null : lbl_FieldName.Text;
                _DAO.IsRequied =  Convert.ToBoolean(lbl_IsRequied.Text);
           








                MarketList.Add(_DAO);

            }

           


           
            ExpenseTypeMaster aMaster = new ExpenseTypeMaster();

            aMaster.ExpenseTypeId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);

            aMaster.ExpenseTypeName = string.IsNullOrEmpty(TxtName.Text) ? null : TxtName.Text;

            if (rbImgType.Items[0].Selected)
            {
                aMaster.ImageRequired = true;
            }
            else
            {
                aMaster.ImageRequired = false;

            }
            aMaster.IsActive = customSwitch1.Checked;
             




            ResultInfo Res = _setupDAL.SaveExpenseTypeMaster(aMaster, MarketList, Session["UserId"].ToString());
            if (Res.isSuccess == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','ExpenseTypeView.aspx');", true);

            }

            else if (Res.isDuplicateCheck == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

            }

            else if (Res.isValiCheck == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Data cannot be deactivated!" + "','Faild');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

            }

        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {

    }
}