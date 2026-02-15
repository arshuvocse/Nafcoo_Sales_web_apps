<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="CustomerCreditLimitSetup.aspx.cs" Inherits="SInventory_UI_CustomerCreditLimitSetup" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.28364, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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


        .input-group {
            padding-bottom: 1px !important;
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                        <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Credit Limit Setup </div>

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
                                    <div class="row">
                                        <div class="col-3"></div>
                                        <div class="col-6">
                                            <div class="form-group row">

                                                <label for="mainName" class="col-sm-3 col-form-label">Customer Name: </label>

                                                <div class="col-sm-7">
                                                    <div class="input-group">


                                                        <asp:TextBox ID="custCodeTextBox" runat="server" CssClass="form-control form-control-sm mb-3 "
                                                            AutoPostBack="True" OnTextChanged="custCodeTextBox_TextChanged"></asp:TextBox>


                                                        <asp:AutoCompleteExtender
                                                            ID="at_txt_JobCirculation"
                                                            TargetControlID="custCodeTextBox"
                                                            runat="server"
                                                            ServiceMethod="GetCustomerForOrder"
                                                            ServicePath="SInventoryWebService.asmx"
                                                            MinimumPrefixLength="1"
                                                            CompletionInterval="10"
                                                            EnableCaching="false"
                                                            CompletionSetCount="1"
                                                            FirstRowSelected="false" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                                        </asp:AutoCompleteExtender>



                                                        <asp:HiddenField ID="hfCustomerId" runat="server" />
                                                        <asp:HiddenField ID="hfCustomerPriceGroupId" runat="server" />
                                                        <asp:HiddenField ID="creditHiddenField" runat="server" />


                                                        <span class="input-group-text text-c-red">*</span>

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

                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-3"></div>
                                    </div>

                                    <div class="row">
                                        <div class="col-3"></div>
                                        <div class="col-6">
                                            <div class="form-group row">

                                                <label for="mainName" class="col-sm-3 col-form-label">Limit Amount: </label>

                                                <div class="col-sm-7">
                                                    <div class="input-group">
                                                        <asp:TextBox runat="server" ID="tbxLimitAmount" type="text" class="form-control form-control-sm mb-3" autocomplete="off"></asp:TextBox>
                                                        <span class="input-group-text text-c-red">*</span>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-3"></div>
                                    </div>

                                    <div class="row" runat="server" visible="true">
                                        <div class="col-3"></div>
                                        <div class="col-6">
                                            <div class="form-group row">

                                                <label for="mainName" class="col-sm-3 col-form-label">Day Limit: </label>

                                                <div class="col-sm-7">
                                                    <div class="input-group">
                                                        <asp:TextBox runat="server" ID="tbxDayLimit" type="text" class="form-control form-control-sm mb-3" autocomplete="off"></asp:TextBox>
                                                        <span class="input-group-text text-c-red">*</span>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-3"></div>
                                    </div>


                                    <div style="padding: 2px!important"></div>

                                    <br />
                                    <div class="row">
                                        <div class="col-3"></div>
                                        <div class="col-6">

                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-3 col-form-label"></label>
                                                <div class="col-sm-8">
                                                    <asp:LinkButton OnClick="submitButton_Click" OnClientClick="return sweetAlertConfirm_Submit(this);" runat="server" ID="submitButton" class="btn btnMyDesignSearch   btn-sm"> <i class="fa fa-check"></i>Submit</asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="resetbtn" OnClick="resetbtn_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-3"></div>
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

