<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="InvoiceCreationByOrder.aspx.cs" Inherits="SInventory_UI_InvoiceCreationByOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        #flex-container {
            display: flex !important;
            flex-direction: row !important;
            width: 100% !important;
        }

            #flex-container > .flex-item {
                flex: auto !important;
            }

            #flex-container > .raw-item {
                width: 8rem !important;
            }
    </style>


    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Invoice Creation</div>

                <div class="ms-auto">
                    <div class="btn-group">
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

                                        var dateNow = new Date();
                                        $('.datepickess').datepicker("setDate", dateNow);
                                        minDate: new Date() // to disable privious dates 
                                    </script>





                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">
                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label">Sales Center:</label>

                                                <div class="col-sm-5">


                                                    <asp:DropDownList ID="salesCenterDropDownList" runat="server"
                                                        CssClass="form-control form-control-sm mySelect2" AutoPostBack="True"
                                                        OnSelectedIndexChanged="salesCenterDropDownList_SelectedIndexChanged">
                                                    </asp:DropDownList>


                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>

                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label">Route:</label>

                                                <div class="col-sm-5">


                                                    <asp:DropDownList ID="rootDropDownList" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2   "
                                                       >
                                                    </asp:DropDownList>


                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>

                                            <div id="Div1" class="form-group row" runat="server" visible="False">
                                                <label for="mainName" class="col-sm-3 col-form-label">Manufacture:</label>

                                                <div class="col-sm-5">


                                                    <asp:DropDownList ID="manufacDropDownList" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2 "
                                                        AutoPostBack="True"
                                                        OnSelectedIndexChanged="manufacDropDownList_SelectedIndexChanged">
                                                    </asp:DropDownList>


                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>


                                            <div id="Div2" class="form-group row" runat="server" visible="False">
                                                <label for="mainName" class="col-sm-3 col-form-label">Market:</label>

                                                <div class="col-sm-5">


                                                    <asp:DropDownList ID="marketDropDownList" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2  "
                                                        AutoPostBack="True"
                                                        OnSelectedIndexChanged="marketDropDownList_SelectedIndexChanged">
                                                    </asp:DropDownList>


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

                                                    <asp:LinkButton OnClick="Button1_Click" runat="server" ID="submitButton" class="btn btnMyDesignSearch   btn-sm">
                                            <i class="fa fa-search"></i> Search
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>


                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">&nbsp;</div>
                                    </div>

                                    <br />

                                    <div class="row" runat="server" Visible="False">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-3">&nbsp;</div>

                                        <div class="col-3">



                                            <asp:LinkButton ID="reportButton" class="btn btn-sm   mb-2  pull-right" Style="background-color: #1A7343; color: #fff;" runat="server" OnClick="viewRptButton_Click"><i class="fa fa-print" aria-hidden="true"></i>&nbsp; Print Report </asp:LinkButton>
                                        </div>
                                        <div class="col-2">

                                            <div class="form-group row">

                                                <asp:TextBox runat="server" ID="batchno" placeholder=" Batch NO" CssClass="form-control form-control-sm mb-3"></asp:TextBox>

                                            </div>
                                            <div class="form-group row">
                                                <asp:LinkButton ID="invoiceButton" runat="server" OnClick="invoiceButton_Click" CssClass="btn btn-sm btn-success mb-2 pull-right"><i class="fa fa-check" aria-hidden="true"></i>&nbsp;Generate Invoice</asp:LinkButton>
                                            </div>


                                        </div>



                                    </div>
                                    <div class="row">
                                        <div id="flex-container">
                                            <div class="flex-item" id="flex">&nbsp;</div>
                                            <div class="raw-item" id="raw">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="table-responsive" id="MainGradeDiv">

                                            <asp:GridView ID="orderGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                CssClass="table table-bordered  text-center thead-dark" OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="ComUnitId,ManufacId,OrderId,IsInvoiceAble">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="OrderCode" HeaderText="Order No" />
                                                    

                                                    <asp:BoundField DataField="SubmissionDate" HeaderText="Order Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    
                                                    <asp:BoundField DataField="EntryDate" HeaderText="Order time" DataFormatString="{0:hh:mm:ss tt}" />
                                                    
                                                   
                                                    <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />
                                                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                                    <asp:BoundField DataField="MarketName" HeaderText="Market" />
                                                    <asp:BoundField DataField="GrossValue" HeaderText="Gross Value(TP)" />
                                                    <asp:BoundField DataField="CustomerType" HeaderText="Customer Type" />
                                                    <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                                    <asp:TemplateField Visible="False">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" AutoPostBack="True" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Go To Invoice">
                                                        <ItemTemplate>
                                                            <asp:Button ID="gotoinvoiceButton" runat="server" Text="Go To Invoice >>" CssClass="btn btn-sm  btn-info"
                                                                OnClick="gotoinvoiceButton_Click" />
                                                            <asp:HiddenField runat="server" ID="hfCustomerCode" Value='<%#Eval("CustomerCode")%>' />
                                                            <asp:HiddenField runat="server" ID="hfCustomerMasterId" Value='<%#Eval("CustomerMasterId")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <%--  <asp:TemplateField HeaderText="Generate Invoice">
                                        <ItemTemplate>
                                            <asp:Button ID="GenerateinvoiceButton" runat="server" Text="Generate"  CssClass="button"
                                                onclick="GeneratetoinvoiceButton_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>


                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="False">
        <ContentTemplate>
            <div>
                <table width="100%" class="TableWorkArea">
                    <tr>
                        <td colspan="6" class="TableHeading">Proforma Invoice Creation
                        </td>
                    </tr>
                    <tr>
                        <td width="13%" class="TDLeft">&nbsp;
                        </td>
                        <td width="20%" class="TDRight"></td>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                    </tr>
                    <tr>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight">Sales Center</td>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                    </tr>
                    <tr>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight">Manufacture</td>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                    </tr>
                    <tr>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight">Market</td>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                    </tr>
                    <tr>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                        <td width="13%" class="TDLeft">
                            <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                        </td>
                        <td width="20%" class="TDRight"></td>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                    </tr>
                    <tr>
                        <td width="13%" class="TDLeft">&nbsp;
                        </td>
                        <td width="20%" class="TDRight"></td>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                    </tr>
                    <tr>
                        <td class="TDLeft" colspan="6"></td>
                    </tr>
                    <tr>
                        <td width="13%" class="TDLeft">&nbsp;
                        </td>
                        <td width="20%" class="TDRight">&nbsp;
                        </td>
                        <td width="13%" class="TDLeft">&nbsp;
                        </td>
                        <td width="20%" class="TDRight">&nbsp;
                        </td>
                        <td width="13%" class="TDLeft">&nbsp;
                        </td>
                        <td width="20%" class="TDRight">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="13%" class="TDLeft">&nbsp;
                        </td>
                        <td width="20%" class="TDRight">&nbsp;
                        </td>
                        <td width="13%" class="TDLeft">&nbsp;
                        </td>
                        <td width="20%" class="TDRight">&nbsp;
                        </td>
                        <td width="13%" class="TDLeft">&nbsp;
                        </td>
                        <td width="20%" class="TDRight">&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

