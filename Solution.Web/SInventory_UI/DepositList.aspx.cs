using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Library.BLL.SInventory_BLL;
using Library.DAL.SInventory_DAL;
using Library.DAO.SInventory_Entities;

public partial class SInventory_UI_DepositList : System.Web.UI.Page
{
    CompanywisebranchDal aDal = new CompanywisebranchDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fromDateTextBox.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            toDateTextBox.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            Loaddropdownlist();
        }
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
    private void Loaddropdownlist()
    {
        aDal.LoadCompany(companyNameDropDownList, Session["UserId"].ToString());

        if (companyNameDropDownList.Items.Count == 2)
        {
            companyNameDropDownList.SelectedIndex = 1;
            
        }
    }

    protected void clearButton_OnClick(object sender, EventArgs e)
    {
        Loaddropdownlist();
        fromDateTextBox.Text = "";
        toDateTextBox.Text = "";

        invoiceGridView.DataSource = null;
        invoiceGridView.DataBind();
    }

    private void LoadInfo()
    {
        if (Validation())
        {
            DataTable aTable = aDal.LoadDepositList(GenerateParameter());

            if (aTable.Rows.Count > 0)
            {
                invoiceGridView.DataSource = aTable;
                invoiceGridView.DataBind();

                decimal total = aTable.AsEnumerable().Sum(row => row.Field<decimal?>("Amount") == null ? 0 : row.Field<decimal>("Amount"));
                 
                lblCount.Text = "Total Amount: " + total.ToString();

                for (int i = 0; i < invoiceGridView.Rows.Count; i++)
                {
                    //if (Session["LoginName"].ToString() == "51419")
                    //{

                      
                    //    ((ImageButton)invoiceGridView.Rows[i].Cells[6].FindControl("editImageButton")).Visible =
                    //       true;
                    //    ((ImageButton)invoiceGridView.Rows[i].Cells[6].FindControl("editImageButton")).ToolTip =
                    //        "";
                    //}
                    //else
                    //{

                    //    invoiceGridView.HeaderRow.Cells[15].Visible = false;
                    //    invoiceGridView.Rows[i].Cells[15].Visible = false;
                       
                    //    ((ImageButton)invoiceGridView.Rows[i].Cells[6].FindControl("editImageButton")).Visible =
                    //       false;
                    //    ((ImageButton)invoiceGridView.Rows[i].Cells[6].FindControl("editImageButton")).ToolTip =
                    //        "You cann't delete !!";
                    //}

                    if (Session["RoleTypeName"].ToString().Trim() == "Admin")
                    {

                        invoiceGridView.HeaderRow.Cells[15].Visible = true;
                        invoiceGridView.Rows[i].Cells[15].Visible = true;

                        invoiceGridView.HeaderRow.Cells[14].Visible = true;
                        invoiceGridView.Rows[i].Cells[14].Visible = true;

                        //((ImageButton)invoiceGridView.Rows[i].FindControl("ImageButton1")).Visible =
                        //    true;
                    }
                    else
                    {
                        //invoiceGridView.HeaderRow.Cells[14].Visible = false;
                        //((ImageButton)invoiceGridView.Rows[i].FindControl("ImageButton1")).Visible =
                        //    false;

                        invoiceGridView.HeaderRow.Cells[14].Visible = false;
                        invoiceGridView.Rows[i].Cells[14].Visible = false;

                        invoiceGridView.HeaderRow.Cells[15].Visible = false;
                        invoiceGridView.Rows[i].Cells[15].Visible = false;
                       

                    }
                }
            }
            else
            {
                invoiceGridView.DataSource = null;
                invoiceGridView.DataBind();
            }
        }
    }

    protected void Button_Click(object sender, EventArgs e)
    {
        LoadInfo();
    }

    private bool Validation()
    {
        //if (companyNameDropDownList.SelectedValue == "")
        //{
        //    ShowMessageBox("Please select sales center !!");
        //    return false;
        //}
        
        if (fromDateTextBox.Text == "")
        {
            ShowMessageBox("Please select from date !!");
            return false;
        }

        if (toDateTextBox.Text == "")
        {
            ShowMessageBox("Please select to date !!");
            return false;
        }

        

        return true;
    }

    private string GenerateParameter()
    {
        string param = "";

        if (fromDateTextBox.Text.Trim() != "")
        {
            if (toDateTextBox.Text.Trim() != "")
            {
                param = param + " AND DP.DepositDate BETWEEN '" +
                        fromDateTextBox.Text.Trim() + "' AND '" + toDateTextBox.Text.Trim() + "'";
            }
        }

        if (companyNameDropDownList.SelectedValue != "")
        {
            param = param + " AND CI.ComUnitId = '" + companyNameDropDownList.SelectedValue + "'";
        }


        param = param + " AND CI.ComUnitId IN (SELECT CompanyUnitId FROM tblUserCompanyUnit WHERE UserId =" + Convert.ToInt32(Session["UserId"]) + ")";
       

        return param;
    }

    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string depositId = invoiceGridView.DataKeys[rowindex][0].ToString();
            if (Session["RoleTypeName"].ToString().Trim() == "Admin")
            {
                if (depositId != null)
                {
                    CompanyWiseDepositDao aDao = new CompanyWiseDepositDao();

                    aDao.IsDelete = true;
                    aDao.DeleteBy = Session["LoginName"].ToString();
                    aDao.DeleteDate = DateTime.Now;
                    aDao.DepositId = Convert.ToInt32(depositId);

                    aDal.UpdateDepositInfo(aDao);
                }
            }
            else
            {
                ShowMessageBox("You can't delete deposit slip !!");
            }
        }


        if (e.CommandName == "UpdateData")
        {

            if (Session["RoleTypeName"].ToString().Trim() == "Admin")
            {
                int rowindex = Convert.ToInt32(e.CommandArgument);
                string depositId = invoiceGridView.DataKeys[rowindex][0].ToString();

                if (depositId != null)
                {
                   
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('CompanyWiseBranch.aspx?mid=" + depositId + "' ,'_blank');", true);
                }
            }
            
        }

    }
    protected void ListImageButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("CompanyWiseBranch.aspx");
    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void ListImageButton_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CompanyWiseBranch.aspx");
    }
   
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (invoiceGridView.Rows.Count > 0)
        {
            string attachment = "attachment; filename=Deposit.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            invoiceGridView.AllowPaging = false;



            //loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
            //            false;
            //loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
            //   false;
            //loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
            //   false;

            this.LoadInfo();

            // Create a form to contain the grid  
            HtmlForm frm = new HtmlForm();
            invoiceGridView.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(loadGridView);
            //frm.RenderControl(htw);

            invoiceGridView.HeaderRow.Style.Add("background-color", "#E5EEF1");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in invoiceGridView.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in invoiceGridView.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }


            invoiceGridView.RenderControl(htw);
            string headerTable = @"<span  style='text-align:left'><h4>From Date : " + fromDateTextBox.Text + "</h4> <h4> To Date : " + toDateTextBox.Text + "</h4>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
   <h3>Deposit List Report	</h3>

</span>";

            HttpContext.Current.Response.Write(headerTable);
            HttpContext.Current.Response.Write(SubTi);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            showMessageBox("No Data Found!!");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
}