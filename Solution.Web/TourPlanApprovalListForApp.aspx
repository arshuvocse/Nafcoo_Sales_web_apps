<%@ Page Title="Work Schedule Approval List" Language="C#" AutoEventWireup="true" CodeFile="TourPlanApprovalListForApp.aspx.cs" Inherits="TourPlanApprovalListForApp" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">

    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>📦 Promo Material Receive </title>
   <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
   <link href="../VerticalAsset/plugins/simplebar/css/simplebar.css" rel="stylesheet" />
   <link href="../VerticalAsset/plugins/perfect-scrollbar/css/perfect-scrollbar.css" rel="stylesheet" />
   <link href="../VerticalAsset/plugins/metismenu/css/metisMenu.min.css" rel="stylesheet" />
   <!-- loader-->

   <%--<link href="../VerticalAsset/css/pace.min.css" rel="stylesheet" />--%>

   <%--<link href="../VerticalAsset/plugins/datatable/css/dataTables.bootstrap5.min.css" rel="stylesheet" />--%>
   <%--<script src="../VerticalAsset/js/pace.min.js"></script>--%>
   <!-- Bootstrap CSS -->
   <link href="../VerticalAsset/css/bootstrap.min.css" rel="stylesheet">
   <link href="../VerticalAsset/css/bootstrap-extended.css" rel="stylesheet">
   <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;500&amp;display=swap" rel="stylesheet">
   <link href="../VerticalAsset/css/app.css" rel="stylesheet">
   <link href="../VerticalAsset/css/icons.css" rel="stylesheet">
   <!-- Theme Style CSS -->
   <link rel="stylesheet" href="../VerticalAsset/css/dark-theme.css" />
   <link rel="stylesheet" href="../VerticalAsset/css/semi-dark.css" />
   <link rel="stylesheet" href="../VerticalAsset/css/header-colors.css" />

   <link href="../VerticalAsset/plugins/select2/css/select2.min.css" rel="stylesheet" />
   <link href="../VerticalAsset/plugins/select2/css/select2-bootstrap4.css" rel="stylesheet" />

   <%--Date Picker--%>

   <link href="../VerticalAsset/plugins/datetimepicker/css/classic.css" rel="stylesheet" />
   <link href="../VerticalAsset/plugins/datetimepicker/css/classic.time.css" rel="stylesheet" />
   <link href="../VerticalAsset/plugins/datetimepicker/css/classic.date.css" rel="stylesheet" />


   
   <link rel="stylesheet" href="../VerticalAsset/plugins/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.min.css">
   <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">

   <link href="../VerticalAsset/plugins/Drag-And-Drop/dist/imageuploadify.min.css" rel="stylesheet" />

   <script src="../VerticalAsset/js/bootstrap.bundle.min.js"></script>
   <script src="../VerticalAsset/js/jquery.min.js"></script>
   <script src="../VerticalAsset/plugins/simplebar/js/simplebar.min.js"></script>
   <script src="../VerticalAsset/plugins/metismenu/js/metisMenu.min.js"></script>
   <script src="../VerticalAsset/plugins/perfect-scrollbar/js/perfect-scrollbar.js"></script>


   <script src="../VerticalAsset/plugins/datetimepicker/js/legacy.js"></script>
   <script src="../VerticalAsset/plugins/datetimepicker/js/picker.js"></script>
   <script src="../VerticalAsset/plugins/datetimepicker/js/picker.time.js"></script>
   <script src="../VerticalAsset/plugins/datetimepicker/js/picker.date.js"></script>
   <script src="../VerticalAsset/plugins/bootstrap-material-datetimepicker/js/moment.min.js"></script>
   <script src="../VerticalAsset/plugins/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker.min.js"></script>

   <script src="../VerticalAsset/plugins/select2/js/select2.min.js"></script>

 <%--  <script src="../VerticalAsset/plugins/datatable/js/jquery.dataTables.min.js"></script>
   <script src="../VerticalAsset/plugins/datatable/js/dataTables.bootstrap5.min.js"></script>--%>

   	<link href="../VerticalAsset/plugins/datatable/css/dataTables.bootstrap5.min.css" rel="stylesheet" />

   <link href="../VerticalAsset/Other/scroller.dataTables.min.css" rel="stylesheet" />
   <script src="../VerticalAsset/plugins/datatable/js/jquery.dataTables.min.js"></script>

