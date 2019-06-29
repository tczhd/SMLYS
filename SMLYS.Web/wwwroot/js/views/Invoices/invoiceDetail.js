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

  
        var spinner = SMLYS.getSpinner();
        $('#primaryModal').modal('show');

        var modalBody = $('div.modal-body');

        var modalContent = $('.modal-content');
        var modalTitle = modalContent.find('.modal-title');
        modalTitle.text("Apply Payment");
        var modalFooter = modalContent.find('.modal-footer');
        modalFooter.html('');

        var button = SMLYS.getModalFooterButton('apply-payment-btn', 'Apply Payment');
        modalFooter.append(button);

        var primaryInvoiceSection = $('section.primary-invoice-section');
        var invoiceIdHidden = primaryInvoiceSection.find('input[id*=InvoiceIdHidden]');

        var primaryInvoiceTotalSection = $('section.invoice-total');
        var invoiceTotal = primaryInvoiceTotalSection.find('td.invoice-total');
        var iinvoiceAmountPaid = primaryInvoiceTotalSection.find('td.invoice-amount-paid');
        var invoiceBalance = primaryInvoiceTotalSection.find('td.invoice-balance');

        var creditCardForm = $("#credit-card-form");
        var cardContent = creditCardForm.clone();

        modalBody.html(cardContent.html());

        var paymentTypeId = 1;
        var cardHeader = modalBody.find('div.card-header');
        var radioCreditCard = cardHeader.find('input[id*=inline-radio-credit-card]');
        var radioCheck = cardHeader.find('input[id*=inline-radio-check]');
        if (radioCreditCard.is(":checked")) {
            paymentTypeId = 3;
        }
        else if (radioCheck.is(":checked")) {
            paymentTypeId = 2;
        }

        var inputPaymentAmount = modalBody.find('input[id*=PaymentAmount]');
        inputPaymentAmount.val(invoiceBalance.text());
        var creditCardInfo = modalBody.find('section.credit-card-info');

        var cardHolderName = creditCardInfo.find('input.card-holder-name');
        var cardNumber = creditCardInfo.find('input.credit-card-number');
        var cardMonth = creditCardInfo.find('select.credit-card-month');
        var cardYear = creditCardInfo.find('select.credit-card-year');
        var cardCvv = creditCardInfo.find('input.credit-card-cvv');
        var cardZip = creditCardInfo.find('input.credit-card-zip');
        var cardAddress = creditCardInfo.find('input.credit-card-adress');

        $(button).click(function () {
            if (isNaN(invoiceBalance.text())) {
                alert("Please input valid payment amount. ");
                return;
            }

            var paidAmount = parseFloat(invoiceBalance.text());

            var paymentDetail = {
                payment_type_id: paymentTypeId,
                invoice_id: invoiceIdHidden.val(),
                amount_paid: paidAmount
            };

            paymentDetail.credit_card = {
                card_holder_name: cardHolderName.val(),
                card_number: cardNumber.val(),
                card_month: parseInt( cardMonth.val()),
                card_year: parseInt( cardYear.val()),
                card_cvv: cardCvv.val(),
                card_zip: cardZip.val(),
                card_address: cardAddress.val()
            };

            var jsonData = JSON.stringify(paymentDetail);

            var paymentButton = button.remove();
            modalFooter.html('');

            modalFooter.append(spinner);
            var dataType = 'application/json; charset=utf-8';

            $.ajax({
                type: "POST",
                url: "/api/Invoice/PostApplyPayment",
                contentType: dataType,
                dataType: "json",
                data: jsonData,
                success: function (result) {
                    modalFooter.html('');
                    modalBody.html(result.message);

                }, //End of AJAX Success function  

                failure: function (data) {
                    alert(data.responseText);
                    modalFooter.html('');
                    modalFooter.append(paymentButton);
                }, //End of AJAX failure function  
                error: function (data) {
                    alert(data.responseText);
                    modalFooter.html('');
                    modalFooter.append(paymentButton);
                } //End of AJAX error function  

            });

        });

    }
};


