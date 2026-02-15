<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="SalesReturnApproval.aspx.cs" Inherits="SInventory_UI_TopSheetGenerateByRouteView" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.28364, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .button-padding-right {
            margin-right: 5px;
        }

        .SelectchkChoice label {
            padding-left: 4px;
            font-weight: bold;
        }

        #ContentPlaceHolder1_rbApprovalStatus > tbody > tr > td {

            padding-right: 8px;
            font-size: 1.3em !important;
            font-weight: bold !important;
        }

        #ContentPlaceHolder1_rbApprovalStatus_0,
        #ContentPlaceHolder1_rbApprovalStatus_1 {

            
            margin-right: 10px !important;

        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Sales Return Approval </div>

                <div class="ms-auto">
                    <div class="btn-group">
                        <%--<asp:LinkButton ID="EmpCetegoryAddImageButton" CssClass="btn btn-sm btn-outline-info " runat="server" OnClick="EmpCetegoryAddImageButton_Click"><i class="fa fa-plus" aria-hidden="true"></i> New Entry </asp:LinkButton>--%>
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
                                            $('.mySelect2').select2({
                                                theme: 'bootstrap4',
                                                width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
                                                placeholder: $(this).data('placeholder'),
                                                allowClear: Boolean($(this).data('allow-clear')),
                                            });
                                            $('.datepicker').pickadate({
                                                selectMonths: true,
                                                selectYears: true
                                            });

                                        }
                                    </script>

                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">
                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label">Depot Name:</label>

                                                <div class="col-sm-5">

                                                    <asp:DropDownList ID="ddlDepot" runat="server"
                                                        CssClass="form-control form-control-sm mySelect2">
                                                    </asp:DropDownList>


                                                </div>
                                                
                                            </div>


                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label">From Date</label>

                                                <div class="col-sm-5">



                                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control form-control-sm datepicker"></asp:TextBox>


                                                </div>
                                                <%-- <span class="text-sm-left text-c-red">*</span>--%>
                                            </div>


                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label">To Date</label>

                                                <div class="col-sm-5">

                                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control form-control-sm datepicker"></asp:TextBox>

                                                </div>
                                                <%-- <span class="text-sm-left text-c-red">*</span>--%>
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

                                                    <asp:LinkButton OnClick="Button1_Click" runat="server" ID="submitButton" class="btn btnMyDesignSearch btn-sm"> <i class="fa fa-search"></i> Search </asp:LinkButton>
                                                    <asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>


                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">&nbsp;</div>
                                    </div>

                                    <br />
                                    
                                    
                                    <div class="row ">
                                        <hr />
                                        <div class="col-2">
                                            <div class="form-group row m-1">
                                                <asp:LinkButton OnClick="submitLinkButton_Click" runat="server" ID="submitLinkButton" class="btn btn-outline-primary btn-sm"> <i class="fa fa-check"></i> Submit </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-8">&nbsp;</div>
                                        <div class="col-2">
                                            <asp:RadioButtonList ID="rbApprovalStatus" RepeatDirection="Horizontal" runat="server">
                                                <asp:ListItem Value="Approved">Approve</asp:ListItem>
                                                <asp:ListItem Value="Rejected">Reject</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <hr />
                                    </div>
                                    


                                    <div class="row">
                                        <div class="table-responsive" id="MainGradeDiv">

                                            <asp:GridView ID="orderGridView" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered  text-center thead-dark" OnRowCommand="loadGridView_RowCommand" OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="ReturnInvoiceId,ReturnInvoiceNo">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" AutoPostBack="True" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Reports">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="topSheetButton" CssClass="btn btn-sm btn-info mb-2" runat="server" OnClick="topSheetButton_Click" ><i class="fa fa-print"></i></asp:LinkButton> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ReturnInvoiceNo" HeaderText="Return Invoice No" />
                                                    <asp:BoundField DataField="ReturnInvoiceDate" HeaderText="Return Invoice Date" />
                                                    <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />
                                                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                                    <asp:BoundField DataField="InvoiceNo" HeaderText="Ref. Invoice No" />
                                                    <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" />
                                                    <asp:BoundField DataField="TpGrandTotal" HeaderText="Total Value" />
                                                    <asp:BoundField DataField="EntryBy" HeaderText="Entry By" />
                                                    <asp:BoundField DataField="CreateDate" HeaderText="Entry Date" />
                                                    <asp:BoundField DataField="Remarks" HeaderText="Return Remarks" />
                                                </Columns>
                                            </asp:GridView>
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
</asp:Content>

