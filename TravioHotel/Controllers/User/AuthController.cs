using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using TravioHotel.DataContext;
using TravioHotel.Models;
using TravioHotel.Services;

namespace TravioHotel.Controllers
{
    public class AuthController : Controller
    {
        // Creating Database Context so we can connect with database then insert our records
        public readonly DatabaseContext Database;
        public readonly IWebHostEnvironment fileEnvironment;
        public readonly MailServer mailServer;

        public  AuthController(DatabaseContext Database  , IWebHostEnvironment _fileEnvironment , MailServer _mailServer)
        {
            this.Database        = Database;
            this.fileEnvironment = _fileEnvironment;
            this.mailServer      = _mailServer;


        }
        // End of Database Context Section
        public IActionResult Registeration()
        {
            return View("Views/User/Register.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> Store(UserModel userData , IFormFile Image)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userData.Password);
            var createdDate    = Convert.ToString(DateTime.UtcNow);
            // user information with image
            if(Image != null && Image.Length > 1)
            {
                      var fileName   = Guid.NewGuid() + Path.GetExtension(Image.FileName); // This Code will create or generate a unique name for the image
                      var filePath   = Path.Combine(fileEnvironment.WebRootPath, "UserProfileImages"); // It will create the directory if does'nt exists
                    if (!Directory.Exists(filePath)) {
                        Directory.CreateDirectory(filePath);
                    }
                       // This will combine the image name and path to save it locally 
                       var ProfileImage =  Path.Combine(filePath, fileName);
                       using (var saveImage = new FileStream(ProfileImage , FileMode.Create))
                        {
                            Image.CopyTo(saveImage);
                        }
                       // In The Above code we are creating and storing our image locally too
                        var UserRecords = new UserModel()
                        {
                            Name        = userData.Name,
                            User_name   = userData.User_name,
                            Email       = userData.Email,
                            Password    = hashedPassword,
                            created_at  = createdDate,
                            Image       = fileName,

                         };
                        await Database.User.AddAsync(UserRecords);
                        var saveUser = await Database.SaveChangesAsync();
                        if (saveUser > 0) // This will check if any record has been save if yes then the success message or else the error
                        {
                            string emailVerifyLink = Url.Action("Verify_Email", "Auth", new { id = UserRecords.Id }, Request.Scheme);
                            string EmailBody       = $"Use this code to verify your account Email: '{userData.Email}' <a href=\"{emailVerifyLink}\">Verify</a>";
                            string Subject         = "Email Verification";
                            await mailServer.Mail(userData.Email, Subject, EmailBody);
                            TempData["Success"] = "User Information has been saved Please Verify Your email using the link we sent.";
                            return RedirectToAction("Login");
                        }
                        else
                        {
                            TempData["Error"] = "Failed to save your information";
                            return RedirectToAction("Registeration");
                        }

            }
           // Upload User information without Image
           else
            {
                var UserRecords = new UserModel()
                {
                    Name        = userData.Name,
                    User_name   = userData.User_name,
                    Email       = userData.Email,
                    Password    = hashedPassword,
                    created_at  = createdDate,
                  
                };

              await Database.User.AddAsync(UserRecords);
              var saveUser = await Database.SaveChangesAsync();       
              if(saveUser > 0) // This will check if any record has been save if yes then the success message or else the error
                {
                    TempData["Success"] = "User Information has been saved";
                    return RedirectToAction("Registeration");
                }
              else
               {
                    TempData["Error"] = "Failed to save your information";
                    return RedirectToAction("Registeration");
                }
            }

        }
        // Login Function for users
        public IActionResult Login()
        {
            return View("Views/User/Login.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult>Authentication(UserModel userData)
        {
            var user = await Database.User.FirstOrDefaultAsync(u => u.Email == userData.Email);
            var hashedPassword = BCrypt.Net.BCrypt.Verify(userData.Password, user.Password);
            if (user != null && hashedPassword == true)
            {
                if (user.email_verified_at == null)
                {
                    TempData["Error"] = "Please Verify Your Email "+userData.Email+" We have sent a link to your mail";
                    return RedirectToAction("Login");
                }
                else
                {
                    if (user.Role == 0)
                    {
                        return RedirectToAction("Index" , "Home");
                    }
                    if (user.Role == 1)
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                }
            }
            
            TempData["Error"] = "Invalid Credientals";
            return RedirectToAction("Login");
            
            
             


            


        }
        // Function to verify Email Address
        public async Task<IActionResult> Verify_Email(UserModel user , int id)
        {
            var userData = await Database.User.FirstOrDefaultAsync(u => u.Id == id);
            var currentData = Convert.ToString(DateTime.UtcNow);

            if (userData != null)
            {
                userData.email_verified_at = currentData;
                await Database.SaveChangesAsync();
                TempData["Success"] = "Your "+userData.Email+" Has been Verified! You can now loggin using this email.";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["Error"] = "Failed to Verify Your Email Address";
                return RedirectToAction("Login");
            }
        }

    }
   
   
}
