using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nhom14_ChuDu.Models;
using PagedList;
using System.IO;

namespace Nhom14_ChuDu.Controllers
{
    public class KhachSansController : Controller
    {
        private ChuDu24 db = new ChuDu24();

        // GET: KhachSans
        public ActionResult Index__Admin(string sortOrder, string searchString, string currentFilter, int? page)
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

            var khachSans = db.KhachSans.Include(k => k.ThanhPho);

            //lọc theo tên hàng
            if (!String.IsNullOrEmpty(searchString))
            {
                khachSans = khachSans.Where(k => k.Ten.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ten_desc":
                    khachSans = khachSans.OrderByDescending(k => k.Ten);
                    break;
                default:
                    khachSans = khachSans.OrderBy(k => k.Ten);
                    break;
            }

            //khachSans = khachSans.OrderBy(k => k.IDKS);
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(khachSans.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Index__detail(string sortOrder, string searchString, string currentFilter, int? page)
        {
            var topphongdat = db.PhongKS.OrderByDescending(u => u.DatPhongs.Count).Take(3);

            return View(topphongdat.ToList());
        }

        public ActionResult Index(string sortOrder, string searchString, string curentFilter, int? page)
        {
            ViewBag.SapTheoTen = String.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";

            //Lấy gtri cảu bộ lọc hiện tại;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = curentFilter;
            }
            ViewBag.curentFilter = searchString;

            var khachsan = db.KhachSans.Select(p => p);

            //Lọc theo tên hàng
            if (!String.IsNullOrEmpty(searchString))
            {
                khachsan = khachsan.Where(p => p.Ten.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "ten_desc":
                    khachsan = khachsan.OrderByDescending(s => s.Ten);
                    break;
                default:
                    khachsan = khachsan.OrderBy(s => s.Ten);
                    break;
            }
            int pageSize = 3;
            //Nếu trang null trả về 1
            int pageNumber = (page ?? 1);

            return View(khachsan.ToPagedList(pageNumber, pageSize));

        }
        public PartialViewResult _DsKhachSan()
        {
            var khachSans = db.KhachSans.Select(p => p);
            return PartialView(khachSans);
        }
        public PartialViewResult TimKhachSan()
        {
            
            if(Request["city"] != "df" && Request["hotel"] == "")
            {
                int ID = int.Parse(Request["city"]);
                var khachSans = db.KhachSans.Where(p => p.IDThanhPho == ID);
                return PartialView(khachSans);
            }
            else if(Request["hotel"] != "" && Request["city"] == "df")
            {
                string tenKS = Request["hotel"];
                var khachSans = db.KhachSans.Where(p => p.Ten.ToLower().Contains(tenKS.ToLower()));
                return PartialView(khachSans);
            }
            else if (Request["city"] != "df" && Request["hotel"] != "")
            {
                int ID = int.Parse(Request["city"]);
                string tenKS = Request["hotel"];
                var tp = db.KhachSans.Where(p => p.IDThanhPho == ID);
                var khachSans = tp.Where(p => p.Ten == tenKS);
                return PartialView(khachSans);
                
            }
            return PartialView();
            
        }

        public ActionResult Detail(string id)
        {
            int maKS = int.Parse(id);
            var phongKS = db.PhongKS.Where(p => p.IDKS.Equals(maKS) && p.ChiTietKhuyenMais.Count() > 0);
            return View(phongKS.ToList());
        }

        // GET: KhachSans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachSan khachSan = db.KhachSans.Find(id);
            if (khachSan == null)
            {
                return HttpNotFound();
            }
            return View(khachSan);
        }

        // GET: KhachSans/Create
        public ActionResult Create()
        {
            ViewBag.IDThanhPho = new SelectList(db.ThanhPhoes, "IDThanhPho", "Ten");
            return View();
        }

        // POST: KhachSans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDKS,Ten,DiaChi,Anh,MoTa,IDThanhPho")] KhachSan khachSan)
        {
            try
            {
                khachSan.Anh = "";
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Path.Combine(Server.MapPath("~/Image/Khách sạn/"), FileName);
                    f.SaveAs(UploadPath);
                    khachSan.Anh = FileName;
                }
                db.KhachSans.Add(khachSan);
                db.SaveChanges();
                return RedirectToAction("Index__Admin");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu !" + ex.Message;
                ViewBag.IDThanhPho = new SelectList(db.ThanhPhoes, "IDThanhPho", "Ten", khachSan.IDThanhPho);
                return View(khachSan);
            }
        }

