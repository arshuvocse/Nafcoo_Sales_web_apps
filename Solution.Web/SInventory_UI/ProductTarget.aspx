<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="ProductTarget.aspx.cs" Inherits="SInventory_UI_ProductTarget" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <style>
        table {
            text-align: center !important;
            border: 1px solid #E2E5E8 !important;
        }

        .table thead th {
            background-color: #7a9ebd !important;
            font-size: 11px !important;
            color: #fff !important;
        }

        .table.table-xs th {
            padding: 0.3rem 2rem !important;
        }

        .table tbody tr {
            border: 1px solid #6b799c !important;
        }

        .table tbody td {
            font-size: 11px !important;
        }

        .table > tbody > tr:not(th):nth-child(even) {
            background-color: #d7e3ee !important;
        }

        .table.table-xs td {
            padding: 0.1rem 2rem !important;
        }
    </style>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div id="popDiv">
    </div>

    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i> Product Wise Target Category Setup </div>

                <div class="ms-auto">
                    <div class="btn-group">


                        <a href="../SInventory_UI/ProductTargetView.aspx" class="btn btn-sm btn-sm btn-outline-info"><i class="fa fa-backward"></i>&nbsp;Back to List</a>


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
                                            })

                                        }
                                    </script>

                                    <div class="row">

                                        <div class="col-2">
                                        </div>
                                        <div class="col-6">

                                            <div class="form-group row ">
                                                <label for="" class="col-sm-4 col-form-label col-form-label-sm">Category Title:  <span style="color: orangered">[*]</span> </label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox class="form-control form-control-sm" runat="server" ID="txtTargetCategory" placeholder="New Target Category Name"></asp:TextBox>
                                                    <asp:HiddenField ID="targetIdHiddenField" runat="server" />
                                                </div>
                                            </div>

                                            <div class="form-group row ">
                                                <label for="" class="col-sm-4 col-form-label col-form-label-sm">Search Category <span style="color: #9400d3 ">(For Edit Only)</span>: </label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList runat="server" ID="ddlSearchCategory" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchCategory_OnSelectedIndexChanged" CssClass="form-control form-control-sm mySelect2" />
                                                </div>
                                            </div>


                                        </div>

                                        <div class="col-4">
                                        </div>

                                    </div>

                                    <hr />

                                    <div class="row">
                                        <div class="col-4">
                                            <h5><i class="fa fa-list-ul" aria-hidden="true"></i> Product List with Updated Price </h5>
                                        </div>
                                        <div class="col-4">
                                            <div class="form-group row" runat="server">

                                                <label class="col-sm-5 col-form-label">TP Total Value:</label>
                                                <div class="col-sm-7">

                                                    <asp:TextBox class="form-control form-control-sm" runat="server" ReadOnly="true" AutoPostBack="True" OnTextChanged="codeTextBox_TextChanged" ID="txtTotalTarget" placeholder="TP Total Value (Will update auto)"></asp:TextBox>

                                                </div>


                                            </div>
                                        </div>
                                        <div class="col-4">

                                            <div class="form-group row" runat="server">

                                                <label class="col-sm-5 col-form-label">TP+VAT Total Value:</label>
                                                <div class="col-sm-7">

                                                    <asp:TextBox class="form-control form-control-sm " runat="server" ReadOnly="true" AutoPostBack="True" OnTextChanged="codeTextBox_TextChanged" ID="txtTotalTargetWithVAt" placeholder="TP+VAT Total Value (Will update auto)"></asp:TextBox>

                                                </div>


                                            </div>

                                        </div>

                                    </div>
                                    <hr />

                                    <div class="row">

                                        <div class="table-responsive" id="MainGradeDiv">
                                            <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-hover"
                                                DataKeyNames="ProductId" OnRowCommand="loadGridView_RowCommand" OnPreRender="gv_DocumentUpload_PreRender">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                            <asp:HiddenField runat="server" ID="hfGatePassMasterId" Value='<%#Eval("ProductId") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Product Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ProductCode" runat="server" ReadOnly="true" AutoPostBack="True" ontextchanged="codeTextBox_TextChanged" CssClass="form-control form-control-sm " Text='<%# Eval("ProductCode")%>' />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                                    <asp:BoundField DataField="PackSize" HeaderText="Pack Size" />


                                                    <asp:TemplateField HeaderText="Target Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TargetQty" runat="server" AutoPostBack="True"
                                                                OnTextChanged="codeTextBox_TextChanged" CssClass="form-control form-control-sm "></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="fupVatTextBox" runat="server"
                                                                TargetControlID="TargetQty"
                                                                FilterType="Custom, Numbers"
                                                                ValidChars="." />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="TP/Pack">
                                                        <ItemTemplate>
                                                            <asp:Label ID="UnitPrice" runat="server" ReadOnly="true" AutoPostBack="True" ontextchanged="codeTextBox_TextChanged" CssClass="form-control form-control-sm " Text='<%# Eval("UnitPrice")%>' />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Target With TP">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TargetValue" runat="server" AutoPostBack="True" ReadOnly="true"
                                                                OnTextChanged="codeTextBox_TextChanged" CssClass="form-control form-control-sm "></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="VAT/Pack">
                                                        <ItemTemplate>
                                                            <asp:Label ID="VATAmountPerUnit" runat="server" ReadOnly="true" AutoPostBack="True" ontextchanged="codeTextBox_TextChanged" CssClass="form-control form-control-sm " Text='<%# Eval("VATAmountPerUnit")%>' />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Target With TP+VAT">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TargetWithVAT" runat="server" ReadOnly="true" AutoPostBack="True" OnTextChanged="codeTextBox_TextChanged"
                                                                CssClass="form-control form-control-sm "></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            
                                        </div>

                                        <div class="col-2">&nbsp;</div>
                                    </div>
                                    
                                    
                                    <br />
                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">

                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-4 col-form-label"></label>
                                                <div class="col-sm-8">

                                                    <asp:LinkButton OnClick="SaveButton_Click" runat="server" ID="SaveButton" class="btn btnMyDesignSearch btn-sm"><i class="fa fa-save" aria-hidden="true"></i> Submit  </asp:LinkButton>    
                                                    <asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i> Reset </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">
                                        </div>
                                    </div>
                                    
                                    <br />
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

