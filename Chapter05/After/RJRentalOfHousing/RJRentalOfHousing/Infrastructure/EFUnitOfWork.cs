using RJRentalOfHousing.Framework;

namespace RJRentalOfHousing.Infrastructure
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly RentalDBContext _dbContext;

        public EFUnitOfWork(RentalDBContext dbContext) => _dbContext = dbContext;

        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}
