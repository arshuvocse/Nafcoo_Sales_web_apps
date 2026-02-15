<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="CustomerApprovalNew.aspx.cs" Inherits="SInventory_UI_CustomerApprovalNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-wrapper">
                <div class="page-content">
                    <!--breadcrumb-->
                    <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                        <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i> Customer Approval List </div>

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

                                    <div class="card-body">

                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                            <ProgressTemplate>

                                                <div class="divWaiting">
                                                    <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="../images/Spinner.gif" Width="180px" Height="180px" />
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>



                                        <hr />

                                        <div class="row">
                                            
                                            
                                            <asp:HiddenField ID="hfEmpTerrId" runat="server" />
                                    <asp:HiddenField ID="hfEmpAreaId" runat="server" />
                                    <asp:HiddenField ID="hfEmpRegionId" runat="server" />
                                    <asp:HiddenField ID="hfEmpGroupId" runat="server" />
                                            

                                            <div class="table-responsive" id="MainGradeDiv" style="height: 600px;">



                                                <asp:GridView ID="itemsGridView" runat="server" AutoGenerateColumns="False"
                                                    DataKeyNames="CustomerMasterId,CustomerApprovalId,FromEmpId,ToEmpId,Step,RoleTypeId,ToRoleTypeId,MaxStep" OnRowCommand="itemsGridView_RowCommand"
                                                    CssClass="table table-striped table-bordered" >
                                                    <Columns>
                                                        
                                                         <asp:TemplateField HeaderText="SL No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfItemNameId" Value='<%#Eval("CustomerMasterId") %>' />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />
                                                        <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                                        <asp:BoundField DataField="MarketName" HeaderText="Market" />

                                                        <asp:BoundField DataField="CustomerType" HeaderText="Customer Type" />
                                                        <asp:BoundField DataField="ProgramTypeName" HeaderText="Provider Type" />
                                                        <asp:BoundField DataField="CellNo" HeaderText="Mobile NO" />
                                                        <asp:BoundField DataField="Address" HeaderText="Address" />

                                                        <asp:BoundField DataField="DistributionRouteName" HeaderText="Distribution RouteName" />

                                                        <asp:BoundField DataField="ApprovalStatusWeb" HeaderText="Approval Status" />
                                                        <asp:BoundField DataField="WaitingForRole" HeaderText="Waiting For" />
                                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="Entry By" />
                                                        <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" />


                                                        <asp:TemplateField HeaderText="Actions">
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="hfCustomerMasterId" Value='<%#Eval("CustomerMasterId")%>' />
                                                                <asp:HiddenField runat="server" ID="hfFromEmpId" Value='<%#Eval("FromEmpId")%>' />
                                                                <asp:HiddenField runat="server" ID="hfToEmpId" Value='<%#Eval("ToEmpId")%>' />
                                                                <asp:HiddenField runat="server" ID="hfStep" Value='<%#Eval("Step")%>' />
                                                                <asp:HiddenField runat="server" ID="hfRoleTypeId" Value='<%#Eval("RoleTypeId")%>' />
                                                                <asp:HiddenField runat="server" ID="hfCustomerApprovalId" Value='<%#Eval("CustomerApprovalId")%>' />

                                                                <asp:HiddenField runat="server" ID="hfToRoleTypeId" Value='<%#Eval("ToRoleTypeId")%>' />

                                                                <asp:Label runat="server" ID="lbMsg" />
                                                                <asp:LinkButton ID="lbEdit" runat="server" class="btn-warning  btn-sm mb-1 mb-md-0"
                                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData"><i class='bx bxs-edit' aria-hidden='true'></i></asp:LinkButton>

                                                                <asp:LinkButton ID="lbApprove" runat="server" class="btn-info  btn-sm mb-1 mb-md-0"
                                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="ApproveData"><i class='fa fa-check' aria-hidden='true'></i> </asp:LinkButton>


                                                                <asp:LinkButton ID="lbReject" runat="server" class="btn-danger  btn-sm mb-1 mb-md-0"
                                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="RejectData"> </i><i class='fadeIn animated bx bx-x' aria-hidden='true'></i> </asp:LinkButton>

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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

