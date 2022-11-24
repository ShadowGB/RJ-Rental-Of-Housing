namespace RJRentalOfHousing.Framework
{
    public interface IApplicationService
    {
        Task Handle(object command);
    }
}
