using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Shop_Help.Models
{
    public partial class ShopHelpContext : DbContext
    {
        public ShopHelpContext()
        {
        }

        public ShopHelpContext(DbContextOptions<ShopHelpContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Itemcost> Itemcost { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<Itemtype> Itemtype { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<Stores> Stores { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-0TGLH2D\\SQLEXPRESS;Database=ShopHelp;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Itemcost>(entity =>
            {
                entity.ToTable("itemcost");

                entity.Property(e => e.Itemcostid)
                    .HasColumnName("itemcostid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Itemcost1)
                    .HasColumnName("itemcost")
                    .HasColumnType("money");

                entity.Property(e => e.Itemid).HasColumnName("itemid");

                entity.Property(e => e.Storeid).HasColumnName("storeid");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Itemcost)
                    .HasForeignKey(d => d.Itemid)
                    .HasConstraintName("FK__itemcost__itemid__49C3F6B7");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Itemcost)
                    .HasForeignKey(d => d.Storeid)
                    .HasConstraintName("FK__itemcost__storei__48CFD27E");
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.HasKey(e => e.Itemid)
                    .HasName("PK__items__56A22C92B89FF3EF");

                entity.ToTable("items");

                entity.Property(e => e.Itemid)
                    .HasColumnName("itemid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Itemname)
                    .HasColumnName("itemname")
                    .HasColumnType("text");

                entity.Property(e => e.Itemtypeid).HasColumnName("itemtypeid");

                entity.HasOne(d => d.Itemtype)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.Itemtypeid)
                    .HasConstraintName("FK__items__itemtypei__45F365D3");
            });

            modelBuilder.Entity<Itemtype>(entity =>
            {
                entity.ToTable("itemtype");

                entity.Property(e => e.Itemtypeid)
                    .HasColumnName("itemtypeid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Itemtypename)
                    .HasColumnName("itemtypename")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Locations>(entity =>
            {
                entity.HasKey(e => e.Locationid)
                    .HasName("PK__location__306F78A680D89B85");

                entity.ToTable("locations");

                entity.Property(e => e.Locationid)
                    .HasColumnName("locationid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Zipcode).HasColumnName("zipcode");
            });

            modelBuilder.Entity<Stores>(entity =>
            {
                entity.HasKey(e => e.Storeid)
                    .HasName("PK__stores__01A2160BE1CCA1FD");

                entity.ToTable("stores");

                entity.Property(e => e.Storeid)
                    .HasColumnName("storeid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Locationid).HasColumnName("locationid");

                entity.Property(e => e.Storeaddress)
                    .HasColumnName("storeaddress")
                    .HasColumnType("text");

                entity.Property(e => e.Storename)
                    .HasColumnName("storename")
                    .HasColumnType("text");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.Locationid)
                    .HasConstraintName("FK__stores__location__398D8EEE");
            });
        }
    }
}
