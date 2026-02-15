<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="CustomerCreditLimitView.aspx.cs" Inherits="SInventory_UI_CustomerCreditLimitView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>List of customer credit limit </div>
                <div class="ms-auto">
                    <div class="btn-group">
                        <asp:LinkButton ID="EmpCetegoryAddImageButton" CssClass="btn btn-sm btn-outline-info " runat="server" OnClick="EmpCetegoryAddImageButton_Click"><i class="fa fa-plus" aria-hidden="true"></i> New Entry </asp:LinkButton>
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
                                            $('.mySelect2').select2({
                                                theme: 'bootstrap4',
                                                width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
                                                placeholder: $(this).data('placeholder'),
                                                allowClear: Boolean($(this).data('allow-clear')),
                                            });
                                        }

                                        var dateNow = new Date();
                                        $('.datepickess').datepicker("setDate", dateNow);
                                        minDate: new Date() // to disable privious dates 
                                    </script>



                                    <br />

                                    <div class="row">
                                        <div class="col-2">
                                            <h3>Details List</h3>
                                        </div>
                                        <div class="col-7">
                                        </div>
                                        <div class="col-3">

                                            <div class="form-group row  pull-right">
                                                <asp:LinkButton ID="btnExport" class="btn btn-sm   mb-2" Style="background-color: #1A7343; color: #fff;" runat="server" OnClick="btnExport_Click"><i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp; Export to Excel </asp:LinkButton>

                                                <%--   <button type="button" class="btn btn-sm   mb-2"  style="background-color: #1A7343; color: #fff;" onclick="exportToExcel()"><i class="fa fa-file-pdf-o" aria-hidden="true"></i>&nbsp; Export to Excel </button>--%>
                                            </div>
                                        </div>

                                    </div>
                                    <hr />

                                    <div class="row">
                                        <div class="table-responsive" id="MainGradeDiv">

                                            <asp:GridView ID="dataGridView" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered  text-center thead-dark" OnRowCommand="loadGridView_RowCommand" OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="CreditLimitid">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="AreaCode" HeaderText="Region Code" />
                                                    <asp:BoundField DataField="AreaName" HeaderText="Region" />
                                                    <asp:BoundField DataField="TerritoryCode" HeaderText="Area Code" />
                                                    <asp:BoundField DataField="TerritoryName" HeaderText="Area" />
                                                    <asp:BoundField DataField="SubTerritoryCode" HeaderText="Territory Code" />
                                                    <asp:BoundField DataField="SubTerritoryName" HeaderText="Territory" />
                                                    <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />
                                                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                                    <asp:BoundField DataField="Address" HeaderText="Customer Address" />
                                                    <asp:BoundField DataField="LimitAmount" HeaderText="Limit Amount" />
                                                    <asp:BoundField DataField="DayLimit" HeaderText="Day Limit" />
                                                    <asp:BoundField DataField="EntryBy" HeaderText="Entry By" />
                                                    <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField DataField="UpdateBy" HeaderText="Update By" />
                                                    <asp:BoundField DataField="UpdateDate" HeaderText="Update Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="editImageButton" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                OnClientClick="return confirm('Are you sure you want to Edit ?');"
                                                                CommandName="EditData" ImageUrl="~/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="deleteImageButton" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                CommandName="DeleteData" OnClientClick="return confirm('Are you sure you want to Delete ?');" ImageUrl="~/images/lineDelete.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>


                                            </asp:GridView>
                                        </div>
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

    
    <script>

        function exportToExcel() {

            var file = new Blob([$('#MainGradeDiv').html()], { type: "application/vnd.ms-excel" });
            var url = URL.createObjectURL(file);
            var a = $("<a />", {
                href: url,
                download: "Invoice Report.xls"
            }).appendTo("body").get(0).click();
            e.preventDefault();

        }

        function exportTableToExcel(tableID, filename) {
            var downloadLink;
            var dataType = 'application/vnd.ms-excel';
            var tableSelect = document.getElementById(tableID);
            var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');

            // Specify file name
            filename = filename ? filename + '.xls' : 'excel_data.xls';

            // Create download link element
            downloadLink = document.createElement("a");

            document.body.appendChild(downloadLink);

            if (navigator.msSaveOrOpenBlob) {
                var blob = new Blob(['\ufeff', tableHTML], {
                    type: dataType
                });
                navigator.msSaveOrOpenBlob(blob, filename);
            } else {
                // Create a link to the file
                downloadLink.href = 'data:' + dataType + ', ' + tableHTML;

                // Setting the file name
                downloadLink.download = filename;

                //triggering the function
                downloadLink.click();
            }
        }
    </script>

</asp:Content>

