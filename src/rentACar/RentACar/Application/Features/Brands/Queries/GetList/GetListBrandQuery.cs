﻿using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetList;

public class GetListBrandQuery : IRequest<GetListResponse<GetListBrandListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

	public string CacheKey => $"GetListBrandQuery({PageRequest.PageIndex}, {PageRequest.PageSize})";

	public bool BypassCache { get; }

public TimeSpan? SlidingExpiration { get; }

	public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, GetListResponse<GetListBrandListItemDto>>
	{
		private readonly IBrandRepository _brandRepository;
		private readonly IMapper _mapper;

		public GetListBrandQueryHandler(IMapper mapper, IBrandRepository brandRepository)
		{
			_mapper = mapper;
			_brandRepository = brandRepository;
		}

		public async Task<GetListResponse<GetListBrandListItemDto>> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
		{
			Paginate<Brand> brands = await _brandRepository.GetListAsync(
					index: request.PageRequest.PageIndex,
					size: request.PageRequest.PageSize,
					cancellationToken : cancellationToken,
					withDeleted: true//includes soft deleted ones
				);
			return _mapper.Map<GetListResponse<GetListBrandListItemDto>>(brands);
		}
	}
}
