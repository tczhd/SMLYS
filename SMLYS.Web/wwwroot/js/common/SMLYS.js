var SMLYS = {

    name: 'SMLYS',

    getSpinner: function () {
        var button = "<img src='/images/loading.gif' width ='30' height ='30' ></img>";
        return $(button);
    },

    getModalFooterButton: function (id, buttonName) {
        var button = "<button type='button' id='" + id + "' class='btn btn-warning btn-lg shiny'  data-dismiss='modal' aria-hidden='true'>" + buttonName + "</button>";
        return $(button);
    },

    getModalPagination: function (filter, controllerName, currentPage, pages, totalPages) {

        var pagination = "<nav class='table-responsive'>";
        pagination += "<ul class='pagination justify-content-center d-flex flex-wrap'>";

        if (currentPage > 1) {
            var href = "/" + controllerName + "?page=1" + filter;
            pagination += getPaginationItem(href, "First", false);

            href = "/" + controllerName + "?page=" + (currentPage - 1) + filter;
            pagination += getPaginationItem(href, "Previous", false);
        }

        $.each(pages, function (index, val) {
            var active = false;
            if (val === currentPage) {
                active = true;
            }

            var href = "/" + controllerName + "?page=" + val + filter;
            pagination += pagination += getPaginationItem(href, "" + val, active);
        });

        if (currentPage < totalPages) {

            href = "/" + controllerName + "?page=" + (currentPage + 1) + filter;
            pagination += getPaginationItem(href, "First", false);

            href = "/" + controllerName + "?page=" + totalPages + filter;
            pagination += getPaginationItem(href, "Previous", false);
        }

        pagination += "</ul></nav >";

        return $(button);
    },

    getPaginationItem: function (href, name, active) {

        var li = "<li class='page-item'>";
        if (active) {
            li = "<li class='page-item active'>";
        }

        li += "<a class='page-link' href='" + href + "'>" + name + "</a>";

        li += " </li>";

        return li;
    }
}; 
