using System;
using Microsoft.EntityFrameworkCore;

namespace QuanLyTinTucConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new AppDbContext();

            while (true)
            {
                Console.WriteLine("\n================= Hệ thống Quản lý Tin Tức =================");
                Console.WriteLine("1. Xem tất cả tin tức");
                Console.WriteLine("2. Xem tin tức theo mã");
                Console.WriteLine("3. Xem tin tức theo loại");
                Console.WriteLine("4. Xem tin tức theo ngày đăng");
                Console.WriteLine("5. Thêm tin tức mới");
                Console.WriteLine("6. Cập nhật tiêu đề tin tức");
                Console.WriteLine("7. Xóa tin tức");
                Console.WriteLine("8. Thoát");
                Console.Write("Nhập lựa chọn của bạn: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PrintAllTinTucs(context);
                        break;

                    case "2":
                        Console.Write("Nhập mã tin tức: ");
                        string? maTinTuc = Console.ReadLine();
                        if (!string.IsNullOrEmpty(maTinTuc))
                            PrintTinTucById(context, maTinTuc);
                        else
                            Console.WriteLine("Mã tin tức không hợp lệ.");
                        break;

                    case "3":
                        Console.Write("Nhập mã loại tin tức: ");
                        string? maLoai = Console.ReadLine();
                        if (!string.IsNullOrEmpty(maLoai))
                            PrintTinTucsByLoaiTinTuc(context, maLoai);
                        else
                            Console.WriteLine("Mã loại tin tức không hợp lệ.");
                        break;

                    case "4":
                        Console.Write("Nhập ngày đăng (yyyy-mm-dd): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime ngayDang))
                            PrintTinTucsByNgayDang(context, ngayDang);
                        else
                            Console.WriteLine("Ngày không hợp lệ.");
                        break;

                    case "5":
                        AddTinTuc(context);
                        break;

                    case "6":
                        UpdateTinTucTieuDe(context);
                        break;

                    case "7":
                        DeleteTinTuc(context);
                        break;

                    case "8":
                        Console.WriteLine("Thoát chương trình...");
                        return;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ, vui lòng thử lại.");
                        break;
                }
            }
        }

        // 1. Xem tất cả tin tức
        static void PrintAllTinTucs(AppDbContext context)
        {
            Console.WriteLine("\n=== Danh sách Tin Tức ===");
            var tinTucs = context.TinTucs
                .Include(tt => tt.NhanVien)
                .Include(tt => tt.LoaiTinTuc)
                .ToList();
            if (tinTucs.Count == 0)
            {
                Console.WriteLine("Không tìm thấy tin tức nào.");
                return;
            }
            foreach (var tt in tinTucs)
            {
                Console.WriteLine($"Mã: {tt.MaTinTuc}, Tiêu đề: {tt.TieuDe}, Ngày đăng: {tt.NgayDang.ToShortDateString()}, " +
                                  $"Người đăng: {tt.NhanVien.Ten}, Loại: {tt.LoaiTinTuc.TenLoai}, Nội dung: {tt.NoiDungTin}");
            }
        }

        // 2. Xem tin tức theo mã
        static void PrintTinTucById(AppDbContext context, string maTinTuc)
        {
            Console.WriteLine("\n=== Tin Tức Theo Mã ===");
            var tinTuc = context.TinTucs
                .Include(tt => tt.NhanVien)
                .Include(tt => tt.LoaiTinTuc)
                .FirstOrDefault(tt => tt.MaTinTuc == maTinTuc);
            if (tinTuc != null)
            {
                Console.WriteLine($"Mã: {tinTuc.MaTinTuc}, Tiêu đề: {tinTuc.TieuDe}, Ngày đăng: {tinTuc.NgayDang.ToShortDateString()}, " +
                                  $"Người đăng: {tinTuc.NhanVien.Ten}, Loại: {tinTuc.LoaiTinTuc.TenLoai}, Nội dung: {tinTuc.NoiDungTin}");
            }
            else
            {
                Console.WriteLine("Không tìm thấy tin tức.");
            }
        }

        // 3. Xem tin tức theo loại
        static void PrintTinTucsByLoaiTinTuc(AppDbContext context, string maLoai)
        {
            Console.WriteLine("\n=== Tin Tức Theo Loại ===");
            var tinTucs = context.TinTucs
                .Include(tt => tt.NhanVien)
                .Include(tt => tt.LoaiTinTuc)
                .Where(tt => tt.MaLoai == maLoai)
                .ToList();
            if (tinTucs.Count > 0)
            {
                foreach (var tt in tinTucs)
                {
                    Console.WriteLine($"Mã: {tt.MaTinTuc}, Tiêu đề: {tt.TieuDe}, Ngày đăng: {tt.NgayDang.ToShortDateString()}, " +
                                      $"Người đăng: {tt.NhanVien.Ten}, Loại: {tt.LoaiTinTuc.TenLoai}, Nội dung: {tt.NoiDungTin}");
                }
            }
            else
            {
                Console.WriteLine($"Không tìm thấy tin tức nào thuộc loại {maLoai}.");
            }
        }

        // 4. Xem tin tức theo ngày đăng
        static void PrintTinTucsByNgayDang(AppDbContext context, DateTime ngayDang)
        {
            Console.WriteLine("\n=== Tin Tức Theo Ngày Đăng ===");
            var tinTucs = context.TinTucs
                .Include(tt => tt.NhanVien)
                .Include(tt => tt.LoaiTinTuc)
                .Where(tt => tt.NgayDang.Date == ngayDang.Date)
                .ToList();
            if (tinTucs.Count > 0)
            {
                foreach (var tt in tinTucs)
                {
                    Console.WriteLine($"Mã: {tt.MaTinTuc}, Tiêu đề: {tt.TieuDe}, Ngày đăng: {tt.NgayDang.ToShortDateString()}, " +
                                      $"Người đăng: {tt.NhanVien.Ten}, Loại: {tt.LoaiTinTuc.TenLoai}, Nội dung: {tt.NoiDungTin}");
                }
            }
            else
            {
                Console.WriteLine($"Không tìm thấy tin tức nào vào ngày {ngayDang.ToShortDateString()}.");
            }
        }

        // 5. Thêm tin tức mới
        static void AddTinTuc(AppDbContext context)
        {
            Console.WriteLine("\n=== Thêm Tin Tức Mới ===");
            Console.Write("Nhập mã tin tức: ");
            string? maTinTuc = Console.ReadLine();
            Console.Write("Nhập tiêu đề: ");
            string? tieuDe = Console.ReadLine();
            Console.Write("Nhập ngày đăng (yyyy-mm-dd): ");
            DateTime.TryParse(Console.ReadLine(), out DateTime ngayDang);
            Console.Write("Nhập nội dung tin: ");
            string? noiDungTin = Console.ReadLine();
            Console.Write("Nhập mã nhân viên đăng tin: ");
            string? nguoiDangTin = Console.ReadLine();
            Console.Write("Nhập mã loại tin tức: ");
            string? maLoai = Console.ReadLine();

            if (string.IsNullOrEmpty(maTinTuc) || string.IsNullOrEmpty(tieuDe) || string.IsNullOrEmpty(noiDungTin) ||
                string.IsNullOrEmpty(nguoiDangTin) || string.IsNullOrEmpty(maLoai))
            {
                Console.WriteLine("Thông tin không hợp lệ. Tin tức không được thêm.");
                return;
            }

            if (context.TinTucs.Any(tt => tt.MaTinTuc == maTinTuc))
            {
                Console.WriteLine($"Tin tức với mã {maTinTuc} đã tồn tại.");
                return;
            }

            if (!context.NhanViens.Any(nv => nv.MaTaiKhoan == nguoiDangTin))
            {
                Console.WriteLine($"Nhân viên với mã {nguoiDangTin} không tồn tại.");
                return;
            }

            if (!context.LoaiTinTucs.Any(lt => lt.MaLoai == maLoai))
            {
                Console.WriteLine($"Loại tin tức với mã {maLoai} không tồn tại.");
                return;
            }

            var newTinTuc = new TinTuc
            {
                MaTinTuc = maTinTuc,
                TieuDe = tieuDe,
                NgayDang = ngayDang,
                NoiDungTin = noiDungTin,
                NguoiDangTin = nguoiDangTin,
                MaLoai = maLoai
            };

            context.TinTucs.Add(newTinTuc);
            context.SaveChanges();
            Console.WriteLine($"Tin tức {maTinTuc} đã được thêm thành công.");
        }

        // 6. Cập nhật tiêu đề tin tức
        static void UpdateTinTucTieuDe(AppDbContext context)
        {
            Console.WriteLine("\n=== Cập nhật Tiêu đề Tin Tức ===");
            Console.Write("Nhập mã tin tức cần cập nhật: ");
            string? maTinTuc = Console.ReadLine();
            Console.Write("Nhập tiêu đề mới: ");
            string? newTieuDe = Console.ReadLine();

            if (string.IsNullOrEmpty(maTinTuc) || string.IsNullOrEmpty(newTieuDe))
            {
                Console.WriteLine("Thông tin không hợp lệ.");
                return;
            }

            var tinTuc = context.TinTucs.FirstOrDefault(tt => tt.MaTinTuc == maTinTuc);
            if (tinTuc != null)
            {
                tinTuc.TieuDe = newTieuDe;
                context.SaveChanges();
                Console.WriteLine($"Tiêu đề của tin tức {maTinTuc} đã được cập nhật thành {newTieuDe}.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy tin tức.");
            }
        }

        // 7. Xóa tin tức
        static void DeleteTinTuc(AppDbContext context)
        {
            Console.WriteLine("\n=== Xóa Tin Tức ===");
            Console.Write("Nhập mã tin tức cần xóa: ");
            string? maTinTuc = Console.ReadLine();

            if (string.IsNullOrEmpty(maTinTuc))
            {
                Console.WriteLine("Mã tin tức không hợp lệ.");
                return;
            }

            var tinTuc = context.TinTucs.FirstOrDefault(tt => tt.MaTinTuc == maTinTuc);
            if (tinTuc != null)
            {
                context.TinTucs.Remove(tinTuc);
                context.SaveChanges();
                Console.WriteLine($"Tin tức {maTinTuc} đã được xóa thành công.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy tin tức.");
            }
        }
    }
}