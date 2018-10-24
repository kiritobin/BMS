<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changePwd.aspx.cs" Inherits="bms.Web.changePwd" %>

<!DOCTYPE html>
<!--[if lt IE 7]>      <html class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
<!--[if IE 7]>         <html class="no-js lt-ie9 lt-ie8"> <![endif]-->
<!--[if IE 8]>         <html class="no-js lt-ie9"> <![endif]-->
<!--[if gt IE 8]><!-->
<html class="no-js">
<!--<![endif]-->

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>修改密码</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 字体图标样式 -->
    <link rel="stylesheet" href="css/font-awesome.min.css">
    <!-- css样式 -->
    <link rel="stylesheet" href="css/material-dashboard.min.css">
    <link rel="stylesheet" href="css/zgz.css">
</head>

<body>
    <!--[if lt IE 7]>
            <p class="browsehappy">You are using an <strong>outdated</strong> browser. Please <a href="#">upgrade your browser</a> to improve your experience.</p>
        <![endif]-->
    <div class="wrapper ">
        <div class="main-panel" style="margin: 0px auto; float: none;">
            <!-- 主界面头部面板 -->
            <!-- 主界面头部面板 -->
            <nav class="navbar navbar-expand-lg navbar-transparent navbar-absolute fixed-top ">
                <div class="container">
                    <div class="navbar-wrapper">
                    </div>
                    <a class="btn btn-white btn-sm" href="javascript:logout();">退出系统</a>
                </div>
            </nav>
            <!-- 主界面内容 -->
            <div class="content">
                <div class="container-fluid">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-6 col-lg-5" style="margin: 0px auto;">
                                <div class="card ">
                                    <div class="card-header card-header-danger">
                                        <h4 class="card-title">修改密码</h4>
                                    </div>
                                    <div class="card-body ">
                                        <div class="form-group">
                                            <label for="oldPwd" class="bmd-label-floating">旧密码：</label>
                                            <input type="password" class="form-control" id="oldPwd" required="true">
                                        </div>
                                        <div class="form-group">
                                            <label for="newPwd" class="bmd-label-floating">新密码：</label>
                                            <input type="password" class="form-control" id="newPwd" required="true"
                                                name="password">
                                        </div>
                                        <div class="form-group">
                                            <label for="confirPwd" class="bmd-label-floating">
                                                确认密码：
                                            </label>
                                            <input type="password" class="form-control" id="confirmpwd" required="true" equalto="#newPwd" name="confirPwd">
                                        </div>
                                    </div>
                                    <div class="card-footer ml-auto mr-auto">
                                        <button class="btn btn-sm btn-outline-danger" id="btnReset">重置</button>
                                        <button class="btn btn-sm btn-outline-info" id="btnchange">确认</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- 主界面页脚部分 -->
            <footer class="footer">
                <div class="container-fluid">
                    <!-- 版权内容 -->
                    <div class="copyright text-center">
                        &copy;
                    <script>
                        document.write(new Date().getFullYear())
                    </script>
                        &nbsp;版权归云南新华书店图书有限公司所有
                        <p>建议使用<a href="../chrome/ChromeDownload.html">Google浏览器</a>浏览网页</p>
                    </div>
                </div>
            </footer>
        </div>
    </div>
    <script src="js/jquery-3.3.1.min.js"></script>
    <!-- 左侧导航栏所需js -->
    <script src="js/popper.min.js"></script>
    <script src="js/bootstrap-material-design.min.js"></script>
    <!-- 移动端手机菜单所需js -->
    <script src="js/perfect-scrollbar.jquery.min.js"></script>
    <script src="js/material-dashboard.min.js"></script>
    <!-- validate.js -->
    <script src="js/jquery.validate.min.js"></script>
    <script src="js/sweetalert2.js"></script>
    <script src="js/changePwd.js"></script>
    <script src="js/public.js"></script>
</body>

</html>

