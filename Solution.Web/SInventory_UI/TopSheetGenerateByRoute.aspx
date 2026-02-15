<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="TopSheetGenerateByRoute.aspx.cs" Inherits="SInventory_UI_TopSheetGenerateByRoute" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        #flex-container {
            display: flex !important;
            flex-direction: row !important;
            width: 100% !important;
        }

            #flex-container > .flex-item {
                flex: auto !important;
            }

            #flex-container > .raw-item {
                width: 8rem !important;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Invoice List for Topsheet Generate</div>

                <div class="ms-auto">
                    <div class="btn-group">
                        <asp:LinkButton ID="detailsViewButton" CssClass="btn btn-sm btn-sm btn-outline-info" runat="server" OnClick="detailsViewButton_Click"> <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
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





                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">
                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label">Sales Center:</label>

                                                <div class="col-sm-5">


                                                    <asp:DropDownList ID="salesCenterDropDownList" runat="server"
                                                        CssClass="form-control form-control-sm mySelect2" AutoPostBack="True"
                                                        OnSelectedIndexChanged="salesCenterDropDownList_SelectedIndexChanged">
                                                    </asp:DropDownList>

                                                    <asp:HiddenField ID="masterHiddenFieldId" runat="server" />


                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>

                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label">Route:</label>

                                                <div class="col-sm-5">


                                                    <asp:DropDownList ID="rootDropDownList" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2   "
                                                        AutoPostBack="True"
                                                        OnSelectedIndexChanged="rootDropDownList_OnSelectedIndexChanged">
                                                    </asp:DropDownList>


                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>

                                            <div id="Div1" class="form-group row" runat="server" visible="False">
                                                <label for="mainName" class="col-sm-3 col-form-label">Manufacture:</label>

                                                <div class="col-sm-5">


                                                    <asp:DropDownList ID="manufacDropDownList" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2 "
                                                        AutoPostBack="True"
                                                        OnSelectedIndexChanged="manufacDropDownList_SelectedIndexChanged">
                                                    </asp:DropDownList>


                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>


                                            <div id="Div2" class="form-group row" runat="server" visible="False">
                                                <label for="mainName" class="col-sm-3 col-form-label">Market:</label>

                                                <div class="col-sm-5">


                                                    <asp:DropDownList ID="marketDropDownList" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2  "
                                                        AutoPostBack="True"
                                                        OnSelectedIndexChanged="marketDropDownList_SelectedIndexChanged">
                                                    </asp:DropDownList>


                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
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

                                                    <asp:LinkButton OnClick="Button1_Click" runat="server" ID="submitButton" class="btn btnMyDesignSearch   btn-sm"><i class="fa fa-search"></i> Search </asp:LinkButton>
                                                    <%--<asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>--%>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">&nbsp;</div>
                                    </div>

                                    <br />


                                    <div class="row">
                                        <div id="flex-container">
                                            <div class="flex-item" id="flex">&nbsp;</div>
                                            <div class="raw-item" id="raw">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="table-responsive" id="MainGradeDiv">

                                            <asp:GridView ID="orderGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                CssClass="table table-bordered  text-center thead-dark" OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="InvoiceId,InvoiceNo">
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

                                                    <asp:BoundField DataField="OrderNo" HeaderText="Order No" />
                                                    <asp:BoundField DataField="SubmissionDate" HeaderText="Order Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                                    <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />
                                                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                                    <asp:BoundField DataField="MarketName" HeaderText="Market" />

                                                    <asp:BoundField DataField="TpGrandTotal" HeaderText="Invoice Value" />
                                                    <asp:BoundField DataField="CustomerType" HeaderText="Customer Type" />
                                                    <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="printButton" runat="server" class="btn btn-sm btn-info" OnClick="printButton_Click"><i class="fa fa-print"></i>&nbsp; Print Invoice</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <br />
                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">

                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-3 col-form-label"></label>
                                                <div class="col-sm-8">

                                                    <asp:LinkButton ID="addButton" runat="server" CssClass="btn btn-sm btn-info"
                                                        OnClick="addButton_Click"><i class="fa fa-plus"></i>&nbsp; Add to List</asp:LinkButton>

                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">&nbsp;</div>
                                    </div>

                                    <hr />
                                    <div class="row">
                                        <div class="col-7">
                                            <h5><i class="bx bx-list-ol"></i>Top Sheet Invoices </h5>
                                        </div>
                                        <div class="col-5">
                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-4 col-form-label">Delivery Man:</label>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlDa" runat="server"
                                                        CssClass="form-control form-control-sm mySelect2"  >
                                                    </asp:DropDownList>
                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>
                                        </div>
                                    </div>
                                    <hr />

                                    <div class="row">
                                        <div class="table-responsive" id="MainGradeDiv2">

                                            <asp:GridView ID="chalanGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                CssClass="table table-bordered  text-center thead-dark" OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="InvoiceId">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="OrderNo" HeaderText="Order No" />
                                                    <asp:BoundField DataField="SubmissionDate" HeaderText="Order Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                                    <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />
                                                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                                    <asp:BoundField DataField="MarketName" HeaderText="Market" />
                                                    <asp:BoundField DataField="TpGrandTotal" HeaderText="Invoice Value" />
                                                    <asp:BoundField DataField="CustomerType" HeaderText="Customer Type" />
                                                    <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                                    <asp:TemplateField HeaderText="Remove Item">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="DeleteImageButton" runat="server"
                                                                ImageUrl="~/images/lineDelete.png" OnClick="DeleteImageButton_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <br />
                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">

                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-3 col-form-label"></label>
                                                <div class="col-sm-8">

                                                    <asp:LinkButton OnClick="submitButton_Click" runat="server" ID="submitBtn" class="btn btnMyDesignSearch   btn-sm"><i class="fa fa-search"></i> Generate Top Sheet </asp:LinkButton>
                                                    <asp:LinkButton runat="server" OnClick="resetButton_Click" class="btn btnMyDesignReset btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>


                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">&nbsp;</div>
                                    </div>

                                    <br />

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

