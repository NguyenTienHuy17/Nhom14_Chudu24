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
    public class LoaiKsController : Controller
    {
        private ChuDu24 db = new ChuDu24();

        // GET: LoaiKs
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int ?page)
        {
            //các biến sắp xếp
            ViewBag.CurrentSort = sortOrder;

            ViewBag.SapTheoTen = String.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";

            //lấy giá trị của bộ lọc dữ liệu hiện tại
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var loaiKS = db.LoaiKS.Select(lks => lks);

            //lọc theo tên hàng
            if (!String.IsNullOrEmpty(searchString))
            {
                loaiKS = loaiKS.Where(k => k.Loai.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ten_desc":
                    loaiKS = loaiKS.OrderByDescending(k => k.Loai);
                    break;
                default:
                    loaiKS = loaiKS.OrderBy(k => k.Loai);
                    break;
            }

            //loaiKS = loaiKS.OrderBy(lks => lks.IDLoaiKs);
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(loaiKS.ToPagedList(pageNumber, pageSize));
        }

        // GET: LoaiKs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiK loaiK = db.LoaiKS.Find(id);
            if (loaiK == null)
            {
                return HttpNotFound();
            }
            return View(loaiK);
        }

        // GET: LoaiKs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiKs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLoaiKs,Loai")] LoaiK loaiK)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.LoaiKS.Add(loaiK);
                    db.SaveChanges();
                }
                    return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu !" + ex.Message;
                return View(loaiK);
            }
        }

        // GET: LoaiKs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiK loaiK = db.LoaiKS.Find(id);
            if (loaiK == null)
            {
                return HttpNotFound();
            }
            return View(loaiK);
        }

        // POST: LoaiKs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLoaiKs,Loai")] LoaiK loaiK)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(loaiK).State = EntityState.Modified;
                    db.SaveChanges();
                }
                    return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu !" + ex.Message;
                return View(loaiK);
            }
        }

        // GET: LoaiKs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiK loaiK = db.LoaiKS.Find(id);
            if (loaiK == null)
            {
                return HttpNotFound();
            }
            return View(loaiK);
        }

        // POST: LoaiKs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiK loaiK = db.LoaiKS.Find(id);
            try
            {
                db.LoaiKS.Remove(loaiK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Không được xóa bản ghi này !" + ex.Message;
                return View("Delete",loaiK);
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
