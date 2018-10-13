﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="retail.aspx.cs" Inherits="bms.Web.SalesMGT.retail" %>

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
    <!-- css样式 -->
    <link rel="stylesheet" href="../css/material-dashboard.min.css">
    <link rel="stylesheet" href="../css/zgz.css">
    <link rel="stylesheet" href="../css/materialdesignicons.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/pretty.min.css">
    <style>
        .gw_num {
            border: 1px solid #dbdbdb;
            width: 110px;
            line-height: 26px;
            overflow: hidden;
        }

            .gw_num em {
                display: block;
                height: 26px;
                width: 26px;
                float: left;
                color: #7A7979;
                border-right: 1px solid #dbdbdb;
                text-align: center;
                cursor: pointer;
            }

            .gw_num .num {
                display: block;
                float: left;
                text-align: center;
                width: 52px;
                font-style: normal;
                font-size: 14px;
                line-height: 24px;
                border: 0;
            }

            .gw_num em.add {
                float: right;
                border-right: 0;
                border-left: 1px solid #dbdbdb;
            }
    </style>
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
                        <div class="col-md-8 text-right">
                            <div class="text-right">时间：<span id="time"></span></div>
                        </div>
                        <!-- 左侧数据表 -->
                        <div class="table-responsive col-md-8">
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
                        <div class="col-md-4">
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
                        <div class="col-md-9">
                            <fieldset>
                                <legend>
                                    <b>合计</b>
                                </legend>
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>品种：￥&nbsp&nbsp<span id="kind"></span></label>
                                        </div>
                                        <div class="col-md-6">
                                            <label>数量：￥&nbsp&nbsp<span id="number"></span></label>
                                        </div>
                                        <div class="col-md-6">
                                            <label>码洋：￥&nbsp&nbsp<span id="total"></span></label>
                                        </div>
                                        <div class="col-md-6">
                                            <label>应收：￥&nbsp&nbsp<span id="real"></span></label>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <!--startprint-->
                        <div class="border content" style=" height: 500px;" id="ticket">
                            <div style="margin: 0 auto; color: black; font-weight: 400">
                                <br />
                                <table class="table">
                                    <tr>
                                        <td>品种：￥&nbsp&nbsp<span id="kindEnd"></span></td>
                                        <td>码洋：￥&nbsp&nbsp<span id="totalEnd"></span></td>
                                    </tr>
                                    <tr>
                                        <td>数量：￥&nbsp&nbsp<span id="numberEnd"></span></td>
                                        <td>应收：￥&nbsp&nbsp<span id="realEnd"></span></td>
                                    </tr>
                                </table>
                                <div style="margin-top: 10px;">
                                    <div id="output" style="width: 190px; height: 200px;display:none"></div>
                                    <img src="#" style="width: 190px; height: 200px" id="img" />
                                </div>
                                <div style="width: 260px;">
                                    <hr / style="color:lightseagreen">
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
            <div class="modal-content" style="width:800px;">
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
                                <th><nobr>ISBN</nobr></th>
                                <th><nobr>书名</nobr></th>
                                <th><nobr>单价</nobr></th>
                                <th><nobr>出版社</nobr></th>
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
    <script src="../js/retail.js"></script>
    <script src="../js/jquery.tabletojson.js"></script>
     <script src="../js/jquery.qrcode.min.js"></script>
    <object id="LODOP_OB" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width="0" height="0">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0"></embed>
    </object>
    <script src="../js/LodopFuncs.js"></script>
</body>
</html>
