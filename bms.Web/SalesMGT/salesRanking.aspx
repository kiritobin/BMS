<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="salesRanking.aspx.cs" Inherits="bms.Web.SalesMGT.seniority" %>

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
    <link rel="stylesheet" href="../css/qc.css" />
</head>

<body>
    <!--[if lt IE 7]>
            <p class="browsehappy">You are using an <strong>outdated</strong> browser. Please <a href="#">upgrade your browser</a> to improve your experience.</p>
        <![endif]-->
   <div class="wrapper wrapper-content">
       <div class="container">
            <div class="row">
                <div class="col-lg-12">
                <div class="ibox float-e-margins center-block">
                    <div class="ibox-title">
                        <h3>客户销售排行</h3>
                    </div>
                    <div class="ibox-content">
                        <table class="table">
                            <thead>
                            <tr>
                                <th>序号</th>
                                <th>客户名称</th>
                                <th>品种</th>
                                <th>数量</th>
                                <th>总码洋</th>
                            </tr>
                            </thead>
                            <tbody>
                            <tr>
                                <td>1</td>
                                <td>小明</td>
                                <td>34</td>
                                <td>23</td>
                                <td>243</td>
                            </tr>
                            </tbody>
                        </table>
                        <div>
                            <button id="Previous" class="btn btn-default"><<</button>
                            <button id="Next" class="btn btn-default">>></button>
                        </div>  
                    </div><div class="col-lg-3"></div></div>
                </div>
            </div>
        </div>
   </div>

</body>
</html>
