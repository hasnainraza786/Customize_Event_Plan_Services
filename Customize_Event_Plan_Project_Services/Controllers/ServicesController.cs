using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Customize_Event_Plan_Project_Services.Models;
using Customize_Event_Plan_Project_Services.ViewModel;

namespace Customize_Event_Plan_Project_Services.Controllers
{
    public class ServicesController : Controller
    {
        Event_plan_ProjectEntities db = new Event_plan_ProjectEntities();
        string event_type, Date, Name, Email ;
        int NO_OF_PPL, phone;



        public ActionResult Main()
        {
            return View();
        }


        //Event Plan Start Here
        [HttpGet]
        public  ActionResult Event_Plan()
        {
            return View();
        }
        

        // Details Of Venue and Selection of Venue 
        [HttpPost]
        public ActionResult Venue_Details(string Event_type, int No_of_People, string Date_of_event)
        {
            event_type = Event_type; NO_OF_PPL = No_of_People; Date = Date_of_event;
            

            var event_selected = db.Venue_Detail.SqlQuery("SELECT * FROM Venue_Detail where Venue_Type=@p0 AND Venue_Capacity=@p1", event_type,NO_OF_PPL).ToList();

            return View(event_selected);
        }
        public ActionResult Venue_Select(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue_Detail ven = db.Venue_Detail.Find(id);
            if (ven == null)
            {
                return HttpNotFound();
            }

            return View(ven);
        }
        [HttpPost]
        public ActionResult Venue_Select(Venue_Detail venue_Detail)
        {


            db.Entry(venue_Detail).State = EntityState.Modified;
            db.SaveChanges();


            return View(venue_Detail);
        }










        //Details Of Menu and Selection Of Menu
        [HttpPost]
        public ActionResult Menu(string a)
        {
            
            var event_selected = db.Menu_Detail.SqlQuery("select *from Menu_Detail").ToList();

            return View(event_selected);
        }
        public ActionResult Menu_Select(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu_Detail ven = db.Menu_Detail.Find(id);
            if (ven == null)
            {
                return HttpNotFound();
            }

            return View(ven);
        }
        [HttpPost]
        public ActionResult Menu_Select(Menu_Detail menu_Detail)
        {


            db.Entry(menu_Detail).State = EntityState.Modified;
            db.SaveChanges();


            return View(menu_Detail);
        }






        //Details Of Deco and Selection Of Deco
        [HttpPost]
        public ActionResult Deco(string a)
        {

            var event_selected = db.Deco_Details.SqlQuery("select *from Deco_Details ").ToList();

            return View(event_selected);
        }
        public ActionResult Deco_Select(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deco_Details ven = db.Deco_Details.Find(id);
            if (ven == null)
            {
                return HttpNotFound();
            }

            return View(ven);
        }
        [HttpPost]
        public ActionResult Deco_Select(Deco_Details deco_Details)
        {


            db.Entry(deco_Details).State = EntityState.Modified;
            db.SaveChanges();


            return View(deco_Details);
        }





        //GETTING DETAILS OF CUSTOMER INFO
        public ActionResult Get_Info()
        {
            return View();
        }




        //SAVING INFORMATION OF CUSTOMER INTO DATABASE
        [HttpGet]
        public ActionResult Customer_Info()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Customer_Info([Bind(Include = "Customer_Name, Customer_PhoneNumber, Customer_Email")] Customer_Info customer_Info, string Customer_Name, string Customer_Email, int Customer_Phone)
        {
            if (ModelState.IsValid)
            {
                customer_Info.Customer_Name = Customer_Name; customer_Info.Customer_Email = Customer_Email; customer_Info.Customer_PhoneNumber = Customer_Phone;
                db.Customer_Info.Add(customer_Info);
                db.SaveChanges();

                return RedirectToAction("multidata");
            }
            ViewBag.Rno = new SelectList(db.Customer_Info, "Venue_Name");
            return View(customer_Info);
        }



        //SHOWING THE SUMARRY
        public ActionResult multidata()
        {
            var mymodel = new MultipleTable();
            mymodel.Venue_Detailses = db.Venue_Detail.SqlQuery("select *from Venue_Detail WHERE Venue_Select = 1").ToList();
            mymodel.Menu_Detailses = db.Menu_Detail.SqlQuery("select *from Menu_Detail WHERE Menu_Select = 1").ToList();
            mymodel.Deco_Detailses = db.Deco_Details.SqlQuery("select *from Deco_Details WHERE Deco_Select = 1").ToList();

            

            return View(mymodel);
        }


        public ActionResult Confirm()
        {
            return View();
        }


        //SAVING INFORMATION OF ORDER INTO DATABASE
        [HttpGet]
        public ActionResult Order_Details()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Order_Details([Bind(Include = "Venue_Name, Menu_Name,Deco_Item")] Order_Details order_Details)
        {
            if (ModelState.IsValid)
            {
                order_Details.Venue_Name = "Catholic";
                order_Details.Menu_Name = "Chicken Biryani";
                order_Details.Deco_Item = "Chair";
                order_Details.People = NO_OF_PPL;
                db.Order_Details.Add(order_Details);
                db.SaveChanges();

                return RedirectToAction("Main");
            }
            ViewBag.Rno = new SelectList(db.Order_Details, "Venue_Name");
            return View(order_Details);
        }











        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Venue_Name, Venue_Capacity,Venue_Address, Venue_Price, Venue_Type, Venue_Select")] Venue_Detail venue_Detail)
        {
            if (ModelState.IsValid)
            {
                db.Venue_Detail.Add(venue_Detail);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Rno = new SelectList(db.Venue_Detail, "Venue_Name");
            return View(venue_Detail);
        }

















        [HttpGet]
        public ActionResult Add_Menu()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Menu([Bind(Include = "Menu_Name, Menu_Info,Menu_Price, Menu_Type, Menu_Select")] Menu_Detail menu_Detail)
        {
            if (ModelState.IsValid)
            {
                db.Menu_Detail.Add(menu_Detail);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Rno = new SelectList(db.Menu_Detail, "Venue_Name");
            return View(menu_Detail);
        }










        [HttpGet]
        public ActionResult Add_Deco()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Deco([Bind(Include = "Deco_Item,Deco_Price, Deco_Select")] Deco_Details deco_Details)
        {
            if (ModelState.IsValid)
            {
                db.Deco_Details.Add(deco_Details);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Rno = new SelectList(db.Deco_Details, "Venue_Name");
            return View(deco_Details);
        }
    }
}