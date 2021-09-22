namespace Nhom14_ChuDu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThanhPho")]
    public partial class ThanhPho
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThanhPho()
        {
            KhachSans = new HashSet<KhachSan>();
        }

        [Key]
        [Required(ErrorMessage = "ID thành phố không được để trống!")]
        [DisplayName("ID thành phố")]
        public int IDThanhPho { get; set; }

        [Required(ErrorMessage = "Tên thành phố không được để trống!")]
        [DisplayName("Tên thành phố")]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Ảnh thành phố không được để trống!")]
        [DisplayName("Ảnh thành phố")]
        public string Anh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhachSan> KhachSans { get; set; }
    }
}
