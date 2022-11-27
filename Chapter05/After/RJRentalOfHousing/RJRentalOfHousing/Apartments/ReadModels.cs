namespace RJRentalOfHousing.Apartments
{
    public static class ReadModels
    {
        public class ApartmentDetails
        {
            public Guid ApartmentId { get; set; }
            public string Address { get; set; }
            public decimal Areas { get; set; }
            public decimal Rent { get; set; }
            public string RentCurrencyCode { get; set; }
            public decimal Deposit { get; set; }
            public string DepositCurrencyCode { get; set; }
            public string OwnerDisplayName { get; set; }
            public string[] PhotoUrls { get; set; }
            public string Remark { get; set; }
        }

        public class ApartmentListItem
        {
            public Guid ApartmentId { get; set; }
            public string Address { get; set; }
            public decimal Areas { get; set; }
            public decimal Rent { get; set; }
            public string RentCurrencyCode { get; set; }
            public decimal Deposit { get; set; }
            public string DepositCurrencyCode { get; set; }
        }
    }
}
