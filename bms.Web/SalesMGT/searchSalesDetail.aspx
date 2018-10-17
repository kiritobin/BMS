<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="searchSalesDetail.aspx.cs" Inherits="bms.Web.SalesMGT.searchSalesDetail" %>

<!DOCTYPE html>

<html class="no-js">
<!--<![endif]-->

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title></title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 字体图标样式 -->
    <link rel="stylesheet" href="../css/font-awesome.min.css">
    <!-- css样式 -->
    <link rel="stylesheet" href="../css/material-dashboard.min.css">
    <link rel="stylesheet" href="../css/zgz.css">
    <link rel="stylesheet" href="../css/lgd.css">
    <link rel="stylesheet" href="../css/demo.css">
    <link rel="stylesheet" type="text/css" href="../css/pretty.min.css">
    <link rel="stylesheet" type="text/css" href="../css/materialdesignicons.min.css">
</head>

<body>
    <!--[if lt IE 7]>
            <p class="browsehappy">You are using an <strong>outdated</strong> browser. Please <a href="#">upgrade your browser</a> to improve your experience.</p>
        <![endif]-->
    <div class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="container-fluid">
                            <h3 class="text-center"><strong>销&nbsp;售&nbsp;统&nbsp;计</strong></h3>
                            <hr />
                        </div>
                        <div class="card-body">
                            <div class="card-header from-group">
                                <div class="input-group">
                                    <%--<div class="btn-group" role="group">
                                        <input type="text" value="" class="" id="sales_bookName" placeholder="请输入书名">
                                    </div>
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="" id="sales_ISBN" placeholder="请输入ISBN">
                                        <button class="btn btn-info" id="btn_search">查询</button>
                                    </div>--%>
                                       <div class="btn-group" role="group">
                                        <button class="btn btn-info btn-sm" id="print">打印</button>
                                    </div>
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-warning btn-sm" id="back">返回</button>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="content_tab col-md-10">
                                    <div class="table-responsive" style="height: 500px;" id="content">
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
                                                        <nobr>折扣</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>实洋</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>采购数</nobr>
                                                    </td>
                                                </tr>
                                            </thead>
                                           <%-- <%=getData() %>--%>
                                        </table>
                                    </div>
                                    <%--<div class="copyright float-right page-box">
                                        <div class="dataTables_paginate paging_full_numbers" id="datatables_paginate">
                                            <div class="m-style paging"></div>
                                        </div>
                                    </div>--%>
                                </div>
                                <div class="container col-md-2">
                                    <div style="border: 1px solid #ddd">
                                        <h4 class="text-center" style="margin-top: 16px;"><strong>统计</strong></h4>
                                        <hr />
                                        <table class="table">
                                            <%--<tr>
                                                <td>书籍种数</td>
                                                <td id="kinds"><%=allkinds.ToString() %></td>
                                            </tr>
                                            <tr>
                                                <td>书本总数</td>
                                                <td id="allnumber"><%=allnumber.ToString() %></td>
                                            </tr>
                                            <tr>
                                                <td>总码洋</td>
                                                <td id="alltotalprice"><%=alltotalprice.ToString() %><input type="hidden" id="limtalltotalprice" value="<%=limtalltotalprice.ToString() %>" /></td>

                                            </tr>
                                            <tr>
                                                <td>总实洋</td>
                                                <td id="allreadprice"><%=allreadprice.ToString() %></td>
                                            </tr>--%>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- 主界面页脚部分 -->
        <footer class="footer content-footer">
            <div class="container-fluid">
                <!-- 版权内容 -->
                <div class="copyright text-center">
                    &copy;
                    <script>
                        document.write(new Date().getFullYear())
                    </script>
                    &nbsp;版权所有
                    <p><a href="../chrome/ChromeDownload.html">Chrome浏览器下载</a></p>
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
    <script src="../js/jquery.jqprint.js"></script>
    <object id="LODOP_OB" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width="0" height="0">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0"></embed>
    </object>
    <script src="../js/LodopFuncs.js"></script>
    <script src="../js/public.js"></script>
</body>

</html>
