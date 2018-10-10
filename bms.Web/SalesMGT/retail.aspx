<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="retail.aspx.cs" Inherits="bms.Web.SalesMGT.retail" %>

<%="" %>
<!DOCTYPE html>
<html class="no-js">

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>图书综合管理平台</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 字体图标样式 -->
    <link rel="stylesheet" href="../css/font-awesome.min.css">
    <!--分页样式-->
    <link rel="stylesheet" href="../css/pagination.css">
    <!-- css样式 -->
    <link rel="stylesheet" href="../css/material-dashboard.min.css">
    <link rel="stylesheet" href="../css/jedate.css" />
    <link rel="stylesheet" href="../css/zgz.css">
    <link rel="stylesheet" href="../css/materialdesignicons.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/pretty.min.css">
</head>

<body>
    <div class="retail-content">
        <div class="container-fluid">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="input-group col-md-12">
                            <div class="btn-group" role="group">
                                <input type="text" placeholder="请输入ISBN" id="search" class="searchOne">
                                <button class="btn btn-info btn-sm" id="btnSearch" data-toggle="modal">扫描</button>
                                <%--<button class="btn btn-info btn-sm" id="save" data-toggle="modal">save</button>--%>
                                <%--<button class="btn btn-success btn-sm" data-toggle="modal" data-target="#myModal">添加</button>--%>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive col-md-10">
                            <div style="height: 500px; width: 80%; display: block; overflow-y: auto; border: none">
                                <table class="table mostTable table-bordered text-center test" id="table">
                                    <thead>
                                        <tr>
                                            <td>
                                                <nobr>ISBN</nobr>
                                            </td>
                                            <td>
                                                <nobr>书名</nobr>
                                            </td>
                                            <td>
                                                <nobr>单价</nobr>
                                            </td>
                                            <td style="display: none">
                                                <nobr>数量</nobr>
                                            </td>
                                            <td>
                                                <nobr>商品数量</nobr>
                                            </td>
                                            <td>
                                                <nobr>折扣</nobr>
                                            </td>
                                            <td>
                                                <nobr>码洋</nobr>
                                            </td>
                                            <td>
                                                <nobr>实洋</nobr>
                                            </td>
                                            <td style="display: none">
                                                <nobr>书号</nobr>
                                            </td>
                                            <td>
                                                <nobr>操作</nobr>
                                            </td>
                                        </tr>
                                    </thead>
                                        <tr class="first">
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                </table>
                            </div>
                        </div>
                        <div class="retailList col-md-2">
                            <h3 class="text-center">零售单</h3>
                            <hr />
                            <ul class="list-unstyled">
                                <li>时间：<span id="time"></span></li>
                                <li>种类：<span id="kind"></span></li>
                                <li>数量：<span id="number"></span></li>
                                <li>总码洋：<span id="total"></span></li>
                                <li>总实洋：<span id="real"></span></li>
                            </ul>
                            <hr />
                            <div class="input-group text-white" id="insert">
                                <a class="btn btn-success btn-sm"><i class="fa fa-print">打印</i></a>
                            </div>
                            <div class="input-group text-white" id="giveup">
                                <a class="btn btn-danger btn-sm"><i class="fa fa-trash">放弃</i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--添加模态框-->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title float-left" id="myModalLabel">请选择需要的图书</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        <i class="material-icons">clear</i>
                    </button>
                </div>
                <div class="modal-body">
                    <table class="table table-bordered mostTable text-center" id="table2">
                        <thead>
                            <tr>
                                <th>
                                    <div class="pretty inline">
                                        <input type="radio" name="radio" disabled="disabled">
                                        <label aria-disabled="true"><i class="mdi mdi-check"></i></label>
                                    </div>
                                </th>
                                <th>ISBN</th>
                                <th>书名</th>
                                <th>单价</th>
                                <th>出版社</th>
                            </tr>
                        </thead>
                        <%=getIsbn() %>
                    </table>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-success btn-sm" id="btnAdd">提交</button>
                </div>
            </div>
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
    <script src="../js/jedate.min.js"></script>
    <script src="../js/retail.js"></script>
    <script src="../js/jquery.tabletojson.js"></script>
</body>
</html>