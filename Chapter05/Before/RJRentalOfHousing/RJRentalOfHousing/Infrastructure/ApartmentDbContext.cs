using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RJRentalOfHousing.Domain;

namespace RJRentalOfHousing.Infrastructure
{
    public class ApartmentDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public ApartmentDbContext(DbContextOptions<ApartmentDbContext> options,ILoggerFactory loggerFactory) : base(options)
            =>_loggerFactory = loggerFactory;
        
        public DbSet<Apartment> Apartments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApartmentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PictureEntityTypeConfiguration());
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

    public static class AppBuilderDatabaseExtensions
    {
        public static void EnsureDatabase(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<ApartmentDbContext>();
            if (!context.Database.EnsureCreated())
                context.Database.Migrate();
        }
    }
}
