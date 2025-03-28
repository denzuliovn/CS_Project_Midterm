using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyTinTucConsole
{
    public class NhanVien
    {
        [Key]
        public string MaTaiKhoan { get; set; } = "";
        public string Email { get; set; } = "";
        public string DiaChi { get; set; } = "";
        public string Ten { get; set; } = "";
        public string VaiTro { get; set; } = "";

        // Mối quan hệ 1-N với TinTuc
        public List<TinTuc> TinTucs { get; set; } = new List<TinTuc>();
    }
}