<script src="../VerticalAsset/plugins/datatable/js/dataTables.bootstrap5.min.js"></script>

   <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.css" />
  <script src="https://cdn.jsdelivr.net/npm/sweetalert@2.1.2/dist/sweetalert.min.js"></script>


   <script src="../VerticalAsset/plugins/Drag-And-Drop/dist/imageuploadify.min.js"></script>


   <link media="screen" rel="stylesheet" type="text/css" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.css" />
   <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.0.1/js/toastr.js"></script>
   <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/fancybox/2.1.5/jquery.fancybox.min.css" media="screen">
   <script src="//cdnjs.cloudflare.com/ajax/libs/fancybox/2.1.5/jquery.fancybox.min.js"></script>


   <script src="../assets/jquery-ui.min.js"></script>
   <link href="../assets/jquery-ui.min.css" rel="stylesheet" />
   <script src="../CustomScript/_myCusGen_Func.js"></script>
   <script src="../CustomScript/_QuickDataAccess.js"></script>

   <script src="../VerticalAsset/js/app.js"></script>
      <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.3.0/jspdf.umd.min.js"></script>
   <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
    <!--Password show & hide js -->

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
    <!--app JS-->
    <script src="VerticalAsset/js/app.js"></script>

    <style>
        .centerimg {
            display: block;
            margin-left: auto;
            margin-right: auto;
            width: 50%;
        }
    </style>
</head>
<body style="padding-left:10px!important;padding-right:10px!important">

    <div class="wrapper">


        <form id="form1" runat="server">
               
            <div class="row" runat="server" visible="false">

                <div class="col-6">

                    <div class="form-group row">
                        <label for="GroupSelect" class="col-sm-4 col-form-label">Distribution Center:  </label>

                        <div class="col-sm-8">
                            <div class="input-group">
                                <asp:DropDownList CssClass="form-select form-select-sm mb-3 mySelect2 " runat="server" ID="ddlDistributionCenter"></asp:DropDownList>


                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
               
                <div class="col-md-12">
                    <div class="form-group row">
                        <label for="FromDate" class="col-sm-4 col-form-label">Month:  </label>

                        <div class="col-sm-8">
                            <asp:DropDownList runat="server" ID="ddlmonth" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>
                        </div>

                    </div>

                </div>
                <div class="col-4 d-none">
                    <div class="form-group row">
                        <label for="EmployeeIdSelect" class="col-sm-4 col-form-label">Employee:  </label>

                        <div class="col-sm-8">


                            <asp:DropDownList runat="server" ID="EmployeeIdSelect" name="EmployeeIdSelect" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>

                        </div>

                    </div>

                </div>
            </div>


            <div class="row">
                 
                <div class="col-md-12">
                    <div class="form-group row">
                        <label for="ToDate" class="col-sm-4 col-form-label">Year:  </label>

                        <div class="col-sm-8">
                            <asp:DropDownList runat="server" ID="ddlYear" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>

                        </div>

                    </div>

                </div>
                <div class="col-4 d-none">
                    <div class="form-group row">
                        <label for="UserRoleSelect" class="col-sm-4 col-form-label">User Role:  </label>

                        <div class="col-sm-8">


                            <asp:DropDownList runat="server" ID="UserRoleSelect" name="UserRoleSelect" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>
                        </div>

                    </div>

                </div>
            </div>


            <div class="row">

                
                <div class="col-md-12">
                    <div class="form-group row">
                        <label for="UserRoleSelect" class="col-sm-4 col-form-label">Approval Status:  </label>

                        <div class="col-sm-8">


                            <asp:DropDownList runat="server" ID="ApprovalStatusSelect" name="ApprovalStatusSelect" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>
                        </div>

                    </div>

                </div>
                <div class="col-4">
                </div>
            </div>

             

         <div class="row">
  <div class="col-12">
    <asp:LinkButton runat="server" ID="btnSearch"
        CssClass="btn btn-primary btn-sm w-100 d-block"
        OnClick="btnSearch_Click">
        <i class="fa fa-search-plus"></i>&nbsp; Search
    </asp:LinkButton>
  </div>
