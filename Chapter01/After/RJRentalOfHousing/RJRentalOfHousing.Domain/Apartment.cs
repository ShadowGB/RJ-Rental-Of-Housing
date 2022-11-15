using System.Net;

namespace RJRentalOfHousing.Domain
{
    public class Apartment:Entity
    {
        public Apartment(ApartmentId id, UserId ownerId)
            => Apply(new Events.ApartmentCreated
            {
                Id = id,
                OwnerId = ownerId
            });

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
            => Apply(new Events.ApartmentAreaUpdated
            {
                Id = Id,
                Areas = area
            });

        public void SetAddress(Address address)
            => Apply(new Events.ApartmentAddressUpdated
            {
                Id = Id,
                Address = address
            });

        public void SetRent(Price rent)
            => Apply(new Events.AparementRentUpdated
            {
                Id = Id,
                Rent = rent.Amount,
                CurrencyCode = rent.Currency.CurrencyCode
            });

        public void SetDeposit(Price deposit)
            => Apply(new Events.ApartmentDepositUpdated
            {
                Id = Id,
                Deposit = deposit.Amount,
                CurrencyCode = deposit.Currency.CurrencyCode
            });

        public void SetRemark(string remark)
            => Apply(new Events.ApartmentRemarkUpdated
            {
                Id = Id,
                Remark = remark
            });

        public void RequestToPublish()
            => Apply(new Events.AparetmentSentForReview
            {
                Id = Id,
            });

        protected override void EnsureValidState()
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
                      Rent?.Amount > 0 &&
                      Deposit?.Amount > 0,
                      ApartmentState.Renting =>
                      Areas != null &&
                      Address != null &&
                      Rent?.Amount > 0 &&
                      Deposit?.Amount > 0 &&
                      ApprovedBy != null,
                      _ => true
                  }
                );
            if (!valid)
                throw new InvalidEntityStateException(this,$"实体提交状态{State}检查失败");
        }

        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.ApartmentCreated e:
                    Id = new ApartmentId(e.Id);
                    Owner = new UserId(e.OwnerId);
                    State = ApartmentState.Created;
                    break;
                case Events.ApartmentAreaUpdated e:
                    Areas = new Area(e.Areas);
                    break;
                case Events.ApartmentAddressUpdated e:
                    Address = new Address(e.Address);
                    break;
                case Events.AparementRentUpdated e:
                    Rent = new Price(e.Rent, e.CurrencyCode);
                    break;
                case Events.ApartmentDepositUpdated e:
                    Deposit = new Price(e.Deposit, e.CurrencyCode);
                    break;
                case Events.ApartmentRemarkUpdated e:
                    Remark = e.Remark;
                    break;
                case Events.AparetmentSentForReview _:
                    State = ApartmentState.PendingReview;
                    break;
            }
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
