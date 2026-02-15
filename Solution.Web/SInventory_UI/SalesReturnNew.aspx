<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="SalesReturnNew.aspx.cs" Inherits="SInventory_UI_SalesReturnNew" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.28364, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        /*AutoComplete flyout */
        .autocomplete_completionListElement {
            margin: 0px !important;
            background-color: White;
            color: windowtext;
            border: buttonshadow;
            border-width: 1px;
            border-style: solid;
            cursor: default;
            overflow: auto;
            font-family: Calibri;
            font-size: 12px;
            text-align: left;
            list-style-type: none;
            margin-left: 0px;
            padding-left: 0px;
            max-height: 350px;
            width: 40% !important;
        }

        /* AutoComplete highlighted item */

        .autocomplete_highlightedListItem {
            background-color: yellow;
            color: black;
            padding: 1px;
        }

        /* AutoComplete item */

        .autocomplete_listItem {
            background-color: white;
            color: blue;
            padding: 0px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="page-wrapper">
                <div class="page-content">
                    <!--breadcrumb-->
                    <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                        <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Sales Return </div>

                        <div class="ms-auto">
                            <div class="btn-group">


                                <<asp:LinkButton ID="detailsViewButton" CssClass="btn btn-sm btn-sm btn-outline-info" runat="server" OnClick="detailsViewButton_Click"> <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <!--end breadcrumb-->
                    <div class="row">
                        <div class="col">

                            <div class="card border-top border-0 border-4 border-success">


                                <script type="text/javascript">
                                    function pageLoad() {
                                        $('.datepicker').pickadate({
                                            selectMonths: true,
                                            selectYears: true
                                        })
                                        $('.mySelect2').select2({
                                            theme: 'bootstrap4',
                                            width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
                                            placeholder: $(this).data('placeholder'),
                                            allowClear: Boolean($(this).data('allow-clear')),
                                        });
                                    }
                                </script>


                                <div class="card-body">

                                    <div class="row">

                                        <div class="col-md-6">
                                            
                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-4 col-form-label"> Sales Center: <span style="color: red !important">*</span> </label>

                                                <div class="col-sm-7">

                                                    <asp:DropDownList ID="ddlSalesCenter" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2"> </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-4 col-form-label">Customer Code: <span style="color: red !important">*</span> </label>

                                                <div class="col-sm-7">

                                                    <asp:TextBox ID="custCodeTextBox" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm "
                                                        OnTextChanged="custCodeTextBox_TextChanged"></asp:TextBox>

                                                    <asp:HiddenField ID="hdCustomerMasterId" runat="server" />
                                                    <asp:HiddenField ID="orderIdHiddenField" runat="server" />
                                                    <asp:HiddenField ID="orderHiddenField" runat="server" />
                                                    <asp:HiddenField ID="hdComUnitId" runat="server" />
                                                    <asp:HiddenField ID="hdMiaId" runat="server" />
                                                    <asp:HiddenField ID="masterHiddenFieldId" runat="server" />

                                                </div>
                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-4 col-form-label">Customer Name: <span style="color: red !important">*</span> </label>

                                                <div class="col-sm-7">

                                                    <asp:TextBox ID="custNameTextBox" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True"
                                                        OnTextChanged="custNameTextBox_TextChanged"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                        DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1"
                                                        CompletionSetCount="10" ServiceMethod="GetCustomer" ServicePath="SInventoryWebService.asmx"
                                                        TargetControlID="custNameTextBox" UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </asp:AutoCompleteExtender>

                                                </div>
                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-4 col-form-label">Customer Address: <span style="color: red !important">*</span> </label>

                                                <div class="col-sm-7">

                                                    <asp:TextBox ID="custAddressTextBox" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>

                                                </div>
                                            </div>

                                        </div>

                                        <div class="col-md-6">


                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-5 col-form-label"> Return Reason: <span style="color: red !important">*</span> </label>

                                                <div class="col-sm-7">

                                                    <asp:DropDownList ID="ddlReturnReason" runat="server" CssClass="form-control form-control-sm mySelect2">

                                                        <asp:ListItem Value=""> Select from list </asp:ListItem>
                                                        <asp:ListItem Value="False Order"> False Order </asp:ListItem>
                                                        <asp:ListItem Value="Cash Short"> Cash Short </asp:ListItem>
                                                        <asp:ListItem Value="Customer unavailable"> Customer unavailable </asp:ListItem>
                                                        <asp:ListItem Value="Delivery delay"> Delivery delay </asp:ListItem>

                                                    </asp:DropDownList>

                                                </div>
                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-5 col-form-label"> Return Date: <span style="color: red !important">*</span> </label>

                                                <div class="col-sm-7">

                                                    <asp:TextBox ID="orderDateTextBox" runat="server" CssClass="form-control form-control-sm  datepicker" ReadOnly="True"></asp:TextBox>

                                                </div>
                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-5 col-form-label">Reference Invoice No: (If any) </label>

                                                <div class="col-sm-7">

                                                    <asp:TextBox ID="ddlInvoice" runat="server" CssClass="form-control form-control-sm"
                                                        AutoPostBack="True" ToolTip="true" OnTextChanged="referenceInvoiceTextBox_TextChanged" Text='<%# Eval("InvoiceNo")%>'></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="productCodeTextBox1_AutoCompleteExtender" runat="server"
                                                        DelimiterCharacters="" EnableCaching="true"
                                                        Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="GetAllInvoice" ServicePath="SInventoryWebService.asmx" TargetControlID="ddlInvoice"
                                                        UseContextKey="True"
                                                        CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:HiddenField ID="hdfInvoiceId" runat="server" />

                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                   
                                   <br />
                                    <h5> <i class="bx bxs-hand-right"></i> Product Details </h5>
                                    <hr />

                                    <div class="row">
                                        <div class="table-responsive" id="MainGradeDivCustomer">
                                            <asp:GridView ID="productGridView" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" CssClass="table table-bordered  text-center thead-dark" ShowFooter="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Code">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="productCodeTextBox" runat="server" CssClass="form-control form-control-sm mb-3" AutoPostBack="True"
                                                                ToolTip="true" OnTextChanged="productCodeTextBox_TextChanged" Text='<%# Eval("ProductCode")%>'></asp:TextBox>
                                                            <asp:HiddenField ID="unitpriceHiddenField" Value='<%# Eval("TradePrice")%>' runat="server" />
                                                            <asp:HiddenField ID="productidHiddenField" Value='<%# Eval("ProductId")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Name" ItemStyle-Width="40px" HeaderStyle-Width="300px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="productNameTextBox" runat="server" CssClass="form-control form-control-sm mb-3" Text='<%# Eval("ProductName")%>'
                                                                AutoPostBack="True" ToolTip="true" OnTextChanged="productNameTextBox_TextChanged"></asp:TextBox>

                                                            <asp:AutoCompleteExtender
                                                                ID="at_txt_JobCirculassstion"
                                                                TargetControlID="productNameTextBox"
                                                                runat="server"
                                                                ServiceMethod="GetProductList"
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

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="TP" >
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="tpTextBox" Width="70px" ReadOnly="True" runat="server" CssClass="form-control form-control-sm mb-3" Text='<%# Eval("TradePrice")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="VAT">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="vatTextBox" Width="70px" ReadOnly="True" runat="server" CssClass="form-control form-control-sm mb-3" Text='<%# Eval("UnitVatAmount")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="reqQtyTextBox" Width="70px" runat="server" AutoPostBack="True" OnTextChanged="reqQtyTextBox_OnTextChanged" CssClass="form-control form-control-sm mb-3" Text='<%# Eval("Quantity")%>'></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderconvRate" runat="server"
                                                Enabled="True" TargetControlID="reqQtyTextBox" FilterType="Custom" ValidChars="0123456789">
                                            </asp:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total TP">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TotaltpTextBox" Width="80px" ReadOnly="True" runat="server" CssClass="form-control form-control-sm mb-3" Text='<%# Eval("TotalTradePrice")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Total Vat">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TotaltpVatTextBox" Width="80px" ReadOnly="True" runat="server" CssClass="form-control form-control-sm mb-3" Text='<%# Eval("TotalVatAmount")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Gross Value">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="tblGrossValue" Width="80px" ReadOnly="True" runat="server" CssClass="form-control form-control-sm mb-3" Text='<%# Eval("NetAmount")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="FOC">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkIsGiftProduct" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    
                                                    <asp:TemplateField HeaderText="Expairy Date">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="expDateTextBox" runat="server" Text='<%# Eval("ExpireDate")%>' CssClass="form-control form-control-sm  datepicker"></asp:TextBox>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Batch No">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="batchNoTextBox" runat="server" Text='<%# Eval("BatchNo")%>' CssClass="form-control form-control-sm "></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Add">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/lineAdd.png"
                                                                OnClick="ImageButton1_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remove">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/lineDelete.png"
                                                                OnClick="ImageButton2_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <br />

                                    

                                    <br />

                                    <div class="row">


                                        <div class="col-md-3"></div>

                                        <div class="col-md-6">

                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label">TP Total :</label>

                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="tpTptalTextBox" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:TextBox>
                                                </div>

                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>

                                            <%--<div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label">Discount Total :</label>

                                                <div class="col-sm-5">

                                                    <asp:TextBox ID="disTotalTextBox" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True"
                                                        OnTextChanged="custNameTextBox_TextChanged"></asp:TextBox>


                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>

                                            <div class="form-group row">
                                                <label for="" class="col-sm-3 col-form-label">Special Discount :</label>

                                                <div class="col-sm-5">


                                                    <asp:TextBox ID="pdTextBox" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>

                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>--%>


                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label"> VAT Total: </label>

                                                <div class="col-sm-5">

                                                    <asp:TextBox ID="vatTotalTextBox" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>

                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>

                                            <div class="form-group row">
                                                <label for="" class="col-sm-3 col-form-label"> Grand Total :</label>

                                                <div class="col-sm-5">

                                                    <asp:TextBox ID="grandTotalTextBox" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>



                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
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

                                                    <asp:LinkButton ID="LinkButton3" CssClass="btn btn-sm btn-primary mb-2" runat="server" OnClick="submitButton_Click" Style="background-color: #00bcd4; color: #fff;"> <i class="fa fa-check-square"></i>&nbsp; Submit Information</asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton4" class="btn btn-sm btn-warning  mb-2" Style="background-color: orangered; color: #fff;" runat="server" OnClick="cancelButton_Click"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset Information </asp:LinkButton>

                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">&nbsp;</div>
                                    </div>

                                    <br />

                                    <div class="row">


                                        <div class="col-md-3"></div>

                                        <div class="col-md-5">

                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-4 col-form-label">Print Invoice No:</label>

                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="invTextBox" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                </div>

                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>


                                        </div>

                                        <div class="col-md-2">

                                            <asp:Button ID="Button1" runat="server" OnClick="printButton_Click" Text="Print" />
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
    </asp:UpdatePanel>


</asp:Content>

