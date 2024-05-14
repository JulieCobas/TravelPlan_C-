using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using projet_csharp_travel_plan.DTO;

namespace projet_csharp_travel_plan.Models;

public partial class TravelPlanContext : DbContext
{
    public TravelPlanContext()
    {
    }

    public TravelPlanContext(DbContextOptions<TravelPlanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activite> Activites { get; set; }

    public virtual DbSet<ActiviteCategorie> ActiviteCategories { get; set; }

    public virtual DbSet<ActiviteOption> ActiviteOptions { get; set; }

    public virtual DbSet<ActivitePrix> ActivitePrixes { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<CategoriePrix> CategoriePrixes { get; set; }

    public virtual DbSet<Chambre> Chambres { get; set; }

    public virtual DbSet<ChambreEquipement> ChambreEquipements { get; set; }

    public virtual DbSet<ChambreOption> ChambreOptions { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<EquipementCategorie> EquipementCategories { get; set; }

    public virtual DbSet<Fournisseur> Fournisseurs { get; set; }

    public virtual DbSet<Invite> Invites { get; set; }

    public virtual DbSet<LocationLogement> LocationLogements { get; set; }

    public virtual DbSet<Logement> Logements { get; set; }

    public virtual DbSet<LogementCategorie> LogementCategories { get; set; }

    public virtual DbSet<Paiement> Paiements { get; set; }

    public virtual DbSet<Pay> Pays { get; set; }

    public virtual DbSet<PrixLogement> PrixLogements { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Transport> Transports { get; set; }

    public virtual DbSet<TransportCategorie> TransportCategories { get; set; }

    public virtual DbSet<TransportOption> TransportOptions { get; set; }

    public virtual DbSet<TransportPrix> TransportPrixes { get; set; }

    public virtual DbSet<VehiculeLocation> VehiculeLocations { get; set; }

    public virtual DbSet<Voyage> Voyages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=TRASHPAD;Database=TravelPlan;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activite>(entity =>
        {
            entity.HasKey(e => e.IdActivite);

            entity.ToTable("ACTIVITE");

            entity.Property(e => e.IdActivite).HasColumnName("ID_ACTIVITE");
            entity.Property(e => e.Details)
                .HasColumnType("text")
                .HasColumnName("DETAILS");
            entity.Property(e => e.HeuresMoyennes).HasColumnName("HEURES_MOYENNES");
            entity.Property(e => e.IdCatActiv).HasColumnName("ID_CAT_ACTIV");
            entity.Property(e => e.IdFournisseur).HasColumnName("ID_FOURNISSEUR");
            entity.Property(e => e.IdOptionActivite).HasColumnName("ID_OPTION_ACTIVITE");
            entity.Property(e => e.IdPays).HasColumnName("ID_PAYS");
            entity.Property(e => e.IdPrixActivite).HasColumnName("ID_PRIX_ACTIVITE");
            entity.Property(e => e.Img).HasColumnName("IMG");
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
            entity.HasKey(e => e.IdCatActiv);

            entity.ToTable("ACTIVITE_CATEGORIE");

            entity.Property(e => e.IdCatActiv).HasColumnName("ID_CAT_ACTIV");
            entity.Property(e => e.Nom)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<ActiviteOption>(entity =>
        {
            entity.HasKey(e => e.IdOptionActivite);

            entity.ToTable("ACTIVITE_OPTION");

            entity.Property(e => e.IdOptionActivite).HasColumnName("ID_OPTION_ACTIVITE");
            entity.Property(e => e.EquipementInclu).HasColumnName("EQUIPEMENT_INCLU");
            entity.Property(e => e.GuideAudio).HasColumnName("GUIDE_AUDIO");
            entity.Property(e => e.VisiteGuidee).HasColumnName("VISITE_GUIDEE");
        });

        modelBuilder.Entity<ActivitePrix>(entity =>
        {
            entity.HasKey(e => e.IdPrixActivite);

            entity.ToTable("ACTIVITE_PRIX");

            entity.Property(e => e.IdPrixActivite).HasColumnName("ID_PRIX_ACTIVITE");
            entity.Property(e => e.DateDebutValidite).HasColumnName("DATE_DEBUT_VALIDITE");
            entity.Property(e => e.DateFinValidite).HasColumnName("DATE_FIN_VALIDITE");
            entity.Property(e => e.Prix)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PRIX");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.IdInvitee).HasColumnName("ID_INVITEE");
            entity.Property(e => e.IdUtilisateur).HasColumnName("ID_UTILISATEUR");
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.IdInviteeNavigation).WithMany(p => p.AspNetUsers)
                .HasForeignKey(d => d.IdInvitee)
                .HasConstraintName("FK_ASPNETUS_REFERENCE_INVITE");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithMany(p => p.AspNetUsers)
                .HasForeignKey(d => d.IdUtilisateur)
                .HasConstraintName("FK_ASPNETUS_REFERENCE_CLIENT");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<CategoriePrix>(entity =>
        {
            entity.HasKey(e => e.IdCategoriePrix);

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
                        j.HasKey("IdCategoriePrix", "IdLogementPrix");
                        j.ToTable("SE_REFERE_LOGEMENT");
                        j.IndexerProperty<int>("IdCategoriePrix").HasColumnName("ID_CATEGORIE_PRIX");
                        j.IndexerProperty<int>("IdLogementPrix").HasColumnName("ID_LOGEMENT_PRIX");
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
                        j.HasKey("IdCategoriePrix", "IdPrixActivite");
                        j.ToTable("SE_REFERE_ACTIV");
                        j.IndexerProperty<int>("IdCategoriePrix").HasColumnName("ID_CATEGORIE_PRIX");
                        j.IndexerProperty<int>("IdPrixActivite").HasColumnName("ID_PRIX_ACTIVITE");
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
                        j.HasKey("IdCategoriePrix", "IdPrixTransport");
                        j.ToTable("SE_REFERE_TRANSP");
                        j.IndexerProperty<int>("IdCategoriePrix").HasColumnName("ID_CATEGORIE_PRIX");
                        j.IndexerProperty<int>("IdPrixTransport").HasColumnName("ID_PRIX_TRANSPORT");
                    });
        });

        modelBuilder.Entity<Chambre>(entity =>
        {
            entity.HasKey(e => e.IdChambre);

            entity.ToTable("CHAMBRE");

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
                        j.HasKey("IdChambre", "IdEquipChambre");
                        j.ToTable("CHAMBRE_EQUIPE");
                        j.IndexerProperty<int>("IdChambre").HasColumnName("ID_CHAMBRE");
                        j.IndexerProperty<int>("IdEquipChambre").HasColumnName("ID_EQUIP_CHAMBRE");
                    });
        });

        modelBuilder.Entity<ChambreEquipement>(entity =>
        {
            entity.HasKey(e => e.IdEquipChambre);

            entity.ToTable("CHAMBRE_EQUIPEMENT");

            entity.Property(e => e.IdEquipChambre).HasColumnName("ID_EQUIP_CHAMBRE");
            entity.Property(e => e.Nom)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<ChambreOption>(entity =>
        {
            entity.HasKey(e => e.IdChambreOption);

            entity.ToTable("CHAMBRE_OPTION");

            entity.Property(e => e.IdChambreOption).HasColumnName("ID_CHAMBRE_OPTION");
            entity.Property(e => e.AnnulationGratuite).HasColumnName("ANNULATION_GRATUITE");
            entity.Property(e => e.DateAnnulationGratuite).HasColumnName("DATE_ANNULATION_GRATUITE");
            entity.Property(e => e.PetitDejeunerInclus).HasColumnName("PETIT_DEJEUNER_INCLUS");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdUtilisateur);

            entity.ToTable("CLIENT");

            entity.Property(e => e.IdUtilisateur)
                .ValueGeneratedNever()
                .HasColumnName("ID_UTILISATEUR");
            entity.Property(e => e.Addresse)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("ADDRESSE");
            entity.Property(e => e.Cp)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CP");
            entity.Property(e => e.DateNaissance).HasColumnName("DATE_NAISSANCE");
            entity.Property(e => e.Mail)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("MAIL");
            entity.Property(e => e.MotDePasse)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("MOT_DE_PASSE");
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
            entity.Property(e => e.Telephone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("TELEPHONE");
            entity.Property(e => e.Ville)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("VILLE");
        });

        modelBuilder.Entity<EquipementCategorie>(entity =>
        {
            entity.HasKey(e => e.IdCatEquipement);

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
                        j.HasKey("IdCatEquipement", "IdLogement");
                        j.ToTable("ASSOCIE_EQUIPEMENT_CATEGORIE");
                        j.IndexerProperty<int>("IdCatEquipement").HasColumnName("ID_CAT_EQUIPEMENT");
                        j.IndexerProperty<int>("IdLogement").HasColumnName("ID_LOGEMENT");
                    });
        });

        modelBuilder.Entity<Fournisseur>(entity =>
        {
            entity.HasKey(e => e.IdFournisseur);

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
            entity.HasKey(e => e.IdInvitee);

            entity.ToTable("INVITE");

            entity.Property(e => e.IdInvitee).HasColumnName("ID_INVITEE");
            entity.Property(e => e.DateNaissance).HasColumnName("DATE_NAISSANCE");
            entity.Property(e => e.IdUtilisateur).HasColumnName("ID_UTILISATEUR");
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
                        j.HasKey("IdInvitee", "IdReservation");
                        j.ToTable("PARTICIPE");
                        j.IndexerProperty<int>("IdInvitee").HasColumnName("ID_INVITEE");
                        j.IndexerProperty<int>("IdReservation").HasColumnName("ID_RESERVATION");
                    });
        });

        modelBuilder.Entity<LocationLogement>(entity =>
        {
            entity.HasKey(e => e.IdLogementLoc);

            entity.ToTable("LOCATION_LOGEMENT");

            entity.Property(e => e.IdLogementLoc).HasColumnName("ID_LOGEMENT_LOC");
            entity.Property(e => e.IdLogementPrix).HasColumnName("ID_LOGEMENT_PRIX");
            entity.Property(e => e.Nom)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("NOM");

            entity.HasOne(d => d.IdLogementPrixNavigation).WithMany(p => p.LocationLogements)
                .HasForeignKey(d => d.IdLogementPrix)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOCATION_LOGEMENT_PRIX_LOGEMENT");
        });

        modelBuilder.Entity<Logement>(entity =>
        {
            entity.HasKey(e => e.IdLogement);

            entity.ToTable("LOGEMENT");

            entity.Property(e => e.IdLogement).HasColumnName("ID_LOGEMENT");
            entity.Property(e => e.Details)
                .HasColumnType("text")
                .HasColumnName("DETAILS");
            entity.Property(e => e.IdFournisseur).HasColumnName("ID_FOURNISSEUR");
            entity.Property(e => e.IdLogementCategorie).HasColumnName("ID_LOGEMENT_CATEGORIE");
            entity.Property(e => e.IdLogementLoc).HasColumnName("ID_LOGEMENT_LOC");
            entity.Property(e => e.IdPays).HasColumnName("ID_PAYS");
            entity.Property(e => e.Img).HasColumnName("IMG");
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

            entity.HasOne(d => d.IdLogementLocNavigation).WithMany(p => p.Logements)
                .HasForeignKey(d => d.IdLogementLoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOGEMENT_LOCATION_LOGEMENT");

            entity.HasOne(d => d.IdPaysNavigation).WithMany(p => p.Logements)
                .HasForeignKey(d => d.IdPays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOGEMENT_PAYS");
        });

        modelBuilder.Entity<LogementCategorie>(entity =>
        {
            entity.HasKey(e => e.IdLogementCategorie);

            entity.ToTable("LOGEMENT_CATEGORIE");

            entity.Property(e => e.IdLogementCategorie).HasColumnName("ID_LOGEMENT_CATEGORIE");
            entity.Property(e => e.Nom)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<Paiement>(entity =>
        {
            entity.HasKey(e => e.IdPaiement);

            entity.ToTable("PAIEMENT");

            entity.Property(e => e.IdPaiement).HasColumnName("ID_PAIEMENT");
            entity.Property(e => e.Crypto).HasColumnName("CRYPTO");
            entity.Property(e => e.DateExpiration).HasColumnName("DATE_EXPIRATION");
            entity.Property(e => e.IdUtilisateur).HasColumnName("ID_UTILISATEUR");
            entity.Property(e => e.NumeroCarteBancaire).HasColumnName("NUMERO_CARTE_BANCAIRE");
            entity.Property(e => e.TypeCarteBancaire)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("TYPE_CARTE_BANCAIRE");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithMany(p => p.Paiements)
                .HasForeignKey(d => d.IdUtilisateur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PAIEMENT_CLIENT");
        });

        modelBuilder.Entity<Pay>(entity =>
        {
            entity.HasKey(e => e.IdPays);

            entity.ToTable("PAYS");

            entity.Property(e => e.IdPays).HasColumnName("ID_PAYS");
            entity.Property(e => e.Nom)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("NOM");
            entity.Property(e => e.Region)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("REGION");
            entity.Property(e => e.Ville)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("VILLE");
        });

        modelBuilder.Entity<PrixLogement>(entity =>
        {
            entity.HasKey(e => e.IdLogementPrix);

            entity.ToTable("PRIX_LOGEMENT");

            entity.Property(e => e.IdLogementPrix).HasColumnName("ID_LOGEMENT_PRIX");
            entity.Property(e => e.DateDebutValidite).HasColumnName("DATE_DEBUT_VALIDITE");
            entity.Property(e => e.DateFinValidite).HasColumnName("DATE_FIN_VALIDITE");
            entity.Property(e => e.Prix)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PRIX");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.IdReservation);

            entity.ToTable("RESERVATION");

            entity.Property(e => e.IdReservation).HasColumnName("ID_RESERVATION");
            entity.Property(e => e.DateHeureDebut).HasColumnName("DATE_HEURE_DEBUT");
            entity.Property(e => e.DateHeureFin).HasColumnName("DATE_HEURE_FIN");
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

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasKey(e => e.IdTransport);

            entity.ToTable("TRANSPORT");

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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSPORT_VEHICULE_LOCATION");
        });

        modelBuilder.Entity<TransportCategorie>(entity =>
        {
            entity.HasKey(e => e.IdCategorieTransport);

            entity.ToTable("TRANSPORT_CATEGORIE");

            entity.Property(e => e.IdCategorieTransport).HasColumnName("ID_CATEGORIE_TRANSPORT");
            entity.Property(e => e.Nom)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<TransportOption>(entity =>
        {
            entity.HasKey(e => e.IdOptionTransport);

            entity.ToTable("TRANSPORT_OPTION");

            entity.Property(e => e.IdOptionTransport).HasColumnName("ID_OPTION_TRANSPORT");
            entity.Property(e => e.BagageEnSoute).HasColumnName("BAGAGE_EN_SOUTE");
            entity.Property(e => e.BagageLarge).HasColumnName("BAGAGE_LARGE");
            entity.Property(e => e.BagageMain).HasColumnName("BAGAGE_MAIN");
            entity.Property(e => e.NumeroSiege)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("NUMERO_SIEGE");
            entity.Property(e => e.Speedyboarding).HasColumnName("SPEEDYBOARDING");
        });

        modelBuilder.Entity<TransportPrix>(entity =>
        {
            entity.HasKey(e => e.IdPrixTransport);

            entity.ToTable("TRANSPORT_PRIX");

            entity.Property(e => e.IdPrixTransport).HasColumnName("ID_PRIX_TRANSPORT");
            entity.Property(e => e.DateDebutValidite).HasColumnName("DATE_DEBUT_VALIDITE");
            entity.Property(e => e.DateFinValidite).HasColumnName("DATE_FIN_VALIDITE");
            entity.Property(e => e.Prix)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PRIX");
        });

        modelBuilder.Entity<VehiculeLocation>(entity =>
        {
            entity.HasKey(e => e.IdVehiculeLoc);

            entity.ToTable("VEHICULE_LOCATION");

            entity.Property(e => e.IdVehiculeLoc).HasColumnName("ID_VEHICULE_LOC");
            entity.Property(e => e.Img).HasColumnName("IMG");
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

        modelBuilder.Entity<Voyage>(entity =>
        {
            entity.HasKey(e => e.IdVoyage);

            entity.ToTable("VOYAGE");

            entity.Property(e => e.IdVoyage).HasColumnName("ID_VOYAGE");
            entity.Property(e => e.DateDebut).HasColumnName("DATE_DEBUT");
            entity.Property(e => e.DateFin).HasColumnName("DATE_FIN");
            entity.Property(e => e.IdUtilisateur).HasColumnName("ID_UTILISATEUR");
            entity.Property(e => e.PrixTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PRIX_TOTAL");
            entity.Property(e => e.StatutPaiement).HasColumnName("STATUT_PAIEMENT");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithMany(p => p.Voyages)
                .HasForeignKey(d => d.IdUtilisateur)
                .OnDelete(DeleteBehavior.ClientSetNull)
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
                        j.HasKey("IdVoyage", "IdPays");
                        j.ToTable("CHOISIR");
                        j.IndexerProperty<int>("IdVoyage").HasColumnName("ID_VOYAGE");
                        j.IndexerProperty<int>("IdPays").HasColumnName("ID_PAYS");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<projet_csharp_travel_plan.DTO.ActiviteDTO> ActiviteDTO { get; set; } = default!;
}
