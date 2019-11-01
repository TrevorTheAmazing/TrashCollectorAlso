using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollectorAlso.Models;

namespace TrashCollectorAlso.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        //member variables
        ApplicationDbContext db;

        //constructor
        public EmployeesController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Employees
        public ActionResult Index()
        {
            //if (User.IsInRole("Employee"))
            try
            {
                //get the employee's user employee id
                //string tempUserId = User.Identity.GetUserId();//tlc
                //var employeesZipCode = db.Employees.Where(e => e.ApplicationId == tempUserId).Single();
                //var customersInZipCode = db.Customers.Where(c => c.zip == employeesZipCode.zip);//tlc*

                //var days = DateTimeFormatInfo.InvariantInfo.DayNames.ToList();
                //int today = days.IndexOf(System.DateTime.Today.DayOfWeek.ToString());
                //var employeePickupsForToday = customersInZipCode.Where(cz => cz.weeklyPickupDay == today);

                //return View(employeePickupsForToday);
                //return RedirectToAction("EmpCustIndex", employeePickupsForToday);
                var employees = db.Employees.Select(c => c);
                return View(employees);
            }
            catch
            {
                var employees = db.Employees.Select(c => c);
                return View(employees);
            }

            
        }

        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            var employeeDetails = db.Employees.Where(c => c.Id == id).Single();
            return View(employeeDetails);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            Employee newEmployee = new Employee();
            return View(newEmployee);
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(Employee employeeIn)
        {
            try
            {
                // TODO: Add insert logic here
                db.Employees.Add(employeeIn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            //if (isEmployeeUser())
            //{//tlc
            //    return RedirectToAction("Edit", "Customers", new { Id = id });
            //}
            var employee = db.Employees.Where(e => e.Id == id).Single();
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee employeeIn)
        {
            Employee employeeFromDb = null;
            try
            {
                // TODO: Add update logic here
                var tempEmployeeFromDb = db.Employees.Where(e => e.Id == employeeIn.Id).Single();
                employeeFromDb = tempEmployeeFromDb;
                employeeFromDb.firstName = employeeIn.firstName;
                employeeFromDb.lastName = employeeIn.lastName;
                employeeFromDb.zip = employeeIn.zip;
                //employeeFromDb.ApplicationId = employeeIn.ApplicationId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
