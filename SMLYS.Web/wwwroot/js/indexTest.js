$(function () {

    var currentPage = 1;
    var searchBoxUl = $('#head-search-box-ul');

    function AddNewPatientModal() {

        var isvalid = $("#myForm").valid();  // Tells whether the form is valid

        if (isvalid) {
            var spinner = SMLYS.getSpinner();
            $('#primaryModal').modal('show');


            var dataType = 'application/json; charset=utf-8';
            var modalBody = $('div.modal-body');
            modalBody.html(spinner);
            var modalContent = $('.modal-content');
            var modalTitle = modalContent.find('.modal-title');
            modalTitle.text("New Patient");
            var modalFooter = modalContent.find('.modal-footer');
            modalFooter.html('');

            var button = SMLYS.getModalFooterButton('create-invoice-btn', 'Create Invoice');

            var jsonPatients = [];

            var patients = $('div.patients');
            var sectionPatients = patients.find('section.patient');
            sectionPatients.each(function () {

                var patient = $(this);
                var patientId = patient.find("input[id*=PatientId]").val();
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

                if (patientId === undefined || patientId === "" || patientId.length === 0) {
                    patientId = "0";
                }

                var newPatient = {
                    patient_id: patientId,
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


        }
    }

    // using modified jQuery Autocomplete plugin v1.2.6 http://xdsoft.net/jqplugins/autocomplete/
    // $.autocomplete -> $.autocompleteInline
    $("#example3").autocompleteInline({
        appendMethod: "replace",
        source: [
            function (text, add) {
                if (!text) {
                    return;
                }

                $.getJSON("/home/autocomplete?searchType=" + $('#SearchTypeSelect').val() + "&term=" + text, function (data) {
                    if (data && data.length > 0) {
                        currentSuggestion3 = data[0];
                        add(data);
                    }
                });
            }
        ]
    });

    // complete on TAB and clear on ESC
    $("#example3").keydown(function (evt) {
        if (evt.keyCode === 9 /* TAB */ && currentSuggestion3) {
            $("#example3").val(currentSuggestion3);
            return false;
        } else if (evt.keyCode === 27 /* ESC */) {
            currentSuggestion3 = "";
            $("#example3").val("");
        }
    });

    $("#example3").autocomplete({
        html: true,
        source: "/home/suggest?searchType=" + $('#SearchTypeSelect').val() + "&highlights=false&fuzzy=false&",
        minLength: 2,
        position: {
            my: "left top",
            at: "left-23 bottom+10"
        },
        select: function (event, ui) {
            Search();
        }
    });


    var searchBoxSubmit = searchBoxUl.find('input.searchBoxSubmit');
    searchBoxSubmit.click(function () {
        Search();
    });

    function Search() {
        // $("#job_details_div").html("Loading...");
        var searchType = $('#SearchTypeSelect').val();

        var q = $("#example3").val();
        if (q.length <= 2) {
            alert('Please input at least two characters. ');
            return;
        }

        // Get center of map to use to score the search results
        var spinner = SMLYS.getSpinner();
        $('#primaryModal').modal('show');
        var modalBody = $('div.modal-body');
        modalBody.html(spinner);
        var modalContent = $('.modal-content');
        var modalTitle = modalContent.find('.modal-title');
        modalTitle.text("Search Patient");
        var modalFooter = modalContent.find('.modal-footer');
        modalFooter.html('');

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

    }

    function UpdatePagination(docCount) {
        // Update the pagination
        var totalPages = Math.round(docCount / 10);
        // Set a max of 5 items and set the current page in middle of pages
        var startPage = currentPage;
        if ((startPage === 1) || (startPage === 2))
            startPage = 1;
        else
            startPage -= 2;

        var maxPage = startPage + 5;
        if (totalPages < maxPage)
            maxPage = totalPages + 1;
        var backPage = parseInt(currentPage) - 1;
        if (backPage < 1)
            backPage = 1;
        var forwardPage = parseInt(currentPage) + 1;

        var htmlString = '<li><a href="javascript:void(0)" onclick="GoToPage(\'' + backPage + '\')" class="fa fa-angle-left"></a></li>';
        for (var i = startPage; i < maxPage; i++) {
            if (i === currentPage)
                htmlString += '<li  class="active"><a href="#">' + i + '</a></li>';
            else
                htmlString += '<li><a href="javascript:void(0)" onclick="GoToPage(\'' + parseInt(i) + '\')">' + i + '</a></li>';
        }

        htmlString += '<li><a href="javascript:void(0)" onclick="GoToPage(\'' + forwardPage + '\')" class="fa fa-angle-right"></a></li>';
        $("#pagination").html(htmlString);
        $("#paginationFooter").html(htmlString);


    }

    function GoToPage(page) {
        currentPage = page;
        Search();
    }

    function GetPatientDetailsHtml(data) {
        var patientDetailsHTML = '<div class="container-fluid">';
        var imgCounter = 0;
        //var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
        //];
        //$("#available-jobs-label").html('Available Jobs <small>(' + data.Count + " jobs)</small>");
        //$("#jobs-count").html(data.Count);

        //$("#available_jobs_subheader").html(data.Count + ' Available Jobs');

        for (var i = 0; i < data.results.length; i++) {

            patientDetailsHTML += '<div class="row patient-detail-row">';
            patientDetailsHTML += '<div class="patient-id" style="display:none;">' + data.results[i].document.Id + '</div>';
            patientDetailsHTML += '<div class="col-sm-12 ">';
            patientDetailsHTML += '<div class="row patient-detail-row-up">';
            patientDetailsHTML += '<div class="col-sm-2 ml-auto">';
            patientDetailsHTML += data.results[i].document.FirstName +  '</div>';
            patientDetailsHTML += '<div class="col-sm-2 ml-auto">';
            patientDetailsHTML +=  data.results[i].document.LastName + '</div>';

            patientDetailsHTML += '<div class="col-sm-1 ml-auto">';
            patientDetailsHTML += data.results[i].document.Title + '</div>';
            patientDetailsHTML += '<div class="col-sm-1 ml-auto">';
            patientDetailsHTML += data.results[i].document.Gender + '</div>';
            patientDetailsHTML += '<div class="col-sm-1 ml-auto">';
            patientDetailsHTML += data.results[i].document.Age + '</div>';
            patientDetailsHTML += '<div class="col-sm-5 ml-auto">';
            patientDetailsHTML += data.results[i].document.Phone + '</div>';
            patientDetailsHTML += '</div>';
            patientDetailsHTML += '<div class="row patient-detail-row-up">';
            patientDetailsHTML += '<div class="col-sm-6 ml-auto">';
            patientDetailsHTML += data.results[i].document.Email + '</div>';
            patientDetailsHTML += '<div class="col-sm-3 ml-auto edit-patient">';
            patientDetailsHTML += 'button1' + '</div>';
            patientDetailsHTML += '<div class="col-sm-3 ml-auto create-invoice">';
            patientDetailsHTML += 'button2' + '</div>';
            patientDetailsHTML += '</div>';

            patientDetailsHTML += '</div>';
            patientDetailsHTML += '</div>';

        }

        patientDetailsHTML += "</div>";

        return patientDetailsHTML;
    }
});
