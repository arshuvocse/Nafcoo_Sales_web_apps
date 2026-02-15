<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="SalesReport.aspx.cs" Inherits="SInventory_UI_ProformaReport" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.28364, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Src="~/SInventory_UI/IVMarketStructureInvoSearchReport.ascx" TagPrefix="uc1" TagName="IVMarketStructure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
<%--    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script src="../assets/js/jquery.sumoselect.min.js"></script>
    <link href="../assets/css/sumoselect.css" rel="stylesheet" />--%>
    


    <style type="text/css">
        .button-padding-right {
            margin-right: 5px;
        }

        .SelectchkChoice label {
            padding-left: 4px;
            font-weight: bold;
        }


        input[type="image"] table {
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i> Sales Report </div>

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

                                            $('.multiple-select').select2({
                                                includeSelectAllOption: true,
                                                theme: 'bootstrap4',
                                                width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
                                                placeholder: $(this).data('placeholder'),
                                                allowClear: Boolean($(this).data('allow-clear')),
                                            });
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
                                    </script>

                                    <div class="row">
                                        <div class="col-4">

                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-4 col-form-label">Group: </label>

                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-select form-select-sm mySelect2"></asp:DropDownList>
                                                </div>

                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-4 col-form-label">From Date:</label>

                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="fromDateTextBox" runat="server" class="form-control form-control-sm datepicker" autocomplete="off" placeholder="Select from date"></asp:TextBox>

                                                </div>

                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-4 col-form-label">To Date:</label>

                                                <div class="col-sm-8">

                                                    <asp:TextBox ID="todateTextBox" runat="server" class="form-control form-control-sm datepicker" autocomplete="off" placeholder="Select to date"></asp:TextBox>
                                                </div>

                                            </div>

                                        </div>
                                        <div class="col-4">

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-4 col-form-label">Cluster: </label>

                                                <div class="col-sm-8">
                                                    <asp:ListBox runat="server" ID="ddlCluster" AutoPostBack="True" SelectionMode="Multiple" OnSelectedIndexChanged="lsbClusterHead_OnSelectedIndexChanged" class="form-select form-select-sm mb-3 multiple-select">
                                                        
                                                    </asp:ListBox>

                                                    <%--<asp:DropDownList ID="ddlCluster" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCluster_OnSelectedIndexChanged" CssClass="form-select form-select-sm mySelect2"></asp:DropDownList>--%>
                                                </div>

                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-4 col-form-label">Region:</label>

                                                <div class="col-sm-8">
                                                    
                                                    <asp:ListBox runat="server" ID="ddlRegion" AutoPostBack="True" SelectionMode="Multiple" OnSelectedIndexChanged="lsblRegion_OnSelectedIndexChanged" class="form-select form-select-sm mb-3 multiple-select">
                                                        
                                                    </asp:ListBox>
                                                    
                                                   
                                                    
                                                    <%--<asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRegion_OnSelectedIndexChanged" CssClass="form-select form-select-sm mySelect2"></asp:DropDownList>--%>

                                                </div>

                                            </div>

                                            <div class="form-group row" runat="server" visible="False">
                                                <label for="mainName" class="col-sm-4 col-form-label">Brand:</label>

                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-select form-select-sm mySelect2"></asp:DropDownList>

                                                </div>

                                            </div>


                                        </div>

                                        <div class="col-4">

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-4 col-form-label">Team: </label>

                                                <div class="col-sm-8">
                                                    
                                                    <asp:ListBox runat="server" ID="ddlArea" AutoPostBack="True" SelectionMode="Multiple" OnSelectedIndexChanged="lsblArea_OnSelectedIndexChanged" class="form-select form-select-sm mb-3 multiple-select">
                                                        
                                                    </asp:ListBox>
                                                    
                                                    <%--<asp:DropDownList ID="ddlArea" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlArea_OnSelectedIndexChanged" CssClass="form-select form-select-sm mySelect2"></asp:DropDownList>--%>
                                                </div>

                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-4 col-form-label">Territory: </label>

                                                <div class="col-sm-8">
                                                    
                                                    <asp:ListBox runat="server" ID="ddlTerritory"  SelectionMode="Multiple" class="form-select form-select-sm mb-3 multiple-select">
                                                        
                                                    </asp:ListBox>
                                                    <%--<asp:DropDownList ID="ddlTerritory" runat="server" CssClass="form-select form-select-sm mySelect2"></asp:DropDownList>--%>
                                                </div>

                                            </div>

                                        </div>

                                    </div>


                                    <div class="row" runat="server" visible="False">



                                        <div class="col-4">
                                            <div class="form-group row" runat="server" visible="False">
                                                <label for="mainName" class="col-sm-5 col-form-label">Sales Center: </label>

                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="dcDropDownList1" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2">
                                                    </asp:DropDownList>

                                                </div>

                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-5 col-form-label">From Date:  <span style="color: red">*</span></label>

                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="InvoiceDateTextBox" AutoPostBack="true" OnTextChanged="fromDateTextBox_TextChanged" runat="server" class="form-control form-control-sm datepicker" autocomplete="off" placeholder="Select Invoice From Date"></asp:TextBox>

                                                </div>

                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-5 col-form-label">To Date:  <span style="color: red">*</span></label>

                                                <div class="col-sm-7">

                                                    <asp:TextBox ID="todateTextBox1" runat="server" class="form-control form-control-sm datepicker" autocomplete="off" placeholder="Select Invoice To Date"></asp:TextBox>




                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-8">
                                            <uc1:IVMarketStructure runat="server" ID="IVMarketStructure" />
                                        </div>
                                    </div>

                                    <div class="row" runat="server" visible="False">
                                        <div class="col-4">&nbsp;</div>
                                        <div class="col-4">

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-5 col-form-label">Cluster Head: </label>

                                                <div class="col-sm-7">
                                                    
                                                    

                                                    <asp:DropDownList ID="ddlClusterHead" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlClusterHead_OnSelectedIndexChanged" CssClass="form-select form-select-sm mySelect2">
                                                    </asp:DropDownList>

                                                </div>

                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-5 col-form-label">RSM Name: </label>

                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlRsm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRsm_OnSelectedIndexChanged" CssClass="form-select form-select-sm mySelect2">
                                                    </asp:DropDownList>

                                                </div>

                                            </div>

                                        </div>

                                        <div class="col-4">

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-5 col-form-label">ASM Name: </label>

                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlASM" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlASM_OnSelectedIndexChanged" CssClass="form-select form-select-sm mySelect2">
                                                    </asp:DropDownList>

                                                </div>

                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-5 col-form-label">MBE Name: </label>

                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlMbe" runat="server" CssClass="form-select form-select-sm mySelect2">
                                                    </asp:DropDownList>

                                                </div>

                                            </div>

                                        </div>

                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">

                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-3 col-form-label"></label>
                                                <div class="col-sm-8">
                                                    <asp:LinkButton OnClick="loadButton_Click" runat="server" ID="LinkButton1" class="btn btnMyDesignSearch   btn-sm"><i class="fa fa-list-ol" aria-hidden="true"></i>&nbsp; Preview Report </asp:LinkButton>
                                                    <asp:LinkButton OnClick="SearchButton_Click" runat="server" ID="submitButton" class="btn btnMyDesignSearch   btn-sm"><i class="fa fa-print" aria-hidden="true"></i>&nbsp; Load Report </asp:LinkButton>
                                                    <asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>



                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">
                                        </div>
                                    </div>



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


                                   

                                    <div class="table-responsive" id="MainGradeDiv" >
                                        
                                        
                                        <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                            CssClass="table table-striped table-bordered" OnPreRender="gv_DocumentUpload_PreRender" AllowPaging="False" PageIndex="0" OnPageIndexChanging="loadGridView_PageIndexChanging">
                                            <Columns>
                                                
                                                <asp:BoundField DataField="ClusterCode" HeaderText="Cluster" />
                                                <asp:BoundField DataField="RegionCode" HeaderText="Region" />
                                                <asp:BoundField DataField="AreaCode" HeaderText="Team" />
                                                 <asp:BoundField DataField="FieldName" HeaderText="Territory" />
                                                 <asp:BoundField DataField="MBECode" HeaderText="MBE Code" />
                                                 <asp:BoundField DataField="FieldForceName" HeaderText="MBE Name" />
                                                 <asp:BoundField ItemStyle-Width="150px" DataField="OrderValue" HeaderText="Order " />
                                                 <asp:BoundField ItemStyle-Width="150px" DataField="ProformaValue" HeaderText="Proforma" />
                                                 <asp:BoundField ItemStyle-Width="150px" DataField="InvoiceValue" HeaderText="Invoice" />
                                                 <asp:BoundField ItemStyle-Width="150px" DataField="ReturnValue" HeaderText="Return" />
                                                 <asp:BoundField ItemStyle-Width="150px" DataField="ReturnPercentage" HeaderText="Return (%)" />
                                                 <asp:BoundField ItemStyle-Width="150px" DataField="OnDelivery" HeaderText="On Delivery" />
                                                 <asp:BoundField ItemStyle-Width="150px" DataField="CreditAmount" HeaderText="Credit" />
                                                 <asp:BoundField ItemStyle-Width="150px" DataField="CollectionAmount" HeaderText="Collection" />
                                                 <asp:BoundField ItemStyle-Width="150px" DataField="TargetValue" HeaderText="Target" />
                                                 <asp:BoundField ItemStyle-Width="150px" DataField="Achivement" HeaderText="Achivement (%)" />

                                                
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" CssClass="GridPager" />
                                        </asp:GridView>
                                        
                                        

                                        <%--<asp:GridView ID="gvClusterHead" runat="server"
                                            AutoGenerateColumns="False" CssClass="table table-striped table-bordered"
                                            OnRowDataBound="gvClusterHead_OnRowDataBound" DataKeyNames="ClusterCode,ClusterHead">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" Style="cursor: pointer" ImageUrl="~/images/cluster.png" />
                                                        <asp:Panel ID="pnlRBM" runat="server" Style="display: none">
                                                            <asp:GridView ID="rbmGrid" runat="server"
                                                                AutoGenerateColumns="false" CssClass="table table-striped table-bordered"
                                                                OnRowDataBound="rbmGrid_RowDataBound" DataKeyNames="RegionCode">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Image ID="Image1" runat="server" OnClientClick="return expandABMPanel(this);" Style="cursor: pointer" ImageUrl="~/images/rbm.png" />
                                                                            <asp:Panel ID="pnlABM" runat="server" Style="display: none">
                                                                                <asp:GridView ID="abmGrid" runat="server"
                                                                                    AutoGenerateColumns="false" CssClass="table table-striped table-bordered"
                                                                                    OnRowDataBound="abmGrid_RowDataBound" DataKeyNames="AreaCode">
                                                                                    <Columns>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Image ID="Image1" runat="server" Style="cursor: pointer" ImageUrl="~/images/abm.png" />
                                                                                                <asp:Panel ID="pnlMbe" runat="server" Style="display: none">
                                                                                                    <asp:GridView ID="mbeGrid" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="false">
                                                                                                        <Columns>
                                                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="TerritoryCode" HeaderText="Territory" />
                                                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="MBE" HeaderText="MBE" />
                                                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="OrderValue" HeaderText="Order " />
                                                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="ProformaValue" HeaderText="Proforma" />
                                                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="InvoiceValue" HeaderText="Invoice" />
                                                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="ReturnValue" HeaderText="Return" />
                                                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="ReturnPercentage" HeaderText="Return (%)" />
                                                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="OnDelivery" HeaderText="On Delivery" />
                                                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="CreditAmount" HeaderText="Credit" />
                                                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="CollectionAmount" HeaderText="Collection" />
                                                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="TargetValue" HeaderText="Target" />
                                                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="Achivement" HeaderText="Achivement (%)" />
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                </asp:Panel>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="AreaCode" HeaderText="Area" />
                                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="ASM" HeaderText="ASM" />
                                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="OrderValue" HeaderText="Order " />
                                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="ProformaValue" HeaderText="Proforma" />
                                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="InvoiceValue" HeaderText="Invoice" />
                                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="ReturnValue" HeaderText="Return" />
                                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="ReturnPercentage" HeaderText="Return (%)" />
                                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="OnDelivery" HeaderText="On Delivery" />
                                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="CreditAmount" HeaderText="Credit" />
                                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="CollectionAmount" HeaderText="Collection" />
                                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="TargetValue" HeaderText="Target" />
                                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Achivement" HeaderText="Achivement (%)" />
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </asp:Panel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="RegionCode" HeaderText="Region" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="RSM" HeaderText="RBM" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="OrderValue" HeaderText="Order " />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="ProformaValue" HeaderText="Proforma" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="InvoiceValue" HeaderText="Invoice" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="ReturnValue" HeaderText="Return" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="ReturnPercentage" HeaderText="Return (%)" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="OnDelivery" HeaderText="On Delivery" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="CreditAmount" HeaderText="Credit" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="CollectionAmount" HeaderText="Collection" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="TargetValue" HeaderText="Target" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="Achivement" HeaderText="Achivement (%)" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField ItemStyle-Width="150px" DataField="ClusterCode" HeaderText="Cluster" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="ClusterHead" HeaderText="Cluster Head" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="OrderValue" HeaderText="Order " />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="ProformaValue" HeaderText="Proforma" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="InvoiceValue" HeaderText="Invoice" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="ReturnValue" HeaderText="Return" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="ReturnPercentage" HeaderText="Return (%)" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="OnDelivery" HeaderText="On Delivery" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="CreditAmount" HeaderText="Credit" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="CollectionAmount" HeaderText="Collection" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="TargetValue" HeaderText="Target" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="Achivement" HeaderText="Achivement (%)" />
                                            </Columns>
                                        </asp:GridView>--%>


                                        <%--<asp:GridView ID="G1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered"
                                            DataKeyNames="ClusterCode,ClusterHead" OnRowDataBound="gvClusterHead_OnRowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imagebutton1" runat="server" AlternateText="ImageButton 1" ImageAlign="left" ImageUrl="../images/icon-list-plus.png" OnClick="imbClusterHead_ImageButton_Click" />
                                                        <asp:Panel ID="pnlRBM" runat="server" Visible="False">
                                                            <asp:GridView ID="gvRBM" runat="server" DataKeyNames="RegionCode,RSM" AutoGenerateColumns="false" CssClass="table table-striped table-bordered">
                                                                <Columns>
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="RegionCode" HeaderText="Region" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="RSM" HeaderText="RBM" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="OrderValue" HeaderText="Order " />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="ProformaValue" HeaderText="Proforma" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="InvoiceValue" HeaderText="Invoice" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="ReturnValue" HeaderText="Return" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="ReturnPercentage" HeaderText="Return (%)" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="OnDelivery" HeaderText="On Delivery" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="CreditAmount" HeaderText="Credit" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="CollectionAmount" HeaderText="Collection" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="TargetValue" HeaderText="Target" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="Achivement" HeaderText="Achivement (%)" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField ItemStyle-Width="150px" DataField="ClusterCode" HeaderText="Cluster" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="ClusterHead" HeaderText="Cluster Head" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="OrderValue" HeaderText="Order " />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="ProformaValue" HeaderText="Proforma" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="InvoiceValue" HeaderText="Invoice" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="ReturnValue" HeaderText="Return" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="ReturnPercentage" HeaderText="Return (%)" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="OnDelivery" HeaderText="On Delivery" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="CreditAmount" HeaderText="Credit" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="CollectionAmount" HeaderText="Collection" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="TargetValue" HeaderText="Target" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="Achivement" HeaderText="Achivement (%)" />
                                            </Columns>
                                        </asp:GridView>--%>



                                        <%--<asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered" OnPreRender="gv_DocumentUpload_PreRender" AllowPaging="True" PageIndex="0" OnPageIndexChanging="loadGridView_PageIndexChanging">
                                            <Columns>
                                                
                                                 <asp:BoundField DataField="EmpMasterCode" HeaderText="ID" />
                                                 <asp:BoundField DataField="SubterritoryCode" HeaderText="Territory Code" />
                                                 <asp:BoundField DataField="EmpName" HeaderText="Name" />
                                                 <asp:BoundField DataField="OrderValue" HeaderText="Order Value" />
                                                 <asp:BoundField DataField="InvoiceValue" HeaderText="Invoice Value" />
                                                 <asp:BoundField DataField="ReturnValue" HeaderText="Return Value" />
                                                 <asp:BoundField DataField="ReturnPercentage" HeaderText="Return (%)" />
                                                 <asp:BoundField DataField="OnDelivery" HeaderText="On Delivery" />
                                                 <asp:BoundField DataField="CreditAmount" HeaderText="Credit Amount" />
                                                 <asp:BoundField DataField="PaymentAmount" HeaderText="Collected Amount" />
                                                 <asp:BoundField DataField="TotalTargetByTpVat" HeaderText="Target" />
                                                 <asp:BoundField DataField="Achivement" HeaderText="Achivement (%)" />

                                                
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" CssClass="GridPager" />
                                        </asp:GridView>--%>
                                    </div>

                                    <%--<asp:BoundField DataField="ComUnitCode" HeaderText="Sales Center" />
                                                <asp:BoundField DataField="ComUnitName" HeaderText="Sales Center Name" />
                                                <asp:BoundField DataField="CustomerCode" HeaderText="Customer ID" />
                                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                                <asp:BoundField DataField="IntransitDay" HeaderText=" Customer Type" />
                                                <asp:BoundField DataField="PayType" HeaderText="Mode of Payment" />
                                                <asp:BoundField DataField="OrderNo" HeaderText="Order Code" />
                                                <asp:BoundField DataField="OrderDate" HeaderText="Order / Submission Date" />
                                                <asp:BoundField DataField="InvoiceNo" HeaderText="Proforma Number" />
                                                <asp:BoundField DataField="InvoiceDate" HeaderText="Proforma Date" />
                                                <asp:BoundField DataField="InvoiceBy" HeaderText="Proforma By" />
                                                <asp:BoundField DataField="DelivaryInvoiceNo" HeaderText="Invoice No" />
                                                <asp:BoundField DataField="UpdateDate" HeaderText="Invoice Date" />
                                                <asp:BoundField DataField="ConfirmBy" HeaderText="Confirm By" />
                                                <asp:BoundField DataField="ProductCode" HeaderText="Product Code" />
                                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                                <asp:BoundField DataField="PackSize" HeaderText="Pack Size" />
                                                <asp:BoundField DataField="BatchNo" HeaderText="Batch No" />
                                                <asp:BoundField DataField="ExpDate" HeaderText="Exp Date" />
                                                <asp:BoundField DataField="Quantity" HeaderText="Invoice Qty" />
                                                <asp:BoundField DataField="GrossValue" HeaderText="TP" />
                                                <asp:BoundField DataField="TotalVat" HeaderText="VAT" />
                                                <asp:BoundField DataField="TotalDiscount" HeaderText="Discount" />
                                                <asp:BoundField DataField="FOC" HeaderText="FOC" />
                                                <asp:BoundField DataField="VatOnFOC" HeaderText="Vat On FOC" />
                                                <asp:BoundField DataField="NetTp" HeaderText="Net TP" />
                                                <asp:BoundField DataField="NetTPVat" HeaderText=" Net Amount" />
                                                <asp:BoundField DataField="AdjustmentAmount" HeaderText="Adjustment" />
                                                <asp:BoundField DataField="PaymentNo" HeaderText="Payment No" />
                                                <asp:BoundField DataField="PaymentDate" HeaderText="PaymentDate" />
                                                <asp:BoundField DataField="PayAmount" HeaderText="Pay Amount" />
                                                <asp:BoundField DataField="Due" HeaderText="Due" />
                                                <asp:BoundField DataField="MarketCode" HeaderText="Market Code" />
                                                <asp:BoundField DataField="MarketName" HeaderText="Market Name" />
                                                <asp:BoundField DataField="SubterritoryCode" HeaderText="Territory Code" />
                                                <asp:BoundField DataField="TerritoryName" HeaderText="Territory" />
                                                <asp:BoundField DataField="AreaName" HeaderText="Area" />
                                                <asp:BoundField DataField="RegionName" HeaderText="Region" />
                                                <asp:BoundField DataField="ZoneName" HeaderText="Cluster" />
                                                <asp:BoundField DataField="GroupName" HeaderText="Group" />
                                                <asp:BoundField DataField="MBE" HeaderText="MBE" />
                                                <asp:BoundField DataField="MIO" HeaderText="ABM" />
                                                <asp:BoundField DataField="AM" HeaderText="RBM" />
                                                <asp:BoundField DataField="DZSM" HeaderText="Cluster Head" />
                                                <asp:BoundField DataField="NSM" HeaderText="NSM" />
                                                <asp:BoundField DataField="InvoiceType" HeaderText="Invoice Type" />--%>
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


    <script type="text/javascript">

        //function exportToExcel() {

        //    var file = new Blob([$('#MainGradeDiv').html()], { type: "application/vnd.ms-excel" });
        //    var url = URL.createObjectURL(file);
        //    var a = $("<a />", {
        //        href: url,
        //        download: "Invoice Report.xls"
        //    }).appendTo("body").get(0).click();
        //    e.preventDefault();

        //}


        $(document).ready(function() {

            $("[src*=cluster]").click(function ()
            {
                alert("cluster image has been clicked!");
            });

            // Select all
            $('#ddlCluster').select2('destroy').find('option').prop('selected', 'selected').end().select2();

            <%--$(<%=ddlCluster.ClientID%>).SumoSelect({ selectAll: true });
            $(<%=ddlCluster.ClientID%>).SumoSelect();

            $(<%=ddlRegion.ClientID%>).SumoSelect({ selectAll: true });
            $(<%=ddlRegion.ClientID%>).SumoSelect();

            $(<%=ddlCluster.ClientID%>).SumoSelect({ selectAll: true });
            $(<%=ddlCluster.ClientID%>).SumoSelect();

            $(<%=ddlArea.ClientID%>).SumoSelect({ selectAll: true });
            $(<%=ddlArea.ClientID%>).SumoSelect();

            $(<%=ddlTerritory.ClientID%>).SumoSelect({ selectAll: true });
            $(<%=ddlTerritory.ClientID%>).SumoSelect();--%>
            
        });

        $("[src*=rbm]").click(function () {

            alert("rbm image has been clicked!");
        });

        $("[src*=abm]").click(function () {

            alert("abm image has been clicked!");
        });

        function expandABMPanel(index)
        {
            alert(index.id);
        }

        $("[src*=cluster]").on("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
            $(this).attr("src", "../images/clusterm.png");
        });
        $("[src*=clusterm]").on("click", function () {
            $(this).attr("src", "../images/cluster.png");
            $(this).closest("tr").next().remove();
        });

        debugger;
        $("[src*=rbm]").on("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
            $(this).attr("src", "../images/rbmm.png");
        });
        $("[src*=rbmm]").on("click", function () {
            $(this).attr("src", "../images/rbm.png");
            $(this).closest("tr").next().remove();
        });

        $("[src*=abm]").on("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
            $(this).attr("src", "../images/abmm.png");
        });
        $("[src*=abmm]").on("click", function () {
            $(this).attr("src", "../images/abm.png");
            $(this).closest("tr").next().remove();
        });

       


        //function exportTableToExcel(tableID, filename) {
        //    var downloadLink;
        //    var dataType = 'application/vnd.ms-excel';
        //    var tableSelect = document.getElementById(tableID);
        //    var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');

        //    // Specify file name
        //    filename = filename ? filename + '.xls' : 'excel_data.xls';

        //    // Create download link element
        //    downloadLink = document.createElement("a");

        //    document.body.appendChild(downloadLink);

        //    if (navigator.msSaveOrOpenBlob) {
        //        var blob = new Blob(['\ufeff', tableHTML], {
        //            type: dataType
        //        });
        //        navigator.msSaveOrOpenBlob(blob, filename);
        //    } else {
        //        // Create a link to the file
        //        downloadLink.href = 'data:' + dataType + ', ' + tableHTML;

        //        // Setting the file name
        //        downloadLink.download = filename;

        //        //triggering the function
        //        downloadLink.click();
        //    }
        //}
    </script>

</asp:Content>

