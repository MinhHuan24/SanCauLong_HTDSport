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
    
    public partial class ChuSan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuSan()
        {
            this.DienDans = new HashSet<DienDan>();
            this.SuKiens = new HashSet<SuKien>();
        }
    
        public int MaChuSan { get; set; }
        public string TenSan { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> MaSan { get; set; }
    
        public virtual SanCauLong SanCauLong { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DienDan> DienDans { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuKien> SuKiens { get; set; }
    }
}
