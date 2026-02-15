<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="MiaTargetEntry.aspx.cs" Inherits="SInventory_UI_MiaTargetEntry" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.28364, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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

    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>MIO Target Entry</div>

                <div class="ms-auto">
                    <div class="btn-group">
                        
                                                
                        <asp:LinkButton ID="viewLinkButton"    class="btn btn-sm btn-sm btn-outline-info" 
                                        OnClick="viewLinkButton_OnClick" runat="server"> <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
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
                                            
                                            
                                         
                                            
                                            
                                            
                                            <div class="form-group row" runat="server" Visible="False">
                                                <label for="mainName" class="col-sm-3 col-form-label">Group Name:</label>

                                                <div class="col-sm-5">
                                                    <asp:DropDownList ID="groupDropDownList" Width="180px" runat="server" CssClass="form-control form-control-sm mySelect2" Enabled="False" >
                                                    </asp:DropDownList>
                                                    
                                                    <asp:HiddenField runat="server" ID="miaTargetInfoIdHiddenField"/>

                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>

                                            

                                            <div class="form-group row">
                                                <label for="companyNameDropDownList" class="col-sm-3 col-form-label">Company Name:</label>

                                                <div class="col-sm-5">


                                                    <asp:DropDownList ID="companyNameDropDownList"  runat="server" CssClass="form-control form-control-sm mySelect2" OnSelectedIndexChanged="companyNameDropDownList_SelectedIndexChanged"
                                                                      AutoPostBack="True" >
                                                    </asp:DropDownList>


                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>
                                            
                                            
                                            
                                          


                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label"> MIO Name:</label>

                                                <div class="col-sm-5">


                                                    <asp:DropDownList ID="mioDropDownList"  runat="server" CssClass="form-control form-control-sm mySelect2">
                                                    </asp:DropDownList>


                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>
                                            
                                            
                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label"> MIO Target Amount:</label>

                                                <div class="col-sm-5">


                                                    <asp:TextBox ID="miaTargetAmountTextBox"  runat="server" CssClass="form-control form-control-sm mySelect2"></asp:TextBox>

                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FmiaTargetAmountTextBox" runat="server"
                                                                                         TargetControlID="miaTargetAmountTextBox"         
                                                                                         FilterType="Custom, Numbers"
                                                                                         ValidChars="." />


                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>
                                            
                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label"> Period:</label>

                                                <div class="col-sm-5">


                                                    <asp:DropDownList ID="periodDropDownList"  runat="server" CssClass="form-control form-control-sm mySelect2">
                                                        <asp:ListItem>-----Select-----</asp:ListItem>
                                                        <asp:ListItem>January</asp:ListItem>
                                                        <asp:ListItem>February</asp:ListItem>
                                                        <asp:ListItem>March</asp:ListItem>
                                                        <asp:ListItem>April</asp:ListItem>
                                                        <asp:ListItem>May</asp:ListItem>
                                                        <asp:ListItem>June</asp:ListItem>
                                                        <asp:ListItem>July</asp:ListItem>
                                                        <asp:ListItem>August</asp:ListItem>
                                                        <asp:ListItem>September</asp:ListItem>
                                                        <asp:ListItem>October</asp:ListItem>
                                                        <asp:ListItem>November</asp:ListItem>
                                                        <asp:ListItem>December</asp:ListItem>
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

                                                    <asp:LinkButton OnClick="submitButton_Click1" runat="server" ID="submitButton" class="btn btnMyDesignSearch   btn-sm">
                                            <i class="fa fa-check"></i> Submit
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" OnClick="clearButton_OnClick" class="btn btnMyDesignReset btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>


                                                </div>
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

   
    

</asp:Content>

