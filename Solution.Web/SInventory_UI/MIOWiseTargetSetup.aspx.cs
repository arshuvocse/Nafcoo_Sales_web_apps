using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.SInventory_DAL;
using Library.DAO.SInventory_Entities;
using SalesSolution.Web.Models;
//using DataTable = DocumentFormat.OpenXml.Drawing.Charts.DataTable;

public partial class SInventory_UI_MIOWiseTargetSetup : System.Web.UI.Page
{

    MIOWiseTargetSetupDal aSetupDal = new MIOWiseTargetSetupDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetMonthList(periodDropDownList);
        }
    }

    public void GetMonthList(DropDownList ddl)
    {
        DateTime month = Convert.ToDateTime(DateTime.Now);
        for (int i = 0; i < 12; i++)
        {
            DateTime NextMont = month.AddMonths(i);
            ListItem list = new ListItem();
            list.Text = NextMont.ToString("MMMM");
            list.Value = NextMont.ToString("MMMM");
            ddl.Items.Add(list);
        }

        var a = DateTime.Now.Month.ToString();
        //ddl.Items.Insert(0, "Select Month");
        ddl.Items.FindByValue(DateTime.Now.ToString("MMMM")).Selected = true;
    }

    private void LoadMIOInfo()
    {
        DataTable aTable = new DataTable();

        aSetupDal.CreateConnection_DAL();


        aSetupDal.CloseAllConnection_DAL();
    }

    protected void btnUpload_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (id_fu.PostedFile.FileName != "")
            {
                ExcelToGrid();
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Excel file is not a correct format !" + "','Faild');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Excel file is not a correct format !" + "','Faild');", true);
        }
    }

    private void ExcelToGrid()
    {

        lbl_up_status.CssClass = "";
        string FileName = Path.GetFileName(id_fu.PostedFile.FileName);
        string Extension = Path.GetExtension(id_fu.PostedFile.FileName);
        string FilePath = "~/ExcelFiles/" + id_fu.FileName;
        id_fu.SaveAs(MapPath(FilePath));

        string path = System.IO.Path.GetFullPath(Server.MapPath(FilePath));
        OleDbConnection oledbConn = null;

        if (Path.GetExtension(path) == ".xls")
        {
            oledbConn =
                new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path +
                                    ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
        }
        else if (Path.GetExtension(path) == ".xlsx")
        {
            oledbConn =
                new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path +
                                    ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
        }

        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        System.Data.DataTable dt = new System.Data.DataTable();
        cmdExcel.Connection = oledbConn;

        oledbConn.Open();
        System.Data.DataTable dtExcelSchema;
        dtExcelSchema = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        oledbConn.Close();

        oledbConn.Open();
        cmdExcel.CommandText = "SELECT  * From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        oledbConn.Close();

        System.Data.DataTable destinationTable = new System.Data.DataTable();
        destinationTable = dt.Clone();

        foreach (DataRow row in dt.Rows)
        {
            if (!string.IsNullOrEmpty(row[0].ToString()))
            {
                destinationTable.ImportRow(row);
            }
        }
        string fileName = Path.GetFileName(FilePath);
        //txtSheetName.Text = fileName;

        productGridView.DataSource = destinationTable;
        productGridView.DataBind();
        lbl_up_status.CssClass = "alert alert-info";

        lbl_up_status.Text = "File Name:" + fileName + " [ " + productGridView.Rows.Count.ToString() + " records Found!]";
        IsFileUploaded.Value = "true";

        for (int i = 0; i < productGridView.Rows.Count; i++)
        {

            TextBox categoryTextBox = (TextBox)productGridView.Rows[i].FindControl("targetCategoryTextBox");

            DataTable aTable = new DataTable();

            string category = categoryTextBox.Text.Trim();

            aTable = aSetupDal.ValidateCategoryList(category);

            if (aTable.Rows.Count == 0)
            {
                System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#F62217");
                productGridView.Rows[i].BackColor = col;
            }


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

    protected void submitButton_Click(object sender, EventArgs e)
    {
        MIOWiseTargetSetupDao aSetupDao = new MIOWiseTargetSetupDao();

        if (Validation())
        {

            MIOWiseTargetSetupMasterDao aMasterDao = new MIOWiseTargetSetupMasterDao();

            aMasterDao.MioTargetMasterId = 0;
            aMasterDao.Month = periodDropDownList.SelectedItem.Text.Trim();

            if (masterHiddenFieldId.Value == "")
            {
                aMasterDao.EntryBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aMasterDao.EntryDate = DateTime.Now;
            }
            //else
            //{
            //    aMasterDao.TopSheetGenReportId = Convert.ToInt32(masterHiddenFieldId.Value);
            //    aMasterDao.UpdateBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
            //    aMasterDao.UpdateDate = DateTime.Now;
            //}


            MIOWiseTargetSetupDao aDetaildao;
            List<MIOWiseTargetSetupDao> aList = new List<MIOWiseTargetSetupDao>();

            for (int i = 0; i < productGridView.Rows.Count; i++)
            {
                aDetaildao = new MIOWiseTargetSetupDao();

                TextBox categoryTextBox = (TextBox)productGridView.Rows[i].Cells[1].FindControl("targetCategoryTextBox");
                TextBox areaCodeTextBox = (TextBox)productGridView.Rows[i].Cells[1].FindControl("areaCodeTextBox");
                TextBox territoryTextBox = (TextBox)productGridView.Rows[i].Cells[1].FindControl("territoryTextBox");
                TextBox mioNameTextBox = (TextBox)productGridView.Rows[i].Cells[1].FindControl("mioNameTextBox");

                MIOWiseTargetSetupDao aDao = new MIOWiseTargetSetupDao();

                aDetaildao.AreaCode = areaCodeTextBox.Text.Trim();
                aDetaildao.TerritoryName = territoryTextBox.Text;
                aDetaildao.MioName = mioNameTextBox.Text;
                aDetaildao.TargetCategory = categoryTextBox.Text;

                aList.Add(aDetaildao);
            }

            ResultInfo Res = aSetupDal.SaveTopSheet(aMasterDao, aList);

            if (Res.isSuccess)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','MIOWiseTargetSetupView.aspx');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

            }

            
        }
    }

    private bool Validation()
    {
        if (periodDropDownList.SelectedValue == "")
        {
            ShowMessageBox("Please Select Target Month !!!");
            return false;
        }

        for (int i = 0; i < productGridView.Rows.Count; i++)
        {
            TextBox categoryTextBox = (TextBox)productGridView.Rows[i].Cells[1].FindControl("targetCategoryTextBox");

            if (categoryTextBox.Text == "")
            {
                ShowMessageBox("Please Select Target Category !!!");
                return false;
            }

        }

        return true;

    }

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void viewLinkButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MIOWiseTargetSetupView.aspx");
    }
}