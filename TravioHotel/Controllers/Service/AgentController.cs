using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravioHotel.DataContext;
using TravioHotel.Models;
using TravioHotel.Services;

namespace TravioHotel.Controllers.Service
{
    public class AgentController : Controller
    {
        public readonly DatabaseContext Database;
        public readonly MailServer mailServer;
        public readonly IWebHostEnvironment fileEnvironment;


        public AgentController(DatabaseContext _database, IWebHostEnvironment _fileEnvironment, MailServer _mailServer)
        {
            this.Database = _database;
            this.mailServer = _mailServer;
            this.fileEnvironment = _fileEnvironment;


        }
        public IActionResult Registeration()
        {
            var Service = Database.Airlines.Where(e => e.deleted_at == null).ToList();
            ViewBag.Data = new { services = Service };
            return View("Views/Service/Registeration.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> Register(int airlineID, UserModel userData, IFormFile Image)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userData.Password);
            var createdDate = Convert.ToString(DateTime.UtcNow);
            // user information with image
            if (Image != null && Image.Length > 1)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(Image.FileName); // This Code will create or generate a unique name for the image
                var filePath = Path.Combine(fileEnvironment.WebRootPath, "UserProfileImages"); // It will create the directory if does'nt exists
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                // This will combine the image name and path to save it locally 
                var ProfileImage = Path.Combine(filePath, fileName);
                using (var saveImage = new FileStream(ProfileImage, FileMode.Create))
                {
                    Image.CopyTo(saveImage);
                }
                // In The Above code we are creating and storing our image locally too
                var UserRecords = new UserModel()
                {
                    Name = userData.Name,
                    User_name = userData.User_name,
                    Email = userData.Email,
                    Password = hashedPassword,
                    created_at = createdDate,
                    Image = fileName,

                };
                await Database.User.AddAsync(UserRecords);
                var saveUser = await Database.SaveChangesAsync();
                if (saveUser > 0) // This will check if any record has been save if yes then the success message or else the error
                {
                    // Fetch the user ID after it is created
                    int userId = UserRecords.Id;

                    var SaveAgentAccount = new Service_account
                    {
                        serviceId = airlineID,
                        userId = userId // Use the fetched user ID here
                    };

                    await Database.Service_Account.AddAsync(SaveAgentAccount);
                    await Database.SaveChangesAsync();
                    string EmailBody = $"Please Wait Till Our System Approve Your account this code to verify your account Email: '{userData.Email}'";
                    string Subject = "Service Agent Email Verification";
                    await mailServer.Mail(userData.Email, Subject, EmailBody);
                    TempData["Success"] = "User Information has been saved Please Verify Your email using the link we sent.";
                    return RedirectToAction("Registeration" , "Agent");
                }
                else
                {
                    TempData["Error"] = "Failed to save your information";
                    return RedirectToAction("Registeration" , "Agent");
                }

            }
            // Upload User information without Image
            else
            {
                var UserRecords = new UserModel()
                {
                    Name = userData.Name,
                    User_name = userData.User_name,
                    Email = userData.Email,
                    Password = hashedPassword,
                    created_at = createdDate,
                    Role = 2 

                };
               
                await Database.User.AddAsync(UserRecords);
                var saveUser = await Database.SaveChangesAsync();
                if (saveUser > 0) // This will check if any record has been save if yes then the success message or else the error
                {
                    // Fetch the user ID after it is created
                    int userId = UserRecords.Id;

                    var SaveAgentAccount = new Service_account
                    {
                        serviceId = airlineID,
                        userId = userId // Use the fetched user ID here
                    };

                    await Database.Service_Account.AddAsync(SaveAgentAccount);
                    await Database.SaveChangesAsync();

                    string EmailBody = $"Please Wait Till Our System Approve Your account this code to verify your account Email: '{userData.Email}'";
                    string Subject = "Service Agent Email Verification";
                    await mailServer.Mail(userData.Email, Subject, EmailBody);

                    TempData["Success"] = "User Information has been saved. Please verify your email using the link we sent.";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["Error"] = "Failed to save your information";
                    return RedirectToAction("Registeration", "Agent");
                }
            }

        }
    }
}
