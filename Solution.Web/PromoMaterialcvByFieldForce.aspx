<%@ Page Title="📦 Promo Material Receive" Language="C#" AutoEventWireup="true" CodeFile="PromoMaterialcvByFieldForce.aspx.cs" Inherits="PromoMaterialcvByFieldForce" %>

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

    <div class="wrapper" >
         
        
                      <form id="form1" runat="server">
                       
                             <div class="text-center my-4 p-3 bg-primary text-white rounded shadow-sm" style="font-size: 24px; font-weight: bold;">
    📦 Promo Material Receive     
</div>

                        <div class="text-center">
    <asp:Label ID="lblMessage" runat="server" CssClass="alert d-inline-block" Visible="false"></asp:Label>
</div>



                      <asp:Repeater ID="RepeaterPromo" runat="server" OnItemCommand="RepeaterPromo_ItemCommand">
    <ItemTemplate>
        <div   style="height: 6px; background-color: #007bff; border-top-left-radius: .25rem; border-top-right-radius: .25rem;"></div>
  
        <div class="card mb-3 p-3 border-primary" >
            <div class="card-body">
                <p><strong>📦 Challan Code:</strong> <%# Eval("PromoChallanCode") %></p>
                <p><strong>🏢 Distribution Center:</strong> <%# Eval("ComUnitName") %></p>
                <p><strong>🗓️ Date:</strong> <%# Eval("ChallanDate", "{0:dd-MMM-yyyy}") %> | <strong>Year:</strong> <%# Eval("Year") %> | <strong>Month:</strong> <%# Eval("Month") %></p>
                <p><strong>📊 Promo Group:</strong> <%# Eval("PromoGroupName") %></p>
                <p><strong>🧪 SQ Name:</strong> <%# Eval("ProductSQName") %></p>
                <p><strong>🎁 Promo Product:</strong> <%# Eval("PromoProductName") %></p>
                <p><strong>🗺️ Territory:</strong> <%# Eval("TerritoryName") %></p>
                <p><strong>👨‍⚕️ MIO:</strong> <%# Eval("MioName") %></p>
                <p><strong>🔢 Quantity:</strong> <%# Eval("Qty") %></p>

                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control mb-2" placeholder="📝 Enter remarks..."></asp:TextBox>
            <div class="d-flex justify-content-center mt-2">
    <asp:Button ID="btnReceive" runat="server" CommandName="Receive" Text="✅ Receive"
        CommandArgument='<%# Eval("PromoChallanId") %>'    CssClass="btn btn-outline-success btn-sm mx-1" />
        
    <asp:Button ID="btnReject" runat="server" CommandName="Reject" Text="❌ Reject"
        CommandArgument='<%# Eval("PromoChallanId") %>'  CssClass="btn btn-outline-danger btn-sm mx-1" />
</div>

            </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>

                              
                               
                           
                          </form>
                   
              
    </div>


</body>
   
</html>
