//var SMLYS = {};
$(document).ready(function () {

    var myForm = $('#myForm');
    var patientBody = myForm.find('div.patient-body');

    var patient_base_form = $('#patient_base_form').html();
    var patient_base_form_address = $('#patient_base_form_address').html();
    var patient_base_form_action = $('#patient_base_form_action').html();

    patientBody.append($(patient_base_form).clone());
    patientBody.append($(patient_base_form_address).clone());
    patientBody.append($(patient_base_form_action));

    $('#add_family_member').click(function () {
        var cardHeader = '<div class="card-header"><strong>Add family member / Dependent</strong></div>'
        var checkBoxAddess = '<div class="card-block"><input type="checkbox" name="AddressCheckBox"  checked="checked" /> Address same as primary member  </div >';
        var cardBody = $('<div class="card-body patient-body"> </div>');
        cardBody.append($(patient_base_form).clone());
        cardBody.append($(patient_base_form_address).clone());

        $(cardHeader).insertBefore(".add-patient-buttons");
        $(checkBoxAddess).insertBefore(".add-patient-buttons");
        cardBody.insertBefore(".add-patient-buttons");
    });
});


SMLYS.Patient = {

    AddNewPatientModal: function () {

        //var isvalid = $("#myForm").valid();  // Tells whether the form is valid

        //if (isvalid) {
        //    var a = 1;
        //}
        //else {
        //    var b = 2;
        //}

        var spinner = "<img src='/images/loading.gif' width='30' height='30' ></img>";
        $('#primaryModal').modal('show');

        var dataType = 'application/json; charset=utf-8';
        var modalBody = $('div.modal-body');
        modalBody.html(spinner);
        var modalContent = $('.modal-content');
        var modalTitle = modalContent.find('.modal-title');
        modalTitle.text("New Patient");
        var modalFooter = modalContent.find('.modal-footer');

        var button = SMLYS.getModalFooterButton('create-invoice-btn', 'Create Invoice');

        var jsonPatients = [];

        var patients = $('div.patients > div.patient');

        patients.each(function () {

            var patient = $(this);
            var email = patient.find("input[id=Email]").val();
            var firstName = patient.find('input[id=FirstName]').val();
            var lastName = patient.find('input[id=LastName]').val();
            var company = patient.find('input[id=Company]').val();
            var address1 = patient.find('input[id=Address_Address1]').val();
            var address2 = patient.find('input[id=Address_Address2]').val();
            var city = patient.find('input[id=Address_City]').val();
            var countryId = patient.find('select[id=selectCountry]').val();
            var stateId = patient.find('select[id=selectState]').val();
            var postalCode = patient.find('input[id=Address_PostalCode]').val();
            var phone = patient.find('input[id=Phone]').val();

            var newPatient = {
                email: email,
                first_name: firstName,
                last_name: lastName,
                company: company,
                address1: address1,
                address2: address2,
                city: city,
                country_id: countryId,
                state_id: stateId,
                postal_code: postalCode,
                phone: phone
            };

            jsonPatients.push(newPatient);
        });

        var jsonData = JSON.stringify(jsonPatients);

        $.ajax({
            type: "POST",
            url: "/api/Patient",
            contentType: dataType,
            dataType: "json",
            data: jsonData,
            success: function (result) {

                modalBody.html("Add new patient success. ");
                modalFooter.append(button);

            }, //End of AJAX Success function  

            failure: function (data) {
                alert(data.responseText);
            }, //End of AJAX failure function  
            error: function (data) {
                alert(data.responseText);
            } //End of AJAX error function  

        });
    }

};





