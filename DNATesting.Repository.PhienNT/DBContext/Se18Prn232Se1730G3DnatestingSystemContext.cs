using System;
using System.Collections.Generic;
using DNATesting.Repository.PhienNT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DNATesting.Repository.PhienNT.DBContext;

public partial class Se18Prn232Se1730G3DnatestingSystemContext : DbContext
{
    public Se18Prn232Se1730G3DnatestingSystemContext()
    {
    }

    public Se18Prn232Se1730G3DnatestingSystemContext(DbContextOptions<Se18Prn232Se1730G3DnatestingSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlleleResultsPhienNt> AlleleResultsPhienNts { get; set; }

    public virtual DbSet<AppointmentStatusesTienDm> AppointmentStatusesTienDms { get; set; }

    public virtual DbSet<AppointmentsTienDm> AppointmentsTienDms { get; set; }

    public virtual DbSet<BlogCategoriesHuyLhg> BlogCategoriesHuyLhgs { get; set; }

    public virtual DbSet<BlogsHuyLhg> BlogsHuyLhgs { get; set; }

    public virtual DbSet<DnaTestsPhienNt> DnaTestsPhienNts { get; set; }

    public virtual DbSet<LociPhienNt> LociPhienNts { get; set; }

    public virtual DbSet<LocusMatchResultsPhienNt> LocusMatchResultsPhienNts { get; set; }

    public virtual DbSet<OrderGiapHd> OrderGiapHds { get; set; }

    public virtual DbSet<ProfileRelationshipThinhLc> ProfileRelationshipThinhLcs { get; set; }

    public virtual DbSet<ProfileThinhLc> ProfileThinhLcs { get; set; }

    public virtual DbSet<SampleThinhLc> SampleThinhLcs { get; set; }

    public virtual DbSet<SampleTypeThinhLc> SampleTypeThinhLcs { get; set; }

    public virtual DbSet<ServiceCategoriesNhanVt> ServiceCategoriesNhanVts { get; set; }

    public virtual DbSet<ServicesNhanVt> ServicesNhanVts { get; set; }

    public virtual DbSet<SystemUserAccount> SystemUserAccounts { get; set; }

    public virtual DbSet<TransactionsGiapHd> TransactionsGiapHds { get; set; }

    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=171.248.235.213;Uid=sa;Pwd=Passw0rd!;Database=SE18_PRN232_SE1730_G3_DNATestingSystem;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlleleResultsPhienNt>(entity =>
        {
            entity.HasKey(e => e.PhienNtid).HasName("PK__AlleleRe__EC32F07A4D4601AC");

            entity.ToTable("AlleleResultsPhienNT");

            entity.Property(e => e.PhienNtid).HasColumnName("PhienNTId");
            entity.Property(e => e.Comments).HasColumnType("text");
            entity.Property(e => e.ConfidenceScore)
                .HasDefaultValue(1.00m)
                .HasColumnType("numeric(4, 2)");
            entity.Property(e => e.IsOutlier).HasDefaultValue(false);
            entity.Property(e => e.ProfileThinhLcid).HasColumnName("ProfileThinhLCID");
            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TestedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Locus).WithMany(p => p.AlleleResultsPhienNts)
                .HasForeignKey(d => d.LocusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlleleResultsPhienNT_LociPhienNT");

            entity.HasOne(d => d.ProfileThinhLc).WithMany(p => p.AlleleResultsPhienNts)
                .HasForeignKey(d => d.ProfileThinhLcid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlleleResultsPhienNT_ProfileThinhLC");

            entity.HasOne(d => d.Test).WithMany(p => p.AlleleResultsPhienNts)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlleleResultsPhienNT_DnaTestsPhienNT");
        });

        modelBuilder.Entity<AppointmentStatusesTienDm>(entity =>
        {
            entity.HasKey(e => e.AppointmentStatusesTienDmid).HasName("PK__Appointm__3E14D369CB49C20F");

            entity.ToTable("AppointmentStatusesTienDM");

            entity.Property(e => e.AppointmentStatusesTienDmid).HasColumnName("AppointmentStatusesTienDMID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.StatusName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AppointmentsTienDm>(entity =>
        {
            entity.HasKey(e => e.AppointmentsTienDmid).HasName("PK__Appointm__590F50BC05F92C1E");

            entity.ToTable("AppointmentsTienDM");

            entity.Property(e => e.AppointmentsTienDmid).HasColumnName("AppointmentsTienDMID");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.AppointmentStatusesTienDmid).HasColumnName("AppointmentStatusesTienDMID");
            entity.Property(e => e.ContactPhone)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsPaid).HasDefaultValue(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Notes)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.SamplingMethod)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ServicesNhanVtid).HasColumnName("ServicesNhanVTID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.UserAccountId).HasColumnName("UserAccountID");

            entity.HasOne(d => d.AppointmentStatusesTienDm).WithMany(p => p.AppointmentsTienDms)
                .HasForeignKey(d => d.AppointmentStatusesTienDmid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppointmentsTienDM_AppointmentStatusesTienDM");

            entity.HasOne(d => d.ServicesNhanVt).WithMany(p => p.AppointmentsTienDms)
                .HasForeignKey(d => d.ServicesNhanVtid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppointmentsTienDM_Services");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.AppointmentsTienDms)
                .HasForeignKey(d => d.UserAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppointmentsTienDM_UserAccount");
        });

        modelBuilder.Entity<BlogCategoriesHuyLhg>(entity =>
        {
            entity.HasKey(e => e.BlogCategoryHuyLhgid).HasName("PK__BlogCate__A82B4E0C8995D4C1");

            entity.ToTable("BlogCategoriesHuyLHG");

            entity.Property(e => e.BlogCategoryHuyLhgid).HasColumnName("BlogCategoryHuyLHGID");
            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<BlogsHuyLhg>(entity =>
        {
            entity.HasKey(e => e.BlogsHuyLhgid).HasName("PK__BlogsHuy__25602EFE165DAD80");

            entity.ToTable("BlogsHuyLHG");

            entity.Property(e => e.BlogsHuyLhgid).HasColumnName("BlogsHuyLHGID");
            entity.Property(e => e.BlogCategoryHuyLhgid).HasColumnName("BlogCategoryHuyLHGID");
            entity.Property(e => e.Content)
                .IsRequired()
                .HasColumnType("text");
            entity.Property(e => e.CoverImage)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PublishDate).HasColumnType("datetime");
            entity.Property(e => e.Summary)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.BlogCategoryHuyLhg).WithMany(p => p.BlogsHuyLhgs)
                .HasForeignKey(d => d.BlogCategoryHuyLhgid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BlogsHuyLHG_BlogCategoriesHuyLHG");

            entity.HasOne(d => d.User).WithMany(p => p.BlogsHuyLhgs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BlogsHuyLHG_UserAccount");
        });

        modelBuilder.Entity<DnaTestsPhienNt>(entity =>
        {
            entity.HasKey(e => e.PhienNtid).HasName("PK__DnaTests__EC32F07A5C7E828A");

            entity.ToTable("DnaTestsPhienNT");

            entity.Property(e => e.PhienNtid).HasColumnName("PhienNTId");
            entity.Property(e => e.Conclusion).HasColumnType("text");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsCompleted).HasDefaultValue(false);
            entity.Property(e => e.ProbabilityOfRelationship).HasColumnType("numeric(8, 5)");
            entity.Property(e => e.RelationshipIndex).HasColumnType("numeric(15, 2)");
            entity.Property(e => e.TestType)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LociPhienNt>(entity =>
        {
            entity.HasKey(e => e.PhienNtid).HasName("PK__LociPhie__EC32F07AA07058EE");

            entity.ToTable("LociPhienNT");

            entity.HasIndex(e => e.Name, "UQ__LociPhie__737584F6922A9401").IsUnique();

            entity.Property(e => e.PhienNtid).HasColumnName("PhienNTId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasDefaultValue("Core marker")
                .HasColumnType("text");
            entity.Property(e => e.IsCodis).HasDefaultValue(true);
            entity.Property(e => e.MutationRate)
                .HasDefaultValue(0.0001m)
                .HasColumnType("numeric(5, 4)");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LocusMatchResultsPhienNt>(entity =>
        {
            entity.HasKey(e => e.PhienNtid).HasName("PK__LocusMat__EC32F07AB466E8A1");

            entity.ToTable("LocusMatchResultsPhienNT");

            entity.Property(e => e.PhienNtid).HasColumnName("PhienNTId");
            entity.Property(e => e.EvaluatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EvaluatorNotes).HasColumnType("text");
            entity.Property(e => e.MatchScore).HasColumnType("numeric(6, 2)");

            entity.HasOne(d => d.Locus).WithMany(p => p.LocusMatchResultsPhienNts)
                .HasForeignKey(d => d.LocusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LocusMatchResultsPhienNT_LociPhienNT");

            entity.HasOne(d => d.Test).WithMany(p => p.LocusMatchResultsPhienNts)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LocusMatchResultsPhienNT_DnaTestsPhienNT");
        });

        modelBuilder.Entity<OrderGiapHd>(entity =>
        {
            entity.HasKey(e => e.OrderGiapHdid).HasName("PK__OrderGia__FFB6F6F5315AF37F");

            entity.ToTable("OrderGiapHD");

            entity.Property(e => e.OrderGiapHdid).HasColumnName("OrderGiapHDID");
            entity.Property(e => e.CancelReason)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CancelledDate).HasColumnType("datetime");
            entity.Property(e => e.CompletedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Notes)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentStatus)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RecipientEmail)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.RecipientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RecipientPhone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ShippingAddress)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserAccountId).HasColumnName("UserAccountID");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.OrderGiapHds)
                .HasForeignKey(d => d.UserAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderGiapHD_UserAccount");
        });

        modelBuilder.Entity<ProfileRelationshipThinhLc>(entity =>
        {
            entity.HasKey(e => e.ProfileRelationshipThinhLcid).HasName("PK__ProfileR__46E9B9D4FFFEA0A2");

            entity.ToTable("ProfileRelationshipThinhLC");

            entity.Property(e => e.ProfileRelationshipThinhLcid).HasColumnName("ProfileRelationshipThinhLCID");
            entity.Property(e => e.Count).HasDefaultValue(0);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.ProfileThinhLcid1).HasColumnName("ProfileThinhLCID1");
            entity.Property(e => e.ProfileThinhLcid2).HasColumnName("ProfileThinhLCID2");
            entity.Property(e => e.RelationshipType)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ProfileThinhLcid1Navigation).WithMany(p => p.ProfileRelationshipThinhLcProfileThinhLcid1Navigations)
                .HasForeignKey(d => d.ProfileThinhLcid1)
                .HasConstraintName("FK_ProfileRelationshipThinhLC_Profile1");

            entity.HasOne(d => d.ProfileThinhLcid2Navigation).WithMany(p => p.ProfileRelationshipThinhLcProfileThinhLcid2Navigations)
                .HasForeignKey(d => d.ProfileThinhLcid2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProfileRelationshipThinhLC_Profile2");
        });

        modelBuilder.Entity<ProfileThinhLc>(entity =>
        {
            entity.HasKey(e => e.ProfileThinhLcid).HasName("PK__ProfileT__D11462A45EC7B06E");

            entity.ToTable("ProfileThinhLC");

            entity.HasIndex(e => e.NationalId, "UQ_ProfileThinhLC_NationalID").IsUnique();

            entity.Property(e => e.ProfileThinhLcid).HasColumnName("ProfileThinhLCID");
            entity.Property(e => e.Count).HasDefaultValue(0);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NationalId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NationalID");
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserAccountId).HasColumnName("UserAccountID");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.ProfileThinhLcs)
                .HasForeignKey(d => d.UserAccountId)
                .HasConstraintName("FK_ProfileThinhLC_UserAccount");
        });

        modelBuilder.Entity<SampleThinhLc>(entity =>
        {
            entity.HasKey(e => e.SampleThinhLcid).HasName("PK__SampleTh__078B5037E4601A8F");

            entity.ToTable("SampleThinhLC");

            entity.Property(e => e.SampleThinhLcid).HasColumnName("SampleThinhLCID");
            entity.Property(e => e.AppointmentsTienDmid).HasColumnName("AppointmentsTienDMID");
            entity.Property(e => e.CollectedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Count).HasDefaultValue(0);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.ProfileThinhLcid).HasColumnName("ProfileThinhLCID");
            entity.Property(e => e.SampleTypeThinhLcid).HasColumnName("SampleTypeThinhLCID");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AppointmentsTienDm).WithMany(p => p.SampleThinhLcs)
                .HasForeignKey(d => d.AppointmentsTienDmid)
                .HasConstraintName("FK_SampleThinhLC_AppointmentsTienDM");

            entity.HasOne(d => d.ProfileThinhLc).WithMany(p => p.SampleThinhLcs)
                .HasForeignKey(d => d.ProfileThinhLcid)
                .HasConstraintName("FK_SampleThinhLC_ProfileThinhLC");

            entity.HasOne(d => d.SampleTypeThinhLc).WithMany(p => p.SampleThinhLcs)
                .HasForeignKey(d => d.SampleTypeThinhLcid)
                .HasConstraintName("FK_SampleThinhLC_SampleTypeThinhLC");
        });

        modelBuilder.Entity<SampleTypeThinhLc>(entity =>
        {
            entity.HasKey(e => e.SampleTypeThinhLcid).HasName("PK__SampleTy__4B9B39CE1A412594");

            entity.ToTable("SampleTypeThinhLC");

            entity.HasIndex(e => e.TypeName, "UQ_SampleTypeThinhLC_TypeName").IsUnique();

            entity.Property(e => e.SampleTypeThinhLcid).HasColumnName("SampleTypeThinhLCID");
            entity.Property(e => e.Count).HasDefaultValue(0);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.TypeName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ServiceCategoriesNhanVt>(entity =>
        {
            entity.HasKey(e => e.ServiceCategoryNhanVtid).HasName("PK__ServiceC__F96A229DED976552");

            entity.ToTable("ServiceCategoriesNhanVT");

            entity.Property(e => e.ServiceCategoryNhanVtid).HasColumnName("ServiceCategoryNhanVTID");
            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<ServicesNhanVt>(entity =>
        {
            entity.HasKey(e => e.ServicesNhanVtid).HasName("PK__Services__049747BFE6EF5F8F");

            entity.ToTable("ServicesNhanVT");

            entity.Property(e => e.ServicesNhanVtid).HasColumnName("ServicesNhanVTID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsClinicVisitAllowed).HasDefaultValue(false);
            entity.Property(e => e.IsHomeVisitAllowed).HasDefaultValue(false);
            entity.Property(e => e.IsSelfSampleAllowed).HasDefaultValue(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Price)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(12, 2)");
            entity.Property(e => e.ServiceCategoryNhanVtid).HasColumnName("ServiceCategoryNhanVTID");
            entity.Property(e => e.ServiceName)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UserAccountId).HasColumnName("UserAccountID");

            entity.HasOne(d => d.ServiceCategoryNhanVt).WithMany(p => p.ServicesNhanVts)
                .HasForeignKey(d => d.ServiceCategoryNhanVtid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServicesNhanVT_ServiceCategoriesNhanVT");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.ServicesNhanVts)
                .HasForeignKey(d => d.UserAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServicesNhanVT_UserAccount");
        });

        modelBuilder.Entity<SystemUserAccount>(entity =>
        {
            entity.HasKey(e => e.UserAccountId);

            entity.ToTable("System.UserAccount");

            entity.Property(e => e.UserAccountId).HasColumnName("UserAccountID");
            entity.Property(e => e.ApplicationCode).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);
            entity.Property(e => e.EmployeeCode)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.RequestCode).HasMaxLength(50);
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<TransactionsGiapHd>(entity =>
        {
            entity.HasKey(e => e.TransactionsGiapHdid).HasName("PK__Transact__69E8B87120B4C992");

            entity.ToTable("TransactionsGiapHD");

            entity.Property(e => e.TransactionsGiapHdid).HasColumnName("TransactionsGiapHDID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.IsRefund).HasDefaultValue(false);
            entity.Property(e => e.OrderGiapHdid).HasColumnName("OrderGiapHDID");
            entity.Property(e => e.PaymentGateway)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RefundReason)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TransactionReference)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.OrderGiapHd).WithMany(p => p.TransactionsGiapHds)
                .HasForeignKey(d => d.OrderGiapHdid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TransactionsGiapHD_OrderGiapHD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
