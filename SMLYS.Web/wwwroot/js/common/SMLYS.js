var SMLYS = {

    name: 'SMLYS',

    getModalFooterButton: function (id, buttonName) {
        var button = "<button type='button' id='" + id + "' class='btn btn-warning btn-lg shiny'  data-dismiss='modal' aria-hidden='true'>" + buttonName + "</button>";
        return $(button);
    }
}; 
