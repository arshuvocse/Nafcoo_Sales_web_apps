<%@ Page Title="Work Schedule Approval List" Language="C#" AutoEventWireup="true" CodeFile="AttendanceApprovalListApps.aspx.cs" Inherits="AttendanceApprovalListApps" %>

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


    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@fancyapps/ui/dist/fancybox.css" />
<script src="https://cdn.jsdelivr.net/npm/@fancyapps/ui/dist/fancybox.umd.js"></script>

    <!--Password show & hide js -->

    <script>
        // global toastr options (tweak as you like)
        toastr.options = {
            "closeButton": true,
            "progressBar": true,
            "newestOnTop": true,
            "timeOut": "3000",
            "extendedTimeOut": "1500",
            "positionClass": "toast-top-right",
            "preventDuplicates": true
        };
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
                               <div class="col-1">
                                   </div>
<div class="col-5">
    <div class="form-group row">
        <label for="FromDate" class="col-sm-4 col-form-label">From Date:  </label>

        <div class="col-sm-8">
             <asp:TextBox  runat="server"  id="FromDate" type="date" class="form-control form-control-sm mb-3" autocomplete="off" placeholder="Select Date" ></asp:TextBox>
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
        </div>

    </div>

</div>
                          <div class="col-5">
      <div class="form-group row">
          <label for="ToDate" class="col-sm-4 col-form-label">To Date:  </label>

          <div class="col-sm-8">
               <asp:TextBox  runat="server"  id="ToDate" type="date" class="form-control form-control-sm mb-3"   autocomplete="off" placeholder="Select Date"></asp:TextBox>

          </div>

      </div>
</div>
</div>
            <div class="row" style="display:none">
               
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


            <div class="row"  style="display:none">
                 
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


            <div class="row"  style="display:none">

                
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
    DataKeyNames="TableId,ApprovalId,FromEmpId,ToEmpId,Step,RoleTypeId,ToRoleTypeId,MaxStep"
    CssClass="table table-borderless" GridLines="None" ShowHeader="False"
    OnRowCommand="loadGridView_RowCommand">

  <Columns>
    <asp:TemplateField>
      <ItemTemplate>
       <div class="col-12 col-md-6 col-lg-4 d-inline-block px-2 pb-3">
  <div class="card shadow-sm h-100 border-0 tp-card hover-lift">
    <div class="card-body p-3">
      <div class="d-flex align-items-start justify-content-between">
        <div class="d-flex align-items-center gap-2">
          <span class="badge text-bg-secondary fw-semibold">#<%# (Container.DataItemIndex + 1).ToString() %></span>
          <h6 class="card-title mb-0 text-truncate" title='<%# Eval("EmpName") %>'><%# Eval("EmpName") %></h6>
        </div>

        <span class='badge status-badge <%# GetStatusBadgeCss(Eval("ApprovalStatusWeb")) %>'>
          <%# Eval("ApprovalStatusWeb") %>
        </span>
      </div>

      <hr class="my-2" />

      <div class="d-flex align-items-center gap-3">
        <a href='<%# Eval("ImageString") %>' id="hpImg"  data-fancybox='gallery-<%# Eval("EmpMasterCode") %>'
 data-caption='Employee: <%# Eval("EmpName") %>' aria-label="Open photo">
          <img
            id="imgShow"
            runat="server"
            src='<%# Eval("ImageString") %>'
            alt="Employee photo"
            class="rounded-3 border img-thumb"
            loading="lazy" width="100" height="120" />
        </a>

 

        <ul class="list-unstyled mb-0 small text-muted meta-list">
          <li><span class="meta-label">Employee ID:</span> <span class="text-body-secondary"><%# Eval("EmpMasterCode") %></span></li>
          <li><span class="meta-label">Designation:</span> <span class="text-body-secondary"><%# Eval("DesigName") %></span></li>
          <li><span class="meta-label">User Role:</span> <span class="text-body-secondary"><%# Eval("RoleName") %></span></li>
          <li><span class="meta-label">Attendance Date:</span> <span class="text-body-secondary"><%# Eval("AttendanceDate") %></span></li>
          <li><span class="meta-label">In/Out Time:</span> <span class="text-body-secondary"><%# Eval("PunchInTime") %></span></li>
          <li><span class="meta-label">Waiting For:</span> <span class="text-body-secondary"><%# Eval("WaitingForRole") %></span></li>
          <li>       <a
   data-fancybox
   data-type="iframe"
   data-caption='Location of <%# Eval("EmpName") %> — <%# Eval("latlong") %>'
   href='<%# "https://www.google.com/maps?q=" + Eval("latlong") + "&output=embed" %>'
   class="btn btn-outline-secondary btn-sm">
  <i class="bx bx-location-plus fs-5 align-middle"></i>
  <span class="d-none d-sm-inline ms-1">Map</span>
