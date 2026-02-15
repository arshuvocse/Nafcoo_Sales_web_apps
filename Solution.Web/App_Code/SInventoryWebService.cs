using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for SInventoryWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class SInventoryWebService : System.Web.Services.WebService {

    public SInventoryWebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    private string strConnectionString = ConfigurationManager.ConnectionStrings["SolutionConnectionStringSSIDB"].ToString();
    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetProductList(string prefixText)
    {
        string sql = "SELECT ProductCode+' : '+ProductName AS Product FROM tblProduct  WITH (NOLOCK) WHERE GroupId=1 AND IsActive=1 and ProductName like @prefixText";
        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["Product"].ToString(), i);
            i++;
        }
        return items;
    }
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetSubDepotInvoiceNo(string prefixText)
    {
        string ComUnitId = "";

        ComUnitId = Session["ComUnitId"].ToString();

        string sql = "Select (Cast(InvoiceId as nvarchar(50))+':'+InvoiceNo) As InvoiceNo from tblSubInvoiceMaster where  InvoiceId Is NOT NULL  And cast( InvoiceDate as date) between '2020/07/01' and CURRENT_TIMESTAMP And ComUnitId='" + ComUnitId + "' AND InvoiceNo like @prefixText";
        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["InvoiceNo"].ToString(), i);
            i++;
        }
        return items;
    }
    //GetProformaInvoice NO
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetProformaInvoiceNo(string prefixText)
    {
        string ComUnitId = "";

        ComUnitId = Session["ComUnitId"].ToString();

        //ComUnitId = string.IsNullOrEmpty(Session["ComUnitId"].ToString() ? 0 : Session["ComUnitId"].ToString());


        string sql = "Select (Cast(InvoiceId as nvarchar(50))+':'+InvoiceNo) As InvoiceNo from tblInvoice where  InvoiceId Is NOT NULL  And cast( InvoiceDate as date) between '2020/07/01' and CURRENT_TIMESTAMP And ComUnitId='" + ComUnitId + "' AND InvoiceNo like @prefixText";
        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["InvoiceNo"].ToString(), i);
            i++;
        }
        return items;
    }



    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetProformaInvoiceNoNew(string prefixText)
    {
        string ComUnitId = "";

        ComUnitId = Session["ComUnitId"].ToString();

        //ComUnitId = string.IsNullOrEmpty(Session["ComUnitId"].ToString() ? 0 : Session["ComUnitId"].ToString());


        string sql = @"  
Select top 1  (Cast(0 as nvarchar(50))+':'+'N/A' ) As InvoiceNo from tblInvoice with(nolock) 

 union all  Select top 20 (Cast(InvoiceId as nvarchar(50))+':'+InvoiceNo) As InvoiceNo from tblInvoice with (nolock)  where  InvoiceId Is NOT NULL  And cast( InvoiceDate as date) between '2020/07/01' and CURRENT_TIMESTAMP And ComUnitId='" + ComUnitId + "' AND InvoiceNo like @prefixText";
        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["InvoiceNo"].ToString(), i);
            i++;
        }
        return items;
    }


    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetPreBatch(string prefixText)
    {

        string sql = "SELECT DISTINCT BatchNo FROM dbo.tblCentralStore WHERE BatchNo like @prefixText";
        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["BatchNo"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetAllInvoice(string prefixText)
    {

        //string CompIDD = Session["CompIDD"].ToString();

        string sql = @"SELECT top 15 InvoiceNo +':'+ Cast(InvoiceId as nvarchar(50))+':'+ Cast(I.ComUnitId as nvarchar(50))+ ':'+ CI.ComUnitName + ':' + CustomerCode  AS InvoiceNo FROM tblInvoice AS I with (nolock) 
LEFT JOIN tblCompanyUnit AS CI ON I.ComUnitId = CI.ComUnitId 
LEFT JOIN tblCustMaster AS CSTMR ON I.CustomerMasterId = CSTMR.CustomerMasterId WHERE InvoiceNo LIKE @prefixText";
        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = "%" + prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["InvoiceNo"].ToString(), i);
            i++;
        }
        return items;
    }



    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetProductWithCode(string prefixText)
    {
        string sql = "SELECT ProductCode+':'+ProductName AS Product FROM tblProduct WHERE ProductCode+':'+ProductName LIKE @prefixText";
        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = "%"+prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["Product"].ToString(), i);
            i++;
        }
        return items;
    }
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCustomer(string prefixText)
    {
        string sql = "";
        
        //if (Session["UserType"].ToString() == "Admin")
        //{
            sql = "select TOP 20 CustomerCode+':'+CustomerName as Customer from tblCustMaster where CustomerName like @prefixText";
        //}
        //else
        //{
        //    string comUnit = Session["ComUnitId"].ToString();
        //    sql = "select CustomerCode+':'+CustomerName as Customer from tblCustMaster where ComUnitId='" + comUnit.Trim() + "'  and  CustomerName like @prefixText";
        //}
        
        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["Customer"].ToString(), i);
            i++;
        }
        return items;
    }



    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCustomer_New(string prefixText)
    {
        string sql = "";

       // if (Session["UserType"].ToString() == "Admin")
        {
            sql = @"select top 10 CustomerCode+' : '+CustomerName+ ' : '+CellNo +' |' + CAST(CustomerMasterId  as nvarchar(max)) as Customer from tblCustMaster with(nolock) 
where   IsActive=1 AND CustomerCode IS NOT NULL    and     ( CustomerName like @prefixText  or  CustomerCode like @prefixText  or  CellNo like @prefixText)
order by CustomerName asc";
        }
        //else
        //{
        //    string comUnit = Session["ComUnitId"].ToString();
        //    sql = "select top 15 CustomerCode+' : '+CustomerName+' |' + CAST(CustomerMasterId  as nvarchar(max)) as Customer from tblCustMaster with(nolock)  where  IsActive=1 AND CustomerCode IS NOT NULL  and  ComUnitId='" + comUnit.Trim() + "'  and  CustomerName like  @prefixText";
        //}

        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = "%"+ prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["Customer"].ToString(), i);
            i++;
        }
        return items;
    }






    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCustomer_New_For_Quotedprice(string prefixText)
    {
        string sql = "";

        // if (Session["UserType"].ToString() == "Admin")
        {
            string a = "'%"+prefixText+"%'";
            sql = @"SELECT convert(varchar,CustomerMasterId)+':'+CustomerCode + ':' + CustomerName AS Customer FROM tblCustMaster WHERE CustomerCode+':'+CustomerName LIKE "+a+" ORDER BY CustomerName";
        }
        //else
        //{
        //    string comUnit = Session["ComUnitId"].ToString();
        //    sql = "select top 15 CustomerCode+' : '+CustomerName+' |' + CAST(CustomerMasterId  as nvarchar(max)) as Customer from tblCustMaster with(nolock)  where  IsActive=1 AND CustomerCode IS NOT NULL  and  ComUnitId='" + comUnit.Trim() + "'  and  CustomerName like  @prefixText";
        //}

        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
      //  da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = "%" + prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["Customer"].ToString(), i);
            i++;
        }
        return items;
    }




    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCustomerForOrder(string prefixText)
    {
        string sql = "";

        // if (Session["UserType"].ToString() == "Admin")
        {
            sql = @"select top 10 CustomerCode+' : '+ CustomerName + ' | ' + CAST(CustomerMasterId  as nvarchar(max)) + ' | ' + CAST(PriceGroupId  as nvarchar(max)) + ' | ' + CAST(CustomerTypeId  as nvarchar(max))  as Customer from tblCustMaster with(nolock) 
where   IsActive=1 AND CustomerCode IS NOT NULL    and     ( CustomerName like @prefixText  or  CustomerCode like @prefixText  or  CellNo like @prefixText)
order by CustomerName asc";
        }
        //else
        //{
        //    string comUnit = Session["ComUnitId"].ToString();
        //    sql = "select top 15 CustomerCode+' : '+CustomerName+' |' + CAST(CustomerMasterId  as nvarchar(max)) as Customer from tblCustMaster with(nolock)  where  IsActive=1 AND CustomerCode IS NOT NULL  and  ComUnitId='" + comUnit.Trim() + "'  and  CustomerName like  @prefixText";
        //}

        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = "%" + prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["Customer"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCustomer_ALL(string prefixText)
    {
        string sql = "";

        
            sql = @"select top 10 CustomerCode+' : '+ case when IsActive=1 then  CustomerName else CustomerName +' (Inactive) ' end +' : '+CellNo +' |' + CAST(CustomerMasterId  as nvarchar(max)) as Customer from tblCustMaster with(nolock) 
where     CustomerCode IS NOT NULL  and     ( CustomerName like @prefixText  or  CustomerCode like @prefixText  or  CellNo like @prefixText)
order by CustomerName asc";
        
        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = "%" + prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["Customer"].ToString(), i);
            i++;
        }
        return items;
    }



    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCustomer_ALL_new(string prefixText)
    {
        string sql = "";


        sql = @"select top 25 * from (select   ISNULL(CustomerCode+' : ','') +ISNULL( + case when IsActive=1 then  CustomerName else CustomerName +' (Inactive) ' end ,'') +ISNULL(' : '+CellNo,'') +' |' + CAST(CustomerMasterId  as nvarchar(max)) as Customer from tblCustMaster with(nolock) 
           
 ) tbl  where Customer like @prefixText";

        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = "%" + prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["Customer"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetDoctor_ALL(string prefixText)
    {
        string sql = "";


        sql = @"select top 10 DoctorCode+' : '+ case when IsActive=1 then  DoctorName else DoctorName +' (Inactive) ' end +' |' + CAST(DoctorId  as nvarchar(max)) as DoctorName from tblDoctorMaster with(nolock) 
where     DoctorCode IS NOT NULL  and  ( DoctorName like @prefixText  or  DoctorCode like @prefixText )
order by DoctorName asc";

        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = "%" + prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["DoctorName"].ToString(), i);
            i++;
        }
        return items;
    }


    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCustomer_WithoutGeneral(string prefixText)
    {
        string sql = "";

        if (Session["UserType"].ToString() == "Admin")
        {
            sql = @"select top 15 CustomerCode+' : '+CustomerName+' |' + CAST(CustomerMasterId  as nvarchar(max)) as Customer from tblCustMaster with(nolock) 
where   IsActive=1 AND CustomerCode IS NOT NULL  and  CustomerName like @prefixText
order by CustomerName asc";
        }
        else
        {
            
            sql = "select top 15 CustomerCode+' : '+CustomerName+' |' + CAST(CustomerMasterId  as nvarchar(max)) as Customer from tblCustMaster with(nolock)  where  IsActive=1 AND CustomerCode IS NOT NULL  and ProgramTypeId <>4   and   CustomerName like  @prefixText";
        }

        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = "%" + prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["Customer"].ToString(), i);
            i++;
        }
        return items;
    }


    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetProduct2(string prefixText)
    {
        string sql = "SELECT ProductName+':'+ProductCode AS Product FROM tblProduct WHERE ProductName like @prefixText";
        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["Product"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetProduct3(string prefixText)
    {
        string sql = "SELECT ProductCode+':'+ProductName+':'+PackSize AS Product FROM tblProduct WHERE ProductName like @prefixText";
        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["Product"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetEmpInfo(string prefixText)
    {
        string sql = "";

        if (Session["UserType"].ToString() == "Admin")
        {
            sql = "select EmpMasterCode+':'+EmpName as EmpInfo from tblEmpGeneralInfo where EmpName like @prefixText";
        }
        else
        {
            string comUnit = Session["ComUnitId"].ToString();
            sql = "select EmpMasterCode+':'+EmpName as EmpInfo from tblEmpGeneralInfo where ComUnitId='" + comUnit.Trim() + "'  and  EmpName like @prefixText";
        }

        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["EmpInfo"].ToString(), i);
            i++;
        }
        return items;
    }



    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetProductByMenufracturer(string prefixText, string contextKey)
    {
        int manufac = 0;
        if (contextKey != "")
        {
            string[] St = new string[] {};
            DataTable dT = new DataTable();
            string[] mainstrnin = contextKey.Split(',');
             manufac = Convert.ToInt32(mainstrnin[0]);
        }
            string sql =
                "SELECT ProductCode+':'+ProductName AS Product FROM tblProduct WHERE  ProductName like @prefixText  and ManufacId ='" +
                manufac + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
            da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = prefixText + "%";
            DataTable DTLocal = new DataTable();
            da.Fill(DTLocal);
            string[] items = new string[DTLocal.Rows.Count];
            int i = 0;
            foreach (DataRow dr in DTLocal.Rows)
            {
                items.SetValue(dr["Product"].ToString(), i);
                i++;
            }
            return items;
        }



    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetProduct(string prefixText, string contextKey)
    {
       
        string sql =
            "SELECT ProductCode+' : '+ProductName AS Product FROM tblProduct   WITH (NOLOCK) WHERE   GroupId=1 AND IsActive=1 and ProductName like @prefixText ";
        SqlDataAdapter da = new SqlDataAdapter(sql, strConnectionString);
        da.SelectCommand.Parameters.Add("@prefixText", System.Data.SqlDbType.VarChar, 50).Value = "%" + prefixText + "%";
        DataTable DTLocal = new DataTable();
        da.Fill(DTLocal);
        string[] items = new string[DTLocal.Rows.Count];
        int i = 0;
        foreach (DataRow dr in DTLocal.Rows)
        {
            items.SetValue(dr["Product"].ToString(), i);
            i++;
        }
        return items;
    }
}

