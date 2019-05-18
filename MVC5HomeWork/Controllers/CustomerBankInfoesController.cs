using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5HomeWork.Models;

namespace MVC5HomeWork.Controllers
{
    public class CustomerBankInfoesController : Controller
    {
        private customerEntities db = new customerEntities();

        // GET: CustomerBankInfoes
        public ActionResult Index()
        {
            var customerBankInfo = db.CustomerBankInfo.Include(c => c.CustomerData);
            return View(customerBankInfo.ToList());
        }

        // GET: CustomerBankInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerBankInfo customerBankInfo = db.CustomerBankInfo.Find(id);
            if (customerBankInfo == null)
            {
                return HttpNotFound();
            }
            return View(customerBankInfo);
        }

        // GET: CustomerBankInfoes/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.CustomerData, "Id", "客戶名稱");
            return View();
        }

        // POST: CustomerBankInfoes/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] CustomerBankInfo customerBankInfo)
        {
            if (ModelState.IsValid)
            {
                db.CustomerBankInfo.Add(customerBankInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.CustomerData, "Id", "客戶名稱", customerBankInfo.客戶Id);
            return View(customerBankInfo);
        }

        // GET: CustomerBankInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerBankInfo customerBankInfo = db.CustomerBankInfo.Find(id);
            if (customerBankInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.CustomerData, "Id", "客戶名稱", customerBankInfo.客戶Id);
            return View(customerBankInfo);
        }

        // POST: CustomerBankInfoes/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] CustomerBankInfo customerBankInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerBankInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.CustomerData, "Id", "客戶名稱", customerBankInfo.客戶Id);
            return View(customerBankInfo);
        }

        // GET: CustomerBankInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerBankInfo customerBankInfo = db.CustomerBankInfo.Find(id);
            if (customerBankInfo == null)
            {
                return HttpNotFound();
            }
            return View(customerBankInfo);
        }

        // POST: CustomerBankInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerBankInfo customerBankInfo = db.CustomerBankInfo.Find(id);
            db.CustomerBankInfo.Remove(customerBankInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
