using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.Update;

public class UpdateBrandCommand : IRequest<UpdateBrandResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, UpdateBrandResponse>
	{
		private readonly IBrandRepository _brandRepository;
		private readonly IMapper _mapper;

		public UpdateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
		{
			_brandRepository = brandRepository;
			_mapper = mapper;
		}
		public async Task<UpdateBrandResponse> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
		{
			Brand? brand = await _brandRepository.GetAsync(b => b.Id == request.Id,cancellationToken : cancellationToken);

			brand.UpdatedDate = DateTime.UtcNow;
			brand = _mapper.Map(request, brand);

			await _brandRepository.UpdateAsync(brand);

			return _mapper.Map<UpdateBrandResponse>(brand);
			
		}
	}
}
