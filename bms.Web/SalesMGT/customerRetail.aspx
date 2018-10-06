<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sales_demo.aspx.cs" Inherits="bms.Web.demo.sales_demo" %>

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
    <title>图书综合管理平台</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 字体图标样式 -->
    <link rel="stylesheet" href="../css/font-awesome.min.css">
    <!-- css file -->
    <link rel="stylesheet" href="../css/material-dashboard.min.css">
    <link rel="stylesheet" href="../css/zgz.css" />
</head>

<body>
    <!--[if lt IE 7]>
            <p class="browsehappy">You are using an <strong>outdated</strong> browser. Please <a href="#">upgrade your browser</a> to improve your experience.</p>
        <![endif]-->
    <div class="retail-content">
        <div class="container-fluid">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="input-group col-md-12">
                            <div class="btn-group" role="group">
                                <input type="text" value="" class="searchOne">
                                <button class="btn btn-info btn-sm">查询</button>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive col-md-10">
                            <table class="table mostTable table-bordered text-center">
                                <thead>
                                    <tr>
                                        <th>ISBN号</th>
                                        <th>书名</th>
                                        <th>单价</th>
                                        <th>数量</th>
                                        <th>实洋</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>1</td>
                                        <td>2</td>
                                        <td>3</td>
                                        <td>4</td>
                                        <td>5</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="retailList col-md-2">
                            <h3 class="text-center">零售单</h3>
                            <hr />
                            <ul class="list-unstyled">
                                <li>时间：</li>
                                <li>单号：</li>
                                <li>种类：</li>
                                <li>数量：</li>
                                <li>总码洋：</li>
                                <li>总实洋：</li>
                            </ul>
                            <hr />
                            <div class="input-group text-white">
                                <a class="btn btn-warning btn-sm"><i class="fa fa-qrcode"></i></a>
                                <a class="btn btn-success btn-sm"><i class="fa fa-jpy"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- js file -->
    <script src="../js/jquery-3.3.1.min.js"></script>
</body>

</html>
