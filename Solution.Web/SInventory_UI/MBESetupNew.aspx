<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="MBESetupNew.aspx.cs" Inherits="SInventory_UI_MBESetupNew" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.28364, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <style type="text/css">
        /*AutoComplete flyout */
        .autocomplete_completionListElement {
            margin: 0px !important;
            background-color: White !important;
            color: windowtext !important;
            border: buttonshadow !important;
            border-width: 1px !important;
            border-style: solid !important;
            cursor: 'default' !important;
            overflow: auto !important;
            font-family: Calibri !important;
            font-size: 14px !important;
            text-align: left !important;
            list-style-type: none !important;
            margin-left: 0px !important;
            padding-left: 0px !important;
            max-height: 200px !important;
            width: 300px !important;
            overflow: auto !important;
            box-shadow: 0 0 3px 1px rgba(0,0,0,.35) !important;
        }


        .autocomplete_completionListElement222 {
            margin: 0px !important;
            background-color: White !important;
            color: windowtext !important;
            border: buttonshadow !important;
            border-width: 1px !important;
            border-style: solid !important;
            cursor: 'default' !important;
            overflow: auto !important;
            font-family: Calibri !important;
            font-size: 14px !important;
            text-align: left !important;
            list-style-type: none !important;
            margin-left: 0px !important;
            padding-left: 0px !important;
            max-height: 200px !important;
            width: 600px !important;
            overflow: auto !important;
            box-shadow: 0 0 3px 1px rgba(0,0,0,.35) !important;
        }
        /* AutoComplete highlighted item */

        .autocomplete_highlightedListItem {
            background-color: #17A2B8 !important;
            color: white !important;
            padding: 6px !important;
            font-weight: bold !important;
        }

        /* AutoComplete item */

        .autocomplete_listItem {
            padding: 6px !important;
            cursor: pointer !important;
            font-weight: bold !important;
            background-color: #fff !important;
            border-bottom: 1px solid #d4d4d4 !important;
            box-shadow: 0 1px 1px rgba(0, 0, 0, 0.075) inset !important;
        }
    </style>



    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i> MBE Setup </div>

                <div class="ms-auto">
                    <div class="btn-group">

                        <asp:LinkButton ID="buttonListPage" CssClass="btn btn-sm btn-outline-info " runat="server" OnClick="buttonListPage_Click"><i class="fa fa-pencil" aria-hidden="true"></i> View List </asp:LinkButton>
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
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">
                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label">Group:</label>

                                                <div class="col-sm-5">


                                                    <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2" OnSelectedIndexChanged="ddlGroup_OnSelectedIndexChanged" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="mioIdHiddenField" runat="server"/>

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
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>


                                            <div  class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-3 col-form-label">Zone:</label>

                                                <div class="col-sm-5">


                                                    <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2" OnSelectedIndexChanged="ddlZone_OnSelectedIndexChanged" AutoPostBack="True">
                                                    </asp:DropDownList>


                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>


                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label">Region:</label>

                                                <div class="col-sm-5">


                                                    <asp:DropDownList ID="ddlArea" OnSelectedIndexChanged="ddlArea_OnSelectedIndexChanged" AutoPostBack="True" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2">
                                                    </asp:DropDownList>


                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>
                                            
                                            
                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label">Area:</label>

                                                <div class="col-sm-5">
                   


                                                    <asp:DropDownList ID="ddlTerritory" AutoPostBack="True" OnSelectedIndexChanged="ddlTerritory_OnSelectedIndexChanged" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2">
                                                    </asp:DropDownList>


                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>
                                            
                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label">Territory:</label>

                                                <div class="col-sm-5">
                   


                                                    <asp:DropDownList ID="ddlSubTerritory" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2">
                                                    </asp:DropDownList>


                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>
                                            
                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label">MBE Name:</label>

                                                <div class="col-sm-5">
                                                   


                                                    <asp:DropDownList ID="ddlMbe" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2">
                                                    </asp:DropDownList>


                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>
                                            
                                            
                                            <div class="form-group row mt-2">
                                                <label for="mainName" class="col-sm-3 col-form-label"> &nbsp; </label>

                                                <div class="col-sm-5">
                                                    
                                                    <asp:CheckBox class="custom-control-input" ID="cbxIsActive" runat="server"/> <span style="font-weight: bold !important;"> Is Active</span> 

                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>
                                            
                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label"> Active Date:  </label>

                                                <div class="col-sm-5">
                                                    
                                                    <asp:TextBox ID="tbxActiveDate" runat="server" CssClass="form-control form-control-sm mb-3 datepicker " autocomplete="off" placeholder="Select Date"></asp:TextBox>

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
                                                <label for="exampleInputUsername2" class="col-sm-3 col-form-label"> </label>
                                                <div class="col-sm-8">

                                                    <asp:LinkButton OnClick="submitButton_Click" runat="server" ID="submitButton" class="btn btnMyDesignSearch   btn-sm"> <i class="fa fa-search"></i> Submit </asp:LinkButton>
                                                    <asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>



                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">&nbsp;</div>
                                    </div>

                                    
                                    
     
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            
                            
                            <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                            
                            
                             <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>





</asp:Content>

