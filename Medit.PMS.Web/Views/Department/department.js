var selectedId = '';
$(function () {
    $('#btnAddRoot').click(function () { add(0); });
    $('#btnAdd').click(function () { add(1); });
    $('#btnSave').click(function () { save(1); });
    $('#btnDelete').click(function () { deleteMulti(1); });
    $('#btnLoadRoot').click(function () {
        selectedId = '';
        loadTables(1, 10);
    });
    $('#checkAll').click(function () { checkAll(this); });

    initTree();
});

// 加载功能树
function initTree() {
    $.jstree.destroy();
    $.ajax({
        type: "get",
        url: "/Department/GetTreeData?_t=" + new Date().getTime(),    //获取数据的ajax请求地址
        success: function (data) {
            $('#treeDiv').jstree({       //创建JsTtree
                'core': {
                    'data': data,        //绑定JsTree数据
                    "multiple": false    //是否多选
                },
                "plugins": ["state", "types", "wholerow"]  //配置信息
            })
            $("#treeDiv").on("ready.jstree", function (e, data) {   //树创建完成事件
                data.instance.open_all();    //展开所有节点
            });
            $("#treeDiv").on('changed.jstree', function (e, data) {   //选中节点改变事件
                var node = data.instance.get_node(data.selected[0]);  //获取选中的节点
                if (node!=null) {
                    selectedId = node.id;
                    loadTables(1, 10);
                };
            });
        }
    });
}


// 加载功能列表数据
function loadTables(page, size) {
    $("#tableBody").html("");
    $("#checkAll").prop("checked", false);
    $.ajax({
        type: "get",
        url: "/Department/GetChildrenByParent?page=" + page + "&size=" + size + "&parentId=" + selectedId + "&_t=" + new Date().getTime(),
        success: function (data) {
            $.each(data.rows, function (i, item) {
                var tr = "<tr>";
                tr += "<td align='center'><input type='checkbox' class='checkboxs' value='" + item.id + "'/></td>";
                tr += "<td>" + item.name + "</td>";
                tr += "<td>" + (item.code == null ? "" : item.code) + "</td>";
                tr += "<td>" + (item.manager == null ? "" : item.manager) + "</td>";
                tr += "<td>" + (item.contactNumber == null ? "" : item.contactNumber) + "</td>";
                tr += "<td>" + (item.remarks == null ? "" : item.remarks) + "</td>";
                tr += "<td><button class='btn btn-info btn-xs' href='javascript:;' onclick='edit(\"" + item.id + "\")'><i class='fa fa-edit'></i> 编辑 </button> <button class='btn btn-danger btn-xs' href='javascript:;' onclick='deleteSingle(\"" + item.id + "\")'><i class='fa fa-trash-o'></i> 删除 </button> </td>"
                tr += "</tr>";
                $("#tableBody").append(tr);
            })
            var elment = $("#grid_paging_part"); //分页插件的容器id
            if (data.rowCount > 0) {
                var options = { //分页插件配置项
                    bootstrapMajorVersion: 3,
                    currentPage: page, //当前页
                    numberOfPages: data.RowCount, //总数
                    totalPages: data.pageCount, //总页数
                    onPageChanged: function (event, oldPage, newPage) { //页面切换事件
                        loadTables(newPage, size);
                    }
                }
                elment.bootstrapPaginator(options); //分页插件初始化
            }
        }
    })
}

// 全选
function checkAll(obj) {
    $('.checkboxs').each(function () {
        if (obj.checked == true) {
            $(this).prop('checked', true);
        }
        if (obj.checked == false) {
            $(this).prop('checked', false);
        }
    });
}

// 新增
function add(type) {
    if (type == 1) {
        if (selectedId == '') {
            layer.alert('请选择部门。');
            return;
        }
        $('#parentId').val(selectedId);
    } else {
        $('#parentId').val('');
    }
    $('#id').val('');
    $('#code').val('');
    $('#name').val('');
    $('#manager').val('');
    $('#contractNumber').val('');
    $('#remarks').val('');
    $('#title').val('新增顶级');

    // 弹出新增窗体
    $('#addRootModal').modal('show');
}

// 编辑
function edit(id) {
    $.ajax({
        type: 'get',
        url: '/Department/Get?id=' + id + '&_t=' + new Date().getTime(),
        success: function (data) {
            $('#id').val(data.id);
            $('#parentId').val(data.parentId);
            $('#code').val(data.code);
            $('#name').val(data.name);
            $('#manager').val(data.manager);
            $('#contractNumber').val(data.contractNumber);
            $('#remarks').val(data.remarks);

            $('#title').val('编辑功能');
            $('#addRootModal').modal('show');            
        }
    })
}

// 保存
function save() {
    var postData = {
        dto: {
            Id: $('#id').val(),
            ParentId: $('#parentId').val(),
            Name: $('#name').val(),
            Code: $('#code').val(),
            Manager: $('#manager').val(),
            ContractNumber: $('#contractNumber').val(),
            Remarks: $('#remarks').val()
        }
    };

    $.ajax({
        type: 'post',
        url: '/Department/Edit',
        data: postData,
        success: function (data) {
            debugger
            if (data.result = 'Success') {
                initTree();
                $('#addRootModal').modal('hide');
            } else {
                layer.tips(data.message, '#btnSave');
            }
        }
    });
}

// 批量删除
function deleteMulti() {
    var ids = "";
    $(".checkboxs").each(function () {
        if ($(this).prop("checked") == true) {
            ids += $(this).val() + ","
        }
    });
    ids = ids.substring(0, ids.length - 1);
    if (ids.length == 0) {
        layer.alert("请选择要删除的记！");
        return;
    };
    //询问框
    layer.confirm("您确认删除选定的记录吗？", {
        btn: ["确定", "取消"]
    }, function () {
        var sendData = { "ids": ids };
        $.ajax({
            type: "post",
            url: "/Department/DeleteMulti",
            data: sendData,
            success: function (data) {
                if (data.result == "Success") {
                    initTree();
                    layer.closeAll();
                }
                else {
                    layer.alert("删除失败！");
                }
            }
        });
    });
};

// 删除单条数据
function deleteSingle(id) {
    layer.confirm("您确认删除选定的记录吗？", {
        btn: ["确定", "取消"]
    }, function () {
        $.ajax({
            type: "post",
            url: "/Department/Delete",
            data: { "id": id },
            success: function (data) {
                if (data.result == "Success") {
                    initTree();
                    layer.closeAll();
                }
                else {
                    layer.alert("删除失败！");
                }
            }
        })
    });
};