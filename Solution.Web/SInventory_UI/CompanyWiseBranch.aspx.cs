using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.SInventory_DAL;
using Library.DAO.SInventory_Entities;

public partial class SInventory_UI_CompanyWiseBranch : System.Web.UI.Page
{
    CompanywisebranchDal aDal = new CompanywisebranchDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Loaddropdownlist();

            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                hfMasterId.Value = Request.QueryString["mid"].ToString();
                GetOneRecord(Request.QueryString["mid"].ToString());
            }
        }
    }




    private void GetOneRecord(string Id)
    {
        try
        {
            DataTable aTable = aDal.GetDepositSlip(Id);
            if (aTable.Rows.Count > 0)
            {
                string payment = aTable.Rows[0]["DepositType"].ToString();
                switch (payment)
                {
                    case "Cash":
                        CheckBoxList1.SelectedValue = "Cash";
                        break;
                    case "Online":
                        CheckBoxList1.SelectedValue = "Online";
                        break;
                    case "Check":
                        CheckBoxList1.SelectedValue = "Check";
                        break;
                    case "Other":
                        CheckBoxList1.SelectedValue = "Other";
                        break;
                    case "DD":
                        CheckBoxList1.SelectedValue = "DD";
                        break;
                    default:
                        CheckBoxList1.SelectedValue = "";
                        break;
                }


                if (aTable.Rows[0]["FileName"] != null)
                {
                    hfImage.Value = aTable.Rows[0]["FileName"].ToString();
                 
                }

                companyNameDropDownList.SelectedValue = aTable.Rows[0]["CompanyId"].ToString();
                ddlBank.SelectedValue = aTable.Rows[0]["BankId"].ToString();
                ddlBank_OnSelectedIndexChanged(null, null);
          
                branchTextBox.Text = aTable.Rows[0]["BranchName"].ToString();
                chkTextBox.Text = aTable.Rows[0]["CheckNumber"].ToString();
                chkDateTextBox.Text = aTable.Rows[0]["CheckDate"].ToString();
                dateTextBox.Text = aTable.Rows[0]["DepositDate"].ToString();
                amount.Text = aTable.Rows[0]["Amount"].ToString();
                if (aTable.Rows[0]["Amount"] != null)
                {
                    AITTextBox1.Text = aTable.Rows[0]["AIT"].ToString();
                }
                if (aTable.Rows[0]["ReferenceName"] != null)
                {
                    txtReferenceName.Text = aTable.Rows[0]["ReferenceName"].ToString();
                }
                if (aTable.Rows[0]["Remarks"] != null)
                {
                    remarksTextBox.Text = aTable.Rows[0]["Remarks"].ToString();
                }

                if (payment != "Cash" || payment != "Online")
                {
                    branch.Visible = false;
                    chkNo.Visible = false;
                    chkDate.Visible = false;
                }
                else
                {
                    branch.Visible = true;
                    chkNo.Visible = true;
                    chkDate.Visible = true;
                }
            }
        }
        catch (Exception ex) { }
    }


    private void Loaddropdownlist()
    {
        aDal.LoadCompany(companyNameDropDownList, Session["UserId"].ToString());
        aDal.LoadBank(ddlBank);
    }


    protected void ddlBank_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBank.SelectedValue != "")
        {
           DataTable aTable = aDal.LoadBankById(ddlBank.SelectedValue);
           txtAccountName.Text = aTable.Rows[0]["AccountName"].ToString();
           accNameTextBox.Text = aTable.Rows[0]["AccountNumber"].ToString();
        }
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validattion())
        {

            if (hfMasterId.Value == "")
            {
                var aDepositDao = new CompanyWiseDepositDao();
                aDepositDao.CompanyId = Convert.ToInt32(companyNameDropDownList.SelectedValue);
                aDepositDao.Amount = Convert.ToDecimal(amount.Text.Trim());
                aDepositDao.EntryBy = Session["LoginName"].ToString();
                aDepositDao.EntryDate = DateTime.Now;
                aDepositDao.DepositDate = Convert.ToDateTime(dateTextBox.Text.Trim());
                aDepositDao.IsDelete = false;
                aDepositDao.Remarks = remarksTextBox.Text;
                aDepositDao.AIT = string.IsNullOrEmpty(AITTextBox1.Text.Trim()) ? 0 :Convert.ToDecimal(AITTextBox1.Text.Trim());
                aDepositDao.ReferenceName = string.IsNullOrEmpty(txtReferenceName.Text.Trim()) ? "" : txtReferenceName.Text.Trim();
                aDepositDao.MonthName = string.IsNullOrEmpty(tbxMonthName.Text.Trim()) ? "" : tbxMonthName.Text.Trim();
                //aDepositDao.ReferenceDate = string.IsNullOrEmpty(txtReferenceDate.Text)?(DateTime?)null : DateTime.Parse(txtReferenceDate.Text).Date;
                for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                {
                    if (CheckBoxList1.Items[i].Selected)
                    {
                        aDepositDao.DepositType = CheckBoxList1.Items[i].Text.Trim();
                    }
                }
                if (aDepositDao.DepositType == "Cash" || aDepositDao.DepositType == "Online")
                {
                    aDepositDao.BankId = Convert.ToInt32(ddlBank.SelectedValue);
                    aDepositDao.AccountName = string.IsNullOrEmpty(accNameTextBox.Text.Trim()) ? "" : accNameTextBox.Text.Trim();
                    aDepositDao.AccountNameActual = string.IsNullOrEmpty(txtAccountName.Text.Trim()) ? "" : txtAccountName.Text.Trim();
                }
                else
                {
                    aDepositDao.BankId = Convert.ToInt32(ddlBank.SelectedValue);
                    aDepositDao.AccountNameActual = string.IsNullOrEmpty(txtAccountName.Text.Trim()) ? "" : txtAccountName.Text.Trim();
                    aDepositDao.AccountName = string.IsNullOrEmpty(accNameTextBox.Text.Trim()) ? "" : accNameTextBox.Text.Trim();
                    aDepositDao.BranchName = branchTextBox.Text;
                    aDepositDao.CheckNumber = chkTextBox.Text;
                    aDepositDao.CheckDate = Convert.ToDateTime(chkDateTextBox.Text.Trim());
                }

                aDepositDao.DocumentLink = string.IsNullOrEmpty(hfDocFile.Value.Trim()) ? null : hfDocFile.Value; 
                aDepositDao.FileName = hfDocFileName.Value;
                aDepositDao.DocumentNote = txtSummaryNote.Text.Trim();

                string extension;
                extension = Path.GetExtension(hfDocFile.Value);
                //jpg, png,xlsx,pdf,txt,doc,docx
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg" || extension == ".JPEG" || extension == ".JPG" || extension == ".PNG")
                {
                    aDepositDao.DocumentLinkPreview = "http://194.233.66.180:340/" + hfDocFile.Value;
                }
                else
                {
                    aDepositDao.DocumentLinkPreview = "https://docs.google.com/gview?url=http://194.233.66.180:340/" + hfDocFile.Value + "&embedded=true";
                }


                if (aDal.SaveDepositInfo(aDepositDao) > 0)
                {


                    Clear();
                    ShowMessageBox("Operation successful!");

                    //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','DepositList.aspx');", true);
                }
            }
            else
            {
                var aDepositDao = new CompanyWiseDepositDao();
                aDepositDao.DepositId = Convert.ToInt32(hfMasterId.Value);

                aDepositDao.CompanyId = Convert.ToInt32(companyNameDropDownList.SelectedValue);
                aDepositDao.Amount = Convert.ToDecimal(amount.Text.Trim());

                aDepositDao.DepositDate = Convert.ToDateTime(dateTextBox.Text.Trim());
                aDepositDao.IsDelete = false;
                aDepositDao.Remarks = remarksTextBox.Text;
                aDepositDao.AIT = string.IsNullOrEmpty(AITTextBox1.Text.Trim()) ? 0 : Convert.ToDecimal(AITTextBox1.Text.Trim());
                aDepositDao.ReferenceName = string.IsNullOrEmpty(txtReferenceName.Text.Trim()) ? "" : txtReferenceName.Text.Trim();
                aDepositDao.MonthName = string.IsNullOrEmpty(tbxMonthName.Text.Trim()) ? "" : tbxMonthName.Text.Trim();

                //aDepositDao.ReferenceDate = string.IsNullOrEmpty(txtReferenceDate.Text)?(DateTime?)null : DateTime.Parse(txtReferenceDate.Text).Date;

                for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                {
                    if (CheckBoxList1.Items[i].Selected)
                    {
                        aDepositDao.DepositType = CheckBoxList1.Items[i].Text.Trim();
                    }
                }
                if (aDepositDao.DepositType == "Cash" || aDepositDao.DepositType == "Online")
                {
                    aDepositDao.BankId = Convert.ToInt32(ddlBank.SelectedValue);
                    aDepositDao.AccountName = string.IsNullOrEmpty(accNameTextBox.Text.Trim()) ? "" : accNameTextBox.Text.Trim();
                    aDepositDao.AccountNameActual = string.IsNullOrEmpty(txtAccountName.Text.Trim()) ? "" : txtAccountName.Text.Trim();
                }
                else
                {
                    aDepositDao.BankId = Convert.ToInt32(ddlBank.SelectedValue);
                    aDepositDao.AccountNameActual = string.IsNullOrEmpty(txtAccountName.Text.Trim()) ? "" : txtAccountName.Text.Trim();
                    aDepositDao.AccountName = string.IsNullOrEmpty(accNameTextBox.Text.Trim()) ? "" : accNameTextBox.Text.Trim();
                    aDepositDao.BranchName = branchTextBox.Text;
                    aDepositDao.CheckNumber = chkTextBox.Text;
                    aDepositDao.CheckDate = Convert.ToDateTime(chkDateTextBox.Text.Trim());
                }

                if (hfDocFileName.Value != "" && txtSummaryNote.Text !="")
                {
                    string extension;
                    extension = Path.GetExtension(hfDocFile.Value);
                    //jpg, png,xlsx,pdf,txt,doc,docx
                    if (extension == ".jpg" || extension == ".png" || extension == ".jpeg" || extension == ".JPEG" || extension == ".JPG" || extension == ".PNG")
                    {
                        aDepositDao.DocumentLinkPreview = "http://45.64.134.85:340/" + hfDocFile.Value;
                    }
                    else
                    {
                        aDepositDao.DocumentLinkPreview = "https://docs.google.com/gview?url=http:http://45.64.134.85:340/" + hfDocFile.Value + "&embedded=true";
                    }

                   
                    aDepositDao.DocumentLink = string.IsNullOrEmpty(hfDocFile.Value.Trim()) ? null : hfDocFile.Value; 
                    aDepositDao.FileName = hfDocFileName.Value;
                    aDepositDao.DocumentNote = txtSummaryNote.Text.Trim();
                }
    
                aDepositDao.UpdateBy = Session["LoginName"].ToString();

                if (aDal.UpdateDepositImage(aDepositDao))
                {

                    Clear();
                    ShowMessageBox("Data Update Successfully");

                    // ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','DepositList.aspx');", true);
                }
            }

        }
    }
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("CompanyWiseBranch.aspx");

    }
    protected void viewLinkButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("DepositList.aspx");
    }
    private bool Validattion()
    {

        if (hfMasterId.Value == "")
        {

            if (companyNameDropDownList.SelectedValue == "")
            {
                ShowMessageBox("You should select sales center !!");
                return false;
            }

            int count = 0;
            string text = "";

            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    count++;
                    text = CheckBoxList1.Items[i].Text.Trim();
                }
            }

            if (count == 0)
            {
                ShowMessageBox("You should select deposit type !!!");
                return false;
            }

            if (text != "")
            {
                if (text == "Cash" || text == "Online")
                {
                    if (ddlBank.SelectedValue == "")
                    {
                        ShowMessageBox("You should select bank !!");
                        return false;
                    }

                    if (txtAccountName.Text == "")
                    {
                        ShowMessageBox("You should select account name !!!");
                        return false;
                    }

                    if (accNameTextBox.Text == "")
                    {
                        ShowMessageBox("You should select account no !!!");
                        return false;
                    }
                }
                else
                {
                    if (ddlBank.SelectedValue == "")
                    {
                        ShowMessageBox("You should select bank !!");
                        return false;
                    }

                    if (accNameTextBox.Text == "")
                    {
                        ShowMessageBox("You should select account name !!!");
                        return false;
                    }

                    if (branchTextBox.Text == "")
                    {
                        ShowMessageBox("You should select branch name !!!");
                        return false;
                    }

                    if (chkTextBox.Text == "")
                    {
                        ShowMessageBox("You should select check no !!!");
                        return false;
                    }

                    if (chkDateTextBox.Text == "")
                    {
                        ShowMessageBox("You should select check date !!!");
                        return false;
                    }
                }
            }



            if (dateTextBox.Text == "")
            {
                ShowMessageBox("You should select deposit date !!");
                return false;
            }

            if (amount.Text == "")
            {
                ShowMessageBox("You should select amount !!");
                return false;
            }



            if (hfDocFile.Value == "")
            {
                ShowMessageBox("Please click Document Upload Button");
                return false;
            }
            if (txtSummaryNote.Text == "")
            {
                ShowMessageBox("Please Enter Summary Note");
                return false;
            }

        }
        else
        {

            if (companyNameDropDownList.SelectedValue == "")
            {
                ShowMessageBox("You should select sales center !!");
                return false;
            }

            int count = 0;
            string text = "";

            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    count++;
                    text = CheckBoxList1.Items[i].Text.Trim();
                }
            }

            if (count == 0)
            {
                ShowMessageBox("You should select deposit type !!!");
                return false;
            }

            if (text != "")
            {
                if (text == "Cash" || text == "Online")
                {
                    if (ddlBank.SelectedValue == "")
                    {
                        ShowMessageBox("You should select bank !!");
                        return false;
                    }

                    if (txtAccountName.Text == "")
                    {
                        ShowMessageBox("You should select account name !!!");
                        return false;
                    }

                    if (accNameTextBox.Text == "")
                    {
                        ShowMessageBox("You should select account no !!!");
                        return false;
                    }
                }
                else
                {
                    if (ddlBank.SelectedValue == "")
                    {
                        ShowMessageBox("You should select bank !!");
                        return false;
                    }

                    if (accNameTextBox.Text == "")
                    {
                        ShowMessageBox("You should select account name !!!");
                        return false;
                    }

                    if (branchTextBox.Text == "")
                    {
                        ShowMessageBox("You should select branch name !!!");
                        return false;
                    }

                    if (chkTextBox.Text == "")
                    {
                        ShowMessageBox("You should select check no !!!");
                        return false;
                    }

                    if (chkDateTextBox.Text == "")
                    {
                        ShowMessageBox("You should select check date !!!");
                        return false;
                    }
                }
            }

            if (dateTextBox.Text == "")
            {
                ShowMessageBox("You should select deposit date !!");
                return false;
            }

            if (amount.Text == "")
            {
                ShowMessageBox("You should select amount !!");
                return false;
            }


            if (hfImage.Value == "")
            {
                if (hfDocFile.Value == "")
                {
                    ShowMessageBox("Please click Document Upload Button");
                    return false;
                }
                if (txtSummaryNote.Text == "")
                {
                    ShowMessageBox("Please Enter Summary Note");
                    return false;
                }
            }     
        }


        return true;

    }

    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void clearButton_OnClick(object sender, EventArgs e)
    {
       Clear();
    }

    private void Clear()
    {
        Loaddropdownlist();
        ddlBranch.Items.Clear();
        accNameTextBox.Text = "";
        chkTextBox.Text = "";
        chkDateTextBox.Text = "";
        branchTextBox.Text = "";
        amount.Text = "";
        dateTextBox.Text = "";
        remarksTextBox.Text = "";

        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        {
            if (CheckBoxList1.Items[i].Selected)
            {
                CheckBoxList1.Items[i].Selected = false;
            }
        }

        branch.Visible = true;
        chkNo.Visible = true;
        chkDate.Visible = true;

        
    }

    protected void ListImageButton_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DepositList.aspx");
    }
    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBoxList1.SelectedValue == "Cash" || CheckBoxList1.SelectedValue == "Online")
        {
            branch.Visible = false;
            chkNo.Visible = false;
            chkDate.Visible = false;
        }
        else
        {

            branch.Visible = true;
            chkNo.Visible = true;
            chkDate.Visible = true;
        }
    }

    //FileUpload

    protected void brnAddDoc_OnClick(object sender, EventArgs e)
    {
        if (docVali())
        {
            AddNewDocGrid_List();

        }
    }

    private bool docVali()
    {
        lblMsg.Text = "";
        if (hfDocFile.Value == "")
        {
            ShowMessageBox("Please click Document Upload Button");
            return false;
        }
        if (txtSummaryNote.Text == "")
        {
            ShowMessageBox("lease Enter Summary Note");
            lblMsg.Text = "<b>" + hfDocFileName.Value + "</b> has been uploaded.";
            return false;
        }
        return true;

    }

    private void AddNewDocGrid_List()
    {
        if (gv_DocumentUpload.Rows.Count == 0)
        {
            if (ViewState["DocGrid_List"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["DocGrid_List"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    string extension;
                    extension = Path.GetExtension(hfDocFile.Value);
                    //jpg, png,xlsx,pdf,txt,doc,docx
                    if (extension == ".jpg" || extension == ".png")
                    {
                        drCurrentRow["DocumentLinkPreview"] = "http://95.211.159.93:187/UpLoadFile/" + hfDocFile.Value;
                    }
                    else
                    {
                        drCurrentRow["DocumentLinkPreview"] = "https://docs.google.com/gview?url=http://95.211.159.93:187/UpLoadFile/" + hfDocFile.Value + "&embedded=true";
                    }
                    drCurrentRow["DocumentLink"] = "../UpLoadFile/" + hfDocFile.Value;
                    //drCurrentRow["DocumentLink"] =  @"file:///D:/UpLoadFile/"+ hfDocFile.Value;
                    drCurrentRow["FileName"] = hfDocFileName.Value;
                    drCurrentRow["DocumentNote"] = txtSummaryNote.Text.Trim();
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    //Store the current data to ViewState for future reference   
                    ViewState["DocGrid_List"] = dtCurrentTable;
                    //Rebind the Grid with the current data to reflect changes   
                    gv_DocumentUpload.DataSource = dtCurrentTable;
                    gv_DocumentUpload.DataBind();
                }
            }
            else
            {
                DataTable dt = new DataTable();
                DataRow dr = null;

                dt.Columns.Add(new DataColumn("DocumentLink", typeof(string)));
                dt.Columns.Add(new DataColumn("DocumentNote", typeof(string)));
                dt.Columns.Add(new DataColumn("FileName", typeof(string)));
                dt.Columns.Add(new DataColumn("DocumentLinkPreview", typeof(string)));
                dr = dt.NewRow();
                string extension;
                extension = Path.GetExtension(hfDocFile.Value);
                //jpg, png,xlsx,pdf,txt,doc,docx
                if (extension == ".jpg" || extension == ".png")
                {
                    dr["DocumentLinkPreview"] = "http://95.211.159.93:187/UpLoadFile/" + hfDocFile.Value;
                }
                else
                {
                    dr["DocumentLinkPreview"] = "https://docs.google.com/gview?url=http://95.211.159.93:187/UpLoadFile/" + hfDocFile.Value + "&embedded=true";
                }
                dr["DocumentLink"] = "../UpLoadFile/" + hfDocFile.Value;
                //dr["DocumentLinkPreview"] = "https://docs.google.com/gview?url=http://95.211.159.93:187/UploadMeetingDocument/" + hfDocFile.Value + "&embedded=true";
                //  dr["DocumentLink"] = @"file:///D:/UploadMeetingDocument/3eec2898121c4467be57981c13852a9e.png"; //@"file:///D:/UploadMeetingDocument/" + hfDocFile.Value;
                dr["FileName"] = hfDocFileName.Value;
                dr["DocumentNote"] = txtSummaryNote.Text.Trim();
                dt.Rows.Add(dr);
                //Store the DataTable in ViewState for future reference   
                ViewState["DocGrid_List"] = dt;
                //Bind the Gridview   
                gv_DocumentUpload.DataSource = dt;
                gv_DocumentUpload.DataBind();
            }
            //Set Previous Data on Postbacks   
            SetDocGrid_List();
            txtSummaryNote.Text = string.Empty;
            // HyperLink2.Text = "No File Uploaded";
            HyperLink100.NavigateUrl = "";
            hfDocFile.Value = "";
        }
        else
        {
            ShowMessageBox("Already Uploaded!");
        }

    }

    private void SetDocGrid_List()
    {
        int rowIndex = 0;
        if (ViewState["DocGrid_List"] != null)
        {
            DataTable dt = (DataTable)ViewState["DocGrid_List"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField hfDocumentLink = (HiddenField)gv_DocumentUpload.Rows[rowIndex].FindControl("hfDocumentLink");
                    HiddenField hfFileName = (HiddenField)gv_DocumentUpload.Rows[rowIndex].FindControl("hfFileName");
                    HiddenField hfDocumentLinkPreview = (HiddenField)gv_DocumentUpload.Rows[rowIndex].FindControl("hfDocumentLinkPreview");
                    HyperLink HLDocumentLink = (HyperLink)gv_DocumentUpload.Rows[rowIndex].FindControl("HLDocumentLink");
                    Label lbl_DocumentLink = (Label)gv_DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentLink");

                    Label lbl_DocumentNote = (Label)gv_DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentNote");


                    if (i < dt.Rows.Count - 1)
                    {
                        hfDocumentLink.Value = dt.Rows[i]["DocumentLink"].ToString();
                        hfFileName.Value = dt.Rows[i]["FileName"].ToString();
                        hfDocumentLinkPreview.Value = dt.Rows[i]["DocumentLinkPreview"].ToString();
                        lbl_DocumentLink.Text = dt.Rows[i]["DocumentLink"].ToString();
                        HLDocumentLink.NavigateUrl = dt.Rows[i]["DocumentLink"].ToString();

                        lbl_DocumentNote.Text = dt.Rows[i]["DocumentNote"].ToString();

                    }

                    rowIndex++;
                }
            }
        }
    }

    protected void btnDocRemove_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["DocGrid_List"] != null)
        {
            DataTable dt = (DataTable)ViewState["DocGrid_List"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["DocGrid_List"] = dt;
                //Re bind the GridView for the updated data  
                gv_DocumentUpload.DataSource = dt;
                gv_DocumentUpload.DataBind();
            }
            else
            {
                ViewState["DocGrid_List"] = null;
                //Re bind the GridView for the updated data  
                gv_DocumentUpload.DataSource = null;
                gv_DocumentUpload.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
        SetDocGrid_List();
    }

    protected void btnDocUp_OnClick(object sender, EventArgs e)
    {
        if (FUDocument.HasFile)
        {
            // Get the file extension of the uploaded file
            string fileExtension = Path.GetExtension(FUDocument.FileName);

            // Generate a unique file name
            string uniqueFileName = "LCOpenDoc_" + Guid.NewGuid().ToString() + fileExtension;

            // Define the directory path where the file will be saved
            string dDrivePath = @"D:\UBL_DepositSlip_Image\";

            // Ensure the directory exists; if not, create it
            if (!Directory.Exists(dDrivePath))
            {
                Directory.CreateDirectory(dDrivePath);
            }

            // Combine the directory path and unique file name
            string fullFilePath = Path.Combine(dDrivePath, uniqueFileName);

            try
            {
                // Save the uploaded file to the specified path
                FUDocument.SaveAs(fullFilePath);

                // Set the hyperlink URL to the uploaded file path
                HyperLink100.NavigateUrl = fullFilePath;
                // Uncomment this if you want to display a success message
                // HyperLink2.Text = "Uploaded Successfully";
            }
            catch (Exception ex)
            {
                // Log the error or display a user-friendly message
                HyperLink100.NavigateUrl = "";
                // HyperLink2.Text = "Error while uploading the file: " + ex.Message;
            }
        }
        else
        {
            // Handle the case when no file is uploaded
            HyperLink100.NavigateUrl = "";
            // Uncomment this if you want to display a message
            // HyperLink2.Text = "No File Uploaded";
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
}