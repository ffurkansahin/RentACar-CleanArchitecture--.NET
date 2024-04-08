using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class FuelTypeRepository : EfRepositoryBase<FuelType, Guid, BaseDbContext>, IFuelTypeRepository
{
	public FuelTypeRepository(BaseDbContext context) : base(context)
	{
	}
}

