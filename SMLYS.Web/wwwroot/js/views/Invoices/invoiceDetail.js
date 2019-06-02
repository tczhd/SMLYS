$(document).ready(function () {

    SMLYS.InvoiceDetail.Init();

});


SMLYS.InvoiceDetail = {

    Init: function () {

        var primaryInvoiceSection = $('section.primary-invoice-section');
        var familyId = primaryInvoiceSection.find('input[id*=FamilyId]').val();
        var patientId = primaryInvoiceSection.find('input[id*=PatientId]').val();

    },

    SendInvoiceEmailModal: function () {

        var dataType = 'application/json; charset=utf-8';
        var spinner = SMLYS.getSpinner();
        $('#primaryModal').modal('show');

        var modalBody = $('div.modal-body');
        modalBody.html(spinner);
        var modalContent = $('.modal-content');
        var modalTitle = modalContent.find('.modal-title');
        modalTitle.text("Send Invoice Email");
        var modalFooter = modalContent.find('.modal-footer');
        modalFooter.html('');


        var primaryInvoiceSection = $('section.primary-invoice-section');
        var invoiceIdHidden = primaryInvoiceSection.find('input[id*=InvoiceIdHidden]');

        var jsonInvoice = { invoice_id: invoiceIdHidden.val() };

        var jsonData = JSON.stringify(jsonInvoice);

        $.ajax({
            type: "POST",
            url: "/api/Invoice/PostSendInvoiceEmail",
            contentType: dataType,
            dataType: "json",
            data: jsonData,
            success: function (result) {

                modalBody.html(result.message);

            }, //End of AJAX Success function  

            failure: function (data) {
                alert(data.responseText);
            }, //End of AJAX failure function  
            error: function (data) {
                alert(data.responseText);
            } //End of AJAX error function  

        });

    },


    ShowPaymentModal: function () {

        var dataType = 'application/json; charset=utf-8';
        var spinner = SMLYS.getSpinner();
        $('#primaryModal').modal('show');

        var modalBody = $('div.modal-body');
        //modalBody.html(spinner);
        var modalContent = $('.modal-content');
        var modalTitle = modalContent.find('.modal-title');
        modalTitle.text("Apply Payment");
        var modalFooter = modalContent.find('.modal-footer');
        modalFooter.html('');

        var creditCardForm = $("#credit-card-form");

        modalBody.html(creditCardForm.html());

        var button = SMLYS.getModalFooterButton('apply-payment-btn', 'Apply Payment');
        modalFooter.append(button);
        //var primaryInvoiceSection = $('section.primary-invoice-section');
        //var invoiceIdHidden = primaryInvoiceSection.find('input[id*=InvoiceIdHidden]');

        //var jsonInvoice = { invoice_id: invoiceIdHidden.val() };

        //var jsonData = JSON.stringify(jsonInvoice);

        //$.ajax({
        //    type: "POST",
        //    url: "/api/Invoice/PostSendInvoiceEmail",
        //    contentType: dataType,
        //    dataType: "json",
        //    data: jsonData,
        //    success: function (result) {

        //        modalBody.html(result.message);

        //    }, //End of AJAX Success function  

        //    failure: function (data) {
        //        alert(data.responseText);
        //    }, //End of AJAX failure function  
        //    error: function (data) {
        //        alert(data.responseText);
        //    } //End of AJAX error function  

        //});

    }
};