        // GET: KhachSans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachSan khachSan = db.KhachSans.Find(id);
            if (khachSan == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDThanhPho = new SelectList(db.ThanhPhoes, "IDThanhPho", "Ten", khachSan.IDThanhPho);
            return View(khachSan);
        }

        // POST: KhachSans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDKS,Ten,DiaChi,Anh,MoTa,IDThanhPho")] KhachSan khachSan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    khachSan.Anh = "";
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string UpLoadPath = Server.MapPath("~/Image/Khách Sạn/" + FileName);
                        f.SaveAs(UpLoadPath);
                        khachSan.Anh = FileName;
                    }
                    db.Entry(khachSan).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index__Admin");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu !" + ex.Message;
                ViewBag.IDThanhPho = new SelectList(db.ThanhPhoes, "IDThanhPho", "Ten", khachSan.IDThanhPho);
                return View(khachSan);
            }
        }

        // GET: KhachSans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachSan khachSan = db.KhachSans.Find(id);
            if (khachSan == null)
            {
                return HttpNotFound();
            }
            return View(khachSan);
        }

        // POST: KhachSans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachSan khachSan = db.KhachSans.Find(id);
            try
            {
                db.KhachSans.Remove(khachSan);
                db.SaveChanges();
                return RedirectToAction("Index__Admin");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không được xóa bản ghi này !" + ex.Message;
                return View("Delete", khachSan);
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

        public ActionResult PhongKs(int IDKS, string sortOrder, string searchString, string curentFilter, int? page)
        {
            ViewBag.SapTheoTen = String.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";

            //Lấy gtri cảu bộ lọc hiện tại;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = curentFilter;
            }
            ViewBag.curentFilter = searchString;

            var phongks = db.PhongKS.Where(p => p.IDKS.Equals(IDKS));

            //Lọc theo tên hàng
            if (!String.IsNullOrEmpty(searchString))
            {
                phongks = phongks.Where(p => p.Ten.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "ten_desc":
                    phongks = phongks.OrderByDescending(s => s.Ten);
                    break;
                default:
                    phongks = phongks.OrderBy(s => s.Ten);
                    break;
            }
            int pageSize = 3;
            //Nếu trang null trả về 1
            int pageNumber = (page ?? 1);

            return View(phongks.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DatPhong(int IDPhongKS, int soluong, string sodienthoai, string email)
        {
            DateTime curTime = new DateTime();
            if (email == null)
            {
                email = "abc@123.com";
            }
            if (Session["sodienthoai"] != null)
            {
                sodienthoai = Session["sodienthoai"].ToString();
            }
            // Nếu đã lưu sesion sdt thì thêm vào giỏ hàng và chuyển đến giỏi hàng
            if (Session["sodienthoai"] != null)
            {
                List<DatPhong> dp = new List<DatPhong>();

                if (Session["giohang"] != null)
                {
                    dp = (List<DatPhong>)Session["giohang"];
                }

                var datphong = new DatPhong();
                datphong.IDPhongKS = IDPhongKS;
                datphong.IDDatPhong = 1;
                datphong.SoLuong = soluong;
                datphong.SDT = sodienthoai;
                datphong.Email = email;
                datphong.ThoiGian = curTime.ToString();
                dp.Add(datphong);

                Session["giohang"] = dp;
                return RedirectToAction("GioHang");
            }

            //Nếu điền số điện thoại thì thêm hàng và chueyenr đến giỏ hàng
            if (!String.IsNullOrEmpty(sodienthoai))
            {
                Session["sodienthoai"] = sodienthoai;
                List<DatPhong> dp = new List<DatPhong>();
                var datphong = new DatPhong();
                datphong.IDDatPhong = 1;
                datphong.IDPhongKS = IDPhongKS;
                datphong.SoLuong = soluong;
                datphong.SDT = sodienthoai;
                datphong.Email = email;
                datphong.ThoiGian = curTime.ToString();
                dp.Add(datphong);
                Session["giohang"] = dp;
                return RedirectToAction("GioHang");
            }
            // Nếu chưa điền số điện thoại bắt điền
            ViewBag.IDPhongKS = IDPhongKS;
            ViewBag.soluong = soluong;
            return View();
        }

        public ActionResult DeleteGioHang(string IDPhongKS)
        {
            var bientam = new DatPhong();
            List<DatPhong> a = new List<DatPhong>();
            a = (List<DatPhong>)Session["gioHang"];
            foreach (var item in a)
            {
                if (item.IDPhongKS == Int32.Parse(IDPhongKS))
                {
                    bientam = item;
                }
            }
            a.Remove(bientam);
            Session["gioHang"] = a;
            return RedirectToAction("GioHang");
        }

        public ActionResult DeleteDonHang(string IDPhongKS)
        {
            var bientam = new DatPhong();
            var sdt = Session["sodienthoai"].ToString();
            var a = db.DatPhongs.Where(p => p.SDT.Equals(sdt));
            foreach (var item in a)
            {
                if (item.IDPhongKS == Int32.Parse(IDPhongKS))
                {
                    bientam = item;
                    break;
                }
            }

            db.DatPhongs.Remove(bientam);
            db.SaveChanges();
            Session["gioHang"] = null;
            return RedirectToAction("DonHang");
        }

        public ActionResult GioHang()
        {
            if (Session["giohang"] == null)
            {
                ViewBag.NoItems = "Chưa có phòng nào được đặt";
            }
            else
            {
                var dsphong = new ArrayList();
                List<DatPhong> dp = (List<DatPhong>)Session["giohang"];
                foreach (var item in dp)
                {
                    var tmpValue = db.PhongKS.Find(item.IDPhongKS);
                    var gia = item.SoLuong * tmpValue.Gia;
                    string[] tmp = { item.IDPhongKS.ToString(), tmpValue.Ten, item.SoLuong.ToString(), gia.ToString() };
                    dsphong.Add(tmp);
                }

                ViewBag.gioHang = dsphong;
            }


            return View();
        }



        public ActionResult DonHang(string sodienthoaiSearch)
        {
            if (Session["sodienthoai"] == null && String.IsNullOrEmpty(sodienthoaiSearch))
            {
                ViewBag.NoSDT = "Nhập số điện thoại đẻ tìm kiếm phòng";
                return View();
            }
            string sdt = "";
            if (!String.IsNullOrEmpty(sodienthoaiSearch))
            {
                sdt = sodienthoaiSearch;
            }
            else
            {
                sdt = Session["sodienthoai"].ToString();
            }

            var phongdat = db.DatPhongs.Where(p => p.SDT.Equals(sdt));

            if (phongdat.Count() == 0 && Session["giohang"] == null)
            {
                ViewBag.NoItems = "Chưa có phòng nào được đặt";
                return View();
            }

            var dsphong = new ArrayList();
            foreach (var item in phongdat)
            {
                var tmpValue = db.PhongKS.Find(item.IDPhongKS);
                var gia = item.SoLuong * tmpValue.Gia;
                string[] tmp = { item.IDPhongKS.ToString(), tmpValue.Ten, item.SoLuong.ToString(), gia.ToString() };
                dsphong.Add(tmp);
            }

            if (Session["giohang"] != null)
            {
                List<DatPhong> dp = (List<DatPhong>)Session["giohang"];
                foreach (var item in dp)
                {
                    db.DatPhongs.Add(item);
                    db.SaveChanges();
                    var tmpValue = db.PhongKS.Find(item.IDPhongKS);
                    var gia = item.SoLuong * tmpValue.Gia;
                    string[] tmp = { item.IDPhongKS.ToString(), tmpValue.Ten, item.SoLuong.ToString(), gia.ToString() };
                    dsphong.Add(tmp);
                }

                Session["giohang"] = null;
            }

            ViewBag.gioHang = dsphong;
            return View();
        }
    }
}
