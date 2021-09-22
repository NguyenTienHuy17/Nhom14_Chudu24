using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nhom14_ChuDu.Models;
using PagedList;

namespace Nhom14_ChuDu.Controllers
{
    public class ChiTietKhuyenMaisController : Controller
    {
        private ChuDu24 db = new ChuDu24();

        // GET: ChiTietKhuyenMais
        public ActionResult Index(int? page)
        {
            var chiTietKhuyenMais = db.ChiTietKhuyenMais.Include(c => c.KhuyenMai).Include(c => c.PhongK);

            chiTietKhuyenMais = chiTietKhuyenMais.OrderBy(c => c.IDKhuyenMai);
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(chiTietKhuyenMais.ToPagedList(pageNumber, pageSize));
        }

        // GET: ChiTietKhuyenMais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietKhuyenMai chiTietKhuyenMai = db.ChiTietKhuyenMais.Find(id);
            if (chiTietKhuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(chiTietKhuyenMai);
        }

        // GET: ChiTietKhuyenMais/Create
        public ActionResult Create()
        {
            ViewBag.IDKhuyenMai = new SelectList(db.KhuyenMais, "IDKhuyenMai", "Ten");
            ViewBag.IDPhongKS = new SelectList(db.PhongKS, "IDPhongKS", "Ten");
            return View();
        }

        // POST: ChiTietKhuyenMais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDKhuyenMai,IDPhongKS,Gia,ThoiGian")] ChiTietKhuyenMai chiTietKhuyenMai)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ChiTietKhuyenMais.Add(chiTietKhuyenMai);
                    db.SaveChanges();
                }
                    return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;
                ViewBag.IDKhuyenMai = new SelectList(db.KhuyenMais, "IDKhuyenMai", "Ten", chiTietKhuyenMai.IDKhuyenMai);
                ViewBag.IDPhongKS = new SelectList(db.PhongKS, "IDPhongKS", "Ten", chiTietKhuyenMai.IDPhongKS);
                return View(chiTietKhuyenMai);
            }
        }

        // GET: ChiTietKhuyenMais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietKhuyenMai chiTietKhuyenMai = db.ChiTietKhuyenMais.Find(id);
            if (chiTietKhuyenMai == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDKhuyenMai = new SelectList(db.KhuyenMais, "IDKhuyenMai", "Ten", chiTietKhuyenMai.IDKhuyenMai);
            ViewBag.IDPhongKS = new SelectList(db.PhongKS, "IDPhongKS", "Ten", chiTietKhuyenMai.IDPhongKS);
            return View(chiTietKhuyenMai);
        }

        // POST: ChiTietKhuyenMais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDKhuyenMai,IDPhongKS,Gia,ThoiGian")] ChiTietKhuyenMai chiTietKhuyenMai)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(chiTietKhuyenMai).State = EntityState.Modified;
                    db.SaveChanges();
                }
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;
                ViewBag.IDKhuyenMai = new SelectList(db.KhuyenMais, "IDKhuyenMai", "Ten", chiTietKhuyenMai.IDKhuyenMai);
                ViewBag.IDPhongKS = new SelectList(db.PhongKS, "IDPhongKS", "Ten", chiTietKhuyenMai.IDPhongKS);
                return View(chiTietKhuyenMai);
            }
        }

        // GET: ChiTietKhuyenMais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietKhuyenMai chiTietKhuyenMai = db.ChiTietKhuyenMais.Find(id);
            if (chiTietKhuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(chiTietKhuyenMai);
        }

        // POST: ChiTietKhuyenMais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietKhuyenMai chiTietKhuyenMai = db.ChiTietKhuyenMais.Find(id);
            try
            {
                db.ChiTietKhuyenMais.Remove(chiTietKhuyenMai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Không được xóa bản ghi này!" + ex.Message;
                return View("Delete",chiTietKhuyenMai);
            }
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
