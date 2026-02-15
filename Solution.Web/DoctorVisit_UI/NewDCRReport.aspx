<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="NewDCRReport.aspx.cs" Inherits="DoctorVisit_UI_NewDCRReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i> DCR Report </div>

                <div class="ms-auto">
                    <div class="btn-group">

                        <%--<asp:LinkButton ID="EmpCetegoryAddImageButton" CssClass="btn btn-sm btn-outline-info " runat="server" OnClick="EmpCetegoryAddImageButton_Click"><i class="fa fa-plus" aria-hidden="true"></i> New Entry </asp:LinkButton>--%>

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

                                        <div class="col-5">
                                            <div class="form-group row ">
                                                <label for="" class="col-sm-4 col-form-label col-form-label-sm">From Date: </label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox  runat="server"  id="FromDate" type="text" class="form-control form-control-sm datepicker" autocomplete="off" placeholder="Select Date" ></asp:TextBox>
                                                </div>
                                            </div>
                                            
                                            <div class="form-group row ">
                                                <label for="" class="col-sm-4 col-form-label col-form-label-sm">To Date: </label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox  runat="server"  id="ToDate" type="text" class="form-control form-control-sm datepicker"   autocomplete="off" placeholder="Select Date"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                        

                                        <div class="col-5">

                                            <div class="form-group row ">
                                                <label for="" class="col-sm-4 col-form-label col-form-label-sm"> Employee Name: </label>
                                                <div class="col-sm-8">
                                                     <asp:DropDownList  runat="server" id="EmployeeIdSelect" name="EmployeeIdSelect" class="form-select form-select-sm mySelect2"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row ">
                                                <label for="" class="col-sm-4 col-form-label col-form-label-sm"> User Role: </label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList  runat="server" id="UserRoleSelect" name="UserRoleSelect" class="form-select form-select-sm mySelect2"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">

                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-4 col-form-label"></label>
                                                <div class="col-sm-8">
                                                    <asp:LinkButton OnClick="SearchButton_Click" runat="server" ID="masterButton" class="btn btnMyDesignSearch btn-sm"><i class="fa fa-print" aria-hidden="true"></i>&nbsp; View Report</asp:LinkButton>
                                                    <%--<asp:LinkButton OnClick="detailButton_Click" runat="server" ID="detailButton" class="btn btn-primary btn-sm"><i class="fa fa-print" aria-hidden="true"></i>&nbsp; View Details </asp:LinkButton>--%>
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
                                            <h4><i class="fa fa-list-ul" aria-hidden="true"></i>&nbsp; DCR List </h4>
                                        </div>
                                        <div class="col-5">
                                        </div>
                                        <div class="col-3">

                                            <div class="form-group row  pull-right">
                                                <asp:LinkButton ID="btnExport" class="btn btn-sm" Style="background-color: #1A7343; color: #fff;" runat="server" OnClick="btnExport_Click"><i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp; Export to Excel </asp:LinkButton>


                                            </div>
                                        </div>

                                    </div>
                                    <hr />

                                    <div class="table-responsive" id="MainGradeDiv" style="min-height: 400px">

                                        <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="DcrId"
                                            CssClass="table table-striped table-bordered" OnPreRender="gv_DocumentUpload_PreRender" AllowPaging="True" PageIndex="0" OnPageIndexChanging="loadGridView_PageIndexChanging">
                                            <Columns>

                                                <asp:BoundField DataField="dcrDate" HeaderText="DCR Date" />
                                                <asp:BoundField DataField="planned" HeaderText="Planned/Unplanned" />
                                                <asp:BoundField DataField="doctorName" HeaderText="Doctor Name" />
                                                <asp:BoundField DataField="doctorType" HeaderText="Doctor Type" />
                                                <%--<asp:BoundField DataField="statusofCall" HeaderText="Call Status" />--%>
                                                <asp:BoundField DataField="callTime" HeaderText="Call Time" />
                                                <asp:BoundField DataField="callType" HeaderText="Call Type" />
                                                <asp:BoundField DataField="EntryBy" HeaderText="DCR By" />
                                                <asp:BoundField DataField="RoleName" HeaderText="Role Name" />
                                                <%--<asp:BoundField DataField="ApprovalStatus" HeaderText="Approval Status" />--%>

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

