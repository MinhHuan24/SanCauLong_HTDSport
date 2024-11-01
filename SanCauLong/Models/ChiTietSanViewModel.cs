using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanCauLong.Models
{
    public class ChiTietSanViewModel
    {
        
        public SanCauLong San { get; set; }
        public DateTime? NgayDat { get; set; }
        public TimeSpan? ThoiGianBatDau { get; set; }
        public TimeSpan? ThoiGianKetThuc { get; set; }
        public string TrangThai { get; set; }
    }
}