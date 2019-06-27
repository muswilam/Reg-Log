using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCRegisterationAndLogin.Models;
using MVCRegisterationAndLogin.Common;
using System.Net.Mail;
using System.Net;

namespace MVCRegisterationAndLogin.Controllers
{
    public class UserController : Controller
    {
        //Registration action
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        //registration post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")]User user)
        {
            bool Status = false;
            string Message = "";

            //model validation 
            if(ModelState.IsValid)
            {
                #region email is exist
                bool isExist = IsEmailExist(user.Email);
                if(isExist)
                {
                    ModelState.AddModelError("Email_Exist", "Email is already exist");
                    return View(user);
                }
                #endregion
                 
                #region generate activation code
                user.ActivationCode = Guid.NewGuid();
                #endregion

                #region password hashing
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPass = Crypto.Hash(user.ConfirmPass); //this line for DbContext validation again .. prevent issue
                #endregion

                user.IsEmailVerified = false;

                #region save to database
                using(LoginContext context = new LoginContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();

                    //send email to user
                    SendVerificationlinkEmail(user.Email, user.ActivationCode.ToString());
                    Message = "Registration successfully done , account activation link " +
                        "has been sent to your email" + user.Email;
                    Status = true;
                }
                #endregion
            }
            else
            {
                Message = "Invalid Request"; 
            }

            ViewBag.Message = Message;
            ViewBag.Status = Status;

            return View(user);
        }


        //verify email 

        //verfy email link

        //login 

        //login post

        //logout

        [NonAction]
        public bool IsEmailExist(string emailId)
        {
            using(LoginContext context = new LoginContext())
            {
                var emailExist = context.Users.Where(e => e.Email == emailId).FirstOrDefault();
                return emailExist != null;
            }
        }

        [NonAction]
        public void SendVerificationlinkEmail(string email , string activationCode)
        {
            var verifyUrl = "/User/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            // ****************** change mail and pass to yours before any debug ***************************
            var fromEmail = new MailAddress("***********@gmail.com", "Sully");
            var toEmail = new MailAddress(email);
            var fromPass = "**********";
            string subject = "Your account is successfully created";

            string body = "<br/><br/> We 're excited to tell you that your account is successfully created." +
                " please check the link below to verify your account"+
                "<br/><br/> <a href='"+link+"'>"+link+"</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address,fromPass)
            };

            using (var messaage = new MailMessage(fromEmail,toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            })

            smtp.Send(messaage);
        }
    }
}