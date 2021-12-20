﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DTLoad').DataTable({
        "ajax": {
            "url": "employeetasks/getall",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "taskName", "width": "20%" },
            { "data": "startTime", "width": "20%" },
            { "data": "deadline", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/employeetasks/Upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:70px;">
                                    Edit
                                </a>
                                &nbsp;
                                <a class="btn btn-danger text-white" style="cursor:pointer; width=70px;"
                                   onclick=Delete('/employeetasks/delete?id='+${data})>
                                    Delete
                                </a>
                            </div>`
                },
                "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "No data found."
        },
        "width": "100%"
    })
}

function Delete(url) {
    swal({
        title: "Do you really want to delete employee task?",
        text: "Once deleted, forever deleted.",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    }));
}