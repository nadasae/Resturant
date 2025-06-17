using AutoMapper;
using Resturant.BL.Contracts;
using Resturant.BL.Features.MenuItems.Requests;
using Resturant.BL.Features.MenuItems.Responses;
using Resturant.BL.Shared;
using Resturant.Core.Entities.Models;
using Resturant.Core.Interfaces.Repositories;


namespace Resturant.BL.AppServices
{
    public class MenuItemsServices : IMenuItemServices
    {
        private static DateTime lastResetDate = DateTime.MinValue;

        private readonly IMenuItemRepository menuItems;
        private readonly ICategoryRepository categories;
        private readonly IMapper mapper;

        public MenuItemsServices(IMenuItemRepository MenuItems, ICategoryRepository Categories, IMapper mapper)
        {
            menuItems = MenuItems;
            categories = Categories;
            this.mapper = mapper;
        }
        public async Task<Result<AddMenuItemResponse>> AddAsync(AddMenuItemRequest request)
        {
            var cat = await categories.GetByIdAsync(request.CategoryId);
            if (cat == null)
            {
                return Result<AddMenuItemResponse>.Fail("Category not found");
            }
            if (request.Price <= 0)
            {
                return Result<AddMenuItemResponse>.Fail("Price must be positive");
            }
            var menI = mapper.Map<MenuItem>(request);
            await menuItems.AddAsync(menI);
            await menuItems.SaveChangesAsync();
            var menMap = mapper.Map<AddMenuItemResponse>(menI);
            return Result<AddMenuItemResponse>.Success(menMap);

        }
        public async Task<Result<DeleteMenuItemResponse>> DeleteAsync(DeleteMenuItemRequest request)
        {
            var menuItem = await menuItems.GetByIdAsync(request.Id);
            if (menuItem == null)
            {
                return Result<DeleteMenuItemResponse>.Fail("Menu item not found");
            }
            menuItems.Delete(menuItem);
            await menuItems.SaveChangesAsync();
            var menMap = mapper.Map<DeleteMenuItemResponse>(menuItem);
            return Result<DeleteMenuItemResponse>.Success(menMap);
        }
        public async Task<Result<UpdateMenuItemResponse>> UpdateAsync(UpdateMenuItemRequest request)
        {
            var menuItem = await menuItems.GetByIdAsync(request.Id);
            if (menuItem == null)
            {
                return Result<UpdateMenuItemResponse>.Fail("Menu item not found");
            }
            var cat = await categories.GetByIdAsync(request.CategoryId);
            if (cat == null)
            {
                return Result<UpdateMenuItemResponse>.Fail("Category not found");
            }
            if (request.Price <= 0)
            {
                return Result<UpdateMenuItemResponse>.Fail("Price must be positive");
            }
            //menuItem.Name = request.Name;
            //menuItem.Price = request.Price;
            //menuItem.CategoryId = request.CategoryId;
            menuItem = mapper.Map(request, menuItem);
            menuItems.Update(menuItem);
            await menuItems.SaveChangesAsync();
            var menMap = mapper.Map<UpdateMenuItemResponse>(menuItem);
            return Result<UpdateMenuItemResponse>.Success(menMap);
        }
        public async Task<Result<List<GetMenuItemResponse>>> GetAllAsync()
        {
            var menuItemsList =  menuItems.GetAllAsync();
            if (menuItemsList == null || !menuItemsList.Any())
            {
                return Result<List<GetMenuItemResponse>>.Fail("No menu items found");
            }
            var menuItemsMap = mapper.Map<List<GetMenuItemResponse>>(menuItemsList);
            return Result<List<GetMenuItemResponse>>.Success(menuItemsMap);
        }
        public async Task<Result<GetMenuItemResponse>> GetByIdAsync(int id)
        {
            var menuItem = await menuItems.GetByIdAsync(id);
            if (menuItem == null)
            {
                return Result<GetMenuItemResponse>.Fail("Menu item not found");
            }
            var menuItemMap = mapper.Map<GetMenuItemResponse>(menuItem);
            return Result<GetMenuItemResponse>.Success(menuItemMap);
        }

