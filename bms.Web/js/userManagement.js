$(document).ready(function () {
    $('.paging').pagination({
        pageCount: $("#countPage").val(), //总页数
        jump: true,
        mode: 'fixed',//固定页码数量
        coping: true,
        homePage: '首页',
        endPage: '尾页',
        prevContent: '上页',
        nextContent: '下页',
        callback: function (api) {
            var strWhere = $("#input-search").val().trim();
            var regionId = $("#select-region").val().trim();
            var roleId = $("#select-role").val().trim();
            $.ajax({
                type: 'Post',
                url: 'userManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    role: roleId,
                    region: region,
                    search: search,
                    op: "paging"
                },
                dataType: 'text',
                success: function (data) {
                    $("#table tr:not(:first)").empty(); //清空table处首行
                    $("#table").append(data); //加载table
                }
            });
        }
    });
})

//点击查询按钮时
$("#btn-search").click(function () {
    var strWhere = $("#input-search").val().trim();
    var regionId = $("#select-region").val().trim();
    var roleId = $("#select-role").val().trim();
    $.ajax({
        type: 'Post',
        url: 'userManagement.aspx',
        data: {
            role: roleId,
            region: regionId,
            search: strWhere,
            op: "paging"
        },
        dataType: 'text',
        success: function (data) {
            $("#table tr:not(:first)").empty(); //清空table处首行
            $("#table").append(data); //加载table
        }
    });
});
//地区下拉框改变事件
$("#select-region").change(function () {
    var strWhere = $("#input-search").val().trim();
    var regionId = $("#select-region").val().trim();
    var roleId = $("#select-role").val().trim();
    $.ajax({
        type: 'Post',
        url: 'userManagement.aspx',
        data: {
            role: roleId,
            region: regionId,
            search: strWhere,
            op: "paging"
        },
        dataType: 'text',
        success: function (data) {
            $("#table tr:not(:first)").empty(); //清空table处首行
            $("#table").append(data); //加载table
        }
    });
})
//角色下拉框改变事件
$("#select-role").change(function () {
    var roleId = $("#select-role").val().trim();
    var strWhere = $("#input-search").val().trim();
    var regionId = $("#select-region").val().trim();
    $.ajax({
        type: 'Post',
        url: 'userManagement.aspx',
        data: {
            role: roleId,
            region: regionId,
            search: strWhere,
            op: "paging"
        },
        dataType: 'text',
        success: function (data) {
            $("#table tr:not(:first)").empty(); //清空table处首行
            $("#table").append(data); //加载table
        }
    });
})

//添加用户
$("#btnAdd").click(function () {
    var name = $("#inputName").val();
    var account = $("#inputAccount").val();
    var region = $("#model-select-region").val();
    var role = $("#model-select-role").val();
    if (account == null || account == "") {
        alert("账号不能为空！");
    } else if (region == null || region == "") {
        alert("组织不能为空！");
    } else if (role == null || role == "") {
        alert("角色不能为空！");
    } else {
        $.ajax({
            type: 'Post',
            url: 'userManagement.aspx',
            data: {
                name: name,
                account: account,
                region: region,
                role: role,
                op: "add"
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "添加成功") {
                    alert("添加成功");
                } else {
                    alert("添加失败");
                }
            }
        });
    }
})
//编辑用户
$(".btn-edit").click(function () {
    var account = $(this).parent().prev().prev().prev().prev().text().trim();
    var name = $(this).parent().prev().prev().prev().text().trim();
    var region = $(this).parent().prev().prev().text().trim();
    sessionStorage.setItem("region", region);
    var role = $(this).parent().prev().text().trim();
    sessionStorage.setItem("role", role);
    var regionId = $("#reginId").val();
    var roleId = $("#roleId").val();
    $("#edit-Account").val(account);
    $("#edit-Name").val(name);

    $("#editRegion").find("option[value=" + 2 + "]").attr("selected", true);
    $("#editRole").find("option[value='" + roleId + "']").attr("selected", true);
})
$("#btnEdit").click(function () {
    var account = $("#edit-Account").val();
    var region = $("#editRegion").val();
    var role = $("#editRole").val();
    var name = $("#edit-Name").val();
    if (region == "") {
        region = sessionStorage.getItem("region")
    } if (role == "") {
        role = sessionStorage.getItem("role")
    }
    $.ajax({
        type: 'Post',
        url: 'userManagement.aspx',
        data: {
            name: name,
            account: account,
            region: region,
            role: role,
            op: "edit"
        },
        dataType: 'text',
        success: function (succ) {
            if (succ == "更新成功") {
                alert("修改成功");
            } else {
                alert("修改失败");
            }
        }
    })
})
//重置密码
$("#reset").click(function () {
    var account = $("#edit-Account").val();
    $.ajax({
        type: 'Post',
        url: 'userManagement.aspx',
        data: {
            account: account,
            op: "reset"
        },
        dataType: 'text',
        success: function (succ) {
            if (succ == "更新成功") {
                alert("重置成功");
            } else {
                alert("重置失败");
            }
        }
    })
})
//删除用户
$(".btn-delete").click(function () {
    var account = $(this).parent().prev().prev().prev().prev().text().trim();
    var flag = confirm("确定要删除账号为：" + account + "的用户吗？");
    if (flag == true) {
        $.ajax({
            type: 'Post',
            url: 'userManagement.aspx',
            data: {
                account: account,
                op: "del"
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "删除成功") {
                    alert("删除成功");
                } else {
                    alert("删除失败");
                }
            }
        })
    }
})