<%@ Page Title="Work Schedule Approval" Language="C#" AutoEventWireup="true" CodeFile="TourPlanDetailViewForApp.aspx.cs" Inherits="TourPlanDetailViewForApp" %>

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
       <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.css" />
   <script src="https://cdn.jsdelivr.net/npm/sweetalert@2.1.2/dist/sweetalert.min.js"></script>

   <script src="../assets/jquery-ui.min.js"></script>
   <link href="../assets/jquery-ui.min.css" rel="stylesheet" />
   <script src="../CustomScript/_myCusGen_Func.js"></script>
   <script src="../CustomScript/_QuickDataAccess.js"></script>

   <script src="../VerticalAsset/js/app.js"></script>
      <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.3.0/jspdf.umd.min.js"></script>
   <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
    <!--Password show & hide js -->

    <script type="text/javascript"> 

            function showpop6(msg, title) {
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": true,
                    "positionClass": "toast-bottom-right",
                    "preventDuplicates": true,
                    "onclick": null,
                    "showDuration": "300",
                    "hideDuration": "1000",
                    "timeOut": "3000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
                // toastr['success'](msg, title);
                var d = Date();
                toastr.error(msg, title);
                return false;
            }
        function showpop5(msg, title) {
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": true,
                "positionClass": "toast-bottom-right",
                "preventDuplicates": true,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "3000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
            // toastr['success'](msg, title);
            var d = Date();
            toastr.success(msg, title);
            return false;
        }
        //$(document).ready(function () {
        //    debugger;
        //    var a = 'aa';
        //    var st = sessionStorage.data;
        //    if (st != null) {
        //        //$("#mCSB_1_container").css(st);
        //        $("#mCSB_1_container").attr("style", st);
        //    }


        //    setInterval(function () {

        //        var styl = $("#mCSB_1_container").attr("style");
        //        sessionStorage.data = styl;
        //    }, 1);
        //});

        function successalert(msg1, type, url) {


            swal({
                icon: 'success',
                title: 'Congratulations!',
                text: msg1,
                type: 'success',
                showClass: {
                    popup: 'animate__animated animate__fadeInDown'
                },
                hideClass: {
                    popup: 'animate__animated animate__fadeOutUp'
                }
            }).then((willDelete) => {
                if (type == 'Success') {


                    window.location.href = url; //replace ID value-->
                }
                else {
                    alert("Operation Faild!!!")
                }

                //swal({
                //    title: "Congratulations!",
                //    text: msg2,
                //    type: 'success'
                //}).then((willDelete) => {
                //    swal({
                //        title: "Congratulations!",
                //        text: msg3,
                //        type: 'success'
                //    })
                //})
            })
        }


        function ShowSuccesalert(msg1, type) {


            swal({
                icon: 'success',
                title: 'Congratulations!',
                text: msg1,

                type: 'success'
            }).then((willDelete) => {
                if (type == 'success') {



                }
                else {

                }

                //swal({
                //    title: "Congratulations!",
                //    text: msg2,
                //    type: 'success'
                //}).then((willDelete) => {
                //    swal({
                //        title: "Congratulations!",
                //        text: msg3,
                //        type: 'success'
                //    })
                //})
            })
        }

        function faildalert(msg1, type) {


            swal({
                icon: 'error',
                title: '',
                text: msg1,

                type: 'faild'
            }).then((willDelete) => {
                if (type == 'Faild') {



                }
                else {

                }

                //swal({
                //    title: "Congratulations!",
                //    text: msg2,
                //    type: 'success'
                //}).then((willDelete) => {
                //    swal({
                //        title: "Congratulations!",
                //        text: msg3,
                //        type: 'success'
                //    })
                //})
            })
        }


        function sweetAlertConfirm_Submit(btnSave) {
            if (btnSave.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnSave.dataset.confirmed = false;
                return true;
            } else {
                // Ask the user to confirm/cancel the action
                event.preventDefault();
                swal({
                    title: 'Are You Sure ?',
                    text: 'You are about to submit the data!',
                    type: 'info',
                    icon: 'warning',
                    buttons: {
                        yes: {
                            text: "Confirm",
                            value: "yes"
                        },
                        no: {
                            text: "Cancel",
                            value: "no",
                            className: "",
                            closeModal: true,
                        }
                    }
                }
                )

                    .then((value) => {
                        if (value === "yes") {
                            btnSave.dataset.confirmed = true;
                            // Trigger button click programmatically
                            btnSave.click();
                        }
                        return false;
                        // Set data-confirmed attribute to indicate that the action was confirmed

                    }).catch(function (reason) {
                        // The action was canceled by the user
                    });

            }
        }


        function sweetAlertConfirm_Update(btnUpdate) {
            if (btnUpdate.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnUpdate.dataset.confirmed = false;
                return true;
            } else {
                // Ask the user to confirm/cancel the action
                event.preventDefault();
                swal({
                    title: 'Are You Sure ?',
                    text: 'You are about to submit the data!',
                    type: 'green',
                    icon: 'warning',
                    buttons: {
                        yes: {
                            text: "Confirm",
                            value: "yes"
                        },
                        no: {
                            text: "Cancel",
                            value: "no",
                            className: "",
                            closeModal: true,
                        }
                    }
                }
                )

                    .then((value) => {
                        if (value === "yes") {
                            btnUpdate.dataset.confirmed = true;
                            // Trigger button click programmatically
                            btnUpdate.click();
                        }
                        return false;
                        // Set data-confirmed attribute to indicate that the action was confirmed

                    }).catch(function (reason) {
                        // The action was canceled by the user
                    });

            }
        }





        function sweetAlertConfirm_Delete(btnUpdate) {
            if (btnUpdate.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnUpdate.dataset.confirmed = false;
                return true;
            } else {
                // Ask the user to confirm/cancel the action
                event.preventDefault();
                swal({
                    title: 'Are You Sure ?',
                    text: 'You are about to Remove the data!',
                    type: 'green',
                    icon: 'warning',
                    buttons: {
                        yes: {
                            text: "Confirm",
                            value: "yes"
                        },
                        no: {
                            text: "Cancel",
                            value: "no",
                            className: "",
                            closeModal: true,
                        }
                    }
                }
                )

                    .then((value) => {
                        if (value === "yes") {
                            btnUpdate.dataset.confirmed = true;
                            // Trigger button click programmatically
                            btnUpdate.click();
                        }
                        return false;
                        // Set data-confirmed attribute to indicate that the action was confirmed

                    }).catch(function (reason) {
                        // The action was canceled by the user
                    });

            }
        }
  

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
<body style="padding-left:2px!important;padding-right:2px!important">
    <form id="form1" runat="server">
            
    <div style="margin-bottom:20px;" >

        <div class="row">
           
                 
                        <div class="">
 

                            <div class="col-md-12">

                                <div class="col">
                                    <div class="card radius-5 bg-success bg-gradient">
                                        <div class="card-body">
                                            <div class="text-center">
                                                <div>
                                                    <h5 class="my-1 text-white">Employee:
                                                            <label style="font-size: 16px;" id="lblEmpName"></label>
                                                    </h5>
                                                    <h5 class="my-1 text-white">Designation:
                                                            <label style="font-size: 16px;" id="lblEmpDgs"></label>
                                                    </h5>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4" style="display:none">

                                <div class="table-responsive" id="MainGradeDsiv">
                                    <table id="dtTb" class="table table-striped table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th class="text-center"># SL No</th>
                                                <th>Tour Type  </th>
                                                <th>Count  </th>

                                            </tr>
                                        </thead>
                                        <tbody id="dtTableBody" class="txtCenter">
                                        </tbody>
                                    </table>

                                </div>

                            </div>

                        </div>

             

                        <div class="table-responsive" id="tableDetail" style="position: relative; min-height: 360px !important;">
                        </div>
                        <div class="table-responsive " id="MainGradeDiv">
                        </div>
                   
               
           
        </div>

       


    <%--For Approve & Reject Button--%>
