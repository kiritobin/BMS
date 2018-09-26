<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="returnManagement.aspx.cs" Inherits="bms.Web.BasicInfor.replenishList" %>

<%="" %>
<!DOCTYPE html>

<html class="no-js">
<!--<![endif]-->

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>图书综合管理系统</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 材料图标样式 -->
    <link rel="stylesheet" href="../css/materialdesignicons.css"/>
    <!-- 字体图标样式 -->
    <link rel="stylesheet" href="../css/font-awesome.min.css"/>
    <!-- css样式 -->
    <link rel="stylesheet" href="../css/material-dashboard.min.css"/>
    <link rel="stylesheet" href="../css/pagination.css" />
    <link rel="stylesheet" href="../css/zgz.css"/>
    <link rel="stylesheet" href="../css/lgd.css"/>
    <link rel="stylesheet" href="../css/qc.css"/>
    <!-- 时间input样式 -->
    <link rel="stylesheet" href="../css/jedate.css"/>
</head>

<body>
    <!--[if lt IE 7]>
            <p class="browsehappy">You are using an <strong>outdated</strong> browser. Please <a href="#">upgrade your browser</a> to improve your experience.</p>
        <![endif]-->
    <div class="wrapper ">
        <!-- 左侧垂直导航 -->
        <div class="sidebar" data-color="danger" data-background-color="white" data-image="../imgs/sidebar-2.jpg">
            <!--
                Tip 1: 需要改变导航条的颜色可以修改: data-color="purple | azure | green | orange | danger"
        
                Tip 2: 需要改变导航条的背景图片可以修改 data-image tag
            -->
            <!-- 平台字体logo -->
            <div class="logo">
                <a href="javascript:;" class="simple-text text-center logo-normal">图书综合管理平台
                </a>
            </div>
          <div class="sidebar-wrapper">
                <ul class="nav">
                    <li class="nav-item">
                        <a class="nav-link" href="#securityManage" data-toggle="collapse">
                            <i class="material-icons">security</i>
                            <p>
                                权限管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse" id="securityManage">
                            <ul class="nav">
                                <li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/userManagement.aspx">
                                        <span class="sidebar-normal">用户管理</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/roleManagement.aspx">
                                        <span class="sidebar-normal">角色管理</span>
                                    </a>
                                </li>
                                <%--<li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/jurisdictionManagement.aspx">
                                        <span class="sidebar-normal">功能管理</span>
                                    </a>
                                </li>--%>
                                <li class="nav-item">
                                    <a class="nav-link" href="../AccessMGT/organizationalManagement.aspx">
                                        <span class="sidebar-normal">组织管理</span>
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
                                <li class="nav-item">
                                    <a class="nav-link" href="../CustomerMGT/customerManagement.aspx">
                                        <span class="sidebar-normal">客户信息管理</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../CustomerMGT/collectionManagement.aspx">
                                        <span class="sidebar-normal">客户馆藏数据</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li class="nav-item active">
                        <a class="nav-link" href="#inventoryManage" data-toggle="collapse">
                            <i class="material-icons">book</i>
                            <p>
                                库存管理
                                <b class="caret"></b>
                            </p>
                        </a>
                        <div class="collapse show" id="inventoryManage">
                            <ul class="nav">
                                <li class="nav-item">
                                    <a class="nav-link" href="warehouseManagement.aspx">
                                        <span class="sidebar-normal">出库管理</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="stockManagement.aspx">
                                        <span class="sidebar-normal">入库管理</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link activeNext" href="returnManagement.aspx">
                                        <span class="sidebar-normal">退货管理</span>
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
                                <li class="nav-item">
                                    <a class="nav-link" href="../SalesMGT/tradeManagement.aspx">
                                        <span class="sidebar-normal">营销管理</span>
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
                                <li class="nav-item">
                                    <a class="nav-link" href="../BasicInfor/bookshelfManagement.aspx">
                                        <span class="sidebar-normal">架位管理</span>
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="../BasicInfor/bookBasicManagement.aspx">
                                        <span class="sidebar-normal">书籍基础数据管理</span>
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
                                    <a class="dropdown-item" href="../changePwd.aspx">修改密码</a>
                                    <a class="dropdown-item" href="javascript:logout();">退出系统</a>
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
                                    <h4 class="card-title">退货管理</h4>
                                </div>
                                <div class="card-body">
                                    <div class="card-header from-group">
                                        <div class="input-group">
                                            <div class="btn-group" role="group">
                                                <input type="text" value="" class="searchOne" placeholder="请输入查询条件">
                                                <button class="btn btn-info btn-sm" id="btn-search">查询</button>
                                            </div>
                                             <div class="btn-group" role="group">
                                                <button class="btn btn-success btn-sm" data-toggle="modal" data-target="#myModal" id="btn-add"><i class="fa fa-plus fa-lg"></i>&nbsp;添加</button>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="table-responsive">
                                        <table class="table mostTable table-bordered text-center">
                                            <thead>
                                                <tr>
                                                    <th>单编ID</th>
                                                    <th>组织名称</th>
                                                    <th>操作员名称</th>
                                                    <th>单据总数</th>
                                                    <th>总实洋</th>
                                                    <th>总码洋</th>
                                                    <th>到货时间</th>
                                                    <th>付款时间</th>
                                                    <th>制单时间</th>
                                                    <th>操作</th>
                                                </tr>
                                            </thead>

                                            <tbody>
                                                <tr>
                                                    <td>10000001</td>
                                                    <td>加基森</td>
                                                    <td>保罗</td>
                                                    <td>3929</td>
                                                    <td>56</td>
                                                    <td>456</td>
                                                    <td>2018-12-23</td>
                                                    <td>2019-8-9</td>
                                                    <td>2020-9-8</td>
                                                    <td>
                                                        <a href="addReturn.aspx?returnId="><button class="btn btn-success btn-sm"><i class="fa fa-plus fa-lg"></i></button></a>
                                                        <button class="btn btn-info btn-sm" onclick="window.location.href='checkReturn.aspx'"><i class="fa fa-search"></i></button>
                                                        <button class="btn btn-danger btn-sm"><i class="fa fa-trash"></i></button>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="copyright float-right page-box">
                                        <div class="dataTables_paginate paging_full_numbers" id="datatables_paginate">
                                            <div class="m-style paging"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
