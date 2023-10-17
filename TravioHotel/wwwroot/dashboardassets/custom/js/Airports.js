$(document).ready(function () {

    let imageUrl    = $('input[name="Image"]');
    let name        = $('input[name="Name"]');
    let country     = $('select[name="Country_iso"]');
    let City        = $('select[name="City_name"]');
    let IataCode    = $('input[name="IataCode"]');
    let Description = $('textarea[name="Description"]');
    let AirportForm = $('#airportForm');
   
    $(AirportForm).submit(function (e) {
        if (imageUrl.val() == "") {
            e.preventDefault();
            toastr["error"]("Airport Image Url is Required");
        }
        if (name.val() == "") {
            e.preventDefault();
            toastr["error"]("Airport name  is Required");
        }
        if (country.val() == "") {
            e.preventDefault();
            toastr["error"]("Airport Country is Required");
        }
        if (IataCode.val() == "") {
            e.preventDefault();
            toastr["error"]("Airport IATA Code is Required");
        }
        if (Description.val() == "") {
            e.preventDefault();
            toastr["error"]("Airport Description is Required");
        }
        if (City.val() == "") {
            e.preventDefault();
            toastr["error"]("Airport City is Required");
        }
    });

    // Drop Down For Cities Data

    $(country).on('change', function (e) {

        e.preventDefault();
        $(City).html('<option value=""> Fetching Cities...</option>');
        $.ajax({
            url: getCitiesUrl,
            type: "Get",
            data: AirportForm.serialize(),
            success: function (response) {
                if (response.message == "Success") {
                    e.preventDefault();
                    tableRow = "";
                    $(response.city).each(function (index, value) {
                        tableRow += `
                            <option value="${value.name}">${value.name}</option>
                            `;
                    });
                    $(City).html(tableRow);
                }
            }
        });

    })
   
})