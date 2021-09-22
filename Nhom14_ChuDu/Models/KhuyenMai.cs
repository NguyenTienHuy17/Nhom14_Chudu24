namespace Nhom14_ChuDu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhuyenMai")]
    public partial class KhuyenMai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhuyenMai()
        {
            ChiTietKhuyenMais = new HashSet<ChiTietKhuyenMai>();
        }

        [Key]
        [Required(ErrorMessage = "ID khuyến mãi không được để trống !")]
        [DisplayName("ID Khuyến mãi")]
        public int IDKhuyenMai { get; set; }

        [Required(ErrorMessage = "Tên khuyến mãi không được để trống !")]
        [DisplayName("Tên")]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Mô tả khuyến mãi không được để trống !")]
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; }
    }
}
