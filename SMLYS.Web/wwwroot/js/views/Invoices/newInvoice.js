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
                SMLYS.Invoice.UpdateTotal();
                itemRow.remove();
            });

            SMLYS.Invoice.UpdateTotal();
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

        var taxRateTotal = SMLYS.Invoice.GetTaxRate();

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

        SMLYS.Invoice.UpdateTotal();
    },

    GetTaxRate: function () {
        var taxRateTotal = 0;
        $.each(SMLYS.Invoice.Taxes, function (key, value) {
            taxRateTotal += value.taxRate;
        });

        return taxRateTotal;
    },

    UpdateTotal: function () {

        var invoiceDetailSection = $('section.invoice-detail-section');
              var invoiceItemsTbody = invoiceDetailSection.find('tbody.invoice-items');
        var invoiceItemsTrs = invoiceItemsTbody.find('tr.invoice-item');

        var invoiceTotalSection = $('section.invoice-total');
        var invoiceSubtotal = invoiceTotalSection.find('td.invoice-subtotal');
        var invoiceTax = invoiceTotalSection.find('td.invoice-tax');
        var invoiceTotal = invoiceTotalSection.find('td.invoice-total');

        var taxRateTotal = SMLYS.Invoice.GetTaxRate();
        var taxTotal = 0;
        var subTotal = 0;
        var total = 0;

        invoiceItemsTrs.each(function () {
            var invoiceItemTr = $(this);
            var itemQuantityTd = invoiceItemTr.find('td.item-quantity');
            var itemQuantityInput = itemQuantityTd.find('input');
            var itemCostTd = invoiceItemTr.find('td.item-cost');

            var quantity = itemQuantityInput.val();
            var cost = itemCostTd.text();

            var singleItemSubTotal = parseInt(quantity) * parseFloat(cost);
            var singleItemTax = singleItemSubTotal * taxRateTotal;
            subTotal += singleItemSubTotal;
            taxTotal += singleItemTax;

        });

        total = subTotal + taxTotal;
        invoiceSubtotal.text(subTotal.toFixed(2));
        invoiceTax.text(taxTotal.toFixed(2));
        invoiceTotal.text(total.toFixed(2));
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

        var taxRateTotal = SMLYS.Invoice.GetTaxRate();

        var inputQuantity = "<div class='col-xs-2'><input type='text' class='form-control input-sm item-quantity' value='1' size='2' ></div>";
        var firstItem = SMLYS.Invoice.Items[0];
        var taxDisplay = "<div class='tax-list'>";

        $.each(SMLYS.Invoice.Taxes, function (key, value) {
            taxDisplay += "<span class='pl-1'>" + value.taxName + "</span>";
        });

        taxDisplay += "</div>";

        var tr = "<tr class='invoice-item'>" +
            "<td>" + SMLYS.Invoice.GetItemList() + "</td> " +
            "<td class='description'>" + firstItem.description +"</td> " +
            "<td class='item-quantity'>" + inputQuantity + "</td> " +
            "<td class='item-cost'>" + firstItem.cost + "</td> " +
            "<td>" + taxDisplay +"</td >" +
            "<td class='item-subtotal'>" + firstItem.cost * (1 + taxRateTotal) + "</td >" +
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

            var button = SMLYS.getModalFooterButton('create-invoice-btn', 'Create Another Invoice');

            var jsonInvoice = { invoice_items:[]};

            var primaryInvoiceSection = $('section.primary-invoice-section');
            var selectPatient = primaryInvoiceSection.find('select[id*=selectPatient]');
            var patientId = $(selectPatient).children("option:selected").val();
            jsonInvoice.patient_id = parseInt(patientId);

            var invoiceDate = primaryInvoiceSection.find('div.invoice-date');
            jsonInvoice.invoice_date = invoiceDate.text();
            var selectDoctor = primaryInvoiceSection.find('select[id*=selectDoctor]');
            var doctorId = $(selectDoctor).children("option:selected").val();
            jsonInvoice.doctor_id = parseInt(doctorId);

            var invoiceDetailSection = $('section.invoice-detail-section');
            var invoiceItemsTbody = invoiceDetailSection.find('tbody.invoice-items');
            var invoiceItemsTrs = invoiceItemsTbody.find('tr.invoice-item');

            invoiceItemsTrs.each(function () {
                var invoiceItemTr = $(this);
                var itemSelect = invoiceItemTr.find('select[id *= selectItemList]');
                var itemId = $(itemSelect).children("option:selected").val();
                var itemQuantityTd = invoiceItemTr.find('td.item-quantity');
                var itemQuantityInput = itemQuantityTd.find('input');
                var itemCostTd = invoiceItemTr.find('td.item-cost');

                var quantity = itemQuantityInput.val();
                var cost = itemCostTd.text();

                var itemData = {
                    item_id: parseInt(itemId),
                    quantity: parseInt(quantity),
                    cost: parseFloat(cost)
                };

                jsonInvoice.invoice_items.push(itemData);

            });

            var jsonData = JSON.stringify(jsonInvoice);

            $.ajax({
                type: "POST",
                url: "/api/Invoice",
                contentType: dataType,
                dataType: "json",
                data: jsonData,
                success: function (result) {

                    modalBody.html("Add new invoice success. ");
                    $(button).click(function () {
                        window.location.href = '/Invoice/InvoiceDeatil?invoiceId=' + result.InvoiceId;
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





