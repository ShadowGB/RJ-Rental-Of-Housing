using RJRentalOfHousing.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJRentalOfHousing.Tests
{
    public class Apartment_Publish_Spec
    {
        private readonly Apartment _apartment;

        public Apartment_Publish_Spec()
        {
            _apartment = new Apartment(new ApartmentId(Guid.NewGuid()), new UserId(Guid.NewGuid()));
        }

        [Fact]
        public void Can_publish_a_valid_apartment()
        {
            _apartment.SetArea(new Area(120m));
            _apartment.SetRent(Price.FromDecimal(1000m, "CNY", new FakeCurrencyLookup()));
            _apartment.SetDeposit(Price.FromDecimal(2000m, "CNY", new FakeCurrencyLookup()));
            _apartment.SetAddress(new Address("广西壮族自治区南宁市青秀区桃源路某个角落里"));
            _apartment.RequestToPublish();
            Assert.Equal(Apartment.ApartmentState.PendingReview, _apartment.State);
        }

        [Fact]
        public void Cannot_Publish_without_Area()
        {
            _apartment.SetRent(Price.FromDecimal(1000m, "CNY", new FakeCurrencyLookup()));
            _apartment.SetDeposit(Price.FromDecimal(2000m, "CNY", new FakeCurrencyLookup()));
            _apartment.SetAddress(new Address("广西壮族自治区南宁市青秀区桃源路某个角落里"));
            Assert.Throws<InvalidEntityStateException>(()=>_apartment.RequestToPublish());
        }

        [Fact]
        public void Cannot_Publish_without_Rent()
        {
            _apartment.SetArea(new Area(120m));
            _apartment.SetDeposit(Price.FromDecimal(2000m, "CNY", new FakeCurrencyLookup()));
            _apartment.SetAddress(new Address("广西壮族自治区南宁市青秀区桃源路某个角落里"));
            Assert.Throws<InvalidEntityStateException>(() => _apartment.RequestToPublish());
        }

        [Fact]
        public void Cannot_Publish_without_Deposit()
        {
            _apartment.SetArea(new Area(120m));
            _apartment.SetRent(Price.FromDecimal(1000m, "CNY", new FakeCurrencyLookup()));
            _apartment.SetAddress(new Address("广西壮族自治区南宁市青秀区桃源路某个角落里"));
            Assert.Throws<InvalidEntityStateException>(() => _apartment.RequestToPublish());
        }

        [Fact]
        public void Cannot_Publish_without_Address()
        {
            _apartment.SetArea(new Area(120m));
            _apartment.SetRent(Price.FromDecimal(1000m, "CNY", new FakeCurrencyLookup()));
            _apartment.SetDeposit(Price.FromDecimal(2000m, "CNY", new FakeCurrencyLookup()));
            Assert.Throws<InvalidEntityStateException>(() => _apartment.RequestToPublish());
        }


        [Fact]
        public void Cannot_Publish_with_zero_Deposit()
        {
            _apartment.SetArea(new Area(120m));
            _apartment.SetRent(Price.FromDecimal(1000m, "CNY", new FakeCurrencyLookup()));
            _apartment.SetDeposit(Price.FromDecimal(0m, "CNY", new FakeCurrencyLookup()));
            _apartment.SetAddress(new Address("广西壮族自治区南宁市青秀区桃源路某个角落里"));
            Assert.Throws<InvalidEntityStateException>(() => _apartment.RequestToPublish());
        }

        [Fact]
        public void Cannot_Publish_with_zero_Rent()
        {
            _apartment.SetArea(new Area(120m));
            _apartment.SetRent(Price.FromDecimal(0m, "CNY", new FakeCurrencyLookup()));
            _apartment.SetDeposit(Price.FromDecimal(2000m, "CNY", new FakeCurrencyLookup()));
            _apartment.SetAddress(new Address("广西壮族自治区南宁市青秀区桃源路某个角落里"));
            Assert.Throws<InvalidEntityStateException>(() => _apartment.RequestToPublish());
        }
    }
}
