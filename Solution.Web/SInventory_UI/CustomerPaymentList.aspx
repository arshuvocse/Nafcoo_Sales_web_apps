<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="CustomerPaymentList.aspx.cs" Inherits="SInventory_UI_CustomerPaymentList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    



   






    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="popDiv">
            </div>

            <div class="page-wrapper">
                <div class="page-content">
                    <!--breadcrumb-->
                    <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                        <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Payment Setup </div>

                        <div class="ms-auto">
                            <div class="btn-group">


                                <%--<a href="../MasterSetup_UI/DAList.aspx" class="btn btn-sm btn-sm btn-outline-info"><i class="fa fa-backward"></i>&nbsp;Back to List</a>--%>
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

                                                <div class="form-group row">
                                                    <div class="col-md-6">

                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-4 col-form-label">Sales Center:</label>

                                                            <div class="col-sm-7">

                                                                <asp:DropDownList ID="salesCenterDropDownList" runat="server"
                                                                    AutoPostBack="True" CssClass="form-select form-select-sm mb-3 mySelect2"
                                                                    OnSelectedIndexChanged="salesCenterDropDownList_SelectedIndexChanged">
                                                                </asp:DropDownList>


                                                            </div>
                                                            <span class="text-sm-left text-c-red">*</span>
                                                        </div>

                                                      
                                                        
                                                        
                                                        
                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-4 col-form-label">Customer:</label>

                                                            <div class="col-sm-7">

                                                                <asp:TextBox ID="customerTextBox" runat="server" CssClass="form-control form-control-sm mb-3" ></asp:TextBox>


                                                            </div>
                                                            <span class="text-sm-left text-c-red">*</span>
                                                        </div>

                                                    </div>
                                                    <div class="col-md-6">

                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-4 col-form-label">From Date(Payment):</label>

                                                            <div class="col-sm-7">

                                                                <asp:TextBox ID="txtFromDate" runat="server"  CssClass="form-control form-control-sm mb-3"></asp:TextBox>


                                                            </div>
                                                            <span class="text-sm-left text-c-red">*</span>
                                                        </div>
                                                        
                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-4 col-form-label">To Date(Payment):</label>

                                                            <div class="col-sm-7">

                                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control form-control-sm mb-3"></asp:TextBox>


                                                            </div>
                                                            <span class="text-sm-left text-c-red">*</span>
                                                        </div>
                                                        


                                                    </div>
                                                    
                                                </div>


                                                <div runat="server" visible="False">


                                                    <div class="col-2">&nbsp;</div>
                                                    <div class="col-8">



                                                        <div class="form-group row">
                                                            <label for="txtNID" class="col-sm-3 col-form-label">Sales Center:</label>

                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-3 col-form-label">Market: </label>

                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                    <asp:DropDownList ID="marketDropDownList" runat="server" AutoPostBack="True"
                                                                        CssClass="form-control form-control-sm mb-3"
                                                                        OnSelectedIndexChanged="marketDropDownList_SelectedIndexChanged">
                                                                    </asp:DropDownList>

                                                                </div>

                                                            </div>
                                                        </div>




                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-3 col-form-label">Customer: </label>

                                                            <div class="col-sm-7">
                                                                <div class="input-group">

                                                                    <asp:DropDownList ID="customerDropDownList" Visible="False" runat="server" AutoPostBack="True" CssClass="DropDown" OnSelectedIndexChanged="customerDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-3 col-form-label">Payment Date: </label>

                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-3 col-form-label">Payment Amount: </label>

                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-3 col-form-label">Payment Type: </label>

                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                </div>

                                                            </div>
                                                        </div>


                                                        <div class="form-group row">
                                                            <label for="mainName" class="col-sm-3 col-form-label">Reference No: </label>

                                                            <div class="col-sm-7">
                                                            </div>
                                                        </div>




                                                        <br />



                                                    </div>

                                                </div>
                                                
                                                
                                            <br />
                                            <div class="row">
                                      <div class="col-sm-5"></div>

                                                <div class="col-sm-7">
                                                    <div class="input-group">
                                                        <asp:LinkButton OnClick="saveButton_Click" runat="server" ID="LinkButton1" class="btn btnMyDesignSearch    btn-sm"> <i class="fa fa-search"></i>Search</asp:LinkButton>

                                                    </div>

                                                </div>
                                            </div>
                                                
                                                

                                                <br />
                                                <br />

                                                <div class="row">
                                                    <div class="table-responsive" id="MainGradeDiv">
                                                        <asp:GridView ID="orderGridView" runat="server"
                                                            AutoGenerateColumns="False" CssClass="table table-bordered  text-center thead-dark" DataKeyNames="CustPayDetailId">
                                                            <Columns>
                                                                

                                                                <asp:TemplateField HeaderText="SL">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>        
                                                                        <asp:HiddenField runat="server" ID="hfDetailsId" Value='<%#Eval("CustPayDetailId")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField DataField="ComUnitName" HeaderText="Depot Name" />
                                                                <asp:BoundField DataField="ComUnitCode" HeaderText="Depot Code" />
                                                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" /> 
                                                                <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />
                                                                <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                                                <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                                <asp:BoundField DataField="PaymentAmount" HeaderText="Payment Amount" />
                                                                <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date " DataFormatString="{0:dd-MMM-yyyy}" />
                                                                <asp:BoundField DataField="CreateBy" HeaderText="Payment By" />
                                                                <asp:BoundField DataField="PayType" HeaderText="Payment Mode" />
                                                                
                                                                
                                                                <asp:TemplateField HeaderText="Preview">
                                                                    <ItemTemplate>

                                                                        <asp:LinkButton ID="btnpreview" runat="server"  class="btn btn-primary btn-sm " CommandName="DeleteData" OnClick="btnPreview_OnClick"><i class="fa fa-eye" aria-hidden="true"></i>
                                                                            

                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                

                                                                
                                                                <asp:TemplateField HeaderText="Upload">
                                                                    <ItemTemplate>

                                                                        <asp:LinkButton ID="btnUpload" runat="server"   data-toggle="modal" data-target="#exampleModal2"  class="btn btn-danger btn-sm  btnTextShadow" CommandName="DeleteData" OnClick="btnUpload_OnClick"><i class="fa fa-upload" aria-hidden="true"></i>
                                                                            
                                                                            

                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
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

        </ContentTemplate>
    </asp:UpdatePanel>
    

</asp:Content>

