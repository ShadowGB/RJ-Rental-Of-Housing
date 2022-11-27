using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RJRentalOfHousing.Domain.Apartments;
using RJRentalOfHousing.Domain.UserProfiles;

namespace RJRentalOfHousing.Infrastructure
{
    public class RentalDBContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public RentalDBContext(DbContextOptions<RentalDBContext> options,ILoggerFactory loggerFactory) : base(options)
            =>_loggerFactory = loggerFactory;
        
        public DbSet<Apartment> Apartments { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApartmentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PictureEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserProfileEntityTypeConfiguration());
        }
    }

    public class ApartmentEntityTypeConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.HasKey(x => x.ApartmentId);
            builder.OwnsOne(x => x.Id);
            builder.OwnsOne(x => x.Address);
            builder.OwnsOne(x => x.Areas);
            builder.OwnsOne(x => x.Rent, r => r.OwnsOne(c => c.Currency));
            builder.OwnsOne(x => x.Deposit, r => r.OwnsOne(c => c.Currency));
            builder.OwnsOne(x => x.Owner);
            builder.OwnsOne(x => x.ApprovedBy);
        }
    }

    public class PictureEntityTypeConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(x => x.PictureId);
            builder.OwnsOne(x => x.Id);
            builder.OwnsOne(x => x.ParentId);
            builder.OwnsOne(x => x.Size);
        }
    }

    public class UserProfileEntityTypeConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(x => x.UserProfileId);
            builder.OwnsOne(x => x.Id);
            builder.OwnsOne(x => x.DisplayName);
            builder.OwnsOne(x => x.FullName);
        }
    }

    public static class AppBuilderDatabaseExtensions
    {
        public static void EnsureDatabase(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<RentalDBContext>();
            if (!context.Database.EnsureCreated())
                context.Database.Migrate();
        }
    }
}
