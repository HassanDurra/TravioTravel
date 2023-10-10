using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using TravioHotel.DataContext;
using TravioHotel.Models;

namespace TravioHotel.Controllers
{
    public class AuthController : Controller
    {
        // Creating Database Context so we can connect with database then insert our records
        public readonly DatabaseContext Database;
        public readonly IWebHostEnvironment fileEnvironment;
        public  AuthController(DatabaseContext Database  , IWebHostEnvironment _fileEnvironment)
        {
            this.Database = Database;
            this.fileEnvironment = _fileEnvironment;
            
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
                            TempData["Success"] = "User Information has been saved";
                            return RedirectToAction("Registeration");
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
        public IActionResult User_Login()
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
                    TempData["Error"] = "Please Verify Your Email Address We have sent a link to your mail";
                    return RedirectToAction("User_Login");
                }
                else
                {
                    if (user.Role == 0)
                    {
                        return RedirectToAction("Index" , "User");
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


    }
}
