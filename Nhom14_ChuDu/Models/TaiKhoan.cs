namespace Nhom14_ChuDu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [Key]
        [Required(ErrorMessage = "Không được để trống ID tài khoản!")]
        [DisplayName("ID tài khoản")]
        public int IDTK { get; set; }

        [Required(ErrorMessage ="Không được để trống mật khẩu!")]
        [DisplayName("Mật khẩu")]
        [StringLength(16)]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Không được để trống tên!")]
        [DisplayName("Tên")]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Không được để trống địa chỉ!")]
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Không được để trống sdt!")]
        [DisplayName("SĐT")]
        [StringLength(16)]
        public string SDT { get; set; }

        [DisplayName("Vai trò")]
        public bool VaiTro { get; set; }
    }
}
