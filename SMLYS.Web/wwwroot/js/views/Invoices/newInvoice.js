//var SMLYS = {};
$(document).ready(function () {

    SMLYS.Invoice.Init();

});



SMLYS.Invoice = {
    Items: [],
    Taxes: [],

    Init: function () {

        var primaryInvoiceSection = $('section.primary-invoice-section');
        var familyId = primaryInvoiceSection.find('input[id*=FamilyId]').val();
        var patientId = primaryInvoiceSection.find('input[id*=PatientId]').val();

        $('#add_item').click(function () {

            var invoiceDetailSection = $('section.invoice-detail-section');
            var invoiceItemsTbody = invoiceDetailSection.find('tbody.invoice-items');
            var itemRow = $(SMLYS.Invoice.GetInvoiceRow());

            var selectItemList = itemRow.find('select[id*=selectItemList]');
            $(selectItemList).change(function () {

                SMLYS.Invoice.UpdateItemRow(selectItemList, itemRow);
            });

            invoiceItemsTbody.append(itemRow);

            var faClose = itemRow.find('i.fa-close');

            $(faClose).click(function () {
                itemRow.remove();
            });
        });

        SMLYS.Invoice.InitData(familyId);
        SMLYS.Invoice.InitSendToCloseIcon();
        SMLYS.Invoice.ChangePatient(patientId);
    },

    UpdateItemRow: function (selectItemList, itemRow) {
        var itemId = $(selectItemList).children("option:selected").val();
        var quantityText = itemRow.find('input.item-quantity').val();
        var costTd = itemRow.find('td.item-cost');
        var itemSubtotalTd = itemRow.find('td.item-subtotal');

        var taxRateTotal = 0;
        $.each(SMLYS.Invoice.Taxes, function (key, value) {
            taxRateTotal += value.taxRate;
        });

        $.each(SMLYS.Invoice.Items, function (key, value) {
            if (value.itemId === parseInt(itemId)) {
                costTd.text(value.cost);
                itemSubtotalTd.text(value.cost);

                if (!isNaN(quantityText)) {
                    var quantity = parseInt(quantityText);
                    var subtotal = quantity * value.cost * (1 + taxRateTotal);
                    itemSubtotalTd.text(subtotal);
                }

                return false;
            }
        });
    },

    UpdateTotal: function (selectItemList, itemRow) {

        var invoiceDetailSection = $('section.invoice-detail-section');
        var invoiceTotalSection = $('section.invoice-total"');

        //var itemId = $(selectItemList).children("option:selected").val();
        //var quantityText = itemRow.find('input.item-quantity').val();
        //var costTd = itemRow.find('td.item-cost');
        //var itemSubtotalTd = itemRow.find('td.item-subtotal');

        //var taxRateTotal = 0;
        //$.each(SMLYS.Invoice.Taxes, function (key, value) {
        //    taxRateTotal += value.taxRate;
        //});

        //$.each(SMLYS.Invoice.Items, function (key, value) {
        //    if (value.itemId === parseInt(itemId)) {
        //        costTd.text(value.cost);
        //        itemSubtotalTd.text(value.cost);

        //        if (!isNaN(quantityText)) {
        //            var quantity = parseInt(quantityText);
        //            var subtotal = quantity * value.cost * (1 + taxRateTotal);
        //            itemSubtotalTd.text(subtotal);
        //        }

        //        return false;
        //    }
        //});
    },

    InitData: function(familyId) {
        var dataType = 'application/json; charset=utf-8';
        $.ajax({
            type: "GET",
            url: "/api/Invoice/GetInitData/" + familyId,
            contentType: dataType,
            dataType: "json",
            success: function (result) {
                SMLYS.Invoice.Items = result.items;
                SMLYS.Invoice.Taxes = result.taxes;
            }, //End of AJAX Success function  
            failure: function (data) {
                alert(data.responseText);
            }, //End of AJAX failure function  
            error: function (data) {
                alert(data.responseText);
            } //End of AJAX error function  
        });
    },

    InitSendToCloseIcon: function () {
        var primaryInvoiceSection = $('section.primary-invoice-section');
        var sendTos = primaryInvoiceSection.find('div.send-to-name');
        sendTos.each(function () {
            var sendTo = $(this);
            var faClose = sendTo.find('i.fa-close');

            $(faClose).click(function () {
                sendTo.remove();
            });
        });
    },

    GetItemList: function() {
        var selectList = "<select id='selectItemList' name='selectItemList' class='form-control'>";

        $.each(SMLYS.Invoice.Items, function (key, value) {
            selectList += "<option value='" + value.itemId + "'>" + value.itemName + "</option>";
        });

        selectList += "</select>";

        return selectList;
    },

    ChangePatient: function (patientId) {
        var primaryInvoiceSection = $('section.primary-invoice-section');
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

    },

    GetInvoiceRow: function () {

        var inputQuantity = "<div class='col-xs-2'><input type='text' class='form-control input-sm item-quantity' value='1' size='2' ></div>";
        var firstItem = SMLYS.Invoice.Items[0];
        var taxDisplay = "<div class='tax-list'>";

        $.each(SMLYS.Invoice.Taxes, function (key, value) {
            taxDisplay += "<div>" + value.taxName + "</div>";
        });

        taxDisplay += "</div>";

        var tr = "<tr class='invoice-item'>" +
            "<td>" + SMLYS.Invoice.GetItemList() + "</td> " +
            "<td class='description'>" + firstItem.description +"</td> " +
            "<td class='item-quantity'>" + inputQuantity + "</td> " +
            "<td class='item-cost'>" + firstItem.cost + "</td> " +
            "<td>" + taxDisplay +"</td >" +
            "<td class='item-subtotal'>" + firstItem.cost + "</td >" +
            "<td class='item-remove'><i class='fa fa-close fa-lg float-right' ></i></td >" +
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





