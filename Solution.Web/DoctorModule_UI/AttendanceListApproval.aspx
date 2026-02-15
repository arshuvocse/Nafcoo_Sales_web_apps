<%@ Page Title="Attendance List Approval" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="AttendanceListApproval.aspx.cs" Inherits="DoctorModule_UI_AttendanceListApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
 <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>   Attendance List Approval</div>
                
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


                             


                          <div class="row">
                              <div class="col-md-4"></div>
                              <div class="col-md-4">
                                  
					<div class="col">
					<div class="card radius-10  bg-gradient">
							<div class="card-body">
								<div class="text-center">
									<div>
										  <div class="form-group">

                                               <label style="font-weight: bold">Approval Status:&nbsp;<span style="color: #a52a2a">*</span></label>
                                               <input type="radio" checked id="Approve" name="rbApprove" value="Approve">
                                               <label for="Approve">Approve</label>
                                               <input type="radio" id="Reject" name="rbApprove" value="Reject">
                                               <label for="Reject">Reject</label><br>
                                             <br />
                                            
                                               <input type="button" name="next" class="btn btnMyDesignSearch   btn-sm" onclick="SaveApproval()" value="Submit Information" />

                                           </div>
									</div>
									
									</div>
								</div>
							</div>
						</div>
					</div>
                               
                              </div>
                         
                          

                              


                            <div style="padding-top:10px;"></div>

                        <div class="table-responsive" id="MainGradeDiv">

                            <table id="dtTble"   class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>SL</th>

                                        <th>Attendance Date </th>
                                        <th>Employee ID</th>
                                        <th>Employee Name</th>
                                        <th>Designation</th>
                                        <th>User Role</th>
                                        <th>Shift</th>
                                        <th>Attendance Time</th>
                                        <th>Attendance Type</th>

                                        <th>Approval Status</th>
                                       
                                        <th><input type="checkbox" id="CheckAll" name="CheckAll"></th>
                                    </tr>
                                </thead>
                                <tbody id="dtTableBody"></tbody>
                            </table>
                        </div>
                            </div>
                            </div>
                            </div>
                            </div>
                            </div>
                            </div>


    <script>

        function un(o) {
            return o != null ? o : '';
        }
        $(function () {

            GetPrescription();

            $("#CheckAll").click(function () {

                for (var i = 0; i < $('#dtTableBody tr').length; i++) {
                    RowId = i;
                    RowId++;
                    $("input[name='CheckBox[" + RowId + "].rowCount']").not(this).prop('checked', this.checked);
                }


            });


    });


        function IsActiveChange() {
            var isActive = $('#customSwitch1').is(':checked');
            $('#acttxt').text("");
            if (isActive) {
                $('#acttxt').text("Approve");

            } else {
                $('#acttxt').text("Reject");
            }
        }



        var RowId = 0;



        function validation() {

            debugger;
            var Isvalid = true;
            var NotValid = false;

            var countCh = 0;

            for (var i = 0; i < $('#dtTableBody tr').length; i++) {
                RowId = i;
                RowId++;

                var Cb = $("input[name='CheckBox[" + RowId + "].rowCount']").is(':checked');

                if (Cb != true) {
                    countCh++;
                }


            }

            if (countCh == i) {

                alert("Please select at least one row from List!!!")
                return NotValid;
            }

             return Isvalid;
        }

        function SaveApproval() {

            if (validation()) {

                var jsonData = {};
                jsonData["Id"] = $('#masterId').val();

               // var jsonObjs = [];

                var MyArry = [];

                var id = "";

                for (var i = 0; i < $('#dtTableBody tr').length; i++) {
                    debugger;
                        RowId = i;
                        RowId++;

                    var AttendanceId = $("input[name='DoctorList[" + RowId + "].AttendanceId']").val();
                        var check = $("input[name='CheckBox[" + RowId + "].rowCount']").is(':checked');
                       if (check == true) {


                           id = id + AttendanceId + ',';

                      //  MyArry.push(DoctorId);
                            //theObj["DoctorId"] = DoctorId;
                            //jsonObjs.push(theObj);
                            //jsonData["doctors"] = jsonObjs;
                    }


                }

                var index = id.lastIndexOf(',');

                var srt = id.substring(0, index);

                var radioValue = $("input[name='rbApprove']:checked").val();

                var rbValue = true;
                if (radioValue == "Approve") {
                    rbValue = true;
                }
                else {
                    rbValue = false;

                }

              //  console.log(MyArry);


                var urlpath = 'AttendanceListApproval.aspx/Approve_AttendanceList';
            $.ajax({
                
                data: JSON.stringify({ 'MyArry': srt, 'rbValue': rbValue }),
                //data: jsonData,
                url: urlpath,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    _open_LoadingPopUp_WithMsg("popDiv", "Please wait. Data is Saving...");
                },
                success: function (result) {
                    _close_LoadingPopUp_WithMsg();
                    result = result.d;
                    if (result.isSuccess == true) {

                        successalert('Operation successful!', 'Success', 'AttendanceListApproval.aspx');
                    } else {
                        faildalert('Operation Faild!', 'Faild');
                    }

                },
                error: function (data) {
                    faildalert('Operation Faild!', 'Faild');
                },

            });
            }
        }


        function GetPrescription() {

            var urlpath = 'AttendanceListApproval.aspx/Get_AttendanceList_Approval';
            $.ajax({
                url: urlpath,
                dataType: 'json',
                data: JSON.stringify({   }),
                contentType: "application/json; charset=utf-8",
                type: "POST",
                async: true,
                beforeSend: function() {
                },
                success: function (data) {

                    $('#tabH').show();
                    var result = JSON.parse(data.d);
                    var row = "";
                    $('#dtTableBody').html("");
                    for (var i = 0; i < result.length; i++) {
                        RowId++;
                        var AttendanceId = result[i].AttendanceId;
                        var rowCount = RowId;
                        row += "<tr>";
                        row += "<td>" + (RowId) + "</td>";
                        row += "<td>" + un(result[i].AttendanceDate) + "</td>";
                        row += "<td>" + un(result[i].EmpMasterCode) + "</td>";
                        row += "<td>" + un(result[i].EmpName) + "</td>";
                        row += "<td  >" + un(result[i].DesigName) + "</td>";
                        row += "<td  >" + un(result[i].RoleName) + "</td>";
                        row += "<td>" + un(result[i].ShiftText) + "</td>";
                        row += "<td>" + un(result[i].AttType) + "</td>";
                        row += "<td>" + un(result[i].PunchInTime) + "   <a  data-toggle='tooltip' title='Show in map'    target='_blank' style='font-size:20px' href='http://maps.google.com/?q=" + result[i].PInLoc + "'  class='bx bx-location-plus'></a> </td>";

                        
                        row += "<td>" + un(result[i].ApprovalStatus) + "</td>";
                      
                        row += "<td>" + '<input type = "hidden" style = "text-align:center" id = "HfFieldName"  name ="DoctorList[' + RowId + '].AttendanceId" value = "' + AttendanceId + '" />' + '<input type="checkbox" id="CheckBox" name="CheckBox[' + RowId + '].rowCount">' + "</td>";
                        
                      
                        row += "</tr>";

                    }

                    $('#dtTableBody').html(row);
                },
                complete: function () {
                    //if ($.fn.dataTable.isDataTable('#dtTble')) {
                    //    table = $('#dtTble').DataTable({
                    //        "ordering": false,
                    //        dom: 'lBfrtip',


                    //        buttons: ['copy', 'excel', 'pdf', 'print']
                    //    });
                    //}
                    //else {
                    //    table = $('#dtTble').DataTable({
                    //        "ordering": false,
                    //        dom: 'lBfrtip',


                    //        buttons: ['copy', 'excel', 'pdf', 'print']
                    //    });
                    //}
                }
            });
    }





    </script>

</asp:Content>

