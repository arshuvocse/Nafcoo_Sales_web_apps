<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="DepotStockAdjustmentsVoucherView.aspx.cs" Inherits="SInventory_UI_DirectStockOutView" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.28364, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="modal fade" id="MDModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog  modal-lg" style="width: 90% !important;" role="document">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="modal-dialog modal-xl">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Update Partial Product</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">

                                <asp:GridView ID="DerectStoctOutGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table  blueTable" OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="DCStoreId,PackSize">
                                    <Columns>


                                        <asp:TemplateField HeaderText="Product Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_PCode" runat="server" Text='<%#Eval("PCode") %>'></asp:Label>

                                                <asp:HiddenField runat="server" ID="hfDcStockOutDetailsId" Value='<%#Eval("DcStockOutDetailsId")%>' />

                                                <asp:HiddenField runat="server" ID="hfDcStoreId" Value='<%#Eval("DcStoreId")%>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Product Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_PName" runat="server" Text='<%#Eval("PName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Stock Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_StockQty" runat="server" Text='<%#Eval("StockQty") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Batch No">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_BatchNo" runat="server" Text='<%#Eval("BatchNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Exp. Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ExpDate" runat="server" Text='<%#Eval("ExpDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Receive Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ReceiveDate" runat="server" Text='<%#Eval("ReceiveDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Stoct Out Qty">
                                            <ItemTemplate>
                                                <asp:TextBox Text='<%#Eval("StockQty") %>' ID="transferQtyTextBox" runat="server" CssClass="form-control form-control-sm"
                                                    OnTextChanged="dQtyTextBox_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderconvRate" runat="server"
                                                    Enabled="True" TargetControlID="transferQtyTextBox" FilterType="Custom" ValidChars="0123456789">
                                                </asp:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                                <asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-sm btn-info" Text="Submit"> <i class="fa fa-check"></i>Update</asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Depot Stock Adjustments Voucher List</div>

                <div class="ms-auto">
                    <div class="btn-group">

                        <a href="DepotStockAdjustmentsVoucher.aspx" class="btn btn-sm btn-outline-info "><i class="fa fa-plus" aria-hidden="true"></i>New Entry</a>

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
                                            $('.mySelect2').select2({
                                                theme: 'bootstrap4',
                                                width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
                                                placeholder: $(this).data('placeholder'),
                                                allowClear: Boolean($(this).data('allow-clear')),
                                            });
                                            $('.datepicker').pickadate({
                                                selectMonths: true,
                                                selectYears: true
                                            })

                                        }
                                    </script>
                                    <div class="row">



                                        <div class="col-4">




                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-5 col-form-label">Sales Center: </label>

                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="dcDropDownList1" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2">
                                                    </asp:DropDownList>




                                                </div>

                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-5 col-form-label">From Date:  <span style="color: red">*</span></label>

                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="InvoiceDateTextBox" runat="server" class="form-control form-control-sm mb-3 datepicker" autocomplete="off" placeholder="Select Invoice From Date"></asp:TextBox>





                                                </div>

                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-5 col-form-label">To Date:  <span style="color: red">*</span></label>

                                                <div class="col-sm-7">

                                                    <asp:TextBox ID="todateTextBox" runat="server" class="form-control form-control-sm mb-3 datepicker" autocomplete="off" placeholder="Select Invoice To Date"></asp:TextBox>




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

                                                    <asp:LinkButton OnClick="SearchButton_Click" runat="server" ID="submitButton" class="btn btnMyDesignSearch   btn-sm">
                                            <i class="fa fa-print" aria-hidden="true"></i>&nbsp; View Report
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>



                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="table-responsive" id="MainGradeDiv">

                                            <asp:HiddenField ID="masSta" runat="server" />

                                            <asp:HiddenField ID="masId" runat="server" />

                                            <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                                DataKeyNames="DcStockOutMasterId,Status,DepotStatus,EntryBy"
                                                OnRowCommand="loadGridView_RowCommand" CssClass="table table-striped table-bordered" OnPreRender="gv_DocumentUpload_PreRender">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Report">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="reportImageButton" runat="server"
                                                                CommandArgument="<%# Container.DataItemIndex %>" CommandName="ReportView" ImageUrl="~/images/report-disk-icon.png" />


                                                            <asp:HiddenField runat="server" ID="hfDepotStatus" Value='<%#Eval("DepotStatus")%>' />
                                                            <asp:HiddenField runat="server" ID="hfDcStockOutMasterId" Value='<%#Eval("DcStockOutMasterId")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DcStockOutCode" HeaderText="Code" />
                                                    <asp:BoundField DataField="ComUnitName" HeaderText="Depot Name" />
                                                    <asp:BoundField DataField="Reason" HeaderText="Invoice Type" />

                                                    <asp:BoundField DataField="Isdoctor" HeaderText="Customer/Doctor" />
                                                    <asp:BoundField DataField="Name" HeaderText="Customer/Doctor Name" />
                                                    <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />

                                                    <asp:BoundField DataField="StockOutDate" HeaderText="StockOutDate " DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                                    <asp:TemplateField HeaderText=" Delivery Status">
                                                        <ItemTemplate>
                                                            <asp:DropDownList Enabled='<%# Eval("Status").ToString().Equals("Approved".ToString()) ? Convert.ToBoolean(1) : Convert.ToBoolean(1) %>' ID="statusDropDownList" runat="server" CssClass="form-control form-control-sm mySelect2">
                                                                <asp:ListItem Value="0">Select One</asp:ListItem>
                                                                <asp:ListItem Value="Full">Full</asp:ListItem>
                                                                <asp:ListItem Value="Partial">Partial</asp:ListItem>
                                                                <asp:ListItem Value="Reject">Reject</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="gotoinvoiceButton" runat="server" CssClass="btn btn-sm btn-info" Text="Submit" OnClick="gotoinvoiceButton_Click" Enabled='<%# Eval("Status").ToString().Equals("Approved".ToString()) ? Convert.ToBoolean(1) : Convert.ToBoolean(1) %>'
                                                                OnClientClick="return sweetAlertConfirm_Submit(this);">
                                                                    <i class="fa fa-check"></i>Submit</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Delete" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="editImageButton" runat="server"
                                                                CommandArgument="<%# Container.DataItemIndex %>" CommandName="DeleteData" ImageUrl="~/images/delete.png"
                                                                OnClientClick="return GetConfirmation();" />
                                                            <script type="text/javascript">
                                                                function GetConfirmation() {
                                                                    var reply = confirm("Ary you sure you want to delete this?");
                                                                    if (reply) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                            </script>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

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

