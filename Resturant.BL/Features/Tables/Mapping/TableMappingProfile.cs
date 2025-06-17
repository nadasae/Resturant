using AutoMapper;
using Resturant.BL.Features.Tables.Requests;
using Resturant.BL.Features.Tables.Responses;
using Resturant.Core.Entities.Models;

namespace Resturant.BL.Features.Tables.Mapping
{
    public class TableMappingProfile : Profile
    {
        public TableMappingProfile()
        {
            // AddTable Mappings
            CreateMap<AddTableRequest, Table>()
     .ForMember(dest => dest.TableNumber, opt => opt.MapFrom(src => src.Number))
     .ForMember(dest => dest.Orders, opt => opt.Ignore());
            CreateMap<Table, AddTableResponse>()
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id));

            // DeleteTable Mappings
            CreateMap<DeleteTableRequest, Table>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Table, DeleteTableResponse>()
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id));

            // UpdateTable Mappings
            CreateMap<UpdateTableRequest, Table>()
                .ForMember(dest => dest.Orders, opt => opt.Ignore()); // Handled separately

            CreateMap<Table, UpdateTableResponse>()
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id));

            // GetTableById Mappings
            CreateMap<Table, GetTableByIdResponse>()
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("Number", opt => opt.MapFrom(src => src.TableNumber))
                .ForCtorParam("Capacity", opt => opt.MapFrom(src => src.Capacity))
                .ForCtorParam("OrdersIds", opt => opt.MapFrom(src => src.Orders.Select(o => o.Id).ToList()));

            // GetTableByNumber Mappings
            CreateMap<Table, GetTableByNumberResponse>()
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.TableNumber))
                .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
                .ForMember(dest => dest.OrdersIds, opt => opt.MapFrom(src => src.Orders.Select(o => o.Id).ToList()));

            // GetAllTablesAvailable Mappings
            CreateMap<Table, GetAllTablesAvailableResponse>()
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("Number", opt => opt.MapFrom(src => src.TableNumber))
                .ForCtorParam("Capacity", opt => opt.MapFrom(src => src.Capacity));
        }
    }
}