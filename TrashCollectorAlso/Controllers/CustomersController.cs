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
            var customersIndexView = db.Customers.Select(c => c);

            if (User.IsInRole("Customer"))
            {
                //string custAppId = User.Identity.GetUserId(); //this is good 1/3
                //var tempCust = db.Customers.Where(d => d.ApplicationId == custAppId).Single(); //this is good 2/3
                //return RedirectToAction("Details", tempCust.Id); //this is good 3/3
                //return RedirectToAction("Details", new { Id = tempCust.Id });
                //return RedirectToAction("Customers", new { Id = id });
                return RedirectToAction("Details", new { Id = customersIndexView.FirstOrDefault().Id });
            }
            
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
        public ActionResult Edit(int id, Customer newCustomerInfo)
        {
            Customer customerFromDb = null;
            try
            {
                // TODO: Add update logic here
                //var tempCustomerFromDb = db.Customers.Where(c => c.Id == customerToEdit.Id).Single();
                //customerFromDb = tempCustomerFromDb;

                customerFromDb = db.Customers.Where(c => c.Id == newCustomerInfo.Id).Single();

                customerFromDb.firstName = newCustomerInfo.firstName;
                customerFromDb.lastName = newCustomerInfo.lastName;

                customerFromDb.address1 = newCustomerInfo.address1;
                customerFromDb.address2 = newCustomerInfo.address2;
                customerFromDb.city = newCustomerInfo.city;
                customerFromDb.state = newCustomerInfo.state;
                customerFromDb.zip = newCustomerInfo.zip;

                customerFromDb.weeklyPickupDay = newCustomerInfo.weeklyPickupDay;

                customerFromDb.pickupConfirmed = newCustomerInfo.pickupConfirmed;

                if (!customerFromDb.pickupConfirmed && newCustomerInfo.pickupConfirmed)
                {
                    customerFromDb.balance = newCustomerInfo.balance;
                }
                else
                {
                    //do not update the balance
                }
                

                //customerFromDb.monthlyCharge = customerToEdit.monthlyCharge;
                customerFromDb.serviceIsSuspended = newCustomerInfo.serviceIsSuspended;
                customerFromDb.serviceStartDate = newCustomerInfo.serviceStartDate.Date;
                customerFromDb.serviceEndDate = newCustomerInfo.serviceEndDate.Date;

                customerFromDb.pickupCharge = newCustomerInfo.pickupCharge;
                customerFromDb.extraPickupRequested = newCustomerInfo.extraPickupRequested;
                customerFromDb.extraPickupDate = newCustomerInfo.extraPickupDate.Date;
                customerFromDb.extraPickupConfirmed = newCustomerInfo.extraPickupConfirmed;
                customerFromDb.extraPickupCharge = newCustomerInfo.extraPickupCharge;



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