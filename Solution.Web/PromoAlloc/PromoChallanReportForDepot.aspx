<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="PromoChallanReportForDepot.aspx.cs" Inherits="PromoAlloc_PromoChallanReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i> Promo Materials Challan View </div>

                <div class="ms-auto">
                    <div class="btn-group">
                        <%--<a href="../SInventory_UI/PromoMaterialsReport.aspx" class="btn btn-sm btn-outline-info "><i class="fa fa-plus" aria-hidden="true"></i> New Challan </a>--%>
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

                                        <div class="col-3"></div>
                                        <div class="col-4">
                                            
                                            <div class="form-group row">
                                                    <label for="" class="col-sm-4 col-form-label col-form-label-sm"> Depot Name: </label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList class="form-select mySelect2" AutoPostBack="True" OnSelectedIndexChanged="ddlUnit_OnSelectedIndexChanged" runat="server" ID="ddlUnit"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            
                                            

                                            <div class="form-group row ">
                                                <label for="" class="col-sm-4 col-form-label col-form-label-sm">From Date: </label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="fromDateTextBox" runat="server" AutoPostBack="True" OnTextChanged="fromDateTextBox_OnTextChanged" CssClass="form-control form-control-sm datepicker"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group row ">
                                                <label for="" class="col-sm-4 col-form-label col-form-label-sm">To Date: </label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="todateTextBox" runat="server" AutoPostBack="True" OnTextChanged="todateTextBox_OnTextChanged" CssClass="form-control form-control-sm datepicker"></asp:TextBox>
                                                </div>
                                            </div>
                                            
                                            
                                            <div class="form-group row" runat="server">
                                                <label class="col-sm-4 col-form-label"> Promo Challan: </label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList runat="server" ID="ddlPromoChallan" CssClass="form-control form-control-sm mySelect2" />
                                                </div>

                                            </div>


                                        </div>
                                        
                                        <div class="col-5"></div>

                                        



                                    </div>

                                   
                                    <br />
                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">

                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-3 col-form-label"></label>
                                                <div class="col-sm-8">

                                                    <asp:LinkButton OnClick="masterButton_Click" runat="server" ID="masterButton" class="btn btnMyDesignSearch btn-sm"><i class="fa fa-print" aria-hidden="true"></i>&nbsp; View Challan </asp:LinkButton>
                                                    <asp:LinkButton OnClick="detailButton_Click" runat="server" ID="detailButton" class="btn btn-primary btn-sm"><i class="fa fa-print" aria-hidden="true"></i>&nbsp; View Details</asp:LinkButton>
                                                    <asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>

                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">
                                        </div>
                                    </div>



                                    <div class="row">
                                        <div class="col-4">
                                            <h4> Promo Materials Challan List </h4>
                                        </div>
                                        <div class="col-5">
                                        </div>
                                        <div class="col-3">

                                            <div class="form-group row  pull-right">
                                                <%--<asp:LinkButton ID="LinkButton1" class="btn btn-sm   mb-2" Style="background-color: #1A7343; color: #fff;" runat="server" OnClick="Button1_Click"><i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp; Print </asp:LinkButton>--%>
                                                <asp:LinkButton ID="btnExport" class="btn btn-sm   mb-2" Style="background-color: #1A7343; color: #fff;" runat="server" OnClick="btnExport_Click"><i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp; Export to Excel </asp:LinkButton>
                                            </div>
                                        </div>

                                    </div>
                                    <hr />

                                    <div class="table-responsive" id="MainGradeDiv" style="height: auto !important;">

                                        <asp:GridView ID="masterGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="PromoChallanId,ApproveStatus"
                                            CssClass="table table-striped table-bordered" OnPreRender="gv_DocumentUpload_PreRender" AllowPaging="True" PageSize="20" PageIndex="0" OnPageIndexChanging="loadGridView_PageIndexChanging">
                                            <Columns>
                                                <asp:BoundField DataField="ComUnitName" HeaderText="To Depot" />
                                                <asp:BoundField DataField="PromoChallanCode" HeaderText="Challan Code" />
                                                <asp:BoundField DataField="IssueDate" HeaderText="Date of Challan" />
                                                <asp:BoundField DataField="IssueBy" HeaderText="Challan By" />
                                                <asp:BoundField DataField="ForwardingStatus" HeaderText="Forward Status" />
                                                <asp:BoundField DataField="ForwardBy" HeaderText="Forward By" />
                                                <asp:BoundField DataField="ForwardDate" HeaderText="Date of Forward" />
                                                <asp:BoundField DataField="ApproveStatus" HeaderText="Approval Status" />
                                                <%--<asp:BoundField DataField="ApproveBy" HeaderText="Approved By" />
                                                <asp:BoundField DataField="ApproveDate" HeaderText="Approved Date" />--%>

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        
                                                        <asp:LinkButton ID="btnForward" runat="server" CommandArgument="<%# Container.DataItemIndex %>" class="btn-info  btn-sm mb-1 mb-md-0"
                                                            CommandName="ForwardData" OnClientClick="return confirm('Are you really aware of this operation?');"
                                                            OnClick="btnForward_Click"><i class="fa fa-check"> Receive </i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" CssClass="GridPager" />
                                        </asp:GridView>


                                        <asp:GridView ID="detailGridView" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered" OnPreRender="gv_DocumentUpload_PreRender2" AllowPaging="True" PageIndex="0" OnPageIndexChanging="loadGridView_PageIndexChanging2">
                                            <Columns>
                                                
                                                <asp:BoundField DataField="ComUnitName" HeaderText="Depot Name" />
                                                <asp:BoundField DataField="PromoChallanCode" HeaderText="Promo Challan Code" />
                                                <asp:BoundField DataField="ChallanDate" HeaderText="Challan Date" />
                                                <asp:BoundField DataField="Year" HeaderText="Year" />
                                                <asp:BoundField DataField="Month" HeaderText="Month" />
                                                <asp:BoundField DataField="TerritoryName" HeaderText="Territory" />
                                                <asp:BoundField DataField="MioName" HeaderText="MBE Name" />
                                                <asp:BoundField DataField="PromoGroupName" HeaderText="Promo Group Name" />
                                                <asp:BoundField DataField="ProductSQName" HeaderText="Brand Name" />
                                                <asp:BoundField DataField="PromoProductName" HeaderText="Promo Product" />
                                                <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                                                <asp:BoundField DataField="CurrentStock" HeaderText="Current Stock" />

                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" CssClass="GridPager" />

                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnExport" />
                                </Triggers>
                            </asp:UpdatePanel>
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
                download: "Packing Report.xls"
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

