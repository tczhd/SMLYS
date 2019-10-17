//var SMLYS = {};
$(document).ready(function () {

    SMLYS.Invoice.Init();

});

SMLYS.Invoice = {
    Items: [],
    Taxes: [],

    Init: function () {

        // complete on TAB and clear on ESC
        $("#invoice-patient-search-input").keydown(function (evt) {
            if (evt.keyCode === 9 /* TAB */ && currentSuggestion3) {
                $("#invoice-patient-search-input").val(currentSuggestion3);
                return false;
            } else if (evt.keyCode === 27 /* ESC */) {
                currentSuggestion3 = "";
                $("#invoice-patient-search-input").val("");
            }
        });

        $("#invoice-patient-search-input").autocomplete({
            html: true,
            source: "/home/suggest?searchType=1&highlights=false&fuzzy=false&",
            minLength: 2,
            position: {
                my: "left top",
                at: "left-23 bottom+10"
            },
            select: function (event, ui) {
                alert($("#invoice-patient-search-input").val());
            }
        });

        var primaryInvoiceSection = $('section.primary-invoice-section');
        var familyId = primaryInvoiceSection.find('input[id*=FamilyId]').val();
        var patientId = primaryInvoiceSection.find('input[id*=PatientId]').val();

        $('#add_item').click(function () {

            var invoiceDetailSection = $('section.invoice-detail-section');
            var invoiceItemsTbody = invoiceDetailSection.find('tbody.invoice-items');
            var itemRow = $(SMLYS.Invoice.GetInvoiceRow());
            //var dropMenu = itemRow.find('div.dropdown-menu');
            //$(dropMenu).mdbDropSearch();

            var selectItemList = itemRow.find('select[id*=selectItemList]');
            $(selectItemList).change(function () {

                SMLYS.Invoice.UpdateItemRow(selectItemList, itemRow);
            });

            var inputQuantity = itemRow.find('input.item-quantity');
            $(inputQuantity).keypress(function (event) {

                if (event.which === 13) {
                    event.preventDefault();
                    SMLYS.Invoice.UpdateItemRow(selectItemList, itemRow);
                    SMLYS.Invoice.UpdateTotal();
                }
            });

            $(inputQuantity).blur(function (event) {
                event.preventDefault();
                SMLYS.Invoice.UpdateItemRow(selectItemList, itemRow);
                SMLYS.Invoice.UpdateTotal();
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

    SearchPatient: function () {
        // $("#job_details_div").html("Loading...");
        var searchType = "1";

        var q = $("#invoice-patient-search-input").val();
        if (q.length <= 2) {
            alert('Please input at least two characters. ');
            return;
        }

        $.post('/home/search',
            {
                searchType: searchType,
                q: q,
                currentPage: currentPage
            },
            function (data) {
                var content = GetPatientDetailsHtml(data);
                modalBody.html(content);

                var patientRows = modalBody.find('div.patient-detail-row');
                patientRows.each(function () {

                    var patientRow = $(this);
                    var patientId = patientRow.find("div.patient-id").text();
                    var createInvoiceDiv = patientRow.find("div.create-invoice");
                    var editPatientDiv = patientRow.find("div.edit-patient");
                    var createInvoiceButton = SMLYS.getButton('create-invoice-btn', 'Create Invoice', 'btn btn-info btn-sm', '', 'false');
                    var editPatientButton = SMLYS.getButton('edit-patient-btn', 'Edit Patient', 'btn btn-secondary btn-sm', '', 'false');
                    $(createInvoiceButton).click(function () {
                        window.location.href = '/Invoice/InvoiceForm?patientId=' + patientId;
                    });
                    createInvoiceDiv.html(createInvoiceButton);
                    $(editPatientButton).click(function () {
                        window.location.href = '/Patient/PatientForm?id=' + patientId;
                    });
                    editPatientDiv.html(editPatientButton);
                });

                //UpdatePagination(data.Count);
            });

    },

    UpdateItemRow: function (selectItemList, itemRow) {
        var itemId = $(selectItemList).children("option:selected").val();
        var quantityText = itemRow.find('input.item-quantity').val();
        var costTd = itemRow.find('td.item-cost');
        var itemSubtotalTd = itemRow.find('td.item-subtotal');
        var descriptionTd = itemRow.find('td.description');
        var taxRateTotal = SMLYS.Invoice.GetTaxRate();

        $.each(SMLYS.Invoice.Items, function (key, value) {
            if (value.itemId === parseInt(itemId)) {
                costTd.text(value.cost);
                itemSubtotalTd.text(value.cost);
                descriptionTd.text(value.description);

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
            var costText = itemCostTd.text();
            var cost = 0;
            if (costText.length > 0 && !isNaN(costText)) {
                cost = parseFloat(costText);
            }

            var singleItemSubTotal = parseInt(quantity) * cost;
            var singleItemTax = singleItemSubTotal * taxRateTotal;
            subTotal += singleItemSubTotal;
            taxTotal += singleItemTax;

        });

        total = subTotal + taxTotal;
        invoiceSubtotal.text(subTotal.toFixed(2));
        invoiceTax.text(taxTotal.toFixed(2));
        invoiceTotal.text(total.toFixed(2));
    },

    InitData: function (familyId) {
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

    GetItemListNew: function () {
        var selectList = "<div class='dropdown'>";

        selectList += "<button class='btn btn-primary dropdown-toggle' type='button' id='dropdownMenu1-1'  data-toggle='dropdown' >Select Service </button >";
        selectList += "<div class='dropdown-menu dropdown-primary' >";
        selectList += "<input class='form-control' type='text' placeholder='Search' aria-label='Search'>";
        $.each(SMLYS.Invoice.Items, function (key, value) {
            selectList += "<a class='dropdown-item mdb-dropdownLink-1' href='#" + value.itemId + "'>" + value.itemName + "</a>";
        });

        selectList += "</div></div>";

        return selectList;
    },

    GetItemList: function () {
        var selectList = "<select id='selectItemList' name='selectItemList' class='form-control'>";

        selectList += "<option value='0'>Select a service</option>";

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
            "<td class='description'></td> " +
            "<td class='item-quantity'>" + inputQuantity + "</td> " +
            "<td class='item-cost'></td> " +
            "<td>" + taxDisplay + "</td >" +
            "<td class='item-subtotal'>" + 0 * (1 + taxRateTotal) + "</td >" +
            "<td class='item-remove'><i class='fa fa-close fa-lg float-right' ></i></td >" +
            "</tr>";

        //var tr = "<tr class='invoice-item'>" +
        //    "<td>" + SMLYS.Invoice.GetItemList() + "</td> " +
        //    "<td class='description'>" + firstItem.description +"</td> " +
        //    "<td class='item-quantity'>" + inputQuantity + "</td> " +
        //    "<td class='item-cost'>" + firstItem.cost + "</td> " +
        //    "<td>" + taxDisplay +"</td >" +
        //    "<td class='item-subtotal'>" + firstItem.cost * (1 + taxRateTotal) + "</td >" +
        //    "<td class='item-remove'><i class='fa fa-close fa-lg float-right' ></i></td >" +
        //    "</tr>";

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

            var button = SMLYS.getModalFooterButton('view-invoice-btn', 'Go to invoice detail');

            var jsonInvoice = { invoice_items: [] };

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

            var forceSelectService = false;

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

                if (itemId === "0") {
                    itemSelect.focus();
                    forceSelectService = true;
                    return;
                }
                else {
                    jsonInvoice.invoice_items.push(itemData);
                }
            });

            if (jsonInvoice.invoice_items.length === 0 || forceSelectService) {
                modalBody.html("Please choose service before submit. ");
                $('#primaryModal').modal('hide');
                return;
            }

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
                        window.location.href = '/Invoice/InvoiceDetail?invoiceId=' + result.displayId;
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





