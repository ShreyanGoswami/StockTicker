// JavaScript source code
function executeQuery(params) {
    if (params.length > 0) {
        var URL = "http://stockticker20170616112945.azurewebsites.net/Home/GetDataV2?" + params;
        var URL = "http://localhost:59841/Home/GetDataV2?" + params;

        $.ajax({
            //url: 'http://localhost:59841/Home/GetData',
            //url: 'http://stockticker20170616112945.azurewebsites.net/Home/GetData',
            url: URL,
            success: function (data) {
                console.log(data);
                $("#result").append(data);
                //$("#result").html("Updates");
            }
        });
        //console.log("Calling");
        // setTimeout(executeQuery, 5000); // you could choose not to continue on failure...
    }
}

$(document).ready(function () {
    var ticker_list = "";
    var count = 0;
    var json = [
        { "company_name": "Honeywell", "ticker": "HON", "market": "NYSE" },
        { "company_name": "Microsoft", "ticker": "MSFT", "market": "NASDAQ" },
        { "company_name": "Facebook", "ticker": "FB", "market": "NASDAQ" },
        { "company_name": "Apple", "ticker": "FB", "market": "NASDAQ" },
        { "company_name": "Citrix", "ticker": "CTXS", "market": "NASDAQ" },
        { "company_name": "Twitter", "ticker": "TWTR", "market": "NASDAQ" },
        { "company_name": "Cisco", "ticker": "CSCO", "market": "NASDAQ" },
        { "company_name": "Amazon", "ticker": "AMZN", "market": "NASDAQ" },
        { "company_name": "HP Inc", "ticker": "HPQ", "market": "NYSE" },
        { "company_name": "Wipro Limited", "ticker": "WIPRO", "market": "NSE" },
        { "company_name": "Accentute", "ticker": "ACN", "market": "NYSE" },
        { "company_name": "Intellect Design Arena Ltd", "ticker": "INTELLECT", "market": "NSE" },
        { "company_name": "Google", "ticker": "GOOG", "market": "NASDAQ" }
    ];


    $("input[name=TypeList]").focusout(function () {
        console.log("Inside");
        if (count >= 0) {
            for (var i = 0; i < json.length; i++) {
                var obj = json[i];
                // console.log(obj);
                if ($(this).val() == obj.company_name) {
                    ticker_list += 'Market=' + obj.market + "&" + "Ticker=" + obj.ticker + "&";
                }
            }
        }

        $("#mainlist").append("<div >" + $(this).val() + "</div>");
        count++;
        $('#s').val("");
        console.log(ticker_list);
    });

    setTimeout(function () { executeQuery(ticker_list) }, 5000);
});