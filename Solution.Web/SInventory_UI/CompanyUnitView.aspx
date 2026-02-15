<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="CompanyUnitView.aspx.cs" Inherits="SInventory_UI_SalesCenterView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="page-wrapper">
                <div class="page-content">
                    <!--breadcrumb-->
                    <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                        <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Depot List </div>

                        <div class="ms-auto">
                            <div class="btn-group">

                                <%--<asp:ImageButton ID="unitViewImageButton" runat="server" CssClass="btn btn-sm btn-sm btn-outline-info" AlternateText="Back to list" OnClick="unitViewImageButton_Click" />--%>
                                
                                <asp:ImageButton ID="salesCenterReloadImageButton" CssClass="btn btn-sm btn-sm btn-outline-info" runat="server" AlternateText="Reload" OnClick="salesCenterReloadImageButton_Click" />
                                 <asp:ImageButton ID="salesCenterNewImageButton" CssClass="btn btn-sm btn-sm btn-outline-info" runat="server" AlternateText="Add New" OnClick="salesCenterNewImageButton_Click" />
                                <%--<a href="../DoctorModule_UI/FinancialYearView.aspx" class="btn btn-sm btn-sm btn-outline-info"><i class="fa fa-backward"></i>&nbsp;Back to List</a>--%>
                            </div>
                        </div>
                    </div>

                    <!--end breadcrumb-->
                    <div class="row">
                        <div class="col">

                            <div class="card border-top border-0 border-4 border-success">
                                <div class="card-body">

                                    <div class="row">
                                        <div class="col-12">

                                            <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-striped table-bordered"  DataKeyNames="ComUnitId"
                                                OnRowCommand="loadGridView_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="ComUnitCode" HeaderText="Sales Center Code" />
                                                    <asp:BoundField DataField="ComUnitName" HeaderText="Sales Center Name" />
                                                    <asp:BoundField DataField="Address" HeaderText="Address" />
                                                    <asp:BoundField DataField="PhoneNo" HeaderText="Phone No" />
                                                    <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                                                    <asp:BoundField DataField="FaxNo" HeaderText="Fax  No" />

                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="editImageButton" runat="server"
                                                                CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData"
                                                                ImageUrl="~/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>

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





                <div runat="server" visible="False">
                    <table width="100%" class="TableWorkArea">
                        <tr>
                            <td colspan="6" class="TableHeading">Sales Center View</td>
                        </tr>
                        <tr>
                            <td width="13%" class="TDLeft">Add New</td>
                            <td width="20%" class="TDRight">
                               
                            </td>
                            <td width="13%" class="TDLeft"></td>
                            <td width="20%" class="TDRight"></td>
                            <td width="13%" class="TDLeft">Reload</td>
                            <td width="20%" class="TDRight">
                                
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
                            <td width="13%" class="TDLeft"></td>
                            <td width="20%" class="TDRight" colspan="4"></td>
                            <td width="20%" class="TDRight"></td>
                        </tr>
                        <tr>
                            <td width="13%" class="TDLeft"></td>
                            <td width="20%" class="TDRight"></td>
                            <td width="13%" class="TDLeft"></td>
                            <td width="20%" class="TDRight"></td>
                            <td width="13%" class="TDLeft"></td>
                            <td width="20%" class="TDRight"></td>
                        </tr>
                        <tr>
                            <td width="13%" class="TDLeft"></td>
                            <td width="20%" class="TDRight">&nbsp;
                            </td>
                            <td width="13%" class="TDLeft"></td>
                            <td width="20%" class="TDRight"></td>
                            <td width="13%" class="TDLeft">&nbsp;
                            </td>
                            <td width="20%" class="TDRight"></td>
                        </tr>
                        <tr>
                            <td width="13%" class="TDLeft">&nbsp;
                            </td>
                            <td width="20%" class="TDRight">&nbsp;
                            </td>
                            <td width="13%" class="TDLeft">&nbsp;
                            </td>
                            <td width="20%" class="TDRight">&nbsp;
                            </td>
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
                            <td width="13%" class="TDLeft">&nbsp;
                            </td>
                            <td width="20%" class="TDRight">&nbsp;
                            </td>
                            <td width="13%" class="TDLeft">&nbsp;
                            </td>
                            <td width="20%" class="TDRight">&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

