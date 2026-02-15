<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="CustomerUpload.aspx.cs" Inherits="SInventory_UI_CustomerUpload" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.28364, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
        /*AutoComplete flyout */
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
            font-size: 14px !important;
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
            font-size: 14px !important;
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


    <style>
        .checkboxlist_nowrap {
            display: inline;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
        <ContentTemplate>
            <div class="page-wrapper">
                <div class="page-content">
                    <!--breadcrumb-->
                    <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                        <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i> Customer Upload </div>

                        <div class="ms-auto">
                            <div class="btn-group">

                                <asp:LinkButton ID="viewLinkButton" class="btn btn-sm btn-sm btn-outline-info"
                                    OnClick="viewLinkButton_OnClick" runat="server"> <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>


                            </div>
                        </div>
                    </div>
                    <!--end breadcrumb-->
                    <div class="row">
                        <div class="col">

                            <div class="card border-top border-0 border-4 border-success">
                                <div class="card-body">


                                    <div class="card-body">

                                        <div class="row">&nbsp;</div>
                                        <div class="row">&nbsp;</div>
                                        <div class="row">
                                            <div class="col-2">&nbsp;</div>
                                            <div class="col-8">


                                            </div>
                                        </div>
                                        <br />
                                          <div class="row">
                                <div class="col-md-2"><a href="../ExcelFiles/UniworldCustomerUpload.xls"  class="btn  btn-secondary   btn-sm">Download Excel Format</a></div>

                                <div class="col-md-10">
                                    <div class="form-group row">

                                        <label for="mainName" class="col-sm-2 col-form-label">Upload File :</label>

                                        <div class="col-sm-7">

                                            <asp:FileUpload ID="id_fu" runat="server" ToolTip="Select File To Upload." class="form-control form-control-sm" />

                                            <asp:HiddenField ID="IsFileUploaded" runat="server" />
                                            <br />
                                            <asp:Label ID="lbl_up_status" runat="server" CssClass=""></asp:Label>
                                        </div>

                                        <div class="col-sm-3">
                                            <asp:Button ID="btnUpload" runat="server" class="btn btnMyDesignAddtoList   btn-sm" Text="Upload" OnClick="btnUpload_OnClick" />

                                            <asp:HiddenField ID="mainid" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <br />
                                        <br />
                                        <div class="row">
                                            <div class="table-responsive" id="MainGradeDiv">
                                                <asp:GridView ID="productGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered  text-center thead-dark" OnPreRender="gv_DocumentUpload_PreRender">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                             
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Market Code">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="MarketCodeTextBox" runat="server"  CssClass="form-control form-control-sm "
                                                                  
                                                                    Text='<%# Eval("MarketCode")%>'></asp:TextBox>

                                                 
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer Code">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="CustomerCodeTextBox" runat="server" CssClass="form-control form-control-sm "
                                                                    Text='<%# Eval("CustomerCode")%>'  ></asp:TextBox>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Customer Name">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="CustomerNameTextBox" CssClass="form-control form-control-sm " runat="server"
                                                                     Text='<%# Eval("CustomerName")%>'></asp:TextBox>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Address">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="AddressTextBox" CssClass="form-control form-control-sm " runat="server"
                                                                     Text='<%# Eval("Address")%>'></asp:TextBox>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Owner Name">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="OwnerNameTextBox" CssClass="form-control form-control-sm " runat="server"
                                                                    Text='<%# Eval("OwnerName")%>'></asp:TextBox>
                                                   
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="CellNo">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="CellNoTextBox" runat="server" Text='<%# Eval("CellNo")%>'
                                                                    CssClass="form-control form-control-sm " ></asp:TextBox>
                                          
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Term of Payment">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TermofPaymentTextBox" runat="server" Text='<%# Eval("TermOfPayment")%>'
                                                                             CssClass="form-control form-control-sm datepicker" ></asp:TextBox>
                                          
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                              <%--          <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/lineAdd.png"
                                                                    OnClick="ImageButton1_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/lineDelete.png"
                                                                    OnClick="ImageButton2_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        

                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                        </div>


                                        


                                        <br />
                                        <div class="row">
                                            <div class="col-2">&nbsp;</div>
                                            <div class="col-8">

                                                <div class="form-group row">
                                                    <label for="exampleInputUsername2" class="col-sm-3 col-form-label"></label>
                                                    <div class="col-sm-8">

                                                        <asp:LinkButton ID="submitButton" CssClass="btn btn-sm btn-primary mb-2" runat="server" OnClick="submitButton_Click" Style="background-color: #00bcd4; color: #fff;"
                                                            OnClientClick="return confirm('Are you sure you want to Save ?');"> <i class="fa fa-check-square"></i>&nbsp; Submit Information</asp:LinkButton>
                                                        <asp:LinkButton ID="cancelButton" class="btn btn-sm btn-warning  mb-2" Style="background-color: orangered; color: #fff;" runat="server" OnClick="cancelButton_Click"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset Information </asp:LinkButton>

                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-2">&nbsp;</div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



            </div>
        </ContentTemplate>
         <Triggers>

                                    <asp:PostBackTrigger ControlID="btnUpload" />
                                </Triggers>

</asp:Content>

