namespace RJRentalOfHousing.Domain.Apartments
{
    public interface IApartmentRepository
    {
        Task<Apartment> Load(ApartmentId id);
        Task Add(Apartment entity);
        Task<bool> Exists(ApartmentId id);
    }
}
