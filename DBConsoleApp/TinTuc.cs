using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyTinTucConsole
{
    public class TinTuc
    {
        [Key]
        public string MaTinTuc { get; set; } = "";
        public string TieuDe { get; set; } = "";
        public DateTime NgayDang { get; set; }
        public string NoiDungTin { get; set; } = "";

        // Foreign Keys
        public string NguoiDangTin { get; set; } = "";
        public string MaLoai { get; set; } = "";

        // Navigation properties
        [ForeignKey("NguoiDangTin")]
        public NhanVien NhanVien { get; set; } = null!;

        [ForeignKey("MaLoai")]
        public LoaiTinTuc LoaiTinTuc { get; set; } = null!;
    }
}