namespace RJRentalOfHousing.Domain
{
    public class Apartment
    {
        public Apartment(ApartmentId id, UserId ownerId)
        {
            Id = id;
            Owner = ownerId;
            State = ApartmentState.Created;
        }

        public ApartmentId Id { get; internal set; }

        public Area Areas { get; internal set; }

        public Address Address { get;internal set; }

        public Price Rent { get; internal set; }

        public Price Deposit { get; internal set; }

        public UserId Owner { get; internal set; }

        public string Remark { get; internal set; }

        public UserId ApprovedBy { get; internal set; }

        public ApartmentState State { get; internal set; }

        public void SetArea(Area area)
        {
            Areas = area;
            EnsureValidState();
        }

        public void SetAddress(Address address)
        {
            Address = address;
            EnsureValidState();
        }

        public void SetRent(Price rent)
        {
            Rent = rent;
            EnsureValidState();
        }

        public void SetDeposit(Price deposit)
        {
            Deposit = deposit;
            EnsureValidState();
        }

        public void SetOwner(UserId owner)
        {
            Owner = owner;
            EnsureValidState();
        }

        public void SetRemark(string remark)
        {
            Remark = remark;
            EnsureValidState();
        }

        public void RequestToPublish()
        {
            State = ApartmentState.Renting;
            EnsureValidState();
        }

        protected void EnsureValidState()
        {
            var valid =
                Id != null &&
                Owner != null &&
                (
                  State switch
                  {
                      ApartmentState.PendingReview =>
                      Areas != null &&
                      Address != null &&
                      Rent.Amount > 0 &&
                      Deposit.Amount > 0,
                      ApartmentState.Renting =>
                      Areas != null &&
                      Address != null &&
                      Rent.Amount > 0 &&
                      Deposit.Amount > 0 &&
                      ApprovedBy != null,
                      _ => true
                  }
                );
            if (!valid)
                throw new InvalidEntityStateException(this,$"实体提交状态{State}检查失败");
        }

        public enum ApartmentState
        {
            Created,
            PendingReview,
            Renting,
            Rented
        }
    }


}
