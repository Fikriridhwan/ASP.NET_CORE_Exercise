var table = null;
var arrLeaveVal = [];
var arrHuman = [];

$(document).ready(function () {
    debugger;
    table = $('#myTable').DataTable({
        "processing": true,
        "ajax": {
            url: "/LeaveReports/LoadLeaveReports",
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
            { "data": "applicationEntry" },
            { "data": "leaveValidationId" },
            { "data": "action" },
            { "data": "durationLV" },
            { "data": "humanResourceId" },
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
    $('#ApplicationEntry').val('');
    $('#HROption').val('');
    $('#LeaveValidationOption').val('');
    $('#Update').hide();
    $('#Save').show();

};

//==============Human Resource Dropdown================
function LoadHumanRes(element) {
    //debugger;
    if (arrHuman.length === 0) {
        $.ajax({
            type: "Get",
            url: "/HumanResources/LoadHumanResources",
            success: function (data) {
                arrHuman = data;
                renderHumanRes(element);
            }
        });
    }
    else {
        renderHumanRes(element);
    }
}

function renderHumanRes(element) {
    //debugger;
    var $option = $(element);
    $option.empty();
    $option.append($('<option/>').val('0').text('Select Human Resource ').hide());
    $.each(arrHuman, function (i, val) {
        $option.append($('<option/>').val(val.id).text(val.name));
    });
}
//==============Human Resource Dropdown end================

//==============Leave Validation Dropdown================
function LoadLeaveVal(element) {
    //debugger;
    if (arrLeaveVal.length === 0) {
        $.ajax({
            type: "Get",
            url: "/LeaveValidations/LoadLeaveValidations",
            success: function (data) {
                arrLeaveVal = data;
                renderLeaveVal(element);
            }
        });
    }
    else {
        renderLeaveVal(element);
    }
}

function renderLeaveVal(element) {
    //debugger;
    var $option = $(element);
    $option.empty();
    $option.append($('<option/>').val('0').text('Select Leave Validation ').hide());
    $.each(arrLeaveVal, function (i, val) {
        $option.append($('<option/>').val(val.id).text(val.id));
    });
}
//==============Leave Validation Dropdown End ================

LoadHumanRes($('#HROption'));
LoadLeaveVal($('#LeaveValidationOption'));


function GetById(id) {
    debugger;
    $.ajax({
        url: "/LeaveReports/GetById/",
        data: { id: id }
    }).then((result) => {
        debugger;

        $('#Id').val(result.id);
        $('#ApplicationEntry').val(result.applicationEntry);
        $('#HROption').val(result.humanResourceId);
        $('#LeaveValidationOption').val(result.leaveValidationId);
        $('#Update').show();
        $('#Save').hide();
        $('#myModal').modal('show')

    })
};

function Save() {
    debugger;
    var LeaveRep = new Object();
    LeaveRep.applicationEntry = $('#ApplicationEntry').val();
    LeaveRep.humanResourceId = $('#HROption').val();
    LeaveRep.leaveValidationId = $('#LeaveValidationOption').val();


    $.ajax({
        type: 'POST',
        url: '/LeaveReports/InsertUpdate/',
        data: LeaveRep
    }).then((result) => {
        debugger;
        if (result.statusCode == 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Leave Report Added Successfully'
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
    var LeaveRep = new Object();
    LeaveRep.id = $('#Id').val();
    LeaveRep.applicationEntry = $('#ApplicationEntry').val();
    LeaveRep.humanResourceId = $('#HROption').val();
    LeaveRep.leaveValidationId = $('#LeaveValidationOption').val();

    $.ajax({
        type: 'POST',
        url: '/LeaveReports/InsertUpdate/',
        data: LeaveRep
    }).then((result) => {
        debugger;
        if (result.statusCode == 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Leave Report Updated Successfully'
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
                url: '/LeaveReports/Delete/',
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
