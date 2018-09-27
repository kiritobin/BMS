<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="backQuery.aspx.cs" Inherits="bms.Web.SalesMGT.backQuery" %>

<!DOCTYPE html>
<!--[if lt IE 7]>      <html class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
<!--[if IE 7]>         <html class="no-js lt-ie9 lt-ie8"> <![endif]-->
<!--[if IE 8]>         <html class="no-js lt-ie9"> <![endif]-->
<!--[if gt IE 8]><!-->
<html class="no-js">
<!--<![endif]-->

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title></title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 字体图标样式 -->
    <link rel="stylesheet" href="../css/font-awesome.min.css">
    <!-- css file -->
    <link rel="stylesheet" href="../css/material-dashboard.min.css">
    <link href="../css/zgz.css" rel="stylesheet" />
</head>

<body>
    <!--[if lt IE 7]>
            <p class="browsehappy">You are using an <strong>outdated</strong> browser. Please <a href="#">upgrade your browser</a> to improve your experience.</p>
        <![endif]-->
    <div class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <h3 class="text-center">销&nbsp;退</h3>
                        <hr />                      
                        <div class="card-body">
                            <div class="card-header card_btn">
                                <div class="input-group">
                                    <div class="btn-group" role="group">
                                        <button class="btn btn-success" data-toggle="modal" data-target="#myModa2">添加销售</button>
                                    </div>
                                    <div class="btn-group" role="group">
                                        <input type="text" value="" class="sales_search">
                                        <button class="btn btn-info">查询</button>
                                    </div>
                                     <div class="btn-group" role="group">
                                        <button class="btn btn-success" data-toggle="modal" data-target="#myModa2"><i class="fa fa-print" aria-hidden="true"></i></button>
                                    </div>
                                </div>
                            </div>
                            <div class="content_tab col-md-12">
                                    <div class="table-responsive col-md-10">
                                        <table class="table mostTable table-bordered text-center">
                                            <thead>
                                                <tr>
                                                    <th>书号</th>
                                                    <th>ISBN号</th>
                                                    <th>实际折扣</th>
                                                    <th>单价</th>
                                                    <th>数量</th>
                                                    <th>实洋</th>
                                                    <th>时间</th>
                                                    <th>操作</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>1</td>
                                                    <td>2</td>
                                                    <td>3</td>
                                                    <td>4</td>
                                                    <td>5</td>
                                                    <td>6</td>
                                                    <td>7</td>
                                                    <td>
                                                        <button class="btn btn-danger">
                                                            <i class="fa fa-trash" aria-hidden="true"></i>
                                                        </button>                                                       
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                <div class="statistics col-md-2">
                                    统计
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--模态框-->
    <div class="modal fade" id="myModa2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog" style="max-width: 900px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title float-left" id="myModalLabel">添加销退</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        <i class="material-icons">clear</i>
                    </button>
                </div>
                <div class="modal-body">
                    <table class="table model-table">
                        <tr>
                            <td>ISBN号</td>
                            <td>
                                <input type="text" value="" class="sales_search"></td>
                            <td>实际折扣</td>
                            <td>
                                <input type="text" value="" class="sales_search"></td>
                            <td>单价</td>
                            <td>
                                <input type="text" value="" class="sales_search"></td>
                        </tr>
                        <tr>
                            <td>数量</td>
                            <td>
                                <input type="text" value="" class="sales_search"></td>
                             <td>实洋</td>
                            <td>
                                <input type="text" value="" class="sales_search"></td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="submit" class="btn btn-success btn-sm" id="btnAdd">添加</button>
                </div>
            </div>
        </div>
    </div>
    <!-- js file -->
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/material-dashboard.min.js"></script>
    <script>
    function print(){
        var printData = $('.bootstrap-table').parent().html();
        window.document.body.innerHTML = printData;
        // 开始打印
        window.print();
        window.location.reload(true);
    }</script>

</body>

</html>