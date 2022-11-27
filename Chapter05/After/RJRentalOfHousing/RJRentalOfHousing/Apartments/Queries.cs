using System.Data.Common;
using static RJRentalOfHousing.Apartments.ReadModels;
using static RJRentalOfHousing.Apartments.QueryModels;
using Dapper;
using RJRentalOfHousing.Domain.Apartments;

namespace RJRentalOfHousing.Apartments
{
    public static class Queries
    {
        private static int Offset(int page, int pageSize) => page * pageSize;

        public static Task<IEnumerable<ApartmentListItem>> Query(this DbConnection connection, GetCreatedApartment query)
            => connection.QueryAsync<ApartmentListItem>("SELECT \"ApartmentId\",\"Areas_Value\" Areas,\"Address_Value\" Address,\"Rent_Amount\" Rent,\"Rent_Currency_CurrencyCode\" RentCurrencyCode,\"Deposit_Amount\" Deposit,  \"Deposit_Currency_CurrencyCode\" DepositCurrencyCode FROM \"Apartments\" Where \"State\"=@State LIMIT @PageSize OFFSET @Offset",
                new
                {
                    State = (int)Apartment.ApartmentState.Created,
                    PageSize = query.PageSize,
                    Offset = Offset(query.Page, query.PageSize)
                });

    }
}