<!--添加模态框-->
        <div class="modal" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
            <div class="modal-dialog" style="max-width:700px;">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title float-left" id="myModalLabel">退货添加</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            <i class="material-icons">clear</i>
                        </button>
                    </div>
                    <div class="modal-body">
                        <table class="table model-table">
                             <tr>
                                <td class="text-right"><span><nobr>单据总数:</nobr></span></td>
                                <td>
                                    <input type="text" class="modal_search_add float-left" id="billCount">
                                </td>
                                <td class="text-right"><span><nobr>总码洋:</nobr></span></td>
                                <td>
                                    <input type="text" class="modal_search_add float-left" id="totalPrice">
                                </td>
                                <td class="text-right"><span><nobr>总实洋:</nobr></span></td>
                                <td>
                                    <input type="text" class="modal_search_add float-left" id="realPrice">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-success btn-sm" id="btnAdd">添加</button>
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
                        &nbsp;版权所有
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
    <!-- selectpicker.js -->
    <script src="../js/bootstrap-selectpicker.js"></script>
    <!-- alert.js -->
    <script src="../js/sweetalert2.js"></script>
    <!-- paging.js -->
    <script src="../js/jquery.pagination.js"></script>
    <script src="../js/bookshelfManagement.js"></script>
    <script src="../js/returnManagement.js"></script>
    <!-- 时间inputjs -->
    <%--<script src="../js/jedate.min.js"></script>
    <script>
        var enLang = {
            name: "en",
            month: ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"],
            weeks: ["SUN", "MON", "TUR", "WED", "THU", "FRI", "SAT"],
            times: ["Hour", "Minute", "Second"],
            timetxt: ["Time", "Start Time", "End Time"],
            backtxt: "Back",
            clear: "Clear",
            today: "Now",
            yes: "Confirm",
            close: "Close"
        }
        //自定义格式选择
        jeDate("#arrivalTime", {
            theme: { bgcolor: "#D91600", pnColor: "#FF6653" },
            format: "YYYY年MM月DD日"
        });
        jeDate("#payTime", {
            theme: { bgcolor: "#D91600", pnColor: "#FF6653" },
            format: "YYYY年MM月DD日"
        });
        jeDate("#makeTime", {
            theme: { bgcolor: "#D91600", pnColor: "#FF6653" },
            format: "YYYY年MM月DD日"
        });
    </script>--%>
</body>

</html>
