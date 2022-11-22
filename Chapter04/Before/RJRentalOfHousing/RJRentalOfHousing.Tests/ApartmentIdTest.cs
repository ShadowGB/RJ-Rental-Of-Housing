using RJRentalOfHousing.Domain;

namespace RJRentalOfHousing.Tests
{
    public class ApartmentIdTest
    {
        [Fact]
        public void Two_Same_Value_Id_Should_Be_Equel()
        {
            Guid id = Guid.NewGuid();
            ApartmentId apartmentId1 = new ApartmentId(id);
            ApartmentId apartmentId2 = new ApartmentId(id);
            Assert.Equal(apartmentId1, apartmentId2);
        }
    }
}
