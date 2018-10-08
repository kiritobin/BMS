<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="salesRanking.aspx.cs" Inherits="bms.Web.SalesMGT.seniority" %>

<%="" %>
<!DOCTYPE html>

<html class="no-js">
<!--<![endif]-->

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>图书综合管理系统</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 字体图标样式 -->
    <link rel="stylesheet" href="../css/font-awesome.min.css">
    <!-- css样式 -->
    <link rel="stylesheet" href="../css/material-dashboard.min.css">
    <link rel="stylesheet" href="../css/pagination.css" />
    <link rel="stylesheet" href="../css/zgz.css">
    <link rel="stylesheet" href="../css/lgd.css">
    <link rel="stylesheet" href="../css/qc.css">

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
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-header card-header-danger">
                                    <h3 class="card-title">客户采购排行</h3>
                                </div>
                                <div class="card-body">
                                    <div class="table-responsive">
                                        <table class="table">                                        
                                            <thead>
                                                <tr>
                                                <th>客户名称</th>
                                                <th>数量</th>
                                                <th>品种</th>
                                                <th>销售总数</th>
                                                <th>总码洋</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>1</td>
                                                    <td>Dakota Rice</td>
                                                    <td>Niger</td>
                                                    <td>Oud-Turnhout</td>
                                                    <td>$36,738</td>
                                                </tr>
                                                <tr>
                                                    <td>2</td>
                                                    <td>Sage Rodriguez
                                                    <td>Netherlands</td>
                                                    <td>Baileux</td>
                                                    <td >$56,142</td>
                                                </tr>                                             
                                            </tbody>
                                        </table>
                                    </div>
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
