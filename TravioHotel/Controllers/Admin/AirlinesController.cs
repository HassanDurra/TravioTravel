using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using TravioHotel.CustomClasses;
using TravioHotel.DataContext;
using TravioHotel.Models;

namespace TravioHotel.Controllers.Admin
{
	public class AirlinesController : Controller
	{
		public readonly DatabaseContext Database;
		public readonly IHttpClientFactory HttpClientFactory;

		public AirlinesController( DatabaseContext _database , IHttpClientFactory _httpClientFactory)
		{
			this.Database = _database;
			this.HttpClientFactory = _httpClientFactory;
		}
		public IActionResult Index()
		{

			return View("Views/Admin/Airlines/Index.cshtml");
		}

		public async Task<IActionResult> addServices(Airlines airlineServices)
		{
			var client = HttpClientFactory.CreateClient();
			client.DefaultRequestHeaders.Add("X-Api-Key", "anTBNYvOBTsdb1abq+8zuw==24rvwrou6aVPb27j");
			var response = await client.GetAsync("https://api.api-ninjas.com/v1/airlines");
			response.EnsureSuccessStatusCode();

			var content   = await response.Content.ReadAsStringAsync();
			var airlines = JsonConvert.DeserializeObject<List<AirlinesApi>>(content); 

			foreach( var airlineList in airlines)
			{
				var allData = new Airlines() { 
					AirlineImage  = airlineList.logo_url,
					Airlinename   = airlineList.name ,
					IATACode      = airlineList.iata ,
					ICAOCode      = airlineList.icao				
				};
				Database.Airlines.Add(allData);
			}
			
			var saveChanges  = 	await Database.SaveChangesAsync();

			if(saveChanges > 0)
			{
				TempData["Success"] = "Airlines Services Added to Database Successfully";
				return RedirectToAction("Index", "Airlines");
			}
			TempData["Error"] = "failed To Save Services To Database";
			return RedirectToAction("Index", "Airlines");
			
		}
	}
}
