using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Customize_Event_Plan_Project_Services.Models;

namespace Customize_Event_Plan_Project_Services.Controllers
{
    public class ContactController : Controller
    {
        Event_plan_ProjectEntities db = new Event_plan_ProjectEntities();

        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }




        //SAVING INFORMATION OF CUSTOMER_QUERY INTO DATABASE
        [HttpGet]
        public ActionResult Customer_Query()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Customer_Query([Bind(Include = "Customer_Name, Customer_PhoneNumber,Customer_Email, Customer_Query")] Customer_Query customer_Query , string C_Name, string C_Email, int C_Phone, string C_Query)
        {
            if (ModelState.IsValid)
            {
                customer_Query.Customer_Name = C_Name;
                customer_Query.Customer_Email = C_Email;
                customer_Query.Customer_PhoneNumber = C_Phone;
                customer_Query.Customer_Query1 = C_Query;
                db.Customer_Query.Add(customer_Query);
                db.SaveChanges();

                return RedirectToAction("Contact");
            }
            ViewBag.Rno = new SelectList(db.Customer_Query, "Venue_Name");
            return View(customer_Query);
        }
    }
}