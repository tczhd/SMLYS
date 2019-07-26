using Microsoft.EntityFrameworkCore;
using SMLYS.ApplicationCore.Entities;
using SMLYS.ApplicationCore.Entities.CommonAggregate;
using SMLYS.ApplicationCore.Entities.DoctorAggregate;
using SMLYS.ApplicationCore.Entities.InvoiceAggregate;
using SMLYS.ApplicationCore.Entities.PatientAggregate;
using SMLYS.ApplicationCore.Entities.SettingsAggregate;
using SMLYS.ApplicationCore.Entities.UserAggregate;


namespace SMLYS.Infrastructure.Data
{
    public partial class SMLYSContext : DbContext
    {
        public SMLYSContext()
        {
        }

        public SMLYSContext(DbContextOptions<SMLYSContext> options)
            : base(options)
        {
        }

        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<AddressType> AddressType { get; set; }
        public virtual DbSet<Clinic> Clinic { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<DoctorSpecality> DoctorSpecality { get; set; }
        public virtual DbSet<Family> Family { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItem { get; set; }
        public virtual DbSet<InvoiceReOccouring> InvoiceReOccouring { get; set; }
        public virtual DbSet<InvoiceReOccouringType> InvoiceReOccouringType { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<PatientStatus> PatientStatus { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<SiteUser> SiteUser { get; set; }
        public virtual DbSet<SiteUserLevel> SiteUserLevel { get; set; }
        public virtual DbSet<Specality> Specality { get; set; }
        public virtual DbSet<Tax> Tax { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<InvoicePayment> InvoicePayment { get; set; }
        public virtual DbSet<PatientCardOnFile> PatientCardOnFile { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-N65A546;Initial Catalog=SMLYS_Data;User ID=sa;Password=Lq160011;Persist Security Info=True;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasAnnotation("ProductVersion", "3.0.0-preview.19074.3");
            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.SiteUsers)
                .WithOne(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Address2).HasMaxLength(50);

                entity.Property(e => e.AttentionTo).HasMaxLength(150);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.CreatedDateUtc)
                    .HasColumnName("CreatedDateUTC")
                    .HasColumnType("datetime");

                entity.Property(e => e.Region).HasMaxLength(50);

                entity.Property(e => e.UpdatedDateUtc)
                    .HasColumnName("UpdatedDateUTC")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.AddressType)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.AddressTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Address_AddressType");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Address_Country");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.AddressCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.RegionNavigation)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_Address_Region");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.AddressUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy);
            });

            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.AddressType1)
                    .IsRequired()
                    .HasColumnName("AddressType")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Clinic>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDateUtc)
                    .HasColumnName("CreatedDateUTC")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Note)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDateUtc)
                    .HasColumnName("UpdatedDateUTC")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ClinicCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ClinicUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Iso2)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDateUtc)
                    .HasColumnName("CreatedDateUTC")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Note)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDateUtc)
                    .HasColumnName("UpdatedDateUTC")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Doctor)
                    .HasForeignKey(d => d.ClinicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctor_Clinic");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.DoctorCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.DoctorUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy);
            });

            modelBuilder.Entity<DoctorSpecality>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.DoctorSpecality)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DoctorSpecality_Doctor");

                entity.HasOne(d => d.Specality)
                    .WithMany(p => p.DoctorSpecality)
                    .HasForeignKey(d => d.SpecalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DoctorSpecality_Specality");
            });

            modelBuilder.Entity<Family>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.AmountPaid).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.DiscountTotal).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.TaxTotal).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 6)");
                entity.Property(e => e.DisplayId).ValueGeneratedOnAddOrUpdate(); 
                entity.Property(e => e.EncryptId).HasMaxLength(100);

                entity.Property(e => e.UpdatedDateUtc)
                    .HasColumnName("UpdatedDateUTC")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.ClinicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_Clinic");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.InvoiceCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_Doctor");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_Patient");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.InvoiceUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy);
            });

            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.TaxTotal).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 6)");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceItem)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceItem_Invoice");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.InvoiceItem)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceItem_Item");
            });

            modelBuilder.Entity<InvoiceReOccouring>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.EndDateUtc)
                    .HasColumnName("EndDateUTC")
                    .HasColumnType("datetime");

                entity.Property(e => e.StartDateUtc)
                    .HasColumnName("StartDateUTC")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceReOccouring)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceReOccouring_Invoice");

                entity.HasOne(d => d.InvoiceReOccouringType)
                    .WithMany(p => p.InvoiceReOccouring)
                    .HasForeignKey(d => d.InvoiceReOccouringTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceReOccouring_InvoiceReOccouringType");
            });

            modelBuilder.Entity<InvoiceReOccouringType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ReOccouringName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.UpdatedDateUtc)
                    .HasColumnName("UpdatedDateUTC")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.ClinicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_Clinic");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.UpdatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDateUtc)
                    .HasColumnName("CreatedDateUTC")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Note)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDateUtc)
                    .HasColumnName("UpdatedDateUTC")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patient_Address");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.ClinicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patient_Clinic");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PatientCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patient_Doctor");

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patient_Family");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patient_PatientStatus");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.PatientUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy);
            });

            modelBuilder.Entity<PatientStatus>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<SiteUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnType("nvarchar(450)")
                    .HasMaxLength(450)
                    .IsFixedLength();

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.SiteUser)
                    .HasForeignKey(d => d.ClinicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SiteUser_Clinic");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.SiteUser)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_SiteUser_Doctor");

                entity.HasOne(d => d.SiteUserLevel)
                    .WithMany(p => p.SiteUser)
                    .HasForeignKey(d => d.SiteUserLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SiteUser_SiteUserLevel");
            });

            modelBuilder.Entity<SiteUserLevel>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Specality>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tax>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.TaxName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.TaxRate).HasColumnType("decimal(18, 6)");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.AuthorizationCode)
                    .HasMaxLength(50);
                entity.Property(e => e.CardToken)
                    .HasMaxLength(150);

                entity.Property(e => e.PaymentDate)
                  .HasColumnName("PaymentDate")
                  .HasColumnType("datetime");

                entity.Property(e => e.UpdatedDateUtc)
                  .HasColumnName("UpdatedDateUTC")
                  .HasColumnType("datetime");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 6)");

                entity.HasOne(d => d.SiteUser)
                .WithMany(p => p.PaymentUpdatedByNavigation)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_SiteUser_UpdatedBy");
            });

            modelBuilder.Entity<InvoicePayment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.AmountPaid).HasColumnType("decimal(18, 6)");
            });

            modelBuilder.Entity<PatientCardOnFile>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CardToken)
                    .HasMaxLength(150);
                entity.Property(e => e.CustomerCode)
                  .HasMaxLength(150);
                entity.Property(e => e.CardF4L4)
                .HasMaxLength(8);

                entity.HasOne(d => d.SiteUser)
                 .WithMany(p => p.PatientCardOnFileUpdatedByNavigation)
                 .HasForeignKey(d => d.UpdatedBy)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_PatientCardOnFile_SiteUser_UpdatedBy");
            });
        }
    }
}
