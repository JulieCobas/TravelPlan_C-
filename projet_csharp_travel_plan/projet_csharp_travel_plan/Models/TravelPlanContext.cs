using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<Aspnetrole> Aspnetroles { get; set; }

    public virtual DbSet<Aspnetroleclaim> Aspnetroleclaims { get; set; }

    public virtual DbSet<Aspnetuser> Aspnetusers { get; set; }

    public virtual DbSet<Aspnetuserclaim> Aspnetuserclaims { get; set; }

    public virtual DbSet<Aspnetuserlogin> Aspnetuserlogins { get; set; }

    public virtual DbSet<Aspnetusertoken> Aspnetusertokens { get; set; }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost\\SQLDevelopper; Initial Catalog=TravelPlan;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activite>(entity =>
        {
            entity.HasKey(e => e.IdActivite).HasName("PK__ACTIVITE__BE6F3312906BB75B");

            entity.ToTable("ACTIVITE");

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
            entity.HasKey(e => e.IdCatActiv).HasName("PK__ACTIVITE__8AED20E3BA235D5E");

            entity.ToTable("ACTIVITE_CATEGORIE");

            entity.Property(e => e.IdCatActiv).HasColumnName("ID_CAT_ACTIV");
            entity.Property(e => e.Nom)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<ActiviteOption>(entity =>
        {
            entity.HasKey(e => e.IdOptionActivite).HasName("PK__ACTIVITE__F19E116B52F69CF2");

            entity.ToTable("ACTIVITE_OPTION");

            entity.Property(e => e.IdOptionActivite).HasColumnName("ID_OPTION_ACTIVITE");
            entity.Property(e => e.GuideAudio).HasColumnName("GUIDE_AUDIO");
            entity.Property(e => e.PrixGuideAudio).HasColumnName("PRIX_GUIDE_AUDIO");
            entity.Property(e => e.PrixVisiteGuide).HasColumnName("PRIX_VISITE_GUIDE");
            entity.Property(e => e.VisiteGuidee).HasColumnName("VISITE_GUIDEE");
        });

        modelBuilder.Entity<ActivitePrix>(entity =>
        {
            entity.HasKey(e => e.IdPrixActivite).HasName("PK__ACTIVITE__D00D170BFB7C7872");

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

        modelBuilder.Entity<Aspnetrole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ASPNETRO__3214EC2746E37EAF");

            entity.ToTable("ASPNETROLES");

            entity.HasIndex(e => e.Normalizedname, "ROLENAMEINDEX")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Concurrencystamp).HasColumnName("CONCURRENCYSTAMP");
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasColumnName("NAME");
            entity.Property(e => e.Normalizedname)
                .HasMaxLength(256)
                .HasColumnName("NORMALIZEDNAME");
        });

        modelBuilder.Entity<Aspnetroleclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ASPNETRO__3214EC275DA4596D");

            entity.ToTable("ASPNETROLECLAIMS");

            entity.HasIndex(e => e.Roleid, "IX_ASPNETROLECLAIMS_ROLEID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Claimtype).HasColumnName("CLAIMTYPE");
            entity.Property(e => e.Claimvalue).HasColumnName("CLAIMVALUE");
            entity.Property(e => e.Roleid).HasColumnName("ROLEID");

            entity.HasOne(d => d.Role).WithMany(p => p.Aspnetroleclaims)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("FK_AspNetRoleClaims_AspNetRoles_RoleId");
        });

        modelBuilder.Entity<Aspnetuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ASPNETUS__3214EC27D4E390BC");

            entity.ToTable("ASPNETUSERS");

            entity.HasIndex(e => e.Normalizedemail, "EMAILINDEX");

            entity.HasIndex(e => e.Normalizedusername, "USERNAMEINDEX")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Accessfailedcount).HasColumnName("ACCESSFAILEDCOUNT");
            entity.Property(e => e.Concurrencystamp).HasColumnName("CONCURRENCYSTAMP");
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Emailconfirmed).HasColumnName("EMAILCONFIRMED");
            entity.Property(e => e.IdInvitee).HasColumnName("ID_INVITEE");
            entity.Property(e => e.Lockoutenabled).HasColumnName("LOCKOUTENABLED");
            entity.Property(e => e.Lockoutend).HasColumnName("LOCKOUTEND");
            entity.Property(e => e.Normalizedemail)
                .HasMaxLength(256)
                .HasColumnName("NORMALIZEDEMAIL");
            entity.Property(e => e.Normalizedusername)
                .HasMaxLength(256)
                .HasColumnName("NORMALIZEDUSERNAME");
            entity.Property(e => e.Passwordhash).HasColumnName("PASSWORDHASH");
            entity.Property(e => e.Phonenumber).HasColumnName("PHONENUMBER");
            entity.Property(e => e.Phonenumberconfirmed).HasColumnName("PHONENUMBERCONFIRMED");
            entity.Property(e => e.Securitystamp).HasColumnName("SECURITYSTAMP");
            entity.Property(e => e.Twofactorenabled).HasColumnName("TWOFACTORENABLED");
            entity.Property(e => e.Username)
                .HasMaxLength(256)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.IdInviteeNavigation).WithMany(p => p.Aspnetusers)
                .HasForeignKey(d => d.IdInvitee)
                .HasConstraintName("FK_ASPNETUS_REFERENCE_INVITE");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Aspnetuserrole",
                    r => r.HasOne<Aspnetrole>().WithMany()
                        .HasForeignKey("Roleid")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId"),
                    l => l.HasOne<Aspnetuser>().WithMany()
                        .HasForeignKey("Userid")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId"),
                    j =>
                    {
                        j.HasKey("Userid", "Roleid").HasName("PK__ASPNETUS__FB9829BB6F4D71AC");
                        j.ToTable("ASPNETUSERROLES");
                        j.HasIndex(new[] { "Roleid" }, "IX_ASPNETUSERROLES_ROLEID");
                        j.IndexerProperty<string>("Userid").HasColumnName("USERID");
                        j.IndexerProperty<string>("Roleid").HasColumnName("ROLEID");
                    });
        });

        modelBuilder.Entity<Aspnetuserclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ASPNETUS__3214EC27A2F9F7B0");

            entity.ToTable("ASPNETUSERCLAIMS");

            entity.HasIndex(e => e.Userid, "IX_ASPNETUSERCLAIMS_USERID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Claimtype).HasColumnName("CLAIMTYPE");
            entity.Property(e => e.Claimvalue).HasColumnName("CLAIMVALUE");
            entity.Property(e => e.Userid).HasColumnName("USERID");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserclaims)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK_AspNetUserClaims_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetuserlogin>(entity =>
        {
            entity.HasKey(e => new { e.Loginprovider, e.Providerkey }).HasName("PK__ASPNETUS__1412E97EFC8987D8");

            entity.ToTable("ASPNETUSERLOGINS");

            entity.HasIndex(e => e.Userid, "IX_ASPNETUSERLOGINS_USERID");

            entity.Property(e => e.Loginprovider)
                .HasMaxLength(128)
                .HasColumnName("LOGINPROVIDER");
            entity.Property(e => e.Providerkey)
                .HasMaxLength(128)
                .HasColumnName("PROVIDERKEY");
            entity.Property(e => e.Providerdisplayname).HasColumnName("PROVIDERDISPLAYNAME");
            entity.Property(e => e.Userid).HasColumnName("USERID");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserlogins)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK_AspNetUserLogins_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetusertoken>(entity =>
        {
            entity.HasKey(e => new { e.Userid, e.Loginprovider, e.Name }).HasName("PK__ASPNETUS__B542FEFED0984329");

            entity.ToTable("ASPNETUSERTOKENS");

            entity.Property(e => e.Userid).HasColumnName("USERID");
            entity.Property(e => e.Loginprovider)
                .HasMaxLength(128)
                .HasColumnName("LOGINPROVIDER");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .HasColumnName("NAME");
            entity.Property(e => e.Value).HasColumnName("VALUE");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetusertokens)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK_AspNetUserTokens_AspNetUsers_UserId");
        });

        modelBuilder.Entity<CategoriePrix>(entity =>
        {
            entity.HasKey(e => e.IdCategoriePrix).HasName("PK__CATEGORI__252DB365B19F299B");

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
                        j.HasKey("IdCategoriePrix", "IdLogementPrix").HasName("PK__SE_REFER__90282C1DE60B0144");
                        j.ToTable("SE_REFERE_LOGEMENT");
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
                        j.HasKey("IdCategoriePrix", "IdPrixActivite").HasName("PK__SE_REFER__882D6215CEC56753");
                        j.ToTable("SE_REFERE_ACTIV");
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
                        j.HasKey("IdCategoriePrix", "IdPrixTransport").HasName("PK__SE_REFER__A9334B49E3FF335E");
                        j.ToTable("SE_REFERE_TRANSP");
                        j.IndexerProperty<short>("IdCategoriePrix").HasColumnName("ID_CATEGORIE_PRIX");
                        j.IndexerProperty<short>("IdPrixTransport").HasColumnName("ID_PRIX_TRANSPORT");
                    });
        });

        modelBuilder.Entity<Chambre>(entity =>
        {
            entity.HasKey(e => e.IdChambre).HasName("PK__CHAMBRE__67377BD2E5097E8E");

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
                        j.HasKey("IdChambre", "IdEquipChambre").HasName("PK__CHAMBRE___9BF2CF5B4E41F8B1");
                        j.ToTable("CHAMBRE_EQUIPE");
                        j.IndexerProperty<short>("IdChambre").HasColumnName("ID_CHAMBRE");
                        j.IndexerProperty<short>("IdEquipChambre").HasColumnName("ID_EQUIP_CHAMBRE");
                    });
        });

        modelBuilder.Entity<ChambreEquipement>(entity =>
        {
            entity.HasKey(e => e.IdEquipChambre).HasName("PK__CHAMBRE___CC5B489EB9EB977E");

            entity.ToTable("CHAMBRE_EQUIPEMENT");

            entity.Property(e => e.IdEquipChambre).HasColumnName("ID_EQUIP_CHAMBRE");
            entity.Property(e => e.Nom)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<ChambreOption>(entity =>
        {
            entity.HasKey(e => e.IdChambreOption).HasName("PK__CHAMBRE___06491AFFE13C382A");

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
            entity.HasKey(e => e.IdClient).HasName("PK__CLIENT__5556D89BCC250100");

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
                .HasColumnType("datetime")
                .HasColumnName("DATE_NAISSANCE");
            entity.Property(e => e.Id)
                .HasMaxLength(450)
                .HasColumnName("ID");
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

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.Id)
                .HasConstraintName("FK_CLIENT_REFERENCE_ASPNETUS");
        });

        modelBuilder.Entity<DisponibiliteLogement>(entity =>
        {
            entity.HasKey(e => new { e.IdChambre, e.IdNumChambre }).HasName("PK__DISPONIB__5FD28D3E7531AC17");

            entity.ToTable("DISPONIBILITE_LOGEMENT");

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
            entity.HasKey(e => new { e.IdSiege, e.IdTransport }).HasName("PK__DISPONIB__4A3158F09AA40ED0");

            entity.ToTable("DISPONIBILITE_TRANSPORT");

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
            entity.HasKey(e => e.IdCatEquipement).HasName("PK__EQUIPEME__6C4F33F8DF816594");

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
                        j.HasKey("IdCatEquipement", "IdLogement").HasName("PK__ASSOCIE___43D2558AF64F9B35");
                        j.ToTable("ASSOCIE_EQUIPEMENT_CATEGORIE");
                        j.IndexerProperty<short>("IdCatEquipement").HasColumnName("ID_CAT_EQUIPEMENT");
                        j.IndexerProperty<short>("IdLogement").HasColumnName("ID_LOGEMENT");
                    });
        });

        modelBuilder.Entity<Fournisseur>(entity =>
        {
            entity.HasKey(e => e.IdFournisseur).HasName("PK__FOURNISS__F955D0EAD9EBE051");

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
            entity.HasKey(e => e.IdInvitee).HasName("PK__INVITE__D0E96D4ED35F95B3");

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
                        j.HasKey("IdInvitee", "IdReservation").HasName("PK__PARTICIP__73221ED63C3F94B0");
                        j.ToTable("PARTICIPE");
                        j.IndexerProperty<short>("IdInvitee").HasColumnName("ID_INVITEE");
                        j.IndexerProperty<short>("IdReservation").HasColumnName("ID_RESERVATION");
                    });
        });

        modelBuilder.Entity<Logement>(entity =>
        {
            entity.HasKey(e => e.IdLogement).HasName("PK__LOGEMENT__F9D66723342B26FF");

            entity.ToTable("LOGEMENT");

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
            entity.HasKey(e => e.IdLogementCategorie).HasName("PK__LOGEMENT__4CEB4680FA0F2233");

            entity.ToTable("LOGEMENT_CATEGORIE");

            entity.Property(e => e.IdLogementCategorie).HasColumnName("ID_LOGEMENT_CATEGORIE");
            entity.Property(e => e.Nom)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<NumChambre>(entity =>
        {
            entity.HasKey(e => e.IdNumChambre).HasName("PK__NUM_CHAM__8E5F6EC2220F465B");

            entity.ToTable("NUM_CHAMBRE");

            entity.Property(e => e.IdNumChambre).HasColumnName("ID_NUM_CHAMBRE");
            entity.Property(e => e.NumeroChambre).HasColumnName("NUMERO_CHAMBRE");
        });

        modelBuilder.Entity<Paiement>(entity =>
        {
            entity.HasKey(e => e.IdPaiement).HasName("PK__PAIEMENT__2390D182AFA076E2");

            entity.ToTable("PAIEMENT");

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
            entity.HasKey(e => e.IdPays).HasName("PK__PAYS__B68ABC4D1CDADFEF");

            entity.ToTable("PAYS");

            entity.Property(e => e.IdPays).HasColumnName("ID_PAYS");
            entity.Property(e => e.Nom)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<PrixLogement>(entity =>
        {
            entity.HasKey(e => e.IdLogementPrix).HasName("PK__PRIX_LOG__5059F78A8AC7941C");

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
            entity.HasKey(e => e.IdRegion).HasName("PK__REGION__D8BB64B03C9A7A7D");

            entity.ToTable("REGION");

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
            entity.HasKey(e => e.IdReservation).HasName("PK__RESERVAT__3CB7398B4DEF609F");

            entity.ToTable("RESERVATION");

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
            entity.HasKey(e => e.IdSiege).HasName("PK__SIEGE__AB9BD2934FC30654");

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
            entity.HasKey(e => e.IdTransport).HasName("PK__TRANSPOR__1AA8A63F84357DAC");

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
            entity.HasKey(e => e.IdCategorieTransport).HasName("PK__TRANSPOR__8D9BA317F38AC089");

            entity.ToTable("TRANSPORT_CATEGORIE");

            entity.Property(e => e.IdCategorieTransport).HasColumnName("ID_CATEGORIE_TRANSPORT");
            entity.Property(e => e.Nom)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("NOM");
        });

        modelBuilder.Entity<TransportOption>(entity =>
        {
            entity.HasKey(e => e.IdOptionTransport).HasName("PK__TRANSPOR__46DC60A5B5F318C8");

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
            entity.HasKey(e => e.IdPrixTransport).HasName("PK__TRANSPOR__C1EF82C951E7B2E7");

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
            entity.HasKey(e => e.IdVehiculeLoc).HasName("PK__VEHICULE__D287C08890C9DFA6");

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
            entity.HasKey(e => e.IdVille).HasName("PK__VILLE__1FFE7135F58D5F91");

            entity.ToTable("VILLE");

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
            entity.HasKey(e => e.IdVoyage).HasName("PK__VOYAGE__9E4C02B425AA1C0C");

            entity.ToTable("VOYAGE");

            entity.Property(e => e.IdVoyage).HasColumnName("ID_VOYAGE");
            entity.Property(e => e.DateDebut)
                .HasColumnType("datetime")
                .HasColumnName("DATE_DEBUT");
            entity.Property(e => e.DateFin)
                .HasColumnType("datetime")
                .HasColumnName("DATE_FIN");
            entity.Property(e => e.IdClient).HasColumnName("ID_CLIENT");
            entity.Property(e => e.IdUtilisateur).HasColumnName("ID_UTILISATEUR");
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
                        j.HasKey("IdVoyage", "IdPays").HasName("PK__CHOISIR__5524A97085A87DA5");
                        j.ToTable("CHOISIR");
                        j.IndexerProperty<short>("IdVoyage").HasColumnName("ID_VOYAGE");
                        j.IndexerProperty<short>("IdPays").HasColumnName("ID_PAYS");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
