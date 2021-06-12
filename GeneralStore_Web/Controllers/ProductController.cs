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
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Product
        public ActionResult Index()
        {
            var products = _db.Products.ToList();
            var orderedList = products.OrderBy(product => product.Name).ToList();

            return View(orderedList);
            //return View(_db.Products.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            _db.Products.Add(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(int? productId)
        {
            if (productId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var product = _db.Products.Find(productId);

            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        public ActionResult Edit(int? productId)
        {
            if (productId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var product = _db.Products.Find(productId);

            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            _db.Entry(product).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? productId)
        {
            if (productId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var product = _db.Products.Find(productId);

            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int productId)
        {
            var product = _db.Products.Find(productId);

            _db.Products.Remove(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}