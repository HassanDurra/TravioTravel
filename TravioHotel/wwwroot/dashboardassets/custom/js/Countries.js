$(document).ready(function () {

    let CountriesTable = $("#countriesTableData");

    $.ajax({
        url: "https://hassandurra.github.io/CountriesData/countries.json",
        type: "Get",
        success: function (response) {
            tableRow = "";

            $(response[2].data).each(function (index, value) {
                tableRow += `<tr>
                        <td>${value.id}</td>
                        <td>${value.name}</td>
                        <td>${value.iso3}</td>
                        <td>${value.phonecode}</td>
                        <td>${value.currency}</td>
                        <td>${value.region}</td>
                        <td>${value.capital}</td>
                        <td class="d-flex">
                        <a href="" class="btn btn-danger"><i class="fa fa-trash"></i></a>
                        <a href="" class="btn btn-success"><i class="fa fa-pencil"></i></a>
                        </td>
                    </tr>`;

            });
            $(CountriesTable).html(tableRow);
        }
    })


})