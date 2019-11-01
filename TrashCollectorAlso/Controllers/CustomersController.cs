using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrashCollectorAlso.Models;

namespace TrashCollectorAlso.Controllers
{
    [Authorize]//tlc
    public class CustomersController : Controller
    {
        //member variables
        ApplicationDbContext db;

        //constructor
        public CustomersController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Customers
        public ActionResult Index()
        {
            //if (User.IsInRole("Customer"))
            //{
            //string custAppId = User.Identity.GetUserId(); //this is good 1/3
            //var tempCust = db.Customers.Where(d => d.ApplicationId == custAppId).Single(); //this is good 2/3
            //return RedirectToAction("Details", tempCust.Id); //this is good 3/3
            //return RedirectToAction("Details", new { Id = tempCust.Id });
            //return RedirectToAction("Customers", new { Id = id });
            //}
            var customersIndexView = db.Customers.Select(c => c);
            //if (isEmployeeUser())
            //{
            //    //customersIndexView = db.Customers.Where(c=>c.Id == User.Identity.GetUserId()).Select();
            //    return RedirectToAction("Index");
            //}

            return View(customersIndexView);
        }

        public ActionResult Edit(int id)
        {
            var customerEditView = db.Customers.Where(c => c.Id == id).Single();
            return View(customerEditView);
        }

        [HttpPost]
        public ActionResult Edit(int id, Customer customerToEdit)
        {
            Customer customerFromDb = null;
            try
            {
                // TODO: Add update logic here
                var tempCustomerFromDb = db.Customers.Where(c => c.Id == customerToEdit.Id).Single();
                customerFromDb = tempCustomerFromDb;
                customerFromDb.firstName = customerToEdit.firstName;
                customerFromDb.lastName = customerToEdit.lastName;

                customerFromDb.address1 = customerToEdit.address1;
                customerFromDb.address2 = customerToEdit.address2;
                customerFromDb.city = customerToEdit.city;
                customerFromDb.state = customerToEdit.state;
                customerFromDb.zip = customerToEdit.zip;

                customerFromDb.weeklyPickupDay = customerToEdit.weeklyPickupDay;
                customerFromDb.balance = customerToEdit.balance;

                //customerFromDb.monthlyCharge = customerToEdit.monthlyCharge;
                customerFromDb.serviceIsSuspended = customerToEdit.serviceIsSuspended;
                customerFromDb.serviceStartDate = customerToEdit.serviceStartDate.Date;
                customerFromDb.serviceEndDate = customerToEdit.serviceEndDate.Date;
                customerFromDb.pickupConfirmed = customerToEdit.pickupConfirmed;
                customerFromDb.pickupCharge = customerToEdit.pickupCharge;
                customerFromDb.extraPickupRequested = customerToEdit.extraPickupRequested;
                customerFromDb.extraPickupDate = customerToEdit.extraPickupDate.Date;
                customerFromDb.extraPickupConfirmed = customerToEdit.extraPickupConfirmed;
                customerFromDb.extraPickupCharge = customerToEdit.extraPickupCharge;



                //customerFromDb.ApplicationId = customerToEdit.ApplicationId;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            Customer newCustomer = new Customer();
            return View(newCustomer);
        }

        // POST: Customers/Create
        [HttpPost]

        public ActionResult Create(Customer customerIn)
        {
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            //string applicationId1 = User.Identity.GetUserId();

            // if (roleManager.FindById(applicationId1).Name == "Customer") ;
            // {
         
                //string applicationId = User.Identity.GetUserId();
                // TODO: Add insert logic here
                //customerIn.ApplicationId = applicationId;

                db.Customers.Add(customerIn);
                customerIn.extraPickupDate = System.DateTime.Now.Date;
                customerIn.serviceEndDate = System.DateTime.Now.Date;
                customerIn.serviceStartDate = System.DateTime.Now.Date;
                db.SaveChanges();
                return RedirectToAction("Index");
                //return RedirectToAction("Details", customerIn.Id);
            
            //}
            //return View();
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            var customerDetails = db.Customers.Where(c => c.Id == id).Single();
            return View(customerDetails);
        }
    }
}