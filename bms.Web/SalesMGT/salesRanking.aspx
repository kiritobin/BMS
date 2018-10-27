<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="salesRanking.aspx.cs" Inherits="bms.Web.SalesMGT.seniority" %>

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
            font-weight: bold;
        }

        #fontHead th {
            font-size: 24px;
            font-weight: bold;
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
            <%--<div class="col-sm-4 col-lg-3">--%>
            <img src="../imgs/YNXH-LOGO.png" id="topImg" class="img-responsive" alt="Cinque Terre" style="width: 450px; height: 80px">
            <%--</div>--%>
            <div class="row3">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header card-header-primary">
                            <label id="time" class="pull-right" style="color: white"></label>
                            <h2 class="card-title">客户采购排行TOP10</h2>
                        </div>
                        <div class="card-body">
                            <div class="table-responsive">
                                <table class="table" id="table">
                                    <thead>
                                        <tr id="fontHead">
                                            <th>排名</th>
                                            <th>客户名称</th>
                                            <th>品种数</th>
                                            <th>销售总数(本)</th>
                                            <th>总码洋</th>
                                        </tr>
                                    </thead>
                                    <%=getData() %>
                                </table>
                            </div>
                            <!--<div>
                                <button id="Previous" class="btn btn-default"><<</button>
                                <button id="Next" class="btn btn-default">>></button>
                            </div>-->
                        </div>
                        <div class="container-fluid">
                            <table class="table text-left">
                                <tr>
                                    
                                    <td>本次书展团采统计：</td>
                                    <td>总品种：<%=kindsNum %>种</td>
                                    <td>总数量：<%=allCount %>册</td>
                                    <td>总码洋：<%=allPrice.ToString("F2") %>元</td>
                                    <%--<td>
                                        <span style="font-size: 35px">本次书展团采统计：</span>
                                        <span style="font-size: 35px">总品种:<%=kindsNum %>种&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                        <span style="font-size: 35px">总数量:<%=allCount %>册&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                        <span style="font-size: 35px">总码洋:<%=allPrice.ToString("F2") %>元</span>
                                    </td>--%>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/public.js"></script>
    <script>
        function reload() {
            window.location.reload();
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
        setTimeout("reload()", 600000);
    </script>
</body>

</html>
