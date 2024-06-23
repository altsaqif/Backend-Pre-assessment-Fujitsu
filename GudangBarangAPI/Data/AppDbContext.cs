using Microsoft.EntityFrameworkCore;
using GudangBarangAPI.Models;

namespace GudangBarangAPI.Data
{
    /// <summary>
    /// This class represents a db.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Adds a new db config
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// App db context config
        /// </summary>
        public AppDbContext() : base() {}

        /// <summary>
        /// Gets or sets the db of the warehouse.
        /// </summary>
        public DbSet<Gudang> Gudang
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the db of the item.
        /// </summary>
        public DbSet<Barang> Barang
        {
            get; set;
        }

        /// <summary>
        /// Adds a protected modelbuilder
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Gudang>().ToTable("Gudang")
                            .HasKey(g => g.Id);
            modelBuilder.Entity<Barang>().ToTable("Barang")
                            .HasKey(b => b.Id);
            modelBuilder.Entity<Barang>()
                            .HasOne(b => b.Gudang)
                            .WithMany()
                            .HasForeignKey(b => b.IDGudang);
        }
    }

}