<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeRecords.aspx.cs" Inherits="MasterSetup_UI_EmployeeRecords" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i> Employee List </div>  

                <div class="ms-auto">
                    <div class="btn-group">
                        <a href="EmployeeSetup.aspx" class="btn btn-sm btn-outline-info "><i class="fa fa-plus" aria-hidden="true"></i>New Entry</a>
                    </div>
                </div>
            </div>
            <!--end breadcrumb-->
            <div class="row">
                <div class="col">

                    <div class="card border-top border-0 border-4 border-success">
                        <div class="card-body">
                     
                             
                            <div class="table-responsive" id="MainGradeDiv">

                                
                            <table id="dtTble"  class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>SL</th>

                                        <th>Employee Code</th>
                                        <th>Employee Name</th>
                                        <th>Role</th>
                                       <%-- <th>Father's Name</th>
                                        <th>Mother's Name</th>
                                        <th>DOB</th>


                                        <th>Present Address</th>
                                        <th>Permanent Address</th>
                                        <th>Gender</th>
                                        <th>Religion</th>
                                        <th>Nationality</th>

                                        <th>Blood Group</th>
                                        <th>Marital Status</th>
                                        <th>NID No</th>--%>
                                        <th>Status</th>
                                       
                                        <th>Employee Information</th>
                                        <th>Login Credential </th>
                                        <%--<th>Market Access </th>--%>
                                         <th>View</th>
                                    </tr>
                                </thead>
                                <tbody id="dtTableBody">
                                </tbody>
                            </table>
 
                            </div>


                                    
                                
                                </div>
                              
                                </div>
                                </div>
                                </div>
                                </div>
                                </div>
       <div id="coverScreen" class="divWaitingJquery ">
        <img src="../images/Spinner.gif" style="width:180px" class="position-set" />
                </div>


      <script>

        function un(o) {
            return o != null ? o : '';
        }
        $(function () {

            GetPrescription();
    });

        function GetPrescription() {

            var urlpath = 'EmployeeRecords.aspx/GetEmployeeInformationList';
            $.ajax({
                url: urlpath,
                dataType: 'json',
                type: "POST", contentType: "application/json; charset=utf-8",
                async: true,
                beforeSend: function () {
                    $("#coverScreen").show();
                },
                success: function(data) {
                    data = data.d;
                    $('#tabH').show();
                    var result = JSON.parse(data);
                    var row = "";
                    $('#dtTableBody').html("");
                    for (var i = 0; i < result.length; i++) {
                        row += "<tr>";
                        row += "<td>" + (i + 1) + "</td>";
                        row += "<td>" + un(result[i].EmpMasterCode) + "</td>";
                        row += "<td>" + un(result[i].EmpName) + "</td>";
                        row += "<td>" + un(result[i].RoleName) + "</td>";
                        //row += "<td>" + un(result[i].FatherName) + "</td>";
                        //row += "<td>" + un(result[i].MotherName) + "</td>";
                        //row += "<td>" + un(result[i].EmpDateOfBirth) + "</td>";
                        //row += "<td>" + un(result[i].AddressPresent) + "</td>";
                        //row += "<td>" + un(result[i].AddressPermanent) + "</td>";
                        //row += "<td>" + un(result[i].Gender) + "</td>";
                        //row += "<td>" + un(result[i].Religion) + "</td>";
                        //row += "<td>" + un(result[i].Nationality) + "</td>";
                        //row += "<td>" + un(result[i].BloodGroup) + "</td>";
                        //row += "<td>" + un(result[i].MaritalStatus) + "</td>";
                        //row += "<td>" + un(result[i].NationalIdNo) + "</td>";
                        if (result[i].EmployeeStatus == 'Active') {
                            row += "<td><span class='badge bg-success'>Active</span></td>";
                        } else {
                            row += "<td><span class='badge bg-warning'>Inactive</span></td>";
                        }
                  

                        row += "<td><button class='btn-outline-warning     btn-xs mb-1 mb-md-0' type='button' onclick='EmpeditClick(" + result[i].EmpInfoId + ")'><i class='bx bxs-edit' aria-hidden='true'></i></button></td>";

                        row += "<td><button class='btn-outline-warning     btn-xs mb-1 mb-md-0' type='button' onclick='UsereditClick(" + result[i].EmpInfoId + ")'><i class='bx bxs-edit' aria-hidden='true'></i></button></td>";

                        //if (result[i].RoleType == 'MIO') {
                        //    row += "<td><button   class='btn-outline-warning     btn-xs mb-1 mb-md-0' type='button' onclick='MIOeditClick(" + result[i].EmpInfoId + ")'><i class='bx bxs-edit' aria-hidden='true'></i></button></td>";
                        //}
                        //else if (result[i].RoleType == 'AM') {
                        //    row += "<td><button   class='btn-outline-warning     btn-xs mb-1 mb-md-0' type='button' onclick='AMeditClick(" + result[i].EmpInfoId + ")'><i class='bx bxs-edit' aria-hidden='true'></i></button></td>";
                        //}

                        //else if (result[i].RoleType == 'DZSM') {
                        //    row += "<td><button   class='btn-outline-warning     btn-xs mb-1 mb-md-0' type='button' onclick='DZSMeditClick(" + result[i].EmpInfoId + ")'><i class='bx bxs-edit' aria-hidden='true'></i></button></td>";
                        //}

                        //else if (result[i].RoleType == 'NSM') {
                        //    row += "<td><button   class='btn-outline-warning     btn-xs mb-1 mb-md-0' type='button' onclick='NSMMeditClick(" + result[i].EmpInfoId + ")'><i class='bx bxs-edit' aria-hidden='true'></i></button></td>";
                        //}

                        //else {
                        //    row += "<td></td>";
                        //}


                        row += "<td><button class='btn-outline-success     btn-xs mb-1 mb-md-0' type='button' onclick='editClick(" + result[i].EmpInfoId + ")'><i class='fa fa-eye' aria-hidden='true'></i></button></td>";

                        row += "</tr>";


                      /*  <button class='btn-outline-danger btn-sm' onclick='DeleteClick(" + result[i].EmpInfoId + ")'><i class='fas fa-trash' aria-hidden='true'></i></button>*/
                    }

                    $('#dtTableBody').html(row);
                },
                complete: function () {
                    $("#coverScreen").hide();

                    $('#dtTble').dataTable({
                        "bInfo": true,
                        "bFilter": true,
                        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
                        pageLength: 10,
                        dom: 'lBfrtip',


                        buttons: ['copy', 'excel', 'pdf', 'print']
                    });
                }
            });
    }

        function editClick(id) {
            location.href = 'EmployeeSetup.aspx?MID=' + id + '';
          }

          function EmpeditClick(id) {
              location.href = 'EmployeeSetupEdit.aspx?MID=' + id + '';
          }


          function EmpeditClick(id) {
              location.href = 'EmployeeSetupEdit.aspx?MID=' + id + '';
          }
          function UsereditClick(id) {
              location.href = '/DoctorModule_UI/UserSetup.aspx?EMPMID=' + id + '';
          }

          function MIOeditClick(id) {
              location.href = '/DoctorModule_UI/MioUpdateInfo.aspx?EMPMID=' + id + '';
          }

          function AMeditClick(id) {
              location.href = '/DoctorModule_UI/AMSetup.aspx?EMPMID=' + id + '';
          }
          function DZSMeditClick(id) {
              location.href = '/DoctorModule_UI/DZSMSetup.aspx?EMPMID=' + id + '';
          }

          function NSMMeditClick(id) {
              location.href = '/DoctorModule_UI/NSMSetup.aspx?EMPMID=' + id + '';
          }
        function DeleteClick(id) {
            $.confirm({
                icon: 'fas fa-question-circle',
                title: 'Are You Sure ?',
                content: 'You are concern to delete the data!',
                theme: 'Supervan',
                type: 'green',
                buttons: {
                    Confirm: {
                        text: 'Confirm',
                        action: function () {
                            Final_DeleteClick(id);
                        }
                    },
                    Cancel: function () {
                    }
                }
            });

            return false;
        }

        function Final_DeleteClick(id) {
            var Id = id;
            $.ajax({
                url: '/EmployeeGeneral/Delete_EmployeeInformation',
                dataType: 'json',
                type: "POST",
                data: { Id: Id },
                async: false,
                beforeSend: function () {
                },
                success: function (data) {
                    alert("Data Deleted Successfully !!!");
                    location.reload();
                },
                complete: function () {
                }
            });
            return false;
        }

      </script>
</asp:Content>

