<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="CompanyWiseBranch.aspx.cs" Inherits="SInventory_UI_CompanyWiseBranch" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.28364, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .star-mark {
            color: red;
        }

        .cb label {
            padding-left: 4px;
            display: inline-block;
            font-weight: bold;
            padding-right: 4px;
        }

        .margin-right {
            margin-right: 7px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    chks[i].checked = false;
                }
            }
        }
    </script>


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
                        <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Deposit slip entry </div>

                        <div class="ms-auto">
                            <div class="btn-group">
                                <asp:HyperLink ID="hyperlink2"
                                    NavigateUrl="DepositSlipExcelUpload.aspx"
                                    CssClass="btn btn-success btn-sm"
                                    Target="_new"
                                    runat="server"> &nbsp;Deposit slip XL upload</asp:HyperLink>

                            </div>


                            <asp:LinkButton ID="viewLinkButton" class="btn btn-sm btn-sm btn-outline-info"
                                OnClick="viewLinkButton_OnClick" runat="server"> <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>

                        </div>
                    </div>
                    <!--end breadcrumb-->
                    <div class="row">
                        <div class="col">

                            <div class="card border-top border-0 border-4 border-success">
                                <div class="card-body">

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
                                    </script>

                                    <div class="card-body">

                                        <div class="row">
                                            <div class="col-2">&nbsp;</div>
                                            <div class="col-8">


                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Sales Center:</label>

                                                    <div class="col-sm-5">

                                                        <asp:DropDownList ID="companyNameDropDownList" runat="server"
                                                            CssClass="form-select form-select-sm mb-3 mySelect2 ">
                                                        </asp:DropDownList>

                                                        <asp:HiddenField runat="server" ID="hfMasterId" />

                                                        <asp:HiddenField runat="server" ID="hfImage" />

                                                    </div>
                                                    <span class="text-sm-left text-c-red">*</span>
                                                </div>

                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Deposit Type:</label>

                                                    <div class="col-sm-5">


                                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" AutoPostBack="True" RepeatDirection="Vertical"
                                                            TextAlign="right" CssClass="cb" RepeatColumns="3" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged">
                                                            <asp:ListItem Value="Cash" onclick="MutExChkList(this);">Cash</asp:ListItem>
                                                            <asp:ListItem Value="Online" onclick="MutExChkList(this);">Online</asp:ListItem>
                                                            <asp:ListItem Value="Check" onclick="MutExChkList(this);">Cheque </asp:ListItem>
                                                            <asp:ListItem Value="Other" onclick="MutExChkList(this);">Other</asp:ListItem>
                                                            <asp:ListItem Value="DD" onclick="MutExChkList(this);">DD/Payorder</asp:ListItem>
                                                        </asp:CheckBoxList>

                                                    </div>
                                                    <span class="text-sm-left text-c-red">*</span>
                                                </div>
                                                
                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Month Name:</label>

                                                    <div class="col-sm-5">

                                                        <asp:TextBox ID="tbxMonthName" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                                                    </div>
                                                    <span class="text-sm-left text-c-red">*</span>
                                                </div>


                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Bank Name:</label>

                                                    <div class="col-sm-5">

                                                        <asp:DropDownList ID="ddlBank" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBank_OnSelectedIndexChanged"
                                                            CssClass="form-select form-select-sm mb-3 mySelect2 ">
                                                        </asp:DropDownList>

                                                    </div>
                                                    <span class="text-sm-left text-c-red">*</span>
                                                </div>


                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Account Name:</label>

                                                    <div class="col-sm-5">

                                                        <asp:TextBox ID="txtAccountName" ReadOnly="True" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                                                    </div>
                                                    <span class="text-sm-left text-c-red">*</span>
                                                </div>



                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Account No.:</label>

                                                    <div class="col-sm-5">

                                                        <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control form-control-sm "
                                                            Visible="False">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="accNameTextBox" ReadOnly="True" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                                                    </div>
                                                    <span class="text-sm-left text-c-red">*</span>
                                                </div>


                                                <div class="form-group row" id="branch" runat="server">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Branch Name:</label>

                                                    <div class="col-sm-5">
                                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control form-control-sm "
                                                            Visible="False">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="branchTextBox" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                                    </div>
                                                    <span class="text-sm-left text-c-red">*</span>
                                                </div>



                                                <div class="form-group row" id="chkNo" runat="server">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Check Number:</label>

                                                    <div class="col-sm-5">
                                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control form-control-sm "
                                                            Visible="False">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="chkTextBox" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                                    </div>
                                                    <span class="text-sm-left text-c-red">*</span>
                                                </div>



                                                <div class="form-group row" id="chkDate" runat="server">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Check Date:</label>

                                                    <div class="col-sm-5">
                                                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control form-control-sm  datepicker "
                                                            Visible="False">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="chkDateTextBox" runat="server" CssClass="form-control form-control-sm datepicker "></asp:TextBox>


                                                    </div>
                                                    <span class="text-sm-left text-c-red">*</span>
                                                </div>



                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Deposit Date:</label>

                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="dateTextBox" runat="server" CssClass="form-control form-control-sm  datepicker "></asp:TextBox>


                                                    </div>
                                                    <span class="text-sm-left text-c-red">*</span>
                                                </div>


                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Amount:</label>

                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="amount" CssClass="form-control form-control-sm " runat="server"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="freqQtyTextBox" runat="server" TargetControlID="amount"
                                                            FilterType="Custom, Numbers" ValidChars="." />
                                                    </div>
                                                    <span class="text-sm-left text-c-red">*</span>
                                                </div>



                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">AIT/Others:</label>

                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="AITTextBox1" CssClass="form-control form-control-sm " runat="server"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="AITTextBox1"
                                                            FilterType="Custom, Numbers" ValidChars="." />
                                                    </div>
                                                    <span class="text-sm-left text-c-red">*</span>
                                                </div>



                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Reference No:</label>

                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="txtReferenceName" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>

                                                    </div>

                                                </div>



                                                <%--    <div class="form-group row">
                                    <label for="mainName" class="col-sm-3 col-form-label"> Reference Date:</label>

                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtReferenceDate"  CssClass="form-control form-control-sm datepicker"  runat="server"></asp:TextBox>
                                      
                                    </div>
                                  
                                </div> 
                                                --%>





                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Remarks:</label>

                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="remarksTextBox" CssClass="form-control " TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                                                    </div>

                                                </div>


                                            </div>
                                        </div>







                                        <div class="form-row">

                                            <div class="col-md-3">

                                                <label style="font-size: 15px">Document Information </label>
                                            </div>
                                        </div>

                                        <hr />

                                        <div class="col-md-12">
                                            <fieldset class="for-panel">
                                                <legend>Document </legend>
                                                <div class="row">


                                                    <asp:HiddenField runat="server" ID="hfDocFileName" />
                                                    <asp:HiddenField runat="server" ID="hfDocFile" />
                                                    <div class="col-4">
                                                        <div class="form-group">
                                                            <label>Document Upload<span style="color: red;" title="please fill out this field"> * </span><span style="color: gray!important">Supported Files are:[jpg,jpeg,png,xlsx,pdf,txt,doc,docx]</span></label>
                                                            <div>
                                                                <input type="file" name="postedFile" id="upImage" onchange="showpreview(this)" class="form-control form-control-sm" />
                                                                <asp:FileUpload ID="FUDocument" Visible="False" CssClass="form-control form-control-sm" runat="server" />
                                                                <br />
                                                                <input type="button" class="btn btn-sm  btn-info" id="btnUpload" value="Upload Document" />
                                                                <asp:LinkButton runat="server" Visible="False" OnClick="btnDocUp_OnClick" ID="btnDocUp" CssClass="btn btn-sm  btn-info">
                                                          
                                                      
              &nbsp;    <span class="btn-label"><i class="fa fa-upload"></i></span>  &nbsp;   &nbsp;Upload Document
                                                                </asp:LinkButton>
                                                                <br />
                                                                <progress id="fileProgress" style="display: none"></progress>
                                                                <br />
                                                                <span id="lblMessage" style="color: Green"></span>
                                                                <asp:Label runat="server" ID="lblMsg" Style="color: Green"></asp:Label>
                                                                <asp:HyperLink Visible="False" ID="HyperLink100" runat="server"
                                                                    Target="_blank" ToolTip="Click to Show Document"></asp:HyperLink>



                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="col-4">
                                                        <div class="form-group">
                                                            <label>Short Description<span style="color: red;" title="please fill out this field"> * </span></label>
                                                            <div>

                                                                <asp:TextBox runat="server" ID="txtSummaryNote" TextMode="MultiLine" Rows="2" class="form-control" />

                                                            </div>
                                                        </div>

                                                        <%--        <div class="form-group">
                                                       <asp:LinkButton runat="server" ID="brnAddDoc"   OnClick="brnAddDoc_OnClick"    CssClass="btn btnMyDesignAddtoList   btn-sm pull-right"><span aria-hidden="true" class="fa fa-plus"></span>  &nbsp; Add to List </asp:LinkButton> 
                                                      </div>--%>
                                                    </div>

                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-8">


                                                        <asp:GridView Width="100%" ShowHeader="True" ID="gv_DocumentUpload" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SL#">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Document">

                                                                    <ItemTemplate>
                                                                        <a class="btn btn-sm btnMyDesignSearch" target="_blank" href="<%# Eval("DocumentLinkPreview") %>">Preview</a>

                                                                        <asp:HyperLink ID="HLDocumentLink" Visible="False" Target="_blank" runat="server" NavigateUrl='<%# Eval("DocumentLink") %>' CssClass="btn btn-sm btnMyDesignSearch" Text='Preview'>
                                                                        </asp:HyperLink>


                                                                        <asp:Label ID="lbl_DocumentLink" Visible="False" runat="server" Text='<%#Eval("DocumentLink")%>'></asp:Label>
                                                                        <asp:HiddenField runat="server" ID="hfFileName" Value='<%#Eval("FileName")%>' />
                                                                        <asp:HiddenField runat="server" ID="hfDocumentLinkPreview" Value='<%#Eval("DocumentLinkPreview")%>' />
                                                                        <asp:HiddenField runat="server" ID="hfDocumentLink" Value='<%#Eval("DocumentLink")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="File Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_FileName" runat="server" Text='<%#Eval("FileName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Short Description	">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_DocumentNote" runat="server" Text='<%#Eval("DocumentNote") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>



                                                                <asp:TemplateField HeaderText="Remove">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="btnDocRemove" OnClick="btnDocRemove_OnClick" CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>


                                                    </div>
                                                </div>
                                            </fieldset>

                                        </div>
                                        <br />




                                        <br />
                                        <div class="row">
                                            <div class="col-2">&nbsp;</div>
                                            <div class="col-8">

                                                <div class="form-group row">
                                                    <label for="exampleInputUsername2" class="col-sm-3 col-form-label"></label>
                                                    <div class="col-sm-8">

                                                        <asp:LinkButton ID="btnSave" runat="server" OnClick="submitButton_Click" class="btn btnMyDesignSearch   btn-sm"> <i class="fa fa-check"></i>&nbsp; Submit </asp:LinkButton>


                                                        <asp:LinkButton ID="cancelButton" runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>

                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-2">&nbsp;</div>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            </div>
            </div></div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="False">
        <ContentTemplate>
            <div>
                <table width="100%" class="TableWorkArea">
                    <tr>
                        <td colspan="6" class="TableHeading">Deposit slip entry
                        </td>
                    </tr>
                    <tr>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                        <td width="13%" class="TDLeft">&nbsp;
                        </td>
                        <td width="20%" class="TDRight">&nbsp;
                        </td>
                        <td width="13%" class="TDLeft">&nbsp;
                        </td>
                        <td width="20%" class="TDRight"></td>
                    </tr>
                    <tr>
                        <td width="13%" class="TDLeft">&nbsp;
                        </td>
                        <td width="20%" class="TDRight">
                            <asp:ImageButton ID="ListImageButton" runat="server" ImageUrl="~/images/viewList.png"
                                OnClick="ListImageButton_Click" />
                        </td>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight">
                            <asp:Label ID="MessageLabel" runat="server" ForeColor="#009900"></asp:Label>
                        </td>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                    </tr>
                    <tr>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                        <td width="13%" class="TDLeft">Sales Center <span class="star-mark">* </span>
                        </td>
                        <td width="20%" class="TDRight"></td>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                    </tr>
                    <tr>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                        <td width="13%" class="TDLeft">Deposit Type <span class="star-mark">* </span>
                        </td>
                        <td width="20%" class="TDRight"></td>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                    </tr>
                    <tr>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight">&nbsp;
                        </td>
                        <td width="13%" class="TDLeft">Bank Name
                        </td>
                        <td width="20%" class="TDRight"></td>
                        <td width="13%" class="TDLeft">&nbsp;
                        </td>
                        <td width="20%" class="TDRight"></td>
                    </tr>
                    <tr>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                        <td class="TDLeft" width="13%">Account Name <span class="star-mark">* </span>
                        </td>
                        <td class="TDRight" width="20%"></td>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                    </tr>
                    <tr runat="server">
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                        <td class="TDLeft" width="13%">Branch Name <span class="star-mark">* </span>
                        </td>
                        <td class="TDRight" width="20%"></td>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                    </tr>
                    <tr runat="server">
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                        <td class="TDLeft" width="13%">Check Number <span class="star-mark">* </span>
                        </td>
                        <td class="TDRight" width="20%"></td>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                    </tr>
                    <tr runat="server">
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                        <td class="TDLeft" width="13%">Check Date <span class="star-mark">* </span>
                        </td>
                        <td class="TDRight" width="20%"></td>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                        <td class="TDLeft" width="13%">Deposit Date <span class="star-mark">* </span>
                        </td>
                        <td class="TDRight" width="20%"></td>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                        <td class="TDLeft" width="13%">Amount <span class="star-mark">* </span>
                        </td>
                        <td class="TDRight" width="20%"></td>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                        <td class="TDLeft" width="13%">AIT <span class="star-mark">* </span>
                        </td>
                        <td class="TDRight" width="20%"></td>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                        <td class="TDLeft" width="13%">Remarks
                        </td>
                        <td class="TDRight" width="20%"></td>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">
                            <asp:Button ID="submitButton" runat="server" OnClick="submitButton_Click" CssClass="margin-right"
                                Text="Save" />
                            <asp:Button ID="clearButton" CssClass="clearButton" BackColor="#E8BC31" runat="server"
                                OnClick="clearButton_OnClick" Text="Clear" />
                        </td>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                        <td class="TDLeft" width="13%">&nbsp;
                        </td>
                        <td class="TDRight" width="20%">&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>

        <Triggers>

            <asp:PostBackTrigger ControlID="btnDocUp" />

        </Triggers>
    </asp:UpdatePanel>






    <script type="text/javascript">


        $(document).ready(function () {
            if ($("##ContentPlaceHolder1_hfImage").val() != '') {
                alert("On!!!");
            } else {
                alert("off");
            }

        });



        $("body").on("click", "#btnUpload", function () {
            if ($("#upImage").val() != '') {
                $.ajax({
                    url: '/FileUploadHandler.ashx',
                    type: 'POST',
                    data: new FormData($('form')[0]),
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (file) {
                        $("#ContentPlaceHolder1_hfDocFile").val('');
                        $("#ContentPlaceHolder1_hfDocFileName").val('');
                        $("#fileProgress").hide();
                        $("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                        $("#ContentPlaceHolder1_hfDocFile").val(file.dbfilename);
                        $("#ContentPlaceHolder1_hfDocFileName").val(file.name);
                    },
                    xhr: function () {
                        var fileXhr = $.ajaxSettings.xhr();
                        if (fileXhr.upload) {
                            $("progress").show();
                            fileXhr.upload.addEventListener("progress", function (e) {
                                if (e.lengthComputable) {
                                    $("#fileProgress").attr({
                                        value: e.loaded,
                                        max: e.total
                                    });
                                }
                            }, false);
                        }
                        return fileXhr;
                    }
                });
            } else {
                alert("Please Upload a Document!!!");
            }
        });
    </script>




    <script type="text/javascript">
        function showpreview(input) {


            debugger;
            //$('#ContentPlaceHolder1_imageFileUpload').val($(this).val().toLowerCase());
            var validExtensions = [
                'jpg', 'png', 'JPG', 'PNG', 'jpeg', 'JPEG', 'XLSX', 'xlsx', 'PDF', 'pdf', 'TXT', 'txt', 'DOC', 'doc', 'DOCX', 'docx']; //array of valid extensions
            var fileName = input.files[0].name;
            var fileNameExt = fileName.substr(fileName.lastIndexOf('.') + 1);
            if ($.inArray(fileNameExt, validExtensions) == -1) {
                input.type = '';
                input.type = 'file';
                alert("Only these file types are accepted : jpeg ,jpg, png,xlsx,pdf,txt,doc,docx");
            }
            else {

                var picsize = (input.files[0].size);
                if (picsize > 5000000) {
                    input.type = '';
                    input.type = 'file';

                    alert("File Size is not accepted");

                } else {

                    if (input.files && input.files[0]) {


                        var filerdr = new FileReader();
                        filerdr.onload = function (e) {

                        }
                        filerdr.readAsDataURL(input.files[0]);
                    }

                }


            }

            //if (input.files && input.files[0]) {

            //    var reader = new FileReader();
            //    reader.onload = function (e) {
            //        $('#imgpreview').css('visibility', 'visible');
            //        $('#imgpreview').attr('src', e.target.result);
            //    }
            //    reader.readAsDataURL(input.files[0]);
            //}

        }

    </script>



    <script type="text/javascript">
        function CheckOtherIsCheckedByGVID(spanChk) {
            var IsChecked = spanChk.checked;
            if (IsChecked) {
                spanChk.checked = true;

            } else {
                spanChk.checked = false;
            }
        }
    </script>
</asp:Content>
