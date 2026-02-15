<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="ChallanReport.aspx.cs" Inherits="Reports_UI_ChallanReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
    

                                                <script type="text/javascript">
                                                      function pageLoad() {

                                                          $('.multiple-select').select2({
                                                              includeSelectAllOption: true,
                                                              theme: 'bootstrap4',
                                                              width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
                                                              placeholder: $(this).data('placeholder'),
                                                              allowClear: Boolean($(this).data('allow-clear')),
                                                          });
                                                          $('.datepicker').pickadate({
                                                              selectMonths: true,
                                                              selectYears: true
                                                          });
                                                  $('.mySelect2').select2({
                                                      theme: 'bootstrap4',
                                                      width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
                                                      placeholder: $(this).data('placeholder'),
                                                      allowClear: Boolean($(this).data('allow-clear')),
                                                  });

                                                             $(".fancybox").fancybox({
              openEffect: "none",
              closeEffect: "none"
          });

          $(".zoom").hover(function () {

              $(this).addClass('transition');
          }, function () {

              $(this).removeClass('transition');
          });
                                              }
                                     
                                                  </script>
   
     <div id="popDiv"></div>
    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i> Challan Report</div>
                
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
                           
                                      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>

                                    <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                       
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="../images/Spinner.gif" Width="180px" Height="180px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                                    

                                    <div class="row">
                                                           <div class="col-md-2">
                                                               </div>
                            <div class="col-md-5">
                              
                            <div class="form-group row" runat="server">
                                <label for="mainName" class="col-sm-5 col-form-label">From Date:  <span style="color: red">*</span></label>

                                <div class="col-sm-7">
                                    <asp:TextBox ID="txtInvoiceFromDate" runat="server" class="form-control form-control-sm mb-3 datepicker" autocomplete="off" placeholder="Select Invoice From Date"></asp:TextBox>

                            </div>
                            
                            </div>
                                
                                <div class="form-group row" runat="server">
                                    <label for="mainName" class="col-sm-5 col-form-label">From Date:  <span style="color: red">*</span></label>

                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txtInvoiceTodate"  runat="server" class="form-control form-control-sm mb-3 datepicker" autocomplete="off" placeholder="Select Invoice to Date"></asp:TextBox>

                                    </div>
                                </div>
                            
                            
                            </div>


                    
                        <div class="row">
                            <div class="col-md-5">
                            </div>
                            <div class="col-md-4" style="align-content:center">

                                   <asp:LinkButton runat="server"  id="btnSearch" class="btn btnMyDesignSearch   btn-sm "  OnClick="btnSearch_OnClick">  <i class="fa fa-search-plus"></i>&nbsp; Search</asp:LinkButton>
                                  
                                
                               <asp:LinkButton  runat="server" class="btn btnMyDesignReset   btn-sm"   id="resetBtn" ><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>
                                
                            </div>
                        </div>
                                           <div class="row">
                 <div class="col-md-12">
                                       <label>  </label>
                                       </div>
                                   
                                   
                                   <div class="col-md-2">
                                       
                                       
                                       </div>
                                   <div class="col-md-2">
                                       
                                       
                                       </div>
                                   <div class="col-md-2">
                                       
                                       
                                       </div>
                                     <div class="col-md-1">
                                       
                                       
                                       </div>
                                   <div class="col-md-1">
                                       
                                       
                                       </div>
                                   <div class="col-md-1">
                                       
                                       
                                       </div>
                                  
                                  
                                     <div class="col-md-3 ">
                                         <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btn btn-success pull-right" OnClick="btnExportToExcel_Click"  ><span aria-hidden="true" class="fa fa-file-excel-o" ></span> &nbsp;Export To Excel</asp:LinkButton>
                                       
                                      
                                       
        
  </div>
                     </div>
                                 
                        <div style="padding-top:10px;"></div>
                                             <div class="table-responsive" id="MainGradeDiv">

                                                 <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                          
                               CssClass="table table-striped table-bordered" OnPreRender="gv_DocumentUpload_PreRender">
                                <Columns>

                                     <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                         
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FromComUnitCode" HeaderText="From Unit Code" />
                                    <asp:BoundField DataField="FromComUnitName" HeaderText="From Unit" />
                                    <asp:BoundField DataField="ToComUnitCode" HeaderText="To Unit Code" />
                                    <asp:BoundField DataField="ToComUnitName" HeaderText="To Unit" />
                                   <asp:BoundField DataField="ProductCode" HeaderText="Product Code" />

                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"/>

                                    <asp:BoundField DataField="ChalanNo" HeaderText="Chalan No"/>
                                    <asp:BoundField DataField="ChalanDate" DataFormatString="{0:dd/MM/yyyy}"  HeaderText="Chalan Date"/>
                                    <asp:BoundField DataField="IsDeliver" HeaderText="IsDeliver"/>     
                                    

                                </Columns>
                            </asp:GridView>
                         
                        </div>

                                   </ContentTemplate>
                                            <Triggers>
                    <asp:PostBackTrigger ControlID="btnExportToExcel" /> 
                </Triggers>
                                </asp:UpdatePanel>
                                            </div>
                                            
                                            </div>
                                            </div>
                                            </div>
                                            </div>
                                            </div>
                                             
</asp:Content>

