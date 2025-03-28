using Microsoft.EntityFrameworkCore;

namespace QuanLyTinTucConsole
{
    public class AppDbContext : DbContext
    {
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<TinTuc> TinTucs { get; set; }
        public DbSet<LoaiTinTuc> LoaiTinTucs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connStr = "Server=DUSTIN;";
            connStr += "Encrypt=false;";
            connStr += "TrustServerCertificate=True;";
            connStr += "Database=QuanLyTinTuc;";
            connStr += "User Id=sa;";
            connStr += "Password=123456789;";
            optionsBuilder.UseSqlServer(connStr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình mối quan hệ
            modelBuilder.Entity<TinTuc>()
                .HasOne(tt => tt.NhanVien)
                .WithMany(nv => nv.TinTucs)
                .HasForeignKey(tt => tt.NguoiDangTin);

            modelBuilder.Entity<TinTuc>()
                .HasOne(tt => tt.LoaiTinTuc)
                .WithMany(lt => lt.TinTucs)
                .HasForeignKey(tt => tt.MaLoai);

            // Seed dữ liệu mẫu
            modelBuilder.Entity<NhanVien>().HasData(
                new NhanVien { MaTaiKhoan = "NV000001", Email = "nguyenvana@example.com", DiaChi = "Hà Nội", Ten = "Nguyễn Văn A", VaiTro = "Admin" },
                new NhanVien { MaTaiKhoan = "NV000002", Email = "tranthib@example.com", DiaChi = "TP.HCM", Ten = "Trần Thị B", VaiTro = "Editor" },
                new NhanVien { MaTaiKhoan = "NV000003", Email = "levanc@example.com", DiaChi = "Đà Nẵng", Ten = "Lê Văn C", VaiTro = "Writer" }
            );

            modelBuilder.Entity<LoaiTinTuc>().HasData(
                new LoaiTinTuc { MaLoai = "LT000001", TenLoai = "Thể thao" },
                new LoaiTinTuc { MaLoai = "LT000002", TenLoai = "Du lịch" },
                new LoaiTinTuc { MaLoai = "LT000003", TenLoai = "Thời sự" },
                new LoaiTinTuc { MaLoai = "LT000004", TenLoai = "Quốc tế" }
            );

            modelBuilder.Entity<TinTuc>().HasData(
                new TinTuc { MaTinTuc = "TT000001", TieuDe = "Trận đấu bóng đá tối nay", NgayDang = new DateTime(2025, 3, 24), NoiDungTin = "Nội dung tin thể thao...", NguoiDangTin = "NV000001", MaLoai = "LT000001" },
                new TinTuc { MaTinTuc = "TT000002", TieuDe = "Khám phá Đà Lạt mùa hoa", NgayDang = new DateTime(2025, 3, 24), NoiDungTin = "Nội dung tin du lịch...", NguoiDangTin = "NV000002", MaLoai = "LT000002" },
                new TinTuc { MaTinTuc = "TT000003", TieuDe = "Tin tức thời sự mới nhất", NgayDang = new DateTime(2025, 3, 24), NoiDungTin = "Nội dung tin thời sự...", NguoiDangTin = "NV000003", MaLoai = "LT000003" }
            );
        }
    }
}