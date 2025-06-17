using AutoMapper;
using Resturant.BL.Features.Categories.Requests;
using Resturant.BL.Features.Categories.Responses;
using Resturant.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.BL.Features.Categories.Mapping
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            // AddCategory Mappings
            CreateMap<AddCategoryRequest, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.MenuItems, opt => opt.Ignore());
            CreateMap<Category, AddCategoryResponse>()
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id));


            // DeleteCategory Mappings
            CreateMap<DeleteCategoryRequest, Category>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Category, DeleteCategoryResponse>()
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id));
            // UpdateCategory Mappings
            CreateMap<UpdateCategoryRequest, Category>()
                .ForMember(dest => dest.MenuItems, opt => opt.Ignore()); // Handled separately
            CreateMap<Category, UpdateCategoryResponse>()
    .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
    .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name))
    .ForCtorParam("menuItemsIds", opt => opt.MapFrom(src => src.MenuItems.Select(mi => mi.Id).ToList()));
            // GetCategoryById Mappings
            CreateMap<Category, GetCategoryByIdResponse>()
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name))
                .ForCtorParam("menuItemsIds", opt => opt.MapFrom(src => src.MenuItems.Select(mi => mi.Id).ToList()));
            // GetCategoryByName Mappings
            CreateMap<Category, GetCategoryByNameResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.menuItemsIds, opt => opt.MapFrom(src => src.MenuItems.Select(mi => mi.Id).ToList()));
            // GetAllCategories Mappings
            CreateMap<Category, GetAllCategoriesResponse>()
              .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
 .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name))
 .ForCtorParam("menuItemsIds", opt => opt.MapFrom(src => src.MenuItems.Select(mi => mi.Id).ToList()));
        }
    }
   
}
