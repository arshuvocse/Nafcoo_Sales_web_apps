<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="TopSheetGenerateByRouteView.aspx.cs" Inherits="SInventory_UI_TopSheetGenerateByRouteView" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.28364, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <style type="text/css">
        .button-padding-right {
            margin-right: 5px;
        }

        .SelectchkChoice label {
            padding-left: 4px;
            font-weight: bold;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i> Top Sheet List </div>

                <div class="ms-auto">
                    <div class="btn-group">
                        <asp:LinkButton ID="EmpCetegoryAddImageButton" CssClass="btn btn-sm btn-outline-info " runat="server" OnClick="EmpCetegoryAddImageButton_Click"><i class="fa fa-plus" aria-hidden="true"></i> New Entry </asp:LinkButton>
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

                             <%--       <script type="text/javascript">
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

                                        var dateNow = new Date();
                                        $('.datepickess').datepicker("setDate", dateNow);
                                        minDate: new Date() // to disable privious dates 
                                    </script>--%>
                                    
                                    
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

                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">

                                                  <div class="form-group row">
          <label for="mainName" class="col-sm-3 col-form-label">Sales Center:</label>

          <div class="col-sm-5">


              <asp:DropDownList ID="salesCenterDropDownList" runat="server"
                  CssClass="form-control form-control-sm mySelect2" AutoPostBack="true" OnSelectedIndexChanged="salesCenterDropDownList_SelectedIndexChanged" >
              </asp:DropDownList>

              <asp:HiddenField ID="masterHiddenFieldId" runat="server" />


          </div>
          <span class="text-sm-left text-c-red">*</span>
      </div>

                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label">Delivery Man:</label>

                                                <div class="col-sm-5">

                                                    <asp:DropDownList ID="ddlDA" runat="server"
                                                        CssClass="form-control form-control-sm mySelect2">
                                                    </asp:DropDownList>


                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
                                            </div>
                                            
                                            
                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label">From Date</label>

                                                <div class="col-sm-5">

                                            
                                                    
                                                    <asp:TextBox runat="server"  ID="txtFromDate"  CssClass="form-control form-control-sm datepicker"></asp:TextBox>


                                                </div>
                                               <%-- <span class="text-sm-left text-c-red">*</span>--%>
                                            </div>
                                            
                                            
                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label">To Date</label>

                                                <div class="col-sm-5">

                                                    <asp:TextBox runat="server"  ID="txtToDate" CssClass="form-control form-control-sm datepicker"></asp:TextBox>



                                                </div>
                                               <%-- <span class="text-sm-left text-c-red">*</span>--%>
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

                                                    <asp:LinkButton OnClick="Button1_Click" runat="server" ID="submitButton" class="btn btnMyDesignSearch btn-sm"> <i class="fa fa-search"></i> Search </asp:LinkButton>
                                                    <asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>


                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">&nbsp;</div>
                                    </div>

                                    <br />


                                    <div class="row">
                                        <div class="table-responsive" id="MainGradeDiv">

                                            <asp:GridView ID="orderGridView" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered  text-center thead-dark" OnRowCommand="loadGridView_RowCommand"  OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="TopSheetGenReportId">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="TopSheetGenCode" HeaderText="Top Sheet Code" />
                                                    <asp:BoundField DataField="DeliveryMan" HeaderText="Delivery Man" />
                                                    <asp:BoundField DataField="EntryBy" HeaderText="Entry By" />
                                                    <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                                    <asp:BoundField DataField="UpdateBy" HeaderText="Update By" />
                                                    <asp:BoundField DataField="UpdateDate" HeaderText="Update Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:TemplateField HeaderText="Actions">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="editImageButton" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                OnClientClick="return confirm('Are you sure you want to Edit ?');"
                                                                CommandName="EditData" ImageUrl="~/images/edit.png" />
                                                            <%--<asp:ImageButton ID="deleteImageButton" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                CommandName="DeleteData" OnClientClick="return confirm('Are you sure you want to Delete ?');" ImageUrl="~/images/lineDelete.png" />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reports">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="topSheetButton" CssClass="btn btn-sm btn-info mb-2" runat="server" OnClick="topSheetButton_Click" ><i class="fa fa-print"></i> Top sheet </asp:LinkButton> 
                                                            <asp:LinkButton ID="pcSlipButton" CssClass="btn btn-sm btn-warning mb-2" runat="server" OnClick="pcSlipButton_Click" ><i class="fa fa-print"></i> Picking Slip </asp:LinkButton> 
<%--                                                            <asp:Button ID="topSheetButton" runat="server" Text="Generate Topsheet" CssClass="btn btn-sm  btn-info" OnClick="" />
                                                            <asp:Button ID="pcSlipButton" runat="server" Text="Generate Picking Slip >>" CssClass="btn btn-sm  btn-info" OnClick="pcSlipButton_Click" />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
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
</asp:Content>

