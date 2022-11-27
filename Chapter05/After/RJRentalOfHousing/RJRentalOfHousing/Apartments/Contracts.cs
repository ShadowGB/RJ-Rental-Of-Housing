namespace RJRentalOfHousing.Apartments
{
    public static class Contracts
    {
        public static class V1
        {
            public class Create
            {
                public Guid Id { get; set; }
                public Guid OwnerId { get; set; }
            }

            public class SetArea
            {
                public Guid Id { get; set; }
                public decimal Areas { get; set; }
            }

            public class SetAddress
            {
                public Guid Id { get; set; }
                public string Address { get; set; }
            }

            public class SetRent
            {
                public Guid Id { get; set; }
                public decimal Rent { get; set; }
                public string CurrencyCode { get; set; }
            }

            public class SetDeposit
            {
                public Guid Id { get; set; }
                public decimal Deposit { get; set; }
                public string CurrencyCode { get; set; }
            }

            public class SetRemark
            {
                public Guid Id { get; set; }
                public string Remark { get; set; }
            }

            public class SentForReview
            {
                public Guid Id { get; set; }
            }
        }
    }
}
