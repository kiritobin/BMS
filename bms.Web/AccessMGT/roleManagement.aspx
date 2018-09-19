<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="roleManagement.aspx.cs" Inherits="bms.Web.AccessMGT.roleManagement" %>

<%="" %>
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
    <link rel="stylesheet" href="../css/qc.css">
    <link rel="stylesheet" href="../css/lgd.css">
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
                <a href="javascript:;" class="simple-text text-center logo-normal">图书综合平台
                </a>
            </div>
            <div class="sidebar-wrapper">
                <ul class="nav">
                    <li class="nav-item active">
                        <a class="nav-link" href="#securityManage" data-toggle="collapse">
                            <i class="material-icons">security</i>
                            <p>
                                权限管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse show" id="securityManage">
                            <ul class="nav">
                                <li class="nav-item hoverColor foucsColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">用户管理</span>
                                    </a>
                                </li>
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">角色管理</span>
                                    </a>
                                </li>
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">功能管理</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>

                    <li class="nav-item">
                        <a class="nav-link" href="#userManage" data-toggle="collapse">
                            <i class="material-icons">person</i>
                            <p>
                                客户管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse" id="userManage">
                            <ul class="nav">
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">客户信息管理</span>
                                    </a>
                                </li>
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">客户馆藏数据</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li class="nav-item ">
                        <a class="nav-link" href="#inventoryManage" data-toggle="collapse">
                            <i class="material-icons">book</i>
                            <p>
                                库存管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse" id="inventoryManage">
                            <ul class="nav">
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">出库</span>
                                    </a>
                                </li>
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">入库</span>
                                    </a>
                                </li>
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">退货</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li class="nav-item ">
                        <a class="nav-link" href="#saleManage" data-toggle="collapse">
                            <i class="material-icons">library_books</i>
                            <p>
                                销售管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse" id="saleManage">
                            <ul class="nav">
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">销售</span>
                                    </a>
                                </li>
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">销退</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="#baseManage" data-toggle="collapse">
                            <i class="material-icons">bubble_chart</i>
                            <p>
                                基础信息
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse" id="baseManage">
                            <ul class="nav">
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">价位管理</span>
                                    </a>
                                </li>
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">书籍基础数据管理</span>
                                    </a>
                                </li>
                                <li class="nav-item hoverColor">
                                    <a class="nav-link" href="javascript:;">
                                        <span class="sidebar-normal">组织管理</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
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
                                </div>
                                <div class="card-body">
                                    <div class="card-header from-group">
                                        <div class="input-group no-border">
                                            <input type="text" value="" class="form-control col-sm-2" placeholder="请输入查询条件">
                                            <button class="btn btn-info btn-sm" id="btn-search"><i class="fa fa-search fa-lg"></i>&nbsp;&nbsp;查询</button>
                                            &nbsp;
                                    <button class="btn btn-success btn-sm" data-toggle="modal" data-target="#myModal" id="btn-add"><i class="fa fa-plus fa-lg"></i>&nbsp;&nbsp;添加</button>
                                        </div>
                                    </div>

                                    <div class="table-responsive">
                                        <table class="table">
                                            <thead class="text-danger">
                                                <tr>
                                                    <th>序号</th>
                                                    <th>职位</th>
                                                    <th>功能</th>
                                                    <th class="table-thead-th">操作</th>
                                                </tr>
                                            </thead>
                                            <%=getData() %>
                                        </table>
                                    </div>
                                    <div class="copyright float-right page-box">

                                        <div class="dataTables_paginate paging_full_numbers" id="datatables_paginate">
                                            <ul class="pagination">
                                                <li class="paginate_button page-item first" id="datatables_first"><a href="#" aria-controls="datatables"
                                                    data-dt-idx="0" tabindex="0" class="page-link">首页</a></li>
                                                <li class="paginate_button page-item previous" id="datatables_previous"><a href="#" aria-controls="datatables"
                                                    data-dt-idx="1" tabindex="0" class="page-link">上一页</a></li>
                                                <li class="paginate_button page-item "><a href="#" aria-controls="datatables" data-dt-idx="2"
                                                    tabindex="0" class="page-link">1</a></li>
                                                <li class="paginate_button page-item active"><a href="#" aria-controls="datatables" data-dt-idx="3"
                                                    tabindex="0" class="page-link">2</a></li>
                                                <!--类名active表示当前页 -->
                                                <li class="paginate_button page-item"><a href="#" aria-controls="datatables" data-dt-idx="4"
                                                    tabindex="0" class="page-link">3</a></li>
                                                <li class="paginate_button page-item "><a href="#" aria-controls="datatables" data-dt-idx="5"
                                                    tabindex="0" class="page-link">4</a></li>
                                                <li class="paginate_button page-item next" id="datatables_next"><a href="#" aria-controls="datatables"
                                                    data-dt-idx="6" tabindex="0" class="page-link">下一页</a></li>
                                                <li class="paginate_button page-item last" id="datatables_last"><a href="#" aria-controls="datatables"
                                                    data-dt-idx="7" tabindex="0" class="page-link">尾页</a></li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!--添加角色模态框-->
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog" style="min-width: 800px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title float-left" id="myModalLabel">添加角色</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table model-table">
                                <tr>
                                    <td><span class="model-tab-td-span">分公司:</span></td>
                                    <td>
                                        <select class="" title="请选择分公司" data-style="btn-sm" id="model-select-region">
                                            <option value="1">五华区</option>
                                            <option value="1">西山区</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="model-tab-td-span">职位:</span></td>
                                    <td>
                                        <select class="" title="请选择地区" data-style="btn-sm" id="model-select-role">
                                            <option value="1">入库管理员</option>
                                            <option value="2">零售员</option>
                                        </select>
                                    </td>
                                </tr>
                            </table>
                            <div class="row">
                                <table class="table">
                                    <tr class="model-tab-function">
                                        <td style="width: 65px"><span class="model-tab-td-functionSpan">功能:</span></td>
                                        <td>
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <input class="form-check-input" type="checkbox" value="">前台销售记录员
                                                    <span class="form-check-sign">
                                                        <span class="check functionCheck"></span>
                                                    </span>
                                                </label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <input class="form-check-input" type="checkbox" value="">前台销售记录员
                                                    <span class="form-check-sign">
                                                        <span class="check"></span>
                                                    </span>
                                                </label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <input class="form-check-input" type="checkbox" value="">前台销售记录员
                                                    <span class="form-check-sign">
                                                        <span class="check"></span>
                                                    </span>
                                                </label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <input class="form-check-input" type="checkbox" value="">前台销售记录员
                                                    <span class="form-check-sign">
                                                        <span class="check"></span>
                                                    </span>
                                                </label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr class="model-tab-function">
                                        <td></td>
                                        <td>
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <input class="form-check-input" type="checkbox" value="">功能一
                                                    <span class="form-check-sign">
                                                        <span class="check"></span>
                                                    </span>
                                                </label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <input class="form-check-input" type="checkbox" value="">功能一
                                                    <span class="form-check-sign">
                                                        <span class="check"></span>
                                                    </span>
                                                </label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <input class="form-check-input" type="checkbox" value="">功能一
                                                    <span class="form-check-sign">
                                                        <span class="check"></span>
                                                    </span>
                                                </label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <input class="form-check-input" type="checkbox" value="">功能一
                                                    <span class="form-check-sign">
                                                        <span class="check"></span>
                                                    </span>
                                                </label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr class="model-tab-function">
                                        <td></td>
                                        <td>
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <input class="form-check-input" type="checkbox" value="">功能一
                                                    <span class="form-check-sign">
                                                        <span class="check"></span>
                                                    </span>
                                                </label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <input class="form-check-input" type="checkbox" value="">功能一
                                                    <span class="form-check-sign">
                                                        <span class="check"></span>
                                                    </span>
                                                </label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <input class="form-check-input" type="checkbox" value="">功能一
                                                    <span class="form-check-sign">
                                                        <span class="check"></span>
                                                    </span>
                                                </label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <input class="form-check-input" type="checkbox" value="">功能一
                                                    <span class="form-check-sign">
                                                        <span class="check"></span>
                                                    </span>
                                                </label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr class="model-tab-function">
                                        <td></td>
                                        <td>
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <input class="form-check-input" type="checkbox" value="">功能一
                                                    <span class="form-check-sign">
                                                        <span class="check"></span>
                                                    </span>
                                                </label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <input class="form-check-input" type="checkbox" value="">功能一
                                                    <span class="form-check-sign">
                                                        <span class="check"></span>
                                                    </span>
                                                </label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <input class="form-check-input" type="checkbox" value="">功能一
                                                    <span class="form-check-sign">
                                                        <span class="check"></span>
                                                    </span>
                                                </label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <input class="form-check-input" type="checkbox" value="">功能一
                                                    <span class="form-check-sign">
                                                        <span class="check"></span>
                                                    </span>
                                                </label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default btn-sm" data-dismiss="modal" id="model-btnclose1">关闭</button>
                            <button type="submit" class="btn btn-success btn-sm" id="btnAdd">提交</button>
                        </div>
                    </div>
                </div>
                <!--编辑职位模态框-->
                <div class="modal fade" id="myModa2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title float-left" id="myModalLabe2">编辑职位</h4>
                            </div>
                            <div class="modal-body">
                                <table class="table model-table">
                                    <tr>
                                        <td><span class="model-tab-td-span">职位名:</span></td>
                                        <td>门卫</td>
                                    </tr>
                                    <tr>
                                        <td><span class="model-tab-td-span">功能:</span></td>
                                        <td>门卫</td>
                                    </tr>
                                </table>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default btn-sm" data-dismiss="modal" id="model-btnclose2">关闭</button>
                                <button type="submit" class="btn btn-success btn-sm">提交</button>
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
                        , made with <i class="material-icons">favorite</i> by
                        <a href="javascript:;" target="_blank"></a>for a better web.
                    </div>
                </div>
            </footer>
        </div>
    </div>
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/roleManagement.js"></script>
    <!-- 左侧导航栏所需js -->
    <script src="../js/popper.min.js"></script>
    <script src="../js/bootstrap-material-design.min.js"></script>
    <!-- 移动端手机菜单所需js -->
    <script src="../js/perfect-scrollbar.jquery.min.js"></script>
    <script src="../js/material-dashboard.min.js"></script>
    <script src="../js/bootstrap-selectpicker.js"></script>

</body>

</html>


