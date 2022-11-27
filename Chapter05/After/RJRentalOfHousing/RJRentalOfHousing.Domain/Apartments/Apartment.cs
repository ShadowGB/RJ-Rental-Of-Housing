using RJRentalOfHousing.Domain.Shared;
using RJRentalOfHousing.Framework;
using static RJRentalOfHousing.Domain.Shared.DomainExceptions;

namespace RJRentalOfHousing.Domain.Apartments
{
    public class Apartment : AggregateRoot<ApartmentId>
    {
        protected Apartment() { }

        public Apartment(ApartmentId id, UserId ownerId)
        {
            Pictures = new List<Picture>();
            Apply(new Events.ApartmentCreated
            {
                Id = id,
                OwnerId = ownerId
            });
        }

        public Guid ApartmentId { get; internal set; }

        public Area? Areas { get; internal set; }

        public Address? Address { get; internal set; }

        public Rent? Rent { get; internal set; }

        public Deposit? Deposit { get; internal set; }

        public UserId? Owner { get; internal set; }

        public string? Remark { get; internal set; }

        public UserId? ApprovedBy { get; internal set; }

        public ApartmentState State { get; internal set; }

        public List<Picture> Pictures { get; internal set; }

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

        public void AddPicture(Uri pictureUri, PictureSize size)
            => Apply(new Events.PictureAddedToAApartment
            {
                PictureId = new Guid(),
                ApartmentId = Id,
                Url = pictureUri.ToString(),
                Height = size.Height,
                Width = size.Width,
                Order = Pictures.Max(x => x.Order)
            });

        private Picture FindFirstPicture(PictureId id) => Pictures.FirstOrDefault(x => x.Id == id);

        private Picture FirstPicture => Pictures.OrderBy(x => x.Order).FirstOrDefault();

        public void ResizePicture(PictureId pictureId, PictureSize newSize)
        {
            var picture = FindFirstPicture(pictureId);
            if (picture == null)
                throw new InvalidOperationException("不能修改不存在的图片的尺寸");
            picture.Resize(newSize);
        }

        protected override void EnsureValidState()
        {
            var valid =
                Id != null &&
                Owner != null &&

                  State switch
                  {
                      ApartmentState.PendingReview =>
                      Areas != null &&
                      Address != null &&
                      Rent?.Amount > 0 &&
                      Deposit?.Amount > 0 &&
                      FirstPicture.HasCorrectSize(),
                      ApartmentState.Renting =>
                      Areas != null &&
                      Address != null &&
                      Rent?.Amount > 0 &&
                      Deposit?.Amount > 0 &&
                      ApprovedBy != null &&
                      FirstPicture.HasCorrectSize(),
                      _ => true
                  }
                ;
            if (!valid)
                throw new InvalidEntityStateException(this, $"实体提交状态{State}检查失败");
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.ApartmentCreated e:
                    Id = new ApartmentId(e.Id);
                    Owner = new UserId(e.OwnerId);
                    State = ApartmentState.Created;
                    ApartmentId = e.Id;
                    break;
                case Events.ApartmentAreaUpdated e:
                    Areas = new Area(e.Areas);
                    break;
                case Events.ApartmentAddressUpdated e:
                    Address = new Address(e.Address);
                    break;
                case Events.AparementRentUpdated e:
                    Rent = new Rent(e.Rent, e.CurrencyCode);
                    break;
                case Events.ApartmentDepositUpdated e:
                    Deposit = new Deposit(e.Deposit, e.CurrencyCode);
                    break;
                case Events.ApartmentRemarkUpdated e:
                    Remark = e.Remark;
                    break;
                case Events.AparetmentSentForReview _:
                    State = ApartmentState.PendingReview;
                    break;
                case Events.PictureAddedToAApartment e:
                    var picture = new Picture(Apply);
                    ApplyToEntity(picture, e);
                    Pictures.Add(picture);
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
