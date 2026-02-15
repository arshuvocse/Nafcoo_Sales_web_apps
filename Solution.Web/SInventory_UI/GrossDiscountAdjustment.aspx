<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="GrossDiscountAdjustment.aspx.cs" Inherits="SInventory_UI_FrossDiscountAdjustment" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.28364, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <style>
        .SelectchkChoice label {
            padding-left: 4px;
            font-weight: bold;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                <ProgressTemplate>

                    <div class="divWaiting">
                        <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="../images/Spinner.gif" Width="180px" Height="180px" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div class="page-wrapper">
                <div class="page-content">
                    <!--breadcrumb-->
                    <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                        <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Invoice Discount Update</div>

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

                                    <div class="card-body">

                                        <br />

                                        

                                        

                                        <div class="row">
                                            <div class="col-2">&nbsp;</div>
                                            <div class="col-8">



                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Delivary Invoice No :</label>

                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="tblDelivaryInvoiceNo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>


                                                    </div>
                                                    <span class="text-sm-left text-c-red">*</span>
                                                </div>




                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-2">&nbsp;</div>
                                            <div class="col-8">



                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label"> Gross Discount Amount:</label>

                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="tblGrossDiscountAmount" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                         <asp:FilteredTextBoxExtender ID="ftpTextBox" runat="server"
                                                                        TargetControlID="tblGrossDiscountAmount"
                                                                        FilterType="Custom, Numbers"
                                                                        ValidChars="." />

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

                                                        <asp:LinkButton ID="submitButton" class="btn btn-info   btn-sm" runat="server" OnClick="saveButton_Click" OnClientClick="return sweetAlertConfirm_Submit(this);">   <i class="fa fa-edit"></i>&nbsp; Update </asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton4" class="btn btnMyDesignReset   btn-sm" runat="server" OnClick="cancelButton_Click"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>

                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-2">&nbsp;</div>
                                        </div>

                                        <br />


                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

