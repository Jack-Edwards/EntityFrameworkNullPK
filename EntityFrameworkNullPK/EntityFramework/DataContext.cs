using EntityFrameworkNullPK.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EntityFrameworkNullPK.EntityFramework
{
    public class DataContext : DbContext
    {
        private readonly DataContextConfiguration _configuration;

        /// <summary>
        /// Constructor for unit testing.
        /// </summary>
        public DataContext()
        {
            _configuration = new DataContextConfiguration();
        }

        public DataContext(IOptions<DataContextConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.ConnectionString);
            }
        }

        public DbSet<CompanyEntity> Companies { get; set; } = null!;
        public DbSet<UserEntity> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            ConfigureCompanies(modelBuilder);
            ConfigureDivisions(modelBuilder);
            ConfigureUsers(modelBuilder);
            ConfigureUserDivisions(modelBuilder);
        }

        private void ConfigureCompanies(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyEntity>(entity =>
            {
                entity.ToTable("company");

                entity.Property(x => x.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("company_id");

                entity.Property(x => x.Name)
                    .HasMaxLength(256)
                    .HasColumnName("company_name");

                entity.HasMany(x => x.Divisions)
                    .WithOne(x => x.Company)
                    .HasForeignKey(x => x.CompanyId);

                entity.HasMany(x => x.Users)
                    .WithOne(x => x.Company)
                    .HasForeignKey(x => x.CompanyId);
            });
        }

        private void ConfigureDivisions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DivisionEntity>(entity =>
            {
                entity.ToTable("division");

                entity.Property(x => x.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("division_id");

                entity.Property(x => x.Name)
                    .HasColumnName("division_name");

                entity.Property(x => x.CompanyId)
                    .HasColumnName("company_id");

                entity.HasOne(x => x.AsUser)
                    .WithOne(x => x.AsDivision)
                    .HasForeignKey<UserEntity>(x => x.Id)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }

        private void ConfigureUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("r_members");

                entity.Property(x => x.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("recruiter_id");

                entity.Property(x => x.FirstName)
                    .HasColumnName("first_name");

                entity.Property(x => x.LastName)
                    .HasColumnName("last_name");

                entity.Property(x => x.CompanyId)
                    .HasColumnName("company_id");
            });
        }

        private void ConfigureUserDivisions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDivisionEntity>(entity =>
            {
                entity.ToTable("recruiter_division");

                entity.HasKey(x => new { x.UserId, x.DivisionId });

                entity.Property(x => x.UserId)
                    .HasColumnName("recruiter_id");

                entity.Property(x => x.DivisionId)
                    .HasColumnName("division_id");

                entity.HasOne(x => x.Division)
                    .WithMany(x => x.DivisionUsers)
                    .HasForeignKey(x => x.DivisionId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(x => x.User)
                    .WithMany(x => x.UserDivisions)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
