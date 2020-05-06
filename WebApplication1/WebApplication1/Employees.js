"use strict"
$(document).ready(function () {
    var ulEmployees = $('#ulEmployees');

    $('#Btn').click(function () {
        $.ajax({
            type: "GET",
            url: 'api/Employees',
            datatype: 'json',
            success: function (data) {
                ulEmployees.empty();
                $.each(data, function (index, val) {
                    var fullName = val.firstname + ' ' + val.lastname;
                    $('#ulEmployees').append('<li>' + fullname + '</li>')
                });
            }
        })
    });

    $('#BtnCLear').click(function () {
        ulEmployees.empty();
    })
});