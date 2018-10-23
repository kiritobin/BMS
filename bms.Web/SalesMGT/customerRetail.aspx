<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customerRetail.aspx.cs" Inherits="bms.Web.SalesMGT.customerRetail" %>

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
    <!--分页样式-->
    <link rel="stylesheet" href="../css/pagination.css">
    <!-- css样式 -->
    <link rel="stylesheet" href="../css/material-dashboard.min.css">
    <link rel="stylesheet" href="../css/jedate.css" />
    <link rel="stylesheet" href="../css/zgz.css">
    <link rel="stylesheet" href="../css/qc.css">
    <link rel="stylesheet" href="../css/materialdesignicons.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/pretty.min.css">
</head>
<body style="overflow:hidden;">
    <div class="retail-content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-4 col-lg-3" onclick="javascript:history.back(-1);">
                    <img src="../imgs/YNXH-LOGO.png" id="topImg" class="img-fluid" alt="Cinque Terre">
                </div>
                <div class="col-sm-2">
                    <input type="text" id="search" style="height:100px;font-size:30px;" class="topSearch">
                </div>
            </div>
            <div class="card">
                <div class="card-header card-header-danger">
                    <h1 class="card-title text-center">收  银</h1>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-8 text-right">
                            <div class="text-right retailTime">时间：<span id="time"></span></div>
                        </div>
                        <div class="table-responsive col-md-8">
                            <div style="height: 600px; display: block; overflow-y: auto;">
                                <table class="table mostTable table-bordered retailTable text-center test" id="table">
                                    <thead>
                                        <tr>
                                            <td>
                                                <nobr>序号</nobr>
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
                                        <td></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="retailList">
                                <img src="../imgs/login.jpg" alt="img" class="img-fluid" />
                            </div>
                            <div class="container">
                                <div class="row">
                                    <div class="text-white col-sm-6 text-right" id="scanning" data-toggle="modal" data-target="#myModal1">
                                        <button class="btn btn-success btn-lg btnText">扫  描</button>
                                    </div>
                                    <div class="text-white col-sm-6 text-left" id="Settlement" data-toggle="modal" data-target="#myModal2">
                                        <button class="btn btn-danger btn-lg btnText">收  银</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- 合计模块 -->
                        <div class="col-md-8">
                            <fieldset>
                                <legend>
                                    <b>合计</b>
                                </legend>
                                <div class="container retailLabel">
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
                        <!--销售小票-->
                        <div class="print_container border" style="width: 400px;position:relative;top:1000px;" id="sale">
                            <div class="row">
                                <span style="font-size:38px;margin-bottom:50px;">新华书店</span>
                                <span>
                                    <span id="output" style="width: 200px; height: 200px;display:none"></span>
                                    <img src="#" id="img" style="width:70px;height:70px;margin-left:10px;margin-top:20px;" />
                                </span>
                            </div>
                            <hr />
                            <div class="container">
                                <span style="font-size: 16px;">购买时间：<%=DateTime.Now %></span><br />
                                <span style="font-size: 16px;">订单编号：<span id="id"></span></span>
                            </div>
                            <hr />
                            <div class="section4">
                                <table id="tablePay" style="width: 300px;">
                                    <thead>
                                        <tr>
                                            <td style="width: 40%">名称</td>
                                            <td style="width: 20%">数量</td>
                                            <td style="width: 40%">金额</td>
                                        </tr>
                                    </thead>
                                </table>
                                <hr />
                                <table class="other_fee table table-bordered" style="width: 300px;height:100px; font-size:14px;">
                                    <thead>
                                        <tr class="noneDiscount">
                                            <td style="width: 40%">合计：</td>
                                            <td style="width: 20%"><span id="noneNumber" style="margin-left:-25px"></span></td>
                                            <td style="width: 40%;">￥<span style="margin-left:-130px;" id="noneTotal"></span></td>
                                        </tr>
                                        <tr class="discount">
                                            <td style="width: 50%">数量:<span id="allnumber"></span></td>
                                            <td style="width: 50%">折扣:<span id="alldiscount"></span></td>
                                        </tr>
                                        <tr class="discount">
                                            <td style="width: 50%">金额:￥<span id="alltotal"></span></td>
                                            <td style="width: 50%">合计:￥<span id="allreal"></span></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3"><hr /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%"><span id="paytype">现金</span>实付:￥<span id="allcope"></span></td>
                                            <td style="width: 50%">找零:￥<span id="allchange"></span></td>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--选择一号多书模态框-->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content" style="width:800px">
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
    <!--扫描模态框-->
    <div class="modal fade" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel1" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content" style="width:400px; margin-top:50px;">
                <div class="modal-header">
                    <h4 class="modal-title float-left" id="myModalLabel1">请扫描</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        <i class="fa fa-close"></i>
                    </button>
                </div>
                <div class="modal-body" style="margin:0 auto;">
                    单头ID:<input type="text" placeholder="请输入单头ID" id="scannSea" class="searchOne">
                </div>
                <div class="modal-footer"></div>
            </div>
        </div>
    </div>
    <!--结算模态框-->
    <div class="modal fade" id="myModal2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel2" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" id="settleClose" data-dismiss="modal" aria-hidden="true">
                        <i class="fa fa-close"></i>
                    </button>
                </div>
                <div class="modal-body" style="width: 360px; margin: 0 auto; padding-top: 8px;">
                    <h4 class="text-center">结算小票</h4>
                    <hr />
                    <ul class="list-unstyled">
                        <li>码洋合计：<span id="totalEnd"></span></li>
                        <li>码洋折扣：<input id="discountEnd" value=""/></li>
                        <li>实洋合计：<span id="realEnd"></span></li>
                        <li>实付金额：<input id="copeEnd" /></li>
                        <li>找补金额：<span id="change"></span></li>
                        <li id="payType">付款方式：
                            <input type="radio" name="paytype" id="moneyPay" value="现金" checked />
                            <span>现金</span>&nbsp&nbsp&nbsp&nbsp
                            <input type="radio" name="paytype" id="threePay" value="第三方"/>
                            <span>第三方</span>
                        </li>
                    </ul>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-success btn-md" id="btnSettle">打印</button>
                    <button class="btn btn-info btn-md" id="preRecord">补打</button>
                    &nbsp&nbsp&nbsp&nbsp
                    <button class="btn btn-success btn-md" id="btnClose">确定</button>
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
    <script src="../js/customerRetail.js"></script>
    <script src="../js/jquery.tabletojson.js"></script>
    <script src="../js/jquery.qrcode.min.js"></script>
    <object id="LODOP_OB" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width="0" height="0">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0"></embed>
    </object>
    <script src="../js/LodopFuncs.js"></script>
    <script src="../js/public.js"></script>
</body>
</html>
