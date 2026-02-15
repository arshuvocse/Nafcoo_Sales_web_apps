<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="MBESetupNewView.aspx.cs" Inherits="SInventory_UI_MBESetupNewView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-wrapper">
                <div class="page-content">
                    
                    <!--breadcrumb-->

                    <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                        <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>MBE Setup List </div>

                        <div class="ms-auto">
                            <div class="btn-group">

                                <a href="../SInventory_UI/MBESetupNew.aspx" class="btn btn-sm btn-outline-info "><i class="fa fa-plus" aria-hidden="true"></i>New Entry</a>

                            </div>
                        </div>
                    </div>
                    <!--end breadcrumb-->

                    <div class="row">
                        <div class="col">

                            <div class="card border-top border-0 border-4 border-success">
                                <div class="card-body">

                                    <div class="card-body">

                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                            <ProgressTemplate>

                                                <div class="divWaiting">
                                                    <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="../images/Spinner.gif" Width="180px" Height="180px" />
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>


                                        <div class="row">

                                            <div class="col-2"></div>

                                            <div class="col-4">

                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Group:</label>

                                                    <div class="col-sm-9">


                                                        <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2" OnSelectedIndexChanged="ddlGroup_OnSelectedIndexChanged" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="mioIdHiddenField" runat="server" />

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


                                                    </div>

                                                </div>


                                                <div class="form-group row" runat="server">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Zone:</label>

                                                    <div class="col-sm-9">


                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2" OnSelectedIndexChanged="ddlZone_OnSelectedIndexChanged" AutoPostBack="True">
                                                        </asp:DropDownList>


                                                    </div>

                                                </div>

                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Region:</label>

                                                    <div class="col-sm-9">


                                                        <asp:DropDownList ID="ddlArea" OnSelectedIndexChanged="ddlArea_OnSelectedIndexChanged" AutoPostBack="True" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2">
                                                        </asp:DropDownList>


                                                    </div>

                                                </div>


                                                <div class="form-group row" runat="server">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Search By Text: </label>

                                                    <div class="col-sm-9">


                                                        <asp:TextBox ID="tbxSearch" runat="server" CssClass="form-control form-control-sm mb-3"></asp:TextBox>



                                                    </div>

                                                </div>

                                            </div>

                                            <div class="col-4">




                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Area:</label>

                                                    <div class="col-sm-9">



                                                        <asp:DropDownList ID="ddlTerritory" AutoPostBack="True" OnSelectedIndexChanged="ddlTerritory_OnSelectedIndexChanged" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2">
                                                        </asp:DropDownList>


                                                    </div>

                                                </div>


                                                <div class="form-group row">

                                                    <label for="mainName" class="col-sm-3 col-form-label">Territory:</label>

                                                    <div class="col-sm-9">



                                                        <asp:DropDownList ID="ddlSubTerritory" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2">
                                                        </asp:DropDownList>


                                                    </div>

                                                </div>


                                                <div class="form-group row" runat="server">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Active Status: </label>

                                                    <div class="col-sm-9">


                                                        <asp:DropDownList ID="ddlActiveStatus" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2">
                                                            
                                         
                                                            <asp:ListItem Value="0"> Select from list </asp:ListItem>
                                                            <asp:ListItem Value="1"> Active </asp:ListItem>
                                                            <asp:ListItem Value="0"> Inactive </asp:ListItem>


                                                        </asp:DropDownList>


                                                    </div>

                                                </div>


                                            </div>

                                            <div class="col-2"></div>

                                        </div>





                                        <br />
                                        <div class="row">
                                            <div class="col-2">&nbsp;</div>
                                            <div class="col-8">

                                                <div class="form-group row">
                                                    <label for="exampleInputUsername2" class="col-sm-3 col-form-label"></label>
                                                    <div class="col-sm-8">

                                                        <asp:LinkButton OnClick="searchButton_Click" runat="server" ID="submitButton" class="btn btnMyDesignSearch   btn-sm"> <i class="fa fa-search"></i> Search </asp:LinkButton>
                                                        <asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>



                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-2">&nbsp;</div>
                                        </div>

                                        <hr />

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
                                                <asp:GridView ID="itemsGridView" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table  blueTable" OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="MBEInfoId, ActiveStatus"
                                                    OnRowCommand="itemsGridView_RowCommand" AllowPaging="True" PageIndex="0" PageSize="15" OnPageIndexChanging="OnPageIndexChanging">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="SL No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfItemNameId" Value='<%#Eval("MBEInfoId") %>' />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="Region" HeaderText="Zone" />
                                                        <asp:BoundField DataField="Area" HeaderText="Region" />
                                                        <asp:BoundField DataField="Territory" HeaderText="Area" />
                                                        <asp:BoundField DataField="SubTerritoryName" HeaderText="Territory" />
                                                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                                        <asp:BoundField DataField="ActiveStatus" HeaderText="Status" />



                                                        <%--                                    <asp:BoundField DataField="ApproveDate" HeaderText="StockIn Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                        <asp:BoundField DataField="ChallanNo" HeaderText="Challan No" />
                                                        <asp:BoundField DataField="ChallanDate" HeaderText="Challan Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                        <asp:BoundField DataField="TotalQuantity" HeaderText="TotalQty" />
                                                        <asp:BoundField DataField="TotalVat" HeaderText="TotalVat" />
                                                        <asp:BoundField DataField="TotalValue" HeaderText="TotalAmount" />--%>

                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>

                                                                <asp:ImageButton ID="editImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("MBEInfoId") %>'
                                                                    CommandName="EditData" ImageUrl="~/Assets/edit.png" />



                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        
        <Triggers>

                                    <asp:PostBackTrigger ControlID="btnExport" />
                                </Triggers>

    </asp:UpdatePanel>


</asp:Content>

