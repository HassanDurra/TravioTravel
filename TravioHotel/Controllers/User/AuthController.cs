﻿using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using TravioHotel.DataContext;
using TravioHotel.Models;
using TravioHotel.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace TravioHotel.Controllers
{
    public class AuthController : Controller
    {
        // Creating Database Context so we can connect with database then insert our records
        public readonly DatabaseContext Database;
        public readonly IWebHostEnvironment fileEnvironment;
        public readonly MailServer mailServer;
        public readonly RandomGenerate random;
        public readonly IHttpContextAccessor httpContext;
        public  AuthController(DatabaseContext Database , IHttpContextAccessor _httpContext  , IWebHostEnvironment _fileEnvironment , MailServer _mailServer , RandomGenerate _random)
        {
            this.Database        = Database;
            this.fileEnvironment = _fileEnvironment;
            this.mailServer      = _mailServer;
            this.random          = _random;
            this.httpContext     = _httpContext;
            
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
                        var UserData = new
                        {

                            id      = user.Id,
                            name    = user.Name,
                            email   = user.Email,
                            image   = user.Image,
                            status  = user.Status,

                        }; // This will the array of our data to be stored in Session
                        string userDataJson = JsonConvert.SerializeObject(UserData); // We Will Get The Data in Json Format
                        httpContext.HttpContext.Session.SetString("user", userDataJson); // Then we will store it to session
                    
                        return RedirectToAction("Index" , "Home");
                    }
                    if (user.Role == 1)
                    {
                        var UserData = new
                        {

                            id     = user.Id,
                            name   = user.Name,
                            email  = user.Email,
                            image  = user.Image,
                            status = user.Status,

                        }; // This will the array of our data to be stored in Session
                        string userDataJson = JsonConvert.SerializeObject(UserData); // We Will Get The Data in Json Format
                        httpContext.HttpContext.Session.SetString("admin", userDataJson); // Then we will store it to session
                        return RedirectToAction("Admin", "Dashboard");
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
        // Reset Password Section
        public IActionResult ResetPassword()
        {
            return View("Views/User/ResetPassword.cshtml");
        }
        public async Task<IActionResult> Check_email(string Email)
        {
            var userData         = await Database.User.FirstOrDefaultAsync(u => u.Email == Email);
            var verificationData = await Database.Verification.FirstOrDefaultAsync(v => v.Verification_email == Email);

            if(verificationData != null)
            {
                Database.Verification.Remove(verificationData);
                await Database.SaveChangesAsync();
            }
            var VerificationCode = random.NumberGenerate(6);
            if (userData != null)
            {
                var createVerification = new VerificationModel
                {
                    Verification_email = Email,
                    Verification_code  = VerificationCode 
                };
                await Database.Verification.AddAsync(createVerification);
                var storeCode = await Database.SaveChangesAsync();
                // Sending Email with verification code
               
                if(storeCode > 0 )
                {
                    var EmailBody = $"use this code to verify your account " + VerificationCode + "";
                    var Subject   = "Email Verification Code";
                    var sendEmail = mailServer.Mail(Email, Subject, EmailBody);
                    var message   = new { message = "Success" };
                    return Json(message);
                }
                else
                {
                    var message = new { message = "Error" };
                    return Json(message);
                }
            }
            var response = new { message = "no records"};
            return Json(response) ;
;        } // this function was used to send verification code to user email

        // To verify The Code 
        public async Task<IActionResult> Verify_Code(VerificationModel verification_table ,  string Verification)
        {
            var verificationData = await Database.Verification.FirstOrDefaultAsync(u => u.Verification_code == Verification);
            if(verificationData != null)
            {
                Database.Verification.Remove(verificationData);
                await Database.SaveChangesAsync();
                var response = new { message = "Success" };
                return Json(response);
            }

            var message = new { message = "Error" };
            return Json(message);
        } 
        // This function will reset the password 
        public async Task<IActionResult> Reset_password( string Password , string Email)
        {
            var userData = await Database.User.FirstOrDefaultAsync(u => u.Email == Email);
            if(userData != null)
            {
                var HashedPassword  = BCrypt.Net.BCrypt.HashPassword(Password);
                userData.Password   = HashedPassword;
                var passwordReseted = await Database.SaveChangesAsync();
                if(passwordReseted > 0)
                {
                    var currentData = Convert.ToString(DateTime.UtcNow);
                    var Subject     = "Your Password Was Reset";
                    var EmailBody   = $"Your Password Reseted On :" + currentData + " ";
                    await           mailServer.Mail(Email, Subject, EmailBody);
                    var response    = new {message = "Success"};
                    return Json(response);
                }
                
            }
            var message = new { message = "Error" };

            return Json(message);
        }
        public IActionResult User_Logout()
        {
            httpContext.HttpContext.Session.Remove("user");
            return RedirectToAction("Index" , "Home");
        }
        
        } // The Password Code Reseted 

    }
   
   

