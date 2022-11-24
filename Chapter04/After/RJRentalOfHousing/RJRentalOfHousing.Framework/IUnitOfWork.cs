namespace RJRentalOfHousing.Framework
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
