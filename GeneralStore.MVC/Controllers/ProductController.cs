using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _dB = new ApplicationDbContext();

        // GET: Product
        public ActionResult Index()
        {
            List<Product> productList = _dB.Products.ToList();
            List<Product> orderedList = productList.OrderBy(prod => prod.Name).ToList();
            return View(orderedList);
        }

        //Get: Product

        public ActionResult Create()
        {
            return View();
        }

        //POST: Product

        [HttpPost]

        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _dB.Products.Add(product);
                _dB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        //GET: Delete
        //Product/Delete/{id}

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Product product = _dB.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        //POST: Product/Delete/{id}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(int id)
        {
            //Find product in the DB by ID
            Product product = _dB.Products.Find(id);

            //Remove the product 
            _dB.Products.Remove(product);

            //Save the changes to the table with the product removed 
            _dB.SaveChanges();

            //As a confirmation action it would return you to the Index page 
            return RedirectToAction("Index");
        }

        //GET: Product/Edit/{id}

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Product product = _dB.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //POST: Product/Edit/{id}

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _dB.Entry(product).State = EntityState.Modified;
                _dB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product); 
        }

        //GET: Product/Details/{id}

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = _dB.Products.Find(id); 

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product); 
        }

    }
}