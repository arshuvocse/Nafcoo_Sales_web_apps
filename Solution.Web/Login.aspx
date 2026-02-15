<%@ Page Title="SalesRoll || Login" Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">

    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>SalesRoll || Login </title>

    <link href="VerticalAsset/plugins/simplebar/css/simplebar.css" rel="stylesheet" />
    <link href="VerticalAsset/plugins/perfect-scrollbar/css/perfect-scrollbar.css" rel="stylesheet" />
    <link href="VerticalAsset/plugins/metismenu/css/metisMenu.min.css" rel="stylesheet" />
    <!-- loader-->
    <link href="VerticalAsset/css/pace.min.css" rel="stylesheet" />
    <script src="VerticalAsset/js/pace.min.js"></script>
    <!-- Bootstrap CSS -->
    <link href="VerticalAsset/css/bootstrap.min.css" rel="stylesheet">
    <link href="VerticalAsset/css/bootstrap-extended.css" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;500&amp;display=swap" rel="stylesheet">
    <link href="VerticalAsset/css/app.css" rel="stylesheet">
    <link href="VerticalAsset/css/icons.css" rel="stylesheet">

    <script src="VerticalAsset/js/bootstrap.bundle.min.js"></script>
    <!--plugins-->
    <script src="VerticalAsset/js/jquery.min.js"></script>
    <script src="VerticalAsset/plugins/simplebar/js/simplebar.min.js"></script>
    <script src="VerticalAsset/plugins/metismenu/js/metisMenu.min.js"></script>
    <script src="VerticalAsset/plugins/perfect-scrollbar/js/perfect-scrollbar.js"></script>
    <!--Password show & hide js -->
    <script>
        $(document).ready(function () {
            $("#show_hide_password a").on('click', function (event) {
                event.preventDefault();
                if ($('#show_hide_password input').attr("type") == "text") {
                    $('#show_hide_password input').attr('type', 'password');
                    $('#show_hide_password i').addClass("bx-hide");
                    $('#show_hide_password i').removeClass("bx-show");
                } else if ($('#show_hide_password input').attr("type") == "password") {
                    $('#show_hide_password input').attr('type', 'text');
                    $('#show_hide_password i').removeClass("bx-hide");
                    $('#show_hide_password i').addClass("bx-show");
                }
            });
        });
    </script>
    <!--app JS-->
    <script src="VerticalAsset/js/app.js"></script>

    <style>
        .centerimg {
            display: block;
            margin-left: auto;
            margin-right: auto;
            width: 50%;
        }
    </style>
</head>
<body>

    <div class="wrapper" style="background-color: #8833ff !important;">
        <div class="authentication-header"></div>
        <div class="section-authentication-signin d-flex align-items-center justify-content-center my-5 my-lg-0">
            <div class="container-fluid">
                <div class="row row-cols-1 row-cols-lg-2 row-cols-xl-3">
                    <div class="col mx-auto">
                        <div class="mb-4 text-center">
                            <%--	<img src="LoginAssets/images/smc.png" style="width: 180px;   " alt="Alternate Text" /> --%>
                            <%--	<img src="assets/images/logo-img.png" width="180" alt="" />--%>
                        </div>
                        <div class="card">
                            <div class="card-body">
                                <div class="p-4 rounded">
                                    <div class="text-center">
                                        
                                        <asp:Label Style="margin-top: 13px !important; padding: 2px !important; font-size: 1.575em;"  ID="ExpireMsg" runat="server" Visible="False" ></asp:Label>

                                        <br/>
                                        <%--<h3 class="">SalesRoll</h3>--%>
                                        <h3 class="">Login</h3>
                                        <asp:Label Style="margin-top: 13px !important; padding: 2px !important; font-size: 1.575em;"  ID="msgLabel" runat="server" ></asp:Label>
                                    </div>
                                    <div class="d-grid centerimg">
                                        <%--<img src="LoginAssets/images/smc.png" style="width: 180px;" alt="Alternate Text" />--%>
                                    </div>

                                    <div class="form-body">
                                        <form id="Form1" runat="server" class="row g-3" defaultbutton="loginButton">

                                            <div class="col-12">
                                                <label for="inputEmailAddress" class="form-label">User Name</label>

                                                <asp:TextBox ID="userNameTextBox" runat="server" CssClass="form-control" placeholder="Enter username"></asp:TextBox>
                                            </div>
                                            <div class="col-12">
                                                <label for="inputChoosePassword" class="form-label">Enter Password</label>
                                                <div class="input-group" id="show_hide_password">

                                                    <asp:TextBox ID="passwordTextBox" runat="server" CssClass="form-control border-end-0" placeholder="Enter password"
                                                        TextMode="Password"></asp:TextBox>
                                                    <a href="javascript:;" class="input-group-text bg-transparent"><i class='bx bx-hide'></i></a>
                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div class="d-grid">

                                                    <asp:LinkButton ID="loginButton" runat="server" CssClass="btn btn-primary"
                                                        OnClick="loginButton_Click"><i class="bx bxs-lock-open"></i>Sign in</asp:LinkButton>

                                                  
                                                </div>
                                            </div>

                                            <div class="col-md-12 text-end">

                                                <a href="APK_File/unicorn_salesroll.apk"  download rel="noopener noreferrer" title="Click to download APK File" target="_blank" class="btn btn-success">Download SalesRoll Apps APK File</a>
												</div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--end row-->
            </div>
        </div>
    </div>


</body>
</html>
