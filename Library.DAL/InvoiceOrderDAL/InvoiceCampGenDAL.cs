using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Library.DAL.DataManager;
using Library.DAO.InvoiceCamDAO;

namespace Library.DAL.InvoiceOrderDAL
{
    public class InvoiceCampGenDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();
        public List<CampaignMaster> GetCurrentCampaign(int customerId)
        {
            Response result = new Response();
            List<CampaignMaster> aList = new List<CampaignMaster>();

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterList = new List<SqlParameter>();
                aSqlParameterList.Add(new SqlParameter("@CustomerID", customerId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_OPAPI_GetCampaignMaster", aSqlParameterList);
                while (dr.Read())
                {
                    CampaignMaster aInfo = new CampaignMaster();
                    if (dr["CampgainMasterId"] != DBNull.Value) aInfo.CampgainMasterId = Convert.ToInt32(dr["CampgainMasterId"]);
                    if (dr["ProductLineID"] != DBNull.Value) aInfo.ProductLineID = Convert.ToInt32(dr["ProductLineID"]);
                    if (dr["FromDate"] != DBNull.Value) aInfo.FromDate = Convert.ToDateTime(dr["FromDate"]);
                    if (dr["Todate"] != DBNull.Value) aInfo.Todate = Convert.ToDateTime(dr["Todate"]);
                    if (dr["CampainTypeId"] != DBNull.Value) aInfo.CampainTypeId = Convert.ToInt32(dr["CampainTypeId"]);
                    if (dr["CustomerTypeId"] != DBNull.Value) aInfo.CustomerTypeId = Convert.ToInt32(dr["CustomerTypeId"]);
                    if (dr["Amount"] != DBNull.Value) aInfo.Amount = Convert.ToDecimal(dr["Amount"]);
                    if (dr["MaxAmount"] != DBNull.Value) aInfo.MaxAmount = Convert.ToDecimal(dr["MaxAmount"]);
                    if (dr["ProductQty"] != DBNull.Value) aInfo.ProductQty = Convert.ToInt32(dr["ProductQty"]);
                    if (dr["BonusProductId"] != DBNull.Value) aInfo.BonusProductId = Convert.ToInt32(dr["BonusProductId"]);
                    if (dr["IsTradePolicy"] != DBNull.Value) aInfo.IsTradePolicy = Convert.ToBoolean(dr["IsTradePolicy"]);
                    if (dr["CampaignName"] != DBNull.Value) aInfo.CampaignName = dr["CampaignName"].ToString();
                    if (dr["CampaignDesc"] != DBNull.Value) aInfo.CampaignDesc = dr["CampaignDesc"].ToString();
                    if (dr["CodeName"] != DBNull.Value) aInfo.CodeName = dr["CodeName"].ToString();
                    aInfo.campDetail = GetDetail(Convert.ToInt32(dr["CampgainMasterId"]));
                    aList.Add(aInfo);
                }




            }
            catch (Exception ex)
            {
                //result.Status = Status.BadRequest;
                //result.Message = "Bad Request";
                //result.ErrorMessage = ex.Message.ToString();
            }

            return aList;
        }

        public List<CampaignMaster> GetCustomerWiseCampaign(List<CampaignInfoOrderWise> aOrderDetailses)
        {
            Response result = new Response();
            List<CampaignMaster> aList = new List<CampaignMaster>();

            //List<int> productid = new List<int>();
            string param = "";
            int custId = 0;
            param = param + " AND tbl_BonusCampaignNewMaster.BonusProductId IN (";
            foreach (var orderMastersCampaignMaster in aOrderDetailses)
            {
                orderMastersCampaignMaster.TotalPrice = orderMastersCampaignMaster.Qty * orderMastersCampaignMaster.UnitPrice;
                custId = orderMastersCampaignMaster.CustomerId;
                param = param + "'" + orderMastersCampaignMaster.ProductId + "',";
            }
            param = param.TrimEnd(',');
            param = param + ")";

            int customerId = 0;
            int custtypeid = 0;
            DataTable dtcustomerinfo = GetCustomerInfo(custId);
            if (dtcustomerinfo.Rows.Count > 0)
            {
                customerId = Convert.ToInt32(dtcustomerinfo.Rows[0]["CustomerMasterId"].ToString());
                custtypeid = Convert.ToInt32(dtcustomerinfo.Rows[0]["CustomerTypeId"].ToString());
            }




            DataTable dtcamdata = GetCampaignMaster(custId, param);

            foreach (DataRow dtcamdataRow in dtcamdata.Rows)
            {
                if (dtcamdataRow["CampainTypeId"].ToString() == "1")
                {
                    int productId = Convert.ToInt32(dtcamdataRow["BonusProductId"].ToString());
                    decimal qtycompare = string.IsNullOrEmpty(dtcamdataRow["ProductQty"].ToString())
                        ? 0
                        : Convert.ToDecimal(dtcamdataRow["ProductQty"].ToString());
                    decimal productqty = aOrderDetailses.Where(x => x.ProductId == productId)
                        .Select(x => x.Qty).First();

                    if (productqty >= qtycompare)
                    {
                        CampaignMaster aCampaignMaster = new CampaignMaster();
                        aCampaignMaster.CampaignName = dtcamdataRow["CampaignName"].ToString();
                        aCampaignMaster.CodeName = dtcamdataRow["CampaignCode"].ToString();
                        aCampaignMaster.CampainTypeId = string.IsNullOrEmpty(dtcamdataRow["CampainTypeId"].ToString()) ? 0 : Convert.ToInt32(dtcamdataRow["CampainTypeId"].ToString());
                        aCampaignMaster.CustomerTypeId = string.IsNullOrEmpty(dtcamdataRow["CustomerTypeId"].ToString()) ? 0 : Convert.ToInt32(dtcamdataRow["CustomerTypeId"].ToString());
                        aCampaignMaster.BonusProductId = string.IsNullOrEmpty(dtcamdataRow["BonusProductId"].ToString()) ? 0 : Convert.ToInt32(dtcamdataRow["BonusProductId"].ToString());

                        aCampaignMaster.CampgainMasterId = Convert.ToInt32(dtcamdataRow["CampgainMasterId"].ToString());

                        aList.Add(aCampaignMaster);

                        CampaignInfoOrderWise aCampaignInfoOrderWise = aOrderDetailses.First(x => x.ProductId == productId);
                        aCampaignInfoOrderWise.IsApplied = true;
                    }



                }
                else if (dtcamdataRow["CampainTypeId"].ToString() == "2")
                {
                    int productId = Convert.ToInt32(dtcamdataRow["BonusProductId"].ToString());
                    decimal minamount = string.IsNullOrEmpty(dtcamdataRow["Amount"].ToString())
                        ? 0
                        : Convert.ToDecimal(dtcamdataRow["Amount"].ToString());
                    decimal maxamount = string.IsNullOrEmpty(dtcamdataRow["MaxAmount"].ToString())
                        ? 0
                        : Convert.ToDecimal(dtcamdataRow["MaxAmount"].ToString());
                    //decimal productqty = orderMasters.OrderDetails.Where(x => x.ProductId == productId)
                    //    .Select(x => x.Quantity).First();

                    decimal proamount = 0;
                    proamount = aOrderDetailses.Where(x => x.ProductId == productId)
                        .Select(x => x.TotalPrice).First();

                    if (proamount >= minamount && proamount <= maxamount)
                    {
                        CampaignMaster aCampaignMaster = new CampaignMaster();
                        aCampaignMaster.CampaignName = dtcamdataRow["CampaignName"].ToString();
                        aCampaignMaster.CodeName = dtcamdataRow["CampaignCode"].ToString();
                        aCampaignMaster.CampainTypeId = string.IsNullOrEmpty(dtcamdataRow["CampainTypeId"].ToString()) ? 0 : Convert.ToInt32(dtcamdataRow["CampainTypeId"].ToString());
                        aCampaignMaster.CustomerTypeId = string.IsNullOrEmpty(dtcamdataRow["CustomerTypeId"].ToString()) ? 0 : Convert.ToInt32(dtcamdataRow["CustomerTypeId"].ToString());
                        aCampaignMaster.BonusProductId = string.IsNullOrEmpty(dtcamdataRow["BonusProductId"].ToString()) ? 0 : Convert.ToInt32(dtcamdataRow["BonusProductId"].ToString());

                        aCampaignMaster.CampgainMasterId = Convert.ToInt32(dtcamdataRow["CampgainMasterId"].ToString());

                        aList.Add(aCampaignMaster);

                        CampaignInfoOrderWise aCampaignInfoOrderWise = aOrderDetailses.First(x => x.ProductId == productId);
                        aCampaignInfoOrderWise.IsApplied = true;
                    }
                }

            }

            bool noncam = false;
            foreach (var campaignInfoOrderWise in aOrderDetailses)
            {
                if (campaignInfoOrderWise.IsApplied == false)
                {
                    noncam = true;
                    break;

                }
            }

            if (noncam)
            {


                decimal remainingtotal = aOrderDetailses.Where(x => x.IsApplied == false).Select(x => x.TotalPrice).Sum();
                DataTable dtdisperc = GetCampaign3rd(remainingtotal, customerId, custtypeid);
                foreach (DataRow dtcamdataRow in dtdisperc.Rows)
                {
                    CampaignMaster aCampaignMaster = new CampaignMaster();
                    aCampaignMaster.CampaignName = dtcamdataRow["CampaignName"].ToString();
                    aCampaignMaster.CodeName = dtcamdataRow["CampaignCode"].ToString();
                    aCampaignMaster.CampainTypeId = string.IsNullOrEmpty(dtcamdataRow["CampainTypeId"].ToString())
                        ? 0
                        : Convert.ToInt32(dtcamdataRow["CampainTypeId"].ToString());
                    aCampaignMaster.CustomerTypeId = string.IsNullOrEmpty(dtcamdataRow["CustomerTypeId"].ToString())
                        ? 0
                        : Convert.ToInt32(dtcamdataRow["CustomerTypeId"].ToString());
                    aCampaignMaster.BonusProductId = string.IsNullOrEmpty(dtcamdataRow["BonusProductId"].ToString())
                        ? 0
                        : Convert.ToInt32(dtcamdataRow["BonusProductId"].ToString());
                    aCampaignMaster.CampgainMasterId = Convert.ToInt32(dtcamdataRow["CampgainMasterId"].ToString());

                    aList.Add(aCampaignMaster);

                }

            }




            return aList;
        }

        public List<CampaignMaster> GetCustomerWiseCampaignOther(List<CampaignInfoOrderWise> aOrderDetailses)
        {
            Response result = new Response();
            List<CampaignMaster> aList = new List<CampaignMaster>();
            //decimal total = aOrderDetailses.Sum(item => (item.UnitPrice*item.Qty));
            List<int> campaignId = new List<int>();


            foreach (var campaignInfoOrderWise in aOrderDetailses)
            {
                try
                {
                    campaignInfoOrderWise.TotalPrice = campaignInfoOrderWise.Qty * campaignInfoOrderWise.UnitPrice;

                    accessManager.SqlConnectionOpen(DataBase.SalesDB);
                    List<SqlParameter> aSqlParameterList = new List<SqlParameter>();
                    aSqlParameterList.Add(new SqlParameter("@customerId", campaignInfoOrderWise.CustomerId));
                    aSqlParameterList.Add(new SqlParameter("@productId", campaignInfoOrderWise.ProductId));
                    aSqlParameterList.Add(new SqlParameter("@qty", campaignInfoOrderWise.Qty));
                    aSqlParameterList.Add(new SqlParameter("@totalprice", campaignInfoOrderWise.TotalPrice));
                    DataTable dt = accessManager.GetDataTable("sp_Webapi_Get_CustomerCampaignData", aSqlParameterList);
                    if (dt.Rows.Count > 0)
                    {
                        campaignInfoOrderWise.IsApplied = true;
                        decimal camqty = string.IsNullOrEmpty(dt.Rows[0]["ProductQty"].ToString())
                            ? 0
                            : Convert.ToDecimal(dt.Rows[0]["ProductQty"].ToString());

                        //decimal maxamount = string.IsNullOrEmpty(dt.Rows[0]["MaxAmount"].ToString())
                        //    ? 0
                        //    : Convert.ToDecimal(dt.Rows[0]["MaxAmount"].ToString());
                        //decimal minamount = string.IsNullOrEmpty(dt.Rows[0]["MinAmount"].ToString())
                        //    ? 0
                        //    : Convert.ToDecimal(dt.Rows[0]["MinAmount"].ToString());
                        //decimal totaltradeprice = campaignInfoOrderWise.Qty * campaignInfoOrderWise.UnitPrice;
                        int campaign = Convert.ToInt32(dt.Rows[0]["CampaignMasterId"].ToString());
                        bool exist = false;
                        if (campaignId != null)
                        {
                            foreach (var i in campaignId)
                            {
                                if (i == campaign)
                                {
                                    exist = true;
                                }
                            }
                        }


                        if (camqty > 0 && camqty <= campaignInfoOrderWise.Qty && exist == false)
                        {
                            CampaignMaster aCampaignMaster = new CampaignMaster();
                            aCampaignMaster.CampaignName = dt.Rows[0]["CampaignName"].ToString();
                            aCampaignMaster.CodeName = dt.Rows[0]["CampaignCode"].ToString();
                            aCampaignMaster.CampainTypeId = Convert.ToInt32(dt.Rows[0]["CampainTypeId"].ToString());
                            aCampaignMaster.CustomerTypeId = Convert.ToInt32(dt.Rows[0]["CustomerTypeId"].ToString());
                            aCampaignMaster.BonusProductId = Convert.ToInt32(dt.Rows[0]["BonusProductId"].ToString());
                            aCampaignMaster.CampgainMasterId = Convert.ToInt32(dt.Rows[0]["CampaignMasterId"].ToString());

                            aList.Add(aCampaignMaster);
                            campaignId.Add(aCampaignMaster.CampgainMasterId);
                            //total = total - (campaignInfoOrderWise.Qty * campaignInfoOrderWise.UnitPrice);
                        }

                    }




                }
                catch (Exception ex)
                {
                    //result.Status = Status.BadRequest;
                    //result.Message = "Bad Request";
                    //result.ErrorMessage = ex.Message.ToString();
                }
            }

            decimal total = aOrderDetailses.Where(x => x.IsApplied == false).Select(x => x.TotalPrice).Sum();

            foreach (var campaignInfoOrderWise in aOrderDetailses)
            {
                if (campaignInfoOrderWise.IsApplied == false)
                {
                    try
                    {


                        accessManager.SqlConnectionOpen(DataBase.SalesDB);
                        List<SqlParameter> aSqlParameterList = new List<SqlParameter>();
                        aSqlParameterList.Add(new SqlParameter("@customerId", campaignInfoOrderWise.CustomerId));
                        aSqlParameterList.Add(new SqlParameter("@productId", campaignInfoOrderWise.ProductId));
                        aSqlParameterList.Add(new SqlParameter("@qty", campaignInfoOrderWise.Qty));
                        aSqlParameterList.Add(new SqlParameter("@totalprice", total));
                        DataTable dt = accessManager.GetDataTable("sp_Webapi_Get_CustomerCampaignData", aSqlParameterList);
                        if (dt.Rows.Count > 0)
                        {
                            decimal maxamount = string.IsNullOrEmpty(dt.Rows[0]["MaxAmount"].ToString())
                                ? 0
                                : Convert.ToDecimal(dt.Rows[0]["MaxAmount"].ToString());
                            decimal minamount = string.IsNullOrEmpty(dt.Rows[0]["MinAmount"].ToString())
                                ? 0
                                : Convert.ToDecimal(dt.Rows[0]["MinAmount"].ToString());

                            {

                                campaignInfoOrderWise.IsApplied = true;

                                //if (total >= minamount && total <= maxamount)
                                {
                                    CampaignMaster aCampaignMaster = new CampaignMaster();
                                    aCampaignMaster.CampaignName = dt.Rows[0]["CampaignName"].ToString();
                                    aCampaignMaster.CodeName = dt.Rows[0]["CampaignCode"].ToString();
                                    aCampaignMaster.CampainTypeId = Convert.ToInt32(dt.Rows[0]["CampainTypeId"].ToString());
                                    aCampaignMaster.CustomerTypeId = Convert.ToInt32(dt.Rows[0]["CustomerTypeId"].ToString());
                                    aCampaignMaster.BonusProductId = Convert.ToInt32(dt.Rows[0]["BonusProductId"].ToString());
                                    aCampaignMaster.CampgainMasterId = Convert.ToInt32(dt.Rows[0]["CampaignMasterId"].ToString());

                                    aList.Add(aCampaignMaster);
                                }
                            }




                        }




                    }
                    catch (Exception ex)
                    {
                        //result.Status = Status.BadRequest;
                        //result.Message = "Bad Request";
                        //result.ErrorMessage = ex.Message.ToString();
                    }
                }
            }

            total = aOrderDetailses.Where(x => x.IsApplied == false).Select(x => x.TotalPrice).Sum();
            foreach (var campaignInfoOrderWise in aOrderDetailses)
            {
                if (campaignInfoOrderWise.IsApplied == false)
                {
                    try
                    {


                        accessManager.SqlConnectionOpen(DataBase.SalesDB);
                        List<SqlParameter> aSqlParameterList = new List<SqlParameter>();
                        aSqlParameterList.Add(new SqlParameter("@customerId", campaignInfoOrderWise.CustomerId));
                        aSqlParameterList.Add(new SqlParameter("@productId", "0"));
                        aSqlParameterList.Add(new SqlParameter("@qty", campaignInfoOrderWise.Qty));
                        aSqlParameterList.Add(new SqlParameter("@totalprice", total));
                        DataTable dt = accessManager.GetDataTable("sp_Webapi_Get_CustomerCampaignData", aSqlParameterList);
                        if (dt.Rows.Count > 0)
                        {
                            decimal maxamount = string.IsNullOrEmpty(dt.Rows[0]["MaxAmount"].ToString())
                                ? 0
                                : Convert.ToDecimal(dt.Rows[0]["MaxAmount"].ToString());
                            decimal minamount = string.IsNullOrEmpty(dt.Rows[0]["MinAmount"].ToString())
                                ? 0
                                : Convert.ToDecimal(dt.Rows[0]["MinAmount"].ToString());

                            {

                                campaignInfoOrderWise.IsApplied = true;

                                //if (total >= minamount && total <= maxamount)
                                {
                                    CampaignMaster aCampaignMaster = new CampaignMaster();
                                    aCampaignMaster.CampaignName = dt.Rows[0]["CampaignName"].ToString();
                                    aCampaignMaster.CodeName = dt.Rows[0]["CampaignCode"].ToString();
                                    aCampaignMaster.CampainTypeId = Convert.ToInt32(dt.Rows[0]["CampainTypeId"].ToString());
                                    aCampaignMaster.CustomerTypeId = Convert.ToInt32(dt.Rows[0]["CustomerTypeId"].ToString());
                                    aCampaignMaster.BonusProductId = Convert.ToInt32(dt.Rows[0]["BonusProductId"].ToString());
                                    aCampaignMaster.CampgainMasterId = Convert.ToInt32(dt.Rows[0]["CampaignMasterId"].ToString());

                                    aList.Add(aCampaignMaster);
                                }
                            }

                            break;


                        }




                    }
                    catch (Exception ex)
                    {
                        //result.Status = Status.BadRequest;
                        //result.Message = "Bad Request";
                        //result.ErrorMessage = ex.Message.ToString();
                    }
                }
            }




            return aList;
        }
        public DataTable GetPrice(int? id, int customerid)
        {
            accessManager.SqlConnectionOpen(DataBase.SalesDB);
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();
            aSqlParameterList.Add(new SqlParameter("@ProductId", (object)id ?? DBNull.Value));
            aSqlParameterList.Add(new SqlParameter("@CustomerId", (object)customerid ?? DBNull.Value));



            DataTable dt = accessManager.GetDataTable("sp_Webapi_Get_ProductPrice", aSqlParameterList);

            accessManager.SqlConnectionClose();


            return dt;
        }
        public DataTable GetCustomerCampaign(int typeid, int customerId)
        {
            accessManager.SqlConnectionOpen(DataBase.SalesDB);
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();
            aSqlParameterList.Add(new SqlParameter("@customerTypeId", typeid));
            aSqlParameterList.Add(new SqlParameter("@customerId", customerId));

            DataTable dt = accessManager.GetDataTable("sp_Webapi_GetCampaignCustomer", aSqlParameterList);

            accessManager.SqlConnectionClose();


            return dt;
        }
        public DataTable GetProductWiseCampaign(int productid, string param)
        {
            accessManager.SqlConnectionOpen(DataBase.SalesDB);
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();

            aSqlParameterList.Add(new SqlParameter("@OrderProductId", (object)productid ?? DBNull.Value));
            aSqlParameterList.Add(new SqlParameter("@param", (object)param ?? DBNull.Value));


            DataTable dt = accessManager.GetDataTable("sp_Webapi_GetCampaignData", aSqlParameterList);

            accessManager.SqlConnectionClose();


            return dt;
        }
        public DataTable GetCustomerInfo(int? id)
        {
            accessManager.SqlConnectionOpen(DataBase.SalesDB);
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();
            aSqlParameterList.Add(new SqlParameter("@CustomerMasterId", (object)id ?? DBNull.Value));



            DataTable dt = accessManager.GetDataTable("sp_Webapi_Get_CustomerInfos", aSqlParameterList);

            accessManager.SqlConnectionClose();


            return dt;
        }
        public DataTable GetCampaignTradePolicyProductPerc(decimal amount, int custid, int typeid)
        {
            accessManager.SqlConnectionOpen(DataBase.SalesDB);
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();
            aSqlParameterList.Add(new SqlParameter("@RemTotalAmount", (object)amount ?? DBNull.Value));
            aSqlParameterList.Add(new SqlParameter("@customerId", (object)custid ?? DBNull.Value));
            aSqlParameterList.Add(new SqlParameter("@cuttypeId", (object)typeid ?? DBNull.Value));



            DataTable dt = accessManager.GetDataTable("sp_Webapi_GetCampaignTradePolicyPerc", aSqlParameterList);

            accessManager.SqlConnectionClose();


            return dt;
        }

        public DataTable GetCampaign3rd(decimal amount, int custid, int typeid)
        {
            accessManager.SqlConnectionOpen(DataBase.SalesDB);
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();
            aSqlParameterList.Add(new SqlParameter("@RemTotalAmount", (object)amount ?? DBNull.Value));
            aSqlParameterList.Add(new SqlParameter("@customerId", (object)custid ?? DBNull.Value));
            aSqlParameterList.Add(new SqlParameter("@cuttypeId", (object)typeid ?? DBNull.Value));



            DataTable dt = accessManager.GetDataTable("sp_Webapi_GetCampaignType3rd", aSqlParameterList);

            accessManager.SqlConnectionClose();


            return dt;
        }

        public DataTable GetCampaignDetail(int camid, string param)
        {
            accessManager.SqlConnectionOpen(DataBase.SalesDB);
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();
            aSqlParameterList.Add(new SqlParameter("@CampaignMasterId", (object)camid ?? DBNull.Value));
            aSqlParameterList.Add(new SqlParameter("@param", (object)param ?? DBNull.Value));


            DataTable dt = accessManager.GetDataTable("sp_Webapi_GetCampaignDetail", aSqlParameterList);

            accessManager.SqlConnectionClose();


            return dt;
        }

        //public List<OrderDetails> GetOrderProductWiseCampaign(OrderMaster orderMasters)
        //{
        //    Response result = new Response();
        //    List<OrderDetails> aList = new List<OrderDetails>();

        //    foreach (var campaignInfoOrderWise in orderMasters.OrderDetails)
        //    {
        //        try
        //        {
        //            string bonustypeId = "";

        //            int count = 0;
        //            DataTable dtdata = null;

        //            foreach (var campaignMaster in orderMasters.CampaignMasters)
        //            {
        //                string param = "";
        //                if (campaignMaster.CampgainMasterId !=0)
        //                {
        //                    param = " AND CampaignMasterId='" + campaignMaster.CampgainMasterId + "'";
        //                }
        //                DataTable dtdata1 = GetProductWiseCampaign(
        //                    campaignInfoOrderWise.ProductId, param);

        //                    dtdata = dtdata1;
        //                    if (dtdata1.Rows.Count > 0)
        //                    {
        //                        count++;
        //                    }
        //            }

        //            if (count < 2)
        //            {

        //                if (dtdata.Rows.Count > 0)
        //                {
        //                    decimal qtycompare= string.IsNullOrEmpty(dtdata.Rows[0]["Quantity"].ToString())
        //                        ? 0
        //                        : Convert.ToDecimal(dtdata.Rows[0]["Quantity"].ToString());
        //                    if (qtycompare<= campaignInfoOrderWise.Quantity)
        //                    {


        //                    bonustypeId = dtdata.Rows[0]["BonusTypeId"].ToString();
        //                    campaignInfoOrderWise.IsCampaignProduct = true;
        //                    if (bonustypeId == "5")
        //                    {


        //                        decimal camqty = 0;
        //                        decimal bonusqty = 0;
        //                        decimal campaignproqty = 0;
        //                        decimal orderqty = campaignInfoOrderWise.Quantity;
        //                        bonusqty = string.IsNullOrEmpty(dtdata.Rows[0]["BonusQuantity"].ToString())
        //                            ? 0
        //                            : Convert.ToDecimal(dtdata.Rows[0]["BonusQuantity"].ToString());
        //                        campaignproqty = string.IsNullOrEmpty(dtdata.Rows[0]["Quantity"].ToString())
        //                            ? 0
        //                            : Convert.ToDecimal(dtdata.Rows[0]["Quantity"].ToString());
        //                        camqty = Convert.ToDecimal((orderqty / campaignproqty).ToString("0000"));
        //                        camqty = camqty * bonusqty;


        //                        OrderDetails aOrderDetails1 = new OrderDetails();
        //                        aOrderDetails1.ProductId = campaignInfoOrderWise.ProductId;
        //                        aOrderDetails1.CustomerId = campaignInfoOrderWise.CustomerId;
        //                        aOrderDetails1.Quantity = campaignInfoOrderWise.Quantity;
        //                        aOrderDetails1.DiscountPercentage = campaignInfoOrderWise.DiscountPercentage;
        //                        aOrderDetails1.DiscountValue = campaignInfoOrderWise.DiscountValue;
        //                        aOrderDetails1.UnitPrice = campaignInfoOrderWise.UnitPrice;
        //                        aOrderDetails1.TotalPrice = campaignInfoOrderWise.TotalPrice;
        //                        aOrderDetails1.TotalVatAmount = campaignInfoOrderWise.TotalVatAmount;
        //                        aOrderDetails1.VatPercentage = campaignInfoOrderWise.VatPercentage;
        //                        aOrderDetails1.UnitVatAmount = campaignInfoOrderWise.UnitVatAmount;
        //                        aOrderDetails1.NetAmount = campaignInfoOrderWise.NetAmount;

        //                        aList.Add(aOrderDetails1);



        //                            OrderDetails aOrderDetails = new OrderDetails();
        //                        aOrderDetails.ProductId = campaignInfoOrderWise.ProductId;
        //                        aOrderDetails.CustomerId = campaignInfoOrderWise.CustomerId;
        //                        aOrderDetails.Quantity = (int) camqty;
        //                        aOrderDetails.DiscountPercentage = 0;
        //                        aOrderDetails.DiscountValue = 0;
        //                        aOrderDetails.UnitPrice = 0;
        //                        aOrderDetails.TotalPrice = 0;
        //                        aOrderDetails.TotalVatAmount = 0;
        //                        aOrderDetails.VatPercentage = 0;
        //                        aOrderDetails.UnitVatAmount = 0;
        //                        aOrderDetails.NetAmount = 0;
        //                        aOrderDetails.IsCampaignProduct = true;
        //                        aOrderDetails.ProductName = campaignInfoOrderWise.ProductName;

        //                        aList.Add(aOrderDetails);
        //                    }

        //                    }

        //                    if (bonustypeId == "1")
        //                    {
        //                        decimal perctradeprice = 0;
        //                        decimal perctotaltradeprice = 0;
        //                        decimal dispercentage =
        //                            string.IsNullOrEmpty(dtdata.Rows[0]["DiscountPercentage"].ToString())
        //                                ? 0
        //                                : Convert.ToDecimal(dtdata.Rows[0]["DiscountPercentage"].ToString());
        //                        perctotaltradeprice = campaignInfoOrderWise.TotalPrice * (dispercentage / 100);

        //                        OrderDetails aOrderDetails = new OrderDetails();
        //                        aOrderDetails.ProductId = campaignInfoOrderWise.ProductId;
        //                        aOrderDetails.CustomerId = campaignInfoOrderWise.CustomerId;
        //                        aOrderDetails.Quantity = campaignInfoOrderWise.Quantity;
        //                        aOrderDetails.DiscountPercentage = dispercentage;
        //                        aOrderDetails.DiscountValue = perctotaltradeprice;
        //                        aOrderDetails.UnitPrice = campaignInfoOrderWise.UnitPrice;
        //                        aOrderDetails.TotalPrice = campaignInfoOrderWise.TotalPrice;
        //                        aOrderDetails.TotalVatAmount = campaignInfoOrderWise.TotalVatAmount;
        //                        aOrderDetails.VatPercentage = campaignInfoOrderWise.VatPercentage;
        //                        aOrderDetails.UnitVatAmount = campaignInfoOrderWise.UnitVatAmount;
        //                        aOrderDetails.NetAmount = campaignInfoOrderWise.NetAmount - perctotaltradeprice;

        //                        aList.Add(aOrderDetails);


        //                    }

        //                    if (bonustypeId == "2")
        //                    {
        //                        decimal perctradeprice = 0;
        //                        //decimal perctotaltradeprice = 0;
        //                        decimal perctotaltradeprice =
        //                            string.IsNullOrEmpty(dtdata.Rows[0]["DiscountAmount"].ToString())
        //                                ? 0
        //                                : Convert.ToDecimal(dtdata.Rows[0]["DiscountAmount"].ToString());
        //                        //perctotaltradeprice = campaignInfoOrderWise.TotalPrice * (dispercentage / 100);

        //                        OrderDetails aOrderDetails = new OrderDetails();
        //                        aOrderDetails.ProductId = campaignInfoOrderWise.ProductId;
        //                        aOrderDetails.CustomerId = campaignInfoOrderWise.CustomerId;
        //                        aOrderDetails.Quantity = campaignInfoOrderWise.Quantity;
        //                        aOrderDetails.DiscountPercentage = 0;
        //                        aOrderDetails.DiscountValue = perctotaltradeprice;
        //                        aOrderDetails.UnitPrice = campaignInfoOrderWise.UnitPrice;
        //                        aOrderDetails.TotalPrice = campaignInfoOrderWise.TotalPrice;
        //                        aOrderDetails.TotalVatAmount = campaignInfoOrderWise.TotalVatAmount;
        //                        aOrderDetails.VatPercentage = campaignInfoOrderWise.VatPercentage;
        //                        aOrderDetails.UnitVatAmount = campaignInfoOrderWise.UnitVatAmount;
        //                        aOrderDetails.NetAmount = campaignInfoOrderWise.NetAmount - perctotaltradeprice;

        //                        aList.Add(aOrderDetails);


        //                    }
        //                }
        //                else
        //                {
        //                    campaignInfoOrderWise.IsCampaignProduct = false;
        //                }




        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            //result.Status = Status.BadRequest;
        //            //result.Message = "Bad Request";
        //            //result.ErrorMessage = ex.Message.ToString();
        //        }
        //    }
        //    decimal remainingtotal = 0;
        //    foreach (var aOrderDetailse in orderMasters.OrderDetails)
        //    {
        //        if (aOrderDetailse.IsCampaignProduct == false)
        //        {
        //            remainingtotal += aOrderDetailse.TotalPrice;
        //        }
        //    }

        //    decimal dispercampaigntrade = 0;
        //    DataTable dtdisperc = GetCampaignTradePolicyProductPerc(remainingtotal);
        //    if (dtdisperc.Rows.Count > 0)
        //    {
        //        dispercampaigntrade = Convert.ToDecimal(dtdisperc.Rows[0]["DiscountPercentage"].ToString());

        //        foreach (var detailse in orderMasters.OrderDetails)
        //        {
        //            if (detailse.IsCampaignProduct == false)
        //            {
        //                OrderDetails aOrderDetails = new OrderDetails();
        //                aOrderDetails.ProductId = detailse.ProductId;
        //                aOrderDetails.CustomerId = detailse.CustomerId;
        //                aOrderDetails.Quantity = detailse.Quantity;
        //                aOrderDetails.DiscountPercentage = dispercampaigntrade;
        //                aOrderDetails.DiscountValue =
        //                    detailse.TotalPrice * (dispercampaigntrade / 100);
        //                aOrderDetails.UnitPrice = detailse.UnitPrice;
        //                aOrderDetails.TotalPrice = detailse.TotalPrice;
        //                aOrderDetails.TotalVatAmount = detailse.TotalVatAmount;
        //                aOrderDetails.VatPercentage = detailse.VatPercentage;
        //                aOrderDetails.UnitVatAmount = detailse.UnitVatAmount;
        //                aOrderDetails.NetAmount =
        //                    detailse.NetAmount - detailse.TotalPrice *
        //                    (dispercampaigntrade / 100);

        //                aList.Add(aOrderDetails);
        //            }
        //        }


        //    }



        //    return aList;
        //}


        //First Version of Campaign Code
        public List<OrderDetails> GetOrderProductWiseCampaignOther(OrderMaster orderMasters)
        {
            Response result = new Response();
            List<OrderDetails> aList = new List<OrderDetails>();
            int customerId = 0;
            int custtypeid = 0;
            string campaignmasterId = "";
            foreach (var campaignInfoOrderWise in orderMasters.OrderDetails)
            {
                try
                {
                    DataTable dtcustomerinfo = GetCustomerInfo(campaignInfoOrderWise.CustomerId);
                    if (dtcustomerinfo.Rows.Count > 0)
                    {
                        customerId = Convert.ToInt32(dtcustomerinfo.Rows[0]["CustomerMasterId"].ToString());
                        custtypeid = Convert.ToInt32(dtcustomerinfo.Rows[0]["CustomerTypeId"].ToString());
                    }

                    DataTable dtprice = GetPrice(campaignInfoOrderWise.ProductId, customerId);
                    if (dtprice.Rows.Count > 0)
                    {
                        campaignInfoOrderWise.UnitPrice = Convert.ToDecimal(dtprice.Rows[0]["UnitPrice"].ToString());
                        campaignInfoOrderWise.VatPercentage = Convert.ToDecimal(dtprice.Rows[0]["VATPercentage"].ToString());
                        campaignInfoOrderWise.UnitVatAmount = Convert.ToDecimal(dtprice.Rows[0]["VATAmountPerUnit"].ToString());
                        campaignInfoOrderWise.TotalPrice =
                            campaignInfoOrderWise.UnitPrice * campaignInfoOrderWise.Quantity;
                        campaignInfoOrderWise.TotalVatAmount =
                            campaignInfoOrderWise.UnitVatAmount * campaignInfoOrderWise.Quantity;
                        campaignInfoOrderWise.NetAmount =
                            campaignInfoOrderWise.TotalPrice + campaignInfoOrderWise.TotalVatAmount;
                    }

                    string bonustypeId = "";


                    int count = 0;
                    DataTable dtdata = null;



                    foreach (var campaignMaster in orderMasters.CampaignMasters)
                    {
                        string param = "";
                        if (campaignMaster.CampgainMasterId != 0)
                        {
                            param = " AND CampaignMasterId='" + campaignMaster.CampgainMasterId + "'";
                        }
                        else
                        {
                            param = " AND CampaignMasterId IN (SELECT CampMasId FROM dbo.GetCampaignCustomer('" +
                                    custtypeid + "') WHERE CustomerId='" + customerId + "') ";
                        }
                        DataTable dtdata1 = GetProductWiseCampaign(
                            campaignInfoOrderWise.ProductId, param);


                        if (dtdata1.Rows.Count > 0)
                        {
                            dtdata = dtdata1;
                            campaignmasterId = dtdata.Rows[0]["CampaignMasterId"].ToString();
                            count++;
                        }
                    }

                    if (count < 2)
                    {
                        if (dtdata != null)
                        {



                            if (dtdata.Rows.Count > 0)
                            {
                                bonustypeId = dtdata.Rows[0]["BonusTypeId"].ToString();
                                //decimal qtycompare = string.IsNullOrEmpty(dtdata.Rows[0]["Quantity"].ToString())
                                //    ? 0
                                //    : Convert.ToDecimal(dtdata.Rows[0]["Quantity"].ToString());
                                //if (qtycompare <= campaignInfoOrderWise.Quantity)
                                //{


                                //    bonustypeId = dtdata.Rows[0]["BonusTypeId"].ToString();
                                //    campaignInfoOrderWise.IsCamp = true;
                                //    if (bonustypeId == "5")
                                //    {


                                //        decimal camqty = 0;
                                //        decimal bonusqty = 0;
                                //        decimal campaignproqty = 0;
                                //        decimal orderqty = campaignInfoOrderWise.Quantity;
                                //        bonusqty = string.IsNullOrEmpty(dtdata.Rows[0]["BonusQuantity"].ToString())
                                //            ? 0
                                //            : Convert.ToDecimal(dtdata.Rows[0]["BonusQuantity"].ToString());
                                //        campaignproqty = string.IsNullOrEmpty(dtdata.Rows[0]["Quantity"].ToString())
                                //            ? 0
                                //            : Convert.ToDecimal(dtdata.Rows[0]["Quantity"].ToString());
                                //        camqty = Math.Floor(Convert.ToDecimal((orderqty / campaignproqty)));
                                //        camqty = camqty * bonusqty;


                                //        OrderDetails aOrderDetails1 = new OrderDetails();
                                //        aOrderDetails1.ProductId = campaignInfoOrderWise.ProductId;
                                //        aOrderDetails1.CustomerId = campaignInfoOrderWise.CustomerId;
                                //        aOrderDetails1.Quantity = campaignInfoOrderWise.Quantity;
                                //        aOrderDetails1.DiscountPercentage = campaignInfoOrderWise.DiscountPercentage;
                                //        aOrderDetails1.DiscountValue = campaignInfoOrderWise.DiscountValue;
                                //        aOrderDetails1.UnitPrice = campaignInfoOrderWise.UnitPrice;
                                //        aOrderDetails1.TotalPrice = campaignInfoOrderWise.TotalPrice;
                                //        aOrderDetails1.TotalVatAmount = campaignInfoOrderWise.TotalVatAmount;
                                //        aOrderDetails1.VatPercentage = campaignInfoOrderWise.VatPercentage;
                                //        aOrderDetails1.UnitVatAmount = campaignInfoOrderWise.UnitVatAmount;
                                //        aOrderDetails1.NetAmount = campaignInfoOrderWise.NetAmount;
                                //        aOrderDetails1.CampaingName = dtdata.Rows[0]["CampaignName"].ToString();
                                //        aOrderDetails1.CampaignType = dtdata.Rows[0]["CampaignDetailId"].ToString();
                                //        aOrderDetails1.IsGiftProduct = false;
                                //        aOrderDetails1.IsCampaignProduct = true;
                                //        aOrderDetails1.ProductName = campaignInfoOrderWise.ProductName;

                                //        aList.Add(aOrderDetails1);




                                //    }

                                //}

                                if (bonustypeId == "1")
                                {
                                    decimal perctradeprice = 0;
                                    decimal perctotaltradeprice = 0;
                                    decimal dispercentage =
                                        string.IsNullOrEmpty(dtdata.Rows[0]["DiscountPercentage"].ToString())
                                            ? 0
                                            : Convert.ToDecimal(dtdata.Rows[0]["DiscountPercentage"].ToString());
                                    perctotaltradeprice = campaignInfoOrderWise.TotalPrice * (dispercentage / 100);

                                    OrderDetails aOrderDetails = new OrderDetails();
                                    aOrderDetails.ProductId = campaignInfoOrderWise.ProductId;
                                    aOrderDetails.CustomerId = campaignInfoOrderWise.CustomerId;
                                    aOrderDetails.Quantity = campaignInfoOrderWise.Quantity;
                                    aOrderDetails.DiscountPercentage = dispercentage;
                                    aOrderDetails.DiscountValue = perctotaltradeprice;
                                    aOrderDetails.UnitPrice = campaignInfoOrderWise.UnitPrice;
                                    aOrderDetails.TotalPrice = campaignInfoOrderWise.TotalPrice;
                                    aOrderDetails.TotalVatAmount = campaignInfoOrderWise.TotalVatAmount;
                                    aOrderDetails.VatPercentage = campaignInfoOrderWise.VatPercentage;
                                    aOrderDetails.UnitVatAmount = campaignInfoOrderWise.UnitVatAmount;
                                    aOrderDetails.NetAmount = campaignInfoOrderWise.NetAmount - perctotaltradeprice;
                                    aOrderDetails.CampaingName = dtdata.Rows[0]["CampaignName"].ToString();
                                    aOrderDetails.CampaignType = dtdata.Rows[0]["CampaignDetailId"].ToString();
                                    aOrderDetails.IsGiftProduct = false;
                                    aOrderDetails.IsCampaignProduct = true;
                                    aOrderDetails.ProductName = campaignInfoOrderWise.ProductName;

                                    campaignInfoOrderWise.IsCamp = true;

                                    aList.Add(aOrderDetails);


                                }

                                if (bonustypeId == "2")
                                {
                                    decimal perctradeprice = 0;
                                    //decimal perctotaltradeprice = 0;
                                    decimal perctotaltradeprice =
                                        string.IsNullOrEmpty(dtdata.Rows[0]["QuantityDteail"].ToString())
                                            ? 0
                                            : Convert.ToDecimal(dtdata.Rows[0]["QuantityDteail"].ToString());
                                    //perctotaltradeprice = campaignInfoOrderWise.TotalPrice * (dispercentage / 100);

                                    OrderDetails aOrderDetails = new OrderDetails();
                                    aOrderDetails.ProductId = campaignInfoOrderWise.ProductId;
                                    aOrderDetails.CustomerId = campaignInfoOrderWise.CustomerId;
                                    aOrderDetails.Quantity = campaignInfoOrderWise.Quantity;
                                    aOrderDetails.DiscountPercentage = 0;
                                    aOrderDetails.DiscountValue = perctotaltradeprice;
                                    aOrderDetails.UnitPrice = campaignInfoOrderWise.UnitPrice;
                                    aOrderDetails.TotalPrice = campaignInfoOrderWise.TotalPrice;
                                    aOrderDetails.TotalVatAmount = campaignInfoOrderWise.TotalVatAmount;
                                    aOrderDetails.VatPercentage = campaignInfoOrderWise.VatPercentage;
                                    aOrderDetails.UnitVatAmount = campaignInfoOrderWise.UnitVatAmount;
                                    aOrderDetails.NetAmount = campaignInfoOrderWise.NetAmount - perctotaltradeprice;
                                    aOrderDetails.CampaingName = dtdata.Rows[0]["CampaignName"].ToString();
                                    aOrderDetails.CampaignType = dtdata.Rows[0]["CampaignDetailId"].ToString();
                                    aOrderDetails.IsGiftProduct = false;
                                    aOrderDetails.ProductName = campaignInfoOrderWise.ProductName;
                                    aOrderDetails.IsCampaignProduct = true;
                                    aOrderDetails.IsCamp = true;
                                    campaignInfoOrderWise.IsCamp = true;
                                    aList.Add(aOrderDetails);


                                }
                                if (bonustypeId == "3")
                                {
                                    decimal perctradeprice = 0;
                                    //decimal perctotaltradeprice = 0;
                                    decimal perctotaltradeprice =
                                        string.IsNullOrEmpty(dtdata.Rows[0]["QuantityDteail"].ToString())
                                            ? 0
                                            : Convert.ToDecimal(dtdata.Rows[0]["QuantityDteail"].ToString());
                                    //perctotaltradeprice = campaignInfoOrderWise.TotalPrice * (dispercentage / 100);

                                    OrderDetails aOrderDetails = new OrderDetails();
                                    aOrderDetails.ProductId = campaignInfoOrderWise.ProductId;
                                    aOrderDetails.CustomerId = campaignInfoOrderWise.CustomerId;
                                    aOrderDetails.Quantity = campaignInfoOrderWise.Quantity;
                                    aOrderDetails.DiscountPercentage = 0;
                                    aOrderDetails.DiscountValue = perctotaltradeprice;
                                    aOrderDetails.UnitPrice = campaignInfoOrderWise.UnitPrice - perctotaltradeprice;
                                    aOrderDetails.TotalPrice = aOrderDetails.UnitPrice * aOrderDetails.Quantity;
                                    aOrderDetails.TotalVatAmount = campaignInfoOrderWise.TotalVatAmount;
                                    aOrderDetails.VatPercentage = campaignInfoOrderWise.VatPercentage;
                                    aOrderDetails.UnitVatAmount = campaignInfoOrderWise.UnitVatAmount;
                                    aOrderDetails.NetAmount = aOrderDetails.TotalPrice + aOrderDetails.TotalVatAmount - perctotaltradeprice;
                                    aOrderDetails.CampaingName = dtdata.Rows[0]["CampaignName"].ToString();
                                    aOrderDetails.CampaignType = dtdata.Rows[0]["CampaignDetailId"].ToString();
                                    aOrderDetails.IsGiftProduct = false;
                                    aOrderDetails.ProductName = campaignInfoOrderWise.ProductName;
                                    aOrderDetails.IsCampaignProduct = true;
                                    aOrderDetails.IsCamp = true;
                                    campaignInfoOrderWise.IsCamp = true;
                                    aList.Add(aOrderDetails);


                                }
                            }
                            else
                            {
                                campaignInfoOrderWise.IsCamp = false;
                            }
                        }
                        else
                        {
                            campaignInfoOrderWise.IsCamp = false;
                        }




                    }

                }
                catch (Exception ex)
                {
                    //result.Status = Status.BadRequest;
                    //result.Message = "Bad Request";
                    //result.ErrorMessage = ex.Message.ToString();
                }
            }



            foreach (var orderMastersCampaignMaster in orderMasters.CampaignMasters)
            {

                decimal camqty = 0;
                decimal bonusqty = 0;
                decimal campaignproqty = 0;
                decimal orderqty = 0;
                DataTable dtdata = null;
                if (orderMastersCampaignMaster.CampgainMasterId != 0)
                {
                    dtdata = GetCampaignDetail(orderMastersCampaignMaster.CampgainMasterId, "");
                }
                else
                {
                    campaignmasterId = string.IsNullOrEmpty(campaignmasterId) ? "0" : campaignmasterId;
                    dtdata = GetCampaignDetail(Convert.ToInt32(campaignmasterId), "");
                }

                if (dtdata.Rows.Count > 0)
                {
                    foreach (var orderMastersOrderDetail in orderMasters.OrderDetails)
                    {

                        {


                            if (orderMastersOrderDetail.ProductId.ToString() == dtdata.Rows[0]["ProductId"].ToString())
                            {
                                if (orderMastersOrderDetail.IsCamp == false)
                                {


                                    OrderDetails aOrderDetails = new OrderDetails();
                                    aOrderDetails.ProductId = orderMastersOrderDetail.ProductId;
                                    aOrderDetails.CustomerId = orderMastersOrderDetail.CustomerId;
                                    aOrderDetails.Quantity = orderMastersOrderDetail.Quantity;
                                    aOrderDetails.DiscountPercentage = orderMastersOrderDetail.DiscountPercentage;
                                    aOrderDetails.DiscountValue = orderMastersOrderDetail.DiscountValue;
                                    aOrderDetails.UnitPrice = orderMastersOrderDetail.UnitPrice;
                                    aOrderDetails.TotalPrice = orderMastersOrderDetail.TotalPrice;
                                    aOrderDetails.TotalVatAmount = orderMastersOrderDetail.TotalVatAmount;
                                    aOrderDetails.VatPercentage = orderMastersOrderDetail.VatPercentage;
                                    aOrderDetails.UnitVatAmount = orderMastersOrderDetail.UnitVatAmount;
                                    aOrderDetails.NetAmount = orderMastersOrderDetail.NetAmount;
                                    aOrderDetails.CampaingName = dtdata.Rows[0]["CampaignName"].ToString();
                                    aOrderDetails.CampaignType = dtdata.Rows[0]["CampaignDetailId"].ToString();
                                    aOrderDetails.IsGiftProduct = false;
                                    aOrderDetails.ProductName = orderMastersOrderDetail.ProductName;
                                    aOrderDetails.IsCampaignProduct = true;
                                    aOrderDetails.ProductName = orderMastersOrderDetail.ProductName;

                                    aList.Add(aOrderDetails);

                                    orderMastersOrderDetail.IsCamp = true;
                                }


                                orderqty = orderMastersOrderDetail.Quantity;
                                break;
                            }
                        }
                    }

                    decimal qtycompare = string.IsNullOrEmpty(dtdata.Rows[0]["Quantity"].ToString())
                        ? 0
                        : Convert.ToDecimal(dtdata.Rows[0]["Quantity"].ToString());
                    if (qtycompare <= orderqty)
                    {

                        foreach (DataRow dtdataRow in dtdata.Rows)
                        {

                            bonusqty = string.IsNullOrEmpty(dtdataRow["BonusQuantity"].ToString())
                                ? 0
                                : Convert.ToDecimal(dtdataRow["BonusQuantity"].ToString());
                            campaignproqty = string.IsNullOrEmpty(dtdata.Rows[0]["Quantity"].ToString())
                                ? 0
                                : Convert.ToDecimal(dtdataRow["Quantity"].ToString());
                            camqty = Math.Floor(Convert.ToDecimal((orderqty / campaignproqty)));
                            camqty = camqty * bonusqty;

                            OrderDetails aOrderDetails = new OrderDetails();
                            aOrderDetails.ProductId = Convert.ToInt32(dtdataRow["BonusProductId"].ToString());
                            aOrderDetails.CustomerId = customerId;
                            aOrderDetails.Quantity = (int)camqty;
                            aOrderDetails.DiscountPercentage = 0;
                            aOrderDetails.DiscountValue = 0;
                            aOrderDetails.UnitPrice = 0;
                            aOrderDetails.TotalPrice = 0;
                            aOrderDetails.TotalVatAmount = 0;
                            aOrderDetails.VatPercentage = 0;
                            aOrderDetails.UnitVatAmount = 0;
                            aOrderDetails.NetAmount = 0;
                            aOrderDetails.IsCampaignProduct = true;
                            aOrderDetails.ProductName = dtdataRow["ProductName"].ToString();
                            aOrderDetails.CampaingName = dtdataRow["CampaignName"].ToString();
                            aOrderDetails.CampaignType = dtdataRow["CampaignDetailId"].ToString();
                            aOrderDetails.IsGiftProduct = true;
                            aOrderDetails.IsCampaignProduct = true;

                            aList.Add(aOrderDetails);
                        }
                    }
                }
            }






            decimal remainingtotal = 0;
            foreach (var aOrderDetailse in orderMasters.OrderDetails)
            {
                if (aOrderDetailse.IsCamp == false)
                {
                    remainingtotal += aOrderDetailse.TotalPrice;
                }
            }

            decimal dispercampaigntrade = 0;
            DataTable dtdisperc = GetCampaignTradePolicyProductPerc(remainingtotal, customerId, custtypeid);
            if (dtdisperc.Rows.Count > 0)
            {
                dispercampaigntrade = Convert.ToDecimal(dtdisperc.Rows[0]["DiscountPercentage"].ToString());

                foreach (var detailse in orderMasters.OrderDetails)
                {
                    if (detailse.IsCamp == false)
                    {
                        OrderDetails aOrderDetails = new OrderDetails();
                        aOrderDetails.ProductId = detailse.ProductId;
                        aOrderDetails.CustomerId = detailse.CustomerId;
                        aOrderDetails.Quantity = detailse.Quantity;
                        aOrderDetails.DiscountPercentage = dispercampaigntrade;
                        aOrderDetails.DiscountValue =
                            detailse.TotalPrice * (dispercampaigntrade / 100);
                        aOrderDetails.UnitPrice = detailse.UnitPrice;
                        aOrderDetails.TotalPrice = detailse.TotalPrice;
                        aOrderDetails.TotalVatAmount = detailse.TotalVatAmount;
                        aOrderDetails.VatPercentage = detailse.VatPercentage;
                        aOrderDetails.UnitVatAmount = detailse.UnitVatAmount;
                        aOrderDetails.NetAmount =
                            detailse.NetAmount - detailse.TotalPrice *
                            (dispercampaigntrade / 100);
                        aOrderDetails.CampaingName = dtdisperc.Rows[0]["CampaignName"].ToString();
                        aOrderDetails.CampaignType = dtdisperc.Rows[0]["CampaignDetailId"].ToString();
                        aOrderDetails.IsGiftProduct = false;
                        aOrderDetails.IsCampaignProduct = true;
                        aOrderDetails.ProductName = detailse.ProductName;

                        aList.Add(aOrderDetails);

                        detailse.IsCamp = true;
                    }
                }


            }
            else
            {
                foreach (var detailse in orderMasters.OrderDetails)
                {

                    if (detailse.IsCamp == false)
                    {
                        OrderDetails aOrderDetails = new OrderDetails();
                        aOrderDetails.ProductId = detailse.ProductId;
                        aOrderDetails.CustomerId = detailse.CustomerId;
                        aOrderDetails.Quantity = detailse.Quantity;
                        aOrderDetails.DiscountPercentage = dispercampaigntrade;
                        aOrderDetails.DiscountValue = detailse.DiscountValue;
                        aOrderDetails.UnitPrice = detailse.UnitPrice;
                        aOrderDetails.TotalPrice = detailse.TotalPrice;
                        aOrderDetails.TotalVatAmount = detailse.TotalVatAmount;
                        aOrderDetails.VatPercentage = detailse.VatPercentage;
                        aOrderDetails.UnitVatAmount = detailse.UnitVatAmount;
                        aOrderDetails.NetAmount = detailse.NetAmount;
                        aOrderDetails.CampaingName = "";
                        aOrderDetails.CampaignType = "";
                        aOrderDetails.IsGiftProduct = false;
                        aOrderDetails.IsCampaignProduct = false;
                        aOrderDetails.ProductName = detailse.ProductName;

                        aList.Add(aOrderDetails);

                        detailse.IsCamp = true;
                    }
                }
            }





            return aList;
        }

        public DataTable GetCampaignMaster(int customerId, string param)
        {
            accessManager.SqlConnectionOpen(DataBase.SalesDB);
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();
            aSqlParameterList.Add(new SqlParameter("@CustomerId", (object)customerId ?? DBNull.Value));
            aSqlParameterList.Add(new SqlParameter("@param", (object)param ?? DBNull.Value));


            DataTable dt = accessManager.GetDataTable("sp_Webapi_Get_CampaignMasterInfo", aSqlParameterList);

            accessManager.SqlConnectionClose();


            return dt;
        }

        public DataTable GetCampaignDetail(int campaignMasterId, int productid)
        {
            accessManager.SqlConnectionOpen(DataBase.SalesDB);
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();
            aSqlParameterList.Add(new SqlParameter("@campaignMasterid", (object)campaignMasterId ?? DBNull.Value));
            aSqlParameterList.Add(new SqlParameter("@productid", (object)productid ?? DBNull.Value));


            DataTable dt = accessManager.GetDataTable("sp_Webapi_Get_CampaingDetail", aSqlParameterList);

            accessManager.SqlConnectionClose();


            return dt;
        }
        public DataTable GetCampaignDetailParam(int campaignMasterId, string param)
        {
            accessManager.SqlConnectionOpen(DataBase.SalesDB);
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();
            aSqlParameterList.Add(new SqlParameter("@campaignMasterid", (object)campaignMasterId ?? DBNull.Value));
            aSqlParameterList.Add(new SqlParameter("@param", (object)param ?? DBNull.Value));


            DataTable dt = accessManager.GetDataTable("sp_Webapi_Get_CampaingDetailParam", aSqlParameterList);

            accessManager.SqlConnectionClose();


            return dt;
        }


        //Second version of Campaign Code
        public List<OrderDetails> GetOrderProductWiseCampaign(OrderMaster orderMasters)
        {

            Response result = new Response();
            List<OrderDetails> aList = new List<OrderDetails>();


            string campaignmasterId = "";
            bool iscampselect = false;

            //Checking if campaign selected
            foreach (var orderMastersCampaignMaster in orderMasters.CampaignMasters)
            {
                if (orderMastersCampaignMaster.CampgainMasterId != 0)
                {
                    iscampselect = true;
                }
            }


            string param = "";
            int customerId = 0;
            int custtypeid = 0;
            DataTable dtcustomerinfo = GetCustomerInfo(orderMasters.OrderDetails[0].CustomerId);
            if (dtcustomerinfo.Rows.Count > 0)
            {
                customerId = Convert.ToInt32(dtcustomerinfo.Rows[0]["CustomerMasterId"].ToString());
                custtypeid = Convert.ToInt32(dtcustomerinfo.Rows[0]["CustomerTypeId"].ToString());
            }

            List<CampaignMaster> aCampaignMasters = new List<CampaignMaster>();
            if (iscampselect == false)
            {
                List<CampaignInfoOrderWise> aOrderDetailses = new List<CampaignInfoOrderWise>();
                foreach (var campaignInfoOrderWise in orderMasters.OrderDetails)
                {
                    DataTable dtprice = GetPrice(campaignInfoOrderWise.ProductId, customerId);
                    if (dtprice.Rows.Count > 0)
                    {
                        campaignInfoOrderWise.UnitPrice = Convert.ToDecimal(dtprice.Rows[0]["UnitPrice"].ToString());
                        campaignInfoOrderWise.VatPercentage = Convert.ToDecimal(dtprice.Rows[0]["VATPercentage"].ToString());
                        campaignInfoOrderWise.UnitVatAmount = Convert.ToDecimal(dtprice.Rows[0]["VATAmountPerUnit"].ToString());
                        campaignInfoOrderWise.TotalPrice =
                            campaignInfoOrderWise.UnitPrice * campaignInfoOrderWise.Quantity;
                        campaignInfoOrderWise.TotalVatAmount =
                            campaignInfoOrderWise.UnitVatAmount * campaignInfoOrderWise.Quantity;
                        campaignInfoOrderWise.NetAmount =
                            campaignInfoOrderWise.TotalPrice + campaignInfoOrderWise.TotalVatAmount;
                    }

                    CampaignInfoOrderWise aCampaignInfoOrderWise = new CampaignInfoOrderWise();
                    aCampaignInfoOrderWise.ProductId = campaignInfoOrderWise.ProductId;
                    aCampaignInfoOrderWise.CustomerId = customerId;
                    aCampaignInfoOrderWise.Qty = campaignInfoOrderWise.Quantity;
                    aCampaignInfoOrderWise.TotalPrice = campaignInfoOrderWise.TotalPrice;
                    aCampaignInfoOrderWise.UnitPrice = campaignInfoOrderWise.UnitPrice;
                    aOrderDetailses.Add(aCampaignInfoOrderWise);

                }

                aCampaignMasters = GetCustomerWiseCampaign(aOrderDetailses);

            }

            bool isexist = false;
            foreach (var aCampaignMaster in aCampaignMasters)
            {
                CampaignMaster aCampaignInfoOrderWise = aCampaignMasters.FirstOrDefault(x => x.BonusProductId == aCampaignMaster.BonusProductId &&
                                                                                    x.CampgainMasterId != aCampaignMaster.CampgainMasterId);
                if (aCampaignInfoOrderWise != null)
                {
                    isexist = true;
                }

            }
            //With campaign selected data
            if (iscampselect)
            {
                param = param + " AND CampgainMasterId IN (";
                foreach (var orderMastersCampaignMaster in orderMasters.CampaignMasters)
                {
                    param = param + "'" + orderMastersCampaignMaster.CampgainMasterId + "',";
                }

                param = param.TrimEnd(',');
                param = param + ")";
            }
            //Without campaign selected data
            else
            {
                param = param + " AND CampgainMasterId IN (";
                foreach (var orderMastersCampaignMaster in aCampaignMasters)
                {
                    param = param + "'" + orderMastersCampaignMaster.CampgainMasterId + "',";
                }

                if (aCampaignMasters.Count == 0)
                {
                    param = param + "'0',";
                }
                param = param.TrimEnd(',');
                param = param + ")";
            }

            DataTable dtcampaigndata = GetCampaignMaster(customerId, param);

            if (!isexist)
            {


                foreach (DataRow dtcampaigndataRow in dtcampaigndata.Rows)
                {

                    //For campaign type 1
                    if (dtcampaigndataRow["CampainTypeId"].ToString() == "1")
                    {
                        int productId = Convert.ToInt32(dtcampaigndataRow["BonusProductId"].ToString());
                        decimal qtycompare = string.IsNullOrEmpty(dtcampaigndataRow["ProductQty"].ToString())
                            ? 0
                            : Convert.ToDecimal(dtcampaigndataRow["ProductQty"].ToString());
                        decimal productqty = orderMasters.OrderDetails.Where(x => x.ProductId == productId)
                            .Select(x => x.Quantity).First();


                        //OrderDetails aOrderDetailsa = orderMasters.OrderDetails.First(x => x.ProductId == productId);
                        //DataTable dtpricecamp = GetPrice(aOrderDetailsa.ProductId);
                        //if (dtpricecamp.Rows.Count > 0)
                        //{
                        //    aOrderDetailsa.UnitPrice = Convert.ToDecimal(dtpricecamp.Rows[0]["UnitPrice"].ToString());
                        //    aOrderDetailsa.VatPercentage = Convert.ToDecimal(dtpricecamp.Rows[0]["VATPercentage"].ToString());
                        //    aOrderDetailsa.UnitVatAmount = Convert.ToDecimal(dtpricecamp.Rows[0]["VATAmountPerUnit"].ToString());
                        //    aOrderDetailsa.TotalPrice =
                        //        aOrderDetailsa.UnitPrice * aOrderDetailsa.Quantity;
                        //    aOrderDetailsa.TotalVatAmount =
                        //        aOrderDetailsa.UnitVatAmount * aOrderDetailsa.Quantity;
                        //    aOrderDetailsa.NetAmount =
                        //        aOrderDetailsa.TotalPrice + aOrderDetailsa.TotalVatAmount;
                        //}
                        //aOrderDetailsa.CampaingName = "";
                        //aOrderDetailsa.CampaignType = "0";
                        //aOrderDetailsa.IsGiftProduct = false;

                        //aOrderDetailsa.IsCampaignProduct = true;


                        //aList.Add(aOrderDetailsa);

                        //aOrderDetailsa.IsCamp = true;


                        if (qtycompare <= productqty)
                        {


                            foreach (var campaignInfoOrderWise in orderMasters.OrderDetails)
                            {

                                DataTable dtprice = GetPrice(campaignInfoOrderWise.ProductId, customerId);
                                if (dtprice.Rows.Count > 0)
                                {
                                    campaignInfoOrderWise.UnitPrice =
                                        Convert.ToDecimal(dtprice.Rows[0]["UnitPrice"].ToString());
                                    campaignInfoOrderWise.VatPercentage =
                                        Convert.ToDecimal(dtprice.Rows[0]["VATPercentage"].ToString());
                                    campaignInfoOrderWise.UnitVatAmount =
                                        Convert.ToDecimal(dtprice.Rows[0]["VATAmountPerUnit"].ToString());
                                    campaignInfoOrderWise.TotalPrice =
                                        campaignInfoOrderWise.UnitPrice * campaignInfoOrderWise.Quantity;
                                    campaignInfoOrderWise.TotalVatAmount =
                                        campaignInfoOrderWise.UnitVatAmount * campaignInfoOrderWise.Quantity;
                                    campaignInfoOrderWise.NetAmount =
                                        campaignInfoOrderWise.TotalPrice + campaignInfoOrderWise.TotalVatAmount;
                                }

                                if (campaignInfoOrderWise.IsCamp == false)
                                {
                                    string bonustypeId = "";
                                    DataTable dtcamdetail = GetCampaignDetail(
                                        Convert.ToInt32(dtcampaigndataRow["CampgainMasterId"].ToString()),
                                        campaignInfoOrderWise.ProductId);
                                    if (dtcamdetail.Rows.Count > 0)
                                    {
                                        bonustypeId = dtcamdetail.Rows[0]["BonusTypeId"].ToString();
                                    }

                                    if (bonustypeId == "1")
                                    {
                                        decimal perctradeprice = 0;
                                        decimal perctotaltradeprice = 0;
                                        decimal dispercentage =
                                            string.IsNullOrEmpty(dtcamdetail.Rows[0]["DiscountPercentage"].ToString())
                                                ? 0
                                                : Convert.ToDecimal(
                                                    dtcamdetail.Rows[0]["DiscountPercentage"].ToString());
                                        perctotaltradeprice = campaignInfoOrderWise.TotalPrice * (dispercentage / 100);

                                        OrderDetails aOrderDetails = new OrderDetails();
                                        aOrderDetails.ProductId = campaignInfoOrderWise.ProductId;
                                        aOrderDetails.CustomerId = campaignInfoOrderWise.CustomerId;
                                        aOrderDetails.Quantity = campaignInfoOrderWise.Quantity;
                                        aOrderDetails.DiscountPercentage = dispercentage;
                                        aOrderDetails.DiscountValue = perctotaltradeprice;
                                        aOrderDetails.UnitPrice = campaignInfoOrderWise.UnitPrice;
                                        aOrderDetails.TotalPrice = campaignInfoOrderWise.TotalPrice;
                                        aOrderDetails.TotalVatAmount = campaignInfoOrderWise.TotalVatAmount;
                                        aOrderDetails.VatPercentage = campaignInfoOrderWise.VatPercentage;
                                        aOrderDetails.UnitVatAmount = campaignInfoOrderWise.UnitVatAmount;
                                        aOrderDetails.NetAmount = campaignInfoOrderWise.NetAmount - perctotaltradeprice;
                                        aOrderDetails.CampaingName = dtcamdetail.Rows[0]["CampaignName"].ToString();
                                        aOrderDetails.CampaignType = dtcamdetail.Rows[0]["CampaignDetailId"].ToString();
                                        aOrderDetails.IsGiftProduct = false;
                                        aOrderDetails.IsCampaignProduct = true;
                                        aOrderDetails.ProductName = campaignInfoOrderWise.ProductName;

                                        campaignInfoOrderWise.IsCamp = true;
                                        campaignInfoOrderWise.CampaingName = dtcamdetail.Rows[0]["CampaignName"].ToString();
                                        campaignInfoOrderWise.CampaignType = dtcamdetail.Rows[0]["CampaignDetailId"].ToString();


                                        aList.Add(aOrderDetails);


                                    }

                                    if (bonustypeId == "2")
                                    {
                                        decimal perctradeprice = 0;
                                        //decimal perctotaltradeprice = 0;
                                        decimal perctotaltradeprice =
                                            string.IsNullOrEmpty(dtcamdetail.Rows[0]["QuantityDteail"].ToString())
                                                ? 0
                                                : Convert.ToDecimal(dtcamdetail.Rows[0]["QuantityDteail"].ToString());
                                        //perctotaltradeprice = campaignInfoOrderWise.TotalPrice * (dispercentage / 100);

                                        OrderDetails aOrderDetails = new OrderDetails();
                                        aOrderDetails.ProductId = campaignInfoOrderWise.ProductId;
                                        aOrderDetails.CustomerId = campaignInfoOrderWise.CustomerId;
                                        aOrderDetails.Quantity = campaignInfoOrderWise.Quantity;
                                        aOrderDetails.DiscountPercentage = 0;
                                        aOrderDetails.DiscountValue = perctotaltradeprice;
                                        aOrderDetails.UnitPrice = campaignInfoOrderWise.UnitPrice;
                                        aOrderDetails.TotalPrice = campaignInfoOrderWise.TotalPrice;
                                        aOrderDetails.TotalVatAmount = campaignInfoOrderWise.TotalVatAmount;
                                        aOrderDetails.VatPercentage = campaignInfoOrderWise.VatPercentage;
                                        aOrderDetails.UnitVatAmount = campaignInfoOrderWise.UnitVatAmount;
                                        aOrderDetails.NetAmount = campaignInfoOrderWise.NetAmount - perctotaltradeprice;
                                        aOrderDetails.CampaingName = dtcamdetail.Rows[0]["CampaignName"].ToString();
                                        aOrderDetails.CampaignType = dtcamdetail.Rows[0]["CampaignDetailId"].ToString();
                                        aOrderDetails.IsGiftProduct = false;
                                        aOrderDetails.ProductName = campaignInfoOrderWise.ProductName;
                                        aOrderDetails.IsCampaignProduct = true;
                                        aOrderDetails.IsCamp = true;
                                        campaignInfoOrderWise.IsCamp = true;
                                        campaignInfoOrderWise.CampaingName = dtcamdetail.Rows[0]["CampaignName"].ToString();
                                        campaignInfoOrderWise.CampaignType = dtcamdetail.Rows[0]["CampaignDetailId"].ToString();

                                        aList.Add(aOrderDetails);


                                    }

                                    if (bonustypeId == "3")
                                    {
                                        decimal perctradeprice = 0;
                                        //decimal perctotaltradeprice = 0;
                                        decimal perctotaltradeprice =
                                            string.IsNullOrEmpty(dtcamdetail.Rows[0]["QuantityDteail"].ToString())
                                                ? 0
                                                : Convert.ToDecimal(dtcamdetail.Rows[0]["QuantityDteail"].ToString());
                                        //perctotaltradeprice = campaignInfoOrderWise.TotalPrice * (dispercentage / 100);

                                        OrderDetails aOrderDetails = new OrderDetails();
                                        aOrderDetails.ProductId = campaignInfoOrderWise.ProductId;
                                        aOrderDetails.CustomerId = campaignInfoOrderWise.CustomerId;
                                        aOrderDetails.Quantity = campaignInfoOrderWise.Quantity;
                                        aOrderDetails.DiscountPercentage = 0;
                                        aOrderDetails.DiscountValue = perctotaltradeprice;
                                        aOrderDetails.UnitPrice = campaignInfoOrderWise.UnitPrice - perctotaltradeprice;
                                        aOrderDetails.TotalPrice = aOrderDetails.UnitPrice * aOrderDetails.Quantity;
                                        aOrderDetails.TotalVatAmount = campaignInfoOrderWise.TotalVatAmount;
                                        aOrderDetails.VatPercentage = campaignInfoOrderWise.VatPercentage;
                                        aOrderDetails.UnitVatAmount = campaignInfoOrderWise.UnitVatAmount;
                                        aOrderDetails.NetAmount =
                                            aOrderDetails.TotalPrice + aOrderDetails.TotalVatAmount -
                                            perctotaltradeprice;
                                        aOrderDetails.CampaingName = dtcamdetail.Rows[0]["CampaignName"].ToString();
                                        aOrderDetails.CampaignType = dtcamdetail.Rows[0]["CampaignDetailId"].ToString();
                                        aOrderDetails.IsGiftProduct = false;
                                        aOrderDetails.ProductName = campaignInfoOrderWise.ProductName;
                                        aOrderDetails.IsCampaignProduct = true;
                                        aOrderDetails.IsCamp = true;
                                        campaignInfoOrderWise.IsCamp = true;
                                        campaignInfoOrderWise.CampaingName = dtcamdetail.Rows[0]["CampaignName"].ToString();
                                        campaignInfoOrderWise.CampaignType = dtcamdetail.Rows[0]["CampaignDetailId"].ToString();

                                        aList.Add(aOrderDetails);


                                    }
                                }




                            }

                            OrderDetails detailse = orderMasters.OrderDetails.First(x => x.ProductId == productId);
                            DataTable dtdata =
                                GetCampaignDetail(Convert.ToInt32(dtcampaigndataRow["CampgainMasterId"].ToString()),
                                    "");


                            if (detailse.IsCamp == false)
                            {
                                OrderDetails aOrderDetails = new OrderDetails();
                                aOrderDetails.ProductId = detailse.ProductId;
                                aOrderDetails.CustomerId = detailse.CustomerId;
                                aOrderDetails.Quantity = detailse.Quantity;
                                aOrderDetails.DiscountPercentage = detailse.DiscountPercentage;
                                aOrderDetails.DiscountValue = detailse.DiscountValue;
                                aOrderDetails.UnitPrice = detailse.UnitPrice;
                                aOrderDetails.TotalPrice = detailse.TotalPrice;
                                aOrderDetails.TotalVatAmount = detailse.TotalVatAmount;
                                aOrderDetails.VatPercentage = detailse.VatPercentage;
                                aOrderDetails.UnitVatAmount = detailse.UnitVatAmount;
                                aOrderDetails.NetAmount = detailse.NetAmount;
                                if (dtdata.Rows.Count > 0)
                                {
                                    aOrderDetails.CampaingName = dtdata.Rows[dtdata.Rows.Count - 1]["CampaignName"].ToString();
                                    aOrderDetails.CampaignType = dtdata.Rows[dtdata.Rows.Count - 1]["CampaignDetailId"].ToString();
                                }
                                else
                                {
                                    aOrderDetails.CampaingName = "";
                                    aOrderDetails.CampaignType = "";
                                }

                                aOrderDetails.IsGiftProduct = false;
                                aOrderDetails.IsCampaignProduct = false;
                                aOrderDetails.ProductName = detailse.ProductName;

                                aList.Add(aOrderDetails);

                                detailse.IsCamp = true;
                            }


                            if (dtdata.Rows.Count > 0)
                            {
                                foreach (DataRow dtdataRow in dtdata.Rows)
                                {
                                    decimal camqty = 0;
                                    decimal bonusqty = 0;
                                    decimal campaignproqty = 0;
                                    decimal orderqty = detailse.Quantity;

                                    bonusqty = string.IsNullOrEmpty(dtdataRow["BonusQuantity"].ToString())
                                        ? 0
                                        : Convert.ToDecimal(dtdataRow["BonusQuantity"].ToString());
                                    campaignproqty = string.IsNullOrEmpty(dtdata.Rows[0]["Quantity"].ToString())
                                        ? 0
                                        : Convert.ToDecimal(dtdataRow["Quantity"].ToString());
                                    camqty = Math.Floor(Convert.ToDecimal((orderqty / campaignproqty)));
                                    camqty = camqty * bonusqty;

                                    OrderDetails aOrderDetails = new OrderDetails();
                                    aOrderDetails.ProductId = Convert.ToInt32(dtdataRow["BonusProductId"].ToString());
                                    aOrderDetails.CustomerId = customerId;
                                    aOrderDetails.Quantity = (int)camqty;
                                    aOrderDetails.DiscountPercentage = 0;
                                    aOrderDetails.DiscountValue = 0;
                                    aOrderDetails.UnitPrice = 0;
                                    aOrderDetails.TotalPrice = 0;
                                    aOrderDetails.TotalVatAmount = 0;
                                    aOrderDetails.VatPercentage = 0;
                                    aOrderDetails.UnitVatAmount = 0;
                                    aOrderDetails.NetAmount = 0;
                                    aOrderDetails.IsCampaignProduct = true;
                                    aOrderDetails.ProductName = dtdataRow["ProductName"].ToString();
                                    aOrderDetails.CampaingName = dtdataRow["CampaignName"].ToString();
                                    aOrderDetails.CampaignType = dtdataRow["CampaignDetailId"].ToString();
                                    aOrderDetails.IsGiftProduct = true;
                                    aOrderDetails.IsCampaignProduct = true;

                                    aList.Add(aOrderDetails);
                                }
                            }

                        }




                    }
                    else if (dtcampaigndataRow["CampainTypeId"].ToString() == "2")
                    {
                        int productId = Convert.ToInt32(dtcampaigndataRow["BonusProductId"].ToString());
                        decimal minamount = string.IsNullOrEmpty(dtcampaigndataRow["Amount"].ToString())
                            ? 0
                            : Convert.ToDecimal(dtcampaigndataRow["Amount"].ToString());
                        decimal maxamount = string.IsNullOrEmpty(dtcampaigndataRow["MaxAmount"].ToString())
                            ? 0
                            : Convert.ToDecimal(dtcampaigndataRow["MaxAmount"].ToString());
                        //decimal productqty = orderMasters.OrderDetails.Where(x => x.ProductId == productId)
                        //    .Select(x => x.Quantity).First();

                        decimal proamount = 0;


                        OrderDetails aOrderDetailsa = orderMasters.OrderDetails.First(x => x.ProductId == productId);
                        DataTable dtpricecamp = GetPrice(aOrderDetailsa.ProductId, customerId);
                        if (dtpricecamp.Rows.Count > 0)
                        {
                            aOrderDetailsa.UnitPrice = Convert.ToDecimal(dtpricecamp.Rows[0]["UnitPrice"].ToString());
                            aOrderDetailsa.VatPercentage =
                                Convert.ToDecimal(dtpricecamp.Rows[0]["VATPercentage"].ToString());
                            aOrderDetailsa.UnitVatAmount =
                                Convert.ToDecimal(dtpricecamp.Rows[0]["VATAmountPerUnit"].ToString());
                            aOrderDetailsa.TotalPrice =
                                aOrderDetailsa.UnitPrice * aOrderDetailsa.Quantity;
                            aOrderDetailsa.TotalVatAmount =
                                aOrderDetailsa.UnitVatAmount * aOrderDetailsa.Quantity;
                            aOrderDetailsa.NetAmount =
                                aOrderDetailsa.TotalPrice + aOrderDetailsa.TotalVatAmount;
                        }
                        //aOrderDetailsa.CampaingName = "";
                        //aOrderDetailsa.CampaignType = "0";
                        //aOrderDetailsa.IsGiftProduct = false;

                        //aOrderDetailsa.IsCampaignProduct = true;


                        //aList.Add(aOrderDetailsa);

                        //aOrderDetailsa.IsCamp = true;
                        proamount = aOrderDetailsa.TotalPrice;

                        if (proamount >= minamount && proamount <= maxamount)
                        {


                            foreach (var campaignInfoOrderWise in orderMasters.OrderDetails)
                            {

                                DataTable dtprice = GetPrice(campaignInfoOrderWise.ProductId, customerId);
                                if (dtprice.Rows.Count > 0)
                                {
                                    campaignInfoOrderWise.UnitPrice =
                                        Convert.ToDecimal(dtprice.Rows[0]["UnitPrice"].ToString());
                                    campaignInfoOrderWise.VatPercentage =
                                        Convert.ToDecimal(dtprice.Rows[0]["VATPercentage"].ToString());
                                    campaignInfoOrderWise.UnitVatAmount =
                                        Convert.ToDecimal(dtprice.Rows[0]["VATAmountPerUnit"].ToString());
                                    campaignInfoOrderWise.TotalPrice =
                                        campaignInfoOrderWise.UnitPrice * campaignInfoOrderWise.Quantity;
                                    campaignInfoOrderWise.TotalVatAmount =
                                        campaignInfoOrderWise.UnitVatAmount * campaignInfoOrderWise.Quantity;
                                    campaignInfoOrderWise.NetAmount =
                                        campaignInfoOrderWise.TotalPrice + campaignInfoOrderWise.TotalVatAmount;
                                }

                                if (campaignInfoOrderWise.IsCamp == false)
                                {
                                    string bonustypeId = "";
                                    DataTable dtcamdetail = GetCampaignDetail(
                                        Convert.ToInt32(dtcampaigndataRow["CampgainMasterId"].ToString()),
                                        campaignInfoOrderWise.ProductId);
                                    if (dtcamdetail.Rows.Count > 0)
                                    {
                                        bonustypeId = dtcamdetail.Rows[0]["BonusTypeId"].ToString();
                                    }

                                    if (bonustypeId == "1")
                                    {
                                        decimal perctradeprice = 0;
                                        decimal perctotaltradeprice = 0;
                                        decimal dispercentage =
                                            string.IsNullOrEmpty(dtcamdetail.Rows[0]["DiscountPercentage"].ToString())
                                                ? 0
                                                : Convert.ToDecimal(
                                                    dtcamdetail.Rows[0]["DiscountPercentage"].ToString());
                                        perctotaltradeprice = campaignInfoOrderWise.TotalPrice * (dispercentage / 100);

                                        OrderDetails aOrderDetails = new OrderDetails();
                                        aOrderDetails.ProductId = campaignInfoOrderWise.ProductId;
                                        aOrderDetails.CustomerId = campaignInfoOrderWise.CustomerId;
                                        aOrderDetails.Quantity = campaignInfoOrderWise.Quantity;
                                        aOrderDetails.DiscountPercentage = dispercentage;
                                        aOrderDetails.DiscountValue = perctotaltradeprice;
                                        aOrderDetails.UnitPrice = campaignInfoOrderWise.UnitPrice;
                                        aOrderDetails.TotalPrice = campaignInfoOrderWise.TotalPrice;
                                        aOrderDetails.TotalVatAmount = campaignInfoOrderWise.TotalVatAmount;
                                        aOrderDetails.VatPercentage = campaignInfoOrderWise.VatPercentage;
                                        aOrderDetails.UnitVatAmount = campaignInfoOrderWise.UnitVatAmount;
                                        aOrderDetails.NetAmount = campaignInfoOrderWise.NetAmount - perctotaltradeprice;
                                        aOrderDetails.CampaingName = dtcamdetail.Rows[0]["CampaignName"].ToString();
                                        aOrderDetails.CampaignType = dtcamdetail.Rows[0]["CampaignDetailId"].ToString();
                                        aOrderDetails.IsGiftProduct = false;
                                        aOrderDetails.IsCampaignProduct = true;
                                        aOrderDetails.ProductName = campaignInfoOrderWise.ProductName;

                                        campaignInfoOrderWise.IsCamp = true;

                                        aList.Add(aOrderDetails);


                                    }

                                    if (bonustypeId == "2")
                                    {
                                        decimal perctradeprice = 0;
                                        //decimal perctotaltradeprice = 0;
                                        decimal perctotaltradeprice =
                                            string.IsNullOrEmpty(dtcamdetail.Rows[0]["QuantityDteail"].ToString())
                                                ? 0
                                                : Convert.ToDecimal(dtcamdetail.Rows[0]["QuantityDteail"].ToString());
                                        //perctotaltradeprice = campaignInfoOrderWise.TotalPrice * (dispercentage / 100);

                                        OrderDetails aOrderDetails = new OrderDetails();
                                        aOrderDetails.ProductId = campaignInfoOrderWise.ProductId;
                                        aOrderDetails.CustomerId = campaignInfoOrderWise.CustomerId;
                                        aOrderDetails.Quantity = campaignInfoOrderWise.Quantity;
                                        aOrderDetails.DiscountPercentage = 0;
                                        aOrderDetails.DiscountValue = perctotaltradeprice;
                                        aOrderDetails.UnitPrice = campaignInfoOrderWise.UnitPrice;
                                        aOrderDetails.TotalPrice = campaignInfoOrderWise.TotalPrice;
                                        aOrderDetails.TotalVatAmount = campaignInfoOrderWise.TotalVatAmount;
                                        aOrderDetails.VatPercentage = campaignInfoOrderWise.VatPercentage;
                                        aOrderDetails.UnitVatAmount = campaignInfoOrderWise.UnitVatAmount;
                                        aOrderDetails.NetAmount = campaignInfoOrderWise.NetAmount - perctotaltradeprice;
                                        aOrderDetails.CampaingName = dtcamdetail.Rows[0]["CampaignName"].ToString();
                                        aOrderDetails.CampaignType = dtcamdetail.Rows[0]["CampaignDetailId"].ToString();
                                        aOrderDetails.IsGiftProduct = false;
                                        aOrderDetails.ProductName = campaignInfoOrderWise.ProductName;
                                        aOrderDetails.IsCampaignProduct = true;
                                        aOrderDetails.IsCamp = true;
                                        campaignInfoOrderWise.IsCamp = true;
                                        aList.Add(aOrderDetails);


                                    }

                                    if (bonustypeId == "3")
                                    {
                                        decimal perctradeprice = 0;
                                        //decimal perctotaltradeprice = 0;
                                        decimal perctotaltradeprice =
                                            string.IsNullOrEmpty(dtcamdetail.Rows[0]["QuantityDteail"].ToString())
                                                ? 0
                                                : Convert.ToDecimal(dtcamdetail.Rows[0]["QuantityDteail"].ToString());
                                        //perctotaltradeprice = campaignInfoOrderWise.TotalPrice * (dispercentage / 100);

                                        OrderDetails aOrderDetails = new OrderDetails();
                                        aOrderDetails.ProductId = campaignInfoOrderWise.ProductId;
                                        aOrderDetails.CustomerId = campaignInfoOrderWise.CustomerId;
                                        aOrderDetails.Quantity = campaignInfoOrderWise.Quantity;
                                        aOrderDetails.DiscountPercentage = 0;
                                        aOrderDetails.DiscountValue = perctotaltradeprice;
                                        aOrderDetails.UnitPrice = campaignInfoOrderWise.UnitPrice - perctotaltradeprice;
                                        aOrderDetails.TotalPrice = aOrderDetails.UnitPrice * aOrderDetails.Quantity;
                                        aOrderDetails.TotalVatAmount = campaignInfoOrderWise.TotalVatAmount;
                                        aOrderDetails.VatPercentage = campaignInfoOrderWise.VatPercentage;
                                        aOrderDetails.UnitVatAmount = campaignInfoOrderWise.UnitVatAmount;
                                        aOrderDetails.NetAmount =
                                            aOrderDetails.TotalPrice + aOrderDetails.TotalVatAmount -
                                            perctotaltradeprice;
                                        aOrderDetails.CampaingName = dtcamdetail.Rows[0]["CampaignName"].ToString();
                                        aOrderDetails.CampaignType = dtcamdetail.Rows[0]["CampaignDetailId"].ToString();
                                        aOrderDetails.IsGiftProduct = false;
                                        aOrderDetails.ProductName = campaignInfoOrderWise.ProductName;
                                        aOrderDetails.IsCampaignProduct = true;
                                        aOrderDetails.IsCamp = true;
                                        campaignInfoOrderWise.IsCamp = true;
                                        aList.Add(aOrderDetails);


                                    }
                                }




                            }
                            DataTable dtdata =
                                GetCampaignDetail(Convert.ToInt32(dtcampaigndataRow["CampgainMasterId"].ToString()),
                                    "");
                            OrderDetails detailse = orderMasters.OrderDetails.First(x => x.ProductId == productId);

                            if (detailse.IsCamp == false)
                            {
                                OrderDetails aOrderDetails = new OrderDetails();
                                aOrderDetails.ProductId = detailse.ProductId;
                                aOrderDetails.CustomerId = detailse.CustomerId;
                                aOrderDetails.Quantity = detailse.Quantity;
                                aOrderDetails.DiscountPercentage = detailse.DiscountPercentage;
                                aOrderDetails.DiscountValue = detailse.DiscountValue;
                                aOrderDetails.UnitPrice = detailse.UnitPrice;
                                aOrderDetails.TotalPrice = detailse.TotalPrice;
                                aOrderDetails.TotalVatAmount = detailse.TotalVatAmount;
                                aOrderDetails.VatPercentage = detailse.VatPercentage;
                                aOrderDetails.UnitVatAmount = detailse.UnitVatAmount;
                                aOrderDetails.NetAmount = detailse.NetAmount;
                                if (dtdata.Rows.Count > 0)
                                {
                                    aOrderDetails.CampaingName = dtdata.Rows[dtdata.Rows.Count - 1]["CampaignName"].ToString();
                                    aOrderDetails.CampaignType = dtdata.Rows[dtdata.Rows.Count - 1]["CampaignDetailId"].ToString();
                                }
                                else
                                {
                                    aOrderDetails.CampaingName = "";
                                    aOrderDetails.CampaignType = "";
                                }
                                aOrderDetails.IsGiftProduct = false;
                                aOrderDetails.IsCampaignProduct = false;
                                aOrderDetails.ProductName = detailse.ProductName;

                                aList.Add(aOrderDetails);

                                detailse.IsCamp = true;
                            }


                            if (dtdata.Rows.Count > 0)
                            {
                                foreach (DataRow dtdataRow in dtdata.Rows)
                                {
                                    decimal camqty = 0;
                                    decimal bonusqty = 0;
                                    decimal campaignproqty = 0;
                                    decimal orderqty = 0;

                                    bonusqty = string.IsNullOrEmpty(dtdataRow["BonusQuantity"].ToString())
                                        ? 0
                                        : Convert.ToDecimal(dtdataRow["BonusQuantity"].ToString());
                                    //campaignproqty = string.IsNullOrEmpty(dtdata.Rows[0]["Quantity"].ToString())
                                    //    ? 0
                                    //    : Convert.ToDecimal(dtdataRow["Quantity"].ToString());
                                    //camqty = Math.Floor(Convert.ToDecimal((orderqty / campaignproqty)));
                                    //camqty = camqty * bonusqty;


                                    OrderDetails aOrderDetails = new OrderDetails();
                                    aOrderDetails.ProductId = Convert.ToInt32(dtdataRow["BonusProductId"].ToString());
                                    aOrderDetails.CustomerId = customerId;
                                    //aOrderDetails.Quantity = (int)camqty;
                                    aOrderDetails.Quantity = (int)bonusqty;
                                    aOrderDetails.DiscountPercentage = 0;
                                    aOrderDetails.DiscountValue = 0;
                                    aOrderDetails.UnitPrice = 0;
                                    aOrderDetails.TotalPrice = 0;
                                    aOrderDetails.TotalVatAmount = 0;
                                    aOrderDetails.VatPercentage = 0;
                                    aOrderDetails.UnitVatAmount = 0;
                                    aOrderDetails.NetAmount = 0;
                                    aOrderDetails.IsCampaignProduct = true;
                                    aOrderDetails.ProductName = dtdataRow["ProductName"].ToString();
                                    aOrderDetails.CampaingName = dtdataRow["CampaignName"].ToString();
                                    aOrderDetails.CampaignType = dtdataRow["CampaignDetailId"].ToString();
                                    aOrderDetails.IsGiftProduct = true;
                                    aOrderDetails.IsCampaignProduct = true;

                                    aList.Add(aOrderDetails);
                                }
                            }

                        }
                    }
                    else if (dtcampaigndataRow["CampainTypeId"].ToString() == "3")
                    {


                        foreach (var campaignInfoOrderWise in orderMasters.OrderDetails)
                        {
                            if (campaignInfoOrderWise.IsCamp == false)
                            {
                                DataTable dtprice = GetPrice(campaignInfoOrderWise.ProductId, customerId);
                                if (dtprice.Rows.Count > 0)
                                {
                                    campaignInfoOrderWise.UnitPrice =
                                        Convert.ToDecimal(dtprice.Rows[0]["UnitPrice"].ToString());
                                    campaignInfoOrderWise.VatPercentage =
                                        Convert.ToDecimal(dtprice.Rows[0]["VATPercentage"].ToString());
                                    campaignInfoOrderWise.UnitVatAmount =
                                        Convert.ToDecimal(dtprice.Rows[0]["VATAmountPerUnit"].ToString());
                                    campaignInfoOrderWise.TotalPrice =
                                        campaignInfoOrderWise.UnitPrice * campaignInfoOrderWise.Quantity;
                                    campaignInfoOrderWise.TotalVatAmount =
                                        campaignInfoOrderWise.UnitVatAmount * campaignInfoOrderWise.Quantity;
                                    campaignInfoOrderWise.NetAmount =
                                        campaignInfoOrderWise.TotalPrice + campaignInfoOrderWise.TotalVatAmount;
                                }


                                string bonustypeId = "";
                                DataTable dtcamdetail = GetCampaignDetail(
                                    Convert.ToInt32(dtcampaigndataRow["CampgainMasterId"].ToString()),
                                    campaignInfoOrderWise.ProductId);
                                if (dtcamdetail.Rows.Count > 0)
                                {
                                    bonustypeId = dtcamdetail.Rows[0]["BonusTypeId"].ToString();
                                }

                                if (bonustypeId == "1")
                                {
                                    decimal perctradeprice = 0;
                                    decimal perctotaltradeprice = 0;
                                    decimal dispercentage =
                                        string.IsNullOrEmpty(dtcamdetail.Rows[0]["DiscountPercentage"].ToString())
                                            ? 0
                                            : Convert.ToDecimal(
                                                dtcamdetail.Rows[0]["DiscountPercentage"].ToString());
                                    perctotaltradeprice = campaignInfoOrderWise.TotalPrice * (dispercentage / 100);

                                    OrderDetails aOrderDetails = new OrderDetails();
                                    aOrderDetails.ProductId = campaignInfoOrderWise.ProductId;
                                    aOrderDetails.CustomerId = campaignInfoOrderWise.CustomerId;
                                    aOrderDetails.Quantity = campaignInfoOrderWise.Quantity;
                                    aOrderDetails.DiscountPercentage = dispercentage;
                                    aOrderDetails.DiscountValue = perctotaltradeprice;
                                    aOrderDetails.UnitPrice = campaignInfoOrderWise.UnitPrice;
                                    aOrderDetails.TotalPrice = campaignInfoOrderWise.TotalPrice;
                                    aOrderDetails.TotalVatAmount = campaignInfoOrderWise.TotalVatAmount;
                                    aOrderDetails.VatPercentage = campaignInfoOrderWise.VatPercentage;
                                    aOrderDetails.UnitVatAmount = campaignInfoOrderWise.UnitVatAmount;
                                    aOrderDetails.NetAmount = campaignInfoOrderWise.NetAmount - perctotaltradeprice;
                                    aOrderDetails.CampaingName = dtcamdetail.Rows[0]["CampaignName"].ToString();
                                    aOrderDetails.CampaignType = dtcamdetail.Rows[0]["CampaignDetailId"].ToString();
                                    aOrderDetails.IsGiftProduct = false;
                                    aOrderDetails.IsCampaignProduct = true;
                                    aOrderDetails.ProductName = campaignInfoOrderWise.ProductName;

                                    campaignInfoOrderWise.IsCamp = true;

                                    aList.Add(aOrderDetails);


                                }

                                //if (bonustypeId == "2")
                                //{
                                //    decimal perctradeprice = 0;
                                //    //decimal perctotaltradeprice = 0;
                                //    decimal perctotaltradeprice =
                                //        string.IsNullOrEmpty(dtcamdetail.Rows[0]["QuantityDteail"].ToString())
                                //            ? 0
                                //            : Convert.ToDecimal(dtcamdetail.Rows[0]["QuantityDteail"].ToString());
                                //    //perctotaltradeprice = campaignInfoOrderWise.TotalPrice * (dispercentage / 100);

                                //    OrderDetails aOrderDetails = new OrderDetails();
                                //    aOrderDetails.ProductId = campaignInfoOrderWise.ProductId;
                                //    aOrderDetails.CustomerId = campaignInfoOrderWise.CustomerId;
                                //    aOrderDetails.Quantity = campaignInfoOrderWise.Quantity;
                                //    aOrderDetails.DiscountPercentage = 0;
                                //    aOrderDetails.DiscountValue = perctotaltradeprice;
                                //    aOrderDetails.UnitPrice = campaignInfoOrderWise.UnitPrice;
                                //    aOrderDetails.TotalPrice = campaignInfoOrderWise.TotalPrice;
                                //    aOrderDetails.TotalVatAmount = campaignInfoOrderWise.TotalVatAmount;
                                //    aOrderDetails.VatPercentage = campaignInfoOrderWise.VatPercentage;
                                //    aOrderDetails.UnitVatAmount = campaignInfoOrderWise.UnitVatAmount;
                                //    aOrderDetails.NetAmount = campaignInfoOrderWise.NetAmount - perctotaltradeprice;
                                //    aOrderDetails.CampaingName = dtcamdetail.Rows[0]["CampaignName"].ToString();
                                //    aOrderDetails.CampaignType = dtcamdetail.Rows[0]["CampaignDetailId"].ToString();
                                //    aOrderDetails.IsGiftProduct = false;
                                //    aOrderDetails.ProductName = campaignInfoOrderWise.ProductName;
                                //    aOrderDetails.IsCampaignProduct = true;
                                //    aOrderDetails.IsCamp = true;
                                //    campaignInfoOrderWise.IsCamp = true;
                                //    aList.Add(aOrderDetails);


                                //}

                                //if (bonustypeId == "3")
                                //{
                                //    decimal perctradeprice = 0;
                                //    //decimal perctotaltradeprice = 0;
                                //    decimal perctotaltradeprice =
                                //        string.IsNullOrEmpty(dtcamdetail.Rows[0]["QuantityDteail"].ToString())
                                //            ? 0
                                //            : Convert.ToDecimal(dtcamdetail.Rows[0]["QuantityDteail"].ToString());
                                //    //perctotaltradeprice = campaignInfoOrderWise.TotalPrice * (dispercentage / 100);

                                //    OrderDetails aOrderDetails = new OrderDetails();
                                //    aOrderDetails.ProductId = campaignInfoOrderWise.ProductId;
                                //    aOrderDetails.CustomerId = campaignInfoOrderWise.CustomerId;
                                //    aOrderDetails.Quantity = campaignInfoOrderWise.Quantity;
                                //    aOrderDetails.DiscountPercentage = 0;
                                //    aOrderDetails.DiscountValue = perctotaltradeprice;
                                //    aOrderDetails.UnitPrice = campaignInfoOrderWise.UnitPrice - perctotaltradeprice;
                                //    aOrderDetails.TotalPrice = aOrderDetails.UnitPrice * aOrderDetails.Quantity;
                                //    aOrderDetails.TotalVatAmount = campaignInfoOrderWise.TotalVatAmount;
                                //    aOrderDetails.VatPercentage = campaignInfoOrderWise.VatPercentage;
                                //    aOrderDetails.UnitVatAmount = campaignInfoOrderWise.UnitVatAmount;
                                //    aOrderDetails.NetAmount =
                                //        aOrderDetails.TotalPrice + aOrderDetails.TotalVatAmount -
                                //        perctotaltradeprice;
                                //    aOrderDetails.CampaingName = dtcamdetail.Rows[0]["CampaignName"].ToString();
                                //    aOrderDetails.CampaignType = dtcamdetail.Rows[0]["CampaignDetailId"].ToString();
                                //    aOrderDetails.IsGiftProduct = false;
                                //    aOrderDetails.ProductName = campaignInfoOrderWise.ProductName;
                                //    aOrderDetails.IsCampaignProduct = true;
                                //    aOrderDetails.IsCamp = true;
                                //    campaignInfoOrderWise.IsCamp = true;
                                //    aList.Add(aOrderDetails);


                                //}


                            }

                            if (campaignInfoOrderWise.IsCamp == false)
                            {



                                string btype = "";
                                DataTable dtothertype = GetCampaignDetailParam(
                                    Convert.ToInt32(dtcampaigndataRow["CampgainMasterId"].ToString()),
                                    " AND  BonusTypeId in (6,7)");
                                if (dtothertype.Rows.Count > 0)
                                {
                                    btype = dtothertype.Rows[0]["BonusTypeId"].ToString();
                                }

                                if (btype == "6")
                                {
                                    decimal perctradeprice = 0;
                                    decimal perctotaltradeprice = 0;
                                    decimal dispercentage =
                                        string.IsNullOrEmpty(dtothertype.Rows[0]["DiscountPercentage"].ToString())
                                            ? 0
                                            : Convert.ToDecimal(
                                                dtothertype.Rows[0]["DiscountPercentage"].ToString());
                                    perctotaltradeprice = campaignInfoOrderWise.TotalPrice * (dispercentage / 100);

                                    OrderDetails aOrderDetails = new OrderDetails();
                                    aOrderDetails.ProductId = campaignInfoOrderWise.ProductId;
                                    aOrderDetails.CustomerId = campaignInfoOrderWise.CustomerId;
                                    aOrderDetails.Quantity = campaignInfoOrderWise.Quantity;
                                    aOrderDetails.DiscountPercentage = dispercentage;
                                    aOrderDetails.DiscountValue = perctotaltradeprice;
                                    aOrderDetails.UnitPrice = campaignInfoOrderWise.UnitPrice;
                                    aOrderDetails.TotalPrice = campaignInfoOrderWise.TotalPrice;
                                    aOrderDetails.TotalVatAmount = campaignInfoOrderWise.TotalVatAmount;
                                    aOrderDetails.VatPercentage = campaignInfoOrderWise.VatPercentage;
                                    aOrderDetails.UnitVatAmount = campaignInfoOrderWise.UnitVatAmount;
                                    aOrderDetails.NetAmount = campaignInfoOrderWise.NetAmount - perctotaltradeprice;
                                    aOrderDetails.CampaingName = dtothertype.Rows[0]["CampaignName"].ToString();
                                    aOrderDetails.CampaignType = dtothertype.Rows[0]["CampaignDetailId"].ToString();
                                    aOrderDetails.IsGiftProduct = false;
                                    aOrderDetails.IsCampaignProduct = true;
                                    aOrderDetails.ProductName = campaignInfoOrderWise.ProductName;

                                    campaignInfoOrderWise.IsCamp = true;

                                    aList.Add(aOrderDetails);
                                }

                                if (btype == "7")
                                {
                                    decimal perctradeprice = 0;
                                    //decimal perctotaltradeprice = 0;
                                    decimal perctotaltradeprice =
                                        string.IsNullOrEmpty(dtothertype.Rows[0]["QuantityDteail"].ToString())
                                            ? 0
                                            : Convert.ToDecimal(dtothertype.Rows[0]["QuantityDteail"].ToString());
                                    //perctotaltradeprice = campaignInfoOrderWise.TotalPrice * (dispercentage / 100);

                                    OrderDetails aOrderDetails = new OrderDetails();
                                    aOrderDetails.ProductId = campaignInfoOrderWise.ProductId;
                                    aOrderDetails.CustomerId = campaignInfoOrderWise.CustomerId;
                                    aOrderDetails.Quantity = campaignInfoOrderWise.Quantity;
                                    aOrderDetails.DiscountPercentage = 0;
                                    aOrderDetails.DiscountValue = perctotaltradeprice;
                                    aOrderDetails.UnitPrice = campaignInfoOrderWise.UnitPrice;
                                    aOrderDetails.TotalPrice = campaignInfoOrderWise.TotalPrice;
                                    aOrderDetails.TotalVatAmount = campaignInfoOrderWise.TotalVatAmount;
                                    aOrderDetails.VatPercentage = campaignInfoOrderWise.VatPercentage;
                                    aOrderDetails.UnitVatAmount = campaignInfoOrderWise.UnitVatAmount;
                                    aOrderDetails.NetAmount = campaignInfoOrderWise.NetAmount - perctotaltradeprice;
                                    aOrderDetails.CampaingName = dtothertype.Rows[0]["CampaignName"].ToString();
                                    aOrderDetails.CampaignType = dtothertype.Rows[0]["CampaignDetailId"].ToString();
                                    aOrderDetails.IsGiftProduct = false;
                                    aOrderDetails.ProductName = campaignInfoOrderWise.ProductName;
                                    aOrderDetails.IsCampaignProduct = true;
                                    aOrderDetails.IsCamp = true;
                                    campaignInfoOrderWise.IsCamp = true;
                                    aList.Add(aOrderDetails);
                                }




                            }

                        }

                        DataTable dtdata =
                            GetCampaignDetail(Convert.ToInt32(dtcampaigndataRow["CampgainMasterId"].ToString()),
                                "");
                        if (dtdata.Rows.Count > 0)
                        {
                            foreach (DataRow dtdataRow in dtdata.Rows)
                            {
                                decimal camqty = 0;
                                decimal bonusqty = 0;
                                decimal campaignproqty = 0;
                                decimal orderqty = 0;

                                bonusqty = string.IsNullOrEmpty(dtdataRow["BonusQuantity"].ToString())
                                    ? 0
                                    : Convert.ToDecimal(dtdataRow["BonusQuantity"].ToString());
                                //campaignproqty = string.IsNullOrEmpty(dtdata.Rows[0]["Quantity"].ToString())
                                //    ? 0
                                //    : Convert.ToDecimal(dtdataRow["Quantity"].ToString());
                                //camqty = Math.Floor(Convert.ToDecimal((orderqty / campaignproqty)));
                                //camqty = camqty * bonusqty;


                                OrderDetails aOrderDetails = new OrderDetails();
                                aOrderDetails.ProductId = Convert.ToInt32(dtdataRow["BonusProductId"].ToString());
                                aOrderDetails.CustomerId = customerId;
                                //aOrderDetails.Quantity = (int)camqty;
                                aOrderDetails.Quantity = (int)bonusqty;
                                aOrderDetails.DiscountPercentage = 0;
                                aOrderDetails.DiscountValue = 0;
                                aOrderDetails.UnitPrice = 0;
                                aOrderDetails.TotalPrice = 0;
                                aOrderDetails.TotalVatAmount = 0;
                                aOrderDetails.VatPercentage = 0;
                                aOrderDetails.UnitVatAmount = 0;
                                aOrderDetails.NetAmount = 0;
                                aOrderDetails.IsCampaignProduct = true;
                                aOrderDetails.ProductName = dtdataRow["ProductName"].ToString();
                                aOrderDetails.CampaingName = dtdataRow["CampaignName"].ToString();
                                aOrderDetails.CampaignType = dtdataRow["CampaignDetailId"].ToString();
                                aOrderDetails.IsGiftProduct = true;
                                aOrderDetails.IsCampaignProduct = true;

                                aList.Add(aOrderDetails);
                            }
                            foreach (var detailse in orderMasters.OrderDetails)
                            {
                                if (detailse.IsCamp == false)
                                {
                                    OrderDetails aOrderDetails = new OrderDetails();
                                    aOrderDetails.ProductId = detailse.ProductId;
                                    aOrderDetails.CustomerId = detailse.CustomerId;
                                    aOrderDetails.Quantity = detailse.Quantity;
                                    aOrderDetails.DiscountPercentage = detailse.DiscountPercentage;
                                    aOrderDetails.DiscountValue = detailse.DiscountValue;
                                    aOrderDetails.UnitPrice = detailse.UnitPrice;
                                    aOrderDetails.TotalPrice = detailse.TotalPrice;
                                    aOrderDetails.TotalVatAmount = detailse.TotalVatAmount;
                                    aOrderDetails.VatPercentage = detailse.VatPercentage;
                                    aOrderDetails.UnitVatAmount = detailse.UnitVatAmount;
                                    aOrderDetails.NetAmount = detailse.NetAmount;
                                    aOrderDetails.CampaingName = dtdata.Rows[0]["CampaignName"].ToString();
                                    aOrderDetails.CampaignType = dtdata.Rows[0]["CampaignDetailId"].ToString();
                                    aOrderDetails.IsGiftProduct = false;
                                    aOrderDetails.IsCampaignProduct = true;
                                    aOrderDetails.ProductName = detailse.ProductName;

                                    aList.Add(aOrderDetails);

                                    detailse.IsCamp = true;
                                }
                            }
                        }



                        //decimal dispercampaigntrade = 0;
                        ////DataTable dtdisperc = GetCampaignTradePolicyProductPerc(remainingtotal, customerId, custtypeid);
                        ////if (dtdisperc.Rows.Count > 0)
                        //{
                        //    dispercampaigntrade = Convert.ToDecimal(dtcampaigndataRow["DiscountPercentage"].ToString());

                        //    foreach (var detailse in orderMasters.OrderDetails)
                        //    {
                        //        if (detailse.IsCamp == false)
                        //        {
                        //            OrderDetails aOrderDetails = new OrderDetails();
                        //            aOrderDetails.ProductId = detailse.ProductId;
                        //            aOrderDetails.CustomerId = detailse.CustomerId;
                        //            aOrderDetails.Quantity = detailse.Quantity;
                        //            aOrderDetails.DiscountPercentage = dispercampaigntrade;
                        //            aOrderDetails.DiscountValue =
                        //                detailse.TotalPrice * (dispercampaigntrade / 100);
                        //            aOrderDetails.UnitPrice = detailse.UnitPrice;
                        //            aOrderDetails.TotalPrice = detailse.TotalPrice;
                        //            aOrderDetails.TotalVatAmount = detailse.TotalVatAmount;
                        //            aOrderDetails.VatPercentage = detailse.VatPercentage;
                        //            aOrderDetails.UnitVatAmount = detailse.UnitVatAmount;
                        //            aOrderDetails.NetAmount =
                        //                detailse.NetAmount - detailse.TotalPrice *
                        //                (dispercampaigntrade / 100);
                        //            aOrderDetails.CampaingName = dtcampaigndataRow["CampaignName"].ToString();
                        //            aOrderDetails.CampaignType = dtcampaigndataRow["CampaignDetailId"].ToString();
                        //            aOrderDetails.IsGiftProduct = false;
                        //            aOrderDetails.IsCampaignProduct = true;
                        //            aOrderDetails.ProductName = detailse.ProductName;

                        //            aList.Add(aOrderDetails);

                        //            detailse.IsCamp = true;
                        //        }
                        //    }


                        //}



                    }

                }


                foreach (var detailse in orderMasters.OrderDetails)
                {

                    if (detailse.IsCamp == false)
                    {
                        OrderDetails aOrderDetails = new OrderDetails();
                        aOrderDetails.ProductId = detailse.ProductId;
                        aOrderDetails.CustomerId = detailse.CustomerId;
                        aOrderDetails.Quantity = detailse.Quantity;
                        aOrderDetails.DiscountPercentage = detailse.DiscountPercentage;
                        aOrderDetails.DiscountValue = detailse.DiscountValue;
                        aOrderDetails.UnitPrice = detailse.UnitPrice;
                        aOrderDetails.TotalPrice = detailse.TotalPrice;
                        aOrderDetails.TotalVatAmount = detailse.TotalVatAmount;
                        aOrderDetails.VatPercentage = detailse.VatPercentage;
                        aOrderDetails.UnitVatAmount = detailse.UnitVatAmount;
                        aOrderDetails.NetAmount = detailse.NetAmount;
                        aOrderDetails.CampaingName = "";
                        aOrderDetails.CampaignType = "";
                        aOrderDetails.IsGiftProduct = false;
                        aOrderDetails.IsCampaignProduct = false;
                        aOrderDetails.ProductName = detailse.ProductName;

                        aList.Add(aOrderDetails);

                        detailse.IsCamp = true;
                    }
                }

                //decimal remainingtotal = 0;
                //foreach (var aOrderDetailse in orderMasters.OrderDetails)
                //{
                //    if (aOrderDetailse.IsCamp == false)
                //    {
                //        remainingtotal += aOrderDetailse.TotalPrice;
                //    }
                //}

                //decimal dispercampaigntradea = 0;
                //DataTable dtdisperc = GetCampaignTradePolicyProductPerc(remainingtotal, customerId, custtypeid);
                //if (dtdisperc.Rows.Count > 0)
                //{
                //    dispercampaigntradea = Convert.ToDecimal(dtdisperc.Rows[0]["DiscountPercentage"].ToString());

                //    foreach (var detailse in orderMasters.OrderDetails)
                //    {
                //        if (detailse.IsCamp == false)
                //        {
                //            OrderDetails aOrderDetails = new OrderDetails();
                //            aOrderDetails.ProductId = detailse.ProductId;
                //            aOrderDetails.CustomerId = detailse.CustomerId;
                //            aOrderDetails.Quantity = detailse.Quantity;
                //            aOrderDetails.DiscountPercentage = dispercampaigntradea;
                //            aOrderDetails.DiscountValue =
                //                detailse.TotalPrice * (dispercampaigntradea / 100);
                //            aOrderDetails.UnitPrice = detailse.UnitPrice;
                //            aOrderDetails.TotalPrice = detailse.TotalPrice;
                //            aOrderDetails.TotalVatAmount = detailse.TotalVatAmount;
                //            aOrderDetails.VatPercentage = detailse.VatPercentage;
                //            aOrderDetails.UnitVatAmount = detailse.UnitVatAmount;
                //            aOrderDetails.NetAmount =
                //                detailse.NetAmount - detailse.TotalPrice *
                //                (dispercampaigntradea / 100);
                //            aOrderDetails.CampaingName = dtdisperc.Rows[0]["CampaignName"].ToString();
                //            aOrderDetails.CampaignType = dtdisperc.Rows[0]["CampaignDetailId"].ToString();
                //            aOrderDetails.IsGiftProduct = false;
                //            aOrderDetails.IsCampaignProduct = true;
                //            aOrderDetails.ProductName = detailse.ProductName;

                //            aList.Add(aOrderDetails);

                //            detailse.IsCamp = true;
                //        }
                //    }


                //}
                //else
                //{

                //}




            }
            else
            {
                aList = null;
            }




            return aList;
        }


        public List<CampaignDetail> GetDetail(int campId)
        {
            List<CampaignDetail> camList = new List<CampaignDetail>();

            try
            {

                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> paramInside = new List<SqlParameter>();
                paramInside.Add(new SqlParameter("@CampgainMasterId", campId));
                SqlDataReader dr2 = accessManager.GetSqlDataReader("sp_OPAPI_GETCamaignDetail", paramInside);
                while (dr2.Read())
                {
                    CampaignDetail camInfo = new CampaignDetail();
                    if (dr2["CampaignDetailId"] != DBNull.Value) camInfo.CampaignDetailId = Convert.ToInt32(dr2["CampaignDetailId"]);
                    //if (dr2["MinAmount"] != DBNull.Value) camInfo.MinAmount = Convert.ToDecimal(dr2["MinAmount"]);
                    //if (dr2["MaxAmount"] != DBNull.Value) camInfo.MaxAmount = Convert.ToDecimal(dr2["MaxAmount"]);
                    if (dr2["DiscountPercentage"] != DBNull.Value) camInfo.DiscountPercentage = Convert.ToDecimal(dr2["DiscountPercentage"]);
                    if (dr2["DiscountAmount"] != DBNull.Value) camInfo.DiscountAmount = Convert.ToDecimal(dr2["DiscountAmount"]);
                    if (dr2["ProductId"] != DBNull.Value) camInfo.ProductId = Convert.ToInt32(dr2["ProductId"]);
                    if (dr2["Quantity"] != DBNull.Value) camInfo.Quantity = Convert.ToInt32(dr2["Quantity"]);
                    if (dr2["BonusProductId"] != DBNull.Value) camInfo.BonusProductId = Convert.ToInt32(dr2["BonusProductId"]);
                    if (dr2["BonusQuantity"] != DBNull.Value) camInfo.BonusQuantity = Convert.ToInt32(dr2["BonusQuantity"]);
                    if (dr2["TypeName"] != DBNull.Value) camInfo.TypeName = dr2["TypeName"].ToString();
                    if (dr2["CodeName"] != DBNull.Value) camInfo.CodeName = dr2["CodeName"].ToString();
                    if (dr2["campaignName"] != DBNull.Value) camInfo.campaignName = dr2["campaignName"].ToString();

                    camList.Add(camInfo);

                }

            }
            catch (Exception ex)
            {
                throw ex;


            }

            return camList;


        }
    }
}
