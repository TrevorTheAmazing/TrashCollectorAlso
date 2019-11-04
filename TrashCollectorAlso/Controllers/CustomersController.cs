﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrashCollectorAlso.Models;
using TrashCollectorAlso.TrashMaps;
using Newtonsoft.Json;

namespace TrashCollectorAlso.Controllers
{
    [Authorize]//tlc
    public class CustomersController : Controller
    {
        //member variables
        ApplicationDbContext db;
        TrashMap trashMap;

        //constructor
        public CustomersController()
        {
            db = new ApplicationDbContext();
            //trashMap = new TrashMap();
        }

        // GET: Customers
        public ActionResult Index()
        {
            var customersIndexView = db.Customers.Select(c => c);

            if (User.IsInRole("Customer"))
            {
                string custAppId = User.Identity.GetUserId(); //this is good 1/3
                var tempCust = db.Customers.Where(d => d.ApplicationId == custAppId).Single(); //this is good 2/3
                //return RedirectToAction("Details", tempCust.Id); //this is good 3/3
                //return RedirectToAction("Details", new { Id = tempCust.Id });
                //return RedirectToAction("Customers", new { Id = id });
                return RedirectToAction("Details", new { Id = tempCust.Id });
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

                //find the customer
                customerFromDb = db.Customers.Where(c => c.Id == newCustomerInfo.Id).Single();

                //update the customer
                customerFromDb.firstName = newCustomerInfo.firstName;
                customerFromDb.lastName = newCustomerInfo.lastName;

                customerFromDb.address1 = newCustomerInfo.address1;
                customerFromDb.address2 = newCustomerInfo.address2;
                customerFromDb.city = newCustomerInfo.city;
                customerFromDb.state = newCustomerInfo.state;
                customerFromDb.zip = newCustomerInfo.zip;

                customerFromDb.weeklyPickupDay = newCustomerInfo.weeklyPickupDay;

                //customerFromDb.monthlyCharge = customerToEdit.monthlyCharge;
                customerFromDb.serviceIsSuspended = newCustomerInfo.serviceIsSuspended;
                customerFromDb.serviceStartDate = newCustomerInfo.serviceStartDate.Date;
                customerFromDb.serviceEndDate = newCustomerInfo.serviceEndDate.Date;

                customerFromDb.extraPickupRequested = newCustomerInfo.extraPickupRequested;
                customerFromDb.extraPickupDate = newCustomerInfo.extraPickupDate.Date;

                //update the balance
                double tempBalance = customerFromDb.balance;

                if (!customerFromDb.pickupConfirmed && newCustomerInfo.pickupConfirmed)
                {
                    tempBalance += customerFromDb.pickupCharge;
                    customerFromDb.pickupConfirmed = false;
                }

                if (customerFromDb.extraPickupRequested && newCustomerInfo.extraPickupConfirmed)
                {
                    tempBalance += customerFromDb.extraPickupCharge;
                    customerFromDb.extraPickupRequested = false;
                    customerFromDb.extraPickupConfirmed = false;
                }

                customerFromDb.balance = tempBalance;

                //save the changes
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Home");
            }
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            Customer newCustomer = new Customer();
            return View(newCustomer);
        }

        [Authorize(Roles = "Employee,Admin")]
        // POST: Customers/Create
        [HttpPost]

        public ActionResult Create(Customer customerIn)
        {
            try
            {
                db.Customers.Add(customerIn);
                string applicationId = User.Identity.GetUserId();
                customerIn.ApplicationId = applicationId;
                customerIn.extraPickupDate = System.DateTime.Now.Date;
                customerIn.serviceEndDate = System.DateTime.Now.Date;
                customerIn.serviceStartDate = System.DateTime.Now.Date;
                //customerIn.coordinates = trashMap.GetAddressCoordinates(customerIn);
                db.SaveChanges();
                return RedirectToAction("Edit", customerIn.Id.ToString(), customerIn);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            var customerDetails = db.Customers.Where(c => c.Id == id).Single();
            return View(customerDetails);
        }







        // GET: Customers/Details/5
        public async Task<ActionResult> MapIt(int id)
        {
            var customerDetails = db.Customers.Where(c => c.Id == id).Single();

            trashMap = new TrashMap();

            //var tempCoordinates = await trashMap.GetAddressCoordinates(customerDetails);
            var tempCoordinates = await trashMap.GetAddressCoordinates(customerDetails);

            return View(customerDetails);
        }



    }
    


}