</div>


            <div style="padding-top: 5px;"></div>


            <asp:HiddenField ID="hfEmpTerrId" runat="server" />
            <asp:HiddenField ID="hfEmpAreaId" runat="server" />
            <asp:HiddenField ID="hfEmpRegionId" runat="server" />
            <asp:HiddenField ID="hfEmpGroupId" runat="server" />


            <div class="table-responsive" id="MainGradeDiv"  >

                <!-- Bootstrap grid container -->
                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
    DataKeyNames="TableId,TourPlanApprovalId,FromEmpId,ToEmpId,Step,RoleTypeId,ToRoleTypeId,MaxStep"
    CssClass="table table-borderless" GridLines="None" ShowHeader="False"
    OnRowCommand="loadGridView_RowCommand">

  <Columns>
    <asp:TemplateField>
      <ItemTemplate>
        <div class="col-12 col-md-6 col-lg-4 d-inline-block px-2 pb-3">
          <div class="card shadow-sm h-100 border-0 tp-card">
            <div class="card-body">
              <div class="d-flex justify-content-between align-items-start mb-2">
                <h6 class="card-title mb-0"><%# Eval("EmpName") %></h6>
                <span class='badge <%# GetStatusBadgeCss(Eval("ApprovalStatusWeb")) %>'>
                  <%# Eval("ApprovalStatusWeb") %>
                </span>
              </div>

              <p class="text-muted small mb-1"><strong>Employee ID:</strong> <%# Eval("EmpMasterCode") %></p>
              <p class="text-muted small mb-1"><strong>Designation:</strong> <%# Eval("DesigName") %></p>
              <p class="text-muted small mb-1"><strong>User Role:</strong> <%# Eval("RoleName") %></p>

              <div class="row small mb-2">
                <div class="col">
                  <strong>Year</strong><br /><%# Eval("YearValue") %>
                </div>
                <div class="col">
                  <strong>Month</strong><br /><%# Eval("MonthValue") %>
                </div>
              </div>

              <p class="small mb-2"><strong>Waiting For:</strong> <%# Eval("WaitingForRole") %></p>
              <p class="small mb-3"><strong>Remarks:</strong> <%# Eval("FinalSubmitRemarks") %></p>

              <asp:HiddenField runat="server" ID="hfTableId" Value='<%#Eval("TableId")%>' />
              <asp:HiddenField runat="server" ID="hfFromEmpId" Value='<%#Eval("FromEmpId")%>' />
              <asp:HiddenField runat="server" ID="hfToEmpId" Value='<%#Eval("ToEmpId")%>' />
              <asp:HiddenField runat="server" ID="hfStep" Value='<%#Eval("Step")%>' />
              <asp:HiddenField runat="server" ID="hfMaxStep" Value='<%#Eval("MaxUserStep")%>' />
              <asp:HiddenField runat="server" ID="hfRoleTypeId" Value='<%#Eval("RoleTypeId")%>' />
              <asp:HiddenField runat="server" ID="hfApprovalId" Value='<%#Eval("TourPlanApprovalId")%>' />
              <asp:HiddenField runat="server" ID="hfToRoleTypeId" Value='<%#Eval("ToRoleTypeId")%>' />
                     <asp:HiddenField runat="server" ID="hfCustomerMasterId" Value='<%#Eval("TableId")%>' />
                     <asp:HiddenField runat="server" ID="hfApprovalStatusWeb" Value='<%#Eval("ApprovalStatusWeb")%>' />
      <asp:HiddenField runat="server" ID="hfCustomerApprovalId" Value='<%#Eval("TourPlanApprovalId")%>' />

     

                           
            </div>

            <div class="card-footer bg-white border-0 pt-0 pb-3">
              <div class="d-flex gap-2 flex-wrap">
                <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn btn-success btn-sm"
                  CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData">
                  Go To Details >>
                </asp:LinkButton>
                    <asp:Label runat="server" ID="lbMsg" />
                <%--<asp:LinkButton ID="lbApprove" runat="server" CssClass='btn btn-info btn-sm <%# ShowApproveRejectCss(Eval("ApprovalStatusWeb")) %>'
                  CommandArgument="<%# Container.DataItemIndex %>" CommandName="ApproveData">
                  <i class='fa fa-check' aria-hidden='true'></i>
                </asp:LinkButton>

                <asp:LinkButton ID="lbReject" runat="server" CssClass='btn btn-danger btn-sm <%# ShowApproveRejectCss(Eval("ApprovalStatusWeb")) %>'
                  CommandArgument="<%# Container.DataItemIndex %>" CommandName="RejectData">
                  <i class='bx bx-x' aria-hidden='true'></i>
                </asp:LinkButton>--%>
              </div>
            </div>
          </div>
        </div>
      </ItemTemplate>
    </asp:TemplateField>
  </Columns>
