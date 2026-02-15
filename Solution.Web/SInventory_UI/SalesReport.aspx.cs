using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.BLL.SInventory_BLL;
using Library.DAL.MasterSetup_DAL;
using OfficeOpenXml;
using System.IO;
using ClosedXML.Excel;
using System.Text;
using Library.DAL.SInventory_DAL;

public partial class SInventory_UI_ProformaReport : System.Web.UI.Page
{
    private static CmnCrystaltoView _DAL = new CmnCrystaltoView();
    private DropDownList GroupSelect, ZoneSelect, AreaSelect, TeritorySelect, SubTeritory, MarketSelect;

    SalesReportDal aDal = new SalesReportDal();
    CommonStructureDal aStructureDal = new CommonStructureDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        GroupSelect = (DropDownList)IVMarketStructure.FindControl("GroupSelect") as DropDownList;
        ZoneSelect = (DropDownList)IVMarketStructure.FindControl("ZoneSelect") as DropDownList;
        AreaSelect = (DropDownList)IVMarketStructure.FindControl("AreaSelect") as DropDownList;
        TeritorySelect = (DropDownList)IVMarketStructure.FindControl("TeritorySelect") as DropDownList;
        SubTeritory = (DropDownList)IVMarketStructure.FindControl("SubTeritory") as DropDownList;
        MarketSelect = (DropDownList)IVMarketStructure.FindControl("MarketSelect") as DropDownList;
        if (!IsPostBack)
        {
            fromDateTextBox.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            todateTextBox.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            LoadDropDown();
            LoadData();
        }
    }
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    public void LoadDropDown()
    {
        try
        {
            OtherStockActionBLL aOtherStockActionBLL = new OtherStockActionBLL();
            aOtherStockActionBLL.DCLoad(dcDropDownList1);
        }
        catch { }

        try
        {
            using (DataTable aTable = aDal.LoadClusterHead())
            {
                ddlClusterHead.DataSource = aTable;
                ddlClusterHead.DataValueField = "ValueField";
                ddlClusterHead.DataTextField = "TextField";
                ddlClusterHead.DataBind();
                ddlClusterHead.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                ddlClusterHead.SelectedIndex = 0;
            }
        }
        catch { }

        try
        {
            using (DataTable aTable = aStructureDal.LoadGroup())
            {
                ddlGroup.DataSource = aTable;
                ddlGroup.DataValueField = "ValueField";
                ddlGroup.DataTextField = "TextField";
                ddlGroup.DataBind();
                ddlGroup.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                ddlGroup.SelectedIndex = 0;
            }
        }
        catch { }

        try
        {
            using (DataTable aTable = aStructureDal.LoadClusterMeansRegion())
            {
                ddlCluster.DataSource = aTable;
                ddlCluster.DataValueField = "ValueField";
                ddlCluster.DataTextField = "TextField";
                ddlCluster.DataBind();
                //ddlCluster.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                //ddlCluster.SelectedIndex = 0;
            }
        }
        catch { }
    }

    protected void ddlCluster_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlCluster.SelectedValue != "")
            {
                using (DataTable aTable = aStructureDal.LoadRegionMeansAreaByClusterId(Convert.ToInt32(ddlCluster.SelectedValue)))
                {
                    ddlRegion.DataSource = aTable;
                    ddlRegion.DataValueField = "ValueField";
                    ddlRegion.DataTextField = "TextField";
                    ddlRegion.DataBind();
                    ddlRegion.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                    ddlRegion.SelectedIndex = 0;
                }
            }
            else
            {
                showMessageBox("Please select cluster !!!");
            }

        }
        catch { }
    }

    protected void ddlRegion_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlRegion.SelectedValue != "")
            {
                using (DataTable aTable = aStructureDal.LoadAreaMeansTerritoryByRegionId(Convert.ToInt32(ddlRegion.SelectedValue)))
                {
                    ddlArea.DataSource = aTable;
                    ddlArea.DataValueField = "ValueField";
                    ddlArea.DataTextField = "TextField";
                    ddlArea.DataBind();
                    ddlArea.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                    ddlArea.SelectedIndex = 0;
                }
            }
            else
            {
                showMessageBox("Please select region !!!");
            }


        }
        catch { }
    }

    protected void ddlArea_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlArea.SelectedValue != "")
            {
                using (DataTable aTable = aStructureDal.LoadTerritoryMeansSubTerritoryByAreaId(Convert.ToInt32(ddlArea.SelectedValue)))
                {
                    ddlTerritory.DataSource = aTable;
                    ddlTerritory.DataValueField = "ValueField";
                    ddlTerritory.DataTextField = "TextField";
                    ddlTerritory.DataBind();
                    ddlTerritory.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                    ddlTerritory.SelectedIndex = 0;
                }
            }
            else
            {
                showMessageBox("Please select team !!!");
            }


        }
        catch { }
    }
    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //loadGridView.PageIndex = e.NewPageIndex;
        //this.LoadData();
    }
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalesReport.aspx");
    }
    protected void SearchButton_Click(object sender, EventArgs e)
    {

        //Session["SalesReport"] = Parm();

        ////if (rptTypeDropDownList.SelectedValue == "SCW")

        //string url = "../SInventory_RPTVIEW/UpdatedSalesReportViewer.aspx";
        //// string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        //string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
        //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);


       LoadData();

        //Session["ProformaReport"] = "";
        //Session["ProformaReport"] = 0;

        //if (InvoiceDateTextBox.Text != "" && todateTextBox.Text != "" && dcDropDownList1.SelectedValue != "")
        //{
        //    if (todateTextBox.Text == "")
        //    {
        //        InvoiceDateTextBox.Text = todateTextBox.Text;
        //    }

        //    string fromDate = InvoiceDateTextBox.Text;
        //    string toDate = todateTextBox.Text;
        //    string districtId = dcDropDownList1.SelectedValue;

        //    string url = "../SInventory_RPTVIEW/ProformaReportViewer.aspx?fromDate=" + fromDate + "&toDate=" + toDate + "&districtId=" + districtId;
        //    // string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        //    string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        //}
        //if (CheckBox1.Checked && todateTextBox.Text != "" && InvoiceDateTextBox.Text != "")
        //{
        //    int i = 1;
        //    string fromDate = InvoiceDateTextBox.Text;
        //    string toDate = todateTextBox.Text;
        //    string url = "../SInventory_RPTVIEW/ProformaReportViewer.aspx?fromDate=" + fromDate + "&toDate=" + toDate + "&NationalReport=" + 1;
        //    // string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        //    string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        //}
    }

    private void LoadData()
    {

        DataTable comUnitDetailDataTable = new DataTable();


        string parameter2 = "";

        if (ddlCluster.SelectedValue != "")
        {
            string pram = "";

            foreach (ListItem item in ddlCluster.Items)
            {
                if (item.Selected)
                {
                    string[] words = (item.Text.Trim()).Split(':');
                    pram = pram + "'" + words[0].Trim() + "',";
                }
            }

            if (pram != "")
            {
                parameter2 = " AND CLSH.ClusterCode IN (" + pram.Remove(pram.Length - 1) + ")";
            }
        }

        if (ddlRegion.SelectedValue != "")
        {
            string pram = "";

            foreach (ListItem item in ddlRegion.Items)
            {
                if (item.Selected)
                {
                    string[] words = (item.Text.Trim()).Split(':');
                    pram = pram + "'" + words[0].Trim() + "',";
                }
            }


            if (pram != "")
            {
                parameter2 = parameter2 + " AND  CLSH.RegionCode IN (" + pram.Remove(pram.Length - 1) + ")";
            }

        }

        if (ddlArea.SelectedValue != "")
        {
            string pram = "";

            foreach (ListItem item in ddlArea.Items)
            {
                if (item.Selected)
                {
                    string[] words = (item.Text.Trim()).Split(':');
                    pram = pram + "'" + words[0].Trim() + "',";
                }
            }

            if (pram != "")
            {
                parameter2 = parameter2 + " AND CLSH.AreaCode IN (" + pram.Remove(pram.Length - 1) + ")";
            }

        }

        if (ddlTerritory.SelectedValue != "")
        {
            string pram = "";

            foreach (ListItem item in ddlTerritory.Items)
            {
                if (item.Selected)
                {
                    string[] words = (item.Text.Trim()).Split(':');
                    pram = pram + "'" + words[0].Trim() + "',";
                }
            }

            if (pram != "")
            {
                parameter2 = parameter2 + " AND CLSH.TerritoryCode IN (" + pram.Remove(pram.Length - 1) + ")";
            }

        }

        string param3 = "";

        if (fromDateTextBox.Text != "")
        {
            DataTable dt = aDal.CheckTargetMonth("AND TG.Month = DATENAME(month,'" + Convert.ToDateTime(fromDateTextBox.Text) + "')");

            if (dt.Rows.Count > 0)
            {
                param3 = param3 + " AND Month = DATENAME(month,'" + Convert.ToDateTime(fromDateTextBox.Text) + "')";
            }
        }


        comUnitDetailDataTable = aDal.GetSalesReportByMBE(Parm(), parameter2, param3);


        //if (ddlTerritory.SelectedValue != "")
        //{
        //    string pram = "";

        //    foreach (ListItem item in ddlTerritory.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            string[] words = (item.Text.Trim()).Split(':');
        //            pram = pram + "'" + words[0].Trim() + "',";
        //        }
        //    }

        //    string parameter = "";

        //    if (pram != "")
        //    {
        //        parameter = " AND CLSH.TerritoryCode IN (" + pram.Remove(pram.Length - 1) + ")";
        //    }


        //    comUnitDetailDataTable = aDal.GetSalesReportByMBE(Parm(), parameter);
        //}
        //else if (ddlArea.SelectedValue != "")
        //{
        //    string pram = "";

        //    foreach (ListItem item in ddlArea.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            string[] words = (item.Text.Trim()).Split(':');
        //            pram = pram + "'" + words[0].Trim() + "',";
        //        }
        //    }

        //    string parameter = "";

        //    if (pram != "")
        //    {
        //        parameter = " AND CLSH.AreaCode IN (" + pram.Remove(pram.Length - 1) + ")";
        //    }


        //    comUnitDetailDataTable = aDal.GetSalesReportByABM(Parm(), parameter);
        //}

        //else if (ddlRegion.SelectedValue != "")
        //{
        //    string pram = "";

        //    foreach (ListItem item in ddlRegion.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            string[] words = (item.Text.Trim()).Split(':');
        //            pram = pram + "'" + words[0].Trim() + "',";
        //        }
        //    }

        //    string parameter = "";

        //    if (pram != "")
        //    {
        //        parameter = " AND  CLSH.RegionCode IN (" + pram.Remove(pram.Length - 1) + ")";
        //    }


        //    comUnitDetailDataTable =  aDal.GetSalesReportByRBM(Parm(), parameter);
        //}
        //else
        //{
        //    string pram = "";

        //    foreach (ListItem item in ddlCluster.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            string[] words = (item.Text.Trim()).Split(':');
        //            pram = pram + "'" + words[0].Trim() + "',";
        //        }
        //    }

        //    string parameter = "";

        //    if (pram != "")
        //    {
        //        parameter = " AND CLSH.ClusterCode IN (" + pram.Remove(pram.Length - 1) + ")";
        //    }


        //    comUnitDetailDataTable = aDal.GetSalesReportByClusterHead(Parm(), parameter);
        //}
    
        //comUnitDetailDataTable = aDal.GetSalesData(Parm(), Pram2());

        //comUnitDetailDataTable = aDal.GetSalesReportByClusterHead(Parm());

        if (comUnitDetailDataTable.Rows.Count > 0)
        {
            loadGridView.DataSource = comUnitDetailDataTable;
            loadGridView.DataBind();


            decimal totalOrderValue = comUnitDetailDataTable.AsEnumerable().Sum(row => row.Field<decimal>("OrderValue"));
            
            loadGridView.FooterRow.Cells[5].Text = "Total: ";
            loadGridView.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            loadGridView.FooterRow.Cells[5].Font.Bold = true;
            loadGridView.FooterRow.Cells[6].Text = totalOrderValue.ToString("N2");
            loadGridView.FooterRow.Cells[6].Font.Bold = true;

            decimal totalProformaValue = comUnitDetailDataTable.AsEnumerable().Sum(row => row.Field<decimal>("ProformaValue"));
            loadGridView.FooterRow.Cells[7].Text = totalProformaValue.ToString("N2");
            loadGridView.FooterRow.Cells[7].Font.Bold = true;

            decimal totalInvoiceValue = comUnitDetailDataTable.AsEnumerable().Sum(row => row.Field<decimal>("InvoiceValue"));
            loadGridView.FooterRow.Cells[8].Text = totalInvoiceValue.ToString("N2");
            loadGridView.FooterRow.Cells[8].Font.Bold = true;

            decimal totalReturnValue = comUnitDetailDataTable.AsEnumerable().Sum(row => row.Field<decimal>("ReturnValue"));
            loadGridView.FooterRow.Cells[9].Text = totalReturnValue.ToString("N2");
            loadGridView.FooterRow.Cells[9].Font.Bold = true;

            decimal returnPer = 0;

            if (totalReturnValue > 0)
            {
                returnPer = (totalReturnValue*100)/(totalInvoiceValue + totalReturnValue);
            }
            loadGridView.FooterRow.Cells[10].Text = returnPer.ToString("N2");
            loadGridView.FooterRow.Cells[10].Font.Bold = true;

            decimal totalOnDelivery = comUnitDetailDataTable.AsEnumerable().Sum(row => row.Field<decimal>("OnDelivery"));
            loadGridView.FooterRow.Cells[11].Text = totalOnDelivery.ToString("N2");
            loadGridView.FooterRow.Cells[11].Font.Bold = true;

            decimal totalCreditAmount = comUnitDetailDataTable.AsEnumerable().Sum(row => row.Field<decimal>("CreditAmount"));
            loadGridView.FooterRow.Cells[12].Text = totalCreditAmount.ToString("N2");
            loadGridView.FooterRow.Cells[12].Font.Bold = true;

            decimal totalCollectionAmount = comUnitDetailDataTable.AsEnumerable().Sum(row => row.Field<decimal>("CollectionAmount"));
            loadGridView.FooterRow.Cells[13].Text = totalCollectionAmount.ToString("N2");
            loadGridView.FooterRow.Cells[13].Font.Bold = true;

            decimal totalTargetValue = comUnitDetailDataTable.AsEnumerable().Sum(row => row.Field<decimal>("TargetValue"));
            loadGridView.FooterRow.Cells[14].Text = totalTargetValue.ToString("N2");
            loadGridView.FooterRow.Cells[14].Font.Bold = true;

            decimal achivementPer = 0;

            if (totalTargetValue > 0)
            {
                achivementPer = (totalCollectionAmount * 100) / totalTargetValue;
            }
            loadGridView.FooterRow.Cells[15].Text = achivementPer.ToString("N2");
            loadGridView.FooterRow.Cells[15].Font.Bold = true;

        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
        }
    }

    private String Pram2()
    {
        string param = "";

        if (fromDateTextBox.Text != "" && todateTextBox.Text != "")
        {
            param = param + " AND CONVERT(date,SubmissionDate)  BETWEEN '" + fromDateTextBox.Text + "' AND '" + todateTextBox.Text + "' ";
        }
        
        return param;

    }

    private string Parm()
    {
        
        string param = "";
        
       

        if (fromDateTextBox.Text != "" && todateTextBox.Text != "")
        {
            param = param + " AND CONVERT(date,InvoiceDate)  BETWEEN '" + fromDateTextBox.Text + "' AND '" + todateTextBox.Text + "' ";
        }
        if (InvoiceDateTextBox.Text != "" && todateTextBox.Text == "")
        {
            param = param + " AND CONVERT(date,SLS.InvoiceDate)  BETWEEN '" + fromDateTextBox.Text + "' AND '" + DateTime.Now + "' ";
        }


        //if (fromDateTextBox.Text != "")
        //{
        //    DataTable dt = aDal.CheckTargetMonth("AND TG.Month = DATENAME(month,'" + Convert.ToDateTime(fromDateTextBox.Text) + "')");

        //    if (dt.Rows.Count > 0)
        //    {
        //        param = param + " AND Month = DATENAME(month,'" + Convert.ToDateTime(fromDateTextBox.Text) + "')";
        //    }     
        //}



        if (ddlGroup.SelectedValue != "")
        {
            param = param + " AND SLS.GroupId='" + ddlGroup.SelectedValue + "' ";
        }

        if (ddlCluster.SelectedValue != "")
        {
            string pram = "";

            foreach (ListItem item in ddlCluster.Items)
            {
                if (item.Selected)
                {
                    pram = pram + item.Value + ",";
                }
            }

            param = param + " AND SLS.RegionId IN (" + pram.Remove(pram.Length - 1) + ")";

            //param = param + " AND SLS.RegionId='" + ddlCluster.SelectedValue + "' ";
        }

        if (ddlRegion.SelectedValue != "")
        {

            string pram = "";

            foreach (ListItem item in ddlRegion.Items)
            {
                if (item.Selected)
                {
                    pram = pram + item.Value + ",";
                }
            }

            param = param + " AND SLS.AreaId IN (" + pram.Remove(pram.Length - 1) + ")";
            //param = param + " AND SLS.AreaId='" + ddlRegion.SelectedValue + "' ";
        }

        if (ddlArea.SelectedValue != "")
        {
            string pram = "";

            foreach (ListItem item in ddlArea.Items)
            {
                if (item.Selected)
                {
                    pram = pram + item.Value + ",";
                }
            }

            param = param + " AND SLS.TerritoryId IN (" + pram.Remove(pram.Length - 1) + ")";

            //param = param + " AND SLS.TerritoryId='" + ddlArea.SelectedValue + "' ";
        }

        if (ddlTerritory.SelectedValue != "")
        {
            string pram = "";

            foreach (ListItem item in ddlTerritory.Items)
            {
                if (item.Selected)
                {
                    pram = pram + item.Value + ",";
                }
            }

            param = param + " AND SLS.SubTerritoryId IN (" + pram.Remove(pram.Length - 1) + ")";

            //param = param + " AND SLS.SubTerritoryId='" + ddlTerritory.SelectedValue + "' ";
        }

        if (MarketSelect.SelectedValue != "")
        {
            param = param + " AND SLS.MarketId='" + MarketSelect.SelectedValue + "' ";
        }

        if (ddlClusterHead.SelectedValue != "")
        {
            param = param + " AND SLS.RSMId='" + ddlClusterHead.SelectedValue + "' ";
        }

        if (ddlRsm.SelectedValue != "")
        {
            param = param + " AND SLS.ASMId='" + ddlRsm.SelectedValue + "' ";
        }

        if (ddlASM.SelectedValue != "")
        {
            param = param + " AND SLS.MIOID='" + ddlASM.SelectedValue + "' ";
        }

        if (ddlMbe.SelectedValue != "")
        {
            param = param + " AND SLS.MBEEmpInfoId='" + ddlMbe.SelectedValue + "' ";
        }


        return param;
    }
    
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        OtherStockActionBLL aOtherStockActionBLL = new OtherStockActionBLL();
        aOtherStockActionBLL.DCLoad(dcDropDownList1);
         
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (loadGridView.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Invoice_Report_List_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".csv");
            Response.Charset = "";
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.Default;
            //To Export all pages.
            loadGridView.AllowPaging = false;
            this.LoadData();

            StringBuilder sb = new StringBuilder();
            foreach (TableCell cell in loadGridView.HeaderRow.Cells)
            {
                //Append data with separator.
                sb.Append(HttpUtility.HtmlDecode(cell.Text) + ',');
            }
            //Append new line character.
            sb.Append("\r\n");

            foreach (GridViewRow row in loadGridView.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    //Append data with separator.
                    if (cell.Text.Contains(","))
                    {
                        sb.Append(String.Format("\"{0}\",", cell.Text));
                    }
                    else
                    { sb.Append(HttpUtility.HtmlDecode(cell.Text) + ','); }
                }
                //Append new line character.
                sb.Append("\r\n");
            }

            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "No Data Found!" + "','Faild');", true);

        }

        if (loadGridView.Rows.Count > 0)
        {
            DataTable dt = new DataTable("GridView_Data");
            foreach (TableCell cell in loadGridView.HeaderRow.Cells)
            {
                dt.Columns.Add(cell.Text);
            }
            loadGridView.AllowPaging = false;
            this.LoadData();
            foreach (GridViewRow row in loadGridView.Rows)
            {
                dt.Rows.Add();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (row.Cells[i].Controls.Count > 0)
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = (row.Cells[i].Controls[1] as Label).Text;
                    }
                    else
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = row.Cells[i].Text;
                    }
                }
            }
            loadGridView.AllowPaging = false;
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Invoice_Report_List (Date Range= " + InvoiceDateTextBox.Text + "-" + todateTextBox.Text + ").xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        if (loadGridView.Rows.Count > 0)
        {


            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Invoice_Report_List_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            loadGridView.AllowPaging = false;

            this.LoadData();
            //Change the Header Row back to white color
            loadGridView.HeaderRow.Style.Add("background-color", "#FFFFFF");
            
            string headerTable = @"<span  style='text-align:center'><h3>  Invoice Report   (Date Range : " + InvoiceDateTextBox.Text + "- " + todateTextBox.Text + ") </h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("MMMM dd, yyyy") + "</h4></span>";

            HttpContext.Current.Response.Write(headerTable);

            loadGridView.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            showMessageBox("No Data Found!!");
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

    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
        //confirms that an HtmlForm control is rendered for the
        //specified ASP.NET server control at run time.
    }
    protected void viewRptButton_Click(object sender, EventArgs e)
    {
        Session["ProformaReport"] = "";
        Session["ProformaReport"] = 1;

        if (InvoiceDateTextBox.Text != "" && todateTextBox.Text != "" && dcDropDownList1.SelectedValue != "")
        {
            if (todateTextBox.Text == "")
            {
                InvoiceDateTextBox.Text = todateTextBox.Text;
            }

            string fromDate = InvoiceDateTextBox.Text;
            string toDate = todateTextBox.Text;
            string districtId = dcDropDownList1.SelectedValue;

            string url = "../SInventory_RPTVIEW/ProformaReportViewer.aspx?fromDate=" + fromDate + "&toDate=" + toDate + "&districtId=" + districtId;
            // string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
            string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }
        //if (CheckBox1.Checked && todateTextBox.Text != "" && InvoiceDateTextBox.Text != "")
        //{
        //    int i = 1;
        //    string fromDate = InvoiceDateTextBox.Text;
        //    string toDate = todateTextBox.Text;
        //    string url = "../SInventory_RPTVIEW/ProformaReportViewer.aspx?fromDate=" + fromDate + "&toDate=" + toDate + "&NationalReport=" + 1;
        //    // string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        //    string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        //}
    }

    protected void fromDateTextBox_TextChanged(object sender, EventArgs e)
    {
        DateTime Fromd = Convert.ToDateTime("01-Apr-2022");
        DateTime inputDateTime = Convert.ToDateTime(InvoiceDateTextBox.Text);
        if (inputDateTime < Fromd)
        {
            InvoiceDateTextBox.Text = DateTime.Now.ToString("01 April, 2022");
        }
    }
    
    protected void ddlClusterHead_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlClusterHead.SelectedValue != "")
            {
                using (DataTable aTable = aDal.LoadRsmByClusterHead(Convert.ToInt32(ddlClusterHead.SelectedValue)))
                {
                    ddlRsm.DataSource = aTable;
                    ddlRsm.DataValueField = "ValueField";
                    ddlRsm.DataTextField = "TextField";
                    ddlRsm.DataBind();
                    ddlRsm.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                    ddlRsm.SelectedIndex = 0;
                }
            }
            else
            {
                showMessageBox("Please select Cluster head !!!");
            }

            
        }
        catch { }
    }

    protected void ddlRsm_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlRsm.SelectedValue != "")
            {
                using (DataTable aTable = aDal.LoadAsmByRsm(Convert.ToInt32(ddlRsm.SelectedValue)))
                {
                    ddlASM.DataSource = aTable;
                    ddlASM.DataValueField = "ValueField";
                    ddlASM.DataTextField = "TextField";
                    ddlASM.DataBind();
                    ddlASM.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                    ddlASM.SelectedIndex = 0;
                }
            }
            else
            {
                showMessageBox("Please select Cluster head !!!");
            }


        }
        catch { }
    }

    protected void ddlASM_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlASM.SelectedValue != "")
            {
                using (DataTable aTable = aDal.LoadMioByAsm(Convert.ToInt32(ddlASM.SelectedValue)))
                {
                    ddlMbe.DataSource = aTable;
                    ddlMbe.DataValueField = "ValueField";
                    ddlMbe.DataTextField = "TextField";
                    ddlMbe.DataBind();
                    ddlMbe.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                    ddlMbe.SelectedIndex = 0;
                }
            }
            else
            {
                showMessageBox("Please select Cluster head !!!");
            }


        }
        catch { }
    }

    protected void gvClusterHead_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    string clusterCode = gvClusterHead.DataKeys[e.Row.RowIndex].Value.ToString();
        //    GridView gvRBM = e.Row.FindControl("rbmGrid") as GridView;
        //    gvRBM.DataSource = aDal.GetSalesReportByRBM(Parm(), clusterCode);
        //    gvRBM.DataBind();
        //}
    }

    protected void imbClusterHead_ImageButton_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton button = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)button.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        //Panel panelRBM = (Panel)gvClusterHead.Rows[rowindex].FindControl("pnlRBM");

        //bool status = panelRBM.Visible;
        //panelRBM.Visible = !status;
       
    }

    protected void imbAbm_ImageButton_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton button = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)button.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        //GridView rbmGridView = (GridView)gvClusterHead.Rows[rowindex].FindControl("gvRBM");

        //Panel panelRBM = (Panel)rbmGridView.Rows[rowindex].FindControl("pnlABM");

        //bool status = panelRBM.Visible;
        //panelRBM.Visible = !status;
    }


    protected void rbmGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView grid = (GridView)sender;
            //data key is assigned on grid control as DataKeyNames="CollegeCode"
            string id1 = grid.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView grdview = e.Row.FindControl("abmGrid") as GridView;
            grdview.DataSource = aDal.GetSalesReportByABM(Parm(), id1);
            grdview.DataBind();
        }
       
    }

    protected void abmGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView grid = (GridView) sender;
            //data key is assigned on grid control as DataKeyNames="CollegeCode"
            string id1 = grid.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView grdview = e.Row.FindControl("mbeGrid") as GridView;
            grdview.DataSource = aDal.GetSalesReportByMBE(Parm(), id1,"");
            grdview.DataBind();
        }
    }

    protected void Show_Hide_RbmGrid(object sender, ImageClickEventArgs e)
    {
        ImageButton imgShowHide = (sender as ImageButton);
        GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);

        if (imgShowHide.CommandArgument == "Show")
        {
            row.FindControl("pnlRBM").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/images/clusterm.png";
            //string customerId = gvClusterHead.DataKeys[row.RowIndex].Value.ToString();
            //GridView gvOrders = row.FindControl("gvOrders") as GridView;
            //BindOrders(customerId, gvOrders);
        }
        else
        {
            row.FindControl("pnlRBM").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "~/images/cluster.png";
        }
    }

    protected void lsbClusterHead_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        string pram = "";

        foreach (ListItem item in ddlCluster.Items)
        {
            if (item.Selected)
            {

                pram = pram + item.Value + ",";
            }
        }

        try
        {

            if (pram != "")
            {
                string parameter = "";

                parameter = " AND RegionId IN (" + pram.Remove(pram.Length - 1) + ")";

                using (DataTable aTable = aStructureDal.LoadRegionMeansAreaByClusterIdListBox(parameter))
                {
                    ddlRegion.DataSource = aTable;
                    ddlRegion.DataValueField = "ValueField";
                    ddlRegion.DataTextField = "TextField";
                    ddlRegion.DataBind();
                    //ddlRegion.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                    //ddlRegion.SelectedIndex = 0;
                }
            }
            else
            {
                showMessageBox("Please select cluster !!!");
            }

        }
        catch { }

        LoadData();
    }


    protected void lsblRegion_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        string pram = "";

        foreach (ListItem item in ddlRegion.Items)
        {
            if (item.Selected)
            {

                pram = pram + item.Value + ",";
            }
        }

        try
        {

            if (pram != "")
            {
                string parameter = "";

                parameter = " AND AreaId IN (" + pram.Remove(pram.Length - 1) + ")";

                using (DataTable aTable = aStructureDal.LoadAreaMeansTerritoryByRegionIdListBox(parameter))
                {
                    ddlArea.DataSource = aTable;
                    ddlArea.DataValueField = "ValueField";
                    ddlArea.DataTextField = "TextField";
                    ddlArea.DataBind();
                    //ddlArea.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                    //ddlArea.SelectedIndex = 0;
                }
            }
            else
            {
                showMessageBox("Please select cluster !!!");
            }

        }
        catch { }

        LoadData();
    }

    protected void lsblArea_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string pram = "";

        foreach (ListItem item in ddlArea.Items)
        {
            if (item.Selected)
            {

                pram = pram + item.Value + ",";
            }
        }

        try
        {

            if (pram != "")
            {
                string parameter = "";

                parameter = " AND TerritoryId IN (" + pram.Remove(pram.Length - 1) + ")";

                using (DataTable aTable = aStructureDal.LoadTerritoryMeansSubTerritoryByAreaIdListBox(parameter))
                {
                    ddlTerritory.DataSource = aTable;
                    ddlTerritory.DataValueField = "ValueField";
                    ddlTerritory.DataTextField = "TextField";
                    ddlTerritory.DataBind();
                    //ddlTerritory.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                    //ddlTerritory.SelectedIndex = 0;
                }
            }
            else
            {
                showMessageBox("Please select cluster !!!");
            }

        }
        catch { }

        LoadData();
    }

    protected void loadButton_Click(object sender, EventArgs e)
    {
        Session["SalesReport"] = Parm();

        //if (rptTypeDropDownList.SelectedValue == "SCW")

        string url = "../SInventory_RPTVIEW/UpdatedSalesReportViewer.aspx";
        // string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }
}