
function PopulateAddModal() {
    var spinner = "<img src='/images/loading.gif' width='30' height='30' ></img>";


    var modalBody = $('div.modal-body');
    modalBody.html(spinner);
    var modalContent = $('.modal-content');
    var modalTitle = modalContent.find('.modal-title');
    modalTitle.text("Search Patient");


    var dataType = 'application/json; charset=utf-8';
    var patientContent = $('div.view-patients-content');
    var addButton = patientContent.find("button[id=add]");
    var searchButton = patientContent.find("button[id=search]");
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

                modalBody.html("Search patients success. ");
            }
            else {
                alert(result.message);
            }
            
        }, //End of AJAX Success function  

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function  
        error: function (data) {
            alert(data.responseText);
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


