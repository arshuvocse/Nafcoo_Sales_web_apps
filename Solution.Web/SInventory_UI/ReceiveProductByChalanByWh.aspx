<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master"
    AutoEventWireup="true" CodeFile="ReceiveProductByChalanByWh.aspx.cs" Inherits="SInventory_UI_ReceiveProductByChalanByWh" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.28364, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
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
                        <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Stock Received By DC  </div>

                        <div class="ms-auto">
                            <div class="btn-group">
                                <asp:LinkButton ID="backLinkButton" runat="server" Font-Bold="True" OnClick="backLinkButton_Click">&lt;&lt;&lt;&lt;&lt;Back To List</asp:LinkButton>
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

                                        <%--        <asp:UpdateProgress ID="UpdateProgress1d" runat="server" ClientIDMode="Static" DisplayAfter="0"
                                            DynamicLayout="true">
                                            <ProgressTemplate>
                                                <div class="divWaiting">
                                                    <asp:Image ID="imgWaitd" runat="server" ImageAlign="Middle" ImageUrl="~/Images/loading-icon-big.gif"
                                                        Height="100%" Width="100%" />
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>--%>


                                        <div class="row">

                                            <div class="col-4">

                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Challan No:</label>

                                                    <div class="col-sm-5">

                                                        <asp:TextBox ID="clnNoTextBox" runat="server" CssClass="form-control form-control-sm mb-3" ReadOnly="True"></asp:TextBox>

                                                    </div>

                                                </div>
                                                
                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Challan Date:</label>

                                                    <div class="col-sm-5">
                                                        
                                                        <asp:TextBox ID="clnDateTextBox" runat="server" CssClass="form-control form-control-sm mb-3" ReadOnly="True"></asp:TextBox>

                                                    </div>

                                                </div>


                                            </div>
                                            <div class="col-4">
                                                
                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Truck No:</label>

                                                    <div class="col-sm-5">

                                                        <asp:TextBox ID="truckTextBox" runat="server" CssClass="form-control form-control-sm mb-3" ReadOnly="True"></asp:TextBox>

                                                    </div>

                                                </div>
                                                
                                                <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Driver Name:</label>

                                                    <div class="col-sm-5">
                                                        
                                                        <asp:TextBox ID="driverNameTextBox" runat="server" CssClass="form-control form-control-sm mb-3" ReadOnly="True"></asp:TextBox>

                                                    </div>

                                                </div>

                                            </div>
                                            <div class="col-4">
                                                
                                                
                                                 <div class="form-group row">
                                                    <label for="mainName" class="col-sm-3 col-form-label">Receive Date:</label>

                                                    <div class="col-sm-5">
                                                        
                                                        <asp:TextBox ID="rcvDateTextBox" runat="server" CssClass="form-control form-control-sm mb-3" ReadOnly="True"></asp:TextBox>
                                                         <asp:HiddenField ID="hdComUnitId" runat="server" />
                     <asp:HiddenField ID="hdReqId" runat="server" />

                                                    </div>

                                                </div>

                                            </div>

                                        </div>



                                        

                                        <br />

                                        <div class="row">
                                            <div class="table-responsive" id="MainGradeDiv">
                                                <asp:GridView ID="rcvGridView" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                                                    DataKeyNames="SChalanId,SChalanDetailsId,DCStoreFreezeId,DCStoreId,ProductId,StockConditionId">
                                                    <Columns>

                                                        <asp:BoundField DataField="ProductCode" HeaderText="ProductCode" />
                                                        <asp:BoundField DataField="ProductName" HeaderText="ProductName" />
                                                        <asp:BoundField DataField="PackSize" HeaderText="PackSize" />
                                                        <asp:BoundField DataField="BatchNo" HeaderText="BatchNo" />
                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                                        <asp:BoundField DataField="MfgDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Mfgdate" />
                                                        <asp:BoundField DataField="ExpDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="ExpDate" />
                                                        <asp:BoundField DataField="ReceiveDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="ReceiveDate" />
                                                        <asp:TemplateField HeaderText="RcvQty">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="rcvQtyTextBox" runat="server" Text='<%# Eval("Quantity")%>' ReadOnly="True"
                                                                    AutoPostBack="True" OnTextChanged="rcvQtyTextBox_TextChanged"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="currentStockTextBox" runat="server" TargetControlID="rcvQtyTextBox"
                                                                    FilterType="Custom, Numbers" ValidChars="." />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UnRcvQty">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="damageTextBox" runat="server" AutoPostBack="True" OnTextChanged="damageTextBox_TextChanged">0</asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="fcurrentStockTextBox" runat="server" TargetControlID="damageTextBox"
                                                                    FilterType="Custom, Numbers" ValidChars="." />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" />
                                                        <asp:BoundField DataField="VatPerUnit" HeaderText="VatPerUnit" />
                                                        <asp:BoundField DataField="Purpose" HeaderText="Purpose" />

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

                                                    

                                                        <asp:LinkButton runat="server" ID="submitButton" OnClientClick="return confirm('Are you sure you want to Save ?');" CssClass="btn btnMyDesignSearch   btn-sm " OnClick="submitButton_Click">  <i class="fa fa-search-plus"></i>&nbsp; Submit </asp:LinkButton>


                                                        <%--<asp:LinkButton runat="server" class="btn btnMyDesignReset   btn-sm" ID="cancelButton" OnClick="cancelButton_Click"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>--%>

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





   <%-- <div>
        <table width="100%" class="TableWorkArea">
            <tr>
                <td colspan="6" class="TableHeading">Stock Receive
                </td>
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
                <td class="TDLeft" width="13%">Chalan No:
                </td>
                <td class="TDRight" width="20%"></td>
                <td class="TDLeft" width="13%">Chalan Date :
                </td>
                <td class="TDRight" width="20%">
                   
                </td>
                <td class="TDLeft" width="13%">Receive Date :
                </td>
                <td class="TDRight" width="20%">
                    <asp:TextBox ID="fff" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TDLeft" width="13%">Truck No
                </td>
                <td class="TDRight" width="20%">
                    <asp:TextBox ID="err" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="TDLeft" width="13%">Driver Name :
                </td>
                <td class="TDRight" width="20%">
                    <asp:TextBox ID="df" runat="server" ReadOnly="True"></asp:TextBox>
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
            <tr>
                <td class="TDLeft" width="13%" colspan="6"></td>
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
                <td class="TDRight" width="20%">
                    
                </td>
                <td class="TDLeft" width="13%">&nbsp;
                </td>
                <td class="TDRight" width="20%">
           
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel3"
                        DisplayAfter="0" DynamicLayout="true">
                        <ProgressTemplate>
                            <center>
                                            <asp:Image ID="Img2" runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                                        </center>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
                <td class="TDLeft" width="13%">&nbsp;
                </td>
                <td class="TDRight" width="20%">&nbsp;
                </td>
            </tr>
            <tr>
                <td class="TDLeft" width="13%">
                   
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
                <td class="TDLeft" width="13%">
                   
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
    </div>--%>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
