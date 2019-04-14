//var SMLYS = {};
$(document).ready(function () {

    SMLYS.Invoice.Init();

    $('#add_item').click(function () {

        var invoiceDetailSection = $('section.invoice-detail-section');
        var invoiceItemsTbody = invoiceDetailSection.find('tbody.invoice-items');
        var itemRow = SMLYS.Invoice.GetInvoiceRow();

        invoiceItemsTbody.append($(itemRow));
    });
});



SMLYS.Invoice = {
    Init: function () {
        var dataType = 'application/json; charset=utf-8';
        var primaryInvoiceSection = $('section.primary-invoice-section');
        var familyId = primaryInvoiceSection.find('input[id*=FamilyId]').val();
        var patientId = primaryInvoiceSection.find('input[id*=PatientId]').val();
        var addressSestion = primaryInvoiceSection.find('div.address-sestion');
        var patientAddresses = addressSestion.find('div.patient-address');
        patientAddresses.each(function () {
            var adderssPatient = $(this);
            var addressPatientId = adderssPatient.find('input[id*=AddressPatientId]').val();
            if (addressPatientId === patientId) {
                adderssPatient.removeClass('d-none');
                return false;
            }
        });

        $.ajax({
            type: "GET",
            url: "/api/Invoice/GetInitData/" + familyId,
            contentType: dataType,
            dataType: "json",
            success: function (result) {
                alert('OK');

            }, //End of AJAX Success function  

            failure: function (data) {
                alert(data.responseText);
            }, //End of AJAX failure function  
            error: function (data) {
                alert(data.responseText);
            } //End of AJAX error function  

        });
    },

    GetInvoiceRow: function () {

        var tr = "<tr>" +
            "<td>Extended Coverage, 1 Year subscription</td> " +
            "<td>Ipso Leurm facto desniata eu</td> " +
            "<td>1</td> " +
            "<td>200.00</td> " +
            "<td>GST, PST</td >" +
            "<td>200.00</td >" +
            "</tr>";

        return tr;
    },

    AddInvoiceModal: function () {

        var isvalid = $("#myForm").valid();  // Tells whether the form is valid

        if (isvalid) {
            var spinner = SMLYS.getSpinner();
            $('#primaryModal').modal('show');
       

            var dataType = 'application/json; charset=utf-8';
            var modalBody = $('div.modal-body');
            modalBody.html(spinner);
            var modalContent = $('.modal-content');
            var modalTitle = modalContent.find('.modal-title');
            modalTitle.text("New Invoice");
            var modalFooter = modalContent.find('.modal-footer');
            modalFooter.html('');

            var button = SMLYS.getModalFooterButton('create-invoice-btn', 'Create Invoice');

            var jsonPatients = [];

            var patients = $('div.patients');
            var sectionPatients = patients.find('section.patient');
            sectionPatients.each(function () {

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

                if ($(patient).hasClass("Additional-patient-section") === true) {
                    var checkBoxAddress = patient.find('input[name="AddressCheckBox"]');
                    if (checkBoxAddress && checkBoxAddress.is(":checked")) {
                        newPatient.address1 = jsonPatients[0].address1;
                        newPatient.address = jsonPatients[0].address;
                        newPatient.company = jsonPatients[0].company;
                        newPatient.country_id = jsonPatients[0].country_id;
                        newPatient.state_id = jsonPatients[0].state_id;
                        newPatient.postal_code = jsonPatients[0].postal_code;
                        newPatient.phone = jsonPatients[0].phone;
                    }
                }

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
                    $(button).click(function () {
                        window.location.href = '/Invoice/InvoiceForm?patientId=' + result.patient_id;
                    });
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
    }
};





