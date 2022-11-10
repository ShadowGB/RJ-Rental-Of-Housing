namespace RJRentalOfHousing.Domain
{
    public class Apartment
    {
        public Apartment(ApartmentId id, UserId ownerId)
        {
            Id = id;
            Owner = ownerId;
        }

        public ApartmentId Id { get; internal set; }

        public Area Areas { get; internal set; }

        public string Address { get;internal set; }

        public decimal Rent { get; internal set; }

        public decimal Deposit { get; internal set; }

        public UserId Owner { get; internal set; }

        public string Remark { get; internal set; }

        public UserId ApprovedBy { get; internal set; }
    }
}
