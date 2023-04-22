using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrugStore.Migrations
{
    /// <inheritdoc />
    public partial class MyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HangSX",
                columns: table => new
                {
                    MaHSX = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHSX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChiNSX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangSX", x => x.MaHSX);
                });

            migrationBuilder.CreateTable(
                name: "HinhThucThanhToan",
                columns: table => new
                {
                    MaHT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhThucThanhToan", x => x.MaHT);
                });

            migrationBuilder.CreateTable(
                name: "LoaiSP",
                columns: table => new
                {
                    MaLoaiSP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenLoaiSP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiSP", x => x.MaLoaiSP);
                });

            migrationBuilder.CreateTable(
                name: "LoaiThuoc",
                columns: table => new
                {
                    MaLT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiThuoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiThuoc", x => x.MaLT);
                });

            migrationBuilder.CreateTable(
                name: "TinTuc",
                columns: table => new
                {
                    MaTT = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGiaDang = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoiDung = table.Column<string>(type: "ntext", nullable: false),
                    MoTaTT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuotXem = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinTuc", x => x.MaTT);
                });

            migrationBuilder.CreateTable(
                name: "TrangThai",
                columns: table => new
                {
                    MaTT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangThai", x => x.MaTT);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    SoDH = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    NgayLap = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TongThanhTien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TenNguoiMua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChiGiao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayGiao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GhiChu = table.Column<string>(type: "ntext", nullable: false),
                    MaHT = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.SoDH);
                    table.ForeignKey(
                        name: "FK_HoaDon_HinhThucThanhToan_MaHT",
                        column: x => x.MaHT,
                        principalTable: "HinhThucThanhToan",
                        principalColumn: "MaHT",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    MaSP = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenSP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CongDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLanMua = table.Column<int>(type: "int", nullable: true),
                    MaLoaiSP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaTT = table.Column<int>(type: "int", nullable: false),
                    MaHSX = table.Column<int>(type: "int", nullable: true),
                    GiamGia = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    DSAnhSP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.MaSP);
                    table.ForeignKey(
                        name: "FK_SanPham_HangSX_MaHSX",
                        column: x => x.MaHSX,
                        principalTable: "HangSX",
                        principalColumn: "MaHSX");
                    table.ForeignKey(
                        name: "FK_SanPham_LoaiSP_MaLoaiSP",
                        column: x => x.MaLoaiSP,
                        principalTable: "LoaiSP",
                        principalColumn: "MaLoaiSP",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPham_TrangThai_MaTT",
                        column: x => x.MaTT,
                        principalTable: "TrangThai",
                        principalColumn: "MaTT",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CT_HoaDon",
                columns: table => new
                {
                    SoDH = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaSP = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CT_HoaDon", x => new { x.SoDH, x.MaSP });
                    table.ForeignKey(
                        name: "FK_CT_HoaDon_HoaDon_SoDH",
                        column: x => x.SoDH,
                        principalTable: "HoaDon",
                        principalColumn: "SoDH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CT_HoaDon_SanPham_MaSP",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    MaSP = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    ThanhTien = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => new { x.Id, x.MaSP });
                    table.ForeignKey(
                        name: "FK_GioHang_SanPham_MaSP",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Thuoc",
                columns: table => new
                {
                    MaSP = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LieuDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TacDungPhu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThanhPhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaLT = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thuoc", x => x.MaSP);
                    table.ForeignKey(
                        name: "FK_Thuoc_LoaiThuoc_MaLT",
                        column: x => x.MaLT,
                        principalTable: "LoaiThuoc",
                        principalColumn: "MaLT",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Thuoc_SanPham_MaSP",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CT_HoaDon_MaSP",
                table: "CT_HoaDon",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_MaSP",
                table: "GioHang",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaHT",
                table: "HoaDon",
                column: "MaHT");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaHSX",
                table: "SanPham",
                column: "MaHSX");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaLoaiSP",
                table: "SanPham",
                column: "MaLoaiSP");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaTT",
                table: "SanPham",
                column: "MaTT");

            migrationBuilder.CreateIndex(
                name: "IX_Thuoc_MaLT",
                table: "Thuoc",
                column: "MaLT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CT_HoaDon");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "Thuoc");

            migrationBuilder.DropTable(
                name: "TinTuc");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "LoaiThuoc");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "HinhThucThanhToan");

            migrationBuilder.DropTable(
                name: "HangSX");

            migrationBuilder.DropTable(
                name: "LoaiSP");

            migrationBuilder.DropTable(
                name: "TrangThai");
        }
    }
}
