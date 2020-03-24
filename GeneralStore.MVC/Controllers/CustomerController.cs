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
    public class CustomerController : Controller
    {
        private ApplicationDbContext _dB = new ApplicationDbContext(); 
        
        // GET: Customer
        public ActionResult Index()
        {
            List<Customer> customerList = _dB.Customers.ToList();
            List<Customer> orderedList = customerList.OrderBy(customer => customer.FirstName).ToList();
            return View(orderedList);
        }

        //GET: Customer (create)

        public ActionResult Create()
        {
            return View();
        }

        //Post: Customer

        [HttpPost]

        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _dB.Customers.Add(customer);
                _dB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        //GET: Delete
        //Customer/Delete/{id}

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Customer customer = _dB.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(int id)
        {
         
            Customer customer = _dB.Customers.Find(id);
            _dB.Customers.Remove(customer);
            _dB.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Customer/Edit/{id}

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Customer customer = _dB.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //POST: Customer/Edit/{id}

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _dB.Entry(customer).State = EntityState.Modified;
                _dB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        //GET: Customer/Details/{id}

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = _dB.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

    }
}