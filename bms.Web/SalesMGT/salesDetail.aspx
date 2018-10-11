<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="salesDetail.aspx.cs" Inherits="bms.Web.SalesMGT.salesDetail" %>

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
                            <h3 class="text-center">销&nbsp;售</h3>
                        </div>
                        <div class="card-body">
                            <div class="card-header card_btn">
                                <div class="col-md-12">
                                    <div class="input-group">
                                        <%--<div class="btn-group" role="group">
                                        <input type="text" value="" class="" id="sales_bookName" placeholder="请输入书名">
                                    </div>
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="" id="sales_ISBN" placeholder="请输入ISBN">
                                        <button class="btn btn-info" id="btn_search">查询</button>
                                    </div>--%>
                                        <%if (type == "look")
                                            {%>
                                        <div class="btn-group" role="group">
                                            <button class="btn btn-success" data-toggle="modal" data-target="#myModa2"><i class="fa fa-print"></i></button>
                                        </div>
                                        <%} %>
                                        <div class="btn-group pull-right" role="group">
                                            <button class="btn btn-success" id="back">返回</button>
                                        </div>
                                        <div class="btn-group" role="group">
                                            <%if (type == "addsale")
                                                {%>
                                            <button class="btn btn-success" id="success">单据完成</button>
                                            <%} %>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="content_tab col-md-12">
                                <div class="table-responsive col-md-10" style="height: 500px;">
                                    <table class="table mostTable table-bordered text-center" id="table">
                                        <thead>
                                            <tr>
                                                <td>
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
                                                    <nobr>实际折扣</nobr>
                                                </td>
                                                <td>
                                                    <nobr>实洋</nobr>
                                                </td>
                                                <td>
                                                    <nobr>已采购数</nobr>
                                                </td>
                                            </tr>
                                        </thead>
                                        <tr class="first">
                                            <td></td>
                                            <td>
                                                <input type="text" class="isbn textareaISBN" autofocus="autofocus" onkeyup="this.value=this.value.replace(/[^\r\n0-9]/g,'');" /></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td>
                                                <input class="count textareaCount" onkeyup="this.value=this.value.replace(/[^\r\n0-9]/g,'');" /></td>
                                            <td>
                                                <input class="discount textareaDiscount" onkeyup="this.value=this.value.replace(/[^\r\n0-9]/g,'');" /></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <%=getData() %>
                                    </table>
                                </div>
                                <div class="container col-md-2" style="border: 1px gray solid">
                                    统计
                                    <table class="table">
                                        <tr>
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
                                        </tr>
                                    </table>
                                </div>
                                <%--<div class="copyright float-right page-box">
                                    <div class="dataTables_paginate paging_full_numbers" id="datatables_paginate">
                                        <div class="m-style paging"></div>
                                    </div>
                                </div>--%>
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
                    <h4 class="modal-title float-left" id="myModalLabel">请选择相应的图书</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        <i class="fa fa-close"></i>
                    </button>
                </div>
                <div class="modal-body">
                    <%--                    <table class="table model-table">
                        <tr>
                            <td>ISBN号</td>
                            <td>
                                <input type="text" id="ISBN" class="addsale"></td>
                            <td>数量</td>
                            <td>
                                <input type="text" id="number" class="addsale"></td>
                            <td>实际折扣</td>
                            <td>
                                <input type="text" id="disCount" value="<%=defaultdiscount %>" class="addsale"></td>

                        </tr>
                    </table>--%>
                    <table id="tablebook" class="table mostTable table-bordered text-center">
                        <%if (bookds != null)
                            {%>
                        <%getbook();%>
                        <% } %>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="submit" class="btn btn-success btn-sm" id="btnAdd">选择此书</button>
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
    <script src="../js/jquery.pagination.js"></script>
    <script src="../js/salesDetail.js"></script>
</body>

</html>
