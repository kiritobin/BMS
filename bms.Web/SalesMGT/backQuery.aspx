<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="backQuery.aspx.cs" Inherits="bms.Web.SalesMGT.backQuery" %>

<!DOCTYPE html>
<html class="no-js">

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
    <link rel="stylesheet" href="../css/pagination.css" />
    <link rel="stylesheet" href="../css/materialdesignicons.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/pretty.min.css">
</head>
<body>
    <div class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="container-fluid">
                            <h3 class="text-center"><strong>销&nbsp;退</strong></h3>
                            <hr />
                        </div>
                        <div class="card-body">
                            <div class="card-header from-group">
                                <div class="input-group">
                                    <!--<div class="btn-group" role="group">
                                        <button class="btn btn-success" id="add_back">添加销退</button>
                                    </div>
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="sales_search">
                                        <button class="btn btn-info">查询</button>
                                    </div>-->
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-warning btn-sm" id="toBack">返回</button>
                                    </div>
                                    <%string type = Session["type"].ToString();
                                        if (type == "search")
                                        { %>
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-info btn-sm" id="export">导出</button>
                                    </div>
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-info btn-sm" id="print">打印</button>
                                    </div>
                                    <%} %>
                                    <%
                                        if (type != "search")
                                        { %>
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-success btn-sm" id="sure">保存单据</button>
                                    </div>
                                    <%} %>
                                </div>
                            </div>
                            <div class="content_tab" id="print_content">
                                <%if (type == "search")
                                    { %>
                                <div id="content">
                                    <table class="table table_stock text-right">
                                        <tr class="text-nowrap">
                                            <td>
                                                <span>任务单据编号:</span>
                                            </td>
                                            <td>
                                                <input value="<%=Session["saleId"].ToString() %>" class="form-control" disabled id="XSRW">
                                            </td>
                                            <td>
                                                <span>单头编号:</span>
                                            </td>
                                            <td>
                                                <input value="<%=Session["sellId"].ToString() %>" class="form-control" disabled id="XT">
                                            </td>
                                            <td>
                                                <span>制单时间:</span>
                                            </td>
                                            <td>
                                                <input value="<%=staticsTime %>" class="form-control" disabled id="makeTime">
                                            </td>
                                        </tr>
                                    </table>
                                    <%} %>
                                    <div>
                                        <table class="table mostTable table-bordered text-center" id="table">
                                            <%=GetData() %>
                                        </table>
                                        <table class="table table_stock text-right">
                                            <tr class="text-nowrap">
                                                <td>总合计：</td>
                                                <td>
                                                    <span>总品种:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="<%=staticsKinds %>" class="form-control" disabled id="allKinds">
                                                </td>
                                                <td>
                                                    <span>总数量:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="<%=staticsNumber %>" class="form-control" disabled id="allCount"></td>
                                                <td>
                                                    <span>总码洋:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="<%=staticsTotalPrice %>" class="form-control" disabled id="allTotalPrice">
                                                </td>
                                                <td>
                                                    <span>总实洋:</span>
                                                </td>
                                                <td>
                                                    <input type="text" value="<%=staticsRealPrice %>" class="form-control" disabled id="allRealPrice">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
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
                    &nbsp;版权归云南新华书店图书有限公司所有
                        <p>建议使用<a href="../chrome/ChromeDownload.html">Google浏览器</a>浏览网页</p>
                </div>
            </div>
        </footer>
    </div>
    <!--模态框-->
    <div class='modal fade' id='myModa2' tabindex='-1' role='dialog' aria-labelledby='myModalLabel' aria-hidden='true' data-backdrop='static'>
        <div class='modal-dialog' style='max-width: 900px;'>
            <div class='modal-content'>
                <div class='modal-header'>
                    <h4 class='modal-title float-left' id='myModalLabel'>请选择你要进行操作的书籍</h4>
                    <button type='button' class='close' data-dismiss='modal' aria-hidden='true'>
                        <i class="fa fa-close"></i>
                    </button>
                </div>
                <div class='modal-body'>
                    <table id='tablebook' class='table mostTable table-bordered text-center'>
                    </table>
                </div>
                <div class='modal-footer'>
                    <button type='button' class='btn btn-success btn-sm' id='sureBook'>确定</button>
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
    <script src="../js/defaults-zh_CN.js"></script>
    <script src="../js/backQuery.js"></script>
    <script src="../js/jquery-migrate-1.2.1.min.js"></script>
    <%--<script src="../js/jquery.jqprint.js"></script>--%>
    <script src="../js/public.js"></script>
    <object id="LODOP_OB" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width="0" height="0">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0"></embed>
    </object>
    <script src="../js/LodopFuncs.js"></script>
</body>

</html>
