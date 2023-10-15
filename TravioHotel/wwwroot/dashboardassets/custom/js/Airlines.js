$(document).ready(function () {

    let AirlineForm = $("#airlinesForm");
    let airlineUrl  = $('input[name="AirlineImage"]');
    let airlineName = $('input[name="Airlinename"]');
    let icaoCode    = $('input[name="ICAOCode"]');
    let iataCode    = $('input[name="IATACode"]');
    //Validations for airline adding form
    $(AirlineForm).submit(function (e) {

        if (airlineUrl.val() == "") {
            e.preventDefault();
            toastr['error']("Airline Service Logo Url Is Required");
        }
        if (airlineName.val() == "") {
            e.preventDefault();
            toastr['error']("Airline Service Name is required");
        }
        if (icaoCode.val() == "") {
            e.preventDefault();
            toastr['error']("ICAO (The International Civil Aviation Organization)  Code  is required");
        }
        if (iataCode.val() == "") {
            e.preventDefault();
            toastr['error']("IATA (International Air Transport Association)  Code  is required");
        }
    })

})