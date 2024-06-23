﻿// <auto-generated />
using System;
using GudangBarangAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GudangBarangAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240623103255_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GudangBarangAPI.Models.Barang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ExpiredBarang")
                        .HasColumnType("timestamp with time zone")
                        .HasAnnotation("Relational:JsonPropertyName", "expired_barang");

                    b.Property<decimal>("HargaBarang")
                        .HasColumnType("numeric")
                        .HasAnnotation("Relational:JsonPropertyName", "harga_barang");

                    b.Property<int>("IDGudang")
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "id_gudang");

                    b.Property<int>("JumlahBarang")
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "jumlah_barang");

                    b.Property<string>("KodeBarang")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "kode_barang");

                    b.Property<string>("NamaBarang")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "nama_barang");

                    b.HasKey("Id");

                    b.HasIndex("IDGudang");

                    b.ToTable("Barang", (string)null);
                });

            modelBuilder.Entity("GudangBarangAPI.Models.Gudang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("KodeGudang")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "kode_gudang");

                    b.Property<string>("NamaGudang")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "nama_gudang");

                    b.HasKey("Id");

                    b.ToTable("Gudang", (string)null);
                });

            modelBuilder.Entity("GudangBarangAPI.Models.Barang", b =>
                {
                    b.HasOne("GudangBarangAPI.Models.Gudang", "Gudang")
                        .WithMany()
                        .HasForeignKey("IDGudang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gudang");
                });
#pragma warning restore 612, 618
        }
    }
}
