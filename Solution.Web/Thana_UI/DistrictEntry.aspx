<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="DistrictEntry.aspx.cs" Inherits="Thana_UI_DistrictEntry" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.28364, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <style>

    .form-switch {
        padding-left: 2.5em;
    }

    .form-check {
        display: block;
        min-height: 1.5rem;
        padding-left: 1.5em;
        margin-bottom: .125rem;
    }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i> District Setup </div>

                <div class="ms-auto">
                    <div class="btn-group">


                        <a href="District_View.aspx" class="btn btn-sm btn-sm btn-outline-info"><i class="fa fa-backward"></i>&nbsp;Back to List</a>


                    </div>
                </div>
            </div>
            <!--end breadcrumb-->
            <div class="row">
                <div class="col">

                    <div class="card border-top border-0 border-4 border-success">
                        <div class="card-body">
                            <br />
 
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
                                    <label class="col-sm-3 col-form-label"> Division Name:</label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>
                                        <asp:HiddenField ID="hdfDistrictId" runat="server" />
                                    </div>
                                    <span class="text-sm-left text-c-red">*</span>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-2">&nbsp;</div>
                            <div class="col-8">
                                <div class="form-group row">
                                    <label for="ThanaName" class="col-sm-3 col-form-label">District Name:</label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="tbxDistrict" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    <span class="text-sm-left text-c-red">*</span>
                                </div>

                            </div>
                        </div>
                            
                       <div class="row">
                            <div class="col-2">&nbsp;</div>
                            <div class="col-8">
                                <div class="form-group row">
                                    <label for="ThanaName" class="col-sm-3 col-form-label">Latitude:</label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="tbxLatitude" runat="server" class="form-control form-control-sm number"></asp:TextBox>
                               <%--         <asp:FilteredTextBoxExtender ID="fcurrentStockTextBox" runat="server"
                                                                        TargetControlID="tbxLatitude"
                                                                        FilterType="Custom, Numbers"
                                                                        />--%>
                                    </div>
                                    
                                </div>

                            </div>
                        </div>
                            
                        <div class="row">
                            <div class="col-2">&nbsp;</div>
                            <div class="col-8">
                                <div class="form-group row">
                                    <label for="ThanaName" class="col-sm-3 col-form-label">Longitude:</label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="tbxLongitude" runat="server" class="form-control form-control-sm number"></asp:TextBox>
                                       <%-- <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                        TargetControlID="tbxLongitude"
                                                                        FilterType="Custom, Numbers"
                                                                        />--%>
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
                                    <div class="col-sm-9">
                                         <asp:LinkButton OnClick="SearchButton_Click" runat="server" ID="submitButton" class="btn btnMyDesignSearch   btn-sm">
                                            <i class="fa fa-print" aria-hidden="true"></i>&nbsp; Update
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" OnClick="cancelButton_Click" class="btn btnMyDesignReset   btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>
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
 



<input id="masterId" value="0" style="display:none" />

</asp:Content>

