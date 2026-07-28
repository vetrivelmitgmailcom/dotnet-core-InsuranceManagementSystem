using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace InsuranceManagementSystemMVC.Models;

public partial class InsuranceContext : DbContext
{
    private readonly IConfiguration _configuration;
    public InsuranceContext()
    {
    }

    public InsuranceContext(DbContextOptions<InsuranceContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<CityMaster> CityMasters { get; set; }

    public virtual DbSet<CountryMaster> CountryMasters { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<GenderMaster> GenderMasters { get; set; }

    public virtual DbSet<InsuranceTypeMaster> InsuranceTypeMasters { get; set; }

    public virtual DbSet<MaritalStatusMaster> MaritalStatusMasters { get; set; }

    public virtual DbSet<ModeOfPremiumMaster> ModeOfPremiumMasters { get; set; }

    public virtual DbSet<NomineeDetail> NomineeDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentTypeMaster> PaymentTypeMasters { get; set; }

    public virtual DbSet<PersonalDetail> PersonalDetails { get; set; }

    public virtual DbSet<PolicyDetail> PolicyDetails { get; set; }

    public virtual DbSet<PolicyValue> PolicyValues { get; set; }

    public virtual DbSet<RelationshipMaster> RelationshipMasters { get; set; }

    public virtual DbSet<StateMaster> StateMasters { get; set; }

    public virtual DbSet<StatusMaster> StatusMasters { get; set; }


    #region Default logging.console
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Data Source=VETRIVELMURUGAN;Initial Catalog = Insurance; User ID = sa; Password=***********;Integrated security=True;TrustServerCertificate=True;");
    #endregion


    #region logging.console
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var cs = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseLoggerFactory(loggerFactory);

        optionsBuilder.UseLoggerFactory(loggerFactory)
                                .EnableSensitiveDataLogging()
                                 //.UseSqlServer("Data Source=VETRIVELMURUGAN;Initial Catalog = Insurance; User ID = sa; Password=***********;Integrated security=True;TrustServerCertificate=True;");
                                 .UseSqlServer(cs);
        //optionsBuilder.UseSqlServer("Data Source=VETRIVELMURUGAN;Initial Catalog = Insurance; User ID = sa; Password=***********;Integrated security=True;TrustServerCertificate=True;")
        optionsBuilder.UseSqlServer(cs)
                                 .LogTo(Console.WriteLine).EnableDetailedErrors();
        base.OnConfiguring(optionsBuilder);
    }
    readonly ILoggerFactory loggerFactory = new LoggerFactory();
    #endregion 


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__admin__AD0500A6C7ABCA31");

            entity.ToTable("admin", "insurance");

            entity.Property(e => e.AdminId).HasColumnName("adminId");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        modelBuilder.Entity<CityMaster>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__city_mas__DE9CEC38771DD4B4");

            entity.ToTable("city_master", "insurance");

            entity.Property(e => e.CityId).HasColumnName("City_id");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.StateId).HasColumnName("State_Id");

