//var SMLYS = {};

SMLYS.Patient = {

    AddNewPatientModal: function () {
        var spinner = "<img src='/images/loading.gif' width='30' height='30' ></img>";

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





