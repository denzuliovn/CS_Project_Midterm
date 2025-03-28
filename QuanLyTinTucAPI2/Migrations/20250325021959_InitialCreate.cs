using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyTinTucAPI2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiTinTucs",
                columns: table => new
                {
                    MaLoai = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenLoai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiTinTucs", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    MaTaiKhoan = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanViens", x => x.MaTaiKhoan);
                });

            migrationBuilder.CreateTable(
                name: "TinTucs",
                columns: table => new
                {
                    MaTinTuc = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayDang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoiDungTin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiDangTin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaLoai = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinTucs", x => x.MaTinTuc);
                    table.ForeignKey(
                        name: "FK_TinTucs_LoaiTinTucs_MaLoai",
                        column: x => x.MaLoai,
                        principalTable: "LoaiTinTucs",
                        principalColumn: "MaLoai",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TinTucs_NhanViens_NguoiDangTin",
                        column: x => x.NguoiDangTin,
                        principalTable: "NhanViens",
                        principalColumn: "MaTaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LoaiTinTucs",
                columns: new[] { "MaLoai", "TenLoai" },
                values: new object[,]
                {
                    { "LT000001", "Thể thao" },
                    { "LT000002", "Du lịch" },
                    { "LT000003", "Thời sự" },
                    { "LT000004", "Quốc tế" }
                });

            migrationBuilder.InsertData(
                table: "NhanViens",
                columns: new[] { "MaTaiKhoan", "DiaChi", "Email", "Ten", "VaiTro" },
                values: new object[,]
                {
                    { "NV000001", "Hà Nội", "nguyenvana@example.com", "Nguyễn Văn A", "Admin" },
                    { "NV000002", "TP.HCM", "tranthib@example.com", "Trần Thị B", "Editor" },
                    { "NV000003", "Đà Nẵng", "levanc@example.com", "Lê Văn C", "Writer" }
                });

            migrationBuilder.InsertData(
                table: "TinTucs",
                columns: new[] { "MaTinTuc", "MaLoai", "NgayDang", "NguoiDangTin", "NoiDungTin", "TieuDe" },
                values: new object[,]
                {
                    { "TT000001", "LT000001", new DateTime(2025, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "NV000001", "Nội dung tin thể thao...", "Trận đấu bóng đá tối nay" },
                    { "TT000002", "LT000002", new DateTime(2025, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "NV000002", "Nội dung tin du lịch...", "Khám phá Đà Lạt mùa hoa" },
                    { "TT000003", "LT000003", new DateTime(2025, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "NV000003", "Nội dung tin thời sự...", "Tin tức thời sự mới nhất" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TinTucs_MaLoai",
                table: "TinTucs",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_TinTucs_NguoiDangTin",
                table: "TinTucs",
                column: "NguoiDangTin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TinTucs");

            migrationBuilder.DropTable(
                name: "LoaiTinTucs");

            migrationBuilder.DropTable(
                name: "NhanViens");
        }
    }
}