        public async Task<Result<GetMenuItemResponse>> GetByNameAsync(GetMenuItemByNameRequest request)
        {
            var mens =  menuItems.GetAllAsync();
            var menuItem = mens.FirstOrDefault(m => m.Name == request.Name);
            if (menuItem == null)
            {
                return Result<GetMenuItemResponse>.Fail("Menu item not found");
            }
            var menMap = mapper.Map<GetMenuItemResponse>(menuItem);
            return Result<GetMenuItemResponse>.Success(menMap);
        }
        public async Task<Result<List<GetMenuItemResponse>>> GetAvailableItemsAsync()
        {
            var availableItems = await menuItems.GetAvailableItemsAsync();
            if (availableItems == null || !availableItems.Any())
            {
                return Result<List<GetMenuItemResponse>>.Fail("No available menu items found");
            }
            var availableItemsMap = mapper.Map<List<GetMenuItemResponse>>(availableItems);
            return Result<List<GetMenuItemResponse>>.Success(availableItemsMap);
        }
        public async Task<Result<GetDailyOrderCountPerMenuItemResponse>> GetDailyOrderCountAsync(GetDailyOrderCountPerMenuItemRequest request)
        {
            var count = await menuItems.GetDailyOrderCountAsync(request.MenuItemId);
            if (count < 0)
            {
                return Result<GetDailyOrderCountPerMenuItemResponse>.Fail("Error retrieving daily order count");
            }
            var response = new GetDailyOrderCountPerMenuItemResponse
            (
                 request.MenuItemId,
                 count
            );
            return Result<GetDailyOrderCountPerMenuItemResponse>.Success(response);
        }
        public async Task<Result<List<GetMenuItemResponse>>> GetAllMenuItemsWithCategoryAsync(GetAllMenuItemsWithCategoryRequest request)
        {
            var items = await menuItems.GetAllMenuItemsWithCategoryAsync(request.CategoryId);
            if (items == null || !items.Any())
            {
                return Result<List<GetMenuItemResponse>>.Fail("No menu items found for this category");
            }
            var itemsMap = mapper.Map<List<GetMenuItemResponse>>(items);
            return Result<List<GetMenuItemResponse>>.Success(itemsMap);
        }



        public async Task<Result<List<GetMenuItemResponse>>> UpdateAvailabilityBasedOnOrderCountAsync()
        {
            var items =  menuItems.GetAllAsync();
            foreach (var item in items)
            {
                var dailyCount = await menuItems.GetDailyOrderCountAsync(item.Id);
                if (dailyCount >= 50)
                {
                    item.IsAvailable = false;
                     menuItems.Update(item);
                }
            }
            await menuItems.SaveChangesAsync();
            var menMap = mapper.Map<List<GetMenuItemResponse>>(items);
            return Result<List<GetMenuItemResponse>>.Success(menMap);
        }


        public async Task ResetAvailabilityDailyAsync()
        {
            var now = DateTime.Now;

           /* Only reset after 12 AM, and only once per day
            lastResetDate.Date != now.Date  ==> it checks day difference
            now.TimeOfDay >= TimeSpan.FromHours(0) ==>
           {
           checks the time if it the midnight or after 
           TimeSpan.FromHours(0) ---> 00:00:00 the midnight 
           }
           */
            if (now.TimeOfDay >= TimeSpan.FromHours(0) && lastResetDate.Date != now.Date)
            {
                var items =  menuItems.GetAllAsync();

                foreach (var item in items)
                {
                    if (!item.IsAvailable)
                    {
                        item.IsAvailable = true;
                        menuItems.Update(item);
                    }
                }

                lastResetDate = now.Date;
            }
        }
    }
}
