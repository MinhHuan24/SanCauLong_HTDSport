//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SanCauLong.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DienDan
    {
        public int MaDienDan { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public Nullable<System.DateTime> NgayDang { get; set; }
        public string TheLoai { get; set; }
        public string TacGiaBaiDang { get; set; }
        public Nullable<int> MaNguoiChoi { get; set; }
        public Nullable<int> MaChuSan { get; set; }
        public string Images { get; set; }
    
        public virtual ChuSan ChuSan { get; set; }
        public virtual NguoiChoi NguoiChoi { get; set; }
    }
}
