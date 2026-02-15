<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="WorkTypeEntry.aspx.cs" Inherits="MasterSetup_UI_WorkTypeEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    

    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i>Work Type Entry</div>

                <div class="ms-auto">
                    <div class="btn-group">
                        <asp:LinkButton ID="detailsViewButton" CssClass="btn btn-sm btn-sm btn-outline-info" runat="server" OnClick="detailsViewButton_Click"> <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
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
                                    </script>





                                    <div class="row">
                                        <div class="col-2">&nbsp;</div>
                                        <div class="col-8">
                                            <div class="form-group row">
                                                <label for="mainName" class="col-sm-3 col-form-label"> Work Type: </label>

                                                <div class="col-sm-5">


                                                     <asp:TextBox ID="tbxWorkType" runat="server" CssClass="form-control form-control-sm mb-3"></asp:TextBox>
                                                     

                                                    <asp:HiddenField ID="masterHiddenFieldId" runat="server" />


                                                </div>
                                                <span class="text-sm-left text-c-red">*</span>
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

                                                    <asp:LinkButton OnClick="submitButton_Click" runat="server" ID="submitBtn" class="btn btnMyDesignSearch   btn-sm"><i class="fa fa-search"></i> Save  </asp:LinkButton>
                                                    <asp:LinkButton runat="server" OnClick="resetButton_Click" class="btn btnMyDesignReset btn-sm"><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>


                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-2">&nbsp;</div>
                                    </div>

                                    <br />

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    

</asp:Content>

