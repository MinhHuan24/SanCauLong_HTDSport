using SanCauLong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SanCauLong.Controllers
{
    public class SanController : Controller
    {
        // GET: San
        SanCauLongEntities db = new SanCauLongEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DanhSachSan()
        {
            var sanCauLongs = db.SanCauLongs.ToList();
            return View(sanCauLongs);
        }
        public ActionResult DatSan(int maSan)
        {
            // Lấy thông tin sân
            var san = db.SanCauLongs.Find(maSan);
            if (san == null)
            {
                return HttpNotFound();
            }

            return View(san); // Trả về view để đặt sân
        }

        [HttpPost]
        public ActionResult DatSan(int maSan, DateTime ngayDat, TimeSpan thoiGianBatDau, TimeSpan thoiGianKetThuc)
        {
            if (thoiGianBatDau >= thoiGianKetThuc)
            {
                ModelState.AddModelError("", "Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc.");
                return View(); // Trả về view với thông báo lỗi
            }
            var datSan = new DatSan
            {
                MaSan = maSan,
                MaNguoiChoi = 1, // Lấy UserID của người dùng hiện tại
                NgayDatSan = ngayDat,
                ThoiGianBatDau = thoiGianBatDau,
                ThoiGianKetThuc = thoiGianKetThuc,
                TrangThai = "Đã đặt"
            };

            db.DatSans.Add(datSan);
            db.SaveChanges();

            return RedirectToAction("DanhSachSan");
        }


    }
}