</a>       <a class="btn btn-outline-secondary btn-sm"
      href='<%# Eval("ImageString") %>'
       data-fancybox='gallery-<%# Eval("EmpMasterCode") %>'
data-caption='Employee: <%# Eval("EmpName") %>'
      title="View photo">
     <i class="bx bx-image fs-5 align-middle"></i>
     <span class="d-none d-sm-inline ms-1">Photo</span>
   </a> </li>
        </ul>

             
      </div>

   
          <asp:HiddenField runat="server" ID="hfExpenseClaimID" Value='<%#Eval("AttendanceId")%>' />
  <asp:HiddenField runat="server" ID="hfCustomerMasterId" Value='<%#Eval("TableId")%>' />
  
  <asp:HiddenField runat="server" ID="hfCustomerApprovalId" Value='<%#Eval("ApprovalId")%>' /> 
    <asp:HiddenField runat="server" ID="HiddenField5" Value='<%#Eval("ToRoleTypeId")%>' />
      <asp:HiddenField runat="server" ID="hfTableId" Value='<%# Eval("TableId") %>' />
      <asp:HiddenField runat="server" ID="hfFromEmpId" Value='<%# Eval("FromEmpId") %>' />
      <asp:HiddenField runat="server" ID="hfToEmpId" Value='<%# Eval("ToEmpId") %>' />
      <asp:HiddenField runat="server" ID="hfStep" Value='<%# Eval("Step") %>' />
      <asp:HiddenField runat="server" ID="hfMaxStep" Value='<%# Eval("MaxStep") %>' />
      <asp:HiddenField runat="server" ID="hfRoleTypeId" Value='<%# Eval("RoleTypeId") %>' />
      <asp:HiddenField runat="server" ID="hfApprovalId" Value='<%# Eval("ApprovalId") %>' />
      <asp:HiddenField runat="server" ID="hfToRoleTypeId" Value='<%# Eval("ToRoleTypeId") %>' />
      <asp:HiddenField runat="server" ID="hfApprovalStatusWeb" Value='<%# Eval("ApprovalStatusWeb") %>' />
    </div>

    <div class="card-footer bg-white border-0 pt-0 pb-3">
      <div class="d-flex flex-wrap gap-2">
       <asp:LinkButton ID="lbApprove" runat="server"
  CssClass='btn btn-success btn-sm d-inline-flex align-items-center <%# ShowApproveRejectCss(Eval("ApprovalStatusWeb")) %>'
  CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CommandName="ApproveData">
  <i class='fa fa-check me-1'></i><span>Approve</span>
</asp:LinkButton>

<asp:LinkButton ID="lbReject" runat="server"
  CssClass='btn btn-danger btn-sm d-inline-flex align-items-center <%# ShowApproveRejectCss(Eval("ApprovalStatusWeb")) %>'
  CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CommandName="RejectData">
  <i class='bx bx-x me-1'></i><span>Reject</span>
</asp:LinkButton>


        <asp:Label runat="server" ID="lbMsg" CssClass="ms-2 small text-muted" />
      </div>
    </div>
  </div>
</div>

      </ItemTemplate>
    </asp:TemplateField>
  </Columns>
</asp:GridView>

<style>
  .tp-card { transition: transform .18s ease, box-shadow .18s ease; }
  .hover-lift:hover { transform: translateY(-2px); box-shadow: 0 .5rem 1rem rgba(0,0,0,.08)!important; }

  .img-thumb { width:100px; height:120px; object-fit:cover; }
  .meta-list .meta-label { min-width: 115px; display:inline-block; color:#6b7280; } /* slate-500 */
  .status-badge { white-space:nowrap; }

  /* Optional: compact card on very small screens */
  @media (max-width: 400px){
    .img-thumb{ width:72px; height:72px; }
    .meta-list .meta-label{ min-width: 100px; }
  }
</style>




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
