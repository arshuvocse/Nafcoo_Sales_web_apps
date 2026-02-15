<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="InvoiceModification.aspx.cs" Inherits="SInventory_UI_InvoiceModification" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.28364, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    
    <style type="text/css">
        .button-padding-right {
            margin-right: 5px;
        }

        .SelectchkChoice label {
            padding-left: 4px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

       <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Invoice Update</div>

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
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                <div class="row">

                                        <div class="card-body">
                                            <br />
                                        
                                        
                                        <div class="row">
                                            <div class="col-md-5"></div>
                                            <div class="col-md-4">
                                                
                                                <div class="form-group row">

                                                   
                                                    
                                                    
                                                    <div class="col-sm-10">

                                                        <label for="mainName" >Invoice No</label>


                                                    </div>

                                                 <div class="col-sm-10">

                                                     <asp:DropDownList ID="ddlInvoiceNo" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2">
                                                     </asp:DropDownList>
                                                     
                                                     
                                                     
                                                     
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


                                                </div>
                                                

                                            </div>

                                        </div>

                                    
                                        <br/>
                                        
                                        <div class="row">
                                            <div class="col-4">&nbsp;</div>
                                            <div class="col-6">

                                                <div class="form-group row">
                                                    <label for="exampleInputUsername2" class="col-sm-3 col-form-label"></label>
                                                    <div class="col-sm-8">
                                                        <asp:LinkButton ID="submitButton" CssClass="btn btn-sm btn-primary mb-2" runat="server" OnClick="submitButton_OnClick"
                                                                       > <i class="fa fa-search"></i>&nbsp; Search</asp:LinkButton>


                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-2">&nbsp;</div>
                                        </div>

                                            <div class="form-group row">


                                            <br />
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GridView1" runat="server"
                                                        AutoGenerateColumns="False" CssClass="table  blueTable" OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="InvoiceDetailId">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Code">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="codeTextBox" runat="server" ReadOnly="True" Text='<%# Eval("ProductCode")%>'
                                                                        CssClass="form-control form-control-sm " AutoPostBack="False"
                                                                       ></asp:TextBox>
                                                                  
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            

                                                            <asp:TemplateField HeaderText="Product Name">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="nameTextBox" runat="server" ReadOnly="True" CssClass="form-control form-control-sm "
                                                                        Text='<%# Eval("ProductName")%>' AutoPostBack="False"
                                                                        ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                      

                                                            <asp:TemplateField HeaderText="Qty">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="qtyTextBox" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"
                                                                        Text='<%# Eval("Quantity")%>' ></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="fqtyTextBox" runat="server"
                                                                        TargetControlID="qtyTextBox"
                                                                        FilterType="Custom, Numbers"
                                                                        ValidChars="." />

                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="TP">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="tpTextBox" runat="server" CssClass="form-control form-control-sm "
                                                                        Text='<%# Eval("TotalPrice")%>' ></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="ftpTextBox" runat="server"
                                                                        TargetControlID="tpTextBox"
                                                                        FilterType="Custom, Numbers"
                                                                        ValidChars="." />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="DP">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dpTextBox" runat="server" CssClass="form-control form-control-sm "
                                                                        Text='<%# Eval("DiscountPercentage")%>' ></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="fdpTextBox" runat="server"
                                                                        TargetControlID="dpTextBox"
                                                                        FilterType="Custom, Numbers"
                                                                        ValidChars="." />

                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="DAmt">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dpAmtTextBox" runat="server" CssClass="form-control form-control-sm "
                                                                        Text='<%# Eval("DiscountAmount")%>' ></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="fdpAmtTextBox" runat="server"
                                                                        TargetControlID="dpAmtTextBox"
                                                                        FilterType="Custom, Numbers"
                                                                        ValidChars="." />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="TPVAT">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="tpVatTextBox" runat="server" CssClass="form-control form-control-sm "
                                                                        Text='<%# Eval("TotalPriceVatAmount")%>' ></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="ftpVatTextBox" runat="server"
                                                                        TargetControlID="tpVatTextBox"
                                                                        FilterType="Custom, Numbers"
                                                                        ValidChars="." />

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            

                                                            <asp:TemplateField HeaderText="NP">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="npTextBox" runat="server" CssClass="form-control form-control-sm "
                                                                        Text='<%# Eval("NetAmount")%>' ></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="fnpTextBox" runat="server"
                                                                        TargetControlID="npTextBox"
                                                                        FilterType="Custom, Numbers"
                                                                        ValidChars="." />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>

                                                </div>
                                            </div>

                                            <br />
                                                
                                                
                                                <br />
                                                <div class="row">
                                                    <div class="col-4">&nbsp;</div>
                                                    <div class="col-6">

                                                        <div class="form-group row">
                                                            <label for="exampleInputUsername2" class="col-sm-3 col-form-label"></label>
                                                            <div class="col-sm-9">

                                                          
                                                                
                                                                
                                                                <asp:LinkButton ID="btnSave" CssClass="btn btnMyDesignSearch   btn-sm" runat="server" OnClick="btnSave_OnClick"
                                                                > <i class="fa fa-check"></i>&nbsp; Submit</asp:LinkButton>
                                                                
                                                           



                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="col-2">&nbsp;</div>
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

