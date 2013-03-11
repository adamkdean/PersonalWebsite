using PersonalWebsite.Models.Contact;
using PoliteCaptcha;
using System.Net.Mail;
using System.Web.Mvc;

namespace PersonalWebsite.Controllers
{
    public partial class ContactController : Controller
    {
        //
        // GET: /Contact/

        public virtual ActionResult Index()
        {
            return View();
        }

        #region ChildActions
        [ChildActionOnly]
        public virtual PartialViewResult ContactForm()
        {            
            return PartialView();
        }

        [HttpPost, ValidateSpamPrevention]
        [ChildActionOnly]
        public virtual PartialViewResult ContactForm(ContactFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var message = new MailMessage();
                    message.To.Add(new MailAddress("adamkdean@gmail.com", "Adam K Dean"));
                    message.From = new MailAddress("adamkdean.website@gmail.com", "Adam K Dean (Website)");
                    message.Subject = "Website contact form message";
                    message.Body = string.Format("Name: {0}\n\nEmail: {1}\n\nMessage:\n\n{2}",
                        model.Name, model.Email, model.Message);

                    var client = new SmtpClient();
                    client.EnableSsl = true;
                    client.Send(message);

                    model.Success = true;
                }
                catch (SmtpException ex)
                {
                    model.ErrorMessage = ex.Message;                    
                }
            }

            return PartialView(model);
        }

        [ChildActionOnly]
        public virtual PartialViewResult TwitterFeed()
        {
            return PartialView();
        }
        #endregion

    }
}
