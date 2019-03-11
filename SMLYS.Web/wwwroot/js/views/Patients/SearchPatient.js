$(document).ready(function () {

    $('#search').click(function () {
        PopulateAddModal($(this));
    });


    $('#add').click(function () {
       
    });

});


function PopulateAddModal(button) {
    //var spinner = "<img src='/images/loading.gif' width='30' height='30' ></img>";
    var spinner = $('#search-spinner');
    spinner.removeClass("invisible");
    button.hide();

    //var modalBody = $('div.modal-body');
    //modalBody.html(spinner);
    //var modalContent = $('.modal-content');
    //var modalTitle = modalContent.find('.modal-title');
    //modalTitle.text("Search Patient");

    //var footerButton = modalContent.find('button.footer-close');
    //var headerClose = modalContent.find('button.header-close');
    //footerButton.addClass("invisible");
    //headerClose.addClass("invisible");
    //$('#primaryModal').modal('show');

    var dataType = 'application/json; charset=utf-8';
    var patientContent = $('div.view-patients-content');

    var tablePatients = patientContent.find('table.table-patients');
    var bodyPatients = tablePatients.find('tbody');
    bodyPatients.empty();

    var searchPatients = [];

    var patient = {
        search_type: 'first_name',
        search_content: 'sdf'
    };

    searchPatients.push(patient);

    var jsonData = JSON.stringify(searchPatients);

    $.ajax({
        type: "POST",
        url: "/api/Patient/PostSearchPatients",
        contentType: dataType,
        dataType: "json",
        data: jsonData,
        success: function (result) {

            if (result.success) {

                $.each(result.patient_detail, function (index, patient) {
                    var tr = GetPatientRow(patient.patient_name, patient.patient_address, patient.patient_phone, patient.patient_email, patient.patient_status);
                    bodyPatients.append(tr);
                });

               // $('#primaryModal').modal('hide');
            
            }
            else {

                modalBody.html("Search patients failed. " + result.message);
                footerButton.removeClass("myClass yourClass");
                headerClose.removeClass("myClass yourClass");
            }

            spinner.addClass("invisible");
            button.show();
        }, //End of AJAX Success function  

        failure: function (data) {
            alert(data.responseText);
            spinner.addClass("invisible");
            button.show();
        }, //End of AJAX failure function  
        error: function (data) {
            alert(data.responseText);
            spinner.addClass("invisible");
            button.show();
        } //End of AJAX error function  

    });
}


function GetPatientRow(name, address, phone, email, status) {
    var tr = "<tr>" +
        "<td>" + name + "</td> " +
        "<td>" + address + "</td> " +
        "<td>" + phone + "</td> " +
        "<td>" + email + "</td> " +
        "<td><span class='badge badge-success'>" + status + "</span></td> " +
        "</tr>";

    return tr;
}


