$(function () {
    // using modified jQuery Autocomplete plugin v1.2.6 http://xdsoft.net/jqplugins/autocomplete/
    // $.autocomplete -> $.autocompleteInline
    //$("#example3").autocompleteInline({
    //    appendMethod: "replace",
    //    source: [
    //        function (text, add) {
    //            if (!text) {
    //                return;
    //            }

    //            $.getJSON("/home/autocomplete?searchType=1&term=" + text, function (data) {
    //                if (data && data.length > 0) {
    //                    currentSuggestion3 = data[0];
    //                    add(data);
    //                }
    //            });
    //        }
    //    ]
    //});

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
        source: "/home/suggest?searchType=1&highlights=false&fuzzy=false&",
        minLength: 2,
        position: {
            my: "left top",
            at: "left-23 bottom+10"
        }
    });
});