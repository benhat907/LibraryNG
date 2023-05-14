using System.ComponentModel.DataAnnotations;
namespace LibraryNG.Models
{
    public class InfoLibrary
    {
        [Required]
        [Display (Name = "Mã Lỗi")]
        public string MaLoi { get; set; }
        [Required]
        [Display (Name = "Tên Lỗi")]
        public string TenLoi { get; set; }
        [Display (Name = "Phân Loại NG")]
        public string PhanLoaiNG { get; set; }
        [Display (Name = "Chú Thích")]
        public string ChuThich { get; set; }
    }
}