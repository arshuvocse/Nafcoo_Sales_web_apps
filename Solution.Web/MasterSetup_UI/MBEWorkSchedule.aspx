<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="MBEWorkSchedule.aspx.cs" Inherits="MasterSetup_UI_MBEWorkSchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Work Schedule for MBE </div>

                <div class="ms-auto">
                    <div class="btn-group">

                        <%--<asp:LinkButton ID="LinkButton1" CssClass="btn btn-sm btn-outline-info " runat="server" OnClick="LinkButton1_Click"><i class="fa fa-plus" aria-hidden="true"></i> New Entry </asp:LinkButton>--%>
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

                                    <div class="p-4 border rounded">
                                        <div class="row g-3 needs-validation">

                                            <div class="table-responsive" id="MainGradeDiv">

                                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                                    DataKeyNames="TWWWPMasterId"
                                                    OnRowCommand="loadGridView_RowCommand" CssClass="table table-striped table-bordered" OnPreRender="gv_DocumentUpload_PreRender">
                                                    <Columns>
                                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                        <asp:BoundField DataField="ApproveStatus" HeaderText="Approve Status" />

                                                        <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By" />
                                                        <asp:BoundField DataField="ApprovedDate" HeaderText="Approved Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>

                                                                <%--<asp:LinkButton ID="editImageButton" runat="server" CssClass="btn-warning  btn-sm mb-1 mb-md-0"
                                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData"> <i class="bx bxs-edit " aria-hidden="true"></i> </asp:LinkButton>--%>

                                                                <asp:LinkButton ID="ApproveButton" runat="server" CssClass=" btn btn-primary  btn-sm mb-1 mb-md-0"
                                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="ApproveData"> <i class="fa fa-check " aria-hidden="true"></i> Approve </asp:LinkButton>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>

        $(document).ready(function () {

            var table = $('#ContentPlaceHolder1_loadGridView').DataTable(
                {
                    "bInfo": true,
                    "bFilter": true,
                    lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
                    pageLength: 10,
                    dom: 'lBfrtip',


                    buttons: ['copy', 'excel', 'pdf', 'print']
                }
            );

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        table = $('#ContentPlaceHolder1_loadGridView').DataTable(
                            {
                                "bInfo": true,
                                "bFilter": true,
                                lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
                                pageLength: 10,
                                dom: 'lBfrtip',


                                buttons: ['copy', 'excel', 'pdf', 'print']


                            }
                        );
                    }
                });
            };


            table.columns().every(function () {
                var that = this;


            });
        });


    </script>


</asp:Content>

