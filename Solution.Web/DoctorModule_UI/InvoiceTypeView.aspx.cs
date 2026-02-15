using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.DoctorModule_DAL;
using Library.DAL.MasterSetup_DAL;
using Newtonsoft.Json;
using SalesSolution.Web.Models;

public partial class DoctorModule_UI_InvoiceTypeView : System.Web.UI.Page
{
    private static InvoiceTypeDal setup = new InvoiceTypeDal();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    public static string Get_InvoiceType()
    {
        DataTable ds = setup.GetInvoiceTypeList();
        string _data = "";
        if (ds.Rows.Count > 0)
        {
            _data = JsonConvert.SerializeObject(ds);
        }
        return _data;
    }

    [WebMethod(EnableSession = true)]
    public static ResultInfo Delete_InvoiceType(int Id)
    {
        ResultInfo resultInfo = new ResultInfo();
        resultInfo = setup.DeleteInvoiceType(Id, HttpContext.Current.Session["UserId"].ToString());
        return resultInfo;
    }
}