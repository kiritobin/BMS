<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="bms.Web.login" %>

<%="" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>云南新华书店项目综合管理系统</title>
    <!-- 字体图标样式 -->
    <link rel="stylesheet" href="css/font-awesome.min.css" />
    <!-- css样式 -->
    <link rel="stylesheet" href="css/material-dashboard.min.css" />
    <link rel="stylesheet" href="css/demo.css" />
    <link rel="stylesheet" href="css/zgz.css" />
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-transparent navbar-absolute fixed-top ">
        <div class="container-fluid">
            <div class="navbar-wrapper">
                <div class="row">
                    <a class="navbar-brand col-md-3" href="javascript:;">
                        <img class="img-fluid" src="imgs/YNXH-LOGO.png" alt="云南新华书店项目综合管理系统" /></a>
                </div>
            </div>
        </div>
    </nav>
    <!-- End Navbar -->
    <div class="wrapper wrapper-full-page">
        <div class="page-header login-page header-filter" filter-color="black" style="background-image: url('../imgs/login.jpg'); background-size: cover; background-position: top center;">
            <!--   you can change the color of the filter page using: data-color="blue | purple | green | orange | red | rose " -->
            <div class="container">
                <div class="row">
                    <h1 class="col-12 text-center">项目综合管理系统</h1>
                </div>
                <br />
                <br />
                <br />
                <div class="col-lg-4 col-md-6 col-sm-6 ml-auto mr-auto">
                    <form class="form">
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
            <!-- 主界面页脚部分 -->
            <footer class="footer">
                <div class="container-fluid">
                    <!-- 版权内容 -->
                    <div class="copyright text-center text-white">
                        &copy;
                        <script>
                            document.write(new Date().getFullYear())
                        </script>
                        &nbsp;版权归云南新华书店图书有限公司所有 滇ICP备09009025号-4
                        <p>建议使用<a href="chrome/ChromeDownload.html">Google浏览器</a>浏览网页</p>
                    </div>
                </div>
            </footer>
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
                $('.card').removeClass('card-hidden');
            }, 700);
        });
    </script>
</body>
</html>
