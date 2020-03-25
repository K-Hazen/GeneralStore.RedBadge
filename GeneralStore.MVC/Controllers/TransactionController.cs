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
    public class TransactionController : Controller
    {
        private ApplicationDbContext _dB = new ApplicationDbContext(); 
       
        // GET: Transaction
        public ActionResult Index()
        {
            List<Transaction> transactionList = _dB.Transactions.ToList();
            List<Transaction> orderedList = transactionList.OrderBy(trans => trans.TransactionID).ToList(); 
            return View(orderedList);
        }

        // GET: Transaction/Create

        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(_dB.Customers, "CustomerID", "FullName");
            ViewBag.ProductID = new SelectList(_dB.Products, "ProductID", "Name");
            return View();
        }

        //POST: Product

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var product = _dB.Products.Find(transaction.ProductID);
                if(product.InventoryCount < 1)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                product.InventoryCount--;

                _dB.Transactions.Add(transaction);
                _dB.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(_dB.Customers, "CustomerID", "FullName", transaction.CustomerID);
            ViewBag.ProductID = new SelectList(_dB.Products, "ProductID", "Name", transaction.ProductID);
            
            return View(transaction);
        }

        //GET: Delete
        //Transaction/Delete/{id}

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Transaction transaction = _dB.Transactions.Find(id);

            if (transaction == null)
            {
                return HttpNotFound();
            }

            return View(transaction);
        }

        //POST: Transaction/Delete/{id}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(int id)
        {
            Transaction transaction = _dB.Transactions.Find(id);
            _dB.Transactions.Remove(transaction);
            _dB.SaveChanges();
            return RedirectToAction("Index");
        }


        //GET: Transaction/Edit/{id}

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Transaction transaction = _dB.Transactions.Find(id);

            if (transaction == null)
            {
                return HttpNotFound();
            }

            ViewBag.CustomerID = new SelectList(_dB.Customers, "CustomerID", "FullName");
            ViewBag.ProductID = new SelectList(_dB.Products, "ProductID", "Name");

            return View(transaction);
        }

        //POST: Transaction/Edit/{id}

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _dB.Entry(transaction).State = EntityState.Modified;
                _dB.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(_dB.Customers, "CustomerID", "FullName", transaction.CustomerID);
            ViewBag.ProductID = new SelectList(_dB.Products, "ProductID", "Name", transaction.ProductID);

            return View(transaction);
        }

        //GET: Product/Details/{id}

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Transaction transaction = _dB.Transactions.Find(id);

            if (transaction == null)
            {
                return HttpNotFound();
            }

            return View(transaction);
        }

    }
}