<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="inventoryStatistics.aspx.cs" Inherits="bms.Web.InventoryMGT.inventoryStatistics" %>

<%="" %>
<!DOCTYPE html>

<html class="no-js">
<!--<![endif]-->

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>云南新华书店项目综合管理系统</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 字体图标样式 -->
    <link rel="stylesheet" href="../css/font-awesome.min.css">
    <!-- css样式 -->
    <link rel="stylesheet" href="../css/material-dashboard.min.css">
    <link rel="stylesheet" href="../css/zgz.css">
    <link rel="stylesheet" href="../css/lgd.css">
    <link rel="stylesheet" href="../css/demo.css">
    <link rel="stylesheet" href="../css/pagination.css">
    <link rel="stylesheet" type="text/css" href="../css/pretty.min.css">
    <link rel="stylesheet" type="text/css" href="../css/materialdesignicons.min.css">
    <link rel="stylesheet" href="../css/jedate.css" />
    <script src="../js/jedate.min.js"></script>
</head>

<body>
    <div class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="container-fluid">
                    <p></p>
                    <ul class="breadcrumb">
                        <li><a href="javascript:;">库存管理</a></li>
                        <li><a href="stockManagement.aspx" id="rkgl">入库管理</a></li>
                        <li class="active" id="rktj">入库统计</li>
                    </ul>
                    <div class="card">
                        <div class="container-fluid">
                            <h3 class="text-center"><strong id="tjType">入&nbsp;库&nbsp;统&nbsp;计</strong></h3>
                            <hr />
                        </div>
                        <div class="card-body">
                            <div class="card-header from-group">
                                <div class="input-group">
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="" id="bookIsbn" placeholder="请输入ISBN">
                                    </div>
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="" id="bookName" placeholder="请输入书名">
                                    </div>
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="" readonly="readonly" id="time" placeholder="请输入时间" data-toggle="modal" data-target="#timeModal">
                                    </div>
                                    <div class="btn-group" role="group">
                                        <select class="modal_select selectpicker" title="请选择供应商" data-live-search="true" id="supplier">
                                            <option>请选择供应商</option>
                                            <%for (int i = 0; i < dsSupplier.Rows.Count; i++)
                                                {%>
                                            <option value="<%=dsSupplier.Rows[i]["supplier"] %>"><%=dsSupplier.Rows[i]["supplier"] %></option>
                                            <%} %>
                                        </select>
                                        <%--<input type="text" value="" class="" id="supplier" placeholder="请输入供应商">--%>
                                    </div>
                                    <div class="btn-group" role="group">
                                        <select class="modal_select selectpicker" title="请选择制单员" data-live-search="true" id="userName">
                                            <option>请选择制单员</option>
                                            <%for (int i = 0; i < dsUser.Rows.Count; i++)
                                                {%>
                                            <option><%=dsUser.Rows[i]["userName"] %></option>
                                            <%} %>
                                        </select>
                                        <%--<input type="text" value="" class="" id="userName" placeholder="请输入制单员">--%>
                                    </div>
                                    <div class="btn-group" role="group">
                                        <select class="modal_select selectpicker" title="<%=title %>" data-live-search="true" id="resource">
                                            <option id="change">请选择<%=title %></option>
                                            <%for (int i = 0; i < dsSource.Rows.Count; i++)
                                                {%>
                                            <option><%=dsSource.Rows[i]["regionName"] %></option>
                                            <%} %>
                                        </select>
                                        <%--<input type="text" value="" class="" id="resource" placeholder="请输入来源组织">--%>
                                    </div>
                                    <%--<div class="btn-group" role="group">
                                        <select class="modal_select selectpicker" title="请选择组织" data-live-search="true" id="region">
                                            <option>全部组织</option>
                                            <%for (int i = 0; i < dsRegion.Tables[0].Rows.Count; i++)
                                                {%>
                                            <option><%=dsRegion.Tables[0].Rows[i]["regionName"] %></option>
                                            <%} %>
                                        </select>
                                    </div>--%>
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-info" id="btn_search">查询</button>
                                    </div>
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-warning btn-sm" onclick="window.history.go(-1);" id="back">返回</button>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="content_tab col-md-12">
                                    <div class="table-responsive" style="" id="content">
                                        <table class="table mostTable table-bordered text-center" id="table">
                                            <thead>
                                                <tr>
                                                    <td class="bbb">
                                                        <nobr>序号</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>书号</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>ISBN</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>书名</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>供应商</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>数量</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>码洋</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>实洋</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr id="diff">来源组织</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>制单员</nobr>
                                                    </td>
                                                    <%if (isAdmin)
                                                        { %>
                                                    <td>
                                                        <nobr>组织名称</nobr>
                                                    </td>
                                                    <%} %>
                                                    <td>
                                                        <nobr>制单时间</nobr>
                                                    </td>
                                                </tr>
                                            </thead>
                                            <%=getData() %>
                                        </table>
                                        <table class="table table_stock text-center" id="table2">
                                            <tr class="text-nowrap">
                                                <td>
                                                    <span>书籍种数:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="<%=sjNum %>" id ="sjNum" class="form-control" disabled>
                                                </td>
                                                <td>
                                                    <span>书本总数:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="<%=sbNum %>" id ="sbNum" class="form-control" disabled></td>
                                                <td>
                                                    <span>总码洋:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="<%=total %>" id ="total" class="form-control" disabled>
                                                </td>
                                                <td>
                                                    <span>总实洋:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="<%=real %>" id ="real" class="form-control" disabled>
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
        </div>

        <!--时间选择模态框-->
        <div class="modal fade" id="timeModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title float-left" id="timeModalLabel">请选择查询时间段</h4>
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
                    &nbsp;版权归云南新华书店图书有限公司所有 滇ICP备09009025号-4
                        <p>建议使用<a href="../chrome/ChromeDownload.html">Google浏览器</a>浏览网页</p>
                </div>
            </div>
        </footer>
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
    <script src="../js/defaults-zh_CN.js"></script>
    <script src="../js/jquery.pagination.js"></script>
    <script src="../js/jquery-migrate-1.2.1.min.js"></script>
    <script src="../js/inventoryStatistics.js"></script>
    <object id="LODOP_OB" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width="0" height="0">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0"></embed>
    </object>
    <script src="../js/LodopFuncs.js"></script>
    <script src="../js/public.js"></script>
    <script src="../js/checkLogined.js"></script>
</body>

</html>
