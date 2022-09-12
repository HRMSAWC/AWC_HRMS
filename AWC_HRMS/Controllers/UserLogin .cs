using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using AWC_HRMS.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Policy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;
using System;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;
using ForgotPasswordModel = AWC_HRMS.Models.ForgotPasswordModel;

namespace AWC_HRMS_Web.Controllers
{
    public class UserLogin : Controller
    {
        [HttpGet]
        public IActionResult CheckLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckLogin(UserMasterVM userMastervm)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            int i;

            if (userMastervm == null)
            {
                return NotFound();

            }
            else
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    SqlCommand check = new SqlCommand("Usp_Check_User_Login", con);
                    check.CommandType = CommandType.StoredProcedure;
                    check.Parameters.AddWithValue("@username", userMastervm.UserMaster.UserName);
                    check.Parameters.AddWithValue("@Password", userMastervm.UserMaster.Password);
                    using (var reader = check.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            i = Convert.ToInt32(reader["Result"]);
                            ViewData["Res"] = i;
                        }
                    }
                }

            }

            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
                IConfiguration configuration = builder.Build();
                string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
                int i;
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_UserEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@User_Name", model.User_Name);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            i = Convert.ToInt32(reader["Result"]);
                            if (i == 1)
                            {
                                sendmail(model.User_Name, model.Email);
                            }
                        }
                    }
                    con.Close();
                }

            }
            return View();
        }

        public void sendmail(String name, string Email)
        {
            

            string br = "";
            br += "<br>";
            string body = "";

            body += "<p>" + "Hello" + " " + name + " " + "Greetings of the Day, " + br + "This is auto generated email, Please Click on the Link to regenerate your password " + br + "https://localhost:7292/UserLogin/ForgotPassword " + " " + br + "" + br + "Thankyou."
+ "</p>";

            MailAddress from = new MailAddress("akashchoudhary199404@gmail.com");
            MailAddress to = new MailAddress(Email);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Reset Password Link";
            message.IsBodyHtml = true;
            message.Body = body;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new System.Net.NetworkCredential("akashchoudhary199404@gmail.com", "ogqhffxlkwmmwiyv"),
                EnableSsl = true
            };

            try
            {
                client.Send(message);
                Console.WriteLine("Mail Sent");
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }



        }


    }
}

