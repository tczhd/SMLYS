var SMLYS = {

    name: 'SMLYS',

    getSpinner: function () {
        var button = "<img src='/images/loading.gif' width ='30' height ='30' ></img>";
        return $(button);
    },

    getModalFooterButton: function (id, buttonName) {
        var button = "<button type='button' id='" + id + "' class='btn btn-warning btn-lg shiny'  data-dismiss='modal' aria-hidden='true'>" + buttonName + "</button>";
        return $(button);
    }


}; 
