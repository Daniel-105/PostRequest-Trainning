﻿let klarnaRequest = document.querySelector("#klarnaRequest");
let klarnaButton = document.querySelector("#KlarnaButton");
let placeOrderButton = document.querySelector("#placeOrderButton");
let authorizationToken = "";
//let dataResponse = "";
//let jsonData = "";
//let client_token = "";
//let identifier = "";

klarnaRequest.addEventListener("click", () => {
    console.log("Clicked");

    $.ajax({
        type: "POST",
        url: URL1,
        //contentType: "application/json; charset=utf-8",
        //data: Data,
        //dataType: 'json',
        success: function (data) {
            dataResponse = data;
            let jsonData = JSON.parse(dataResponse);
            let client_token = jsonData.client_token;
            let identifier = jsonData.payment_method_categories[0].identifier;
            console.log(client_token);
            console.log(identifier);
            klarnaButton.style.display = "block";
            createSession(client_token, identifier);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.error("An error occurred:", errorThrown);
        }
    });
});

function createSession(client_token, identifier) {
    //The following method initializes the Klarna Payments JS library
    klarnaAsyncCallback = function () {
        Klarna.Payments.init({
            client_token: client_token
        });
        console.log("Payments initialized");
        //The following method loads the payment_method_category in the container with the id of 'klarna_container'
        Klarna.Payments.load({
            container: '#klarna_container',
            payment_method_category: identifier

        },
            function (res) {
                console.log("Load function called")
                console.debug(res);
            });
    };
    klarnaAsyncCallback();
}

/*The following is the authorize function, which triggers Klarna to perform a risk assessment of the purchase
The successful response of this risk assessment is an authorization token, which in this example is logged in the console*/
klarnaButton.addEventListener("click", () => {

    Klarna.Payments.authorize({
        payment_method_category: 'pay_later'
    },
        {
            purchase_country: "PT",
            purchase_currency: "EUR",
            locale: "pt-PT",
            order_amount: 20000,
            order_tax_amount: 0,
            order_lines: [{
                type: "physical",
                reference: "19-402",
                name: "black T-Shirt",
                quantity: 2,
                unit_price: 5000,
                tax_rate: 0,
                total_amount: 10000,
                total_discount_amount: 0,
                total_tax_amount: 0
            },
            {
                type: "physical",
                reference: "123123",
                name: "red trousers",
                quantity: 1,
                unit_price: 10000,
                tax_rate: 0,
                total_amount: 10000,
                total_discount_amount: 0,
                total_tax_amount: 0
            }],
        }, function (res) {
            console.log("Response from the authorize call:");
            console.log(res);
            authorizationToken = res["authorization_token"];
            //authorizationToken = { "authorizationToken": res["authorization_token"] }
            console.log(authorizationToken);
            //let authorizationToken = res["authorization_token"];
            //console.log(`${authorizationToken} is of type ${typeof authorizationToken}`)
            placeOrderButton.style.display = "block";
        })
});

placeOrderButton.addEventListener("click", () => {
    console.log(authorizationToken);
    let data = { "authorizationToken": authorizationToken }
    $.ajax({
        type: "POST",
        url: URL2,
        data: data,
        dataType: "text",
        success: function (response) {
            let parsedReponse = JSON.parse(JSON.parse(response));
            console.log(response);

            console.log(typeof parsedReponse);
            console.log(parsedReponse.fraud_status);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.error("An error occurred:", errorThrown);
        }
    });
});