<style>
  :root{
    --tp-border:#e5e7eb; --tp-text:#111827; --tp-sub:#6b7280; --tp-bg:#fff; --tp-soft:#f9fafb;
    --morn:#2563eb; /* blue accent */
    --eve:#d97706;  /* amber accent */
    --tp-shadow:0 12px 28px rgba(2,6,23,.10), 0 4px 10px rgba(2,6,23,.06);
    --tp-radius:14px;
  }

  /* Card list */
  .tp-cards{
    max-width: 980px; margin:0 auto;
    display: grid; grid-template-columns: 1fr; gap: 18px;
  }

  /* Single card */
  .tp-card{
    background:var(--tp-bg); border:1px solid var(--tp-border);
    border-radius:var(--tp-radius); box-shadow:var(--tp-shadow); overflow:hidden;
  }

  .tp-card-head{
    padding:14px 16px; background:#fff; border-bottom:1px solid var(--tp-border);
    display:flex; justify-content:space-between; align-items:baseline;
  }
  .tp-date-main{ font-weight:700; font:24px; color:#0f172a; }
  .tp-date-sub{ color:var(--tp-sub); font-size:18px; background:var(--tp-soft); padding:2px 10px; border-radius:999px; }

  .tp-section{ padding:14px 16px; }
  .tp-sec-header{
    margin:0 0 10px; padding:0 0 0 10px; font-weight:700; font-size:14px;
    text-transform:uppercase; letter-spacing:.02em;
    border-left:4px solid #cbd5e1; color:#0f172a;
  }
  .tp-sec-header.tp-morn{ border-left-color:var(--morn); color:#0b2e73; }
  .tp-sec-header.tp-eve{  border-left-color:var(--eve);  color:#5a3403; }

  /* 3-col grid */
  .tp-grid{ display:grid; grid-template-columns: repeat(3, minmax(0,1fr)); gap:10px; }
  .tp-grid-head{ display:none; } /* mobile-first: hidden */
  .tp-colhead{
    font-weight:600; font-size:12px; color:#475569; text-transform:uppercase; letter-spacing:.04em;
    padding:2px 2px 6px;
  }

  .tp-grid-body{ margin-top:2px; }
  .tp-cell{
    background:var(--tp-soft); border:1px solid var(--tp-border); border-radius:10px;
    padding:10px 12px; display:flex; justify-content:space-between; gap:8px;
  }
  /* label (mobile) */
  .tp-cell::before{ content: attr(data-label); font-weight:600; color:#111827; }
  /* value span */
  .tp-cell > span{ color:#111827; font-weight:600; white-space:nowrap; overflow:hidden; text-overflow:ellipsis; }

  /* Accent bars */
  .tp-cell.tp-morn{ box-shadow: inset 3px 0 0 0 var(--morn); }
  .tp-cell.tp-eve{  box-shadow: inset 3px 0 0 0 var(--eve); }

  .tp-empty{
    border:1px dashed var(--tp-border); border-radius:var(--tp-radius);
    padding:20px; text-align:center; color:var(--tp-sub); background:#fff; max-width:980px; margin:0 auto;
  }

  /* Desktop tweaks */
  @media (min-width: 900px){
    .tp-grid-head{ display:grid; grid-template-columns: repeat(3, minmax(0,1fr)); gap:10px; margin-top:4px; }
    .tp-cell::before{ display:none; }         /* desktop-এ হেডার দেখাব */
    .tp-cell{ display:block; padding:12px; }  /* value একা থাকবে */
  }
  /* Mobile: show full text instead of ... */
/* === Mobile: 3 columns, label + value in one line === */
/* Mobile: Tour Type (1 line), then Territory, then Market */
@media (max-width: 640px){
  /* হেডার লুকাও */
  .tp-grid-head{ display:none !important; }

  /* তিনটা আইটেমকে স্ট্যাক করো */
  .tp-grid-body{
    display:block !important;          /* grid off */
  }

  /* প্রতিটি আইটেম = এক লাইন (label + value) */
  .tp-cell{
    display:flex !important;
    align-items:center;
    justify-content:space-between;
    gap:10px;
    padding:10px 12px;
    margin-bottom:8px;                 /* লাইন স্পেসিং */
    border-radius:10px;
  }
  .tp-cell:last-child{ margin-bottom:0; }

  /* label */
  .tp-cell::before{
    display:inline !important;
    margin:0;                          /* একই লাইনে থাকবে */
    font-weight:600;
    font-size:13px;
  }

  /* value */
  .tp-cell > span{
    display:inline !important;
    white-space:normal !important;     /* লম্বা হলে ভাঙবে */
    overflow:visible !important;
    text-overflow:clip !important;
    overflow-wrap:anywhere;
  }
}


/* চাইলে একদম ছোট স্ক্রিনে ২ কলামে নামাতে পারো (ঐচ্ছিক) */
@media (max-width: 400px){
  .tp-grid-body{
    grid-template-columns: repeat(2, minmax(0,1fr)) !important;
  }
}


  /* Subtle hover (desktop) */
  @media (hover:hover){ .tp-card:hover{ transform: translateY(-1px); transition: transform .2s; } }
</style>

 

    <div class="row" style="margin:10px">
        
            <div class="row pb-5" style="text-align:center">
                

                <div class="col-md-12" style="align-content: center">
<asp:Label 
    runat="server" 
    ID="warnToast" 
    >
   
</asp:Label>


                    
<asp:LinkButton 
    runat="server" 
    ID="btnApprove" 
    CssClass="btn btn-success btn-lg fw-bold me-3 px-4 py-2 text-shadow w-100 d-block" 
    OnClick="btnApprove_Click">
    ✅ Approve
</asp:LinkButton>






                </div>
                
                <div class="col-md-12" style="margin-top:5px;align-content: center">
                <asp:LinkButton 
    runat="server" 
    ID="btnReject" 
    CssClass="btn btn-danger btn-lg fw-bold px-4 py-2 text-shadow w-100 d-block" 
    OnClick="btnReject_Click">
    ❌ Dis-Approve
</asp:LinkButton>
                 
            </div>
            </div>
            <div class="row d-none">
                <div class="text-center my-4 p-3 bg-primary text-white rounded shadow-sm" style="font-size: 24px; font-weight: bold;">
               Work Schedule Approval List     
            </div>
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
                <div class="col-4">
                </div>
                <div class="col-4">
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
                <div class="col-4">
                </div>
                <div class="col-4">
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

                <div class="col-4">
                </div>
                <div class="col-4">
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


            <br />

            <div class="row">
                <div class="col-md-5">
                </div>
                <div class="col-md-4" style="align-content: center">

                    <asp:LinkButton runat="server" ID="btnSearch" class="btn btn-primary btn-sm " OnClick="btnSearch_Click">  <i class="fa fa-search-plus"></i>&nbsp; Search</asp:LinkButton>


                    <asp:LinkButton runat="server" class="btn btn-warning  btn-sm" ID="resetBtn" OnClick="resetBtn_Click"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>

                </div>
            </div>
            <div style="padding-top: 10px;"></div>


            <asp:HiddenField ID="hfEmpTerrId" runat="server" />
            <asp:HiddenField ID="hfEmpAreaId" runat="server" />
            <asp:HiddenField ID="hfEmpRegionId" runat="server" />
            <asp:HiddenField ID="hfEmpGroupId" runat="server" />


            <div class="table-responsive" id="MainGradeDiv2" style="height: 600px">



                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
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
                                    <asp:HiddenField runat="server" ID="hfApprovalStatusWeb" Value='<%#Eval("ApprovalStatusWeb")%>' />
                                <asp:HiddenField runat="server" ID="hfCustomerMasterId" Value='<%#Eval("TableId")%>' />
                                <asp:HiddenField runat="server" ID="hfFromEmpId" Value='<%#Eval("FromEmpId")%>' />
                                <asp:HiddenField runat="server" ID="hfToEmpId" Value='<%#Eval("ToEmpId")%>' />
                                <asp:HiddenField runat="server" ID="hfStep" Value='<%#Eval("Step")%>' />
                                <asp:HiddenField runat="server" ID="hfRoleTypeId" Value='<%#Eval("RoleTypeId")%>' />
                                <asp:HiddenField runat="server" ID="hfCustomerApprovalId" Value='<%#Eval("TourPlanApprovalId")%>' />
                                   
              <asp:HiddenField runat="server" ID="hfMaxStep" Value='<%#Eval("MaxUserStep")%>' />
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
                </asp:GridView>
            </div>
            </div>

     
    </div>


    <input id="masterId" value="0" style="display: none" />
    <input id="Month" value="0" style="display: none" />
    <input id="year" value="0" style="display: none" />
    <input id="empId" value="0" style="display: none" />
            </div>
        </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.17.0/xlsx.full.min.js"></script>

    <script>

        $(document).ready(function () {
            document.getElementById("dtTb").style.display = "none";
            $("#exportButton").click(function () {
                var table = document.getElementById('exportTable');
                var wb = XLSX.utils.table_to_book(table, { sheet: "Sheet1" });
                XLSX.writeFile(wb, "myFileName.xlsx");
            });
        });
        function dayNameFromDate(dstr) {
            // Best-effort weekday (optional; use if you want to show day)
            var d = new Date(dstr);
            if (isNaN(d.getTime())) return '';
            return d.toLocaleDateString('en-GB', { weekday: 'short' }); // e.g., Sat
        }
        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }

        function un(o) {
            return o != null ? o : '';
        }

        $(function () {
            var masterid = getUrlVars()["id"];
            if (masterid) {
                $("#masterId").val(getUrlVars()["id"]);
                GetDegree(masterid);
            }
        });

        function safeParsePayload(raw) {
            // ASP.NET PageMethod cases: {d:"[...]"} | {d:[...]} | [...] | "[]"
            if (!raw) return [];
            var x = raw;
            if (typeof x === 'string') {
                try { x = JSON.parse(x); } catch (_) { return []; }
            }
            if (x && typeof x === 'object' && x.hasOwnProperty('d')) {
                var d = x.d;
                if (typeof d === 'string') {
                    try { return JSON.parse(d) || []; } catch (_) { return []; }
                }
                return Array.isArray(d) ? d : [];
            }
            return Array.isArray(x) ? x : [];
        }
        function buildCards(rows) {
            if (!rows.length) return "<div class='tp-empty'>No data found</div>";

            var html = "<div class='tp-cards'>";
            for (var j = 0; j < rows.length; j++) {
                var r = rows[j];
                var dateTxt = showVal(r.TourPlanDate);
                var dayTxt = dayNameFromDate(dateTxt);

                html += "<div class='tp-card'>";

                // header
                html += "<div class='tp-card-head'>";
                html += "<div class='tp-date-main'>" + dateTxt + "</div>";
                html += "<div class='tp-date-sub'>" + (dayTxt || '') + "</div>";
                html += "</div>";

                // Morning
                html += "<div class='tp-section'>";
                html += "<div class='tp-sec-header tp-morn'>Morning</div>";
                html += "<div class='tp-grid tp-grid-head'>";
                html += "<div class='tp-colhead'>Tour Type</div>";
                html += "<div class='tp-colhead'>Terr.</div>";
                html += "<div class='tp-colhead'>Market</div>";
                html += "</div>";
                html += "<div class='tp-grid tp-grid-body'>";
                html += "<div class='tp-cell tp-morn' data-label='Tour Type'><span>" + showVal(r.MorTourType) + "</span></div>";
                html += "<div class='tp-cell tp-morn' data-label='Territory'><span>" + showVal(r.MorTerritory) + "</span></div>";
                html += "<div class='tp-cell tp-morn' data-label='Market'><span>" + showVal(r.MorMarket) + "</span></div>";
                html += "</div>";
                html += "</div>";

                // Evening
                html += "<div class='tp-section'>";
                html += "<div class='tp-sec-header tp-eve'>Evening</div>";
                html += "<div class='tp-grid tp-grid-head'>";
                html += "<div class='tp-colhead'>Tour Type</div>";
                html += "<div class='tp-colhead'>Terr. </div>";
                html += "<div class='tp-colhead'>Market</div>";
                html += "</div>";
                html += "<div class='tp-grid tp-grid-body'>";
                html += "<div class='tp-cell tp-eve' data-label='Tour Type'><span>" + showVal(r.EveTourType) + "</span></div>";
                html += "<div class='tp-cell tp-eve' data-label='Territory'><span>" + showVal(r.EveTerritory) + "</span></div>";
                html += "<div class='tp-cell tp-eve' data-label='Market'><span>" + showVal(r.EveMarket) + "</span></div>";
                html += "</div>";
                html += "</div>";

                html += "</div>";
            }
            html += "</div>";
            return html;
        }

        // ---------- main ----------
        function GetDegree(id) {
            var urlpath = 'TourPlanDetailViewForApp.aspx/GetTourPlanDetailsViewDatabyID';

            $.ajax({
                url: urlpath,
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                async: true,

                beforeSend: function () {
                    $('#tableDetail').html("<div class='tp-empty'>Loading...</div>");
                },

                success: function (data) {
                    var html = "";
                    var empId = null, mVal = null, yVal = null;

                    try {
                        $('#tabH').show();

                        // parse
                        var arr = safeParsePayload(data);

                        if (arr.length) {
                            // header
                            $('#lblEmpName').text(un(arr[0].EmpName));
                            $('#lblEmpDgs').text(un(arr[0].DesigName));
                            empId = un(arr[0].EmpInfoId);
                            mVal = un(arr[0].MonthValue);
                            yVal = un(arr[0].YearValue);
                        } else {
                            $('#lblEmpName').text(''); $('#lblEmpDgs').text('');
                        }

                        // group by date
                        var map = new Map();
                        for (var i = 0; i < arr.length; i++) {
                            var r = arr[i], d = un(r.TourPlanDate);
                            if (!map.has(d)) {
                                map.set(d, {
                                    TourPlanDate: d,
                                    MorTourType: '', MorTerritory: '', MorMarket: '',
                                    EveTourType: '', EveTerritory: '', EveMarket: ''
                                });
                            }
                            var it = map.get(d);
                            it.MorTourType = un(r.MorTourType) || it.MorTourType;
                            it.MorTerritory = un(r.MorTerritory) || it.MorTerritory;
                            it.MorMarket = un(r.MorMarket) || it.MorMarket;
                            it.EveTourType = un(r.EveTourType) || it.EveTourType;
                            it.EveTerritory = un(r.EveTerritory) || it.EveTerritory;
                            it.EveMarket = un(r.EveMarket) || it.EveMarket;
                        }
                        var rows = Array.from(map.values());

                        // render
                        html = buildCards(rows);
                    } catch (ex) {
                        console.error('Render error:', ex);
                        html = "<div class='tp-empty'>Could not render data.</div>";
                    } finally {
                        // Always put something on the page so "Loading..." না থাকে
                        $('#tableDetail').html(html);

                        // Dependent call — এটা ফেল করলেও কার্ড থাকবে
                        try {
                            if (empId && mVal && yVal && typeof GetStationType === 'function') {
                                GetStationType(empId, mVal, yVal);
                            }
                        } catch (e) {
                            console.warn('GetStationType failed:', e);
                        }
                    }
                },

                error: function (xhr, status, err) {
                    console.error('AJAX error:', status, err, xhr && xhr.responseText);
                    var msg = "Request failed";
                    if (xhr && xhr.status) msg += " (" + xhr.status + ")";
                    $('#tableDetail').html("<div class='tp-empty'>" + msg + "</div>");
                },

                complete: function () { /* no-op */ }
            });
        }
        function showVal(v) { v = un(v); return v && v.trim() ? v : '—'; }
        function GetStationType(empId, Month, year) {
            var urlpath = 'TourPlanDetailsView.aspx/Get_TourPlanBalance';
            $.ajax({
                url: urlpath,
                dataType: 'json',
                data: JSON.stringify({
                    "empId": empId,
                    "Month": Month,
                    "year": year


                }),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                async: true,
                beforeSend: function () {
                },
                success: function (data) {




                    $('#tabH').show();

                    var row = "";
                    $('#dtTableBody').html("");

                    var result = JSON.parse(data.d);

                    console.log("result:", result);
                    for (var i = 0; i < result.length; i++) {

                        row += "<tr>";
                        row += "<td  >" + (i + 1) + "</td>";
                        row += "<td>" + (result[i].StationTypeName) + "</td>";
                        row += "<td>" + un(result[i].Balance) + "</td>";
                        row += "</tr>";

                    }

                    $('#dtTableBody').html(row);


                },
                complete: function () {

                }
            });
        }

    </script>
</body>
   
</html>
