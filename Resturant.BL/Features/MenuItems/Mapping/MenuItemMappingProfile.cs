using AutoMapper;
using Resturant.BL.Features.MenuItems.Requests;
using Resturant.BL.Features.MenuItems.Responses;
using Resturant.Core.Entities.Models;

namespace Resturant.BL.Features.MenuItems.Mapping
{
    public class MenuItemMappingProfile : Profile
    {
        public MenuItemMappingProfile()
        {
            // AddMenuItemRequest → MenuItem (Entity)
            CreateMap<AddMenuItemRequest, MenuItem>();

            // UpdateMenuItemRequest → MenuItem (Entity)
            CreateMap<UpdateMenuItemRequest, MenuItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ensure Id is not overwritten

            // MenuItem (Entity) → GetMenuItemResponse (DTO)
            CreateMap<MenuItem, GetMenuItemResponse>()
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name))
                .ForCtorParam("Price", opt => opt.MapFrom(src => src.Price))
                .ForCtorParam("CategoryId", opt => opt.MapFrom(src => src.CategoryId));

            // MenuItem (Entity) → AddMenuItemResponse (DTO)
            CreateMap<MenuItem, AddMenuItemResponse>()
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id));

            // MenuItem (Entity) → DeleteMenuItemResponse (DTO)
            CreateMap<MenuItem, DeleteMenuItemResponse>()
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id));

            // MenuItem (Entity) → UpdateMenuItemResponse (DTO)
            CreateMap<MenuItem, UpdateMenuItemResponse>()
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id));
       

            //CreateMap<int, GetDailyOrderCountPerMenuItemResponse>()
            // .ConstructUsing((count, menuItemId) => new GetDailyOrderCountPerMenuItemResponse(count, menuItemId));
        }
    }
    }
