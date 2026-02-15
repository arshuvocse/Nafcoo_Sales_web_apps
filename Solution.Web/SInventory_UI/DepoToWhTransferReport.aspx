<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="DepoToWhTransferReport.aspx.cs" Inherits="SInventory_UI_DepoToWhTransferReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>

            <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                <ProgressTemplate>

                    <div class="divWaiting">
                        <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="../images/Spinner.gif" Width="180px" Height="180px" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <div class="page-wrapper">
                <div class="page-content">
                    <!--breadcrumb-->
                    <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                        <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i> Depo to WH Transfer Report </div>

                        <div class="ms-auto">
                            <div class="btn-group">

                                <%--<asp:LinkButton ID="viewLinkButton" CssClass="btn btn-sm btn-outline-info " runat="server" OnClick="custCetegoryAddImageButton_Click"><i class="fa fa-plus" aria-hidden="true"></i> New Entry </asp:LinkButton>--%>

                            </div>


                        </div>
                    </div>
                </div>
                <!--end breadcrumb-->
                <div class="row">
                    <div class="col">

                        <div class="card border-top border-0 border-4 border-success">
                            <div class="card-body">

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




                                <div class="row">
                                    <div class="col-2">&nbsp;</div>
                                    <div class="col-8">
                                        <div class="form-group row">
                                            <label for="mainName" class="col-sm-3 col-form-label"> Chalan From Date:</label>

                                            <div class="col-sm-5">



                                                <asp:TextBox ID="fromDateTextBox" runat="server" CssClass="form-control form-control-sm  datepicker "></asp:TextBox>


                                            </div>
                                            <span class="text-sm-left text-c-red">*</span>
                                        </div>


                                        <div class="form-group row">
                                            <label for="mainName" class="col-sm-3 col-form-label">Chalan To Date:</label>

                                            <div class="col-sm-5">



                                                <asp:TextBox ID="toDateTextBox" runat="server" CssClass="form-control form-control-sm  datepicker "></asp:TextBox>

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


                                                <asp:LinkButton runat="server" ID="btnSearch" class="btn btnMyDesignSearch   btn-sm " OnClick="searchButton_Click">  <i class="fa fa-search-plus"></i>&nbsp; View Report </asp:LinkButton>
                                                <asp:LinkButton runat="server" class="btn btnMyDesignReset   btn-sm" ID="cancelButton" OnClick="cancelButton_Click"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>


                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-2">&nbsp;</div>
                                </div>
                                <hr />

                                <div class="row">

                                    <div class="col-md-2">
                                    </div>

                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-3">
                                        <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btn btn-success pull-right" OnClick="btnExportToExcel_Click"><span aria-hidden="true" class="fa fa-file-excel-o" ></span> &nbsp;Export To Excel</asp:LinkButton>

                                    </div>
                                </div>

                                <hr />
 
                                <div class="row">
                                    <div class="table-responsive" id="MainGradeDiv2" style="height: 600px">

                                        <asp:GridView ID="detailGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered  text-center thead-dark" OnPreRender="gv_DocumentUpload_PreRender"
                                            DataKeyNames="SChalanId,ChalanNo">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#SL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ChalanNo" HeaderText="Chalan No" />
                                                <asp:BoundField DataField="ChalanDate" HeaderText="Chalan Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                <%-- <asp:BoundField DataField="WearhouseName" HeaderText="WearhouseName" />--%>
                                                
                                                 <asp:BoundField DataField="FromComUnitCode" HeaderText="From Unit Code" />
                                                <asp:BoundField DataField="FromComUnitName" HeaderText="From Unit Name" />
                                               


                                                <asp:BoundField DataField="TrackNo" HeaderText="Track No" />
                                                <asp:BoundField DataField="DriverName" HeaderText="DriverName" />



                                                <asp:BoundField DataField="ProductCode" HeaderText="Product Code" />
                                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                                <asp:BoundField DataField="StockUOMName" HeaderText="UOM" />
                                                <asp:BoundField DataField="BatchNo" HeaderText="Batch No" />
                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                                
                                                
                                                 <asp:BoundField DataField="EmpMasterCode" HeaderText="Forward By ID" />
                                                <asp:BoundField DataField="ForwardBy" HeaderText="Forward By" />
                                                <asp:BoundField DataField="ForwardDate" HeaderText="Forward Date" />
                                                
                                                
                                                 <asp:BoundField DataField="ReceiveEmpMasterCode" HeaderText="ReceiveBy By ID" />
                                                <asp:BoundField DataField="ReceiveByBy" HeaderText="ReceiveBy By" />
                                                <asp:BoundField DataField="ChalanReceiveDate" HeaderText="Receive Date" />

<%--                                                <asp:BoundField DataField="TotalValue" HeaderText="Total Value" />
                                                <asp:BoundField DataField="TotalVat" HeaderText="Total Vat" />
                                                <asp:BoundField DataField="GrandTotal" HeaderText="Grand Total" />



                                                <asp:BoundField DataField="Status" HeaderText="Status" />

                                                <asp:BoundField DataField="ReceiveEmpMasterCode" HeaderText="Receive By ID" />
                                                <asp:BoundField DataField="ReceiveByBy" HeaderText="ReceiveBy By" />
                                                <asp:BoundField DataField="ChalanReceiveDate" HeaderText="Receive Date" />--%>



                                            </Columns>
                                        </asp:GridView>

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
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
        </Triggers>

    </asp:UpdatePanel>
    

</asp:Content>

