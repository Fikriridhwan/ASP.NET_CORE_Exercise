var table = null;
var arrLeaveApp = [];

$(document).ready(function () {
    debugger;
    table = $('#myTable').DataTable({
        "processing": true,
        "ajax": {
            url: "/LeaveApplications/LoadLeaveApplications",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        "columns": [
            {
                "data": null, "sortable": false,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "employeeId" },
            { "data": "employeeName" },
            { "data": "nip" },
            { "data": "reason" },
            { "data": "leaveDuration" },
            {
                "render": function (data, type, row) {
                    return '<button class ="btn btn-warning fa fa-pencil-square-o" data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + row.id + ')"></button>'
                        + '&nbsp;'
                        + '<button class ="btn btn-danger fa fa-trash" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="return Delete(' + row.id + ')"></button>'
                }
            }
        ],
        "searching": true,
        "paging": true,
        "order": [[0, "asc"]],
        "ordering": true,
        "columnDefs": [{
            "targets": [6], //disabled sorting action
            "orderable": false
        }]
    });
});

function ClearScreen() {
    $('#Id').val('');    
    $('#EmployeeOption').val(''); 
    $('#Reason').val('');
    $('#LeaveDuration').val('');
    $('#Update').hide();
    $('#Save').show();

};

function LoadEmployee(element) {
    //debugger;
    if (arrLeaveApp.length === 0) {
        $.ajax({
            type: "Get",
            url: "/Employees/LoadEmployees",
            success: function (data) {
                arrLeaveApp = data;
                renderEmployee(element);
                document.getElementById("LeaveDuration").max = data.annualLeaveRemaining
            }
        });
    }
    else {
        renderEmployee(element);
    }
}

function renderEmployee(element) {
    //debugger;
    var $option = $(element);
    $option.empty();
    $option.append($('<option/>').val('0').text('Select Employee').hide());
    $.each(arrLeaveApp, function (i, val) {
        $option.append($('<option/>').val(val.id).text(val.name));
    });
}
LoadEmployee($('#EmployeeOption'));

function GetById(id) {
    debugger;
    $.ajax({
        url: "/LeaveApplications/GetById/",
        data: { id: id }
    }).then((result) => {
        debugger;

        $('#Id').val(result.id);
        $('#EmployeeOption').val(result.employeeId);
        $('#Reason').val(result.reason);
        $('#LeaveDuration').val(result.leaveDuration);
        $('#Update').show();
        $('#Save').hide();
        $('#myModal').modal('show')

    })
};

function Save() {
    debugger;
    var LeaveApp = new Object();
    LeaveApp.employeeId = $('#EmployeeOption').val();
    LeaveApp.reason = $('#Reason').val();
    LeaveApp.leaveDuration = $('#LeaveDuration').val();
    $.ajax({
        type: 'POST',
        url: '/LeaveApplications/InsertUpdate/',
        data: LeaveApp
    }).then((result) => {
        debugger;
        if (result.statusCode == 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Leave Application Added Successfully'
            });
        } else {
            Swal.fire('Eror', 'Failed to input', 'error');
            ClearScreen();
        }
    })
    location = location.href
}

function Update() {
    debugger;
    var LeaveApp = new Object();
    LeaveApp.id = $('#Id').val();
    LeaveApp.employeeId = $('#EmployeeOption').val();
    LeaveApp.reason = $('#Reason').val();
    LeaveApp.leaveDuration = $('#LeaveDuration').val();
    $.ajax({
        type: 'POST',
        url: '/LeaveApplications/InsertUpdate/',
        data: LeaveApp
    }).then((result) => {
        debugger;
        if (result.statusCode == 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Leave Application Updated Successfully'
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
            debugger;
            $.ajax({
                url: '/LeaveApplications/Delete/',
                data: { id: id }
            }).then((result) => {
                debugger;
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