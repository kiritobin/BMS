<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="bms.Web.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>图书综合平台</title>
    <!-- 字体图标样式 -->
    <link rel="stylesheet" href="css/font-awesome.min.css" />
    <!-- css样式 -->
    <link rel="stylesheet" href="css/material-dashboard.min.css" />
    <link rel="stylesheet" href="css/demo.css" />
    <link rel="stylesheet" href="css/zgz.css" />
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-transparent navbar-absolute fixed-top text-white" id="navigation-example">
        <div class="container">
            <div class="navbar-wrapper">
                <a class="navbar-brand" href="#pablo">图书综合管理平台</a>
            </div>
        </div>
    </nav>
    <!-- End Navbar -->
    <div class="wrapper wrapper-full-page">
        <div class="page-header login-page header-filter" filter-color="black" style="background-image: url('../imgs/login.jpg'); background-size: cover; background-position: top center;">
            <!--   you can change the color of the filter page using: data-color="blue | purple | green | orange | red | rose " -->
            <div class="container">
                <div class="col-lg-4 col-md-6 col-sm-6 ml-auto mr-auto">
                    <form class="form" method="" action="">
                        <div class="card card-login card-hidden">
                            <div class="card-header card-header-rose text-center">
                                <h4 class="card-title">登&nbsp;&nbsp;录</h4>
                            </div>
                            <div class="card-body ">
                                <div class="bmd-form-group">
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="fa fa-user fa-lg"></i>
                                            </span>
                                        </div>
                                        <input type="text" class="form-control" id="userName" placeholder="用户名..." />
                                    </div>
                                </div>
                                <div class="bmd-form-group">
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="fa fa-lock fa-lg"></i>
                                            </span>
                                        </div>
                                        <input type="password" class="form-control" id="userPwd" placeholder="密码..." />
                                    </div>
                                </div>
                                <div class="bmd-form-group">
                                    <div class="text-right">
                                        <label><a href="customerLogin.aspx">客户登录</a></label>
                                    </div>
                                </div>
                            </div>
                            <div class="card-footer justify-content-center">
                                <a href="#" id="submit" class="btn btn-danger btn-link btn-sm btnLogin">登录</a>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <!--   Core JS Files   -->
    <script src="js/jquery-3.3.1.min.js" type="text/javascript"></script>
    <script src="js/popper.min.js" type="text/javascript"></script>
    <script src="js/bootstrap-material-design.min.js" type="text/javascript"></script>
    <script src="js/demo.js"></script>
    <script src="js/login.js"></script>
    <script src="js/jsencrypt.min.js"></script>
    <script src="js/sweetalert2.js"></script>
    <!-- 登录界面js -->
    <script>
        $(document).ready(function () {
            demo.checkFullPageBackgroundImage();
            setTimeout(function () {
                // after 1000 ms we add the class animated to the login/register card
                $('.card').removeClass('card-hidden');
            }, 700);
        });
    </script>
</body>
</html>
