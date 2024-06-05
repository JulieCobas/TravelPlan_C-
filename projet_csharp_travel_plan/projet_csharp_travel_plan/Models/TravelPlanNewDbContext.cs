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

    public virtual DbSet<Activite> Activites { get; set; }

    public virtual DbSet<ActiviteCategorie> ActiviteCategories { get; set; }

    public virtual DbSet<ActiviteOption> ActiviteOptions { get; set; }

    public virtual DbSet<ActivitePrix> ActivitePrixes { get; set; }

    public virtual DbSet<CategoriePrix> CategoriePrixes { get; set; }

    public virtual DbSet<Chambre> Chambres { get; set; }

    public virtual DbSet<ChambreEquipement> ChambreEquipements { get; set; }

    public virtual DbSet<ChambreOption> ChambreOptions { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<DisponibiliteLogement> DisponibiliteLogements { get; set; }

    public virtual DbSet<DisponibiliteTransport> DisponibiliteTransports { get; set; }

    public virtual DbSet<EquipementCategorie> EquipementCategories { get; set; }

    public virtual DbSet<Fournisseur> Fournisseurs { get; set; }

    public virtual DbSet<Invite> Invites { get; set; }

    public virtual DbSet<Logement> Logements { get; set; }

    public virtual DbSet<LogementCategorie> LogementCategories { get; set; }

    public virtual DbSet<NumChambre> NumChambres { get; set; }

    public virtual DbSet<Paiement> Paiements { get; set; }

    public virtual DbSet<Pay> Pays { get; set; }

    public virtual DbSet<PrixLogement> PrixLogements { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Siege> Sieges { get; set; }

    public virtual DbSet<Transport> Transports { get; set; }

    public virtual DbSet<TransportCategorie> TransportCategories { get; set; }

    public virtual DbSet<TransportOption> TransportOptions { get; set; }

    public virtual DbSet<TransportPrix> TransportPrixes { get; set; }

    public virtual DbSet<VehiculeLocation> VehiculeLocations { get; set; }

    public virtual DbSet<Ville> Villes { get; set; }

    public virtual DbSet<Voyage> Voyages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("French_CI_AS");

        modelBuilder.Entity<Activite>(entity =>
        {
            entity.HasKey(e => e.IdActivite).HasName("PK__ACTIVITE__BE6F331202AE0FEF");

            entity.ToTable("ACTIVITE");

            entity.HasIndex(e => e.IdCatActiv, "IX_ACTIVITE_ID_CAT_ACTIV");

            entity.HasIndex(e => e.IdFournisseur, "IX_ACTIVITE_ID_FOURNISSEUR");

            entity.HasIndex(e => e.IdOptionActivite, "IX_ACTIVITE_ID_OPTION_ACTIVITE");

            entity.HasIndex(e => e.IdPays, "IX_ACTIVITE_ID_PAYS");

            entity.HasIndex(e => e.IdPrixActivite, "IX_ACTIVITE_ID_PRIX_ACTIVITE");

            entity.Property(e => e.IdActivite).HasColumnName("ID_ACTIVITE");
            entity.Property(e => e.CapaciteMax).HasColumnName("CAPACITE_MAX");
            entity.Property(e => e.Details)
                .HasColumnType("text")
                .HasColumnName("DETAILS");
            entity.Property(e => e.HeuresMoyennes)
                .HasColumnType("datetime")
                .HasColumnName("HEURES_MOYENNES");
            entity.Property(e => e.IdCatActiv).HasColumnName("ID_CAT_ACTIV");
            entity.Property(e => e.IdFournisseur).HasColumnName("ID_FOURNISSEUR");
            entity.Property(e => e.IdOptionActivite).HasColumnName("ID_OPTION_ACTIVITE");
            entity.Property(e => e.IdPays).HasColumnName("ID_PAYS");
            entity.Property(e => e.IdPrixActivite).HasColumnName("ID_PRIX_ACTIVITE");
            entity.Property(e => e.Img)
                .HasColumnType("image")
                .HasColumnName("IMG");
            entity.Property(e => e.NbEvaluation).HasColumnName("NB_EVALUATION");
            entity.Property(e => e.Nom)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("NOM");
            entity.Property(e => e.Note).HasColumnName("NOTE");

            entity.HasOne(d => d.IdCatActivNavigation).WithMany(p => p.Activites)
                .HasForeignKey(d => d.IdCatActiv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ACTIVITE_ACTIVITE_CATEGORIE");

            entity.HasOne(d => d.IdFournisseurNavigation).WithMany(p => p.Activites)
                .HasForeignKey(d => d.IdFournisseur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ACTIVITE_FOURNISSEUR");

            entity.HasOne(d => d.IdOptionActiviteNavigation).WithMany(p => p.Activites)
                .HasForeignKey(d => d.IdOptionActivite)
                .HasConstraintName("FK_ACTIVITE_ACTIVITE_OPTION");

            entity.HasOne(d => d.IdPaysNavigation).WithMany(p => p.Activites)
                .HasForeignKey(d => d.IdPays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ACTIVITE_PAYS");

            entity.HasOne(d => d.IdPrixActiviteNavigation).WithMany(p => p.Activites)
                .HasForeignKey(d => d.IdPrixActivite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ACTIVITE_ACTIVITE_PRIX");
        });

        modelBuilder.Entity<ActiviteCategorie>(entity =>
        {
            entity.HasKey(e => e.IdCatActiv).HasName("PK__ACTIVITE__8AED20E33FEEECA8");

            entity.ToTable("ACTIVITE_CATEGORIE");

            entity.Property(e => e.IdCatActiv).HasColumnName("ID_CAT_ACTIV");
            entity.Property(e => e.Nom)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<ActiviteOption>(entity =>
        {
            entity.HasKey(e => e.IdOptionActivite).HasName("PK__ACTIVITE__F19E116B9100E9D1");

            entity.ToTable("ACTIVITE_OPTION");

            entity.Property(e => e.IdOptionActivite).HasColumnName("ID_OPTION_ACTIVITE");
            entity.Property(e => e.GuideAudio).HasColumnName("GUIDE_AUDIO");
            entity.Property(e => e.PrixGuideAudio).HasColumnName("PRIX_GUIDE_AUDIO");
            entity.Property(e => e.PrixVisiteGuide).HasColumnName("PRIX_VISITE_GUIDE");
            entity.Property(e => e.VisiteGuidee).HasColumnName("VISITE_GUIDEE");
        });

        modelBuilder.Entity<ActivitePrix>(entity =>
        {
            entity.HasKey(e => e.IdPrixActivite).HasName("PK__ACTIVITE__D00D170B12A097CF");

            entity.ToTable("ACTIVITE_PRIX");

            entity.Property(e => e.IdPrixActivite).HasColumnName("ID_PRIX_ACTIVITE");
            entity.Property(e => e.DateDebutValidite)
                .HasColumnType("datetime")
                .HasColumnName("DATE_DEBUT_VALIDITE");
            entity.Property(e => e.DateFinValidite)
                .HasColumnType("datetime")
                .HasColumnName("DATE_FIN_VALIDITE");
            entity.Property(e => e.Prix)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PRIX");
        });

        modelBuilder.Entity<CategoriePrix>(entity =>
        {
            entity.HasKey(e => e.IdCategoriePrix).HasName("PK__CATEGORI__252DB365B3E09517");

            entity.ToTable("CATEGORIE_PRIX");

            entity.Property(e => e.IdCategoriePrix).HasColumnName("ID_CATEGORIE_PRIX");
            entity.Property(e => e.Nom)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("NOM");

            entity.HasMany(d => d.IdLogementPrixes).WithMany(p => p.IdCategoriePrixes)
                .UsingEntity<Dictionary<string, object>>(
                    "SeRefereLogement",
                    r => r.HasOne<PrixLogement>().WithMany()
                        .HasForeignKey("IdLogementPrix")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SE_REFERE_LOGEMENT_PRIX_LOGEMENT"),
                    l => l.HasOne<CategoriePrix>().WithMany()
                        .HasForeignKey("IdCategoriePrix")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SE_REFERE_LOGEMENT_CATEGORIE_PRIX"),
                    j =>
                    {
                        j.HasKey("IdCategoriePrix", "IdLogementPrix").HasName("PK__SE_REFER__90282C1D1B670CBB");
                        j.ToTable("SE_REFERE_LOGEMENT");
                        j.HasIndex(new[] { "IdLogementPrix" }, "IX_SE_REFERE_LOGEMENT_ID_LOGEMENT_PRIX");
                        j.IndexerProperty<short>("IdCategoriePrix").HasColumnName("ID_CATEGORIE_PRIX");
                        j.IndexerProperty<short>("IdLogementPrix").HasColumnName("ID_LOGEMENT_PRIX");
                    });

            entity.HasMany(d => d.IdPrixActivites).WithMany(p => p.IdCategoriePrixes)
                .UsingEntity<Dictionary<string, object>>(
                    "SeRefereActiv",
                    r => r.HasOne<ActivitePrix>().WithMany()
                        .HasForeignKey("IdPrixActivite")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SE_REFERE_ACTIV_ACTIVITE_PRIX"),
                    l => l.HasOne<CategoriePrix>().WithMany()
                        .HasForeignKey("IdCategoriePrix")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SE_REFERE_ACTIV_CATEGORIE_PRIX"),
                    j =>
                    {
                        j.HasKey("IdCategoriePrix", "IdPrixActivite").HasName("PK__SE_REFER__882D621502DC5ECF");
                        j.ToTable("SE_REFERE_ACTIV");
                        j.HasIndex(new[] { "IdPrixActivite" }, "IX_SE_REFERE_ACTIV_ID_PRIX_ACTIVITE");
                        j.IndexerProperty<short>("IdCategoriePrix").HasColumnName("ID_CATEGORIE_PRIX");
                        j.IndexerProperty<short>("IdPrixActivite").HasColumnName("ID_PRIX_ACTIVITE");
                    });

            entity.HasMany(d => d.IdPrixTransports).WithMany(p => p.IdCategoriePrixes)
                .UsingEntity<Dictionary<string, object>>(
                    "SeRefereTransp",
                    r => r.HasOne<TransportPrix>().WithMany()
                        .HasForeignKey("IdPrixTransport")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SE_REFERE_TRANSP_TRANSPORT_PRIX"),
                    l => l.HasOne<CategoriePrix>().WithMany()
                        .HasForeignKey("IdCategoriePrix")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SE_REFERE_TRANSP_CATEGORIE_PRIX"),
                    j =>
                    {
                        j.HasKey("IdCategoriePrix", "IdPrixTransport").HasName("PK__SE_REFER__A9334B4949A3637B");
                        j.ToTable("SE_REFERE_TRANSP");
                        j.HasIndex(new[] { "IdPrixTransport" }, "IX_SE_REFERE_TRANSP_ID_PRIX_TRANSPORT");
                        j.IndexerProperty<short>("IdCategoriePrix").HasColumnName("ID_CATEGORIE_PRIX");
                        j.IndexerProperty<short>("IdPrixTransport").HasColumnName("ID_PRIX_TRANSPORT");
                    });
        });

        modelBuilder.Entity<Chambre>(entity =>
        {
            entity.HasKey(e => e.IdChambre).HasName("PK__CHAMBRE__67377BD2A8D06D42");

            entity.ToTable("CHAMBRE");

            entity.HasIndex(e => e.IdChambreOption, "IX_CHAMBRE_ID_CHAMBRE_OPTION");

            entity.HasIndex(e => e.IdLogement, "IX_CHAMBRE_ID_LOGEMENT");

            entity.HasIndex(e => e.IdLogementPrix, "IX_CHAMBRE_ID_LOGEMENT_PRIX");

            entity.Property(e => e.IdChambre).HasColumnName("ID_CHAMBRE");
            entity.Property(e => e.DetailsChambre)
                .HasColumnType("text")
                .HasColumnName("DETAILS_CHAMBRE");
            entity.Property(e => e.IdChambreOption).HasColumnName("ID_CHAMBRE_OPTION");
            entity.Property(e => e.IdLogement).HasColumnName("ID_LOGEMENT");
            entity.Property(e => e.IdLogementPrix).HasColumnName("ID_LOGEMENT_PRIX");
            entity.Property(e => e.NbOccupants).HasColumnName("NB_OCCUPANTS");
            entity.Property(e => e.Nom)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("NOM");
            entity.Property(e => e.Surface).HasColumnName("SURFACE");
            entity.Property(e => e.TypeDeChambre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("TYPE_DE_CHAMBRE");

            entity.HasOne(d => d.IdChambreOptionNavigation).WithMany(p => p.Chambres)
                .HasForeignKey(d => d.IdChambreOption)
                .HasConstraintName("FK_CHAMBRE_CHAMBRE_OPTION");

            entity.HasOne(d => d.IdLogementNavigation).WithMany(p => p.Chambres)
                .HasForeignKey(d => d.IdLogement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHAMBRE_LOGEMENT");

            entity.HasOne(d => d.IdLogementPrixNavigation).WithMany(p => p.Chambres)
                .HasForeignKey(d => d.IdLogementPrix)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHAMBRE_PRIX_LOGEMENT");

            entity.HasMany(d => d.IdEquipChambres).WithMany(p => p.IdChambres)
                .UsingEntity<Dictionary<string, object>>(
                    "ChambreEquipe",
                    r => r.HasOne<ChambreEquipement>().WithMany()
                        .HasForeignKey("IdEquipChambre")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CHAMBRE_EQUIPE_CHAMBRE_EQUIPEMENT"),
                    l => l.HasOne<Chambre>().WithMany()
                        .HasForeignKey("IdChambre")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CHAMBRE_EQUIPE_CHAMBRE"),
                    j =>
                    {
                        j.HasKey("IdChambre", "IdEquipChambre").HasName("PK__CHAMBRE___9BF2CF5B4E734436");
                        j.ToTable("CHAMBRE_EQUIPE");
                        j.HasIndex(new[] { "IdEquipChambre" }, "IX_CHAMBRE_EQUIPE_ID_EQUIP_CHAMBRE");
                        j.IndexerProperty<short>("IdChambre").HasColumnName("ID_CHAMBRE");
                        j.IndexerProperty<short>("IdEquipChambre").HasColumnName("ID_EQUIP_CHAMBRE");
                    });
        });

        modelBuilder.Entity<ChambreEquipement>(entity =>
        {
            entity.HasKey(e => e.IdEquipChambre).HasName("PK__CHAMBRE___CC5B489E730EE6BC");

            entity.ToTable("CHAMBRE_EQUIPEMENT");

            entity.Property(e => e.IdEquipChambre).HasColumnName("ID_EQUIP_CHAMBRE");
            entity.Property(e => e.Nom)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<ChambreOption>(entity =>
        {
            entity.HasKey(e => e.IdChambreOption).HasName("PK__CHAMBRE___06491AFF7652D34B");

            entity.ToTable("CHAMBRE_OPTION");

            entity.Property(e => e.IdChambreOption).HasColumnName("ID_CHAMBRE_OPTION");
            entity.Property(e => e.AnnulationGratuite).HasColumnName("ANNULATION_GRATUITE");
            entity.Property(e => e.DateAnnulationGratuite)
                .HasColumnType("datetime")
                .HasColumnName("DATE_ANNULATION_GRATUITE");
            entity.Property(e => e.PetitDejeunerInclus).HasColumnName("PETIT_DEJEUNER_INCLUS");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("PK__CLIENT__5556D89B5C6273D1");

            entity.ToTable("CLIENT");

            entity.Property(e => e.IdClient)
                .ValueGeneratedNever()
                .HasColumnName("ID_CLIENT");
            entity.Property(e => e.Addresse)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("ADDRESSE");
            entity.Property(e => e.Cp)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CP");
            entity.Property(e => e.DateNaissance)
                .HasColumnType("date")
                .HasColumnName("DATE_NAISSANCE");
            entity.Property(e => e.Id)
                .HasMaxLength(450)
                .HasColumnName("ID");
            entity.Property(e => e.Nom)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("NOM");
            entity.Property(e => e.Pays)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("PAYS");
            entity.Property(e => e.Prenom)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("PRENOM");
            entity.Property(e => e.Ville)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("VILLE");
        });

        modelBuilder.Entity<DisponibiliteLogement>(entity =>
        {
            entity.HasKey(e => new { e.IdChambre, e.IdNumChambre }).HasName("PK__DISPONIB__5FD28D3EC9E0B01A");

            entity.ToTable("DISPONIBILITE_LOGEMENT");

            entity.HasIndex(e => e.IdNumChambre, "IX_DISPONIBILITE_LOGEMENT_ID_NUM_CHAMBRE");

            entity.Property(e => e.IdChambre).HasColumnName("ID_CHAMBRE");
            entity.Property(e => e.IdNumChambre).HasColumnName("ID_NUM_CHAMBRE");
            entity.Property(e => e.Disponible).HasColumnName("DISPONIBLE");

            entity.HasOne(d => d.IdChambreNavigation).WithMany(p => p.DisponibiliteLogements)
                .HasForeignKey(d => d.IdChambre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DISPONIBILITE_LOGEMENT_CHAMBRE");

            entity.HasOne(d => d.IdNumChambreNavigation).WithMany(p => p.DisponibiliteLogements)
                .HasForeignKey(d => d.IdNumChambre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DISPONIBILITE_LOGEMENT_NUM_CHAMBRE");
        });

        modelBuilder.Entity<DisponibiliteTransport>(entity =>
        {
            entity.HasKey(e => new { e.IdSiege, e.IdTransport }).HasName("PK__DISPONIB__4A3158F0067F17B4");

            entity.ToTable("DISPONIBILITE_TRANSPORT");

            entity.HasIndex(e => e.IdTransport, "IX_DISPONIBILITE_TRANSPORT_ID_TRANSPORT");

            entity.Property(e => e.IdSiege).HasColumnName("ID_SIEGE");
            entity.Property(e => e.IdTransport).HasColumnName("ID_TRANSPORT");
            entity.Property(e => e.Disponible).HasColumnName("DISPONIBLE");

            entity.HasOne(d => d.IdSiegeNavigation).WithMany(p => p.DisponibiliteTransports)
                .HasForeignKey(d => d.IdSiege)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DISPONIBILITE_TRANSPORT_SIEGE");

            entity.HasOne(d => d.IdTransportNavigation).WithMany(p => p.DisponibiliteTransports)
                .HasForeignKey(d => d.IdTransport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DISPONIBILITE_TRANSPORT_TRANSPORT");
        });

        modelBuilder.Entity<EquipementCategorie>(entity =>
        {
            entity.HasKey(e => e.IdCatEquipement).HasName("PK__EQUIPEME__6C4F33F8F4F4153A");

            entity.ToTable("EQUIPEMENT_CATEGORIE");

            entity.Property(e => e.IdCatEquipement).HasColumnName("ID_CAT_EQUIPEMENT");
            entity.Property(e => e.Nom)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("NOM");

            entity.HasMany(d => d.IdLogements).WithMany(p => p.IdCatEquipements)
                .UsingEntity<Dictionary<string, object>>(
                    "AssocieEquipementCategorie",
                    r => r.HasOne<Logement>().WithMany()
                        .HasForeignKey("IdLogement")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ASSOCIE_EQUIPEMENT_CATEGORIE_LOGEMENT"),
                    l => l.HasOne<EquipementCategorie>().WithMany()
                        .HasForeignKey("IdCatEquipement")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ASSOCIE_EQUIPEMENT_CATEGORIE_EQUIPEMENT_CATEGORIE"),
                    j =>
                    {
                        j.HasKey("IdCatEquipement", "IdLogement").HasName("PK__ASSOCIE___43D2558AD9C46795");
                        j.ToTable("ASSOCIE_EQUIPEMENT_CATEGORIE");
                        j.HasIndex(new[] { "IdLogement" }, "IX_ASSOCIE_EQUIPEMENT_CATEGORIE_ID_LOGEMENT");
                        j.IndexerProperty<short>("IdCatEquipement").HasColumnName("ID_CAT_EQUIPEMENT");
                        j.IndexerProperty<short>("IdLogement").HasColumnName("ID_LOGEMENT");
                    });
        });

        modelBuilder.Entity<Fournisseur>(entity =>
        {
            entity.HasKey(e => e.IdFournisseur).HasName("PK__FOURNISS__F955D0EA7DC5DAED");

            entity.ToTable("FOURNISSEUR");

            entity.Property(e => e.IdFournisseur).HasColumnName("ID_FOURNISSEUR");
            entity.Property(e => e.Adresse)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("ADRESSE");
            entity.Property(e => e.CompteBancaire)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("COMPTE_BANCAIRE");
            entity.Property(e => e.Cp)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CP");
            entity.Property(e => e.Mail)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("MAIL");
            entity.Property(e => e.NomCompagnie)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOM_COMPAGNIE");
            entity.Property(e => e.Pays)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("PAYS");
            entity.Property(e => e.Telephone)
                .HasMaxLength(24)
                .IsUnicode(false)
                .HasColumnName("TELEPHONE");
            entity.Property(e => e.Ville)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("VILLE");
        });

        modelBuilder.Entity<Invite>(entity =>
        {
            entity.HasKey(e => e.IdInvitee).HasName("PK__INVITE__D0E96D4E9108CC5A");

            entity.ToTable("INVITE");

            entity.Property(e => e.IdInvitee).HasColumnName("ID_INVITEE");
            entity.Property(e => e.DateNaissance)
                .HasColumnType("datetime")
                .HasColumnName("DATE_NAISSANCE");
            entity.Property(e => e.Mail)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("MAIL");
            entity.Property(e => e.Nom)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("NOM");
            entity.Property(e => e.Prenom)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("PRENOM");

            entity.HasMany(d => d.IdReservations).WithMany(p => p.IdInvitees)
                .UsingEntity<Dictionary<string, object>>(
                    "Participe",
                    r => r.HasOne<Reservation>().WithMany()
                        .HasForeignKey("IdReservation")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PARTICIPE_RESERVATION"),
                    l => l.HasOne<Invite>().WithMany()
                        .HasForeignKey("IdInvitee")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PARTICIPE_INVITE"),
                    j =>
                    {
                        j.HasKey("IdInvitee", "IdReservation").HasName("PK__PARTICIP__73221ED67D00B44C");
                        j.ToTable("PARTICIPE");
                        j.HasIndex(new[] { "IdReservation" }, "IX_PARTICIPE_ID_RESERVATION");
                        j.IndexerProperty<short>("IdInvitee").HasColumnName("ID_INVITEE");
                        j.IndexerProperty<short>("IdReservation").HasColumnName("ID_RESERVATION");
                    });
        });

        modelBuilder.Entity<Logement>(entity =>
        {
            entity.HasKey(e => e.IdLogement).HasName("PK__LOGEMENT__F9D66723377A15D5");

            entity.ToTable("LOGEMENT");

            entity.HasIndex(e => e.IdFournisseur, "IX_LOGEMENT_ID_FOURNISSEUR");

            entity.HasIndex(e => e.IdLogementCategorie, "IX_LOGEMENT_ID_LOGEMENT_CATEGORIE");

            entity.HasIndex(e => e.IdLogementPrix, "IX_LOGEMENT_ID_LOGEMENT_PRIX");

            entity.HasIndex(e => e.IdPays, "IX_LOGEMENT_ID_PAYS");

            entity.Property(e => e.IdLogement).HasColumnName("ID_LOGEMENT");
            entity.Property(e => e.Details)
                .HasColumnType("text")
                .HasColumnName("DETAILS");
            entity.Property(e => e.Disponibilite).HasColumnName("DISPONIBILITE");
            entity.Property(e => e.IdFournisseur).HasColumnName("ID_FOURNISSEUR");
            entity.Property(e => e.IdLogementCategorie).HasColumnName("ID_LOGEMENT_CATEGORIE");
            entity.Property(e => e.IdLogementPrix).HasColumnName("ID_LOGEMENT_PRIX");
            entity.Property(e => e.IdPays).HasColumnName("ID_PAYS");
            entity.Property(e => e.Img)
                .HasColumnType("image")
                .HasColumnName("IMG");
            entity.Property(e => e.NbEvaluation).HasColumnName("NB_EVALUATION");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOM");
            entity.Property(e => e.Note).HasColumnName("NOTE");

            entity.HasOne(d => d.IdFournisseurNavigation).WithMany(p => p.Logements)
                .HasForeignKey(d => d.IdFournisseur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOGEMENT_FOURNISSEUR");

            entity.HasOne(d => d.IdLogementCategorieNavigation).WithMany(p => p.Logements)
                .HasForeignKey(d => d.IdLogementCategorie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOGEMENT_LOGEMENT_CATEGORIE");

            entity.HasOne(d => d.IdLogementPrixNavigation).WithMany(p => p.Logements)
                .HasForeignKey(d => d.IdLogementPrix)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOGEMENT_PRIX_LOGEMENT");

            entity.HasOne(d => d.IdPaysNavigation).WithMany(p => p.Logements)
                .HasForeignKey(d => d.IdPays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOGEMENT_PAYS");
        });

        modelBuilder.Entity<LogementCategorie>(entity =>
        {
            entity.HasKey(e => e.IdLogementCategorie).HasName("PK__LOGEMENT__4CEB46809D416567");

            entity.ToTable("LOGEMENT_CATEGORIE");

            entity.Property(e => e.IdLogementCategorie).HasColumnName("ID_LOGEMENT_CATEGORIE");
            entity.Property(e => e.Nom)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<NumChambre>(entity =>
        {
            entity.HasKey(e => e.IdNumChambre).HasName("PK__NUM_CHAM__8E5F6EC2DD2522D9");

            entity.ToTable("NUM_CHAMBRE");

            entity.Property(e => e.IdNumChambre).HasColumnName("ID_NUM_CHAMBRE");
            entity.Property(e => e.NumeroChambre).HasColumnName("NUMERO_CHAMBRE");
        });

        modelBuilder.Entity<Paiement>(entity =>
        {
            entity.HasKey(e => e.IdPaiement).HasName("PK__PAIEMENT__2390D18204E89F3E");

            entity.ToTable("PAIEMENT");

            entity.HasIndex(e => e.IdClient, "IX_PAIEMENT_ID_CLIENT");

            entity.Property(e => e.IdPaiement).HasColumnName("ID_PAIEMENT");
            entity.Property(e => e.Crypto).HasColumnName("CRYPTO");
            entity.Property(e => e.DateExpiration)
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRATION");
            entity.Property(e => e.IdClient).HasColumnName("ID_CLIENT");
            entity.Property(e => e.IdUtilisateur).HasColumnName("ID_UTILISATEUR");
            entity.Property(e => e.NumeroCarteBancaire).HasColumnName("NUMERO_CARTE_BANCAIRE");
            entity.Property(e => e.TypeCarteBancaire)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("TYPE_CARTE_BANCAIRE");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Paiements)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_PAIEMENT_CLIENT");
        });

        modelBuilder.Entity<Pay>(entity =>
        {
            entity.HasKey(e => e.IdPays).HasName("PK__PAYS__B68ABC4DA2C3D9CC");

            entity.ToTable("PAYS");

            entity.Property(e => e.IdPays).HasColumnName("ID_PAYS");
            entity.Property(e => e.Nom)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<PrixLogement>(entity =>
        {
            entity.HasKey(e => e.IdLogementPrix).HasName("PK__PRIX_LOG__5059F78AA5C3E682");

            entity.ToTable("PRIX_LOGEMENT");

            entity.Property(e => e.IdLogementPrix).HasColumnName("ID_LOGEMENT_PRIX");
            entity.Property(e => e.DateDebutValidite)
                .HasColumnType("datetime")
                .HasColumnName("DATE_DEBUT_VALIDITE");
            entity.Property(e => e.DateFinValidite)
                .HasColumnType("datetime")
                .HasColumnName("DATE_FIN_VALIDITE");
            entity.Property(e => e.Prix)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PRIX");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.IdRegion).HasName("PK__REGION__D8BB64B09BF23A17");

            entity.ToTable("REGION");

            entity.HasIndex(e => e.IdPays, "IX_REGION_ID_PAYS");

            entity.Property(e => e.IdRegion).HasColumnName("ID_REGION");
            entity.Property(e => e.IdPays).HasColumnName("ID_PAYS");
            entity.Property(e => e.Nom)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("NOM");

            entity.HasOne(d => d.IdPaysNavigation).WithMany(p => p.Regions)
                .HasForeignKey(d => d.IdPays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REGION_PAYS");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.IdReservation).HasName("PK__RESERVAT__3CB7398B9A29C2AD");

            entity.ToTable("RESERVATION");

            entity.HasIndex(e => e.IdActivite, "IX_RESERVATION_ID_ACTIVITE");

            entity.HasIndex(e => e.IdLogement, "IX_RESERVATION_ID_LOGEMENT");

            entity.HasIndex(e => e.IdTransport, "IX_RESERVATION_ID_TRANSPORT");

            entity.HasIndex(e => e.IdVoyage, "IX_RESERVATION_ID_VOYAGE");

            entity.Property(e => e.IdReservation).HasColumnName("ID_RESERVATION");
            entity.Property(e => e.DateDebut)
                .HasColumnType("datetime")
                .HasColumnName("DATE_DEBUT");
            entity.Property(e => e.DateFin)
                .HasColumnType("datetime")
                .HasColumnName("DATE_FIN");
            entity.Property(e => e.DateHeureDebut)
                .HasColumnType("datetime")
                .HasColumnName("DATE_HEURE_DEBUT");
            entity.Property(e => e.DateHeureFin)
                .HasColumnType("datetime")
                .HasColumnName("DATE_HEURE_FIN");
            entity.Property(e => e.Disponibilite).HasColumnName("DISPONIBILITE");
            entity.Property(e => e.IdActivite).HasColumnName("ID_ACTIVITE");
            entity.Property(e => e.IdLogement).HasColumnName("ID_LOGEMENT");
            entity.Property(e => e.IdTransport).HasColumnName("ID_TRANSPORT");
            entity.Property(e => e.IdVoyage).HasColumnName("ID_VOYAGE");

            entity.HasOne(d => d.IdActiviteNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdActivite)
                .HasConstraintName("FK_RESERVATION_ACTIVITE");

            entity.HasOne(d => d.IdLogementNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdLogement)
                .HasConstraintName("FK_RESERVATION_LOGEMENT");

            entity.HasOne(d => d.IdTransportNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdTransport)
                .HasConstraintName("FK_RESERVATION_TRANSPORT");

            entity.HasOne(d => d.IdVoyageNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdVoyage)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RESERVATION_VOYAGE");
        });

        modelBuilder.Entity<Siege>(entity =>
        {
            entity.HasKey(e => e.IdSiege).HasName("PK__SIEGE__AB9BD2939262F3B0");

            entity.ToTable("SIEGE");

            entity.Property(e => e.IdSiege).HasColumnName("ID_SIEGE");
            entity.Property(e => e.NumeroSiege)
                .HasMaxLength(32)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("NUMERO_SIEGE");
        });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasKey(e => e.IdTransport).HasName("PK__TRANSPOR__1AA8A63F96E632ED");

            entity.ToTable("TRANSPORT");

            entity.HasIndex(e => e.IdCategorieTransport, "IX_TRANSPORT_ID_CATEGORIE_TRANSPORT");

            entity.HasIndex(e => e.IdFournisseur, "IX_TRANSPORT_ID_FOURNISSEUR");

            entity.HasIndex(e => e.IdOptionTransport, "IX_TRANSPORT_ID_OPTION_TRANSPORT");

            entity.HasIndex(e => e.IdPays, "IX_TRANSPORT_ID_PAYS");

            entity.HasIndex(e => e.IdPrixTransport, "IX_TRANSPORT_ID_PRIX_TRANSPORT");

            entity.HasIndex(e => e.IdVehiculeLoc, "IX_TRANSPORT_ID_VEHICULE_LOC");

            entity.Property(e => e.IdTransport).HasColumnName("ID_TRANSPORT");
            entity.Property(e => e.Classe).HasColumnName("CLASSE");
            entity.Property(e => e.HeureArrivee)
                .HasColumnType("datetime")
                .HasColumnName("HEURE_ARRIVEE");
            entity.Property(e => e.HeureDepart)
                .HasColumnType("datetime")
                .HasColumnName("HEURE_DEPART");
            entity.Property(e => e.IdCategorieTransport).HasColumnName("ID_CATEGORIE_TRANSPORT");
            entity.Property(e => e.IdFournisseur).HasColumnName("ID_FOURNISSEUR");
            entity.Property(e => e.IdOptionTransport).HasColumnName("ID_OPTION_TRANSPORT");
            entity.Property(e => e.IdPays).HasColumnName("ID_PAYS");
            entity.Property(e => e.IdPrixTransport).HasColumnName("ID_PRIX_TRANSPORT");
            entity.Property(e => e.IdVehiculeLoc).HasColumnName("ID_VEHICULE_LOC");
            entity.Property(e => e.LieuDepart)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("LIEU_DEPART");

            entity.HasOne(d => d.IdCategorieTransportNavigation).WithMany(p => p.Transports)
                .HasForeignKey(d => d.IdCategorieTransport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSPORT_TRANSPORT_CATEGORIE");

            entity.HasOne(d => d.IdFournisseurNavigation).WithMany(p => p.Transports)
                .HasForeignKey(d => d.IdFournisseur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSPORT_FOURNISSEUR");

            entity.HasOne(d => d.IdOptionTransportNavigation).WithMany(p => p.Transports)
                .HasForeignKey(d => d.IdOptionTransport)
                .HasConstraintName("FK_TRANSPORT_TRANSPORT_OPTION");

            entity.HasOne(d => d.IdPaysNavigation).WithMany(p => p.Transports)
                .HasForeignKey(d => d.IdPays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSPORT_PAYS");

            entity.HasOne(d => d.IdPrixTransportNavigation).WithMany(p => p.Transports)
                .HasForeignKey(d => d.IdPrixTransport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSPORT_TRANSPORT_PRIX");

            entity.HasOne(d => d.IdVehiculeLocNavigation).WithMany(p => p.Transports)
                .HasForeignKey(d => d.IdVehiculeLoc)
                .HasConstraintName("FK_TRANSPORT_VEHICULE_LOCATION");
        });

        modelBuilder.Entity<TransportCategorie>(entity =>
        {
            entity.HasKey(e => e.IdCategorieTransport).HasName("PK__TRANSPOR__8D9BA317A25FA442");

            entity.ToTable("TRANSPORT_CATEGORIE");

            entity.Property(e => e.IdCategorieTransport).HasColumnName("ID_CATEGORIE_TRANSPORT");
            entity.Property(e => e.Nom)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<PrixLogement>(entity =>
        {
            entity.HasKey(e => e.IdLogementPrix).HasName("PK__PRIX_LOG__5059F78AA5C3E682");

            entity.ToTable("PRIX_LOGEMENT");

            entity.Property(e => e.IdLogementPrix).HasColumnName("ID_LOGEMENT_PRIX");
            entity.Property(e => e.DateDebutValidite)
                .HasColumnType("datetime")
                .HasColumnName("DATE_DEBUT_VALIDITE");
            entity.Property(e => e.DateFinValidite)
                .HasColumnType("datetime")
                .HasColumnName("DATE_FIN_VALIDITE");
            entity.Property(e => e.Prix)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PRIX");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.IdRegion).HasName("PK__REGION__D8BB64B09BF23A17");

            entity.ToTable("REGION");

            entity.HasIndex(e => e.IdPays, "IX_REGION_ID_PAYS");

            entity.Property(e => e.IdRegion).HasColumnName("ID_REGION");
            entity.Property(e => e.IdPays).HasColumnName("ID_PAYS");
            entity.Property(e => e.Nom)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("NOM");

            entity.HasOne(d => d.IdPaysNavigation).WithMany(p => p.Regions)
                .HasForeignKey(d => d.IdPays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REGION_PAYS");
        });

        modelBuilder.Entity<TransportOption>(entity =>
        {
            entity.HasKey(e => e.IdOptionTransport).HasName("PK__TRANSPOR__46DC60A5FF69360E");

            entity.ToTable("TRANSPORT_OPTION");

            entity.Property(e => e.IdOptionTransport).HasColumnName("ID_OPTION_TRANSPORT");
            entity.Property(e => e.BagageEnSoute).HasColumnName("BAGAGE_EN_SOUTE");
            entity.Property(e => e.BagageLarge).HasColumnName("BAGAGE_LARGE");
            entity.Property(e => e.BagageMain).HasColumnName("BAGAGE_MAIN");
            entity.Property(e => e.PrixBagagelarge).HasColumnName("PRIX_BAGAGELARGE");
            entity.Property(e => e.PrixBagagemain).HasColumnName("PRIX_BAGAGEMAIN");
            entity.Property(e => e.PrixBagagesoute).HasColumnName("PRIX_BAGAGESOUTE");
            entity.Property(e => e.PrixSpeedyboarding).HasColumnName("PRIX_SPEEDYBOARDING");
            entity.Property(e => e.Speedyboarding).HasColumnName("SPEEDYBOARDING");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.IdReservation).HasName("PK__RESERVAT__3CB7398B9A29C2AD");

            entity.ToTable("RESERVATION");

            entity.HasIndex(e => e.IdActivite, "IX_RESERVATION_ID_ACTIVITE");

            entity.HasIndex(e => e.IdLogement, "IX_RESERVATION_ID_LOGEMENT");

            entity.HasIndex(e => e.IdTransport, "IX_RESERVATION_ID_TRANSPORT");

            entity.HasIndex(e => e.IdVoyage, "IX_RESERVATION_ID_VOYAGE");

            entity.Property(e => e.IdReservation).HasColumnName("ID_RESERVATION");
            entity.Property(e => e.DateDebut)
                .HasColumnType("datetime")
                .HasColumnName("DATE_DEBUT");
            entity.Property(e => e.DateFin)
                .HasColumnType("datetime")
                .HasColumnName("DATE_FIN");
            entity.Property(e => e.DateHeureDebut)
                .HasColumnType("datetime")
                .HasColumnName("DATE_HEURE_DEBUT");
            entity.Property(e => e.DateHeureFin)
                .HasColumnType("datetime")
                .HasColumnName("DATE_HEURE_FIN");
            entity.Property(e => e.Disponibilite).HasColumnName("DISPONIBILITE");
            entity.Property(e => e.IdActivite).HasColumnName("ID_ACTIVITE");
            entity.Property(e => e.IdLogement).HasColumnName("ID_LOGEMENT");
            entity.Property(e => e.IdTransport).HasColumnName("ID_TRANSPORT");
            entity.Property(e => e.IdVoyage).HasColumnName("ID_VOYAGE");

            entity.HasOne(d => d.IdActiviteNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdActivite)
                .HasConstraintName("FK_RESERVATION_ACTIVITE");

            entity.HasOne(d => d.IdLogementNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdLogement)
                .HasConstraintName("FK_RESERVATION_LOGEMENT");

            entity.HasOne(d => d.IdTransportNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdTransport)
                .HasConstraintName("FK_RESERVATION_TRANSPORT");

            entity.HasOne(d => d.IdVoyageNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdVoyage)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RESERVATION_VOYAGE");
        });

        modelBuilder.Entity<Siege>(entity =>
        {
            entity.HasKey(e => e.IdSiege).HasName("PK__SIEGE__AB9BD2939262F3B0");

            entity.ToTable("SIEGE");

            entity.Property(e => e.IdSiege).HasColumnName("ID_SIEGE");
            entity.Property(e => e.NumeroSiege)
                .HasMaxLength(32)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("NUMERO_SIEGE");
        });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasKey(e => e.IdTransport).HasName("PK__TRANSPOR__1AA8A63F96E632ED");

            entity.ToTable("TRANSPORT");

            entity.HasIndex(e => e.IdCategorieTransport, "IX_TRANSPORT_ID_CATEGORIE_TRANSPORT");

            entity.HasIndex(e => e.IdFournisseur, "IX_TRANSPORT_ID_FOURNISSEUR");

            entity.HasIndex(e => e.IdOptionTransport, "IX_TRANSPORT_ID_OPTION_TRANSPORT");

            entity.HasIndex(e => e.IdPays, "IX_TRANSPORT_ID_PAYS");

            entity.HasIndex(e => e.IdPrixTransport, "IX_TRANSPORT_ID_PRIX_TRANSPORT");

            entity.HasIndex(e => e.IdVehiculeLoc, "IX_TRANSPORT_ID_VEHICULE_LOC");

            entity.Property(e => e.IdTransport).HasColumnName("ID_TRANSPORT");
            entity.Property(e => e.Classe).HasColumnName("CLASSE");
            entity.Property(e => e.HeureArrivee)
                .HasColumnType("datetime")
                .HasColumnName("HEURE_ARRIVEE");
            entity.Property(e => e.HeureDepart)
                .HasColumnType("datetime")
                .HasColumnName("HEURE_DEPART");
            entity.Property(e => e.IdCategorieTransport).HasColumnName("ID_CATEGORIE_TRANSPORT");
            entity.Property(e => e.IdFournisseur).HasColumnName("ID_FOURNISSEUR");
            entity.Property(e => e.IdOptionTransport).HasColumnName("ID_OPTION_TRANSPORT");
            entity.Property(e => e.IdPays).HasColumnName("ID_PAYS");
            entity.Property(e => e.IdPrixTransport).HasColumnName("ID_PRIX_TRANSPORT");
            entity.Property(e => e.IdVehiculeLoc).HasColumnName("ID_VEHICULE_LOC");
            entity.Property(e => e.LieuDepart)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("LIEU_DEPART");

            entity.HasOne(d => d.IdCategorieTransportNavigation).WithMany(p => p.Transports)
                .HasForeignKey(d => d.IdCategorieTransport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSPORT_TRANSPORT_CATEGORIE");

            entity.HasOne(d => d.IdFournisseurNavigation).WithMany(p => p.Transports)
                .HasForeignKey(d => d.IdFournisseur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSPORT_FOURNISSEUR");

            entity.HasOne(d => d.IdOptionTransportNavigation).WithMany(p => p.Transports)
                .HasForeignKey(d => d.IdOptionTransport)
                .HasConstraintName("FK_TRANSPORT_TRANSPORT_OPTION");

            entity.HasOne(d => d.IdPaysNavigation).WithMany(p => p.Transports)
                .HasForeignKey(d => d.IdPays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSPORT_PAYS");

            entity.HasOne(d => d.IdPrixTransportNavigation).WithMany(p => p.Transports)
                .HasForeignKey(d => d.IdPrixTransport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSPORT_TRANSPORT_PRIX");

            entity.HasOne(d => d.IdVehiculeLocNavigation).WithMany(p => p.Transports)
                .HasForeignKey(d => d.IdVehiculeLoc)
                .HasConstraintName("FK_TRANSPORT_VEHICULE_LOCATION");
        });

        modelBuilder.Entity<TransportCategorie>(entity =>
        {
            entity.HasKey(e => e.IdCategorieTransport).HasName("PK__TRANSPOR__8D9BA317A25FA442");

            entity.ToTable("TRANSPORT_CATEGORIE");

            entity.Property(e => e.IdCategorieTransport).HasColumnName("ID_CATEGORIE_TRANSPORT");
            entity.Property(e => e.Nom)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<TransportOption>(entity =>
        {
            entity.HasKey(e => e.IdOptionTransport).HasName("PK__TRANSPOR__46DC60A5FF69360E");

            entity.ToTable("TRANSPORT_OPTION");

            entity.Property(e => e.IdOptionTransport).HasColumnName("ID_OPTION_TRANSPORT");
            entity.Property(e => e.BagageEnSoute).HasColumnName("BAGAGE_EN_SOUTE");
            entity.Property(e => e.BagageLarge).HasColumnName("BAGAGE_LARGE");
            entity.Property(e => e.BagageMain).HasColumnName("BAGAGE_MAIN");
            entity.Property(e => e.PrixBagagelarge).HasColumnName("PRIX_BAGAGELARGE");
            entity.Property(e => e.PrixBagagemain).HasColumnName("PRIX_BAGAGEMAIN");
            entity.Property(e => e.PrixBagagesoute).HasColumnName("PRIX_BAGAGESOUTE");
            entity.Property(e => e.PrixSpeedyboarding).HasColumnName("PRIX_SPEEDYBOARDING");
            entity.Property(e => e.Speedyboarding).HasColumnName("SPEEDYBOARDING");
        });

        modelBuilder.Entity<TransportPrix>(entity =>
        {
            entity.HasKey(e => e.IdPrixTransport).HasName("PK__TRANSPOR__C1EF82C936644DEF");

            entity.ToTable("TRANSPORT_PRIX");

            entity.Property(e => e.IdPrixTransport).HasColumnName("ID_PRIX_TRANSPORT");
            entity.Property(e => e.DateDebutValidite)
                .HasColumnType("datetime")
                .HasColumnName("DATE_DEBUT_VALIDITE");
            entity.Property(e => e.DateFinValidite)
                .HasColumnType("datetime")
                .HasColumnName("DATE_FIN_VALIDITE");
            entity.Property(e => e.Prix)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PRIX");
        });

        modelBuilder.Entity<VehiculeLocation>(entity =>
        {
            entity.HasKey(e => e.IdVehiculeLoc).HasName("PK__VEHICULE__D287C088552A11D2");

            entity.ToTable("VEHICULE_LOCATION");

            entity.Property(e => e.IdVehiculeLoc).HasColumnName("ID_VEHICULE_LOC");
            entity.Property(e => e.Img)
                .HasColumnType("image")
                .HasColumnName("IMG");
            entity.Property(e => e.KillometreIllimite).HasColumnName("KILLOMETRE_ILLIMITE");
            entity.Property(e => e.Marque)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("MARQUE");
            entity.Property(e => e.NbSiege).HasColumnName("NB_SIEGE");
            entity.Property(e => e.TypeConducteur)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TYPE_CONDUCTEUR");
            entity.Property(e => e.TypeVehicule)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("TYPE_VEHICULE");
        });

        modelBuilder.Entity<Ville>(entity =>
        {
            entity.HasKey(e => e.IdVille).HasName("PK__VILLE__1FFE7135C936DC9F");

            entity.ToTable("VILLE");

            entity.HasIndex(e => e.IdRegion, "IX_VILLE_ID_REGION");

            entity.Property(e => e.IdVille).HasColumnName("ID_VILLE");
            entity.Property(e => e.IdRegion).HasColumnName("ID_REGION");
            entity.Property(e => e.Nom)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("NOM");

            entity.HasOne(d => d.IdRegionNavigation).WithMany(p => p.Villes)
                .HasForeignKey(d => d.IdRegion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VILLE_REGION");
        });

        modelBuilder.Entity<Voyage>(entity =>
        {
            entity.HasKey(e => e.IdVoyage).HasName("PK__VOYAGE__9E4C02B4E28C8529");

            entity.ToTable("VOYAGE");

            entity.HasIndex(e => e.IdClient, "IX_VOYAGE_ID_CLIENT");

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

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Voyages)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_VOYAGE_CLIENT");

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
                        j.HasKey("IdVoyage", "IdPays").HasName("PK__CHOISIR__5524A9701611409F");
                        j.ToTable("CHOISIR");
                        j.HasIndex(new[] { "IdPays" }, "IX_CHOISIR_ID_PAYS");
                        j.IndexerProperty<short>("IdVoyage").HasColumnName("ID_VOYAGE");
                        j.IndexerProperty<short>("IdPays").HasColumnName("ID_PAYS");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
