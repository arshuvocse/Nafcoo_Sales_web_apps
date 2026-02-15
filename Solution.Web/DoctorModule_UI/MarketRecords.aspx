<%@ Page Title="Market List" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="MarketRecords.aspx.cs" Inherits="DoctorModule_UI_MarketRecords" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
     <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>  Market List</div>
                
                <div class="ms-auto">
                    <div class="btn-group">
                        <a href="../DoctorModule_UI/MarketSetup.aspx"  class="btn btn-sm btn-outline-info " ><i class="fa fa-plus" aria-hidden="true"></i> New Entry</a>
                      

                    </div>
                </div>
            </div>
            <!--end breadcrumb-->
            <div class="row">
                <div class="col">

                    <div class="card border-top border-0 border-4 border-success">
                        <div class="card-body">
                            <div class="table-responsive" id="MainGradeDiv">
                                 <table id="dtTb"    class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                      <th>SL</th>
                                        <th>Group </th>
                                        <th>Zone </th>
                                        <th>Region </th>
                                        <th>Area </th> 
                                        <th>Territory </th> 
                                        <th>Market Code</th>
                                        <th>Market Name</th>
                                        <th>Division</th>
                                        <th>District</th>
                                        <th>Thana</th>
                                       
                                        <th>For DZSM</th>
                                        <th>For AM</th>
                                        <th>For MIO</th>

                                       
                                        <th>Entry By </th>
                                        <th>Entry Date </th>
                                        <th>Update By </th>
                                        <th>Update Date </th>
                                        <th>Inactive  By </th>
                                        <th>Active/Inactive  Date </th>


                                       
                                        <th>Status</th>

                                        
                                        <th>Actions</th>


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
    
 
 <style>
            

    </style>
    <div id="coverScreen" class="divWaitingJquery ">
        <img src="../images/Spinner.gif" style="width:180px" class="position-set" />
                </div>

    <script>

        function un(o) {
            return o != null ? o : '';
        }
        $(function () {

        Getdata();
    });
        function Getdata() {
            var urlpath = 'MarketRecords.aspx/GetMarketList';
            $.ajax({
                url: urlpath,
                //url: urlpath,
                dataType: 'json',
                type: "POST", contentType: "application/json; charset=utf-8",
                async: true,
                beforeSend: function () {
                    $("#coverScreen").show();
                },
                success: function (data) {

                    $('#tabH').show();

                    var result = JSON.parse(data.d);


                    var row = "";
                    $('#dtTableBody').html("");
                    for (var i = 0; i < result.length; i++) {
                        row += "<tr>";
                        row += "<td>" + (i + 1) + "</td>";

                        row += "<td>" + un(result[i].GroupName) + "</td>";
                        row += "<td>" + un(result[i].RegionName) + "</td>";
                        row += "<td>" + un(result[i].AreaName) + "</td>";
                        row += "<td>" + un(result[i].TerritoryName) + "</td>";
                        row += "<td>" + un(result[i].SubTerritoryName) + "</td>";

                        row += "<td>" + un(result[i].MarketCode) + "</td>";
                        row += "<td> " + un(result[i].MarketName) + "</td>";

                        row += "<td> " + un(result[i].DivisionName) + "</td>";
                        row += "<td> " + un(result[i].DistrictName) + "</td>";
                        row += "<td> " + un(result[i].ThanaName) + "</td>";

                        row += "<td> " + un(result[i].DZSMtationType) + "</td>";
                        row += "<td> " + un(result[i].AMStationType) + "</td>";
                        row += "<td> " + un(result[i].MIOStationType) + "</td>";


                        row += "<td>" + un(result[i].EMPEntryBy) + "</td>";
                        row += "<td>" + un(result[i].EntryDatee) + "</td>";

                        row += "<td>" + un(result[i].EMPUpdateBy) + "</td>";
                        row += "<td>" + un(result[i].UpdateDatee)+ "</td>";

                        row += "<td>" + un(result[i].EMPActiveInactiveBy) + "</td>";
                        row += "<td>" + un(result[i].InactiveDatee) + "</td>";

                        if (result[i].IsActive) {
                            row += "<td><span class='badge bg-success'>Active</span></td>";
                        } else {
                            row += "<td><span class='badge bg-warning'>Inactive</span></td>";
                        }
                        row += "<td><button class='btn-outline-warning   btn-xs mb-1 mb-md-0 '   type='button'   onclick='editClick(" + result[i].MarketId + ")' ><i class='bx bxs-edit' aria-hidden='true'></i></button></td>";
                        row += "</tr>";
                    }

                    $('#dtTableBody').html(row);

                },
                complete: function () {

                    $("#coverScreen").hide();

                    $('#dtTb').dataTable({
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
            window.location.href = '../DoctorModule_UI/MarketSetup.aspx?id=' + id + '';

        }
    </script>




</asp:Content>


 

