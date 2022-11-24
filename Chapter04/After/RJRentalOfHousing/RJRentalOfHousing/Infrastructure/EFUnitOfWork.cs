using RJRentalOfHousing.Framework;

namespace RJRentalOfHousing.Infrastructure
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly ApartmentDbContext _dbContext;

        public EFUnitOfWork(ApartmentDbContext dbContext) => _dbContext = dbContext;

        public Task Commit() => _dbContext.SaveChangesAsync();
    }
}
