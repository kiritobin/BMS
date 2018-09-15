<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="bms.Web.main" %>

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
    <title>图书综合管理系统</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 材料图标样式 -->
    <link rel="stylesheet" href="css/materialdesignicons.css">
    <!-- 字体图标样式 -->
    <link rel="stylesheet" href="css/font-awesome.min.css">
    <!-- css样式 -->
    <link rel="stylesheet" href="css/material-dashboard.min.css">
</head>

<body>
    <!--[if lt IE 7]>
            <p class="browsehappy">You are using an <strong>outdated</strong> browser. Please <a href="#">upgrade your browser</a> to improve your experience.</p>
        <![endif]-->
    <div class="wrapper ">
        <!-- 左侧垂直导航 -->
        <div class="sidebar" data-color="danger" data-background-color="white" data-image="imgs/sidebar-2.jpg">
            <!--
                Tip 1: 需要改变导航条的颜色可以修改: data-color="purple | azure | green | orange | danger"
        
                Tip 2: 需要改变导航条的背景图片可以修改 data-image tag
            -->
            <!-- 平台字体logo -->
            <div class="logo">
                <a href="javascript:;" class="simple-text logo-normal">
                    图书综合平台
                </a>
            </div>
            <div class="sidebar-wrapper">
                <ul class="nav">
                    <li class="nav-item active  ">
                        <a class="nav-link" href="javascript:;">
                            <i class="material-icons">security</i>
                            <p>权限管理</p>
                        </a>
                    </li>
                    <li class="nav-item ">
                        <a class="nav-link" href="javascript:;">
                            <i class="material-icons">person</i>
                            <p>客户管理</p>
                        </a>
                    </li>
                    <li class="nav-item ">
                        <a class="nav-link" href="javascript:;">
                            <i class="material-icons">book</i>
                            <p>库存管理</p>
                        </a>
                    </li>
                    <li class="nav-item ">
                        <a class="nav-link" href="javascript:;">
                            <i class="material-icons">library_books</i>
                            <p>销售管理</p>
                        </a>
                    </li>
                    <li class="nav-item ">
                        <a class="nav-link" href="javascript:;">
                            <i class="material-icons">bubble_chart</i>
                            <p>基础信息</p>
                        </a>
                    </li>
                    <li class="nav-item ">
                        <a class="nav-link" href="javascript:;">
                            <i class="material-icons">lock</i>
                            <p>修改密码</p>
                        </a>
                    </li>
                    <li class="nav-item ">
                        <a class="nav-link" href="javascript:;">
                            <i class="material-icons">close</i>
                            <p>退出系统</p>
                        </a>
                    </li>
                    <li class="nav-item active-pro ">
                        <a class="nav-link" href="javascript:;">
                            <i class="material-icons">unarchive</i>
                            <p>关于我们</p>
                        </a>
                    </li>
                </ul>
            </div>
        </div>
        <div class="main-panel">
            <!-- 主界面头部面板 -->
            <nav class="navbar navbar-expand-lg navbar-transparent navbar-absolute fixed-top ">
                <div class="container-fluid">
                    <div class="navbar-wrapper">
                        <a class="navbar-brand" href="#pablo">权限管理</a>
                    </div>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" aria-controls="navigation-index"
                        aria-expanded="false" aria-label="Toggle navigation">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="navbar-toggler-icon icon-bar"></span>
                        <span class="navbar-toggler-icon icon-bar"></span>
                        <span class="navbar-toggler-icon icon-bar"></span>
                    </button>
                    <!-- 导航栏 -->
                    <div class="collapse navbar-collapse justify-content-end">
                        <ul class="navbar-nav">
                            
                        </ul>
                    </div>
                </div>
            </nav>
            
            <!-- 主界面内容 -->
            <div class="content">
                <div class="container-fluid">
                    
                </div>
            </div>

            <!-- 主界面页脚部分 -->
            <footer class="footer">
                <div class="container-fluid">
                    <nav class="float-left">
                        <ul>
                            <li>
                                <a href="javascript:;">
                                    一个只做图书的销售平台
                                </a>
                            </li>
                        </ul>
                    </nav>
                    <!-- 版权内容 -->
                    <div class="copyright float-right">
                        &copy;
                        <script>
                            document.write(new Date().getFullYear())
                        </script>, made with <i class="material-icons">favorite</i> by
                        <a href="javascript:;" target="_blank"></a> for a better web.
                    </div>
                </div>
            </footer>
        </div>
    </div>
    <script src="js/jquery-3.3.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <!-- 左侧导航栏所需js -->
    <script src="js/popper.min.js"></script>
    <script src="js/bootstrap-material-design.min.js"></script>
    <!-- 移动端手机菜单所需js -->
    <script src="js/perfect-scrollbar.jquery.min.js"></script>
    <script src="js/material-dashboard.min.js"></script>
</body>

</html>
