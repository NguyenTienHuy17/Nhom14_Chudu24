namespace Nhom14_ChuDu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiKS")]
    public partial class LoaiK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiK()
        {
            KhachSans = new HashSet<KhachSan>();
        }

        [Key]
        [Required(ErrorMessage ="ID loại khách sạn không được để trống !")]
        [DisplayName("ID loại khách sạn")]
        public int IDLoaiKs { get; set; }

        [Required(ErrorMessage = "loại khách sạn không được để trống !")]
        [DisplayName("loại khách sạn")]
        public string Loai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhachSan> KhachSans { get; set; }
    }
}
