<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="CustomerPayment.aspx.cs" Inherits="SInventory_UI_CustomerPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="popDiv">
            </div>

            <div class="page-wrapper">
                <div class="page-content">
                    <!--breadcrumb-->
                    <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                        <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Payment Setup </div>

                        <div class="ms-auto">
                            <div class="btn-group">


                                <%--<a href="../MasterSetup_UI/DAList.aspx" class="btn btn-sm btn-sm btn-outline-info"><i class="fa fa-backward"></i>&nbsp;Back to List</a>--%>
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
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                                <ProgressTemplate>

                                                    <div class="divWaiting">
                                                        <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="../images/Spinner.gif" Width="180px" Height="180px" />
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            <div class="row">

                                                <div class="form-group row">
                                                    <div class="col-4">
                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-4 col-form-label">Sales Center:</label>

                                                            <div class="col-sm-7">

                                                                <asp:DropDownList ID="salesCenterDropDownList" runat="server"
                                                                    AutoPostBack="True" CssClass="form-select form-select-sm mySelect2"
                                                                    OnSelectedIndexChanged="salesCenterDropDownList_SelectedIndexChanged">
                                                                </asp:DropDownList>


                                                            </div>
                                                            <span class="text-sm-left text-c-red">*</span>
                                                        </div>

                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-4 col-form-label">Payment Type:</label>

                                                            <div class="col-sm-7">

                                                                <asp:DropDownList ID="payTypeDDL" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>


                                                            </div>
                                                            <span class="text-sm-left text-c-red">*</span>
                                                        </div>
                                                    </div>
                                                    <div class="col-4">

                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-4 col-form-label">Payment Date:</label>

                                                            <div class="col-sm-7">

                                                                <asp:TextBox ID="paymentDtTextBox" runat="server" AutoPostBack="True" ReadOnly="True" CssClass="form-control form-control-sm"></asp:TextBox>


                                                            </div>
                                                            <span class="text-sm-left text-c-red">*</span>
                                                        </div>

                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-4 col-form-label">Customer:</label>

                                                            <div class="col-sm-7">

                                                                <asp:TextBox ID="customerTextBox" runat="server" CssClass="form-control form-control-sm " AutoPostBack="True" OnTextChanged="customerTextBox_TextChanged"></asp:TextBox>


                                                            </div>
                                                            <span class="text-sm-left text-c-red">*</span>
                                                        </div>



                                                    </div>
                                                    <div class="col-4">

                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-4 col-form-label">Reference No:</label>

                                                            <div class="col-sm-7">

                                                                <div class="input-group">
                                                                    <asp:TextBox ID="refNameTextBox" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                                                    <asp:TextBox ID="refDtTextBox" Visible="False" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm mb-3"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="paymentDtTextBox_CalendarExtender0" runat="server"
                                                                        Format="dd-MMM-yyyy" PopupButtonID="ImageButton0"
                                                                        TargetControlID="refDtTextBox">
                                                                    </asp:CalendarExtender>
                                                                    <asp:ImageButton Visible="False" ID="ImageButton0" runat="server"
                                                                        AlternateText="Click to show calendar"
                                                                        ImageUrl="~/Images/Calendar_scheduleHS.png" TabIndex="4" />

                                                                </div>


                                                            </div>
                                                            <span class="text-sm-left text-c-red">*</span>
                                                        </div>


                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-4 col-form-label">Payment Amount:</label>

                                                            <div class="col-sm-7">

                                                                <asp:TextBox ID="paymentAmountTextBox" ReadOnly="True" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderunitValue" runat="server"
                                                                    Enabled="True" TargetControlID="paymentAmountTextBox" FilterType="Custom" ValidChars="0123456789.">
                                                                </asp:FilteredTextBoxExtender>


                                                            </div>
                                                            <span class="text-sm-left text-c-red">*</span>
                                                        </div>

                                                    </div>
                                                </div>


                                                <div runat="server" visible="False">


                                                    <div class="col-2">&nbsp;</div>
                                                    <div class="col-8">



                                                        <div class="form-group row">
                                                            <label for="txtNID" class="col-sm-3 col-form-label">Sales Center:</label>

                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-3 col-form-label">Market: </label>

                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                    <asp:DropDownList ID="marketDropDownList" runat="server" AutoPostBack="True"
                                                                        CssClass="form-control form-control-sm "
                                                                        OnSelectedIndexChanged="marketDropDownList_SelectedIndexChanged">
                                                                    </asp:DropDownList>

                                                                </div>

                                                            </div>
                                                        </div>




                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-3 col-form-label">Customer: </label>

                                                            <div class="col-sm-7">
                                                                <div class="input-group">

                                                                    <asp:DropDownList ID="customerDropDownList" Visible="False" runat="server" AutoPostBack="True" CssClass="DropDown" OnSelectedIndexChanged="customerDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-3 col-form-label">Payment Date: </label>

                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-3 col-form-label">Payment Amount: </label>

                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-3 col-form-label">Payment Type: </label>

                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                </div>

                                                            </div>
                                                        </div>


                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-3 col-form-label">Reference No: </label>

                                                            <div class="col-sm-7">
                                                            </div>
                                                        </div>




                                                        <br />



                                                    </div>

                                                </div>

                                                <br />

                                                <div class="row mt-2">

                                                    <hr />
                                                    <div class="col-4">
                                                        <h5><i class="fa fa-list" aria-hidden="true"></i>Due Invoice List </h5>
                                                    </div>
                                                    <div class="col-5">
                                                    </div>
                                                    <div class="col-3">
                                                    </div>

                                                </div>
                                                <hr />

                                                <div class="row">
                                                    <div class="table-responsive" id="MainGradeDiv">
                                                        <asp:GridView ID="orderGridView" runat="server"
                                                            AutoGenerateColumns="False" CssClass="table table-bordered  text-center thead-dark" DataKeyNames="InvoiceId">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True"
                                                                            OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" AutoPostBack="True" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="InvoiceNo" HeaderText="Pro.Invoice No" />
                                                                <asp:BoundField DataField="InvoiceDate" HeaderText="Pro.Invoice Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                                <asp:BoundField DataField="DelivaryInvoiceNo" HeaderText="Del Invoice No" />
                                                                <asp:BoundField DataField="UpdateDate" HeaderText="Del Invoice Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                                <asp:BoundField DataField="TotalDelivery" HeaderText="Del Inv Amount" HtmlEncodeFormatString="False" />
                                                                <asp:BoundField DataField="PaymentAmount" HeaderText="Previous Pay" HtmlEncodeFormatString="False" />
                                                                <asp:BoundField DataField="Due" HeaderText="Due Amount" />
                                                                <asp:TemplateField HeaderText="Pay Amount">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="payAmountTextBox" runat="server" AutoPostBack="True"
                                                                            OnTextChanged="payAmountTextBox_TextChanged"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderunitValue" runat="server"
                                                                            Enabled="True" TargetControlID="payAmountTextBox" FilterType="Custom" ValidChars="0123456789.">
                                                                        </asp:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:BoundField DataField="PreviousDiscount" HeaderText="Previous Discount" />

                                                                <asp:TemplateField HeaderText="Discount">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="discountTextBox" Width="80px" runat="server" AutoPostBack="True" OnTextChanged="discountTextBox_TextChanged"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderunitValue12" runat="server"
                                                                            Enabled="True" TargetControlID="discountTextBox" FilterType="Custom" ValidChars="0123456789.">
                                                                        </asp:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkAdjust" OnCheckedChanged="chkAdjust_OnCheckedChanged" AutoPostBack="True" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="AdjustableAmount" HeaderText="AdjustableAmount" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>


                                                <hr />
                                                <div class="row">
                                                    <div class="col-2">&nbsp;</div>
                                                    <div class="col-8">

                                                        <div class="form-group row">
                                                            <label for="exampleInputUsername2" class="col-sm-3 col-form-label"></label>
                                                            <div class="col-sm-8">

                                                                <asp:LinkButton OnClick="saveButton_Click" runat="server" ID="masterButton" OnClientClick="return sweetAlertConfirm_Submit(this);" class="btn btnMyDesignSearch btn-sm"><i class="fa fa-print" aria-hidden="true"></i>&nbsp; Submit</asp:LinkButton>

                                                                <asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>

                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="col-2">
                                                    </div>
                                                </div>




                                                <div class="col-2">&nbsp;</div>
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

