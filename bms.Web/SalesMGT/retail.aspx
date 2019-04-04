<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="retail.aspx.cs" Inherits="bms.Web.SalesMGT.retail" %>

<%="" %>
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
    <link rel="stylesheet" href="../css/lgd.css">
    <link rel="stylesheet" href="../css/zgz.css">
    <link rel="stylesheet" href="../css/materialdesignicons.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/pretty.min.css">
</head>
<body style="overflow:hidden;">
    <div class="retail-content" >
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-4 col-lg-3">
                    <img src="../imgs/YNXH-LOGO.png" id="topImg" class="img-fluid" alt="Cinque Terre">
                </div>
                <div class="col-sm-2">
                    <input type="text" id="search" style="height:100px;font-size:30px;background-color:#eee;" class="topSearch">
                </div>
                <div class="col-sm-6 text-right">
                    <button class="col-sm-6 btnText" style="background-color: #eee; border: 0; color: #eee;" id="preRecord"></button>
                </div>
            </div>
            <div class="card">
                <div class="card-header card-header-danger">
                    <h1 class="card-title text-center">自助购书开单</h1>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-8 text-right">
                            <div class="text-right retailTime">时间：<span id="time"></span></div>
                        </div>
                        <!-- 左侧数据表 -->
                        <div class="table-responsive col-md-8">
                            <div style="height: 450px; display: block; overflow-y: auto;">
                                <table class="table mostTable retailTable table-bordered text-center test" id="table">
                                    <thead>
                                         <tr>
                                            <td style="width:10%">
                                                <nobr>ISBN</nobr>
                                            </td>
                                            <td style="width:25%">
                                                <nobr>书名</nobr>
                                            </td>
                                            <td style="width:5%">
                                                <nobr>单价</nobr>
                                            </td>
                                             <td style="display: none">
                                                <nobr>数量</nobr>
                                            </td>
                                            <td style="width:20%">
                                                <nobr>商品数量</nobr>
                                            </td>
                                            <td>
                                                <nobr>折扣</nobr>
                                            </td>
                                            <td style="width:10%">
                                                <nobr>码洋</nobr>
                                            </td>
                                            <td style="display: none">
                                                <nobr>实洋</nobr>
                                            </td>
                                             <td style="display: none">
                                                <nobr>书号</nobr>
                                            </td>
                                            <td style="width:10%">
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
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <!-- 右侧功能区 -->
                        <div class="col-md-4">
                            <div class="retailList">
                                <img src="../imgs/书市指引.gif" alt="img" class="img-fluid" />
                            </div>
                            <div class="row">
                                <div class="text-white col-sm-6 text-right" id="insert">
                                    <button class="btn btn-success btn-lg col-sm-12" style="height:100px;border-radius: 15px;" value=""><p style="font-size:30px;margin-bottom:0px;margin-left:-17px;">确认购买</p></button>
                                </div>
                                <div class="text-white col-sm-6 text-left" id="giveup">
                                    <button class="btn btn-danger btn-lg col-sm-12" style="height:100px;border-radius: 15px;" value=""><p style="font-size:30px;margin-bottom:0px;margin-left:-17px;">放弃购买</p></button>
                                </div>
                            </div>
                        </div>
                        <!-- 合计模块 -->
                        <div class="col-md-8" style="margin-top:-150px">
                            <fieldset>
                                <legend>
                                    <b>合计</b>
                                </legend>
                                <div class="container retailLabel">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>品种：&nbsp&nbsp<span id="kind"></span></label>
                                        </div>
                                        <div class="col-md-6">
                                            <label>数量：&nbsp&nbsp<span id="number"></span></label>
                                        </div>
                                        <div class="col-md-6">
                                            <label>码洋：￥&nbsp&nbsp<span id="total"></span></label>
                                        </div>
                                        <div class="col-md-6">
                                            <label>应收：￥&nbsp&nbsp<span id="real"></span></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="pull-right">
                                    <p style="color: red; font-size:16px;margin-bottom:0px;">*实际收款以POS结算为准，此处应收金额仅供参考！</p>
                                </div>
                            </fieldset>
                        </div>
                        <!--小票样式-->
                        <div class="border content" style="position: relative; top: 300px; height: 500px;" id="ticket">
                            <div style="margin: 0 auto; color: black; font-weight: 400">
                                <br />
                                <table class="table">
                                    <tr>
                                        <td style="width: 50%; text-align: left;">品种：<span id="kindEnd"></span>&nbsp&nbsp</td>
                                        <td style="width: 50%; text-align: left;">数量：<span id="numberEnd"></span>&nbsp&nbsp</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%; text-align: left;">金额：￥&nbsp&nbsp<span id="totalEnd"></span></td>
                                        <td style="width: 50%; text-align: left;">折扣：&nbsp&nbsp<span id="discount"></span></td>
                                    </tr>
                                </table>
                                <div style="margin-top: 10px;">
                                    <div id="output" style="width: 200px; height: 200px; display: none"></div>
                                    <img src="#" style="width: 150px; height: 150px; margin-left: 30px;" id="img" />
                                </div>
                                <div style="width: 260px; margin-left: 10px;">
                                    <hr style="color: lightseagreen" />
                                    时间：<span id="timeEnd"></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- footer -->
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
    <!--选择图书模态框-->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog" style="max-width:800px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title float-left" id="myModalLabel">请选择需要的图书</h4>
                    <button type="button" id="close" class="close" data-dismiss="modal" aria-hidden="true">
                        <i class="fa fa-close"></i>
                    </button>
                </div>
                <div class="modal-body">
                    <table class="table mostTable table-bordered text-center" id="table2">
                        <thead>
                            <tr>
                                <th>
                                    <div class="pretty inline">
                                        <input type="radio" name="radio" disabled="disabled">
                                        <label aria-disabled="true"><i class="mdi mdi-check"></i></label>
                                    </div>
                                </th>
                                <th>
                                    <nobr>ISBN</nobr>
                                </th>
                                <th>
                                    <nobr>书号</nobr>
                                </th>
                                <th>
                                    <nobr>书名</nobr>
                                </th>
                                <th>
                                    <nobr>单价</nobr>
                                </th>
                                <th>
                                    <nobr>出版社</nobr>
                                </th>
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
    <script src="../js/defaults-zh_CN.js"></script>
    <!-- alert.js -->
    <script src="../js/sweetalert2.js"></script>
    <script src="../js/retail.js"></script>
    <script src="../js/jquery.tabletojson.js"></script>
    <script src="../js/jquery.qrcode.min.js"></script>
    <object id="LODOP_OB" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width="0" height="0">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0"></embed>
    </object>
    <script src="../js/LodopFuncs.js"></script>
    <script src="../js/public.js"></script>
</body>
</html>
