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
    
    public partial class DanhGiaSan
    {
        public int MaDanhGia { get; set; }
        public Nullable<int> MaSan { get; set; }
        public Nullable<int> MaNguoiChoi { get; set; }
        public Nullable<decimal> Score { get; set; }
        public string Comment { get; set; }
    
        public virtual NguoiChoi NguoiChoi { get; set; }
        public virtual SanCauLong SanCauLong { get; set; }
    }
}