using System.Activities.Expressions;
using System.Globalization;
using Library.DAL.ChartDAL;
using Library.DAL.SInventory_DAL;
using Library.DAO.MasterSetup_DAO;
using Library.DAO.SInventory_Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_UI_AdminDashboard : System.Web.UI.Page
{

    static ChartDAL aChartDal = new ChartDAL();

    static SalesDashboardDal aDashboardDal = new SalesDashboardDal();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    public static CompanyDao[] GetCompanyInfo()
    {

        List<CompanyDao> aList = new List<CompanyDao>();


        DataTable aTable = aDashboardDal.GetCompanyInfo();

        if (aTable.Rows.Count > 0)
        {

            foreach (DataRow DR in aTable.Rows)
            {
                var aData = new CompanyDao();

                aData.CompanyId = Convert.ToInt32(DR["CompanyId"].ToString());
                aData.CompanyName = DR["CompanyName"].ToString();

                aList.Add(aData);

            }

            return aList.ToArray();
        }


        return aList.ToArray();

    }


    [WebMethod(EnableSession = true)]
    public static CompanyWiseInvoiceDao[] GetCompanyWiseInvoice(string companyId, string fromDate, string toDate)
    {
        List<CompanyWiseInvoiceDao> aList = new List<CompanyWiseInvoiceDao>();

        DateTime date = DateTime.Today;
        var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        bool national = true;

        if (companyId != "")
        {
            national = false;
        }

        string pram = "";

        if (companyId != "")
        {
            pram = pram + " AND UNT.ComUnitId = '" + companyId + "'";
        }

        if (fromDate != "" && toDate != "")
        {
            pram = pram + " AND InvoiceDate BETWEEN '" + Convert.ToDateTime(fromDate) + "' AND '" + Convert.ToDateTime(toDate) + "'";
        }
        else if (fromDate != "" && toDate == "")
        {
            pram = pram + " AND INV.InvoiceDate >= '" + Convert.ToDateTime(fromDate) + "'";
        }
        else if (fromDate == "" && toDate != "")
        {
            pram = pram + " AND INV.InvoiceDate <= '" + Convert.ToDateTime(toDate) + "'";
        }
        else
        {
            pram = pram + " AND InvoiceDate BETWEEN  '" + firstDayOfMonth + "' AND '" + lastDayOfMonth + "'";
        }


        DataTable aTable = aDashboardDal.GetCompanyInvoice(pram,national);

        if (aTable.Rows.Count > 0)
        {

            foreach (DataRow DR in aTable.Rows)
            {
                var aData = new CompanyWiseInvoiceDao();

                aData.InvoiceDate = DR["InvoiceDate"].ToString();
                aData.NoOfInvoice = Convert.ToDecimal(DR["NoOfInvoice"].ToString());

                aList.Add(aData);

            }

            return aList.ToArray();
        }

        return aList.ToArray();

    }


    [WebMethod(EnableSession = true)]
    public static ExpireProductDao[] GetCompanyWiseExpireProduct(string companyId, string expireIn)
    {

        List<ExpireProductDao> aList = new List<ExpireProductDao>();

        string pram = "";

        if (companyId != "")
        {
            pram = pram + " AND CU.ComUnitId = '" + companyId + "'";
        }


        DataTable aTable = aDashboardDal.GetCompanyWiseExpireProduct(pram, Convert.ToInt32(expireIn));

        if (aTable.Rows.Count > 0)
        {

            foreach (DataRow DR in aTable.Rows)
            {
                var aData = new ExpireProductDao();

                aData.ProductCode = DR["ProductCode"].ToString();
                aData.ProductName = DR["ProductName"].ToString();
                aData.PackSize = DR["PackSize"].ToString();
                aData.MfgDate = DR["MfgDate"].ToString();
                aData.ExpDate = DR["ExpDate"].ToString();
                aData.BatchNo = DR["BatchNo"].ToString();
                aData.StockQty = Convert.ToDecimal(DR["StockQty"].ToString());

                aList.Add(aData);

            }

            return aList.ToArray();
        }


        return aList.ToArray();

    }


    [WebMethod(EnableSession = true)]
    public static Top50CustomerDao[] GetTop50Customer(string companyId, string fromDate, string toDate)
    {

        List<Top50CustomerDao> aList = new List<Top50CustomerDao>();


        DateTime date = DateTime.Today;
        var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        DataTable aTable = aDashboardDal.GetTop50Customer(companyId, fromDate == "" ? firstDayOfMonth : Convert.ToDateTime(fromDate), toDate == "" ? lastDayOfMonth : Convert.ToDateTime(toDate));

        if (aTable.Rows.Count > 0)
        {

            foreach (DataRow DR in aTable.Rows)
            {
                var aData = new Top50CustomerDao();

                aData.CustomerCode = DR["CustomerCode"].ToString();
                aData.CustomerName = DR["CustomerName"].ToString();
                aData.ActualSales = Convert.ToDecimal(DR["ActualSales"].ToString());
                aData.PrevActualSales = Convert.ToDecimal(DR["PrevActualSales"].ToString());
                aData.PrevMonthDue = Convert.ToDecimal(DR["PrevMonthDue"].ToString());
                aData.Due = Convert.ToDecimal(DR["Due"].ToString());

                aList.Add(aData);

            }

            return aList.ToArray();
        }


        return aList.ToArray();

    }



    [WebMethod(EnableSession = true)]
    public static CompanyWiseProductSalesDao[] GetCompanyWiseProductSales(string companyId, string fromDate, string toDate)
    {

        List<CompanyWiseProductSalesDao> aList = new List<CompanyWiseProductSalesDao>();

        string pram = "";

        if (fromDate == "" || toDate == "")
        {
            pram = pram + " AND YEAR(INV.InvoiceDate) = YEAR('" + DateTime.Today +
                   "') AND MONTH(INV.InvoiceDate) = MONTH('" + DateTime.Today + "')";
        }
        else
        {
            pram = pram + "AND  INV.InvoiceDate BETWEEN '" + fromDate + "' AND '" + toDate + "'";
        }

        string pram2 = "";

        if (companyId != "")
        {
            pram2 = pram2 + " AND NTP.ComUnitId = '" + companyId + "'";
        }


        DataTable aTable = aDashboardDal.GetCompanyWiseProductSales(pram2, pram);

        if (aTable.Rows.Count > 0)
        {

            foreach (DataRow DR in aTable.Rows)
            {
                var aData = new CompanyWiseProductSalesDao();

                aData.ProductName = DR["Product"].ToString();
                aData.SalesValue = Convert.ToDecimal(DR["SalesValue"].ToString());
                aData.SalesQuantity = Convert.ToDecimal(DR["SalesQuantity"].ToString());

                aList.Add(aData);

            }

            return aList.ToArray();
        }


        return aList.ToArray();

    }


    [WebMethod(EnableSession = true)]
    public static AglingReportDao[] GetAglingReport(string companyId, string toDate)
    {

        //string timePeriod, string year, string month

        string parameter = "";


        if (companyId != 0.ToString(CultureInfo.InvariantCulture))
        {
            parameter = parameter + " AND INV.ComUnitId = " + companyId;
        }

        if (toDate == "")
        {
            parameter = parameter + " AND INV.InvoiceDate <= GETDATE()";
        }
        else
        {
            parameter = parameter + " AND INV.InvoiceDate <= '" + toDate + "'";
        }


        //if (timePeriod == "Day")
        //{
        //    parameter = parameter + " AND INV.InvoiceDate <= GETDATE()";

        //}
        //else
        //{

        //    string parm = "";
        //    parm = parm + year + "-" + month + "-01";

        //    parameter = parameter + " AND INV.InvoiceDate < '" + Convert.ToDateTime(parm) + "'";

        //}



        var aList = new List<AglingReportDao>();


        DataTable aTable = aDashboardDal.GetAglingReport(parameter);

        if (aTable.Rows.Count > 0)
        {

            foreach (DataRow dr in aTable.Rows)
            {
                var aData = new AglingReportDao();

                aData.RegionName = dr["RegionName"].ToString();
                aData.Oneto10 = Convert.ToDecimal(dr["D10"].ToString());
                aData.Tento20 = Convert.ToDecimal(dr["D20"].ToString());
                aData.Twentyto30 = Convert.ToDecimal(dr["D30"].ToString());
                aData.Thirtyto40 = Convert.ToDecimal(dr["D40"].ToString());
                aData.Fortyto50 = Convert.ToDecimal(dr["D50"].ToString());
                aData.Fiftyto60 = Convert.ToDecimal(dr["D60"].ToString());
                aData.SixtyPlus = Convert.ToDecimal(dr["D61"].ToString());

                aList.Add(aData);

            }

            return aList.ToArray();
        }


        return aList.ToArray();

    }



    //public static string GetSalesChartData()
    //{
    //    DataTable dtdata=new DataTable();
    //    dtdata.Columns.Add("Criteria");
    //    dtdata.Columns.Add("Oct21");
    //    dtdata.Columns.Add("Oct21(upto28th)");
    //    dtdata.Columns.Add("Nov21(upto28th)");
    //    dtdata.Columns.Add("Total");

    //    DataRow dataRow = null;

    //    dataRow = dtdata.NewRow();
    //    dataRow["Criteria"] = "Campaign Sales";
    //    dataRow["Oct21"] = "1.15";
    //    dataRow["Oct21(upto28th)"] = "1.12";
    //    dataRow["Nov21(upto28th)"] = "1.32";
    //    dataRow["Total"] = "2.47";
    //    dtdata.Rows.Add(dataRow);

    //    dataRow = dtdata.NewRow();
    //    dataRow["Criteria"] = "General Sales";
    //    dataRow["Oct21"] = "0.72";
    //    dataRow["Oct21(upto28th)"] = "0.65";
    //    dataRow["Nov21(upto28th)"] = "1.24";
    //    dataRow["Total"] = "1.96";
    //    dtdata.Rows.Add(dataRow);


    //    dataRow = dtdata.NewRow();
    //    dataRow["Criteria"] = "FCB Sales";
    //    dataRow["Oct21"] = "1.08";
    //    dataRow["Oct21(upto28th)"] = "1";
    //    dataRow["Nov21(upto28th)"] = "1.42";
    //    dataRow["Total"] = "2.5";
    //    dtdata.Rows.Add(dataRow);


    //    dataRow = dtdata.NewRow();
    //    dataRow["Criteria"] = "Institution";
    //    dataRow["Oct21"] = "0.5";
    //    dataRow["Oct21(upto28th)"] = "0.5";
    //    dataRow["Nov21(upto28th)"] = "0.02";
    //    dataRow["Total"] = "0.52";
    //    dtdata.Rows.Add(dataRow);

    //    dataRow = dtdata.NewRow();
    //    dataRow["Criteria"] = "Total Sales";
    //    dataRow["Oct21"] = "1.37";
    //    dataRow["Oct21(upto28th)"] = "1.25";
    //    dataRow["Nov21(upto28th)"] = "1.26";
    //    dataRow["Total"] = "2.63";
    //    dtdata.Rows.Add(dataRow);

    //    string JSONresult;
    //    JSONresult = JsonConvert.SerializeObject(dtdata);
    //    return JSONresult;

    //}


    [WebMethod]
    public static string Get_DeptoWiseOrder(string param)
    {
   
        DataTable dt = aChartDal.GetDeptoWiseOrderDAL(param);
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return  JSONresult;
    }

    [WebMethod]
    public static string Get_DeptoWiseInvoice(string param)
    {

        DataTable dt = aChartDal.Get_DeptoWiseInvoiceDAL(param);
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }


    [WebMethod]
    public static string Get_TopBarChartOrder(string param)
    {

        DataTable dt = aChartDal.Get_TopBarChartOrderDAL();
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }


    [WebMethod]
    public static string Get_TopBarChartDeliveryAmount(string param)
    {

        DataTable dt = aChartDal.Get_TopBarChartDeliveryAmountDAL();
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }


    [WebMethod]
    public static string Get_TopBarChartRejectionAmount(string param)
    {

        DataTable dt = aChartDal.Get_TopBarChartRejectionAmountDAL();
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }


    [WebMethod]
    public static string Get_TopBarChartDCR(string param)
    {

        DataTable dt = aChartDal.Get_TopBarChartDCRDAL();
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }

    [WebMethod]
    public static string Get_TopBarChartTotalRX(string param)
    {

        DataTable dt = aChartDal.Get_TopBarChartTotalRXDAL();
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }

    [WebMethod]
    public static string Get_TopBarChartTotalAttandence(string param)
    {

        DataTable dt = aChartDal.Get_TopBarChartTotalAttandenceDAL();
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }

    [WebMethod]
    public static string Get_TopBarChartCustomerCoverage(string param)
    {

        DataTable dt = aChartDal.Get_TopBarChartCustomerCoverageDAL();
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }

    [WebMethod]
    public static string Get_TopBarChartTotalLeave(string param)
    {

        DataTable dt = aChartDal.Get_TopBarChartTotalLeaveDAL();
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }

    [WebMethod]
    public static string Get_TopBarChartOrderCount(string param)
    {

        DataTable dt = aChartDal.Get_TopBarChartOrderCountDAL();
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }

    [WebMethod]
    public static string Get_TopBarChartTotalInvoice(string param)
    {

        DataTable dt = aChartDal.Get_TopBarChartTotalInvoiceDAL();
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }

    [WebMethod]
    public static TotalInfoDAO GetTotalInfoByCurrentDate(int id)
    {
        return (aChartDal.GetTotalInfoByCurrentDateDAL(id));
    }

    [WebMethod]
    public static string GetAttandenceMonthlyReport(string fromdt, string todt, string param)
    {


        DataTable dt = aChartDal.GetAttandenceMonthlyReportDAL(fromdt, todt, param);
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }

    [WebMethod]
    public static string GetBrandWiseOrderReport(string fromdt, string todt, string param)
    {

       
        DataTable dt = aChartDal.GetBrandWiseOrderReportDAL(fromdt, todt, param);
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }

    [WebMethod]
    public static string GetBrandWiseOrderReportDayWise(string fromdt, string todt, string param, string Brand)
    {

        
            DataTable dt = aChartDal.GetBrandWiseOrderReportDayWiseDAL(fromdt, todt, param,  Brand);
        

        
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }

  

    [WebMethod]
    public static string GetGMPRXReportChartDataDayWise(string fromdt, string todt, string param)
    {


        DataTable dt = aChartDal.GetGMPRXReportChartDataDayWiseDAL(fromdt, todt, param);
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }


    [WebMethod]
    public static string GetGMPVisitReportChartDataDayWise(string fromdt, string todt, string param)
    {


        DataTable dt = aChartDal.GetGMPVisitReportChartDataDayWiseDAL(fromdt, todt, param);
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }
 
    [WebMethod]
    public static string GetSalesChartData(string fromdt, string todt, string param, string SSMonthNew, string SSYearNew)
    {
        string FirstMont = "";
        string LastMont = "";

        int myCount = 0;

        if (SSMonthNew.Contains(','))
        {
            string[] MonthSplt = SSMonthNew.Split(',');

            int count = MonthSplt.Length;
            myCount = count;

            FirstMont = MonthSplt[0].Trim();

                
                    LastMont = MonthSplt[count - 1].Trim();
                

           

        }

        else
        {
            myCount = 1;

            FirstMont = SSMonthNew;
            LastMont = SSMonthNew;
        }

        DateTime fromdate = new DateTime(Convert.ToInt32(SSYearNew), Convert.ToInt32(FirstMont), Convert.ToInt32(DateTime.Now.Day));
        DateTime todate = new DateTime(Convert.ToInt32(SSYearNew), Convert.ToInt32(LastMont), Convert.ToInt32(DateTime.Now.Day));
      
        DateTime tempdt = fromdate;
        DataTable dtdata = new DataTable();
        dtdata.Columns.Add("Criteria");
        while (tempdt.Month <= todate.Month)
        {
            if (CheckExist(tempdt.Month, SSMonthNew))
            {
                dtdata.Columns.Add(tempdt.ToString("MMM-yyyy"));
            }
            tempdt = tempdt.AddMonths(1);
        }
        dtdata.Columns.Add("Total");


        tempdt = fromdate;
        int i = 0;
        while (tempdt.Month <= todate.Month)
        {
            if (CheckExist(tempdt.Month, SSMonthNew))
            {

                DataTable dtperdata = aChartDal.GetPerMonthAmount(tempdt.Month.ToString(), tempdt.Year.ToString(), param, fromdt, todt);



                DataRow dataRow = null;

                if (i == 0)
                {


                    foreach (DataRow dtperdataRow in dtperdata.Rows)
                    {
                        dataRow = dtdata.NewRow();
                        dataRow["Criteria"] = dtperdataRow["Criteria"].ToString();
                        dataRow[tempdt.ToString("MMM-yyyy")] = dtperdataRow["Amount"].ToString();
                        dtdata.Rows.Add(dataRow);

                    }
                }
                else
                {
                    int j = 0;
                    foreach (DataRow dtperdataRow in dtperdata.Rows)
                    {
                        string colname = tempdt.ToString("MMM-yyyy");

                        try
                        {
                            dtdata.Rows[j][colname] = dtperdataRow["Amount"].ToString();
                        }
                        catch
                        {
                            dataRow = dtdata.NewRow();
                            dataRow["Criteria"] = dtperdataRow["Criteria"].ToString();
                            dataRow[tempdt.ToString("MMM-yyyy")] = dtperdataRow["Amount"].ToString();
                            dtdata.Rows.Add(dataRow);
                        }
                        
                        j++;
                    }
                }
                

            }
            tempdt = tempdt.AddMonths(1);
            i++;
        }


        foreach (DataRow dtdataRow in dtdata.Rows)
        {
            tempdt = fromdate;
            decimal total = 0;
            while (tempdt.Month <= todate.Month)
            {
                if (CheckExist(tempdt.Month, SSMonthNew))
                {
                    try
                    {
                        total += Convert.ToDecimal(dtdataRow[tempdt.ToString("MMM-yyyy")].ToString());
                    }
                    catch
                    {

                    }

                }
                tempdt = tempdt.AddMonths(1);
            }

            dtdataRow["Total"] = total/ myCount;
        }


        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dtdata);
        return JSONresult;

    }

    [WebMethod]
    public static string GetSalesRetrunChartData(string fromdt, string todt, string param)
    {
        DateTime fromdate = Convert.ToDateTime(fromdt);
        DateTime todate = Convert.ToDateTime(todt);
        DateTime tempdt = fromdate;
        DataTable dtdata = new DataTable();
        dtdata.Columns.Add("Criteria");
        while (tempdt.Month <= todate.Month)
        {
            dtdata.Columns.Add(tempdt.ToString("MMM-yyyy"));
            tempdt = tempdt.AddMonths(1);
        }
        dtdata.Columns.Add("Total");


        tempdt = fromdate;
        int i = 0;
        while (tempdt.Month <= todate.Month)
        {
           
                DataTable dtperdata = aChartDal.GetSalesRetrunChartData(tempdt.Month, tempdt.Year, param);



            DataRow dataRow = null;

            if (i == 0)
            {


                foreach (DataRow dtperdataRow in dtperdata.Rows)
                {
                    dataRow = dtdata.NewRow();
                    dataRow["Criteria"] = dtperdataRow["Criteria"].ToString();
                    dataRow[tempdt.ToString("MMM-yyyy")] = dtperdataRow["Amount"].ToString();
                    dtdata.Rows.Add(dataRow);

                }
            }
            else
            {
                int j = 0;
                foreach (DataRow dtperdataRow in dtperdata.Rows)
                {
                    string colname = tempdt.ToString("MMM-yyyy");


                    dtdata.Rows[j][colname] = dtperdataRow["Amount"].ToString();
                    j++;
                }
            }
           
            tempdt = tempdt.AddMonths(1);
            i++;
        }


        foreach (DataRow dtdataRow in dtdata.Rows)
        {
            tempdt = fromdate;
            decimal total = 0;
            while (tempdt.Month <= todate.Month)
            {

                try
                {
                    total += Convert.ToDecimal(dtdataRow[tempdt.ToString("MMM-yyyy")].ToString());
                }
                catch
                {

                }
                tempdt = tempdt.AddMonths(1);
            }

            dtdataRow["Total"] = total;
        }


        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dtdata);
        return JSONresult;

    }

    public static bool CheckExist(int currentmonth,string choosenmonth)
    {
        string[] MonthSplt = choosenmonth.Split(',');
        foreach(string month in MonthSplt)
        {
            int monthnum = Convert.ToInt32(month);
            if(monthnum==currentmonth)
            {
                return true;
            }

        }


        return false;

    }

    [WebMethod]
    public static string GetOrderChartData(string fromdt, string todt, string param, string SSMonthNew, string SSYearNew)
    {


        string FirstMont = "";
        string LastMont = "";
        int myCount = 0;

        if (SSMonthNew.Contains(','))
        {
            string[] MonthSplt = SSMonthNew.Split(',');

            int count = MonthSplt.Length;
            myCount = count;
            FirstMont = MonthSplt[0].Trim();


            LastMont = MonthSplt[count - 1].Trim();




        }

        else
        {
            myCount = 1;
            FirstMont = SSMonthNew;
            LastMont = SSMonthNew;
        }

        DateTime fromdate = new DateTime(Convert.ToInt32(SSYearNew), Convert.ToInt32(FirstMont), Convert.ToInt32(DateTime.Now.Day));
        DateTime todate = new DateTime(Convert.ToInt32(SSYearNew), Convert.ToInt32(LastMont), Convert.ToInt32(DateTime.Now.Day));

        DateTime tempdt = fromdate;
        DataTable dtdata = new DataTable();
        dtdata.Columns.Add("Criteria");
        while (tempdt.Month <= todate.Month)
        {
            if (CheckExist(tempdt.Month, SSMonthNew))
            {
                dtdata.Columns.Add(tempdt.ToString("MMM-yyyy"));
            }
            tempdt = tempdt.AddMonths(1);
        }
        dtdata.Columns.Add("Total");


        tempdt = fromdate;
        int i = 0;
        while (tempdt.Month <= todate.Month)
        {
            if (CheckExist(tempdt.Month, SSMonthNew))
            {

                DataTable dtperdata = aChartDal.GetOrderChartDataDAL(tempdt.Month.ToString(), tempdt.Year.ToString(), param, todt);



                DataRow dataRow = null;

                if (i == 0)
                {


                    foreach (DataRow dtperdataRow in dtperdata.Rows)
                    {
                        dataRow = dtdata.NewRow();
                        dataRow["Criteria"] = dtperdataRow["Criteria"].ToString();
                        dataRow[tempdt.ToString("MMM-yyyy")] = dtperdataRow["Amount"].ToString();
                        dtdata.Rows.Add(dataRow);

                    }
                }
                else
                {
                    int j = 0;
                    foreach (DataRow dtperdataRow in dtperdata.Rows)
                    {
                        string colname = tempdt.ToString("MMM-yyyy");

                        try
                        {
                            dtdata.Rows[j][colname] = dtperdataRow["Amount"].ToString();
                        }
                        catch
                        {
                            dataRow = dtdata.NewRow();
                            dataRow["Criteria"] = dtperdataRow["Criteria"].ToString();
                            dataRow[tempdt.ToString("MMM-yyyy")] = dtperdataRow["Amount"].ToString();
                            dtdata.Rows.Add(dataRow);
                        }

                        j++;
                    }
                }


            }
            tempdt = tempdt.AddMonths(1);
            i++;
        }


        foreach (DataRow dtdataRow in dtdata.Rows)
        {
            tempdt = fromdate;
            decimal total = 0;
            while (tempdt.Month <= todate.Month)
            {
                if (CheckExist(tempdt.Month, SSMonthNew))
                {
                    try
                    {
                        total += Convert.ToDecimal(dtdataRow[tempdt.ToString("MMM-yyyy")].ToString());
                    }
                    catch
                    {

                    }

                }
                tempdt = tempdt.AddMonths(1);
            }

            dtdataRow["Total"] = total/ myCount;
        }


        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dtdata);
        return JSONresult;

      

    }



    [WebMethod]
    public static string GetCustomerReportChartData(string fromdt, string todt, string param, string SSMonthNew, string SSYearNew)
    {

        string FirstMont = "";
        string LastMont = "";


        if (SSMonthNew.Contains(','))
        {
            string[] MonthSplt = SSMonthNew.Split(',');

            int count = MonthSplt.Length;

            FirstMont = MonthSplt[0].Trim();


            LastMont = MonthSplt[count - 1].Trim();




        }

        else
        {
            FirstMont = SSMonthNew;
            LastMont = SSMonthNew;
        }

        DateTime fromdate = new DateTime(Convert.ToInt32(SSYearNew), Convert.ToInt32(FirstMont), Convert.ToInt32(DateTime.Now.Day));
        DateTime todate = new DateTime(Convert.ToInt32(SSYearNew), Convert.ToInt32(LastMont), Convert.ToInt32(DateTime.Now.Day));

        DateTime tempdt = fromdate;
        DataTable dtdata = new DataTable();
        dtdata.Columns.Add("Criteria");
        while (tempdt.Month <= todate.Month)
        {
            if (CheckExist(tempdt.Month, SSMonthNew))
            {
                dtdata.Columns.Add(tempdt.ToString("MMM-yyyy"));
            }
            tempdt = tempdt.AddMonths(1);
        }
        dtdata.Columns.Add("Total");


        tempdt = fromdate;
        int i = 0;
        while (tempdt.Month <= todate.Month)
        {
            if (CheckExist(tempdt.Month, SSMonthNew))
            {

                DataTable dtperdata = aChartDal.GetCustomerReportChartDataDAL(tempdt.Month.ToString(), tempdt.Year.ToString(), param, fromdt, todt);



                DataRow dataRow = null;

                if (i == 0)
                {


                    foreach (DataRow dtperdataRow in dtperdata.Rows)
                    {
                        dataRow = dtdata.NewRow();
                        dataRow["Criteria"] = dtperdataRow["Criteria"].ToString();
                        dataRow[tempdt.ToString("MMM-yyyy")] = dtperdataRow["Amount"].ToString();
                        dtdata.Rows.Add(dataRow);

                    }
                }
                else
                {
                    int j = 0;
                    foreach (DataRow dtperdataRow in dtperdata.Rows)
                    {
                        string colname = tempdt.ToString("MMM-yyyy");

                        try
                        {
                            dtdata.Rows[j][colname] = dtperdataRow["Amount"].ToString();
                        }
                        catch
                        {
                            dataRow = dtdata.NewRow();
                            dataRow["Criteria"] = dtperdataRow["Criteria"].ToString();
                            dataRow[tempdt.ToString("MMM-yyyy")] = dtperdataRow["Amount"].ToString();
                            dtdata.Rows.Add(dataRow);
                        }

                        j++;
                    }
                }


            }
            tempdt = tempdt.AddMonths(1);
            i++;
        }


        foreach (DataRow dtdataRow in dtdata.Rows)
        {
            tempdt = fromdate;
            decimal total = 0;
            while (tempdt.Month <= todate.Month)
            {
                if (CheckExist(tempdt.Month, SSMonthNew))
                {
                    try
                    {
                        total += Convert.ToDecimal(dtdataRow[tempdt.ToString("MMM-yyyy")].ToString());
                    }
                    catch
                    {

                    }

                }
                tempdt = tempdt.AddMonths(1);
            }

            dtdataRow["Total"] = total;
        }


        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dtdata);
        return JSONresult;

 

    }



    [WebMethod]
    public static string GetGMPVisitReportChartData(string fromdt, string todt, string param)
    {
        DateTime fromdate = Convert.ToDateTime(fromdt);
        DateTime todate = Convert.ToDateTime(todt);
        DateTime tempdt = fromdate;
        DataTable dtdata = new DataTable();
        dtdata.Columns.Add("Criteria");
        while (tempdt.Month <= todate.Month)
        {
            dtdata.Columns.Add(tempdt.ToString("MMM-yyyy"));
            tempdt = tempdt.AddMonths(1);
        }
        dtdata.Columns.Add("Total");


        tempdt = fromdate;
        int i = 0;
        while (tempdt.Month <= todate.Month)
        {
            DataTable dtperdata = aChartDal.GetGMPVisitReportChartDataDAL(fromdt,  todt, param);



            DataRow dataRow = null;

            if (i == 0)
            {


                foreach (DataRow dtperdataRow in dtperdata.Rows)
                {
                    dataRow = dtdata.NewRow();
                    dataRow["Criteria"] = dtperdataRow["Criteria"].ToString();
                    dataRow[tempdt.ToString("MMM-yyyy")] = dtperdataRow["Amount"].ToString();
                    dtdata.Rows.Add(dataRow);

                }
            }
            else
            {
                int j = 0;
                foreach (DataRow dtperdataRow in dtperdata.Rows)
                {
                    string colname = tempdt.ToString("MMM-yyyy");


                    dtdata.Rows[j][colname] = dtperdataRow["Amount"].ToString();
                    j++;
                }
            }

            tempdt = tempdt.AddMonths(1);
            i++;
        }


        foreach (DataRow dtdataRow in dtdata.Rows)
        {
            tempdt = fromdate;
            decimal total = 0;
            while (tempdt.Month <= todate.Month)
            {

                try
                {
                    total += Convert.ToDecimal(dtdataRow[tempdt.ToString("MMM-yyyy")].ToString());
                }
                catch
                {

                }
                tempdt = tempdt.AddMonths(1);
            }

            dtdataRow["Total"] = total;
        }


        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dtdata);
        return JSONresult;

    }



    [WebMethod]
    public static string GetNONGMPVisitReportChartData(string fromdt, string todt)
    {
        DateTime fromdate = Convert.ToDateTime(fromdt);
        DateTime todate = Convert.ToDateTime(todt);
        DateTime tempdt = fromdate;
        DataTable dtdata = new DataTable();
        dtdata.Columns.Add("Criteria");
        while (tempdt.Month <= todate.Month)
        {
            dtdata.Columns.Add(tempdt.ToString("MMM-yyyy"));
            tempdt = tempdt.AddMonths(1);
        }
        dtdata.Columns.Add("Total");


        tempdt = fromdate;
        int i = 0;
        while (tempdt.Month <= todate.Month)
        {
            DataTable dtperdata = aChartDal.GetNONGMPVisitReportChartDataDAL(tempdt.Month, tempdt.Year);



            DataRow dataRow = null;

            if (i == 0)
            {


                foreach (DataRow dtperdataRow in dtperdata.Rows)
                {
                    dataRow = dtdata.NewRow();
                    dataRow["Criteria"] = dtperdataRow["Criteria"].ToString();
                    dataRow[tempdt.ToString("MMM-yyyy")] = dtperdataRow["Amount"].ToString();
                    dtdata.Rows.Add(dataRow);

                }
            }
            else
            {
                int j = 0;
                foreach (DataRow dtperdataRow in dtperdata.Rows)
                {
                    string colname = tempdt.ToString("MMM-yyyy");


                    dtdata.Rows[j][colname] = dtperdataRow["Amount"].ToString();
                    j++;
                }
            }

            tempdt = tempdt.AddMonths(1);
            i++;
        }


        foreach (DataRow dtdataRow in dtdata.Rows)
        {
            tempdt = fromdate;
            decimal total = 0;
            while (tempdt.Month <= todate.Month)
            {

                try
                {
                    total += Convert.ToDecimal(dtdataRow[tempdt.ToString("MMM-yyyy")].ToString());
                }
                catch
                {

                }
                tempdt = tempdt.AddMonths(1);
            }

            dtdataRow["Total"] = total;
        }


        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dtdata);
        return JSONresult;

    }



    [WebMethod]
    public static string GetGMPRXReportChartData(string fromdt, string todt, string param)
    {
        DateTime fromdate = Convert.ToDateTime(fromdt);
        DateTime todate = Convert.ToDateTime(todt);
        DateTime tempdt = fromdate;
        DataTable dtdata = new DataTable();
        dtdata.Columns.Add("Criteria");
        while (tempdt.Month <= todate.Month)
        {
            dtdata.Columns.Add(tempdt.ToString("MMM-yyyy"));
            tempdt = tempdt.AddMonths(1);
        }
        dtdata.Columns.Add("Total");


        tempdt = fromdate;
        int i = 0;
        while (tempdt.Month <= todate.Month)
        {
            DataTable dtperdata = aChartDal.GetGMPRXReportChartDataDAL(fromdt, todt, param);



            DataRow dataRow = null;

            if (i == 0)
            {


                foreach (DataRow dtperdataRow in dtperdata.Rows)
                {
                    dataRow = dtdata.NewRow();
                    dataRow["Criteria"] = dtperdataRow["Criteria"].ToString();
                    dataRow[tempdt.ToString("MMM-yyyy")] = dtperdataRow["Amount"].ToString();
                    dtdata.Rows.Add(dataRow);

                }
            }
            else
            {
                int j = 0;
                foreach (DataRow dtperdataRow in dtperdata.Rows)
                {
                    string colname = tempdt.ToString("MMM-yyyy");


                    dtdata.Rows[j][colname] = dtperdataRow["Amount"].ToString();
                    j++;
                }
            }

            tempdt = tempdt.AddMonths(1);
            i++;
        }


        foreach (DataRow dtdataRow in dtdata.Rows)
        {
            tempdt = fromdate;
            decimal total = 0;
            while (tempdt.Month <= todate.Month)
            {

                try
                {
                    total += Convert.ToDecimal(dtdataRow[tempdt.ToString("MMM-yyyy")].ToString());
                }
                catch
                {

                }
                tempdt = tempdt.AddMonths(1);
            }

            dtdataRow["Total"] = total;
        }


        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dtdata);
        return JSONresult;

    }


    //[WebMethod]
    //public static string GetGMPRXReportChartDataDayWise(string fromdt, string todt, string param)
    //{
    //    DateTime fromdate = Convert.ToDateTime(fromdt);
    //    DateTime todate = Convert.ToDateTime(todt);
    //    DateTime tempdt = fromdate;
    //    DataTable dtdata = new DataTable();
    //    dtdata.Columns.Add("Criteria");
    //    while (tempdt.Month <= todate.Month)
    //    {
    //        dtdata.Columns.Add(tempdt.ToString("MMM-yyyy"));
    //        tempdt = tempdt.AddMonths(1);
    //    }
    //    dtdata.Columns.Add("Total");


    //    tempdt = fromdate;
    //    int i = 0;
    //    while (tempdt.Month <= todate.Month)
    //    {
    //        DataTable dtperdata = aChartDal.GetGMPRXReportChartDataDayWiseDAL(fromdt, todt, param);



    //        DataRow dataRow = null;

    //        if (i == 0)
    //        {


    //            foreach (DataRow dtperdataRow in dtperdata.Rows)
    //            {
    //                dataRow = dtdata.NewRow();
    //                dataRow["Criteria"] = dtperdataRow["Criteria"].ToString();
    //                dataRow[tempdt.ToString("MMM-yyyy")] = dtperdataRow["Amount"].ToString();
    //                dtdata.Rows.Add(dataRow);

    //            }
    //        }
    //        else
    //        {
    //            int j = 0;
    //            foreach (DataRow dtperdataRow in dtperdata.Rows)
    //            {
    //                string colname = tempdt.ToString("MMM-yyyy");


    //                dtdata.Rows[j][colname] = dtperdataRow["Amount"].ToString();
    //                j++;
    //            }
    //        }

    //        tempdt = tempdt.AddMonths(1);
    //        i++;
    //    }


    //    foreach (DataRow dtdataRow in dtdata.Rows)
    //    {
    //        tempdt = fromdate;
    //        decimal total = 0;
    //        while (tempdt.Month <= todate.Month)
    //        {

    //            total += Convert.ToDecimal(dtdataRow[tempdt.ToString("MMM-yyyy")].ToString());
    //            tempdt = tempdt.AddMonths(1);
    //        }

    //        dtdataRow["Total"] = total;
    //    }


    //    string JSONresult;
    //    JSONresult = JsonConvert.SerializeObject(dtdata);
    //    return JSONresult;

    //}

    [WebMethod]
    public static string GetNONGMPRXReportChartData(string fromdt, string todt)
    {
        DateTime fromdate = Convert.ToDateTime(fromdt);
        DateTime todate = Convert.ToDateTime(todt); 
        DateTime tempdt = fromdate;
        DataTable dtdata = new DataTable();
        dtdata.Columns.Add("Criteria");
        while (tempdt.Month <= todate.Month)
        {
            dtdata.Columns.Add(tempdt.ToString("MMM-yyyy"));
            tempdt = tempdt.AddMonths(1);
        }
        dtdata.Columns.Add("Total");


        tempdt = fromdate;
        int i = 0;
        while (tempdt.Month <= todate.Month)
        {
            DataTable dtperdata = aChartDal.GetNONGMPRXReportChartDataDAL(tempdt.Month, tempdt.Year);



            DataRow dataRow = null;

            if (i == 0)
            {


                foreach (DataRow dtperdataRow in dtperdata.Rows)
                {
                    dataRow = dtdata.NewRow();
                    dataRow["Criteria"] = dtperdataRow["Criteria"].ToString();
                    dataRow[tempdt.ToString("MMM-yyyy")] = dtperdataRow["Amount"].ToString();
                    dtdata.Rows.Add(dataRow);

                }
            }
            else
            {
                int j = 0;
                foreach (DataRow dtperdataRow in dtperdata.Rows)
                {
                    string colname = tempdt.ToString("MMM-yyyy");


                    dtdata.Rows[j][colname] = dtperdataRow["Amount"].ToString();
                    j++;
                }
            }

            tempdt = tempdt.AddMonths(1);
            i++;
        }


        foreach (DataRow dtdataRow in dtdata.Rows)
        {
            tempdt = fromdate;
            decimal total = 0;
            while (tempdt.Month <= todate.Month)
            {

                try
                {
                    total += Convert.ToDecimal(dtdataRow[tempdt.ToString("MMM-yyyy")].ToString());
                }
                catch
                {

                }
                tempdt = tempdt.AddMonths(1);
            }

            dtdataRow["Total"] = total;
        }


        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dtdata);
        return JSONresult;

    }

    [WebMethod]
    public static string GetExpanseClaimMonthlyChartData(string fromdt, string todt, string param)
    {

        DataTable dt = aChartDal.GetExpanseClaimMonthlyChartDataDAL(fromdt, todt, param);
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }


    [WebMethod]
    public static string GetExpanseClaimMonthlyChartDataDayWise(string fromdt, string todt, string param)
    {

        DataTable dt = aChartDal.GetExpanseClaimMonthlyChartDataDayWiseDAL(fromdt, todt, param);
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }


    //[WebMethod]
    //public static string GetExpanseClaimMonthlyChartData(string fromdt, string todt, string param)
    //{
    //    DateTime fromdate = Convert.ToDateTime(fromdt);
    //    DateTime todate = Convert.ToDateTime(todt);
    //    DateTime tempdt = fromdate;
    //    DataTable dtdata = new DataTable();
    //    dtdata.Columns.Add("Criteria");
    //    while (tempdt.Month <= todate.Month)
    //    {
    //        dtdata.Columns.Add(tempdt.ToString("MMM-yyyy"));
    //        tempdt = tempdt.AddMonths(1);
    //    }
    //    dtdata.Columns.Add("Total");


    //    tempdt = fromdate;
    //    int i = 0;
    //    while (tempdt.Month <= todate.Month)
    //    {
    //        DataTable dtperdata = aChartDal.GetExpanseClaimMonthlyChartDataDAL(tempdt.Month, tempdt.Year, param);



    //        DataRow dataRow = null;

    //        if (i == 0)
    //        {


    //            foreach (DataRow dtperdataRow in dtperdata.Rows)
    //            {
    //                dataRow = dtdata.NewRow();
    //                dataRow["Criteria"] = dtperdataRow["Criteria"].ToString();
    //                dataRow[tempdt.ToString("MMM-yyyy")] = dtperdataRow["Amount"].ToString();
    //                dtdata.Rows.Add(dataRow);

    //            }
    //        }
    //        else
    //        {
    //            int j = 0;
    //            foreach (DataRow dtperdataRow in dtperdata.Rows)
    //            {
    //                string colname = tempdt.ToString("MMM-yyyy");


    //                dtdata.Rows[j][colname] = dtperdataRow["Amount"].ToString();
    //                j++;
    //            }
    //        }

    //        tempdt = tempdt.AddMonths(1);
    //        i++;
    //    }


    //    foreach (DataRow dtdataRow in dtdata.Rows)
    //    {
    //        tempdt = fromdate;
    //        decimal total = 0;
    //        while (tempdt.Month <= todate.Month)
    //        {

    //            total += Convert.ToDecimal(dtdataRow[tempdt.ToString("MMM-yyyy")].ToString());
    //            tempdt = tempdt.AddMonths(1);
    //        }

    //        dtdataRow["Total"] = total;
    //    }


    //    string JSONresult;
    //    JSONresult = JsonConvert.SerializeObject(dtdata);
    //    return JSONresult;

    //}

}