using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace projet_csharp_travel_plan.Models;

public partial class TravelPlanNewDbContext : DbContext
{
    public TravelPlanNewDbContext()
    {
    }

    public TravelPlanNewDbContext(DbContextOptions<TravelPlanNewDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pay> Pays { get; set; }

    public virtual DbSet<Voyage> Voyages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=groupeoneserver.database.windows.net; Initial Catalog=TravelPlanNewDB;User ID=groupeone;Password=.Etml-123;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("French_CI_AS");

        modelBuilder.Entity<Pay>(entity =>
        {
            entity.HasKey(e => e.IdPays).HasName("PK__PAYS__B68ABC4DA8200F56");

            entity.ToTable("PAYS");

            entity.Property(e => e.IdPays).HasColumnName("ID_PAYS");
            entity.Property(e => e.Nom)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<Voyage>(entity =>
        {
            entity.HasKey(e => e.IdVoyage).HasName("PK__VOYAGE__9E4C02B4FAFCF365");

            entity.ToTable("VOYAGE");

            entity.Property(e => e.IdVoyage).HasColumnName("ID_VOYAGE");
            entity.Property(e => e.DateDebut)
                .HasColumnType("datetime")
                .HasColumnName("DATE_DEBUT");
            entity.Property(e => e.DateFin)
                .HasColumnType("datetime")
                .HasColumnName("DATE_FIN");
            entity.Property(e => e.IdClient).HasColumnName("ID_CLIENT");
            entity.Property(e => e.PrixTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PRIX_TOTAL");
            entity.Property(e => e.StatutPaiement).HasColumnName("STATUT_PAIEMENT");

            entity.HasMany(d => d.IdPays).WithMany(p => p.IdVoyages)
                .UsingEntity<Dictionary<string, object>>(
                    "Choisir",
                    r => r.HasOne<Pay>().WithMany()
                        .HasForeignKey("IdPays")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CHOISIR_PAYS"),
                    l => l.HasOne<Voyage>().WithMany()
                        .HasForeignKey("IdVoyage")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CHOISIR_VOYAGE"),
                    j =>
                    {
                        j.HasKey("IdVoyage", "IdPays").HasName("PK__CHOISIR__5524A97030ECC7A9");
                        j.ToTable("CHOISIR");
                        j.IndexerProperty<short>("IdVoyage").HasColumnName("ID_VOYAGE");
                        j.IndexerProperty<short>("IdPays").HasColumnName("ID_PAYS");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
