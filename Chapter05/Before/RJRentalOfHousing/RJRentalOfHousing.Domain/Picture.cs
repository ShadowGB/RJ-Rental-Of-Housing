using RJRentalOfHousing.Framework;

namespace RJRentalOfHousing.Domain
{
    public class Picture : Entity<PictureId>
    {
        protected Picture() { }

        internal Picture(Action<object> applier) : base(applier) { }

        public Guid PictureId { get; internal set; }
        public PictureSize Size { get; internal set; }
        public Uri Location { get; internal set; }
        public int Order { get; internal set; }
        public ApartmentId ParentId { get; internal set; }

        public void Resize(PictureSize newSize)
            => Apply(new Events.ApartmentPictureResized
            {
                PictureId = Id.Value,
                Height = newSize.Height,
                Width = newSize.Width,
            });

        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.PictureAddedToAApartment e:
                    Id = new PictureId(e.PictureId);
                    Location = new Uri(e.Url);
                    Size = new PictureSize{ Height = e.Height, Width = e.Width };
                    Order = e.Order;
                    break;
                case Events.ApartmentPictureResized e:
                    Size = new PictureSize { Height = e.Height, Width = e.Width };
                    break;
            }
        }
    }

    public record PictureId
    {
        public Guid Value { get; internal set; }

        public PictureId(Guid value) => Value = value;
    }
}
