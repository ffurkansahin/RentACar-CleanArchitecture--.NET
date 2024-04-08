using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IFuelTypeRepository : IAsyncRepository<FuelType,Guid>, IRepository<FuelType, Guid>
{

}
