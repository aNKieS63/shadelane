using Microsoft.AspNetCore.Mvc;
using prjwwwShadeLane.Models;
using System.Net.Mail;

namespace prjwwwShadeLane.Controllers
{
    
    public class CustomizeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(CustomizeSignRequest model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mail = new MailMessage
                    {
                        From = new MailAddress("youremail@example.com"),
                        Subject = "Custom Neon Sign Request",
                        Body = $"Name: {model.Name}\nEmail: {model.Email}\nMessage: {model.Message}",
                        IsBodyHtml = false
                    };

                    mail.To.Add("businessowner@example.com");

                    using var smtp = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new System.Net.NetworkCredential("youremail@example.com", "yourpassword"),
                        EnableSsl = true
                    };

                    smtp.Send(mail);
                    ViewBag.Message = "Your customization request has been sent!";
                }
                catch
                {
                    ViewBag.Message = "There was an error sending your request. Please try again later.";
                }
            }
            else
            {
                ViewBag.Message = "Please fill out all fields.";
            }

            return View("Index");
        }
    }
}
