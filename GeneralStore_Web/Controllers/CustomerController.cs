using GeneralStore_Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore_Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Customer
        public ActionResult Index()
        {
            var customers = _db.Customers.ToList();
            var orderedList = customers.OrderBy(customer => customer.LastName).ToList();

            return View(orderedList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);

            _db.Customers.Add(customer);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(int? customerId)
        {
            if (customerId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var customer = _db.Customers.Find(customerId);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult Edit(int? customerId)
        {
            if (customerId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var customer = _db.Customers.Find(customerId);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);

            _db.Entry(customer).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? customerId)
        {
            if (customerId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var customer = _db.Customers.Find(customerId);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int customerId)
        {
            var customer = _db.Customers.Find(customerId);

            _db.Customers.Remove(customer);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}