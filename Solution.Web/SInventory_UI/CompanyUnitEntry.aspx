<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="CompanyUnitEntry.aspx.cs" Inherits="SInventory_UI_CompanyUnitEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>




            <div class="page-wrapper">
                <div class="page-content">
                    <!--breadcrumb-->
                    <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                        <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i> Depot Entry </div>

                        <div class="ms-auto">
                            <div class="btn-group">

                                 <asp:ImageButton ID="unitViewImageButton" runat="server" CssClass="btn btn-sm btn-sm btn-outline-info" AlternateText="Back to list"  OnClick="unitViewImageButton_Click" />
                                <%--<a href="../DoctorModule_UI/FinancialYearView.aspx" class="btn btn-sm btn-sm btn-outline-info"><i class="fa fa-backward"></i>&nbsp;Back to List</a>--%>


                            </div>
                        </div>
                    </div>

                    <!--end breadcrumb-->
                    <div class="row">
                        <div class="col">

                            <div class="card border-top border-0 border-4 border-success">
                                <div class="card-body">

                                    <br />
                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">
                                            <div class="form-group row" >
                                                <label for="comUnitCodeTextBox" class="col-sm-3 col-form-label">Depot Code: <span style="color: orangered">*</span> </label>

                                                <div class="col-sm-7">
                                                    
                                                     <asp:TextBox ID="comUnitCodeTextBox" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                                </div>

                                            </div>

                                        </div>
                                    </div>

                                    <div class="row">
                                        
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">
                                            <div class="form-group row" >
                                                <label for="salesCenternameTextBox" class="col-sm-3 col-form-label">Depot Name: <span style="color: orangered">*</span> </label>

                                                <div class="col-sm-7">
                                                    
                                                    <asp:TextBox ID="salesCenternameTextBox" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                     

                                                </div>

                                            </div>

                                        </div>

                                    </div>
                                    
                                    
                                    <div class="row">
                                        
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">
                                            <div class="form-group row" >
                                                <label for="addressTextBox" class="col-sm-3 col-form-label">Depot Address: <span style="color: orangered">*</span> </label>

                                                <div class="col-sm-7">
                                                    
                                                    <asp:TextBox ID="addressTextBox" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                     

                                                </div>


                                            </div>

                                        </div>

                                    </div>
                                    
                                    
                                    <div class="row">
                                        
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">
                                            <div class="form-group row" >
                                                <label for="phoneNoTextBox" class="col-sm-3 col-form-label">Phone No: <span style="color: orangered">*</span> </label>

                                                <div class="col-sm-7">
                                                    
                                                    
                                                    <asp:TextBox ID="phoneNoTextBox" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                     

                                                </div>


                                            </div>

                                        </div>

                                    </div>
                                    
                                    
                                    <div class="row">
                                        
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">
                                            <div class="form-group row" >
                                                <label for="mobileNoTextBox" class="col-sm-3 col-form-label">Mobile No: <span style="color: orangered">*</span> </label>

                                                <div class="col-sm-7">
                                                    
                                                    <asp:TextBox ID="mobileNoTextBox" runat="server"  CssClass="form-control form-control-sm"></asp:TextBox>

                                                </div>
                                           

                                            </div>

                                        </div>

                                    </div>
                                    
                                    <div class="row">
                                        
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">
                                            <div class="form-group row" >
                                                <label for="faxNoTextBox" class="col-sm-3 col-form-label">FAX No: </label>

                                                <div class="col-sm-7">
                                                    
                                                    <asp:TextBox ID="faxNoTextBox" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                                </div>
                                                

                                            </div>

                                        </div>

                                    </div>
                                    

                                    

                                    

                                    <br />
                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">

                                            <div class="form-group row">
                                                <label for="submitButton" class="col-sm-5 col-form-label"></label>
                                                <div class="col-sm-7">
                                                    
                                                    <asp:Button ID="submitButton" CssClass="btn btnMyDesignSearch   btn-sm" runat="server" OnClick="submitButton_Click1" Text="Submit" />
                                                    <asp:LinkButton ID="cancelButton" runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm">&nbsp; Reset </asp:LinkButton>
                                                  
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

            
























            <div runat="server" Visible="False">
                <table width="100%" class="TableWorkArea">
                    <tr>
                        <td colspan="6" class="TableHeading">Sales Center Entry
                        </td>
                    </tr>
                    <tr>
                        <td width="13%" class="TDLeft">&nbsp; View List</td>
                        <td width="20%" class="TDRight">
                           
                        </td>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight">&nbsp;</td>
                        <td width="13%" class="TDLeft"></td>
                        <td width="20%" class="TDRight"></td>
                    </tr>
                    <tr>
                        <td width="13%" class="TDLeft">&nbsp;</td>
                        <td width="20%" class="TDRight">&nbsp;</td>
                        <td width="13%" class="TDLeft">Sales Center Code</td>
                        <td width="20%" class="TDRight">
                           
                        </td>
                        <td width="13%" class="TDLeft">&nbsp;</td>
                        <td width="20%" class="TDRight">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="TDLeft" width="13%"></td>
                        <td class="TDRight" width="20%"></td>
                        <td class="TDLeft" width="13%">Sales Center Name</td>
                        <td class="TDRight" width="20%">
                            
                        </td>
                        <td class="TDLeft" width="13%">&nbsp;</td>
                        <td class="TDRight" width="20%"></td>
                    </tr>
                    <tr>
                        <td width="13%" class="TDLeft">&nbsp;</td>
                        <td width="20%" class="TDRight">&nbsp;</td>
                        <td width="13%" class="TDLeft">Address</td>
                        <td width="20%" class="TDRight">
                            
                        </td>
                        <td width="13%" class="TDLeft">&nbsp;</td>
                        <td width="20%" class="TDRight">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="TDLeft" width="13%">&nbsp;</td>
                        <td class="TDRight" width="20%">&nbsp;</td>
                        <td class="TDLeft" width="13%">Phone No</td>
                        <td class="TDRight" width="20%">
                            
                        </td>
                        <td class="TDLeft" width="13%">&nbsp;</td>
                        <td class="TDRight" width="20%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="TDLeft" width="13%">&nbsp;</td>
                        <td class="TDRight" width="20%">&nbsp;</td>
                        <td class="TDLeft" width="13%">MobileNo</td>
                        <td class="TDRight" width="20%">
                            
                        </td>
                        <td class="TDLeft" width="13%">&nbsp;</td>
                        <td class="TDRight" width="20%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="TDLeft" width="13%">&nbsp;</td>
                        <td class="TDRight" width="20%">&nbsp;</td>
                        <td class="TDLeft" width="13%">FaxNo</td>
                        <td class="TDRight" width="20%">
                            
                        </td>
                        <td class="TDLeft" width="13%">&nbsp;</td>
                        <td class="TDRight" width="20%">&nbsp;</td>
                    </tr>
                    <div runat="server" visible="False" id="div">
                        <tr>
                            <td class="TDLeft" width="13%"></td>
                            <td class="TDRight" width="20%"></td>
                            <td class="TDLeft" width="13%">Company Name</td>
                            <td class="TDRight" width="20%">
                                <asp:DropDownList ID="companyNameDropDownList" runat="server"
                                    CssClass="DropDown" AutoPostBack="True"
                                    OnSelectedIndexChanged="companyNameDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="TDLeft" width="13%"></td>
                            <td class="TDRight" width="20%"></td>
                        </tr>
                        <tr>
                            <td width="13%" class="TDLeft"></td>
                            <td width="20%" class="TDRight">&nbsp;
                            </td>
                            <td width="13%" class="TDLeft">Region Name</td>
                            <td width="20%" class="TDRight">
                                <asp:DropDownList ID="regionDropDownList" runat="server" CssClass="DropDown">
                                </asp:DropDownList>
                            </td>
                            <td width="13%" class="TDLeft">&nbsp;
                            </td>
                            <td width="20%" class="TDRight"></td>
                        </tr>
                    </div>
                    <tr>
                        <td width="13%" class="TDLeft">&nbsp;
                        </td>
                        <td width="20%" class="TDRight">&nbsp;
                        </td>
                        <td width="13%" class="TDLeft">&nbsp;</td>
                        <td width="20%" class="TDRight">&nbsp;</td>
                        <td width="13%" class="TDLeft">&nbsp;
                        </td>
                        <td width="20%" class="TDRight">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="13%" class="TDLeft">&nbsp;
                        </td>
                        <td width="20%" class="TDRight">&nbsp;
                        </td>
                        <td width="13%" class="TDLeft">&nbsp;</td>
                        <td width="20%" class="TDRight">
                            
                        </td>
                        <td width="13%" class="TDLeft">&nbsp;
                        </td>
                        <td width="20%" class="TDRight">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="TDLeft" width="13%">&nbsp;</td>
                        <td class="TDRight" width="20%">&nbsp;</td>
                        <td class="TDLeft" width="13%">&nbsp;</td>
                        <td class="TDRight" width="20%">&nbsp;</td>

                        <td class="TDLeft" width="13%">&nbsp;</td>
                        <td class="TDRight" width="20%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="TDLeft" width="13%">&nbsp;</td>
                        <td class="TDRight" width="20%">&nbsp;</td>
                        <td class="TDLeft" width="13%">&nbsp;</td>
                        <td class="TDRight" width="20%">&nbsp;</td>
                        <td class="TDLeft" width="13%">&nbsp;</td>
                        <td class="TDRight" width="20%">&nbsp;</td>
                    </tr>
                </table>
            </div>
            
            

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

