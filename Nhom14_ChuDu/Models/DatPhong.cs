namespace Nhom14_ChuDu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DatPhong")]
    public partial class DatPhong
    {
        [Key]
        [Required(ErrorMessage = "ID đặt phòng không được để trống!")]
        [DisplayName("ID đặt phòng")]
        public int IDDatPhong { get; set; }

        [Required(ErrorMessage = "ID phòng không được để trống!")]
        [DisplayName("ID phòng khách sạn")]
        public int IDPhongKS { get; set; }

        [Required(ErrorMessage = "Email phòng không được để trống!")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "SDT không được để trống!")]
        [DisplayName("SDT")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Thời gian không được để trống!")]
        [DisplayName("Thời gian")]
        public string ThoiGian { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống!")]
        [DisplayName("Số lượng")]
        public int? SoLuong { get; set; }

        public virtual PhongK PhongK { get; set; }
    }
}
