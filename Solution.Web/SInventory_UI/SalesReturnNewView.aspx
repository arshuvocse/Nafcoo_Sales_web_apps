<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="SalesReturnNewView.aspx.cs" Inherits="SInventory_UI_SalesReturnNewView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Invoice Creation</div>

                <div class="ms-auto">
                    <div class="btn-group">
                        <asp:LinkButton ID="EmpCetegoryAddImageButton" CssClass="btn btn-sm btn-outline-info " runat="server" OnClick="EmpCetegoryAddImageButton_Click"><i class="fa fa-plus" aria-hidden="true"></i> New Entry </asp:LinkButton>
                        

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
                                        <div class="col-4">
                                            
                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-4 col-form-label">Depot Name: </label>

                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlDepot" runat="server" CssClass="form-select form-select-sm mySelect2"> </asp:DropDownList>
                                                </div>

                                            </div>
                                            
                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-4 col-form-label">From Date:</label>

                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="fromDateTextBox" runat="server" class="form-control form-control-sm datepicker" autocomplete="off" placeholder="Select from date"></asp:TextBox>

                                                </div>

                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-4 col-form-label">To Date:</label>

                                                <div class="col-sm-8">

                                                    <asp:TextBox ID="todateTextBox" runat="server" class="form-control form-control-sm datepicker" autocomplete="off" placeholder="Select to date"></asp:TextBox>
                                                </div>

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

                                                    <asp:LinkButton OnClick="submitButton_Click" runat="server" ID="submitButton" class="btn btnMyDesignSearch   btn-sm"><i class="fa fa-search"></i> Search</asp:LinkButton>
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
                                                CssClass="table table-bordered  text-center thead-dark" OnRowCommand="loadGridView_RowCommand" OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="ReturnInvoiceId,ReturnInvoiceNo">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ReturnInvoiceNo" HeaderText="Return Invoice No" />
                                                    <asp:BoundField DataField="ReturnInvoiceDate" HeaderText="Return Invoice Date" />
                                                    <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />
                                                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                                    <asp:BoundField DataField="InvoiceNo" HeaderText="Ref. Invoice No" />
                                                    <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" />
                                                    <asp:BoundField DataField="TpGrandTotal" HeaderText="Total Value" />
                                                    <asp:BoundField DataField="EntryBy" HeaderText="Entry By" />
                                                    <asp:BoundField DataField="CreateDate" HeaderText="Entry Date" />
                                                    <asp:BoundField DataField="Remarks" HeaderText="Return Remarks" />
                                                    
                                                    <asp:TemplateField HeaderText="Reports">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="topSheetButton" CssClass="btn btn-sm btn-info mb-2" runat="server" OnClick="topSheetButton_Click" ><i class="fa fa-print"></i> Invoice Print </asp:LinkButton> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField Visible="False">
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
                                                    </asp:TemplateField>--%>



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
    
    
    

</asp:Content>

