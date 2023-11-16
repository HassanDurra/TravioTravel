using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using TravioHotel.DataContext;
using Newtonsoft.Json;
using TravioHotel.Models;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravioHotel.Services;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace TravioHotel.Controllers.User
{
    public class BookingController : Controller
    {
        public readonly DatabaseContext Database;
        public readonly IWebHostEnvironment fileEnvironment;
        public readonly IHttpContextAccessor httpContext;
        public readonly PDFGenerate _PdfGenerator;
        public BookingController(DatabaseContext _database, IWebHostEnvironment fileEnvironment , IHttpContextAccessor _httpContext , PDFGenerate pDFGenerate)
        {
            this.Database = _database;
            this.fileEnvironment = fileEnvironment;
            this.httpContext = _httpContext;
            this._PdfGenerator = pDFGenerate;
       
        }
        public IActionResult FlightBooking(string FlightDetails)
        {
            string isLogedIn = httpContext.HttpContext.Session.GetString("user") ?? "";
            ViewBag.isLoggedIn = isLogedIn;
            if (isLogedIn != "")
            {
               
             
                ViewBag.details = FlightDetails;
                return View("Views/User/FlightBooking.cshtml");
            }
            TempData["Error"] = "Before Booking Tickets You Must Login To Your Account";
            return RedirectToAction("Login" , "Auth");
        }
        public IActionResult checkCurrency(string Currency)
        {
            var currencySymbol = Database.Countries.Where(e => e.currency == Currency).FirstOrDefault();
            return Json(currencySymbol);
        }
        // Adding Booking details and Passangers Details 
        public async Task<IActionResult> addBookingDetails(IFormFileCollection passanger_images, IFormCollection requestData)
        {
            var createdDate    = Convert.ToString(DateTime.UtcNow);
            var userId        = Convert.ToInt64(requestData["user_id"]);
   
            var airlineDetails = new BookingFlightDetails
            {
                airline_image = requestData["airline_image"],
                airline_name = requestData["airline_name"],
                air_craft_id = requestData["aircraft_code"],
                journey_type = requestData["journey_type"],
                from = requestData["from"],
                to = requestData["to"],
                departure_date = requestData["departure_date"],
                departure_time = requestData["departure_time"],
                arrival_time = requestData["arrival_time"],
                arrival_date = requestData["arrival_date"],
                flight_duration = requestData["duration"],
                class_type = requestData["class_name"],
                total_price = requestData["currency"] + "" + requestData["total_price"],
                created_at = createdDate ,
                user_id    = (int)userId
            };
            await Database.BookingFlightDetails.AddAsync(airlineDetails);
            var addAirlinesDetails = await Database.SaveChangesAsync();
            if (addAirlinesDetails > 0) {
                List<BookingFlightClientDetails> passangersDetailsList = new List<BookingFlightClientDetails>();

                for (int index = 0; index < requestData["email[]"].Count; index++)
                {
                    Random random = new Random();
                    long maxNumber = 9999999999;
                    long randomNumericNumber = (long)(random.NextDouble() * (maxNumber - 1000000000) + 1000000000);
                    string randomNumericString = "BKN" + "-" + randomNumericNumber.ToString();
                    var FileName = requestData["email[]"][index] + Path.GetExtension(passanger_images[index].FileName);
                    var FilePath = Path.Combine(fileEnvironment.WebRootPath, "PassangersImages");
                    if (!Directory.Exists(FilePath))
                    {
                        Directory.CreateDirectory(FilePath);
                    }
                    var PassangerImageProfile = Path.Combine(FilePath, FileName);
                    using (var SaveImage = new FileStream(PassangerImageProfile, FileMode.Create))
                    {
                        passanger_images[index].CopyTo(SaveImage);
                    }
                    // Above Code will get the path and it will store the image to the local Folder;  
                    var passangersDetailsAdd = new BookingFlightClientDetails
                    {
                        flight_details_id = airlineDetails.id,
                        firstName = requestData["first_name[]"][index],
                        lastName = requestData["last_name[]"][index],
                        passport_number = requestData["passport_number[]"][index],
                        Cnic_number = requestData["cnic_number[]"][index],
                        country_name = requestData["country_name[]"][index],
                        city_name = requestData["city[]"][index],
                        email = requestData["email[]"][index],
                        contact_number = requestData["phone_number[]"][index],
                        date_of_birth = requestData["date_of_birth[]"][index],
                        age = requestData["age[]"][index],
                        image = FileName,
                        created_at = createdDate,
                        Booking_Number = randomNumericString,
                        payment_method = "EasyPaisa"

                    };
                    passangersDetailsList.Add(passangersDetailsAdd);
                    await Database.BookingClientDetails.AddAsync(passangersDetailsAdd);
                
                }
                var passangersDetailsSave = await Database.SaveChangesAsync();

                if(passangersDetailsSave > 0)
                {
                    TempData["Success"] = "Booking Has Been successful";
                    var passanger = JsonConvert.SerializeObject(passangersDetailsList);
                    httpContext.HttpContext.Session.SetString("passangers", passanger);
                    return RedirectToAction("ThankYou" , "Booking");
                }

            }
            ViewBag.Data = "Error";
            return View("Views/User/Testing.cshtml");

        }

        public IActionResult ThankYou()
        {
            ViewBag.Data = httpContext.HttpContext.Session.GetString("passangers");
            return View("Views/User/Thankyou.cshtml");
        }
        // Pdf Generate
       
        
        public async Task<IActionResult> generatePdf(int Id)
        {
            var userData = await Database.BookingClientDetails.Where(e => e.id == Id).FirstOrDefaultAsync();
            var FlightDetails = await Database.BookingFlightDetails.Where(e => e.id == userData.flight_details_id).FirstOrDefaultAsync();
            // Construct the base path for the PassangersImages folder
            string baseImagePath = "~/PassangersImages/";

            string fullImagePath = $"{baseImagePath}/{userData.image}";
            var htmlContent = "  <div class=\"main-container\" >\r\n        <div class=\"heading-container\" style=\"border-bottom:2px solid orange;background-color: black;  padding:0px 20px ; font-family: arial;\">\r\n            <table style=\"width: 100%; \">\r\n                <tr>\r\n                       <th>      <h1 style=\"color: white; position: relative; top:5px; right:70px\">Travio<b style=\"color: orange;\">Travel</b></h1></th>\r\n                        <th><h2 style=\"color: white; position: relative; top:5px; left:70px\"> Flight Ticket</h2></th>\r\n                </tr>\r\n            </table>\r\n        </div>\r\n            <!-- Heading Ends Here -->\r\n        <div class=\"airline-section\" style=\"font-family: arial; margin-top:30px ; padding: 0px 10px;\">\r\n           <table style=\"width: 100%; border:1px solid rgb(135, 135, 135) ; padding:10px\">\r\n            <tr>\r\n                <td class=\"image\">\r\n\r\n                        <img src='"+FlightDetails.airline_image+"' style=\"width:30pox ; height:30px ;object-fit:contain\" alt=\"\">\r\n\r\n\r\n                        <b>| '"+ FlightDetails.airline_name + "'</b>\r\n\r\n\r\n                </td>\r\n                <th class=\"date\">\r\n                    <h2 style=\"color: black; position: relative; top:5px; left:80px; font-size: 13px;\"> Booking Date : <b>'"+userData.created_at+"' </b></h2>\r\n                </th>\r\n            </tr>\r\n           </table>\r\n        </div>\r\n           <div class=\"airline-section\" style=\"font-family: arial; margin-top:30px ; padding: 0px 10px;\">\r\n            <table style=\"width: 100%; padding:10px\">\r\n             <tr>\r\n                 <td class=\"image\">\r\n\r\n\r\n\r\n                    <img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAACXBIWXMAAAsTAAALEwEAmpwYAAAEq0lEQVR4nO1ZWYhcVRB9cYlLFFQURf0Q/VDIh4pfgjKiIoEsmul7evrVqe7WfPSXccMvxUxAfxQ3EHH7FIS4oGLEDZkxGI0QCaJjZNQhgkk0YogGHYnLyH39WtvXb739Xk+QFNyPXl7VPVWn6tat53lH5H8qAJaStatI3KNinlNiuxK7lGa/CvaoYEZp3lcxm1Rwe9PH5etXrDjOOxxkbGzsGJHaKhW8oMRBJRYKrl9VzLPWAZ7nLVkk75v1SrPbYfOxi4IvSejIAJFYScFc3yZmAzoRO1I3Skw3fYy1GrXlJDZ2qRYLaKsILqsMgKouU8GTSvwVGt1JYk3Pg5OTk0cBOE1ETgVwkv3ObpyCx1SwpQ/Q8zcBZwQ6E6Nj/lTBfQCOLhVEu42zVMzHfcYONKV+bREqquCtPjD7mmKQSTdiutFonFkKiNbExAURKv2ziuoKKFU0fwRfNBqNs4ePBPF1khEXnU5giFlnMJ3OqhMjdCoFiCsYEh9YijoYM09khHzKFYgrGBU8XshIcMj9W50SPBQcYk5iq5atXk7nDWvXFDnsvspS6ArCVitbtVxAaJgvuVobErfmUTjCRF/oW93CI7gl1VCn0zk2aPAqAFICiAVV3BzQSzCXeliSWJtX6chBEAu2dbEdRTdXYBINqmBzXqW2ZxolCA0iYloquDME8kriuUEx83mV2g1GdUQbwqAyJTSHjutBcu25QS9mrwCqy+I8t7KQUsHMQETL2/BCrPMEbwd7FWwNvvPrq+OA3F9UcZReVQNRmu/CFHgoiRX2x1cLeyiiqHogCA5i9U0jMU961WAYeqUA/ozEXSTuHTpnBFOq9Yu7VDOfxwAxP7h5qHvz6wHpfY79r+BbEi8NHRnfMNS3Ny4iLsOD/rVdBZ1169acXDXNKNgWRmR+MNkFv5Vk5GcST/Xu3Qn/OxiMiYgfs5pTTbf1SxyQvaV7jtgY3NuJ6X4KRopMcMA5rl2DQIhPqqGB+dQmupciPbDqQOe4HHmtSl6nAXFuZwSbh1dSyKCZ9HKKyPj5tmjY9sbmWwZ1NwxGxK+vXsxoxIkKbkzVrbh64CFbNos0jaMA0p6YOC8xGmIO2UY3yQOF25QyW/6oBO1IIq3Mm16S9HqYsldsc5dD1LYjibSq35D4oL0+5hk8OKydRUG0GrXlifoEe+y1PFVB93VBBVERbCPhZ24gRxUlcbeXc+Bc5q0umqQv5qTVTIJDvom9GcYJWbtymB4oY+1Is930cQVp3nHKjQSPPFpJRGhui7Nnh24qeCbj2de9ohLOuLaUTKtDvZc8/WIHCirmw4wcm4t7NpfYUX6ZVYxiXo7asKdz9gjV7Pf98YucQPR7qzQw/+X3EjuetVHKiMT3zUbtUq8MabVwjhIfDZcb2NcrvbYdCl9pZ0QQcyQu9MqUIBlpnnYGI3jE6rEby1XexWxqt68/xatKyNp1LlQTwSXBbFnMTxn5sLspaHqjEAAnkLjDTkUKRGTKls+0hCaxIfdhVzKgpXawTMEbSvzuQLk/lOY9q6Pdbh/vHQ7i+/7pqvWaCh6g4N1w4HcgLLvz4dRk1kaFNA83pTZeaQ4cEW9x5W/WGd/1UdZDKwAAAABJRU5ErkJggg==\" style=\"position: relative; top:25px\">\r\n                     <b style=\"position: relative ;left:65px ;font-size:15px;\">'"+ FlightDetails.from+ "'</b><br>\r\n                     <small style=\"position: relative ;left:120px ;font-size:10px; bottom:5px\">PAKISTAN</small>\r\n\r\n\r\n                 </td>\r\n                 <td class=\"image\">\r\n\r\n\r\n                    <img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAACXBIWXMAAAsTAAALEwEAmpwYAAABTElEQVR4nO2bQQqDMBBF/yUS6v2v0a7aQkFXdeFxUgxZtJJaJWZmrP/BgCQMmD8/CcQIEEIIIVviAXQpvEK+Kg7AE0BIMQBoBPNV8QD69OL95NkL5Kvi3io3Vu2UaWsq5qvTfamWX1jJ0nx1upmXXDKI0nx13A+71u43gaMIoAgjdAIoQoROAEWI0AmgCBE6ARQhQieAIkToBFAEG064vx1CWojH9AUXHIoU9Qdjcc4IUFWEkGIPVJkOYUcCTCvZbtEfKAB244Dc94Pi/mAsLtKL4M3AoFW3wb+09Yp+VUwN/p6mQ802SVuvtn3I7AZbt5msvLQAJgePZNcr6raZs70kZisvAQcPVh60PWSnhSqOCx642g/c6j7hnMefLngjh78o2R39qqzLnLevsW1pvgn8ka/Lz/3wcBLMN4FPX1zagl9mSvIJIYQQLOMFHqPaAEAakSIAAAAASUVORK5CYII=\">\r\n                       </td>\r\n                <td class=\"image\">\r\n\r\n                          <b style=\"position: relative ;right:15px ;font-size:15px; top:30px\">'"+ FlightDetails.to + "'</b><br>\r\n                     <small style=\"position: relative ;right:15px ;font-size:10px; bottom:10px \">PAKISTAN</small>\r\n\r\n\r\n                   <img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAYAAAAeP4ixAAAACXBIWXMAAAsTAAALEwEAmpwYAAAEq0lEQVR4nO1ZWYhcVRB9cYlLFFQURf0Q/VDIh4pfgjKiIoEsmul7evrVqe7WfPSXccMvxUxAfxQ3EHH7FIS4oGLEDZkxGI0QCaJjZNQhgkk0YogGHYnLyH39WtvXb739Xk+QFNyPXl7VPVWn6tat53lH5H8qAJaStatI3KNinlNiuxK7lGa/CvaoYEZp3lcxm1Rwe9PH5etXrDjOOxxkbGzsGJHaKhW8oMRBJRYKrl9VzLPWAZ7nLVkk75v1SrPbYfOxi4IvSejIAJFYScFc3yZmAzoRO1I3Skw3fYy1GrXlJDZ2qRYLaKsILqsMgKouU8GTSvwVGt1JYk3Pg5OTk0cBOE1ETgVwkv3ObpyCx1SwpQ/Q8zcBZwQ6E6Nj/lTBfQCOLhVEu42zVMzHfcYONKV+bREqquCtPjD7mmKQSTdiutFonFkKiNbExAURKv2ziuoKKFU0fwRfNBqNs4ePBPF1khEXnU5giFlnMJ3OqhMjdCoFiCsYEh9YijoYM09khHzKFYgrGBU8XshIcMj9W50SPBQcYk5iq5atXk7nDWvXFDnsvspS6ArCVitbtVxAaJgvuVobErfmUTjCRF/oW93CI7gl1VCn0zk2aPAqAFICiAVV3BzQSzCXeliSWJtX6chBEAu2dbEdRTdXYBINqmBzXqW2ZxolCA0iYloquDME8kriuUEx83mV2g1GdUQbwqAyJTSHjutBcu25QS9mrwCqy+I8t7KQUsHMQETL2/BCrPMEbwd7FWwNvvPrq+OA3F9UcZReVQNRmu/CFHgoiRX2x1cLeyiiqHogCA5i9U0jMU961WAYeqUA/ozEXSTuHTpnBFOq9Yu7VDOfxwAxP7h5qHvz6wHpfY79r+BbEi8NHRnfMNS3Ny4iLsOD/rVdBZ1169acXDXNKNgWRmR+MNkFv5Vk5GcST/Xu3Qn/OxiMiYgfs5pTTbf1SxyQvaV7jtgY3NuJ6X4KRopMcMA5rl2DQIhPqqGB+dQmupciPbDqQOe4HHmtSl6nAXFuZwSbh1dSyKCZ9HKKyPj5tmjY9sbmWwZ1NwxGxK+vXsxoxIkKbkzVrbh64CFbNos0jaMA0p6YOC8xGmIO2UY3yQOF25QyW/6oBO1IIq3Mm16S9HqYsldsc5dD1LYjibSq35D4oL0+5hk8OKydRUG0GrXlifoEe+y1PFVB93VBBVERbCPhZ24gRxUlcbeXc+Bc5q0umqQv5qTVTIJDvom9GcYJWbtymB4oY+1Is930cQVp3nHKjQSPPFpJRGhui7Nnh24qeCbj2de9ohLOuLaUTKtDvZc8/WIHCirmw4wcm4t7NpfYUX6ZVYxiXo7asKdz9gjV7Pf98YucQPR7qzQw/+X3EjuetVHKiMT3zUbtUq8MabVwjhIfDZcb2NcrvbYdCl9pZ0QQcyQu9MqUIBlpnnYGI3jE6rEby1XexWxqt68/xatKyNp1LlQTwSXBbFnMTxn5sLspaHqjEAAnkLjDTkUKRGTKls+0hCaxIfdhVzKgpXawTMEbSvzuQLk/lOY9q6Pdbh/vHQ7i+/7pqvWaCh6g4N1w4HcgLLvz4dRk1kaFNA83pTZeaQ4cEW9x5W/WGd/1UdZDKwAAAABJRU5ErkJggg==\" style=\"position: relative; left :110px\">\r\n\r\n\r\n                </td>\r\n             </tr>\r\n            </table>\r\n         </div>\r\n               <div class=\"passangersDetails\"  style=\"font-family: arial;  margin-top:30px ; padding: 0px 30px; white-space:nowrap\">\r\n            <div class=\"heading\" style=\"display: flex; justify-content: space-between;\">\r\n                <table style=\"width: 100%;\">\r\n                    <tr>\r\n                        <td>\r\n                            <h1 style=\"border-bottom: 2px solid orange; font-size: 20px; width:max-content;position: relative; top:5px; right:10px\">Passangers Details</h1>\r\n                        </td>\r\n                        <td >\r\n                            <span style=\"font-size: 20px; padding:10px 10px; width:max-content;position: relative; top:5px; left:170px ;border: 1px solid rgb(135, 135, 135)\">Booking Number : '"+userData.Booking_Number+"'</span>\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n\r\n\r\n            </div>\r\n            <table class=\"passanger-data\" >\r\n                <tr>\r\n                <td>\r\n                <ul style=\"list-style: none; position: relative; right: 50px;\">\r\n                    <li style=\"border:1px solid rgb(97, 94, 94); padding:3px 10px;color: grey; margin-top:3px; font-size: 15px; text-align: start;\"><span>Name :</span> &nbsp; <span style=\"color: rgb(67, 66, 66);\">'"+userData.firstName+" "+ userData.lastName +"'</span></li>\r\n                    <li style=\"border:1px solid rgb(97, 94, 94); padding:3px 10px;color: grey; margin-top:3px; font-size: 15px; text-align: start;\"><span>Email :</span> &nbsp; <span style=\"color: rgb(101, 101, 101);\">'"+userData.email+"'</span></li>\r\n                    <li style=\"border:1px solid rgb(97, 94, 94); padding:3px 10px;color: grey; margin-top:3px; font-size: 15px; text-align: start;\"><span>Phone Number :</span> &nbsp; <span style=\"color: rgb(103, 103, 103);\">'"+userData.contact_number+ "'</span></li>\r\n                    <li style=\"border:1px solid rgb(97, 94, 94); padding:3px 10px;color: grey; margin-top:3px; font-size: 15px; text-align: start;\"><span>Passport Number :</span> &nbsp; <span style=\"color: rgb(81, 80, 80);\">52005-24387654</span></li>\r\n                    <li style=\"border:1px solid rgb(97, 94, 94); padding:3px 10px;color: grey; margin-top:3px; font-size: 15px; text-align: start;\"><span>Age :</span> &nbsp; <span style=\"color: rgb(92, 88, 88);\">20 Yrs</span></li>\r\n                    <li style=\"border:1px solid rgb(97, 94, 94); padding:3px 10px;color: grey; margin-top:3px; font-size: 15px; text-align: start;\"><span>Country Name :</span> &nbsp; <span style=\"color: rgb(98, 95, 95);\">Pakistan</span></li>\r\n                    <li style=\"border:1px solid rgb(97, 94, 94); padding:3px 10px;color: grey; margin-top:3px; font-size: 15px; text-align: start;\"><span>Address :</span> &nbsp; <span style=\"color: rgb(75, 74, 74);\">Lyari chakiwara no 1 near old post office</span></li>\r\n                </ul></td>\r\n                <tD><img src="+ fullImagePath + " style =\"width: 200px; height: 150px; object-fit: contain; padding:10px ; position:relative; bottom:10px;right:20px \" alt=\"\"></td>\r\n\r\n            </tr>\r\n        </table>\r\n        </div>\r\n</div>\r\n";
            var pdfBytes    = _PdfGenerator.GeneratePDF(htmlContent);
            return File(pdfBytes, "application/pdf", "Ticket.pdf");
        }
        public IActionResult pdfViewer()
        {
            return View("Views/User/pdfViewer.cshtml");
        }
    }
}
