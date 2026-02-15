<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="TargetDeclaration.aspx.cs" Inherits="Target_UI_TargetDeclaration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
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

    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Target Declaration</div>

                <div class="ms-auto">
                    <div class="btn-group">
                        <a href="../Target_UI/TargetDeclarationView.aspx" class="btn btn-sm btn-sm btn-outline-info"><i class="fa fa-backward"></i>&nbsp;Back to List</a>
                    </div>
                </div>
            </div>
            <!--end breadcrumb-->
            <div class="row">
                <div class="col">

                    <div class="card border-top border-0 border-4 border-success">
                        <div class="card-body">

                             <div class="row mt-1">
                                <div class="col-2">&nbsp;</div>
                                <div class="col-7">
                                    <div class="form-group row">
                                        <label for="SchemaName" class="col-sm-3 col-form-label">Schema Name</label>
                                        <div class="col-sm-7">
                                            <div class="input-group">
                                                <asp:DropDownList
                                                    ID="schemaDropdown"
                                                    runat="server"
                                                    class="form-select form-select-sm mb-3 mySelect2 "
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                                <span class="input-group-text text-danger">*</span>
                                                <asp:HiddenField ID="hfSchemaId" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row mt-1">
                                <div class="col-2">&nbsp;</div>
                                <div class="col-7">
                                    <div class="form-group row">
                                        <label for="ddlYear" id="pacinTxt" class="col-sm-3 col-form-label">Year</label>
                                        <div class="col-sm-7">
                                            <div class="input-group">
                                                <asp:DropDownList
                                                    ID="ddlYear"
                                                    runat="server"
                                                    class="form-select form-select-sm mb-3 mySelect2 "
                                                    AppendDataBoundItems="true">
                                                    <asp:ListItem Text="Select Year" Value="" />
                                                </asp:DropDownList>
                                                <span class="input-group-text text-danger">*</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row mt-1">
                                <div class="col-2">&nbsp;</div>
                                <div class="col-7">
                                    <div class="form-group row">
                                        <label for="Month" class="col-sm-3 col-form-label">Month</label>
                                        <div class="col-sm-7">
                                            <div class="input-group">
                                                <asp:ListBox
                                                    runat="server"
                                                    ID="ddlMonth"
                                                    SelectionMode="Multiple"
                                                    CssClass="month-select"
                                                    ClientIDMode="Static"
                                                    name="Month"></asp:ListBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row mt-1">
                                <div class="col-2">&nbsp;</div>
                                <div class="col-7">
                                    <div class="form-group row">
                                        <label for="GroupSelect" class="col-sm-3 col-form-label">Group:  </label>

                                        <div class="col-sm-7">
                                            <div class="input-group">
                                                <asp:DropDownList runat="server" ID="GroupSelect" AutoPostBack="true" OnSelectedIndexChanged="GroupSelect_SelectedIndexChanged" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>
                                                <asp:HiddenField ID="hfMarket" runat="server" />
                                                <asp:HiddenField ID="hfSubTeritory" runat="server" />
                                                <asp:HiddenField ID="hfTeritory" runat="server" />
                                                <asp:HiddenField ID="hfArea" runat="server" />
                                                <asp:HiddenField ID="hfZone" runat="server" />
                                                <asp:HiddenField ID="hfGroupId" runat="server" />



                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row mt-1">
                                <div class="col-2">&nbsp;</div>
                                <div class="col-7">
                                    <div class="form-group row">


                                        <label for="ZoneSelect" class="col-sm-3 col-form-label">Zone:  </label>

                                        <div class="col-sm-7">
                                            <div class="input-group">
                                                <asp:DropDownList runat="server" ID="ZoneSelect" AutoPostBack="true" OnSelectedIndexChanged="ZoneSelect_SelectedIndexChanged" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>




                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="row mt-1">
                                <div class="col-2">&nbsp;</div>
                                <div class="col-7">
                                    <div class="form-group row" style="margin-top: 6px;">
                                        <label class="col-sm-3 col-form-label">Region:  </label>

                                        <div class="col-sm-7">
                                            <div class="input-group">
                                                <asp:DropDownList runat="server" ID="AreaSelect" AutoPostBack="true" OnSelectedIndexChanged="AreaSelect_SelectedIndexChanged" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>



                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>


                            <div class="row mt-1">
                                <div class="col-2">&nbsp;</div>
                                <div class="col-7">
                                    <div class="form-group row">


                                        <label for="AreaSelect" class="col-sm-3 col-form-label">Area:  </label>

                                        <div class="col-sm-7">

                                            <div class="input-group">
                                                <asp:DropDownList runat="server" ID="TeritorySelect" AutoPostBack="true" OnSelectedIndexChanged="TeritorySelect_SelectedIndexChanged" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>

                                                <span id="v-TeritorySelect" class="invalid-tooltip fade hide" data-delay="2000"></span>


                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>


                            <div class="row mt-1">
                                <div class="col-2">&nbsp;</div>
                                <div class="col-7">
                                    <div class="form-group row" style="margin-top: 6px;">

                                        <label for="MarketSelect" class="col-sm-3 col-form-label">Territory:  </label>

                                        <div class="col-sm-7">

                                            <div class="input-group">
                                                <asp:DropDownList runat="server" AutoPostBack="true"  ID="SubTeritory" class="form-select form-select-sm mb-3 mySelect2"></asp:DropDownList>



                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>


                            <br />
                            <div class="row">
                                <div class="col-2">&nbsp;</div>
                                <div class="col-7">

                                    <div class="form-group row">
                                        <label for="exampleInputUsername2" class="col-sm-3 col-form-label"></label>
                                        <div class="col-sm-9">
                                            <button type="button" id="btnSave" runat="server" class="btn btnMyDesignSearch   btn-sm" onserverclick="SaveData">
                                                <i class="fa fa-check"></i>Submit
                                            </button>
                                            <button onserverclick="SaveData" Visible="false" runat="server" ID="btnUpdate" class="btn btnMyDesignSearch   btn-sm" OnClientClick="return sweetAlertConfirm_Update(this);"></button>
                                            <button type="button" class="btn btnMyDesignReset   btn-sm" onclick="ResetLink()"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </button>

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

     <script>


         $(function () {

             $('.datepicker').pickadate({
                 selectMonths: true,
                 selectYears: true
             })

             $('.month-select').select2({
                 theme: 'bootstrap4',
                 width: '100%',
                 placeholder: 'Select month(s)',
                 allowClear: true
             });

             var isEditMode = $('#<%= hfSchemaId.ClientID %>').val() !== '';

             console.log("isEditMode: "+isEditMode);
            if (isEditMode) {
                // Destroy the existing select2 instance
                $('.month-select').select2('destroy');
            
                // Reinitialize with single selection
                $('.month-select').select2({
                    theme: 'bootstrap4',
                    width: '100%',
                    placeholder: 'Select month',
                    allowClear: true,
                    maximumSelectionLength: 1 // Force single selection
                });
            
                // Also remove the multiple attribute from the original select
                $('#<%= ddlMonth.ClientID %>').removeAttr('multiple');
            }
             

             $('.mySelect2').select2({
                 theme: 'bootstrap4',
                 width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
                 placeholder: $(this).data('placeholder'),
                 allowClear: Boolean($(this).data('allow-clear')),
             });
         });
     </script>
}}
}


</asp:Content>

