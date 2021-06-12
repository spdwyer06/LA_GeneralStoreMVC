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
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Transaction
        public ActionResult Index()
        {
            var transactions = _db.Transactions.ToList();
            var orderedList = transactions.OrderBy(t => t.Customer.LastName).ToList();

            return View(orderedList);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Create(Transaction transaction)
        {
            if (!ModelState.IsValid)
                return View(transaction);

            _db.Transactions.Add(transaction);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(int? transactionId)
        {
            if (transactionId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var transaction = _db.Transactions.Find(transactionId);

            if (transaction == null)
                return HttpNotFound();

            return View(transaction);
        }

        public ActionResult Edit(int? transactionId)
        {
            if (transactionId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var transaction = _db.Transactions.Find(transactionId);

            if (transaction == null)
                return HttpNotFound();

            return View(transaction);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transaction transaction)
        {
            if (!ModelState.IsValid)
                return View(transaction);

            _db.Entry(transaction).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? transactionId)
        {
            if (transactionId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var transaction = _db.Transactions.Find(transactionId);

            if (transaction == null)
                return HttpNotFound();

            return View(transaction);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int transactionId)
        {
            var transaction = _db.Transactions.Find(transactionId);

            _db.Transactions.Remove(transaction);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}