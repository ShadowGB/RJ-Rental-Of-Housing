namespace RJRentalOfHousing.Domain
{
    public static class Events
    {
        public class ApartmentCreated
        {
            public Guid Id { get; set; }
            public Guid OwnerId { get; set; }
        }

        public class ApartmentAreaUpdated
        {
            public Guid Id { get; set; }
            public decimal Areas { get; set; }
        }

        public class ApartmentAddressUpdated
        {
            public Guid Id { get; set; }
            public string Address { get; set; }
        }

        public class AparementRentUpdated
        {
            public Guid Id { get; set; }
            public decimal Rent { get; set; }
            public string CurrencyCode { get; set; }
        }

        public class ApartmentDepositUpdated
        {
            public Guid Id { get; set; }
            public decimal Deposit { get; set; }
            public string CurrencyCode { get; set; }
        }

        public class ApartmentRemarkUpdated
        {
            public Guid Id { get; set; }
            public string Remark { get; set; }
        }

        public class AparetmentSentForReview
        {
            public Guid Id { get; set; }
        }
    }
}
