﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="backQuery.aspx.cs" Inherits="bms.Web.SalesMGT.backQuery" %>

<!DOCTYPE html>
<!--[if lt IE 7]>      <html class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
<!--[if IE 7]>         <html class="no-js lt-ie9 lt-ie8"> <![endif]-->
<!--[if IE 8]>         <html class="no-js lt-ie9"> <![endif]-->
<!--[if gt IE 8]><!-->
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
    <link rel="stylesheet" href="../css/pagination.css" />
    <link rel="stylesheet" href="../css/materialdesignicons.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/pretty.min.css">
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
                            <h3 class="text-center"><strong>销&nbsp;退</strong></h3>
                            <hr />
                        </div>
                        <div class="card-body">
                            <div class="card-header from-group">
                                <div class="input-group">
                                    <!--<div class="btn-group" role="group">
                                        <button class="btn btn-success" id="add_back">添加销退</button>
                                    </div>
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="sales_search">
                                        <button class="btn btn-info">查询</button>
                                    </div>-->
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-warning btn-sm" id="toBack">返回</button>
                                    </div>
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-info btn-sm" id="print">打印</button>
                                    </div>
                                    <%string type = Session["type"].ToString();
                                        if (type != "search")
                                        { %>
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-success btn-sm" id="sure">保存单据</button>
                                    </div>
                                    <%} %>
                                </div>
                            </div>
                            <div class="content_tab">
                                <div class="table-responsive">
                                    <table class="table mostTable table-bordered text-center" id="table">
                                        <%--<thead>
                                            <tr>
                                                <th>序号</th>
                                                <th>ISBN号</th>
                                                <th>书号</th>
                                                <th>单价</th>
                                                <th>数量</th>
                                                <th>实际折扣</th>
                                                <th>码洋</th>
                                                <th>实洋</th>
                                            </tr>
                                        </thead>--%>
                                        <%=GetData() %>
                                    </table>
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
                </div>
            </div>
        </footer>
    </div>
    <!--模态框-->
    <div class='modal fade' id='myModa2' tabindex='-1' role='dialog' aria-labelledby='myModalLabel' aria-hidden='true' data-backdrop='static'>
        <div class='modal-dialog' style='max-width: 900px;'>
            <div class='modal-content'>
                <div class='modal-header'>
                    <h4 class='modal-title float-left' id='myModalLabel'>请选择你要进行操作的书籍</h4>
                    <button type='button' class='close' data-dismiss='modal' aria-hidden='true'>
                        <i class='material-icons'>clear</i>
                    </button>
                </div>
                <div class='modal-body'>
                    <table id='tablebook' class='table mostTable table-bordered text-center'>
                    </table>
                </div>
                <div class='modal-footer'>
                    <button type='button' class='btn btn-success btn-sm' id='sureBook'>确定</button>
                </div>
            </div>
        </div>
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
    <script src="../js/backQuery.js"></script>
    <script src="../js/jquery-migrate-1.2.1.min.js"></script>
    <script src="../js/jquery.jqprint.js"></script>
</body>

</html>
