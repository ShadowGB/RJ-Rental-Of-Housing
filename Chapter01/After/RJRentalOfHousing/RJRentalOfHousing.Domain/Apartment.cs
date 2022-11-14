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

        public Address Address { get;internal set; }

        public Price Rent { get; internal set; }

        public Price Deposit { get; internal set; }

        public UserId Owner { get; internal set; }

        public string Remark { get; internal set; }

        public UserId ApprovedBy { get; internal set; }

        public void SetArea(Area area) => Areas = area;

        public void SetAddress(Address address) => Address = address;

        public void SetRent(Price rent) => Rent = rent;

        public void SetDeposit(Price deposit) => Deposit = deposit;

        public void SetOwner(UserId owner) => Owner = owner;

        public void SetRemark(string remark) => Remark = remark;
    }
}
