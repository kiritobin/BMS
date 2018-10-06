<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="backQuery.aspx.cs" Inherits="bms.Web.SalesMGT.backQuery" %>

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
</head>

<body>
    <!--[if lt IE 7]>
            <p class="browsehappy">You are using an <strong>outdated</strong> browser. Please <a href="#">upgrade your browser</a> to improve your experience.</p>
        <![endif]-->
    <div class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="card" style="height:500px">
                        <h3 class="text-center">销&nbsp;退</h3>
                        <hr />
                        <div class="card-body">
                            <div class="card-header card_btn">
                                <div class="input-group">
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-success" data-toggle="modal" data-target="#myModa2">添加销退</button>
                                    </div>
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="sales_search">
                                        <button class="btn btn-info">查询</button>
                                    </div>
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-success"><i class="fa fa-print" aria-hidden="true"></i></button>
                                    </div>
                                    <div class="btn-group text-right" role="group">
                                        <button class="btn btn-danger" id="toBack">返回</button>
                                    </div>
                                </div>
                            </div>
                            <div class="content_tab col-md-12">
                                <div class="table-responsive col-md-10">
                                    <table class="table mostTable table-bordered text-center">
                                        <thead>
                                            <tr>
                                                <th>单据号</th>
                                                <th>ISBN号</th>
                                                <th>书号</th>
                                                <th>实际折扣</th>
                                                <th>单价</th>
                                                <th>数量</th>
                                                <th>码洋</th>
                                                <th>实洋</th>
                                                <th>时间</th>
                                                <th>操作</th>
                                            </tr>
                                        </thead>
                                        <%=GetData() %>
                                    </table>
                                </div>
                                <div class="statistics col-md-2">
                                    统计
                                </div>
                                <!--分页-->
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
    <!--模态框-->
    <div class="modal fade" id="myModa2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 900px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title float-left" id="myModalLabel">添加销退</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        <i class="material-icons">clear</i>
                    </button>
                </div>
                <div class="modal-body">
                    <table class="table model-table">
                        <tr>
                            <td>ISBN号</td>
                            <td>
                                <input id="search_sim" type="text" class="sales_search">
                            </td>
                            <td>实际折扣</td>
                            <td>
                                <input type="text" value="" class="sales_search"></td>
                            <td>数量</td>
                            <td>
                                <input type="text" value="" class="sales_search"></td>
                        </tr>
                        <%if (searchDs != null)
                            { %>
                        <tr id="Book">
                            <td colspan="6" style="border:1px solid black">
                                <table class="table">
                                    <tr>
                                        <th>书号</th>
                                        <th>书名</th>
                                        <th>出版社</th>
                                    </tr>
                                    <%for (int i = 0; i < searchDs.Tables[0].Rows.Count; i++)
                                        { %>
                                    <tr>
                                        <td><%=searchDs.Tables[0].Rows[i]["bookNum"] %></td>
                                        <td><%=searchDs.Tables[0].Rows[i]["bookName"] %></td>
                                        <td><%=searchDs.Tables[0].Rows[i]["supplier"] %></td>
                                    </tr>
                                    <%} %>
                                </table>
                            </td>
                        </tr>
                        <%} %>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="submit" class="btn btn-success btn-sm" id="btnAdd">添加</button>
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
</body>

</html>
