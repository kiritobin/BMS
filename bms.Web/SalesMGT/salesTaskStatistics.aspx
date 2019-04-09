<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="salesTaskStatistics.aspx.cs" Inherits="bms.Web.SalesMGT.salesTaskStatistics" %>

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
    <link rel="stylesheet" id="changeprint" href="../css/a4print.css" media="print">
</head>

<body style="page-break-before: always;">
    <div class="content" id="all">
        <div class="container-fluid">
            <ul class="breadcrumb">
                <li><a href="javascript:;">团采管理</a></li>
                <li><a href="tradeManagement.aspx">销售管理</a></li>
                <li><a href="javascript:;" onclick="history.go(-1);">添加销售</a></li>
                <li class="active">销售计划统计</li>
            </ul>
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="container-fluid">
                            <h3 class="text-center"><strong>销&nbsp;售&nbsp;计&nbsp;划&nbsp;统&nbsp;计</strong></h3>
                            <hr />
                        </div>
                        <div class="card-body">
                            <div class="card-header from-group">
                                <div class="input-group">
                                    <%--<div class="btn-group" role="group">
                                        <input type="text" value="" class="" id="sales_bookName" placeholder="请输入书名">
                                    </div>
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="" id="sales_ISBN" placeholder="请输入ISBN">
                                        <button class="btn btn-info" id="btn_search">查询</button>
                                    </div>--%>
                                    <div class="btn-group" role="group">
                                        <%--<button class="btn btn-info btn-sm" id="print">打印</button>--%>
                                        <button class="btn btn-info btn-sm" id="print" data-toggle="modal" data-target="#printmodel">打印</button>
                                    </div>
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-info btn-sm" id="excel">导出</button>
                                    </div>
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-warning btn-sm" onclick="window.history.back(-1)" id="back">返回</button>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="content_tab col-md-12">
                                    <div class="table-responsive" style="" id="content">
                                        <table class="table table_stock text-center">
                                            <tr class="text-nowrap">
                                                <td>
                                                    <span>销售任务编号:</span>
                                                </td>
                                                <td>
                                                    <input value="<%=saletaskId.ToString() %>" class="form-control" disabled id="XSRWnum">
                                                </td>
                                                <td>
                                                    <span>所属客户:</span>
                                                </td>
                                                <td>
                                                    <input value="<%=customerName %>" class="form-control" disabled id="customer">
                                                </td>
                                                <td>
                                                    <span>所属组织:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="<%=regionName %>" class="form-control" disabled id="startTime">
                                                </td>
                                            </tr>
                                            <tr class="text-nowrap">
                                                <td>
                                                    <span>操作员:</span>
                                                </td>
                                                <td>
                                                    <input value="<%=userName %>" class="form-control" disabled id="operator">
                                                </td>
                                                <td>
                                                    <span>开始日期:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="<%=startTime %>" class="form-control" disabled id="startTime">
                                                </td>
                                                <td>
                                                    <span>结束日期:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="<%=finishTime %>" class="form-control" disabled id="endTime">
                                                </td>
                                            </tr>
                                        </table>

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
                                                        <nobr>单价</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>数量</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>码洋</nobr>
                                                    </td>
                                                    <%--<td>
                                                        <nobr>销折</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>码洋</nobr>
                                                    </td>
                                                    <td>
                                                        <nobr>供应商</nobr>
                                                    </td>--%>
                                                </tr>
                                            </thead>
                                            <%=getData() %>
                                        </table>

                                        <table class="table table_stock text-center">
                                            <tr class="text-nowrap">
                                                <td>
                                                    <span>书籍种数:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="<%=allkinds %>" class="form-control" disabled id="bookKinds">
                                                </td>
                                                <td>
                                                    <span>书本总数:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="<%=allnumber %>" class="form-control" disabled id="allBookCount"></td>
                                                <td>
                                                    <span>总码洋:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="<%=alltotalprice %>" class="form-control" disabled id="alltotalprice">
                                                </td>
                                                <td>
                                                    <span>总实洋:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="<%=allreadprice %>" class="form-control" disabled id="allreadprice">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <%if (ds.Tables[0].Rows.Count > 0)
                                        { %>
                                    <div class="copyright float-right page-box">
                                        <div class="dataTables_paginate paging_full_numbers" id="datatables_paginate">
                                            <div class="m-style paging"></div>
                                        </div>
                                    </div>
                                    <%} %>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="table-responsive" id="a4t">
            <p>
                <h3 class="table-responsive" style="text-align: center"><span id="pname"></span>销售任务单</h3>
            </p>
            <table class="table table_stock text-center">
                <tr class="text-nowrap">
                    <td>
                        <span>销售任务编号:</span>
                    </td>
                    <td>
                        <%--<input value="<%=saletaskId.ToString() %>" class="form-control" disabled>--%>
                        <div><%=saletaskId.ToString() %></div>
                    </td>
                    <td>
                        <span>所属客户:</span>
                    </td>
                    <td>
                        <%--<input value="<%=customerName %>" class="form-control" disabled>--%>
                        <div><%=customerName %></div>
                    </td>
                    <td>
                        <span>操作员:</span>
                    </td>
                    <td>
                        <%--<input value="<%=userName %>" class="form-control" disabled>--%>
                        <div><%=userName %></div>
                    </td>
                </tr>
                <tr class="text-nowrap">
                    <td>
                        <span>开始日期:</span>
                    </td>
                    <td>
                        <div class="jeinpbox">
                            <%--<input type="text" value="<%=startTime %>" class="form-control" disabled>--%>
                            <div><%=startTime %></div>
                        </div>
                    </td>
                    <td>
                        <span>结束日期:</span>
                    </td>
                    <td>
                        <div class="jeinpbox">
                            <%--<input type="text" value="<%=finishTime %>" class="form-control" disabled>--%>
                            <div><%=finishTime %></div>
                        </div>
                    </td>
                </tr>
                <tr class="text-nowrap">
                    <td>
                        <span>书籍种数:</span>
                    </td>
                    <td>
                        <%--<input type="text" value="<%=allkinds %>" class="form-control" disabled>--%>
                        <div><%=allkinds %></div>
                    </td>
                    <td>
                        <span>书本总数:</span>
                    </td>
                    <td>
                        <%--<input type="text" value="<%=allnumber %>" class="form-control" disabled>--%>
                        <div><%=allnumber %></div>
                    </td>
                    <td>
                        <span>总码洋:</span>
                    </td>
                    <td>
                        <%--<input type="text" value="<%=alltotalprice %>" class="form-control" disabled>--%>
                        <div><%=alltotalprice %></div>
                    </td>
                    
                </tr>
                <tr class="text-nowrap">
                    <td>
                        <span>总实洋:</span>
                    </td>
                    <td>
                        <%--<input type="text" value="<%=allreadprice %>" class="form-control" disabled>--%>
                        <div><%=allreadprice %></div>
                    </td>
                </tr>
            </table>
            <table border="1" cellspacing="0" class="table mostTable table-bordered text-center" id="printtable">
                <thead>
                    <tr>
                        <td class="bbb">
                            <nobr>序号</nobr>
                        </td>
                     <%--   <td>
                            <nobr>书号</nobr>
                        </td>--%>
                        <td>
                            <nobr>商品编号</nobr>
                        </td>
                        <td>
                            <nobr>商品名称</nobr>
                        </td>
                        <td>
                            <nobr>单价</nobr>
                        </td>
                        <td>
                            <nobr>数量</nobr>
                        </td>
                        <td>
                            <nobr>码洋</nobr>
                        </td>
                    </tr>
                </thead>
            </table>
        </div>

        <!--打印弹窗-->
        <div class="modal fade" id="printmodel" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
            <div class="modal-dialog" style="max-width: 300px">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title float-left" id="showTittle">请选择打印方式</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            <i class="fa fa-close"></i>
                        </button>
                    </div>
                    <div class="modal-body text-center" style="max-height: 400px;">
                        <div>
                            <button type="button" class="btn btn-info" id="a4">A4纸打印</button>
                            <button type="button" class="btn btn-info" id="zhen">多联纸打印</button>
                        </div>
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
    <script src="../js/salesTaskStatistics.js"></script>
    <!-- selectpicker.js -->
    <script src="../js/bootstrap-selectpicker.js"></script>
    <script src="../js/defaults-zh_CN.js"></script>
    <script src="../js/jquery.pagination.js"></script>
    <script src="../js/jquery-migrate-1.2.1.min.js"></script>
    <script src="../js/jquery.jqprint.js"></script>
    <object id="LODOP_OB" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width="0" height="0">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0"></embed>
    </object>
    <script src="../js/LodopFuncs.js"></script>
    <script src="../js/public.js"></script>
    <script src="../js/checkLogined.js"></script>
</body>

</html>

