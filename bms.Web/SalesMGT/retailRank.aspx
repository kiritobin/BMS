<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="retailRank.aspx.cs" Inherits="bms.Web.SalesMGT.retailRank" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>云南新华书店项目综合管理系统</title>
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- 字体图标样式 -->
    <link rel="stylesheet" href="../css/font-awesome.min.css" />
    <!-- css样式 -->
    <link rel="stylesheet" href="../css/material-dashboard.min.css" />
    <link rel="stylesheet" href="../css/pagination.css" />
    <link rel="stylesheet" href="../css/zgz.css" />
    <link rel="stylesheet" href="../css/lgd.css" />
    <link rel="stylesheet" href="../css/qc.css" />
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
            <img src="../imgs/YNXH-LOGO.png" id="topImg" class="img-responsive" alt="Cinque Terre" style="width: 450px; height: 80px" />
            <%--</div>--%>
            <div class="row3">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header card-header-danger">
                            <label id="time" class="pull-right" style="color: white"></label>
                            <h2 class="card-title">图书零售排行榜TOP10</h2>
                        </div>
                        <div class="card-body">
                            <div class="table-responsive">
                                <table class="table" id="table">
                                    <thead>
                                        <tr id="fontHead">
                                            <th>排名</th>
                                            <th>书名</th>
                                            <!--<th>数量</th>-->
                                            <th>单价</th>
                                            <th>销售总数</th>
                                            <th>总码洋</th>
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
                                    <td>总品种：<%=kindsNum %></td>
                                    <td>总数量：<%=allCount %></td>
                                    <td>总码洋：<%=Math.Round(allPrice,2) %></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../js/public.js"></script>
    <script>
        function reload() {
            window.location.reload();
        }
        setTimeout("reload()", 600000);
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
    </script>
</body>
</html>
