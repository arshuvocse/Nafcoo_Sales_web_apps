<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="PaymentAttachment.aspx.cs" Inherits="SInventory_UI_PaymentAttachment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  

    
<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

            <div id="popDiv">
            </div>

            <div class="page-wrapper">
                <div class="page-content">
                    <!--breadcrumb-->
                    <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                        <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Image Upload </div>

                        <div class="ms-auto">
                            <div class="btn-group">


                                <a href="../SInventory_UI/CustomerPaymentList.aspx" class="btn btn-sm btn-sm btn-outline-info"><i class="fa fa-backward"></i>&nbsp;Back to List</a>
                            </div>
                        </div>
                    </div>
                    <!--end breadcrumb-->
                    <div class="row">
                        <div class="col">

                            <div class="card border-top border-0 border-4 border-success">
                                <div class="card-body">
                            <%--        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>--%>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                                <ProgressTemplate>

                                                    <div class="divWaiting">
                                                        <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="../images/Spinner.gif" Width="180px" Height="180px" />
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        

                                            <div class="row">

                                                
                                                <asp:HiddenField runat="server" ID="hfCustPayDetailId"/>

                                                <asp:FileUpload ID="FUDocument"  CssClass="form-control form-control-sm" runat="server"  accept="image/*"  />
    
                                                <asp:LinkButton  runat="server"  OnClick="btnDocUp_OnClick" ID="LinkButton1"  CssClass="btn btn-sm  btn-info">
                                                          
                                                      
                                                    &nbsp;    <span class="btn-label"><i class="fa fa-upload"></i></span>  &nbsp;   &nbsp;Upload Document
                                                </asp:LinkButton>
                                                
                                                
                                                
                                                <asp:Image ID="Image1" runat="server" Visible="false" />  

                                                

                                              
                                            </div>
                                    
                                    
                                    
                                    
                                 
                                       
                                     
                                        <asp:Image ID="Image2" runat="server" Visible="false" />  
                               

                                    <%--    </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
    

</asp:Content>

