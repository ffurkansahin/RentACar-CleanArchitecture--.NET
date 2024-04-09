using Application.Features.Models.Queries.GetList;
using Application.Features.Models.Queries.GetListByDynamic;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Profiles;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        CreateMap<Model,GetListModelListItemDto>()
            //if automapper does not automatically map fields use this methods
            .ForMember(destinationMember:c=>c.BrandName,memberOptions:opt=>opt.MapFrom(c=>c.Brand.Name))
            .ForMember(destinationMember:c=>c.FuelTypeName,memberOptions:opt=>opt.MapFrom(c=>c.FuelType.Name))
            .ForMember(destinationMember:c=>c.TransmissionName,memberOptions:opt=>opt.MapFrom(c=>c.Transmission.Name))
            .ReverseMap();
        CreateMap<Model, GetListByDynamicModelListItemDto>().ReverseMap();
        CreateMap<Paginate<Model>,GetListResponse<GetListModelListItemDto>>().ReverseMap();
        CreateMap<Paginate<Model>, GetListResponse<GetListByDynamicModelListItemDto>>().ReverseMap();
    }
} 