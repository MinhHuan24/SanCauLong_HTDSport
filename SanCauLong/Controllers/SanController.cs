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
        SanCauLongEntities1 db = new SanCauLongEntities1();
        public ActionResult Index()
        {
            return View();
        }
        //DANH SÁCH SÂN
        public ActionResult DanhSachSan(string search)
        {
            var courts = db.SanCauLongs.Include("DatSans").ToList(); 
            if (!string.IsNullOrEmpty(search))
            {
                courts = courts.Where(c => c.TenSan.Contains(search)).ToList();
            }
            return View(courts);
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
                return View("ThanhToanTruoc", san);
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
            var san = db.SanCauLongs.Find(maSan);
            if (san == null)
            {
                return HttpNotFound();
            }

            // Lấy thông tin đặt sân tương ứng
            var datSan = db.DatSans.Where(ds => ds.MaSan == maSan).ToList();

            // Nếu cần chỉ lấy thông tin mới nhất hoặc phù hợp nhất
            var thongTinDatSan = datSan.OrderByDescending(ds => ds.NgayDatSan).FirstOrDefault();

            // Tạo một ViewModel hoặc trực tiếp truyền dữ liệu vào ViewBag/ViewData
            var viewModel = new ChiTietSanViewModel
            {
                San = san,
                NgayDat = thongTinDatSan?.NgayDatSan,
                ThoiGianBatDau = thongTinDatSan?.ThoiGianBatDau,
                ThoiGianKetThuc = thongTinDatSan?.ThoiGianKetThuc,
                TrangThai = thongTinDatSan?.TrangThai
            };

            return View(viewModel);
        }

    }
}