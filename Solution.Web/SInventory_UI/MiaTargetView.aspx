<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="MiaTargetView.aspx.cs" Inherits="SInventory_UI_MiaTargetView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
    
    
    
    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i> MIO Target List</div>
                
                <div class="ms-auto">
                    <div class="btn-group">
                         
                             <a href="MiaTargetEntry.aspx"  class="btn btn-sm btn-outline-info " ><i class="fa fa-plus" aria-hidden="true"></i> New Entry</a>
                       
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
                    

<%--                                    <div class="row">



                                        <div class="col-4">


                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-5 col-form-label">Sales Center: </label>

                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="dcDropDownList1" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2">
                                                    </asp:DropDownList>

                                                </div>

                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-5 col-form-label">From Date:  <span style="color: red">*</span></label>

                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="InvoiceDateTextBox"  runat="server" class="form-control form-control-sm mb-3 datepicker" autocomplete="off" placeholder="Select Invoice From Date"></asp:TextBox>





                                                </div>

                                            </div>

                                            <div class="form-group row" runat="server">
                                                <label for="mainName" class="col-sm-5 col-form-label">To Date:  <span style="color: red">*</span></label>

                                                <div class="col-sm-7">

                                                    <asp:TextBox ID="todateTextBox" runat="server" class="form-control form-control-sm mb-3 datepicker" autocomplete="off" placeholder="Select Invoice To Date"></asp:TextBox>




                                                </div>

                                            </div>
                                        </div>

                                        
                                    </div>

                                 
                                    <br />
                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">

                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-3 col-form-label"></label>
                                                <div class="col-sm-8">

                                                    <asp:LinkButton OnClick="SearchButton_Click" runat="server" ID="submitButton" class="btn btnMyDesignSearch   btn-sm">
                                            <i class="fa fa-print" aria-hidden="true"></i>&nbsp; View Report
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>



                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">
                                        </div>
                                    </div>--%>


                        <div class="row">
                                        <div class="table-responsive" id="MainGradeDiv">

                                              
                                            <div id="gridContainer1" style="height: 400px; overflow: auto; width: 97%; margin: 0 auto;">
                                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False" 
                                                              CssClass="table table-striped table-bordered" DataKeyNames="MiaTargetId"
                                                              onrowcommand="loadGridView_RowCommand" >
                                                    <Columns>
                                                        <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                                                        <asp:BoundField DataField="MiaCode" HeaderText="Mia Code" />
                                                        <asp:BoundField DataField="MiaName" HeaderText="Mia Name" />
                                                        <asp:BoundField DataField="MiaTargetAmount" HeaderText="Mia Target Amount " />
                                                        <asp:BoundField DataField="Period" HeaderText="Period" />
                                                        <asp:BoundField DataField="EntryBy" HeaderText="Entry By" />
                                                        <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:dd-MMM-yyyy}" />
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

                    
</ContentTemplate>
                </asp:UpdatePanel>
                            </div>
                            </div>
                            </div>
                            </div>
                            </div>
                            </div>
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    

          


</asp:Content>

