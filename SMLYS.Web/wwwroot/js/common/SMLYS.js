var SMLYS = {

    name: 'SMLYS',

    getSpinner: function () {
        var button = "<img src='/images/loading.gif' width ='30' height ='30' ></img>";
        return $(button);
    },

    getButton: function (id, buttonName, classNames, dataDismiss, ariaHidden) {
        var button = "<button type='button' id='" + id + "' class='" + classNames + "'  data-dismiss='" + dataDismiss + "' aria-hidden='" + ariaHidden + "'>" + buttonName + "</button>";
        return $(button);
    },

    getModalFooterButtonString: function (id, buttonName) {
        var button = "<button type='button' id='" + id + "' class='btn btn-warning btn-lg shiny'  data-dismiss='modal' aria-hidden='true'>" + buttonName + "</button>";
        return $(button);
    },

    getModalFooterButton: function (id, buttonName) {
        var button = getModalFooterButtonString(id, buttonName);
        return $(button);
    },

    setModalPagination: function (methodName, currentPage, pages, totalPages) {

        var pagination = "<ul class='pagination justify-content-center d-flex flex-wrap'>";

        if (currentPage > 1) {
            pagination += SMLYS.getPaginationItem(1, "First", false);

            pagination += SMLYS.getPaginationItem(currentPage - 1, "Previous", false);
        }

        $.each(pages, function (index, val) {
            var active = false;
            if (val === currentPage) {
                active = true;
            }

            pagination += SMLYS.getPaginationItem(val, "" + val, active);
        });

        if (currentPage < totalPages) {

            pagination += SMLYS.getPaginationItem(currentPage + 1, "Next", false);

            pagination += SMLYS.getPaginationItem(totalPages, "Last", false);
        }

        pagination += "</ul>";

        var footerNavPagination = $('#footer-nav-pagination');
        footerNavPagination.html(pagination);

        var liPages = footerNavPagination.find("a.page-link");

        $(liPages).each(function () {
            var link = $(this);
            var pageIndex = link.attr("name");
            $(link).click(function (event) {
                event.preventDefault();
                SMLYS.executeFunctionByName(methodName, $(link), parseInt(pageIndex));
            });
        });
    },

    getPaginationItem: function (page, name, active) {

        var li = "<li class='page-item'>";
        if (active) {
            li = "<li class='page-item active'>";
        }

        li += "<a class='page-link' href='#' name='" + page + "'>" + name + "</a>";

        li += " </li>";

        return li;
    },

    executeFunctionByName: function executeFunctionByName(functionName, button, page ) {
        var context = window;
        //var args = Array.prototype.slice.call(arguments, 2);
        var namespaces = functionName.split(".");
        var func = namespaces.pop();
        for (var i = 0; i < namespaces.length; i++) {
            context = context[namespaces[i]];
        }

        return context[func](button, page);
        //return context[func].apply(context, button, page );
    }

    //executeFunctionByName: function (functionName, context /*, args */) {
    //    var args, namespaces, func;

    //    if (typeof functionName === 'undefined') { throw 'function name not specified'; }

    //    if (typeof eval(functionName) !== 'function') { throw functionName + ' is not a function'; }

    //    if (typeof context !== 'undefined') {
    //        if (typeof context === 'object' && context instanceof Array === false) {
    //            if (typeof context[functionName] !== 'function') {
    //                throw context + '.' + functionName + ' is not a function';
    //            }
    //            args = Array.prototype.slice.call(arguments, 2);

    //        } else {
    //            args = Array.prototype.slice.call(arguments, 1);
    //            context = window;
    //        }

    //    } else {
    //        context = window;
    //    }

    //    namespaces = functionName.split(".");
    //    func = namespaces.pop();

    //    for (var i = 0; i < namespaces.length; i++) {
    //        context = context[namespaces[i]];
    //    }

    //    return context[func].apply(context, args);
    //}
}; 
