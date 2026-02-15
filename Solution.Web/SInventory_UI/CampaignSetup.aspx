<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="CampaignSetup.aspx.cs" Inherits="MasterSetup_UI_QuotedPriceSetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/MasterSetup_UI/IVMarketStructure.ascx" TagPrefix="uc1" TagName="IVMarketStructure" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .autocomplete_completionListElement {
            margin: 0px !important;
            background-color: White !important;
            color: windowtext !important;
            border: buttonshadow !important;
            border-width: 1px !important;
            border-style: solid !important;
            cursor: 'default' !important;
            overflow: auto !important;
            font-family: Calibri !important;
            font-size: 12px !important;
            text-align: left !important;
            list-style-type: none !important;
            margin-left: 0px !important;
            padding-left: 0px !important;
            max-height: 200px !important;
            width: 300px !important;
            overflow: auto !important;
            box-shadow: 0 0 3px 1px rgba(0,0,0,.35) !important;
        }


        .autocomplete_completionListElement222 {
            margin: 0px !important;
            background-color: White !important;
            color: windowtext !important;
            border: buttonshadow !important;
            border-width: 1px !important;
            border-style: solid !important;
            cursor: 'default' !important;
            overflow: auto !important;
            font-family: Calibri !important;
            font-size: 12px !important;
            text-align: left !important;
            list-style-type: none !important;
            margin-left: 0px !important;
            padding-left: 0px !important;
            max-height: 200px !important;
            width: 600px !important;
            overflow: auto !important;
            box-shadow: 0 0 3px 1px rgba(0,0,0,.35) !important;
        }
        /* AutoComplete highlighted item */

        .autocomplete_highlightedListItem {
            background-color: #17A2B8 !important;
            color: white !important;
            padding: 6px !important;
            font-weight: bold !important;
        }

        /* AutoComplete item */

        .autocomplete_listItem {
            padding: 6px !important;
            cursor: pointer !important;
            font-weight: bold !important;
            background-color: #fff !important;
            border-bottom: 1px solid #d4d4d4 !important;
            box-shadow: 0 1px 1px rgba(0, 0, 0, 0.075) inset !important;
        }
    </style>
    <div id="popDiv">
    </div>
    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i> Campaign Setup </div>

                <div class="ms-auto">
                    <div class="btn-group">


                        <a href="../SInventory_UI/SpecialDiscountView.aspx" class="btn btn-sm btn-sm btn-outline-info"><i class="fa fa-backward"></i> &nbsp; Back to List </a>


                    </div>
                </div>
            </div>

            <!--end breadcrumb-->

            <div class="row">
                <div class="col">

                    <div class="card border-top border-0 border-4 border-success">
                        <div class="card-body">

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                        <ProgressTemplate>

                                            <div class="divWaiting">
                                                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="../images/Spinner.gif" Width="180px" Height="180px" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <asp:HiddenField runat="server" ID="id_mastetID" />


                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">
                                            <div class="form-group row">
                                                <label for="txtNID" class="col-sm-3 col-form-label"> Customer Name: </label>

                                                <div class="col-sm-5">
                                                    <div class="input-group">
                                                        
                                                        
                                                        <asp:TextBox ID="custCodeTextBox" runat="server" CssClass="form-control form-control-sm"
                                                            AutoPostBack="True" OnTextChanged="custCodeTextBox_TextChanged"></asp:TextBox>


                                                        <asp:AutoCompleteExtender
                                                            ID="at_txt_JobCirculation"
                                                            TargetControlID="custCodeTextBox"
                                                            runat="server"
                                                            ServiceMethod="GetCustomerForOrder"
                                                            ServicePath="SInventoryWebService.asmx"
                                                            MinimumPrefixLength="1"
                                                            CompletionInterval="10"
                                                            EnableCaching="false"
                                                            CompletionSetCount="1"
                                                            FirstRowSelected="false" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                                        </asp:AutoCompleteExtender>



                                                        <asp:HiddenField ID="hfCustomerId" runat="server" />
                                                        <asp:HiddenField ID="hfSpecialCampaignId" runat="server" />
                                                        <asp:DropDownList ID="ddlCustomer" runat="server" Visible="False" CssClass="form-select form-select-sm  mySelect2"></asp:DropDownList>
                                                        <span class="input-group-text text-c-red">*</span>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">
                                            <div class="form-group row">
                                                <label for="txtNID" class="col-sm-3 col-form-label">Slab Amount:</label>

                                                <div class="col-sm-5">
                                                    <div class="input-group">
                                                        <asp:TextBox class="form-control form-control-sm  clsDecimal" runat="server" placeholder="Amount" ID="tbxSlabAmount" ></asp:TextBox>
                                                    <span class="input-group-text text-c-red">*</span>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">
                                            <div class="form-group row">
                                                <label for="txtNID" class="col-sm-3 col-form-label"> Discount  Percent (%):</label>

                                                <div class="col-sm-5">
                                                    <div class="input-group">
                                                         <asp:TextBox class="form-control form-control-sm  clsDecimal"  runat="server" ID="tbxDiscountPercent" placeholder="Percent (%)"></asp:TextBox>
                                                    <span class="input-group-text text-c-red">*</span>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <hr />
                                    <span style="font-size: 1.2em !important;"> <i class="fa fa-arrow-right"></i> &nbsp;<b>Product List</b> </span>
                                    <hr />
                                    
                                    <div class="row">

                                        <div class="col-12">

                                            <div class="table-responsive" id="MainGradeDiv">

                                                <asp:GridView ID="gv_ProductList" runat="server" AutoGenerateColumns="False"  DataKeyNames="ProductCode"
                                                    CssClass="table table-bordered  text-center thead-dark">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                                <asp:HiddenField runat="server" ID="hfProductId" Value='<%#Eval("ProductId")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                   <asp:CheckBox 
    ID="chkSelectAll" 
    runat="server" 
    CssClass="form-control-sm" 
    AutoPostBack="True" 
    OnCheckedChanged="chkSelectAll_CheckedChanged"
   />


  
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" CssClass="form-control-sm" runat="server"  Checked='<%# Eval("IsAutoSelect") != null && Eval("IsAutoSelect").ToString() == "Active" %>'  />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Product Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ProductCode" runat="server" Text='<%#Eval("ProductCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Product Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ProductName" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                       

                                                    </Columns>
                                                </asp:GridView>


                                            </div>

                                        </div>
                                    </div>



                                    <br />
                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">

                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-3 col-form-label"></label>
                                                <div class="col-sm-8">




                                                    <asp:LinkButton OnClick="btnSave_Click" Visible="false" OnClientClick="return sweetAlertConfirm_Submit(this);" runat="server" ID="btnSave" class="btn btnMyDesignSearch   btn-sm">
                                            <i class="fa fa-check"></i>Submit
                                                    </asp:LinkButton>

                                                    <asp:LinkButton OnClick="btnSave_Click" Visible="false" runat="server" ID="btnUpdate" class="btn btnMyDesignSearch   btn-sm" OnClientClick="return sweetAlertConfirm_Update(this);">
                                            <i class="fa fa-check"></i>Update
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="restbtn" OnClick="restbtn_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">&nbsp;</div>
                                    </div>


                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

