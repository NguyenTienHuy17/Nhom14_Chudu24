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
    public class PhongKsController : Controller
    {
        private ChuDu24 db = new ChuDu24();

        // GET: PhongKs
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int ?page)
        {
            //các biến sắp xếp
            ViewBag.CurrentSort = sortOrder;

            ViewBag.SapTheoTen = String.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";
            ViewBag.SapTheoGia = sortOrder == "gia" ? "gia_desc" : "gia";

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

            var phongKS = db.PhongKS.Include(p => p.KhachSan);

            //lọc theo tên hàng
            if (!String.IsNullOrEmpty(searchString))
            {
                phongKS = phongKS.Where(p => p.KhachSan.Ten.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ten_desc":
                    phongKS = phongKS.OrderByDescending(p => p.Ten);
                    break;
                case "gia":
                    phongKS = phongKS.OrderBy(p => p.Gia);
                    break;
                case "gia_desc":
                    phongKS = phongKS.OrderByDescending(p => p.Gia);
                    break;
                default:
                    phongKS = phongKS.OrderBy(p => p.Ten);
                    break;
            }

            //phongKS = phongKS.OrderBy(p => p.IDPhongKS);
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            return View(phongKS.ToPagedList(pageNumber, pageSize));
        }

        // GET: PhongKs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongK phongK = db.PhongKS.Find(id);
            if (phongK == null)
            {
                return HttpNotFound();
            }
            return View(phongK);
        }

        // GET: PhongKs/Create
        public ActionResult Create()
        {
            ViewBag.IDKS = new SelectList(db.KhachSans, "IDKS", "Ten");
            return View();
        }

        // POST: PhongKs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPhongKS,Ten,Loai,Gia,MoTa,IDKS")] PhongK phongK)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.PhongKS.Add(phongK);
                    db.SaveChanges();
                }
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu !" + ex.Message;
                ViewBag.IDKS = new SelectList(db.KhachSans, "IDKS", "Ten", phongK.IDKS);
                return View(phongK);
            }
        }

        // GET: PhongKs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongK phongK = db.PhongKS.Find(id);
            if (phongK == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDKS = new SelectList(db.KhachSans, "IDKS", "Ten", phongK.IDKS);
            return View(phongK);
        }

        // POST: PhongKs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPhongKS,Ten,Loai,Gia,MoTa,IDKS")] PhongK phongK)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(phongK).State = EntityState.Modified;
                    db.SaveChanges();
                }
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu !" + ex.Message;
                ViewBag.IDKS = new SelectList(db.KhachSans, "IDKS", "Ten", phongK.IDKS);
                return View(phongK);
            }
        }

        // GET: PhongKs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongK phongK = db.PhongKS.Find(id);
            if (phongK == null)
            {
                return HttpNotFound();
            }
            return View(phongK);
        }

        // POST: PhongKs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhongK phongK = db.PhongKS.Find(id);
            try
            {
                db.PhongKS.Remove(phongK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Không được xóa bản ghi này !" + ex.Message;
                return View("Delete", phongK);
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
