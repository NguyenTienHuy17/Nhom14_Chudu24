namespace Nhom14_ChuDu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachSan")]
    public partial class KhachSan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachSan()
        {
            PhongKS = new HashSet<PhongK>();
            LoaiKS = new HashSet<LoaiK>();
        }

        [Key]
        [Required(ErrorMessage ="ID khách sạn không được để trống!")]
        [DisplayName("ID khách sạn")]
        public int IDKS { get; set; }

        [Required(ErrorMessage = "Tên khách sạn không được để trống!")]
        [DisplayName("Tên khách sạn")]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Địa chỉ khách sạn không được để trống!")]
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [DisplayName("Ảnh")]
        public string Anh { get; set; }

        [Required(ErrorMessage = "Mô tả khách sạn không được để trống!")]
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }

        [DisplayName("ID thành phố")]
        public int? IDThanhPho { get; set; }

        [DisplayName("Thành phố")]
        public virtual ThanhPho ThanhPho { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhongK> PhongKS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoaiK> LoaiKS { get; set; }
    }
}
