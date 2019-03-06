$(document).ready(function () {



});

function printData(divToPrint) {
    newWin = window.open("");
    newWin.document.write(divToPrint);
    newWin.print();
    newWin.close();
}


function PopulateAddModal() {
    var spinner = "<img src='/images/loading.gif' width='30' height='30' ></img>";

    var dataType = 'application/json; charset=utf-8';
    var modalBody = $('div.modal-body');
    modalBody.html(spinner);
    var modalContent = $('.modal-content');
    var modalTitle = modalContent.find('.modal-title');
    modalTitle.text("New Patient");

    var jsonPatients = [];

    var patients = $('div.patients');
    var patientArray = patients.children('.patient');

    $.each(patientArray, function (index, patient) {

        var email = patient.find('input[id=Email]').val();
        var firstName = patient.find('input[id=FirstName]').val();
        var lastName = patient.find('input[id=LastName]').val();
        var company = patient.find('input[id=Company]').val();
        var address1 = patient.find('input[id=Address_Address1]').val();
        var address2 = patient.find('input[id=Address_Address2]').val();
        var city = patient.find('input[id=City]').val();
        var countryId = patient.find('select[id=selectCountry]').val();
        var stateId = patient.find('select[id=selectState]').val();
        var postalCode = patient.find('input[id=Address_PostalCode]').val();
        var phone = patient.find('input[id=Phone]').val();

        var newPatient = {
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

        jsonPatients.push(newPatient);
    });

    var x = 1;

    //var jsonData = JSON.stringify({
    //    ProductId: productId,
    //    Quantity: productQuantity,
    //    QuoteRequestProductCustomFieldData: quoteRequestProductCustomFieldData
    //});

    //$.ajax({
    //    type: "POST",
    //    url: "/api/Quote",
    //    contentType: dataType,
    //    dataType: "json",
    //    data: jsonData,
    //    success: function (data) {

    //        var bodyContent = GetQuoteDetail(data);
    //        modalBody.html(bodyContent);

    //        var printQuote = modalBody.find("button[name='PrintQuote']");
    //        $(printQuote).click(function () {
    //            var buttonSection = modalBody.find('div.button-section');
    //            buttonSection.hide();
    //            printData(modalBody.html());
    //            buttonSection.show();
    //        });

    //        var addToCart = modalBody.find("button[name='AddToCart']");
    //        $(addToCart).click(function () {
    //            $.ajax({
    //                type: "POST",
    //                url: "/Basket/AddToBasket",
    //                contentType: dataType,
    //                dataType: "json",
    //                data: JSON.stringify(data),
    //                success: function (resultData) {
    //                    window.location.href = "/Basket";
    //                }, //End of AJAX Success function  
    //                failure: function (resultData) {
    //                    alert(resultData.message);
    //                }, //End of AJAX failure function  
    //                error: function (resultData) {
    //                    alert(resultData.message);
    //                } //End of AJAX error function  
    //            });
    //        });


    //        var checkOut = modalBody.find("button[name='CheckOut']");
    //        checkOut.hide();
    //        $(checkOut).click(function () {
    //            alert("建设中...");
    //        });

    //        console.log(data);
    //    }, //End of AJAX Success function  

    //    failure: function (data) {
    //        alert(data.responseText);
    //    }, //End of AJAX failure function  
    //    error: function (data) {
    //        alert(data.responseText);
    //    } //End of AJAX error function  

    //});
}



function GetQuoteDetail(data) {

    var tableContent = "";

    $.each(data.productCustomFieldGroups, function (index, group) {
        var groupRow = "<div class='row'><div class='col-md-12'><h4 style='color:Lime;'>" +
            group.productCustomFieldGroupName + "</h4></div></div><hr style='border:1px solid;'>";

        tableContent += groupRow;

        var productCustomFieldRow = "";

        var lastIndex = 0;
        $.each(group.productCustomFields, function (index, productCustomField) {

            if (index % 2 === 0) {
                productCustomFieldRow += "<div class='row' style='margin-bottom:10px;'>";
            }

            productCustomFieldRow += "<div class='col-sm-6'>" +
                "<span style='display:inline-block;min-width:80px;font-weight: bold;'>" + productCustomField.productCustomFieldName + ":" + "</span>" +
                "<span style='padding-left:10px;'>" + productCustomField.productCustomFieldDataDescription + "</span></div>";

            if (index % 2 === 1) {
                productCustomFieldRow += "</div>";
            }

            lastIndex = index;
        });

        if (lastIndex % 2 === 0) {
            productCustomFieldRow += "</div>";
        }

        tableContent += productCustomFieldRow;
    });

    var table = "<div class='table-responsive'><table class='table table-bordered' width='100%'>" +
        "<thead><tr><th scope='row' colspan='4'>" + "<h3 style='text-align:center;font-weight: bold;'>方舟标识报价单</h3>" +
        "</th ></tr > <tr>" +
        "<th scope='col'>印品类型：</th>" +
        "<th scope='col' >" + data.productName +
        "</th>" +
        "<th scope='col'>报价日期：</th>" +
        "<th scope='col'>" + data.quoteDateString +
        "</th>" +
        "</tr></thead><tbody>" +
        "<tr>" +
        "<th scope='row' colspan='4'>印刷要求+后加工</th>" +
        "</tr><tr>" +
        "<td scope='row' colspan='4'>" + tableContent +
        "</td> " +
        "</tr><tr>" +
        "<td colspan='4' style='color:red;'>总金额：￥" + data.quoteTotal + "</td></tr>" +
        "</tbody></table></div>";

    var divButtons = "<div class='button-section' style='width:100%;margin:20px 10px;text-align:center'><button type='button' name='PrintQuote' class='btn btn-success'>打印报价单</button>" +
        "<span style='padding-left:5px;'><button type='button' name='AddToCart' class='btn btn-success'>加入购物车</button></span>" +
        "<span style='padding-left:5px;'><button type='button' name='CheckOut' class='btn btn-success'>立即购买</button></span></div>";

    var divInfo = "<div class='infoy'>" +
        "<b style = 'float:left;' > 温馨提示：</b >" +
        "<ul style='clear:both'>" +
        "<li>1、此报价请提供可直接印刷的转曲pdf文件或jpg文件，如需调整/修改文件，请与前台联系。</li>" +
        "<li>2、此报价不含税，开票需另加税点（税票分2种：6%和17%），增票/普票税点相同。</li>" +
        "<li>3、此报价不含运费，可替客户代发快递和物流（收取6元/件打包费）默认顺丰和德邦到付。</li>" +
        "<li>4、自带纸/艺术纸大额报价（2千元以上）请致电业务经理争取更多优惠。</li>" +
        "<li>5、充值会员可获取更多优惠。</li>" +
        "<li>6、自提地址：太仓方舟标识有限公司。</li>" +
        "</ul></div >";

    table = table + divButtons + divInfo;

    return table;
}