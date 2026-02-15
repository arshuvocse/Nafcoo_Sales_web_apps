<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NewMasterPage.master" AutoEventWireup="true" CodeFile="PriceGroupSetup.aspx.cs" Inherits="MasterSetup_UI_PriceGroupSetup" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.28364, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
<style type="text/css">
        /*AutoComplete flyout */
        .autocomplete_completionListElement {
            margin: 0px !important;
            background-color: White !important;
            color: windowtext !important;
            border: buttonshadow !important;
            border-width: 1px !important;
            border-style: solid !important;
            cursor: 'default' !important;
            overflow: auto!important;
            font-family: Calibri !important;
            font-size: 14px !important;
            text-align: left !important;
            list-style-type: none !important;
            margin-left: 0px !important;
            padding-left: 0px !important;
            max-height: 200px !important;
            width: 300px !important;

            overflow: auto!important;
            box-shadow: 0 0 3px 1px rgba(0,0,0,.35)!important;
        }


         .autocomplete_completionListElement222 {
            margin: 0px !important;
            background-color: White !important;
            color: windowtext !important;
            border: buttonshadow !important;
            border-width: 1px !important;
            border-style: solid !important;
            cursor: 'default' !important;
            overflow: auto!important;
            font-family: Calibri !important;
            font-size: 14px !important;
            text-align: left !important;
            list-style-type: none !important;
            margin-left: 0px !important;
            padding-left: 0px !important;
            max-height: 200px !important;
            width: 600px !important;

            overflow: auto!important;
            box-shadow: 0 0 3px 1px rgba(0,0,0,.35)!important;
        }
        /* AutoComplete highlighted item */

        .autocomplete_highlightedListItem {
            
            
              
    
            background-color: #17A2B8 !important;
            color: white !important;
            padding: 6px !important;
            font-weight: bold !important;
    
    
        }

        /* AutoComplete item */

        .autocomplete_listItem {
            padding: 6px !important;
            cursor: pointer !important;
            font-weight: bold !important;
            background-color: #fff !important;
            border-bottom: 1px solid #d4d4d4 !important; 
            box-shadow: 0 1px 1px rgba(0, 0, 0, 0.075) inset !important;
        }
    </style>

      <div id="popDiv">

</div>
    <div class="page-wrapper">
        <div class="page-content">
            <!--breadcrumb-->
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><i class="bx bx-customize"></i> Price Group  Setup</div>

                <div class="ms-auto">
                    <div class="btn-group">


                        <a href="../MasterSetup_UI/PriceGroupView.aspx" class="btn btn-sm btn-sm btn-outline-info"><i class="fa fa-backward"></i>&nbsp;Back to List</a>


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
                                    
                                    

                                             <asp:HiddenField runat="server" ID="id_mastetID"/>
                                    
                                    


                                        <div class="row">
                                            
                                            
                                            <div class="col-md-2"></div>

                                            <div class="col-md-5">
                                                <div class="form-group row">

                                                    <label for="txtGroupName" class="col-sm-4 col-form-label">Group Name: </label>

                                                    <div class="col-sm-6">
                                                        <div class="input-group">
                                                        <asp:TextBox runat="server" class="form-control form-control-sm"  id="txtGroupName" placeholder="Group Name" OnTextChanged="txtGroupName_OnTextChanged"></asp:TextBox> <span class="input-group-text text-c-red">*</span>

                                              </div>
                                                    </div>
                                                   
                                                </div>
                                            </div>

                                        </div>


              
                   
                                     


                                    




                                      
                                   




                                            <br />
                                            <div class="row">
                                                <div class="col-4">&nbsp;</div>

                                                <div class="col-6">

                                                    <div class="form-group row">
                   
                                                        <div class="col-sm-8">
                                                              <asp:LinkButton  OnClick="btnSave_Click" Visible="false" OnClientClick="return sweetAlertConfirm_Submit(this);"   runat="server" id="btnSave" class="btn btnMyDesignSearch   btn-sm"  >
                                            <i class="fa fa-check"></i>Submit
                                        </asp:LinkButton>

                                                             <asp:LinkButton  OnClick="btnSave_Click"  Visible="false"   runat="server" id="btnUpdate" class="btn btnMyDesignSearch   btn-sm" OnClientClick="return sweetAlertConfirm_Update(this);"   >
                                            <i class="fa fa-check"></i>Update
                                        </asp:LinkButton>

                                        <asp:LinkButton  runat="server"  OnClick="Unnamed_Click"  class="btn btnMyDesignReset   btn-sm"  ><i class="fa fa-retweet" aria-hidden="true"></i>&nbsp; Reset </asp:LinkButton>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="col-2">&nbsp;</div>
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