            entity.HasOne(d => d.State).WithMany(p => p.CityMasters)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stateF_id");
        });

        modelBuilder.Entity<CountryMaster>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__country___8037C7D643E9E2B0");

            entity.ToTable("country_master", "insurance");

            entity.Property(e => e.CountryId).HasColumnName("Country_id");
            entity.Property(e => e.Country)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__8CB382B19F2D5FD5");

            entity.ToTable("customers", "insurance");

            entity.Property(e => e.CustomerId).HasColumnName("Customer_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("First_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Last_name");
            //entity.Property(e => e.Age)
            //    .HasMaxLength(10)
            //    .IsUnicode(false)
            //    .HasColumnName("Age");

            entity.Property(e => e.StatusId).HasColumnName("Status_id");

           entity.HasOne(d => d.Status).WithMany(p => p.Customers)            
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_status2_id");
        });

        modelBuilder.Entity<GenderMaster>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK__gender_m__AF740A3C4D92378F");

            entity.ToTable("gender_master", "insurance");

            entity.Property(e => e.GenderId)
                .ValueGeneratedNever()
                .HasColumnName("Gender_id");
            entity.Property(e => e.Gender)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<InsuranceTypeMaster>(entity =>
        {
            entity.HasKey(e => e.InsuranceId).HasName("PK__insuranc__FFF1644B9C333A9C");

            entity.ToTable("insurance_type_master", "insurance");

            entity.Property(e => e.InsuranceId)
                .ValueGeneratedNever()
                .HasColumnName("Insurance_id");
            entity.Property(e => e.InsuranceType)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Insurance_type");
        });

        modelBuilder.Entity<MaritalStatusMaster>(entity =>
        {
            entity.HasKey(e => e.MaritalStatusId).HasName("PK__marital___EB830C2D851EA3C0");

            entity.ToTable("marital_status_master", "insurance");

            entity.Property(e => e.MaritalStatusId)
                .ValueGeneratedNever()
                .HasColumnName("Marital_status_id");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Marital_status");
        });

        modelBuilder.Entity<ModeOfPremiumMaster>(entity =>
        {
            entity.HasKey(e => e.ModeOfPremiumId).HasName("PK__mode_of___61535EFC29C4C980");

            entity.ToTable("mode_of_premium_master", "insurance");

            entity.Property(e => e.ModeOfPremiumId)
                .ValueGeneratedNever()
                .HasColumnName("mode_of_premium_id");
            entity.Property(e => e.ModeOfPremium)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("mode_of_premium");
        });

        modelBuilder.Entity<NomineeDetail>(entity =>
        {
            entity.HasKey(e => e.NomineeId).HasName("PK__nominee___917234FC2B1A1421");

            entity.ToTable("nominee_details", "insurance");

            entity.HasIndex(e => e.AadharNumber, "UQ__nominee___097AF695B810004D").IsUnique();

            entity.HasIndex(e => e.PanNumber, "UQ__nominee___0CB01CC46680DD9B").IsUnique();

            entity.HasIndex(e => e.MobileNumber, "UQ__nominee___9E090FFF6CB3CDBF").IsUnique();

            entity.Property(e => e.NomineeId).HasColumnName("Nominee_id");
            entity.Property(e => e.AadharNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Aadhar_number");
            entity.Property(e => e.Dob).HasColumnType("date");
            entity.Property(e => e.GenderId).HasColumnName("Gender_id");
            entity.Property(e => e.MobileNumber).HasColumnName("Mobile_number");
            entity.Property(e => e.NomineeName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Nominee_name");
            entity.Property(e => e.PanNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PAN_number");
            entity.Property(e => e.PolicyId).HasColumnName("Policy_id");
            entity.Property(e => e.RelationshipId).HasColumnName("Relationship_id");

            entity.HasOne(d => d.Gender).WithMany(p => p.NomineeDetails)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("fk_gender_id");

            entity.HasOne(d => d.Policy).WithMany(p => p.NomineeDetails)
                .HasForeignKey(d => d.PolicyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_policy2_id");

            entity.HasOne(d => d.Relationship).WithMany(p => p.NomineeDetails)
                .HasForeignKey(d => d.RelationshipId)
                .HasConstraintName("fk_relationship_id");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__payments__DA638B199F3EDA5F");

            entity.ToTable("payments", "insurance");

            entity.Property(e => e.PaymentId).HasColumnName("Payment_id");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("date")
                .HasColumnName("Payment_Date");
            entity.Property(e => e.PaymentTypeId).HasColumnName("Payment_type_id");
            entity.Property(e => e.PremiumId).HasColumnName("Premium_Id");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentTypeId)
                .HasConstraintName("fk_payment_type_id");

            entity.HasOne(d => d.Premium).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PremiumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_primium_id");
        });

        modelBuilder.Entity<PaymentTypeMaster>(entity =>
        {
            entity.HasKey(e => e.PaymentTypeId).HasName("PK__payment___19563F1C73524910");

            entity.ToTable("payment_type_master", "insurance");

            entity.Property(e => e.PaymentTypeId)
                .ValueGeneratedNever()
                .HasColumnName("Payment_type_id");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Payment_type");
        });

        modelBuilder.Entity<PersonalDetail>(entity =>
        {
            entity.HasKey(e => e.PersonalId).HasName("PK__personal__732C802264EA0558");

            entity.ToTable("personal_details", "insurance");

            entity.HasIndex(e => e.AadharNumber, "UQ__personal__097AF695983FC34F").IsUnique();

            entity.HasIndex(e => e.PanNumber, "UQ__personal__0CB01CC4E84EC167").IsUnique();

            entity.HasIndex(e => e.MobileNumber, "UQ__personal__9E090FFF73DA8EAF").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__personal__A9D1053460EF173E").IsUnique();

            entity.Property(e => e.PersonalId).HasColumnName("Personal_id");
            entity.Property(e => e.AadharNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Aadhar_number");
            entity.Property(e => e.CityId).HasColumnName("City_id");
            entity.Property(e => e.CountryId).HasColumnName("Country_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Dob).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.GenderId).HasColumnName("Gender_id");
            entity.Property(e => e.MaritalStatusId).HasColumnName("Marital_status_id");
            entity.Property(e => e.MobileNumber).HasColumnName("Mobile_number");
            entity.Property(e => e.PanNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PAN_number");
            entity.Property(e => e.PostalCode).HasColumnName("Postal_code");
            entity.Property(e => e.StateId).HasColumnName("State_id");
            entity.Property(e => e.Street)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.City).WithMany(p => p.PersonalDetails)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_city2_id");

            entity.HasOne(d => d.Country).WithMany(p => p.PersonalDetails)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_country2_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.PersonalDetails)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customer2_id");

            entity.HasOne(d => d.Gender).WithMany(p => p.PersonalDetails)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("fk_gender2_id");

            entity.HasOne(d => d.MaritalStatus).WithMany(p => p.PersonalDetails)
                .HasForeignKey(d => d.MaritalStatusId)
                .HasConstraintName("fk_marital_status2_id");

            entity.HasOne(d => d.State).WithMany(p => p.PersonalDetails)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_state2_id");
        });

        modelBuilder.Entity<PolicyDetail>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__policy_d__4569BF19CA8DD8D5");

            entity.ToTable("policy_details", "insurance");

            entity.Property(e => e.PolicyId).HasColumnName("Policy_id");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_id");
            entity.Property(e => e.DateOfExpire)
                .HasColumnType("date")
                .HasColumnName("Date_of_expire");
            entity.Property(e => e.DateOfIssue)
                .HasColumnType("date")
                .HasColumnName("Date_of_issue");
            entity.Property(e => e.InsuranceId).HasColumnName("Insurance_id");
            entity.Property(e => e.StatusId).HasColumnName("Status_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.PolicyDetails)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customer_id");

            entity.HasOne(d => d.Insurance).WithMany(p => p.PolicyDetails)
                .HasForeignKey(d => d.InsuranceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_insurance_id");

            entity.HasOne(d => d.Status).WithMany(p => p.PolicyDetails)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_status_id");
        });

        modelBuilder.Entity<PolicyValue>(entity =>
        {
            entity.HasKey(e => e.PremiumId).HasName("PK__policy_v__368DE5EA17999DD8");

            entity.ToTable("policy_value", "insurance");

            entity.HasIndex(e => e.PolicyId, "UQ__policy_v__47DA3F02C1715153").IsUnique();

            entity.Property(e => e.PremiumId).HasColumnName("Premium_id");
            entity.Property(e => e.AmountOfPeriod).HasColumnName("Amount_of_period");
            entity.Property(e => e.InsuredDeclaredValue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Insured_Declared_value");
            entity.Property(e => e.ModeOfPremiumId).HasColumnName("mode_of_premium_id");
            entity.Property(e => e.PolicyId).HasColumnName("policy_id");
            entity.Property(e => e.PremiumToBePaid)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("premium_to_be_paid");

            entity.HasOne(d => d.ModeOfPremium).WithMany(p => p.PolicyValues)
                .HasForeignKey(d => d.ModeOfPremiumId)
                .HasConstraintName("fk_mode_of_premium_id");

            entity.HasOne(d => d.Policy).WithOne(p => p.PolicyValue)
                .HasForeignKey<PolicyValue>(d => d.PolicyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_policy3_id");
        });

        modelBuilder.Entity<RelationshipMaster>(entity =>
        {
            entity.HasKey(e => e.RelationshipId).HasName("PK__relation__1D4D88B87EC455F9");

            entity.ToTable("relationship_master", "insurance");

            entity.Property(e => e.RelationshipId).HasColumnName("Relationship_id");
            entity.Property(e => e.Relationship)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StateMaster>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PK__state_ma__AF9444CFB582AA42");

            entity.ToTable("state_master", "insurance");

            entity.Property(e => e.StateId).HasColumnName("State_id");
            entity.Property(e => e.CountryId).HasColumnName("Country_Id");
            entity.Property(e => e.State)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Country).WithMany(p => p.StateMasters)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_countryF_id");
        });

        modelBuilder.Entity<StatusMaster>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__status_m__5191052418AFB1B4");

            entity.ToTable("status_master", "insurance");

            entity.Property(e => e.StatusId)
                .ValueGeneratedNever()
                .HasColumnName("Status_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
