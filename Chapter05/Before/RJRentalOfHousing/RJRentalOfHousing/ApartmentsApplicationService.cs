using RJRentalOfHousing.Domain;
using RJRentalOfHousing.Framework;
using static RJRentalOfHousing.Contracts.Apartments;

namespace RJRentalOfHousing
{
    public class ApartmentsApplicationService : IApplicationService
    {
        private readonly IEntityStore _entityStore;
        private readonly ICurrencyLookup _currencyLookup;

        public ApartmentsApplicationService(IEntityStore entityStore,ICurrencyLookup currencyLookup)
        {
            _entityStore = entityStore;
            _currencyLookup = currencyLookup;
        }

        private async Task HandldCreate(V1.Create cmd)
        {
            if (await _entityStore.Exists(cmd.Id.ToString()))
                throw new InvalidOperationException($"{cmd.Id}已存在");
            var apartment = new Apartment(new ApartmentId(cmd.Id), new UserId(cmd.OwnerId));
            await _entityStore.Save(apartment);
        }

        private async Task HandleUpdate(Guid ApartmentId,Action<Apartment> operation)
        {
            var apartment = await _entityStore.Load<Apartment>(ApartmentId.ToString());
            if (apartment == null)
                throw new InvalidOperationException($"{ApartmentId}不存在");
            operation(apartment);
            await _entityStore.Save(apartment);
        }

        public async Task Handle(object command)
        {
            switch(command)
            {
                case V1.Create cmd:
                    await HandldCreate(cmd);
                    break;
                case V1.SetArea cmd:
                    await HandleUpdate(cmd.Id, x => x.SetArea(new Area(cmd.Areas)));
                    break;
                case V1.SetAddress cmd:
                    await HandleUpdate(cmd.Id, x => x.SetAddress(new Address(cmd.Address)));
                    break;
                case V1.SetRent cmd:
                    await HandleUpdate(cmd.Id, x => Price.FromDecimal(cmd.Rent, cmd.CurrencyCode, _currencyLookup));
                    break;
                case V1.SetDeposit cmd:
                    await HandleUpdate(cmd.Id, x => Price.FromDecimal(cmd.Deposit, cmd.CurrencyCode, _currencyLookup));
                    break;
                case V1.SetRemark cmd:
                    await HandleUpdate(cmd.Id, x => x.SetRemark(cmd.Remark));
                    break;
                case V1.SentForReview cmd:
                    await HandleUpdate(cmd.Id, x => x.RequestToPublish());
                    break;
                default:
                    throw new InvalidOperationException($"未知的命令类型:{command.GetType().FullName}");
            }
        }
    }
}
