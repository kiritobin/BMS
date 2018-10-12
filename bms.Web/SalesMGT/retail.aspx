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
            <div class="row">
                <div class="col-sm-4 col-lg-3">
                    <img src="../imgs/YNXH-LOGO.png" id="topImg" class="img-fluid" alt="Cinque Terre">
                </div>
                <div class="col-sm-2">
                    <input type="text" id="search" class="topSearch">
                </div>
            </div>
            <div class="card">
                <div class="card-header card-header-danger">
                    <h3 class="card-title text-center">自助扫码开单</h3>
                </div>
                <div class="card-body">
                    <%--<div class="row">
                        <div class="input-group col-md-12">
                            <div class="btn-group" role="group">
                                <input type="text" placeholder="请输入ISBN" id="search" class="searchOne">
                                <button class="btn btn-info btn-sm" id="btnSearch" data-toggle="modal">扫描</button>
                                <button class="btn btn-info btn-sm" id="save" data-toggle="modal">save</button>
                                <button class="btn btn-success btn-sm" data-toggle="modal" data-target="#myModal">添加</button>
                            </div>
                        </div>
                    </div>--%>
                    <div class="row">
                        <div class="col-md-9 col-lg-8 text-right">
                            <div class="text-right">时间：<span id="time"></span></div>
                        </div>
                        <!-- 左侧数据表 -->
                        <div class="table-responsive col-md-9 col-lg-8">
                            <div style="height: 500px; display: block; overflow-y: auto;">
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
                        <!-- 右侧功能区 -->
                        <div class="col-md-3 col-lg-4">
                            <div class="retailList">
                                <img src="../imgs/login.jpg" alt="img" class="img-fluid" />
                            </div>
                            <div class="container">
                                <div class="row">
                                    <div class="text-white col-sm-6 text-right" id="insert">
                                        <button class="btn btn-success btn-sm btnText">打  印</button>
                                    </div>
                                    <div class="text-white col-sm-6 text-left" id="giveup">
                                        <button class="btn btn-danger btn-sm btnText">放  弃</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- 合计模块 -->
                        <div class="col-md-9 col-lg-8">
                            <fieldset>
                                <legend>
                                    <b>合计</b>
                                </legend>
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>品种：<span id="kind"></span></label>
                                        </div>
                                        <div class="col-md-6">
                                            <label>数量：<span id="number"></span></label>
                                        </div>
                                        <div class="col-md-6">
                                            <label>码洋：<span id="total"></span></label>
                                        </div>
                                        <div class="col-md-6">
                                            <label>应收：<span id="real"></span></label>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <!--startprint-->
                        <div class="border content" style="width: 155px; height: 350px;" id="ticket">
                            <div style="position:relative;left:27px; color:black;font-size:16px;line-height:2;" >
                                <br />
                                <div>
                                    品种：<span id="kindEnd"></span>
                                </div>
                                <div>
                                    数量：<span id="numberEnd"></span>
                                </div>
                                <div>
                                    码洋：<span id="totalEnd"></span>
                                </div>
                                <div>
                                    应收：<span id="realEnd"></span>
                                </div>
                                <div style="margin-top:10px;">
                                    <img src="#" style="width: 100px; height: 100px;" id="img" />
                                </div>
                                <div style="position:relative;right:22px;width: 147px;">
                                <hr />
                                    <span id="timeEnd"></span>
                                </div>
                            </div>
                        </div>
                        <!--endprint-->
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--选择图书模态框-->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title float-left" id="myModalLabel">请选择需要的图书</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        <i class="fa fa-close"></i>
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