</asp:GridView>


<%--                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="TableId,TourPlanApprovalId,FromEmpId,ToEmpId,Step,RoleTypeId,ToRoleTypeId,MaxStep" OnRowCommand="loadGridView_RowCommand"
                    CssClass="table table-striped table-bordered" OnPreRender="gv_DocumentUpload_PreRender">
                    <Columns>

                        <asp:TemplateField HeaderText="SL">
                            <ItemTemplate>
                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                        <asp:BoundField DataField="DesigName" HeaderText="Designation" />
                        <asp:BoundField DataField="RoleName" HeaderText="User Role" />
                        <asp:BoundField DataField="YearValue" HeaderText="Year" />
                        <asp:BoundField DataField="MonthValue" HeaderText="Month" />
                        <asp:BoundField DataField="FinalSubmitRemarks" HeaderText="Remarks" />


                        <asp:BoundField DataField="ApprovalStatusWeb" HeaderText="Approval Status" />
                        <asp:BoundField DataField="WaitingForRole" HeaderText="Waiting For" />





                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hfCustomerMasterId" Value='<%#Eval("TableId")%>' />
                                <asp:HiddenField runat="server" ID="hfFromEmpId" Value='<%#Eval("FromEmpId")%>' />
                                <asp:HiddenField runat="server" ID="hfToEmpId" Value='<%#Eval("ToEmpId")%>' />
                                <asp:HiddenField runat="server" ID="hfStep" Value='<%#Eval("Step")%>' />
                                <asp:HiddenField runat="server" ID="hfRoleTypeId" Value='<%#Eval("RoleTypeId")%>' />
                                <asp:HiddenField runat="server" ID="hfCustomerApprovalId" Value='<%#Eval("TourPlanApprovalId")%>' />

                                <asp:HiddenField runat="server" ID="hfToRoleTypeId" Value='<%#Eval("ToRoleTypeId")%>' />

                                <asp:Label runat="server" ID="lbMsg" />
                                <asp:LinkButton ID="lbEdit" runat="server" class="btn-success  btn-sm mb-1 mb-md-0"
                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData">Go To Approval</asp:LinkButton>

                                <asp:LinkButton ID="lbApprove" runat="server" class="btn-info  btn-sm mb-1 mb-md-0 d-none"
                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="ApproveData"><i class='fa fa-check' aria-hidden='true'></i> </asp:LinkButton>


                                <asp:LinkButton ID="lbReject" runat="server" class="btn-danger  btn-sm mb-1 mb-md-0 d-none"
                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="RejectData"> </i><i class='fadeIn animated bx bx-x' aria-hidden='true'></i> </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>--%>
            </div>

        </form>


    </div>
</body>
   
</html>
