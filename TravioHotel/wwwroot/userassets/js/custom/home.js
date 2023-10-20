$(document).ready(function () {
    let from_country = $('select[name="from_country"]');
    let from_city    = $('select[name="from_city"]');
    let to_country   = $('select[name="to_country"]');
    let to_city      = $('select[name="to_city"]');

    $(from_country).on("change", function (e) {
        e.preventDefault();
        $(from_city).html("<option>Fetching Data..</option>");
        $.ajax({
            url: getCitiesUrl + '/' + from_country.val(),
            type: 'get', 
            success: function (response) {
                $(from_city).prop("readonly" , false)
                selectOptions = "";
                $(response.city).each(function (index, value) {
                    selectOptions += `
                    <option value="${value.name}">${value.name}</option>
                    `;
                })
                $(from_city).html(selectOptions);
            }
        })
    }) // From Country Cascading Drop Down

    $(to_country).on("change", function (e) {
        e.preventDefault();
        $(to_city).html("<option>Fetching Data..</option>");
        $.ajax({
            url: getCitiesUrl + '/' + to_country.val(),
            type: 'get',
            success: function (response) {
                $(from_city).prop("readonly", false)
                selectOptions = "";
                $(response.city).each(function (index, value) {
                    selectOptions += `
                    <option value="${value.name}">${value.name}</option>
                    `;
                })
                $(to_city).html(selectOptions);
            }
        })
    }) // to Country Cascading Drop Down

})