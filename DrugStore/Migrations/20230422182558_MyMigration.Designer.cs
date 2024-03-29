﻿// <auto-generated />
using System;
using DrugStore.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DrugStore.Migrations
{
    [DbContext(typeof(DrugStoreDbContext))]
    [Migration("20230422182558_MyMigration")]
    partial class MyMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DrugStore.Models.Entities.CT_HoaDon", b =>
                {
                    b.Property<Guid>("SoDH")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(0);

                    b.Property<Guid>("MaSP")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<int?>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("SoDH", "MaSP");

                    b.HasIndex("MaSP");

                    b.ToTable("CT_HoaDon");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.GioHang", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)")
                        .HasColumnOrder(0);

                    b.Property<Guid>("MaSP")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<int?>("SoLuong")
                        .HasColumnType("int");

                    b.Property<decimal?>("ThanhTien")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id", "MaSP");

                    b.HasIndex("MaSP");

                    b.ToTable("GioHang");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.HangSX", b =>
                {
                    b.Property<int>("MaHSX")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaHSX"));

                    b.Property<string>("DiaChiNSX")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TenHSX")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaHSX");

                    b.ToTable("HangSX");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.HinhThucThanhToan", b =>
                {
                    b.Property<int>("MaHT")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaHT"));

                    b.Property<string>("TenHT")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MaHT");

                    b.ToTable("HinhThucThanhToan");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.HoaDon", b =>
                {
                    b.Property<Guid>("SoDH")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiaChiGiao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int?>("MaHT")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayGiao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayLap")
                        .HasColumnType("datetime2");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenNguoiMua")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("TongThanhTien")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("SoDH");

                    b.HasIndex("MaHT");

                    b.ToTable("HoaDon");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.LoaiSP", b =>
                {
                    b.Property<string>("MaLoaiSP")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TenLoaiSP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaLoaiSP");

                    b.ToTable("LoaiSP");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.LoaiThuoc", b =>
                {
                    b.Property<int>("MaLT")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaLT"));

                    b.Property<string>("TenLoaiThuoc")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MaLT");

                    b.ToTable("LoaiThuoc");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.SanPhamInput", b =>
                {
                    b.Property<Guid>("MaSP")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AnhDaiDien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CongDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DSAnhSP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("DonGia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("GiamGia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("MaHSX")
                        .HasColumnType("int");

                    b.Property<string>("MaLoaiSP")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("MaTT")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SoLanMua")
                        .HasColumnType("int");

                    b.Property<int?>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("TenSP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaSP");

                    b.HasIndex("MaHSX");

                    b.HasIndex("MaLoaiSP");

                    b.HasIndex("MaTT");

                    b.ToTable("SanPham");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.ThuocInput", b =>
                {
                    b.Property<Guid>("MaSP")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DonViTinh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LieuDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaLT")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("TacDungPhu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThanhPhan")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaSP");

                    b.HasIndex("MaLT");

                    b.ToTable("Thuoc");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.TinTuc", b =>
                {
                    b.Property<Guid>("MaTT")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AnhDaiDien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTaTT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.Property<int?>("SoLuotXem")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ThoiGiaDang")
                        .HasColumnType("datetime2");

                    b.HasKey("MaTT");

                    b.ToTable("TinTuc");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.TrangThai", b =>
                {
                    b.Property<int>("MaTT")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaTT"));

                    b.Property<string>("TenTT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaTT");

                    b.ToTable("TrangThai");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.CT_HoaDon", b =>
                {
                    b.HasOne("DrugStore.Models.Entities.SanPhamInput", "SanPham")
                        .WithMany("CT_HoaDon")
                        .HasForeignKey("MaSP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DrugStore.Models.Entities.HoaDon", "HoaDon")
                        .WithMany("CT_HoaDon")
                        .HasForeignKey("SoDH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HoaDon");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.GioHang", b =>
                {
                    b.HasOne("DrugStore.Models.Entities.SanPhamInput", "SanPham")
                        .WithMany("GioHangs")
                        .HasForeignKey("MaSP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.HoaDon", b =>
                {
                    b.HasOne("DrugStore.Models.Entities.HinhThucThanhToan", "HinhThucThanhToan")
                        .WithMany("HoaDons")
                        .HasForeignKey("MaHT")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HinhThucThanhToan");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.SanPhamInput", b =>
                {
                    b.HasOne("DrugStore.Models.Entities.HangSX", "HangSX")
                        .WithMany("SanPhams")
                        .HasForeignKey("MaHSX");

                    b.HasOne("DrugStore.Models.Entities.LoaiSP", "LoaiSP")
                        .WithMany("SanPhams")
                        .HasForeignKey("MaLoaiSP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DrugStore.Models.Entities.TrangThai", "TrangThai")
                        .WithMany("SanPhams")
                        .HasForeignKey("MaTT")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HangSX");

                    b.Navigation("LoaiSP");

                    b.Navigation("TrangThai");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.ThuocInput", b =>
                {
                    b.HasOne("DrugStore.Models.Entities.LoaiThuoc", "LoaiThuoc")
                        .WithMany("Thuocs")
                        .HasForeignKey("MaLT")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DrugStore.Models.Entities.SanPhamInput", "SanPham")
                        .WithOne("Thuoc")
                        .HasForeignKey("DrugStore.Models.Entities.ThuocInput", "MaSP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoaiThuoc");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.HangSX", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.HinhThucThanhToan", b =>
                {
                    b.Navigation("HoaDons");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.HoaDon", b =>
                {
                    b.Navigation("CT_HoaDon");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.LoaiSP", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.LoaiThuoc", b =>
                {
                    b.Navigation("Thuocs");
                });

            modelBuilder.Entity("DrugStore.Models.Entities.SanPhamInput", b =>
                {
                    b.Navigation("CT_HoaDon");

                    b.Navigation("GioHangs");

                    b.Navigation("Thuoc")
                        .IsRequired();
                });

            modelBuilder.Entity("DrugStore.Models.Entities.TrangThai", b =>
                {
                    b.Navigation("SanPhams");
                });
#pragma warning restore 612, 618
        }
    }
}
