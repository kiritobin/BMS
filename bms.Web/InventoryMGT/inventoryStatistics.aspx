<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="inventoryStatistics.aspx.cs" Inherits="bms.Web.InventoryMGT.inventoryStatistics" %>

<!DOCTYPE html>

<html class="no-js">
<!--<![endif]-->

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>云南新华书店项目综合管理系统</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 字体图标样式 -->
    <link rel="stylesheet" href="../css/font-awesome.min.css">
    <!-- css样式 -->
    <link rel="stylesheet" href="../css/material-dashboard.min.css">
    <link rel="stylesheet" href="../css/zgz.css">
    <link rel="stylesheet" href="../css/lgd.css">
    <link rel="stylesheet" href="../css/demo.css">
    <link rel="stylesheet" href="../css/pagination.css">
    <link rel="stylesheet" type="text/css" href="../css/pretty.min.css">
    <link rel="stylesheet" type="text/css" href="../css/materialdesignicons.min.css">
</head>

<body>
    <div class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="container-fluid">
                            <h3 class="text-center"><strong id="tjType">入&nbsp;库&nbsp;统&nbsp;计</strong></h3>
                            <hr />
                        </div>
                        <div class="card-body">
                            <div class="card-header from-group">
                                <div class="input-group">
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="" id="bookNum" placeholder="请输入书号">
                                    </div>
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="" id="bookName" placeholder="请输入书名">
                                    </div>
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="" id="supplier" placeholder="请输入供应商">
                                    </div>
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="" id="time" placeholder="请输入时间">
                                    </div>
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="" id="userName" placeholder="请输入制单员">
                                    </div>
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="" id="resource" placeholder="请输入来源组织">
                                    </div>
                                    <%if (isNotAdmin)
                                        { %>
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="" id="region" placeholder="请输入自身组织">
                                    </div>
                                    <%} %>
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-info" id="btn_search">查询</button>
                                    </div>
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-warning btn-sm" onclick="window.history.go(-1);" id="back">返回</button>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="content_tab col-md-12">
                                    <div class="table-responsive" style="" id="content">
                                        <table class="table mostTable table-bordered text-center" id="table">
                                            <thead>
                                                <tr>
                                                    <td class="bbb">
                                                        <nobr>序号</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>ISBN号</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>书号</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>书名</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>单价</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>数量</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>实洋</nobr>
                                                    </td>
                                                </tr>
                                            </thead>
                                            <%=getData() %>
                                        </table>
                                        <table class="table table_stock text-center" id="table2">
                                            <tr class="text-nowrap">
                                                <td>
                                                    <span>书籍种数:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="" class="form-control" disabled>
                                                </td>
                                                <td>
                                                    <span>书本总数:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="" class="form-control" disabled></td>
                                                <td>
                                                    <span>总码洋:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="" class="form-control" disabled>
                                                </td>
                                                <td>
                                                    <span>总实洋:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="" class="form-control" disabled>
                                                </td>
                                            </tr>
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
                    &nbsp;版权归云南新华书店图书有限公司信息部所有
                        <p>建议使用<a href="../chrome/ChromeDownload.html">Google浏览器</a>浏览网页</p>
                </div>
            </div>
        </footer>
    </div>
    <!-- js file -->
    <script src="../js/jquery-3.3.1.min.js"></script>
    <!-- 左侧导航栏所需js -->
    <script src="../js/popper.min.js"></script>
    <script src="../js/bootstrap-material-design.min.js"></script>
    <!-- 事物处理 -->
    <script src="../js/sweetalert2.js"></script>
    <script src="../js/demo.js"></script>
    <!-- 移动端手机菜单所需js -->
    <script src="../js/perfect-scrollbar.jquery.min.js"></script>
    <script src="../js/material-dashboard.min.js"></script>
    <!-- selectpicker.js -->
    <script src="../js/bootstrap-selectpicker.js"></script>
    <script src="../js/jquery.pagination.js"></script>
    <script src="../js/jquery-migrate-1.2.1.min.js"></script>
    <script src="../js/inventoryStatistics.js"></script>
    <object id="LODOP_OB" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width="0" height="0">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0"></embed>
    </object>
    <script src="../js/LodopFuncs.js"></script>
    <script src="../js/public.js"></script>
</body>

</html>
