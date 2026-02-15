<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="CustomerReport.aspx.cs" Inherits="MasterSetup_UI_CustomerReport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <style type="text/css">
        /*AutoComplete flyout */
      

       

        /* AutoComplete item */

        .autocomplete_listItem {
            padding: 6px !important;
            cursor: pointer !important;
            font-weight: bold !important;
            background-color: #fff !important;
            border-bottom: 1px solid #d4d4d4 !important;
            box-shadow: 0 1px 1px rgba(0, 0, 0, 0.075) inset !important;
        }

        .ssss {
            font-size: 13px;
            font-weight: bold;
        }
    </style>

    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Customer  Report</div>

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
                                            $('.multiple-select').select2({
                                                includeSelectAllOption: true,
                                                theme: 'bootstrap4',
                                                width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
                                                placeholder: $(this).data('placeholder'),
                                                allowClear: Boolean($(this).data('allow-clear')),
                                            });
                                            $('.mySelect2').select2({
                                                theme: 'bootstrap4',
                                                width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
                                                placeholder: $(this).data('placeholder'),
                                                allowClear: Boolean($(this).data('allow-clear')),
                                            });
                                        }
                                        
                                    </script>


                                    <div style="padding: 2px!important"></div>

                                    <div class="row">
                                        
                                        
                                        
                                        <div class="col-md-3"></div>
                                    

                                        <div class="col-6">

                                            <div class="form-group row" runat="server">
                                                <label  class="col-sm-3 col-form-label">Division: </label>

                                                <div class="col-sm-8">
                                                    <div class="input-group">
                                                        <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="DivisionSelect_SelectedIndexChanged"  id="DivisionSelect" name="DivisionSelect" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label for="" class="col-sm-3 col-form-label">District: </label>

                                                <div class="col-sm-8">
                                                    <div class="input-group">
                                                        
                                                        
                                                        <asp:DropDownList runat="server"   OnSelectedIndexChanged="DistrictSelect_SelectedIndexChanged"  AutoPostBack="true"  id="DistrictSelect" name="DistrictSelect"  class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>
                                                        
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label">Thana: </label>

                                                <div class="col-sm-8">
                                                    <div class="input-group">
                                                         <asp:DropDownList runat="server"    id="ThanaSelect" name="ThanaSelect"  class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>                                                
                                                    </div>
                                                </div>
                                            </div>
                                     
                                        </div>
                                    </div>



                                    <div style="padding-top: 16px;"></div>
                                    <div class="row">
                                        <div class="col-md-5">
                                        </div>
                                        <div class="col-md-4" style="align-content: center">
                                            <asp:LinkButton runat="server" ID="btnSearch" class="btn btnMyDesignSearch   btn-sm " OnClick="btnSearch_Click">  <i class="fa fa-search-plus"></i>&nbsp; Search</asp:LinkButton>


                                            <asp:LinkButton runat="server" class="btn btnMyDesignReset   btn-sm" ID="resetBtn" OnClick="resetBtn_Click"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label></label>
                                        </div>


                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-1">
                                        </div>

                                        <div class="col-md-2" style="margin-top: 5px;">
                                           <%-- <asp:Label ID="lblCount" runat="server" CssClass="ssss btn bg-info pull-right" Text="Total : 0"></asp:Label>--%>


                                        </div>


                                        <div class="col-md-3">
                                            <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btn btn-success pull-right" OnClick="btnExportToExcel_Click"><span aria-hidden="true" class="fa fa-file-excel-o" ></span> &nbsp;Export To Excel</asp:LinkButton>


                                        </div>
                                    </div>
                                    <br />
                                    <div class="table-responsive" id="MainGradeDiv">

                                        <%--onrowcommand="loadGridView_RowCommand"--%>

                                        <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                             OnRowCommand="loadGridView_RowCommand"
                                            CssClass="table table-striped table-bordered" OnPreRender="gv_DocumentUpload_PreRender" AllowPaging="True" PageIndex="0" OnPageIndexChanging="loadGridView_PageIndexChanging">
                                            <Columns>

                           
                                                <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />
                                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                                <asp:BoundField DataField="MarketCode" HeaderText="Market Code" />
                                                <asp:BoundField DataField="MarketName" HeaderText="Market Name" />

                                        
                                                <asp:BoundField DataField="DivisionName" HeaderText="Division " />
                                                        
                                                <asp:BoundField DataField="DistrictName" HeaderText="District " />
                                   
                                                <asp:BoundField DataField="ThanaName" HeaderText="Thana " />
                                      
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" CssClass="GridPager" />
                                        </asp:GridView>
                                    </div>






                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnExportToExcel" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>

        //$(document).ready(function () {

        //    var table = $('#ContentPlaceHolder1_loadGridView').DataTable(
        //        {
        //            "bInfo": true,
        //            "bFilter": true,
        //            lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
        //            pageLength: 10,
        //            dom: 'lBfrtip',


        //            buttons: ['copy', 'excel', 'pdf', 'print']
        //        }
        //    );

        //    var prm = Sys.WebForms.PageRequestManager.getInstance();
        //    if (prm != null) {
        //        prm.add_endRequest(function (sender, e) {
        //            if (sender._postBackSettings.panelsToUpdate != null) {
        //                table = $('#ContentPlaceHolder1_loadGridView').DataTable(
        //                    {
        //                        "bInfo": true,
        //                        "bFilter": true,
        //                        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
        //                        pageLength: 10,
        //                        dom: 'lBfrtip',


        //                        buttons: ['copy', 'excel', 'pdf', 'print']


        //                    }
        //                );
        //            }
        //        });
        //    };


        //    table.columns().every(function () {
        //        var that = this;


        //    });
        //});


    </script>
</asp:Content>

