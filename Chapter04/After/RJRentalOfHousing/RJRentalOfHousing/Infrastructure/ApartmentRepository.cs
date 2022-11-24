using RJRentalOfHousing.Domain;

namespace RJRentalOfHousing.Infrastructure
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly ApartmentDbContext _dbContext;

        public ApartmentRepository(ApartmentDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(Apartment entity) => await _dbContext.Apartments.AddAsync(entity);

        public async Task<bool> Exists(ApartmentId id) => await _dbContext.Apartments.FindAsync(id.Value) != null;

        public async Task<Apartment> Load(ApartmentId id) => await _dbContext.Apartments.FindAsync(id.Value);
    }
}
