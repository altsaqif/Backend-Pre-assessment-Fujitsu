using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GudangBarangAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gudang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KodeGudang = table.Column<string>(type: "text", nullable: false),
                    NamaGudang = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gudang", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Barang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KodeBarang = table.Column<string>(type: "text", nullable: false),
                    IDGudang = table.Column<int>(type: "integer", nullable: false),
                    NamaBarang = table.Column<string>(type: "text", nullable: false),
                    HargaBarang = table.Column<decimal>(type: "numeric", nullable: false),
                    JumlahBarang = table.Column<int>(type: "integer", nullable: false),
                    ExpiredBarang = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Barang_Gudang_IDGudang",
                        column: x => x.IDGudang,
                        principalTable: "Gudang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Barang_IDGudang",
                table: "Barang",
                column: "IDGudang");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Barang");

            migrationBuilder.DropTable(
                name: "Gudang");
        }
    }
}
