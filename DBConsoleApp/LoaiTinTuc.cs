using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyTinTucConsole
{
    public class LoaiTinTuc
    {
        [Key]
        public string MaLoai { get; set; } = "";
        public string TenLoai { get; set; } = "";

        // Mối quan hệ 1-N với TinTuc
        public List<TinTuc> TinTucs { get; set; } = new List<TinTuc>();
    }
}