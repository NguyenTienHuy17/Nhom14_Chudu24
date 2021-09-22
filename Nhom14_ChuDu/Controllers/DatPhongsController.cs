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
    public class DatPhongsController : Controller
    {
        private ChuDu24 db = new ChuDu24();

        // GET: DatPhongs
        public ActionResult Index(int? page)
        {
            var datPhongs = db.DatPhongs.Include(d => d.PhongK);

            datPhongs = datPhongs.OrderBy(d => d.IDDatPhong);
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(datPhongs.ToPagedList(pageNumber, pageSize));
        }

        // GET: DatPhongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatPhong datPhong = db.DatPhongs.Find(id);
            if (datPhong == null)
            {
                return HttpNotFound();
            }
            return View(datPhong);
        }

        // GET: DatPhongs/Create
        public ActionResult Create()
        {
            ViewBag.IDPhongKS = new SelectList(db.PhongKS, "IDPhongKS", "Ten");
            return View();
        }

        // POST: DatPhongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDDatPhong,IDPhongKS,Email,SDT,ThoiGian,SoLuong")] DatPhong datPhong)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.DatPhongs.Add(datPhong);
                    db.SaveChanges();
                }
                    return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu !" + ex.Message;
                ViewBag.IDPhongKS = new SelectList(db.PhongKS, "IDPhongKS", "Ten", datPhong.IDPhongKS);
                return View(datPhong);
            }
        }

        // GET: DatPhongs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatPhong datPhong = db.DatPhongs.Find(id);
            if (datPhong == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPhongKS = new SelectList(db.PhongKS, "IDPhongKS", "Ten", datPhong.IDPhongKS);
            return View(datPhong);
        }

        // POST: DatPhongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDDatPhong,IDPhongKS,Email,SDT,ThoiGian,SoLuong")] DatPhong datPhong)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(datPhong).State = EntityState.Modified;
                    db.SaveChanges();
                }
                    return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu !" + ex.Message;
                ViewBag.IDPhongKS = new SelectList(db.PhongKS, "IDPhongKS", "Ten", datPhong.IDPhongKS);
                return View(datPhong);
            }
        }

        // GET: DatPhongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatPhong datPhong = db.DatPhongs.Find(id);
            if (datPhong == null)
            {
                return HttpNotFound();
            }
            return View(datPhong);
        }

        // POST: DatPhongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DatPhong datPhong = db.DatPhongs.Find(id);
            try
            {
                db.DatPhongs.Remove(datPhong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không được xóa bản ghi này !" + ex.Message;
                return View("Delete", datPhong);
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
