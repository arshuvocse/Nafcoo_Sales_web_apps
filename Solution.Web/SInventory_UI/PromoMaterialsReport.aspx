<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="PromoMaterialsReport.aspx.cs" Inherits="SInventory_UI_PromoMaterialsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Promo Material Report </div>

                <div class="ms-auto">
                    <div class="btn-group">
                        <a href="../PromoAlloc/GroupWisePromoQtyEntry.aspx" class="btn btn-sm btn-outline-info " style="margin-right: 7px !important;"><i class="fa fa-plus" aria-hidden="true"></i> New Allocation </a>

                        <a href="../PromoAlloc/PromoChallanReport.aspx" class="btn btn-sm btn-sm btn-outline-primary"> Go to challan list &nbsp; <i class="fa fa-forward"></i></a>

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
                                            
                                            <div class="form-group row ">
                                                <label for="" class="col-sm-4 col-form-label col-form-label-sm"> Depot Name: <span style="color: orangered">[*]</span> </label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList runat="server" ID="ddlUnit" CssClass="form-control form-control-sm mySelect2" AutoPostBack="True" OnSelectedIndexChanged="ddlUnit_OnSelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row ">
                                                <label for="" class="col-sm-4 col-form-label col-form-label-sm">Year: </label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList runat="server" ID="ddlYear" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>
                                                </div>
                                            </div>
                                            
                                            <div class="form-group row ">
                                                <label for="" class="col-sm-4 col-form-label col-form-label-sm">Month: </label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList runat="server" ID="ddlmonth" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-4"></div>

                                        

                                        



                                    </div>

                                    
                                    <br />
                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">

                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-3 col-form-label"></label>
                                                <div class="col-sm-8">

                                                    <asp:LinkButton OnClick="masterButton_Click" runat="server" ID="masterButton" class="btn btnMyDesignSearch btn-sm"><i class="fa fa-print" aria-hidden="true"></i>&nbsp; View Summery</asp:LinkButton>
                                                    <asp:LinkButton OnClick="detailButton_Click" runat="server" ID="detailButton" class="btn btn-primary btn-sm"><i class="fa fa-print" aria-hidden="true"></i>&nbsp; View Details </asp:LinkButton>
                                                    <asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>



                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">
                                        </div>
                                    </div>



                                    <div class="row">
                                        <div class="col-4">
                                            <h4> Promo Material List </h4>
                                        </div>
                                        <div class="col-5">
                                        </div>
                                        <div class="col-3">

                                            <div class="form-group row  pull-right">
                                                <asp:LinkButton ID="btnExport" class="btn btn-sm   mb-2" Style="background-color: #1A7343; color: #fff;" runat="server" OnClick="btnExport_Click"><i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp; Export to Excel </asp:LinkButton>

                                                <%--   <button type="button" class="btn btn-sm   mb-2"  style="background-color: #1A7343; color: #fff;" onclick="exportToExcel()"><i class="fa fa-file-pdf-o" aria-hidden="true"></i>&nbsp; Export to Excel </button>--%>
                                            </div>
                                        </div>

                                    </div>
                                    <hr />

                                    <div class="table-responsive" id="MainGradeDiv" style="height: auto !important;">


                                        <asp:GridView ID="masterGridView" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered" OnPreRender="gv_DocumentUpload_PreRender" 
                                            AllowPaging="True" PageIndex="0" PageSize="20" OnPageIndexChanging="loadGridView_PageIndexChanging">
                                            <Columns>
                                                <asp:BoundField DataField="ComUnitName" HeaderText="Depot Name" />
                                                <asp:BoundField DataField="Year" HeaderText="Year" />
                                                <asp:BoundField DataField="Month" HeaderText="Month" />
                                                <asp:BoundField DataField="PromoProductName" HeaderText="Promo Product" />
                                                <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                                                
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" CssClass="GridPager" />
                                        </asp:GridView>


                                        <asp:GridView ID="detailGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="GWPromoQtyId,isForwardAble"
                                            CssClass="table table-striped table-bordered" OnPreRender="gv_DocumentUpload_PreRender2" AllowPaging="False" PageSize="20" PageIndex="0" OnPageIndexChanging="loadGridView_PageIndexChanging2">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" AutoPostBack="True" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ComUnitName" HeaderText="Depot Name" />
                                                <asp:BoundField DataField="Year" HeaderText="Year" />
                                                <asp:BoundField DataField="Month" HeaderText="Month" />
                                                <asp:BoundField DataField="TerritoryName" HeaderText="Territory" />
                                                <asp:BoundField DataField="MioName" HeaderText="MBE Name" />
                                                <asp:BoundField DataField="PromoGroupName" HeaderText="Promo Group Name" />
                                                <asp:BoundField DataField="ProductSQName" HeaderText="Brand Name" />
                                                <asp:BoundField DataField="PromoProductName" HeaderText="Promo Product" />
                                                <asp:BoundField DataField="Qty" HeaderText="Quantity" />

                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" CssClass="GridPager" />

                                        </asp:GridView>
                                    </div>

                                    <br />
                                    <div class="row" id="divReport" runat="server" visible="False">
                                        <div class="col-4">&nbsp;</div>
                                        <div class="col-4">

                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-3 col-form-label"></label>
                                                <div class="col-sm-8">

                                                    <asp:LinkButton OnClick="btnProductionReport_Click" OnClientClick="return confirm('Are you really aware of this operation?');" runat="server" ID="btnProductionReport" class="btn btnMyDesignSearch btn-sm"><i class="fa fa-print" aria-hidden="true"></i>&nbsp; Generate Challan </asp:LinkButton>
                                                    <%--<asp:LinkButton OnClick="detailButton_Click" runat="server" ID="LinkButton2" class="btn btnMyDesignSearch btn-sm"><i class="fa fa-print" aria-hidden="true"></i>&nbsp; View Detail Report</asp:LinkButton>
                                                    <asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>--%>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-4">
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <br />

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

