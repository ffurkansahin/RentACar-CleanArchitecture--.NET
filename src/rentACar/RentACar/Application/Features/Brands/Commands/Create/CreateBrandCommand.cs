﻿using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transactions;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.Create;

public class CreateBrandCommand : IRequest<CreatedBrandResponse> , ITransactionalRequest
{
    public string Name { get; set; }

	public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandResponse>
	{
		private readonly IBrandRepository _brandRepository;
		private readonly IMapper _mapper;
		private readonly BrandBusinessRules _brandBusinessRules;

		public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
		{
			_brandRepository = brandRepository;
			_mapper = mapper;
			_brandBusinessRules = brandBusinessRules;
		}

		public async Task<CreatedBrandResponse>? Handle(CreateBrandCommand request, CancellationToken cancellationToken)
		{
			await _brandBusinessRules.BrandNameCannotBeDuplicatedWhenInserted(request.Name);

			Brand brand = _mapper.Map<Brand>(request);
			brand.Id=Guid.NewGuid();
			brand.CreatedDate = DateTime.UtcNow;

			await _brandRepository.AddAsync(brand);

			CreatedBrandResponse createdBrandResponse = _mapper.Map<CreatedBrandResponse>(brand);
			return createdBrandResponse;
		}
	}

}
