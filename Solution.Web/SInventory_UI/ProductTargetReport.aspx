<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="ProductTargetReport.aspx.cs" Inherits="SInventory_UI_ProductTargetReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div id="popDiv">
    </div>

    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Product Target Report</div>

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
                            <div class="row ">

                                <div class="col-3">&nbsp;</div>
                                <div class="col-6">
                               
                                    <div class="form-group row mb-4">
                                        <label for="txtTargetCategory" class="col-sm-4 col-form-label">Select Target Category: </label>
                                        
                                        
                                        <div class="col-sm-3">
                                            <div class="input-group">
                                                
                                                <asp:DropDownList ID="TargetCategoryDropDownList" AutoPostBack="true" OnTextChanged="TargetCategoryDropDownList_OnTextChanged" ToolTip="" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>


                                            </div>

                                        </div>
                                    </div>
 

                                </div>
                                
                                <div class="table-responsive" id="MainGradeDiv">
                            <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-hover"
                                DataKeyNames="TargetDetailsId" OnRowCommand="loadGridView_RowCommand" OnPreRender="gv_DocumentUpload_PreRender">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            <asp:HiddenField runat="server" ID="hfGatePassMasterId" Value='<%#Eval("TargetDetailsId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                  
                                    <asp:BoundField DataField="TargetCategory" HeaderText="Target Category" />
                                    <asp:BoundField DataField="TotalTargetByTp" HeaderText="Total Target (TP)" />
                                   <asp:BoundField DataField="TotalTargetByTpVat" HeaderText="Total Target (TP+VAT)" />
                                   <asp:BoundField DataField="ProductCode" HeaderText="Product Code" />
                                   <asp:BoundField DataField="Description" HeaderText="Description" />
                                   <asp:BoundField DataField="PackSize" HeaderText="Pack Size" />
                                   <asp:BoundField DataField="TargetQty" HeaderText="Target Qty" />
                                   <asp:BoundField DataField="TpPerPack" HeaderText="TP/Pack" />
                                   
                                   <asp:BoundField DataField="TargetValueByTp" HeaderText="Total Target (TP)" />
                                   <asp:BoundField DataField="TargetValueByTpVat" HeaderText="Total Target (TP+VAT)" />
                                   

                                   
                                </Columns>
                            </asp:GridView>
                                    
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
    
    <script>

        $(document).ready(function () {

            var table = $('#ContentPlaceHolder1_loadGridView').DataTable(
                {
                    "bInfo": true,
                    "bFilter": true,
                    lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
                    pageLength: 10,
                    dom: 'lBfrtip',


                    buttons: ['excel']
                }
            );

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        table = $('#ContentPlaceHolder1_loadGridView').DataTable(
                            {
                                "bInfo": true,
                                "bFilter": true,
                                lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
                                pageLength: 10,
                                dom: 'lBfrtip',


                                buttons: ['excel']


                            }
                        );
                    }
                });
            };


            table.columns().every(function () {
                var that = this;

               
            });
        });


    </script>

</asp:Content>



