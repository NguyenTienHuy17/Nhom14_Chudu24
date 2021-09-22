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
    public class KhuyenMaisController : Controller
    {
        private ChuDu24 db = new ChuDu24();

        // GET: KhuyenMais
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
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

            var khuyenMais = db.KhuyenMais.Select(km => km);

            //lọc theo tên hàng
            if (!String.IsNullOrEmpty(searchString))
            {
                khuyenMais = khuyenMais.Where(km => km.Ten.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ten_desc":
                    khuyenMais = khuyenMais.OrderByDescending(km => km.Ten);
                    break;
                default:
                    khuyenMais = khuyenMais.OrderBy(km => km.Ten);
                    break;
            }


            //khuyenMais = khuyenMais.OrderBy(km => km.IDKhuyenMai);
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(khuyenMais.ToPagedList(pageNumber, pageSize));
        }

        // GET: KhuyenMais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            if (khuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenMai);
        }

        // GET: KhuyenMais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KhuyenMais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDKhuyenMai,Ten,MoTa")] KhuyenMai khuyenMai)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.KhuyenMais.Add(khuyenMai);
                    db.SaveChanges();
                }
                    return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu !" + ex.Message;
                return View(khuyenMai);
            }
        }

        // GET: KhuyenMais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            if (khuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenMai);
        }

        // POST: KhuyenMais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDKhuyenMai,Ten,MoTa")] KhuyenMai khuyenMai)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(khuyenMai).State = EntityState.Modified;
                    db.SaveChanges();
                }
                    return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu !" + ex.Message;
                return View(khuyenMai);
            }
        }

        // GET: KhuyenMais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            if (khuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenMai);
        }

        // POST: KhuyenMais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            try
            {
                db.KhuyenMais.Remove(khuyenMai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không được xóa bản ghi này !" + ex.Message;
                return View("Delete", khuyenMai);
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
