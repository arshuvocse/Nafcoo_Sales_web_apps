<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="ProformaReport.aspx.cs" Inherits="SInventory_UI_ProformaReport"  %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.28364, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Src="~/SInventory_UI/IVMarketStructureInvoSearchReport.ascx" TagPrefix="uc1" TagName="IVMarketStructure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .button-padding-right {
            margin-right: 5px;
        }
        .SelectchkChoice label {
            padding-left: 4px;
            font-weight: bold;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Invoice Report</div>

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
           <%--           <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>

                                    <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                        <ProgressTemplate>

                                            <div class="divWaiting">
                                                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="../images/Spinner.gif" Width="180px" Height="180px" />
                                            </div>

                                        </ProgressTemplate>
                                    </asp:UpdateProgress>--%>
                            <asp:HiddenField ID="HiddenFieldData" runat="server" />

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
                                                    <asp:TextBox ID="InvoiceDateTextBox" AutoPostBack="true" OnTextChanged="fromDateTextBox_TextChanged" runat="server" class="form-control form-control-sm mb-3 datepicker" autocomplete="off" placeholder="Select Invoice From Date"></asp:TextBox>





                                                </div>

                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-5 col-form-label">To Date:  <span style="color: red">*</span></label>

                                                <div class="col-sm-7">

                                                    <asp:TextBox ID="todateTextBox" runat="server" class="form-control form-control-sm mb-3 datepicker" autocomplete="off" placeholder="Select Invoice To Date"></asp:TextBox>




                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-8">
                                            <uc1:IVMarketStructure runat="server" ID="IVMarketStructure" />
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">


                                            <br />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">

                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-3 col-form-label"></label>
                                                <div class="col-sm-8">

                                                    <asp:LinkButton OnClick="SearchButton_Click" runat="server" ID="submitButton" class="btn btnMyDesignSearch   btn-sm"><i class="fa fa-print" aria-hidden="true"></i>&nbsp; View Report </asp:LinkButton>
                                                    <asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>



                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">
                                        </div>
                                    </div>



                                    <div class="row">
                                        <div class="col-2">
                                            <h3>Details List</h3>
                                        </div>
                                        <div class="col-7">
                                        </div>
                                        <div class="col-3">

                                            <div class="form-group row  pull-right">
                                                <asp:LinkButton ID="btnExport" class="btn btn-sm   mb-2" Style="background-color: #1A7343; color: #fff;" runat="server" OnClick="btnExport_Click"><i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp; Export to Excel </asp:LinkButton>

                                                <%--   <button type="button" class="btn btn-sm   mb-2"  style="background-color: #1A7343; color: #fff;" onclick="exportToExcel()"><i class="fa fa-file-pdf-o" aria-hidden="true"></i>&nbsp; Export to Excel </button>--%>
                                            </div>
                                        </div>

                                    </div>
                                    <hr />

                                    <div class="table-responsive" id="MainGradeDiv" style="height: 600px">


                                        <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered" OnPreRender="gv_DocumentUpload_PreRender" AllowPaging="True" PageIndex="0" OnPageIndexChanging="loadGridView_PageIndexChanging">
                                            <Columns>
 
                                                 <asp:BoundField DataField="Sales Center" HeaderText="Sales Center" />
        <asp:BoundField DataField="Sales Center Name" HeaderText="Sales Center Name" />
        <asp:BoundField DataField="Customer ID" HeaderText="Customer ID" />
        <asp:BoundField DataField="Customer Name" HeaderText="Customer Name" />
        <asp:BoundField DataField="Customer Type" HeaderText="Customer Type" />
        <asp:BoundField DataField="Mode of Payment" HeaderText="Mode of Payment" />
        <asp:BoundField DataField="Order Code" HeaderText="Order Code" />
        <asp:BoundField DataField="Order / Submission Date" HeaderText="Order / Submission Date" />
        <asp:BoundField DataField="Proforma Number" HeaderText="Proforma Number" />
        <asp:BoundField DataField="Proforma Date" HeaderText="Proforma Date" />
        <asp:BoundField DataField="Proforma By" HeaderText="Proforma By" />
        <asp:BoundField DataField="Invoice No" HeaderText="Invoice No" />
        <asp:BoundField DataField="Invoice Date" HeaderText="Invoice Date" />
        <asp:BoundField DataField="Confirm By" HeaderText="Confirm By" />
        <asp:BoundField DataField="Product Code" HeaderText="Product Code" />
        <asp:BoundField DataField="Product Name" HeaderText="Product Name" />
        <asp:BoundField DataField="Pack Size" HeaderText="Pack Size" />
        <asp:BoundField DataField="Batch No" HeaderText="Batch No" />
        <asp:BoundField DataField="Exp Date" HeaderText="Exp Date" />
        <asp:BoundField DataField="Invoice Qty" HeaderText="Invoice Qty" />
        <asp:BoundField DataField="TP" HeaderText="TP" />
        <asp:BoundField DataField="VAT" HeaderText="VAT" />
        <asp:BoundField DataField="Discount" HeaderText="Discount" />
        <asp:BoundField DataField="FOC" HeaderText="FOC" />
        <asp:BoundField DataField="Vat On FOC" HeaderText="Vat On FOC" />
        <asp:BoundField DataField="Net TP" HeaderText="Net TP" />
        <asp:BoundField DataField="Net Amount" HeaderText="Net Amount" />
        <asp:BoundField DataField="Adjustment" HeaderText="Adjustment" />
        <asp:BoundField DataField="Payment No" HeaderText="Payment No" />
        <asp:BoundField DataField="PaymentDate" HeaderText="PaymentDate" />
        <asp:BoundField DataField="Pay Amount" HeaderText="Pay Amount" />
        <asp:BoundField DataField="Due" HeaderText="Due" />
        <asp:BoundField DataField="Sales Return" HeaderText="Sales Return" />
        <asp:BoundField DataField="Invoice Type" HeaderText="Invoice Type" />
        <asp:BoundField DataField="Market Code" HeaderText="Market Code" />
        <asp:BoundField DataField="Market Name" HeaderText="Market Name" />
        <asp:BoundField DataField="Territory Code" HeaderText="Territory Code" />
        <asp:BoundField DataField="Territory" HeaderText="Territory" />
        <asp:BoundField DataField="MBE" HeaderText="MBE" />
        <asp:BoundField DataField="Area Code" HeaderText="Area Code" />
        <asp:BoundField DataField="Area" HeaderText="Area" />
        <asp:BoundField DataField="ABM" HeaderText="ABM" />
        <asp:BoundField DataField="Region Code" HeaderText="Region Code" />
        <asp:BoundField DataField="Region" HeaderText="Region" />
        <asp:BoundField DataField="RBM" HeaderText="RBM" />
        <asp:BoundField DataField="Cluster Code" HeaderText="Cluster Code" />
        <asp:BoundField DataField="Cluster" HeaderText="Cluster" />
        <asp:BoundField DataField="Cluster Head" HeaderText="Cluster Head" />
        <asp:BoundField DataField="Group Code" HeaderText="Group Code" />
        <asp:BoundField DataField="Group" HeaderText="Group" />
        <asp:BoundField DataField="NSM" HeaderText="NSM" />

                                                <%--      <asp:BoundField DataField="ProductOffer" HeaderText="Campaign Type" />--%>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" CssClass="GridPager" />
                                        </asp:GridView>
                                    </div>

                            <%--    </ContentTemplate>
                                <Triggers>

                                    <asp:PostBackTrigger ControlID="btnExport" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>


    <script>

        function exportToExcel() {

            var file = new Blob([$('#MainGradeDiv').html()], { type: "application/vnd.ms-excel" });
            var url = URL.createObjectURL(file);
            var a = $("<a />", {
                href: url,
                download: "Invoice Report.xls"
            }).appendTo("body").get(0).click();
            e.preventDefault();

        }

        function exportTableToExcel(tableID, filename) {
            var downloadLink;
            var dataType = 'application/vnd.ms-excel';
            var tableSelect = document.getElementById(tableID);
            var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');

            // Specify file name
            filename = filename ? filename + '.xls' : 'excel_data.xls';

            // Create download link element
            downloadLink = document.createElement("a");

            document.body.appendChild(downloadLink);

            if (navigator.msSaveOrOpenBlob) {
                var blob = new Blob(['\ufeff', tableHTML], {
                    type: dataType
                });
                navigator.msSaveOrOpenBlob(blob, filename);
            } else {
                // Create a link to the file
                downloadLink.href = 'data:' + dataType + ', ' + tableHTML;

                // Setting the file name
                downloadLink.download = filename;

                //triggering the function
                downloadLink.click();
            }
        }
    </script>

</asp:Content>

