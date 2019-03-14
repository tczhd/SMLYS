$(document).ready(function () {

    $('#search').click(function () {
        SMLYS.Patient.SearchPatient($(this));
    });


    $('#add').click(function () {
        SMLYS.Patient.AddSearchFilter();
    });

});

SMLYS.Patient = {

    AddSearchFilter: function () {
        var patientContent = $('div.view-patients-content');
        var searchSection = patientContent.find('div.view-patients-search-section');
        var selectType = searchSection.find('select[name*=selectType]');
        var selectedType = $(selectType).children("option:selected").val();
        var selectedTypeName = $(selectType).children("option:selected").text();

        var searchFilters = searchSection.find('div.search-filters');
        var filterInput = searchSection.find('input[id=filterInput]');
        var inputValue = filterInput.val();

        var searchItem = $('<div class="search-filter float-sm-left"></div>');
        var searchTypeSpan = $('<span class="search_type ' + selectedType + '">' + selectedTypeName + ': </span>');
        var searchContentSpan = $('<span class="search_content ' + selectedType + '" title="' + selectedType + '">' + inputValue + ' </span>');
        var closeIcon = $('<i class="fa fa-close fa-lg"></i>');

        $(closeIcon).click(function () {
            $(searchItem).remove();
        });
        searchItem.append(searchTypeSpan);
        searchItem.append(searchContentSpan);
        searchItem.append(closeIcon);
        searchFilters.append(searchItem);
    },

    SearchPatient: function (button) {
        var spinner = $('#search-spinner');
        spinner.removeClass("invisible");
        button.hide();

        var dataType = 'application/json; charset=utf-8';
        var patientContent = $('div.view-patients-content');

        var tablePatients = patientContent.find('table.table-patients');
        var bodyPatients = tablePatients.find('tbody');
        bodyPatients.empty();

        var searchPatients = [];

        var searchSection = patientContent.find('div.view-patients-search-section');
        var searchFilters = searchSection.find('div.search-filters');
        var searchItems = searchFilters.children('div.search-filter');

        $.each(searchItems, function (index, searchItem) {
            var searchContent = $(searchItem).find('span.search_content');
            var patient = {
                search_type: searchContent.attr("title"),
                search_content: searchContent.text()
            };

            searchPatients.push(patient);
        });

        var jsonData = JSON.stringify(searchPatients);

        $.ajax({
            type: "POST",
            url: "/api/Patient/PostSearchPatients",
            contentType: dataType,
            dataType: "json",
            data: jsonData,
            success: function (result) {

                if (result.success) {

                    if (result.patient_detail.length > 0) {
                        $.each(result.patient_detail, function (index, patient) {
                            var tr = SMLYS.Patient.GetPatientRow(patient.patient_name, patient.patient_address, patient.patient_phone, patient.patient_email, patient.patient_status);
                            bodyPatients.append(tr);
                        });
                    }
                    else {
                        alert("No patient was found. ");
                    }
                }
                else {

                    alert(result.message);
                }

                spinner.addClass("invisible");
                button.show();
            }, //End of AJAX Success function  

            failure: function (data) {
                alert(data.responseText);
                spinner.addClass("invisible");
                button.show();
            }, //End of AJAX failure function  
            error: function (data) {
                alert(data.responseText);
                spinner.addClass("invisible");
                button.show();
            } //End of AJAX error function  

        });
    },


    GetPatientRow: function (name, address, phone, email, status) {
        var tr = "<tr>" +
            "<td>" + name + "</td> " +
            "<td>" + address + "</td> " +
            "<td>" + phone + "</td> " +
            "<td>" + email + "</td> " +
            "<td><span class='badge badge-success'>" + status + "</span></td> " +
            "</tr>";

        return tr;
    }
};


