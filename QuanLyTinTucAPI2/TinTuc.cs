using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyTinTucAPI2
{
    public class TinTuc
    {
        [Key]
        public string MaTinTuc { get; set; } = "";
        public string TieuDe { get; set; } = "";
        public DateTime NgayDang { get; set; }
        public string NoiDungTin { get; set; } = "";

        // Foreign Keys
        [Required]
        public string NguoiDangTin { get; set; } = "";
        [Required]
        public string MaLoai { get; set; } = "";

        // Navigation properties
        [ForeignKey("NguoiDangTin")]
        public NhanVien? NhanVien { get; set; } // Có thể null, EF Core sẽ tự ánh xạ

        [ForeignKey("MaLoai")]
        public LoaiTinTuc? LoaiTinTuc { get; set; } // Có thể null, EF Core sẽ tự ánh xạ
    }
}