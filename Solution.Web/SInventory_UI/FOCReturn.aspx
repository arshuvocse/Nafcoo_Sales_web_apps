<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="FOCReturn.aspx.cs" Inherits="SInventory_UI_FOCReturn" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.28364, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



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
                        <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>FOC Return </div>

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
                                        <br />

                                        <div class="row">&nbsp;</div>
                                        <div class="row">&nbsp;</div>

                                        <div class="row">
                                            <div class="col-2">&nbsp;</div>
                                            <div class="col-8">

                                                <div class="form-group row" runat="server">
                                                    <label for="mainName" class="col-sm-3 col-form-label">FOC :</label>

                                                    <div class="col-sm-5">

                                                        <asp:DropDownList ID="ddlFOC" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlFOC_SelectedIndexChanged">
                                                        </asp:DropDownList>

                                                    </div>


                                                </div>
                                                
                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Return Date:</label>

                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="tbxReturnDate" runat="server" CssClass="form-control form-control-sm  datepicker"></asp:TextBox>
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





                                                        <%--                                                        <asp:LinkButton ID="LinkButton1" CssClass="btn btn-sm btn-primary mb-2" runat="server" OnClick="submitButton_Click" Style="background-color: #00bcd4; color: #fff;"><i class="fa fa-search-plus"></i>&nbsp; Search Information</asp:LinkButton>


                                                        <asp:LinkButton ID="LinkButton2" CssClass="btn btn-sm btn-primary mb-2" runat="server" OnClick="submitButton0_OnClick" Style="background-color: #00bcd4; color: #fff;"><i class="fa fa-check-square"></i>&nbsp; Submit Information</asp:LinkButton>


                                                        <asp:LinkButton ID="LinkButton4" class="btn btn-sm btn-warning  mb-2" Style="background-color: orangered; color: #fff;" runat="server" OnClick="cancelButton_Click"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset Information </asp:LinkButton>
                                                        --%>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-2">&nbsp;</div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="table-responsive" id="MainGradeDiv">



                                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" OnPreRender="gv_DocumentUpload_PreRender"
                                                    DataKeyNames="DcStockOutMasterId,DcStockOutDetailsId,ProductCode,ProductName,BatchNo,ExpDate,ReceiveDate,StackOutQty,ReturnAbleStock" OnRowCommand="loadGridView_RowCommand">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="#SL">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="ProductCode" HeaderText="ProductCode" />
                                                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                                        <asp:BoundField DataField="BatchNo" HeaderText="Batch" />
                                                        <asp:BoundField DataField="ExpDate" HeaderText="ExpDate" DataFormatString="{0:dd-MMM-yyyy}" />
                                                        <asp:BoundField DataField="ReceiveDate" HeaderText="Receive Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                        <asp:BoundField DataField="StackOutQty" HeaderText="Stack Out Qty" />
                                                        <asp:BoundField DataField="ReturnAbleStock" HeaderText="Returnable Stock" />

                                                        <asp:TemplateField HeaderText="Qty">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="returnQtyTextBox" runat="server" AutoPostBack="True" OnTextChanged="returnQtyTextBox_OnTextChanged" CssClass="form-control form-control-sm"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderconvRate" runat="server"
                                                                    Enabled="True" TargetControlID="returnQtyTextBox" FilterType="Custom" ValidChars="0123456789.">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="remarksTextBox" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" AutoPostBack="True" OnCheckedChanged="chkSelect_CheckedChanged" runat="server" />
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

                                                        <asp:LinkButton OnClick="submitButton_Click" runat="server" ID="submitButton" class="btn btnMyDesignSearch   btn-sm"> <i class="fa fa-search"></i> Submit </asp:LinkButton>
                                                        <asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>


                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-2">&nbsp;</div>
                                        </div>


                                    </div>
                                </div>



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

        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>

