namespace RJRentalOfHousing.Apartments
{
    public static class QueryModels
    {
        public class GetAllApartmentsByPage
        {
            public int Page { get; set;}
            public int PageSize { get; set;}
        }

        public class GetOwnersApartment
        {
            public Guid OwnerId { get; set;}
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetCreatedApartment
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetApartmentDetailById
        {
            public Guid ApartmentId { get; set; }
        }
    }
}
