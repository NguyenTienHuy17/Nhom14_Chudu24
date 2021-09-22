namespace Nhom14_ChuDu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhongKS")]
    public partial class PhongK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhongK()
        {
            ChiTietKhuyenMais = new HashSet<ChiTietKhuyenMai>();
            DatPhongs = new HashSet<DatPhong>();
        }

        [Key]
        [Required(ErrorMessage = "ID phòng không được để trống !")]
        [DisplayName("ID phòng khách sạn")]
        public int IDPhongKS { get; set; }

        [Required(ErrorMessage = "Tên phòng không được để trống !")]
        [DisplayName("Tên phòng")]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Loại phòng không được để trống !")]
        [DisplayName("Loại phòng")]
        public string Loai { get; set; }

        [Required(ErrorMessage = "Giá phòng không được để trống !")]
        [Column(TypeName = "money")]
        [DisplayName("Giá phòng")]
        public decimal? Gia { get; set; }

        [Required(ErrorMessage = "Mô tả phòng không được để trống !")]
        [DisplayName("Mô tả phòng")]
        public string MoTa { get; set; }
        [DisplayName("ID khách sạn")]
        public int IDKS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatPhong> DatPhongs { get; set; }

        public virtual KhachSan KhachSan { get; set; }
    }
}
