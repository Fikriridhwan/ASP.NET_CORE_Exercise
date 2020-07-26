$(document).ready(function () {
    //debugger;

    table = $('#myTable').DataTable({
        "processing": true,
        "ajax": {
            url: "/HumanResources/LoadHumanResources",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        "columns": [
            {
                "data": null, "sortable": true,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                },
            },
            { "data": "id" },
            { "data": "name" },
            {
                "render": function (data, type, row) {
                    return '<button class ="btn btn-warning fa fa-pencil-square-o" data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + row.id + ')"></button>'
                        + '&nbsp;'
                        + '<button class ="btn btn-danger fa fa-trash-o" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="return Delete(' + row.id + ')"></button>'
                }
            }
        ],
        "searching": true,
        "paging": true,
        "order": [[0, "asc"]],
        "ordering": true,
        "columnDefs": [{
            "targets": [3], //check action target disabled
            "orderable": false
        }]
    });
});


function ClearScreen() {
    $('#Id').val('');
    $('#Name').val('');
    $('#Update').hide();
    $('#Save').show();

};

function GetById(id) {
    //debugger;
    $.ajax({
        url: "/HumanResources/GetById/",
        data: { id: id }
    }).then((result) => {
        //debugger;

        $('#Id').val(result.id);
        $('#Name').val(result.name);
        $('#Update').show();
        $('#Save').hide();
        $('#myModal').modal('show')

    })
};

function Save() {
    //debugger;
    var HumanRes = new Object();
    HumanRes.name = $('#Name').val();

    $.ajax({
        type: 'POST',
        url: '/HumanResources/InsertUpdate/',
        data: HumanRes
    }).then((result) => {
        //debugger;
        if (result.statusCode == 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Employee Added Successfully'
            });
        } else {
            Swal.fire('Eror', 'Failed to input', 'error');
            ClearScreen();
        }
    })
    location = location.href
}

function Update() {
    //debugger;
    var HumanRes = new Object();
    HumanRes.id = $('#Id').val();
    HumanRes.name = $('#Name').val();

    $.ajax({
        type: 'POST',
        url: '/HumanResources/InsertUpdate/',
        data: HumanRes
    }).then((result) => {
        //debugger;
        if (result.statusCode == 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Employee Updated Successfully'
            });
        } else {
            Swal.fire('Eror', 'Failed to input', 'error');
            ClearScreen();
        }
    })
    location = location.href
}

function Delete(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            //debugger;
            $.ajax({
                url: '/HumanResources/Delete/',
                data: { id: id }
            }).then((result) => {
                //debugger;
                if (result.statusCode == 200) {
                    Swal.fire({
                        position: 'center',
                        type: 'success',
                        title: 'Delete Successfully'
                    });
                } else {
                    Swal.fire('Eror', 'Failed to Delete', 'error');
                    ClearScreen();
                }
            })
        };
        location = location.href
    });

}