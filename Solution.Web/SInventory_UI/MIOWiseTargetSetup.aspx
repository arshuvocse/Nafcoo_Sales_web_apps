<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="MIOWiseTargetSetup.aspx.cs" Inherits="SInventory_UI_MIOWiseTargetSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="page-wrapper">
                <div class="page-content">
                    <!--breadcrumb-->
                    <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                        <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>MBE Wise Target Category Setup </div>

                        <div class="ms-auto">
                            <div class="btn-group">

                                <asp:LinkButton ID="viewLinkButton" class="btn btn-sm btn-sm btn-outline-info"
                                    OnClick="viewLinkButton_OnClick" runat="server"> <i class="fa fa-backward"></i>&nbsp; Back to List </asp:LinkButton>

                            </div>
                        </div>
                    </div>
                    <!--end breadcrumb-->
                    <div class="row">
                        <div class="col">

                            <div class="card border-top border-0 border-4 border-success">
                                <div class="card-body">


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
                                                <label for="" class="col-sm-4 col-form-label col-form-label-sm">Month Name:  <span style="color: orangered">[*]</span> </label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="periodDropDownList" runat="server" CssClass="form-control form-control-sm mySelect2">
                                                        
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="masterHiddenFieldId" runat="server" />
                                                </div>
                                            </div>

                                            <div class="form-group row ">
                                                <label for="" class="col-sm-4 col-form-label col-form-label-sm">Upload File: </label>
                                                <div class="col-sm-6">
                                                    <asp:FileUpload ID="id_fu" runat="server" ToolTip="Select File To Upload." class="form-control form-control-sm" />
                                                    <asp:HiddenField ID="IsFileUploaded" runat="server" />
                                                    <br />
                                                    
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:Button ID="btnUpload" runat="server" class="btn btnMyDesignAddtoList btn-sm" Text="Upload" OnClick="btnUpload_OnClick" />
                                                    <asp:HiddenField ID="mainid" runat="server" />
                                                </div>
                                            </div>
                                            
                                        </div>

                                        <div class="col-4">
                                        </div>

                                    </div>

                                    <hr />

                                    <div class="row">
                                        <div class="col-4">
                                            <h5><i class="fa fa-list-ul" aria-hidden="true"></i>&nbsp;MBE Wise Target Category List </h5>
                                        </div>
                                        <div class="col-5"> <asp:Label ID="lbl_up_status" runat="server" CssClass=" pt-1"></asp:Label> </div>
                                        <div class="col-3">

                                            <div class="form-group row" runat="server">

                                                <label class="col-sm-5 col-form-label"></label>
                                                <div class="col-sm-7">

                                                    <a href="../ExcelFiles/MIOWiseTargetSetup_Format.xls" class="btn btn-success btn-sm"> Download Excel Format </a>

                                                </div>


                                            </div>

                                        </div>

                                    </div>
                                    <hr />


                                    <div class="row">
                                        <div class="table-responsive" id="MainGradeDiv">
                                            <asp:GridView ID="productGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered  text-center thead-dark" OnPreRender="gv_DocumentUpload_PreRender">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Cluster">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="clusterTextBox" runat="server" CssClass="form-control form-control-sm "
                                                                AutoPostBack="True" Text='<%# Eval("Cluster")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Region">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="RegionTextBox" runat="server" CssClass="form-control form-control-sm "
                                                                Text='<%# Eval("Region")%>' AutoPostBack="True"></asp:TextBox>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Area Code">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="areaCodeTextBox" Enabled="False" CssClass="form-control form-control-sm " runat="server"
                                                                Text='<%# Eval("AreaCode")%>'></asp:TextBox>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Area">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="areaTextBox" CssClass="form-control form-control-sm " runat="server"
                                                                ReadOnly="True" Text='<%# Eval("Area")%>'></asp:TextBox>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Territory Name">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="territoryTextBox" CssClass="form-control form-control-sm " runat="server"
                                                                Text='<%# Eval("TerritoryName")%>'></asp:TextBox>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MIO Namee">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="mioNameTextBox" runat="server" Text='<%# Eval("MioName")%>'
                                                                CssClass="form-control form-control-sm datepicker"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Target Category">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="targetCategoryTextBox" runat="server" Text='<%# Eval("TargetCategory")%>'
                                                                CssClass="form-control form-control-sm"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <hr />   
                                        <br />
                                        <div class="row">
                                            <div class="col-2">&nbsp;</div>
                                            <div class="col-8">

                                                <div class="form-group row">
                                                    <label for="exampleInputUsername2" class="col-sm-4 col-form-label"></label>
                                                    <div class="col-sm-8">

                                                        <asp:LinkButton ID="submitButton" CssClass="btn btn-sm btn-primary mb-2" runat="server" OnClick="submitButton_Click" Style="background-color: #00bcd4; color: #fff;"
                                                            OnClientClick="return confirm('Are you sure you want to Save ?');"> <i class="fa fa-check-square"></i>&nbsp; Submit </asp:LinkButton>
                                                        <asp:LinkButton ID="cancelButton" class="btn btn-sm btn-warning  mb-2" Style="background-color: orangered; color: #fff;" runat="server" OnClick="cancelButton_Click"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>

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
        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>

    </asp:UpdatePanel>


</asp:Content>

