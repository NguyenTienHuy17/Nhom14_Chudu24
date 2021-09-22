using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nhom14_ChuDu.Models;
using PagedList;

namespace Nhom14_ChuDu.Controllers
{
    public class ThanhPhoesController : Controller
    {
        private ChuDu24 db = new ChuDu24();

        // GET: ThanhPhoes
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

            var thanhPho = db.ThanhPhoes.Select(tp => tp);

            //lọc theo tên hàng
            if (!String.IsNullOrEmpty(searchString))
            {
                thanhPho = thanhPho.Where(k => k.Ten.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ten_desc":
                    thanhPho = thanhPho.OrderByDescending(k => k.Ten);
                    break;
                default:
                    thanhPho = thanhPho.OrderBy(k => k.Ten);
                    break;
            }

            //thanhPho = thanhPho.OrderBy(tp => tp.IDThanhPho);
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(thanhPho.ToPagedList(pageNumber, pageSize));
        }

        // GET: ThanhPhoes/Details/5
        public PartialViewResult _DsThanhPho()
        {
            var thanhPhoes = db.ThanhPhoes.Select(p => p);
            return PartialView(thanhPhoes);
        }
        // GET: ThanhPhoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhPho thanhPho = db.ThanhPhoes.Find(id);
            if (thanhPho == null)
            {
                return HttpNotFound();
            }
            return View(thanhPho);
        }

        // GET: ThanhPhoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ThanhPhoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDThanhPho,Ten,Anh")] ThanhPho thanhPho)
        {
            try
            {
                thanhPho.Anh = "";
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Path.Combine(Server.MapPath("~/Image/Tỉnh thành/"), FileName);
                    f.SaveAs(UploadPath);
                    thanhPho.Anh = FileName;
                }
                db.ThanhPhoes.Add(thanhPho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu !" + ex.Message;
                return View(thanhPho);
            }
        }

        // GET: ThanhPhoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhPho thanhPho = db.ThanhPhoes.Find(id);
            if (thanhPho == null)
            {
                return HttpNotFound();
            }
            return View(thanhPho);
        }

        // POST: ThanhPhoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDThanhPho,Ten,Anh")] ThanhPho thanhPho)
        {
            try
            {
                thanhPho.Anh = "";
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Path.Combine(Server.MapPath("~/Image/Tỉnh thành/") + FileName);
                    f.SaveAs(UploadPath);
                    thanhPho.Anh = FileName;
                }
                db.Entry(thanhPho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu !" + ex.Message;
                return View(thanhPho);
            }
        }

        // GET: ThanhPhoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhPho thanhPho = db.ThanhPhoes.Find(id);
            if (thanhPho == null)
            {
                return HttpNotFound();
            }
            return View(thanhPho);
        }

        // POST: ThanhPhoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThanhPho thanhPho = db.ThanhPhoes.Find(id);
            try
            {
                db.ThanhPhoes.Remove(thanhPho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không được xóa bản ghi này !" + ex.Message;
                return View("Delete", thanhPho);
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
        public PartialViewResult _SearchThanhPho()
        {
            var thanhPhoes = db.ThanhPhoes.Select(p => p);
            return PartialView(thanhPhoes);
        }
    }
}
