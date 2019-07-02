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
        var tdInvoiceTotal = primaryInvoiceTotalSection.find('td.invoice-total');
        var tdInvoiceAmountPaid = primaryInvoiceTotalSection.find('td.invoice-amount-paid');
        var tdInvoiceBalance = primaryInvoiceTotalSection.find('td.invoice-balance');

        var invoiceAmountTotal = parseFloat(tdInvoiceTotal.text());
        var invoiceAmountPaid = parseFloat(tdInvoiceAmountPaid.text());
        var amountDifference = invoiceAmountTotal - invoiceAmountPaid;

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
        inputPaymentAmount.val(tdInvoiceBalance.text());
        var creditCardInfo = modalBody.find('section.credit-card-info');

        var cardHolderName = creditCardInfo.find('input.card-holder-name');
        var cardNumber = creditCardInfo.find('input.credit-card-number');
        var cardMonth = creditCardInfo.find('select.credit-card-month');
        var cardYear = creditCardInfo.find('select.credit-card-year');
        var cardCvv = creditCardInfo.find('input.credit-card-cvv');
        var cardZip = creditCardInfo.find('input.credit-card-zip');
        var cardAddress = creditCardInfo.find('input.credit-card-adress');

        var paidInFullDiv = primaryInvoiceTotalSection.find('div.paid-in-full');

        $(button).click(function () {
            if (isNaN(tdInvoiceBalance.text())) {
                alert("Please input valid payment amount. ");
                return;
            }

            var paidAmount = parseFloat(tdInvoiceBalance.text());
            if (paidAmount > amountDifference) {
                alert("Cannot pay more than invoice total. ");
                return;
            }

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
            var creditCardRow = modalBody.find('div.credit-card-row');
            var creditCardRowContent = creditCardRow.remove();
           // modalBody.html('');
            modalBody.append(spinner);

            var paymentButton = button.remove();
            modalFooter.html('');

            var dataType = 'application/json; charset=utf-8';

            $.ajax({
                type: "POST",
                url: "/api/Invoice/PostApplyPayment",
                contentType: dataType,
                dataType: "json",
                data: jsonData,
                success: function (result) {

                    modalFooter.html('');
                    if (result.approved) {
                        var paymentMessage = "<h2>" + result.message + "</h2> <h3>Amount paid: " + result.amount_paid + "</h3>";
                        paymentMessage += "<h3>AuthCode: " + result.auth_code+ "</h3>";
                        paymentMessage += "<h3>Card: " + result.card_last4 + "</h3>";
                        paymentMessage += "<h3>TransactionId: " + result.transaction_id + "</h3>";

                        modalBody.html(paymentMessage);

                        tdInvoiceAmountPaid.html(result.amount_paid_total);
                        tdInvoiceBalance.html(invoiceAmountTotal - result.amount_paid_total);

                        if (invoiceAmountTotal - result.amount_paid_total === 0) {
                            paidInFullDiv.removeClass("d-none");
                        }
                    }
                    else {
                        modalBody.html(result.message);
                        var tryAgainBbutton = SMLYS.getModalFooterButton('try-again-btn', 'Try Again');
                        modalFooter.html(tryAgainBbutton);
                        $(tryAgainBbutton).click(function () {
                            modalBody.html(creditCardRowContent);
                            modalFooter.html(paymentButton);
                        });
                    }
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


