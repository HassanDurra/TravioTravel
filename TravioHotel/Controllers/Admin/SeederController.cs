﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TravioHotel.DataContext;
using TravioHotel.Models;

namespace TravioHotel.Controllers.Admin
{
    public class SeederController : Controller
    {   // Purpose of creating this controller is to store a large amount of un changeable data like countries list , airports , states , cities , airlines , services and  etc Admin Data
        public readonly DatabaseContext Database;
        public SeederController(DatabaseContext _database)
        {
            this.Database = _database;
        }
        // this will add Services Data 

        public async Task<IActionResult> addServices(Airlines airlineServices)
        {
            var JsonData = System.IO.File.ReadAllText("D:\\E-project2023\\TravioTravel\\TravioHotel\\wwwroot\\airlines.json");
            JArray jsonArrays = JArray.Parse(JsonData);

            foreach (var jsonArray in jsonArrays)
            {
                var airlineModel = new Airlines()
                {


                    AirlineImage = (string)jsonArray["AirlineImage"],
                    Airlinename = (string)jsonArray["Airlinename"],
                    ICAOCode = (string)jsonArray["ICAOCode"],
                    IATACode = (string)jsonArray["IATACode"],

                };
                Database.Airlines.AddAsync(airlineModel);

            }

            var saveChanges = await Database.SaveChangesAsync();

            if (saveChanges > 0)
            {
                TempData["Success"] = "Airlines Services Added to Database Successfully";
                return RedirectToAction("Index", "Airlines");
            }
            TempData["Error"] = "failed To Save Services To Database";
            return RedirectToAction("Index", "Airlines");

        }
        //this will add data to countries table
        public async Task<IActionResult> addCountries()
        {
            var JsonData = System.IO.File.ReadAllText("D:\\E-project2023\\TravioTravel\\TravioHotel\\wwwroot\\countries.json");
            JArray jsonArrays = JArray.Parse(JsonData);

            foreach (var jsonArray in jsonArrays)
            {
                var countryModel = new Countries()
                {

                    name = (string)jsonArray["name"],
                    iso3 = (string)jsonArray["iso3"],
                    iso2 = (string)jsonArray["iso2"],
                    timezone = (string)jsonArray["timezones"],
                    region = (string)jsonArray["region"],
                    subregion = (string)jsonArray["subregion"],
                    phonecode = (string)jsonArray["phonecode"],
                    emoji = (string)jsonArray["emoji"],
                    currency = (string)jsonArray["currency"],
                    capital = (string)jsonArray["capital"],
                    currency_symbol = (string)jsonArray["currency_symbol"],
                    tld = (string)jsonArray["tld"],
                    native = (string)jsonArray["native"]


                };
                Database.Countries.AddAsync(countryModel);

            }

            var saveChanges = await Database.SaveChangesAsync();

            if (saveChanges > 0)
            {
                TempData["Success"] = "Countries Added to Database Successfully";
                return RedirectToAction("Index", "Country");
            }
            TempData["Error"] = "failed To Save Services To Database";
            return RedirectToAction("Index", "Airlines");

        } // Countries Seeder
        public async Task<IActionResult> addStates()
        {
            var JsonData = System.IO.File.ReadAllText("D:\\E-project2023\\TravioTravel\\TravioHotel\\wwwroot\\states.json");
            JArray jsonArrays = JArray.Parse(JsonData);

            foreach (var jsonArray in jsonArrays)
            {
                var stateModel = new State()
                {

                    name         = (string)jsonArray["name"],
                    country_code = (string)jsonArray["country_code"],
                    iso2         = (string)jsonArray["iso2"],
                    country_id   = (int)jsonArray["country_id"],
                    fips         = (string)jsonArray["fips_code"],
                 
                };
                Database.State.AddAsync(stateModel);

            }

            var saveChanges = await Database.SaveChangesAsync();

            if (saveChanges > 0)
            {
                TempData["Success"] = "States  Added to Database Successfully";
                return RedirectToAction("Index", "Country");
            }
            TempData["Error"] = "failed To Save Services To Database";
            return RedirectToAction("Index", "Airlines");

        } // Countries Seeder
        public async Task<IActionResult> addCities()
        {
            var JsonData = System.IO.File.ReadAllText("D:\\E-project2023\\TravioTravel\\TravioHotel\\wwwroot\\cities.json");
            JArray jsonArrays = JArray.Parse(JsonData);

            foreach (var jsonArray in jsonArrays)
            {
                var cityModel = new Cities()
                {

                    name = (string)jsonArray["name"],
                    country_code = (string)jsonArray["country_code"],
                    country_id = (int)jsonArray["country_id"],
                    state_code = (string)jsonArray["state_code"],
                    state_id = (int)jsonArray["state_id"],
                  


                };
                Database.Cities.AddAsync(cityModel);

            }

            var saveChanges = await Database.SaveChangesAsync();

            if (saveChanges > 0)
            {
                TempData["Success"] = "Cities Added to Database Successfully";
                return RedirectToAction("Index", "Country");
            }
            TempData["Error"] = "failed To Save Services To Database";
            return RedirectToAction("Index", "Airlines");

        }
    }
}
