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
        //DANH SÁCH SÂN
        public ActionResult DanhSachSan()
        {
            var sanCauLongs = db.SanCauLongs.ToList();
            return View(sanCauLongs);
        }
        // CHUC NANG DAT SAN
        // GET: DatSan
        public ActionResult DatSan(int maSan)
        {
            var san = db.SanCauLongs.Find(maSan);
            if (san == null) return HttpNotFound();

            return View(san);
        }

        [HttpPost]
        public ActionResult DatSan(int maSan, DateTime NgayDat, DateTime ThoiGianBatDau, DateTime ThoiGianKetThuc, string thanhToan = null)
        {
            var san = db.SanCauLongs.Find(maSan);
            if (san == null) return HttpNotFound();

            // Tính toán thời gian và ngày đặt
            double hours = (ThoiGianKetThuc - ThoiGianBatDau).TotalHours;
            int daysDifference = (NgayDat - DateTime.Now.Date).Days;

            // Kiểm tra điều kiện ngày và thời gian
            if (daysDifference > 2 || hours > 4)
            {
                TempData["Error"] = "Ngày đặt sân không quá 2 ngày từ hôm nay và thời gian không quá 4 giờ!";
                return RedirectToAction("DatSan", new { maSan });
            }

            // Nếu chưa chọn thanh toán, chuyển đến view chọn phương thức thanh toán
            if (string.IsNullOrEmpty(thanhToan))
            {
                ViewBag.MaSan = maSan;
                ViewBag.NgayDat = NgayDat;
                ViewBag.ThoiGianBatDau = ThoiGianBatDau;
                ViewBag.ThoiGianKetThuc = ThoiGianKetThuc;
                return View("ChonPhuongThucThanhToan");
            }

            // Xử lý khi đã chọn thanh toán
            if (thanhToan == "truoc")
            {
                san.TrangThai = "Đã được đặt";
                double tienThanhToan = hours * (double)san.GiaTien.GetValueOrDefault();

                // Thêm mã QR vào ViewBag
                ViewBag.QRCodeImage = "/Content/QRCode.png";
                ViewBag.SoTien = tienThanhToan;

                // Trả về view thanh toán trước
                return View("ThanhToanTruoc");
            }
            else
            {
                san.TrangThai = "Đã được đặt";
                db.SaveChanges();

                TempData["Message"] = "Đặt sân thành công! Thanh toán khi đến sân.";
                return RedirectToAction("DanhSachSan");
            }
        }

        // POST: Xác nhận đã chuyển khoản
        [HttpPost]
        public ActionResult XacNhanThanhToan(int maSan)
        {
            var san = db.SanCauLongs.Find(maSan);
            if (san == null) return HttpNotFound();

            // Cập nhật trạng thái sân
            san.TrangThai = "Đã được đặt";
            db.SaveChanges();

            // Thiết lập thời gian hoàn thành để cập nhật lại trạng thái sân
            var thoiGianKetThuc = DateTime.Now.AddHours(4); // Giả sử thời gian đặt là 4 giờ
            System.Threading.Timer timer = new System.Threading.Timer(_ =>
            {
                san.TrangThai = "Trống";
                db.SaveChanges();
            }, null, thoiGianKetThuc - DateTime.Now, System.Threading.Timeout.InfiniteTimeSpan);

            TempData["Message"] = "Đã xác nhận thanh toán thành công!";
            return RedirectToAction("DanhSachSan");
        }
        // CHI TIẾT SÂN
        public ActionResult ChiTietSan(int maSan)
        {
            // Tìm sân dựa vào mã sân
            var san = db.SanCauLongs.FirstOrDefault(s => s.MaSan == maSan);

            // Kiểm tra nếu không tìm thấy sân thì trả về NotFound
            if (san == null)
            {
                return HttpNotFound();
            }

            // Trả về view với thông tin chi tiết của sân
            return View(san);
        }

    }
}