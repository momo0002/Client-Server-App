using WebApp.Models;
using System.Web.Mvc;
using WebApp.DAL;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home		WebApp.Models.MessageModel.Message.get returned	"Gizmo"	string

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(MessageDBModel model)
        {
            if (model != null)
            {
                using ( var db = new MessageDBContext())
                {
                    try
                    {
                        db.Messages.Add(model);
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        Debug.WriteLine("Saving Messages into db Error 1: " + e.EntityValidationErrors);
                    }
                }
                return View(model);
            }
            else
            {
                return Json("An Error Has occoured");
            }
        }

        public ActionResult ListMessages()
        {
            using (var db = new MessageDBContext())
            {
                var records = new MessageList<MessageDBModel>();
                records.Content = db.Messages.OrderBy(x => x.time).ToList();

                return PartialView("ListMessages", records.Content);
            }
        }     
    }
}