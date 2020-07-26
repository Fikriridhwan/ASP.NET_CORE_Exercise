var table = null;
var arrLeaveApp = [];
var arrManager = [];

$(document).ready(function () {
    debugger;
    table = $('#myTable').DataTable({
        "processing": true,
        "ajax": {
            url: "/LeaveValidations/LoadLeaveValidations",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        "columns": [
            {
                "data": null, "sortable": true,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "name" },
            { "data": "nip" },
            { "data": "reason" },
            { "data": "duration" },
            { "data": "managerId" },
            { "data": "validDuration" },
            { "data": "action" },
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
            "targets": [8], //disabled sorting action
            "orderable": false
        }]
    });
});


function ClearScreen() {
    $('#Id').val('');
    $('#ManagerOption').val('');
    $('#LeaveApplicationOption').val('');
    $('#Action').val('');
    $('#ValidDuration').val('');
    $('#Update').hide();
    $('#Save').show();

};

//==============Manager Dropdown================
function LoadManager(element) {
    //debugger;
    if (arrManager.length === 0) {
        $.ajax({
            type: "Get",
            url: "/Managers/LoadManagers",
            success: function (data) {
                arrManager = data;
                renderManager(element);
            }
        });
    }
    else {
        renderManager(element);
    }
}

function renderManager(element) {
    //debugger;
    var $option = $(element);
    $option.empty();
    $option.append($('<option/>').val('0').text('Select Manager ').hide());
    $.each(arrManager, function (i, val) {
        $option.append($('<option/>').val(val.id).text(val.name));
    });
}
//==============Manager Dropdown end================

//==============Leave Application Dropdown================
function LoadLeaveApp(element) {
    //debugger;
    if (arrLeaveApp.length === 0) {
        $.ajax({
            type: "Get",
            url: "/LeaveApplications/LoadLeaveApplications",
            success: function (data) {
                arrLeaveApp = data;
                renderLeaveApp(element);
            }
        });
    }
    else {
        renderLeaveApp(element);
    }
}

function renderLeaveApp(element) {
    //debugger;
    var $option = $(element);
    $option.empty();
    $option.append($('<option/>').val('0').text('Select Leave Application ').hide());
    $.each(arrLeaveApp, function (i, val) {
        $option.append($('<option/>').val(val.id).text(val.employeeName));
    });
}
//==============Leave Application Dropdown End ================

LoadManager($('#ManagerOption'));
LoadLeaveApp($('#LeaveApplicationOption'));


function GetById(id) {
    debugger;
    $.ajax({
        url: "/LeaveValidations/GetById/",
        data: { id: id }
    }).then((result) => {
        debugger;
       
        $('#Id').val(result.id);
        $('#ManagerOption').val(result.managerId);
        $('#LeaveApplicationOption').val(result.leaveApplicationId);
        $('#Action').val(result.action);
        $('#ValidDuration').val(result.validDuration);
        $('#Update').show();
        $('#Save').hide();
        $('#myModal').modal('show')

    })
};

function Save() {
    debugger;
    var LeaveVal = new Object();
    LeaveVal.managerId = $('#ManagerOption').val();
    LeaveVal.leaveApplicationId = $('#LeaveApplicationOption').val();
    LeaveVal.action = $('#Action').val();
    LeaveVal.validDuration = $('#ValidDuration').val();

    $.ajax({
        type: 'POST',
        url: '/LeaveValidations/InsertUpdate/',
        data: LeaveVal
    }).then((result) => {
        debugger;
        if (result.statusCode == 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Leave Validation Added Successfully'
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
    var LeaveVal = new Object();
    LeaveVal.id = $('#Id').val();
    LeaveVal.managerId = $('#ManagerOption').val();
    LeaveVal.leaveApplicationId = $('#LeaveApplicationOption').val();
    LeaveVal.action = $('#Action').val();
    LeaveVal.validDuration = $('#ValidDuration').val();

    $.ajax({
        type: 'POST',
        url: '/LeaveValidations/InsertUpdate/',
        data: LeaveVal
    }).then((result) => {
        debugger;
        if (result.statusCode == 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Leave Validation Updated Successfully'
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
                url: '/LeaveValidations/Delete/',
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
