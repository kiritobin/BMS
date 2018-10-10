﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="textarea.aspx.cs" Inherits="bms.Web.demo.textarea" %>

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
    <!-- CSS Files -->
    <link href="../css/material-dashboard.min.css?v=2.0.2" rel="stylesheet" />
    <!-- 消息提示框所需css -->
    <style>
        .textarea{
            height:22px;
            width:200px;
            overflow:hidden;
        }
    </style>
</head>

<body>
    <textarea name="textarea" class="textarea" rows="1" maxlength="14" onkeyup="this.value=this.value.replace(/[^\r\n0-9]/g,'');"></textarea>
    <!--   Core JS Files   -->
    <script src="../js/jquery-3.3.1.min.js"></script>
</body>

</html>