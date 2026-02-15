using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.DoctorModule_DAL;
using Library.DAL.MasterSetup_DAL;
using Library.DAO.DoctorModule_DAO;
using SalesSolution.Web.Models;

public partial class DoctorModule_UI_InvoiceType : System.Web.UI.Page
{
    public static InvoiceTypeDal setup = new InvoiceTypeDal();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["id"] != "" && Request.QueryString["id"] != null)
        {
            masterId.Text = Request.QueryString["id"];
        }
        else
        {
            masterId.Text = 0.ToString();
        }
    }

    [WebMethod(EnableSession = true)]
    public static ResultInfo Save_InvoiceType(InvoiceTypeDao doctorDesignation)
    {
        ResultInfo resultInfo = new ResultInfo();
        resultInfo = setup.SaveInvoiceType(doctorDesignation, HttpContext.Current.Session["UserId"].ToString());
        return resultInfo;
    }

    [WebMethod(EnableSession = true)]
    public static InvoiceTypeDao GetInvoiceTypeEditData(int id)
    {
        return setup.GetInvoiceTypeForEdit(id);
    }
}