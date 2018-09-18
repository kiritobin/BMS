//存储当前页数
var page = $("#page").val();
sessionStorage.setItem("page", page);
//存储总页数
var countPage = $("#countPage").val();
sessionStorage.setItem("countPage", countPage);

//点击翻页按钮
$(".jump").click(function () {
    switch ($(this).text().trim()) {
        //点击上一页按钮时
        case ('上一页'):
            if (parseInt(sessionStorage.getItem("page")) > 1) {
                jump(parseInt(sessionStorage.getItem("page")) - 1);
                sessionStorage.setItem("page", parseInt(sessionStorage.getItem("page")) - 1);
                break;
            } else {
                jump(1);
                break;
            }
        //点击下一页按钮时
        case ('下一页'):
            if (parseInt(sessionStorage.getItem("page")) < parseInt(sessionStorage.getItem("countPage"))) {
                jump(parseInt(sessionStorage.getItem("page")) + 1);
                sessionStorage.setItem("page", parseInt(sessionStorage.getItem("page")) + 1);
                break;
            } else {
                jump(parseInt(sessionStorage.getItem("countPage")));
                break;
            }
        //点击首页按钮时
        case ('首页'):
            jump(1);
            break;
        //点击尾页按钮时
        case ('尾页'):
            jump(parseInt(sessionStorage.getItem("countPage")));
            break;
    }
});
//翻页时获取当前页数
function jump(cur) {
    var strWhere = sessionStorage.getItem("strWhere");
    var region = sessionStorage.getItem("region");
    var role = sessionStorage.getItem("role");
    var type = sessionStorage.getItem("type");
    if (strWhere != null && strWhere != "") {
        window.location.href = "userManagement.aspx?currentPage=" + cur + "&search=" + strWhere + "&type=" + type;
    } else if (region == null && (role != null || role != "")) {
        window.location.href = "userManagement.aspx?currentPage=" + cur + "&role=" + role + "&type=" + type;
    } else if (role == null && (region != null || region != "")) {
        window.location.href = "userManagement.aspx?currentPage=" + cur + "&region=" + region + "&type=" + type;
    } else if ((role != null || role != "") && (region != null || region != "")) {
        window.location.href = "userManagement.aspx?currentPage=" + cur + "&region=" + region + "&role=" + role + "&type=" + type;
    } else {
        window.location.href = "userManagement.aspx?currentPage=" + cur
    }
}
//点击查询按钮时
$("#btn-search").click(function () {
    var strWhere = $("#input-search").val();
    sessionStorage.setItem("strWhere", strWhere);
    sessionStorage.setItem("type", "search");
    jump(1);
});
//地区下拉框改变事件
$("#select-region").change(function () {
    var regionId = $("#select-region").val();
    if (regionId == 0) {
        sessionStorage.removeItem("region");
        sessionStorage.removeItem("type");
    } else {
        sessionStorage.setItem("region", regionId);
    }
    var roleId = $("#select-role").val();
    if (roleId == 0) {
        sessionStorage.setItem("type", "region");
        jump(1);
    } else {
        sessionStorage.setItem("role", roleId);
        sessionStorage.setItem("type", "all");
        jump(1);
    }
})
//角色下拉框改变事件
$("#select-role").change(function () {
    var roleId = $("#select-role").val();
    if (roleId == 0) {
        sessionStorage.removeItem("role");
        sessionStorage.removeItem("type");
    } else {
        sessionStorage.setItem("role", roleId);
    }
    var regionId = $("#select-region").val();
    if (regionId == 0) {
        sessionStorage.setItem("type", "role");
        jump(1);
    } else {
        sessionStorage.setItem("region", regionId);
        sessionStorage.setItem("type", "all");
        jump(1);
    }
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
    var region = $("#reginId").val();
    var role = $("#roleId").val();
    $("#edit-Account").val(account);
    $("#edit-Name").val(name);
    $.ajax({
        type: 'Post',
        url: 'userManagement.aspx',
        data: {
            region: region,
            role: role,
            op: "editData"
        },
        dataType: 'text',
        success: function (succ) {
            if (succ == "展示") {
                $("#myModa2").click(function () {
                    $("#new").modal("show")
                });
            }
        }
    })
})
$("#btnEdit").click(function () {
    var account = $("#edit-Account").val();
    var region = $("#editRegion").val();
    var role = $("#editRole").val();
    var name = $("#edit-Name").val();
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