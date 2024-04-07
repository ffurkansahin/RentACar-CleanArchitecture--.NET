using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.Delete;

public class DeleteBrandCommand:IRequest<DeleteBrandResponse>
{
    public Guid Id { get; set; }

    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, DeleteBrandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;

		public DeleteBrandCommandHandler(IMapper mapper, IBrandRepository brandRepository)
		{
			_mapper = mapper;
			_brandRepository = brandRepository;
		}

		public async Task<DeleteBrandResponse> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
		{
			Brand? brand = await _brandRepository.GetAsync(b => b.Id == request.Id,cancellationToken:cancellationToken);
			//brand.DeletedDate= DateTime.UtcNow;

			await _brandRepository.DeleteAsync(brand);//Soft delete

			return _mapper.Map<DeleteBrandResponse>(brand);
			
		}
	}
}
