<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userManagement.aspx.cs" Inherits="bms.Web.AccessMGT.userManagement" %>

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
    <link rel="stylesheet" href="../css/materialdesignicons.css">
    <!-- 字体图标样式 -->
    <link rel="stylesheet" href="../css/font-awesome.min.css">
    <!-- css样式 -->
    <link rel="stylesheet" href="../css/material-dashboard.min.css">
    <link rel="stylesheet" href="../css/zgz.css">
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
                <a href="javascript:;" class="simple-text logo-normal">图书综合平台
                </a>
            </div>
            <div class="sidebar-wrapper">
                <ul class="nav">
                    <li class="nav-item active">
                        <a class="nav-link" href="#securityManage" data-toggle="collapse" aria-expanded="false">
                            <i class="material-icons">security</i>
                            <p>权限管理</p>
                        </a>
                        <ul id="securityManage" class="collapse panel-body">
                            <li class="list-group-item"><a href="javascript:;">用户管理</a></li>
                            <li class="list-group-item"><a href="javascript:;">角色管理</a></li>
                            <li class="list-group-item"><a href="javascript:;">功能管理</a></li>
                        </ul>
                    </li>

                    <li class="nav-item">
                        <a class="nav-link" href="#userManage" data-toggle="collapse" aria-expanded="false">
                            <i class="material-icons">person</i>
                            <p>客户管理</p>
                        </a>
                        <ul id="userManage" class="collapse panel-body">
                            <li class="list-group-item"><a href="javascript:;">客户信息管理</a></li>
                            <li class="list-group-item"><a href="javascript:;">客户馆藏数据</a></li>
                        </ul>
                    </li>
                    <li class="nav-item ">
                        <a class="nav-link" href="#inventoryManage" data-toggle="collapse" aria-expanded="false">
                            <i class="material-icons">book</i>
                            <p>库存管理</p>
                        </a>
                        <ul id="inventoryManage" class="collapse">
                            <li class="list-group-item"><a href="javascript:;">出库</a></li>
                            <li class="list-group-item"><a href="javascript:;">入库</a></li>
                            <li class="list-group-item"><a href="javascript:;">退货</a></li>
                        </ul>
                    </li>
                    <li class="nav-item ">
                        <a class="nav-link" href="#saleManage" data-toggle="collapse" aria-expanded="false">
                            <i class="material-icons">library_books</i>
                            <p>销售管理</p>
                        </a>
                        <ul id="saleManage" class="collapse">
                            <li class="list-group-item"><a href="javascript:;">销售</a></li>
                            <li class="list-group-item"><a href="javascript:;">销退</a></li>
                        </ul>
                    </li>
                    <li class="nav-item " href="#baseManage" data-toggle="collapse" aria-expanded="false">
                        <a class="nav-link" href="javascript:;">
                            <i class="material-icons">bubble_chart</i>
                            <p>基础信息</p>
                        </a>
                        <ul id="baseManage" class="collapse">
                            <li class="list-group-item"><a href="javascript:;">价位管理</a></li>
                            <li class="list-group-item"><a href="javascript:;">书籍基础数据管理</a></li>
                            <li class="list-group-item"><a href="javascript:;">组织管理</a></li>
                        </ul>
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
                        <form class="navbar-form">
                        </form>
                        <ul class="navbar-nav">
                            <li class="nav-item dropdown">
                                <a class="nav-link" href="javascript:;" id="navbarDropdownMenuLink" data-toggle="dropdown"
                                    aria-haspopup="true" aria-expanded="false">
                                    <i class="material-icons">settings</i>
                                    <p class="d-lg-none d-md-block">
                                        更多设置
                                    </p>
                                </a>
                                <div class="dropdown-menu dropdown-menu-right" aria-labelledby="navbarDropdownMenuLink">
                                    <a class="dropdown-item" href="#">个人中心</a>
                                    <a class="dropdown-item" href="#">修改密码</a>
                                    <a class="dropdown-item" href="#">退出系统</a>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
            <!-- 主界面内容 -->
            <div class="content">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-header card-header-danger">
                                    <h4 class="card-title ">角色管理</h4>
                                    <!-- <p class="card-category"> Here is a subtitle for this table</p>-->
                                </div>
                                <div class="card-body">
                                    <div class="card-header from-group">
                                        <div class="input-group no-border">
                                        <input type="text" value="" class="form-control col-sm-2" placeholder="请输入查询条件">
                                        <button class="btn btn-success btn-sm"><i class="fa fa-search fa-lg"></i>&nbsp;&nbsp;查询</button>
                                    </div>
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table">
                                            <thead class="text-danger">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                角色
                                            </th>
                                            <th>
                                                角色名
                                            </th>
                                            <th>
                                                备注
                                            </th>
                                            <th>
                                                操作
                                            </th>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        1
                                                    </td>
                                                    <td>
                                                        10001
                                                    </td>
                                                    <td>
                                                        门卫
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <button class="btn btn-warning btn-sm"><i class="fa fa-pencil fa-lg"></i>&nbsp;编辑</button>
                                                        <button class="btn btn-primary btn-sm"><i class="fa fa-plus fa-lg"></i>&nbsp;添加</button>
                                                        <button class="btn btn-danger btn-sm"><i class="fa fa-trash-o fa-lg"></i>&nbsp;删除</button>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        2
                                                    </td>
                                                    <td>
                                                        10002
                                                    </td>
                                                    <td>
                                                        操作员
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <button class="btn btn-warning btn-sm"><i class="fa fa-pencil fa-lg"></i>&nbsp;编辑</button>
                                                        <button class="btn btn-primary btn-sm"><i class="fa fa-plus fa-lg"></i>&nbsp;添加</button>
                                                        <button class="btn btn-danger btn-sm"><i class="fa fa-trash-o fa-lg"></i>&nbsp;删除</button>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="copyright float-right">
                                        <ul class="pagination">
                                            <li>
                                                <a href="#" class="jump  text-danger" id="first">首页</a>
                                            </li>
                                            <li>
                                                <a href="#" class="jump" id="prev">
                                                    <span class="angle-left"></span>
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#" class="jump">1
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#" class="jump">/</a>
                                            </li>
                                            <li>
                                                <a href="#" class="jump">1
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#" id="next" class="jump">
                                                    <span class="iconfont icon-more"></span>
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#" class="jump" id="last">尾页</a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
!-- 主界面页脚部分 -->
            <footer class="footer">
                <div class="container-fluid">
                    <!-- 版权内容 -->
                    <div class="copyright text-center">
                        &copy;
                        <script>
                            document.write(new Date().getFullYear())
                        </script>
                        , made with <i class="material-icons">favorite</i> by
                        <a href="javascript:;" target="_blank"></a>for a better web.
                    </div>
                </div>
            </footer>
        </div>
    </div>
    <script src="../js/jquery-3.3.1.min.js"></script>
    <!-- 左侧导航栏所需js -->
    <script src="../js/popper.min.js"></script>
    <script src="../js/bootstrap-material-design.min.js"></script>
    <!-- 移动端手机菜单所需js -->
    <script src="../js/perfect-scrollbar.jquery.min.js"></script>
    <script src="../js/material-dashboard.min.js"></script>
    <script>
        $(document).ready(function () {
            // 隐藏折叠内容
            $('.collapse').collapse('hide');
        });
    </script>
</body>

</html>


