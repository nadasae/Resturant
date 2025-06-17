using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Resturant.BL.Contracts;
using Resturant.BL.Features.Categories.Requests;
using Resturant.BL.Features.Categories.Responses;
using Resturant.BL.Shared;
using Resturant.Core.Entities.Models;
using Resturant.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.BL.AppServices
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository categories;
        private readonly IMenuItemRepository MenuItems;
        private readonly IMapper mapper;

        public CategoryServices(ICategoryRepository Categories,IMenuItemRepository MenuItems,IMapper mapper)
        {
            categories = Categories;
            this.MenuItems = MenuItems;
            this.mapper = mapper;
        }
        public async Task<Result<AddCategoryResponse>> AddCategoryAsync(AddCategoryRequest request)
        {
            var category = mapper.Map<Category>(request);   

           await categories.AddAsync(category);
            await categories.SaveChangesAsync();

            var catMap = mapper.Map<AddCategoryResponse>(category);
            return Result<AddCategoryResponse>.Success(catMap);
        }

        public async Task<Result<DeleteCategoryResponse>> DeleteCategoryAsync(DeleteCategoryRequest request)
        {
            var cat = await categories.GetByIdAsync(request.Id);
            if(cat == null)
            {
                return Result<DeleteCategoryResponse>.Fail("Category not found");
            }
             categories.Delete(cat);
            await categories.SaveChangesAsync();
            var catMap = mapper.Map<DeleteCategoryResponse>(cat);
            return Result<DeleteCategoryResponse>.Success(catMap);
        }

        public async Task<Result<List<GetAllCategoriesResponse>>> GetAllCategoriesAsync()
        {
            var cats =  categories.GetAllAsync();
            var catMap = mapper.Map<List<GetAllCategoriesResponse>>(cats);
            return Result<List<GetAllCategoriesResponse>>.Success(catMap);
        }

        public async Task<Result<List<GetAllCategoriesResponse>>> GetAllCategoriesWithActiveMenuItemsAsync()
        {
            var cats = await categories.GetWithActiveMenuItemsAsync();
            var catMap = mapper.Map<List<GetAllCategoriesResponse>>(cats);
            return Result<List<GetAllCategoriesResponse>>.Success(catMap);
        }

        public async Task<Result<GetCategoryByIdResponse>> GetCategoryByIdAsync(GetCategoryByIdRequest request)
        {
            var cat = await categories.GetAllAsync()
             .Include(c => c.MenuItems)
             .FirstOrDefaultAsync(c => c.Id == request.Id);
            if (cat == null)
                return Result<GetCategoryByIdResponse>.Fail("Category not found");
            var catMap = mapper.Map<GetCategoryByIdResponse>(cat);
            return Result<GetCategoryByIdResponse>.Success(catMap);
        }

        public async Task<Result<GetCategoryByNameResponse>> GetCategoryByNameAsync(GetCategoryByNameRequest request)
        {
            var category = await categories.GetAllAsync()
               .Include(c => c.MenuItems)
               .FirstOrDefaultAsync(c => c.Name == request.Name);
            if (category == null)
            {
                return Result<GetCategoryByNameResponse>.Fail("Category not found");
            }
            var catMap = mapper.Map<GetCategoryByNameResponse>(category);
            return Result<GetCategoryByNameResponse>.Success(catMap);
        }

        public async Task<Result<UpdateCategoryResponse>> UpdateCategoryAsync(UpdateCategoryRequest request)
        {
            var cat = await categories.GetByIdAsync(request.Id);
            if (cat == null)
            {
                return Result<UpdateCategoryResponse>.Fail("Category not found");
            }
            var UpCat = mapper.Map<Category>(request);

            if (request.menuItemIds?.Any() == true)
            {
                var menuItems = await Task.WhenAll(request.menuItemIds
                    .Select(id => MenuItems.GetByIdAsync(id)));

                UpCat.MenuItems = menuItems.Where(o => o != null).ToList()!;
            }
            categories.Update(UpCat);
            await categories.SaveChangesAsync();
            var catMap = mapper.Map<UpdateCategoryResponse>(UpCat);
            return Result<UpdateCategoryResponse>.Success(catMap);

        }
    }
}
