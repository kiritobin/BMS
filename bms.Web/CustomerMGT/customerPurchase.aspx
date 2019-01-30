<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customerPurchase.aspx.cs" Inherits="bms.Web.CustomerMGT.customerPurchase" %>

<!DOCTYPE html>
<html class="no-js">

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
    <link rel="stylesheet" href="../css/zgz.css">
    <link rel="stylesheet" href="../css/pagination.css" />
    <link rel="stylesheet" href="../css/jedate.css" />
    <script src="../js/jedate.min.js"></script>
</head>

<body>
    <!--[if lt IE 7]>
            <p class="browsehappy">You are using an <strong>outdated</strong> browser. Please <a href="#">upgrade your browser</a> to improve your experience.</p>
        <![endif]-->
    <div class="wrapper ">
        <div class="main-panel" style="margin: 0px auto; float: none;">
            <!-- 主界面头部面板 -->
            <nav class="navbar navbar-expand-lg navbar-transparent navbar-absolute fixed-top ">
                <div class="container">
                    <div class="navbar-wrapper">
                    </div>
                    <a class="btn btn-default btn-md" href="#"><%=userName %></a>
                    <a class="btn btn-danger btn-md" href="javascript:logout();">退出系统</a>
                </div>
            </nav>
            <!-- 主界面内容 -->
            <div class="content">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-header card-header-danger">
                                    <h4 class="card-title">客户采购查询</h4>
                                </div>
                                <div class="card-body">
                                    <div class="card-header from-group">
                                        <div class="input-group">
                                            <div class="btn-group" role="group">
                                                <input type="text" value="" class="" id="isbnSearch" placeholder="请输入ISBN">
                                            </div>
                                            <div class="btn-group" role="group">
                                                <input type="text" value="" class="" id="bookSearch" placeholder="请输入书名">
                                            </div>
                                            <div class="btn-group" role="group">
                                                <input type="text" class="" placeholder="请输入时间段" readonly="readonly" id="time" data-toggle="modal" data-target="#myModal" />
                                            </div>
                                            <div class="btn-group" role="group">
                                                <select id="goodsSearch" class="selectpicker" data-live-search="true" title="请选择团采地点">
                                                    <option value="">请选择团采地点</option>
                                                    <%for (int i = 0; i < dtRegion.Rows.Count; i++)
                                                        {%>
                                                    <option value="<%=dtRegion.Rows[i]["regionName"] %>"><%=dtRegion.Rows[i]["regionName"] %></option>
                                                    <%} %>
                                                </select>
                                                <%--<input type="text" value="" class="searchOne" id="goodsSearch" placeholder="请输入团采地点">--%>
                                                <button class="btn btn-info btn-sm" id="btn-search">查询</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table mostTable table-bordered text-center" id="table">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <nobr>采集单号</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>ISBN</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>书名</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>单价</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>数量</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>码洋（册）</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>实洋</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>折扣</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>团采地点</nobr>
                                                    </th>
                                                    <th>
                                                        <nobr>采购日期</nobr>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <%=getData()%>
                                        </table>
                                        <table class="table table_stock text-center">
                                            <tr class="text-nowrap">
                                                <td>
                                                    <span>书籍种数:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="" class="form-control" disabled id="bookKinds">
                                                </td>
                                                <td>
                                                    <span>书本总数:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="" class="form-control" disabled id="allBookCount"></td>
                                                <td>
                                                    <span>总码洋:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="" class="form-control" disabled id="alltotalprice">
                                                </td>
                                                <td>
                                                    <span>总实洋:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="" class="form-control" disabled id="allreadprice">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="copyright float-right page-box">
                                        <div class="dataTables_paginate paging_full_numbers" id="datatables_paginate">
                                            <div class="m-style paging"></div>
                                            <%--分页栏--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--时间选择模态框-->
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title float-left" id="myModalLabel">请选择查询时间段</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                <i class="fa fa-close"></i>
                            </button>
                        </div>
                        <div class="modal-body" style="max-height: 400px; overflow: auto;">
                            <table class="table text-center model-table">
                                <tr>
                                    <td class="text-right" style="width: 40%">开始时间:
                                    </td>
                                    <td class="text-left">
                                        <div class="jeinpbox">
                                            <input type="text" class="jeinput text-center" readonly="readonly" id="startTime" placeholder="年--月--日" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-right" style="width: 40%">结束时间:
                                    </td>
                                    <td class="text-left">
                                        <div class="jeinpbox">
                                            <input type="text" class="jeinput text-center" readonly="readonly" id="endTime" placeholder="年--月--日" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">例：开始时间2018-10-26,结束时间2018-10-29;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">只统计26、27、28;&nbsp;&nbsp;&nbsp;不统计29</td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-info" id="modalClose">清空</button>&nbsp;&nbsp;&nbsp;&nbsp;
                            
                            <button type="button" class="btn btn-info" id="btnOK">确认</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- 主界面页脚部分 -->
            <footer class="footer">
                <div class="container-fluid">
                    <!-- 版权内容 -->
                    <div class="copyright text-center">
                        &copy;
                        <script>
                            document.write(new Date().getFullYear())
                        </script>
                        &nbsp;版权归云南新华书店图书有限公司所有
                        <p>建议使用<a href="../chrome/ChromeDownload.html">Google浏览器</a>浏览网页</p>
                    </div>
                </div>
            </footer>
        </div>
    </div>
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/popper.min.js"></script>
    <script src="../js/bootstrap-material-design.min.js"></script>
    <script src="../js/perfect-scrollbar.jquery.min.js"></script>
    <script src="../js/material-dashboard.min.js"></script>
    <script src="../js/sweetalert2.js"></script>
    <script src="../js/customerPurchase.js"></script>
    <script src="../js/jquery.pagination.js"></script>
    <script src="../js/public.js"></script>
    <script src="../js/checkLogined.js"></script>
    <script src="../js/bootstrap-selectpicker.js"></script>
    <script src="../js/defaults-zh_CN.js"></script>
</body>
</html>
