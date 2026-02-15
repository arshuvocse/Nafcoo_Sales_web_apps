<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="ProductTargetView.aspx.cs" Inherits="SInventory_UI_ProductTargetView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div id="popDiv">
    </div>

    <div class="page-wrapper">
        <div class="page-content">

            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Product Wise Target Category View</div>

                <div class="ms-auto">
                    <div class="btn-group">


                        <a href="../SInventory_UI/ProductTarget.aspx" class="btn btn-sm btn-sm btn-outline-info"><i class="fa fa-plus"></i>Add New</a>


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

                                        <div class="col-2"></div>
                                        <div class="col-6">

                                            <div class="form-group row ">
                                                <label for="" class="col-sm-4 col-form-label col-form-label-sm">Search Category: </label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList runat="server" ID="ddlSearchCategory" CssClass="form-control form-control-sm mySelect2" />
                                                </div>
                                            </div>

                                        </div>

                                        <div class="col-3"></div>
                                    </div>

                                    <hr />

                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">

                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-3 col-form-label"></label>
                                                <div class="col-sm-8">

                                                    <asp:LinkButton OnClick="masterButton_Click" runat="server" ID="masterButton" class="btn btnMyDesignSearch btn-sm"><i class="fa fa-print" aria-hidden="true"></i>&nbsp; View Category </asp:LinkButton>
                                                    <asp:LinkButton OnClick="detailButton_Click" runat="server" ID="detailButton" class="btn btn-primary btn-sm"><i class="fa fa-print" aria-hidden="true"></i>&nbsp; View Details</asp:LinkButton>
                                                    <asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>

                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">
                                        </div>
                                    </div>
                                    <hr />

                                    <div class="row">
                                        <div class="col-4">
                                            <h5> <i class="fa fa-list-ul" aria-hidden="true"></i> Product Wise Target Category List </h5>
                                        </div>
                                        <div class="col-5">
                                        </div>
                                        <div class="col-3">

                                            <div class="form-group row  pull-right">
                                                <asp:LinkButton ID="btnExport" class="btn btn-sm " Style="background-color: #1A7343; color: #fff;" runat="server" OnClick="btnExport_Click"><i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp; Export to Excel </asp:LinkButton>
                                            </div>
                                        </div>

                                    </div>
                                    <hr />



                                    <div class="table-responsive" id="MainGradeDiv" style="height: auto !important;">

                                        <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-hover"
                                            DataKeyNames="TargetId" OnRowCommand="loadGridView_RowCommand" CssClass="table table-striped table-bordered" OnPreRender="gv_DocumentUpload_PreRender" AllowPaging="True" PageSize="15" PageIndex="0">
                                            <Columns>

                                                <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfGatePassMasterId" Value='<%#Eval("TargetId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="TargetCategory" HeaderText="Target Category" />
                                                <asp:BoundField DataField="TotalTargetByTp" HeaderText="Total Target (TP)" />
                                                <asp:BoundField DataField="TotalTargetByTpVat" HeaderText="Total Target (TP+VAT)" />
                                                <asp:BoundField DataField="EntryBy" HeaderText="Entry By" />
                                                <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" />
                                                <asp:BoundField DataField="UpdateBy" HeaderText="Updated By" />
                                                <asp:BoundField DataField="UpdatedDate" HeaderText="Updated Date" />

                                            </Columns>
                                        </asp:GridView>


                                        <asp:GridView ID="detailGridView" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered" OnPreRender="gv_DocumentUpload_PreRender2" AllowPaging="True" PageSize="15" PageIndex="0" OnPageIndexChanging="loadGridView_PageIndexChanging2">
                                            <Columns>

                                                <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="TargetCategory" HeaderText="Target Category" />
                                                <asp:BoundField DataField="ProductCode" HeaderText="Product Code" />
                                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                                <asp:BoundField DataField="PackSizeName" HeaderText="Pack Size" />
                                                <asp:BoundField DataField="TargetQty" HeaderText="Target Qty" />
                                                <asp:BoundField DataField="TpPerPack" HeaderText="TP/Pack" />
                                                <asp:BoundField DataField="VatPerPack" HeaderText="VAT/Pack" />
                                                <asp:BoundField DataField="TargetValueByTp" HeaderText="Total Value (TP)" />
                                                <asp:BoundField DataField="TargetValueByTpVat" HeaderText="Total Value (VAT)" />


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


</asp:Content>



