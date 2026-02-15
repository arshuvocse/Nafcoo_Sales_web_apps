<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="FOCReturnList.aspx.cs" Inherits="SInventory_UI_FOCReturnList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-wrapper">
                <div class="page-content">
                    <!--breadcrumb-->
                    <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                        <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i> FOC Return List </div>

                        <div class="ms-auto">
                            <div class="btn-group">
                                
                                <a href="../SInventory_UI/FOCReturn.aspx" class="btn btn-sm btn-outline-info "><i class="fa fa-plus" aria-hidden="true"></i> New Entry </a>

                            </div>
                        </div>
                    </div>
                    <!--end breadcrumb-->
                    <div class="row">
                        <div class="col">

                            <div class="card border-top border-0 border-4 border-success">
                                <div class="card-body">

                                    <div class="card-body">

                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                            <ProgressTemplate>

                                                <div class="divWaiting">
                                                    <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="../images/Spinner.gif" Width="180px" Height="180px" />
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>


                                        <div class="row">

                                            <div class="col-4"></div>


                                            <div class="col-4">
                                                

                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Return From Date:</label>

                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="tbxFromDate" runat="server" CssClass="form-control form-control-sm  datepicker"></asp:TextBox>
                                                    </div>
                                                    <span class="text-sm-left text-c-red">*</span>
                                                </div>


                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Return To Date:</label>

                                                    <div class="col-sm-5">
                                                        <asp:TextBox ID="tbxToDate" runat="server" CssClass="form-control form-control-sm  datepicker"></asp:TextBox>
                                                    </div>
                                                    <span class="text-sm-left text-c-red">*</span>
                                                </div>


                                                
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

                                             <div class="col-4"></div>

                                        </div>





                                        <br />
                                        <div class="row">
                                            <div class="col-2">&nbsp;</div>
                                            <div class="col-8">

                                                <div class="form-group row">
                                                    
                                                    <label for="exampleInputUsername2" class="col-sm-4 col-form-label"></label>

                                                    <div class="col-sm-8">

                                                        <asp:LinkButton OnClick="searchButton_Click" runat="server" ID="submitButton" class="btn btnMyDesignSearch   btn-sm"> <i class="fa fa-search"></i> Search </asp:LinkButton>
                                                        <asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>



                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-2">&nbsp;</div>
                                        </div>

                                        <hr />

                                        <div class="row">
                                            <div class="table-responsive" id="MainGradeDiv">
                                                <asp:GridView ID="itemsGridView" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table  blueTable"  DataKeyNames="FOCReturnMasterId"
                                                    OnRowCommand="itemsGridView_RowCommand" AllowPaging="True" PageIndex="0" PageSize="15" OnPageIndexChanging="OnPageIndexChanging">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="SL No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfItemNameId" Value='<%#Eval("FOCReturnMasterId") %>' />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="FOCReturnCode" HeaderText="Return Code" />

                                                        <asp:BoundField DataField="DcStockOutCode" HeaderText="FOC Code" />
                                                        <asp:BoundField DataField="StockOutDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="FOC Date" />

                                                        <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />
                                                        <asp:BoundField DataField="CustomerName" HeaderText="Custome Name" />
                                                        <asp:BoundField DataField="TotalReturnQuantity" HeaderText="Total Return Quantity" />
                                                        <asp:BoundField DataField="UserName" HeaderText="Return By" />
                                                        <asp:BoundField DataField="ReturnDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Chalan Date" />
                                                        
                                                        

            



                                                        <%--                                    <asp:BoundField DataField="ApproveDate" HeaderText="StockIn Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                        <asp:BoundField DataField="ChallanNo" HeaderText="Challan No" />
                                                        <asp:BoundField DataField="ChallanDate" HeaderText="Challan Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                        <asp:BoundField DataField="TotalQuantity" HeaderText="TotalQty" />
                                                        <asp:BoundField DataField="TotalVat" HeaderText="TotalVat" />
                                                        <asp:BoundField DataField="TotalValue" HeaderText="TotalAmount" />--%>

                                                       <%-- <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                
                                                                <asp:ImageButton ID="editImageButton" runat="server" class="btn btn-white btn-sm  "  CommandArgument='<%#Eval("MBEInfoId") %>'
                                                             CommandName="EditData" ImageUrl="~/Assets/edit.png" />

                                                             

                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

                                                    </Columns>
                                                </asp:GridView>
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
    </asp:UpdatePanel>
    
    


</asp:Content>

