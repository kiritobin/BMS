﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="booksOut.aspx.cs" Inherits="bms.Web.SalesMGT.booksOut" %>

<%="" %>
<!DOCTYPE html>

<html class="no-js">
<!--<![endif]-->

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>云南新华书店项目综合管理系统</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 字体图标样式 -->
    <link rel="stylesheet" href="../css/font-awesome.min.css">
    <!-- css样式 -->
    <link rel="stylesheet" href="../css/material-dashboard.min.css">
    <link rel="stylesheet" href="../css/pagination.css" />
    <link rel="stylesheet" href="../css/zgz.css">
    <link rel="stylesheet" href="../css/lgd.css">
    <link rel="stylesheet" href="../css/qc.css">
    <style>
        table {
            font-size: 24px;
            font-weight:bold;
        }

        #fontHead th {
            font-size: 24px;
            font-weight:bold;
        }
        td {
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }
    </style>
</head>
<body>
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
        </div>
    </nav>
    <!-- 主界面内容 -->
    <div class="content">
        <div class="container-fluid">
            <img src="../imgs/YNXH-LOGO.png" class="img-responsive" id="topImg" alt="Cinque Terre" style="width:450px;height:80px">
            <div class="row3">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header card-header-warning">
                            <label id="time" class="pull-right" style="color: white"></label>
                            <h2 class="card-title">图书团采排行榜TOP10</h2>
                        </div>
                        <div class="card-body">
                            <div class="table-responsive">
                                <table class="table" style="table-layout: fixed;width:100%" id="table">
                                    <thead>
                                        <tr id="fontHead">
                                            <td style="width:5%">排名</td>
                                            <td style="width:60%">书名</td>
                                            <!--<th>数量</th>-->
                                            <td style="width:10%">单价</td>
                                            <td style="width:10%">销售总数</td>
                                            <td style="width:15%">总码洋</td>
                                        </tr>
                                    </thead>
                                    <%=GetData() %>
                                </table>
                            </div>
                            <%--<div>
                                <button id="Previous" class="btn btn-default"><<</button>
                                <button id="Next" class="btn btn-default">>></button>
                            </div>--%>
                        </div>
                        <div class="container-fluid">
                            <table class="table">
                                <tr>
                                    <%--<td>
                                        <p style="font-size: 35px">本次书展团采统计&nbsp;&nbsp;&nbsp;总品种：<%=kindsNum %>种 &nbsp;&nbsp;总数量：<%=allCount %>册&nbsp;&nbsp;总码洋：<%=allPrice.ToString("F2") %>元</p>
                                    </td>--%>
                                    <td>本次会展团采统计：</td>
                                    <td>总品种：<%=kindsNum %>种</td>
                                    <td>总数量：<%=allCount %>册</td>
                                    <td>总码洋：<%=allPrice.ToString("F2") %>元</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../js/jquery.min.js"></script>
    <script src="../js/sweetalert2.js"></script>
    <script src="../js/checkLogined.js"></script>
    <script src="../js/public.js"></script>
    <script>
        function reload() {
            //window.location.reload();
            window.location.href = "retailRank.aspx?regionName=" + $("#rName").val();
        }
        window.onload = function () {
            function nowTime(ev, type) {
                /*
                 * ev:显示时间的元素
                 * type:时间显示模式.若传入12则为12小时制,不传入则为24小时制
                 */
                //年月日时分秒
                var Y, M, D, W, H, I, S;
                //月日时分秒为单位时前面补零
                function fillZero(v) {
                    if (v < 10) { v = '0' + v; }
                    return v;
                }
                (function () {
                    var d = new Date();
                    Y = d.getFullYear();
                    M = fillZero(d.getMonth() + 1);
                    D = fillZero(d.getDate());
                    H = fillZero(d.getHours());
                    I = fillZero(d.getMinutes());
                    S = fillZero(d.getSeconds());
                    ev.innerHTML = Y + '年' + M + '月' + D + '日' + '&nbsp;' + H + ':' + I + ':' + S;
                    //每秒更新时间
                    setTimeout(arguments.callee, 1000);
                })();
            }
            nowTime(document.getElementById('time'));
        }
        setTimeout("reload()", 10000);
    </script>
</body>
</html>
