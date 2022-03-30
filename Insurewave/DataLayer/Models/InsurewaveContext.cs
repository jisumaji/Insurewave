using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataLayer.Models
{
    public partial class InsurewaveContext : DbContext
    {
        public InsurewaveContext()
        {
        }

        public InsurewaveContext(DbContextOptions<InsurewaveContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BrokerDetail> BrokerDetails { get; set; }
        public virtual DbSet<BrokerInsurer> BrokerInsurers { get; set; }
        public virtual DbSet<BuyerAsset> BuyerAssets { get; set; }
        public virtual DbSet<CurrencyConversion> CurrencyConversions { get; set; }
        public virtual DbSet<InsurerDetail> InsurerDetails { get; set; }
        public virtual DbSet<PolicyDetail> PolicyDetails { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= SWETASARKAR\\MSSQLSERVER03;Database=Insurewave;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BrokerDetail>(entity =>
            {
                entity.HasKey(e => e.BrokerId)
                    .HasName("PK__Broker.D__5D1D9A505DBAC72B");

                entity.ToTable("Broker.Details");

                entity.HasIndex(e => e.LicenseId, "UQ__Broker.D__72D600831DC706DE")
                    .IsUnique();

                entity.Property(e => e.BrokerId).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BrokerDetails)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Broker.De__UserI__32E0915F");
            });

            modelBuilder.Entity<BrokerInsurer>(entity =>
            {
                entity.HasKey(e => e.Biid)
                    .HasName("PK__Broker.I__3B3C5D6967A3C1CC");

                entity.ToTable("Broker.Insurer");

                entity.Property(e => e.Biid)
                    .ValueGeneratedNever()
                    .HasColumnName("BIId");

                entity.Property(e => e.AssetId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.BrokerId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.BrokerStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.InsurerId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.PolicyId).HasDefaultValueSql("((-1))");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.BrokerInsurers)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FK__Broker.In__Asset__4316F928");

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.BrokerInsurers)
                    .HasForeignKey(d => d.BrokerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Broker.In__Broke__3F466844");

                entity.HasOne(d => d.Insurer)
                    .WithMany(p => p.BrokerInsurers)
                    .HasForeignKey(d => d.InsurerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Broker.In__Insur__412EB0B6");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.BrokerInsurers)
                    .HasForeignKey(d => d.PolicyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Broker.In__Polic__45F365D3");
            });

            modelBuilder.Entity<BuyerAsset>(entity =>
            {
                entity.HasKey(e => e.AssetId)
                    .HasName("PK__Buyer.As__43492352423C0FF7");

                entity.ToTable("Buyer.Assets");

                entity.Property(e => e.AssetId).ValueGeneratedNever();

                entity.Property(e => e.AssetName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId).HasDefaultValueSql("((1))");

                entity.Property(e => e.PriceUsd)
                    .HasColumnType("money")
                    .HasColumnName("PriceUSD");

                entity.Property(e => e.Request)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('no')");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.BuyerAssets)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__Buyer.Ass__Count__2C3393D0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BuyerAssets)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Buyer.Ass__UserI__2B3F6F97");
            });

            modelBuilder.Entity<CurrencyConversion>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PK__Currency__10D1609FE673318B");

                entity.ToTable("CurrencyConversion");

                entity.HasIndex(e => e.CountryName, "UQ__Currency__E056F201A81A60F6")
                    .IsUnique();

                entity.Property(e => e.CountryId).ValueGeneratedNever();

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InsurerDetail>(entity =>
            {
                entity.HasKey(e => e.InsurerId)
                    .HasName("PK__Insurer.__7E508CE65F7F28FA");

                entity.ToTable("Insurer.Details");

                entity.HasIndex(e => e.LicenseId, "UQ__Insurer.__72D6008382A8B672")
                    .IsUnique();

                entity.Property(e => e.InsurerId).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InsurerDetails)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Insurer.D__UserI__36B12243");
            });

            modelBuilder.Entity<PolicyDetail>(entity =>
            {
                entity.HasKey(e => e.PolicyId)
                    .HasName("PK__PolicyDe__2E1339A40CC6C047");

                entity.Property(e => e.PolicyId).ValueGeneratedNever();

                entity.Property(e => e.Feedback).IsUnicode(false);

                entity.Property(e => e.InsurerId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.LumpSum).HasColumnType("money");

                entity.Property(e => e.MaturityAmount).HasColumnType("money");

                entity.Property(e => e.PolicyStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Premium).HasColumnType("money");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.PolicyDetails)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FK__PolicyDet__Asset__398D8EEE");

                entity.HasOne(d => d.Insurer)
                    .WithMany(p => p.PolicyDetails)
                    .HasForeignKey(d => d.InsurerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PolicyDet__Insur__3A81B327");
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__User.Det__1788CC4C1E06FE05");

                entity.ToTable("User.Details");

                entity.HasIndex(e => e.MailId, "UQ__User.Det__09A8749BA9D466C8")
                    .IsUnique();

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MailId)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
