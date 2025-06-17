using Resturant.BL.Features.MenuItems.Requests;
using Resturant.BL.Features.MenuItems.Responses;
using Resturant.BL.Shared;
using Resturant.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.BL.Contracts
{
    public interface IMenuItemServices
    {
        Task<Result<AddMenuItemResponse>> AddAsync(AddMenuItemRequest request);
        Task<Result<DeleteMenuItemResponse>> DeleteAsync(DeleteMenuItemRequest request);
        Task<Result<UpdateMenuItemResponse>> UpdateAsync(UpdateMenuItemRequest request);
        Task<Result<List<GetMenuItemResponse>>> GetAllAsync();
        Task<Result<GetMenuItemResponse>> GetByIdAsync(int id);
        Task<Result<GetMenuItemResponse>> GetByNameAsync(GetMenuItemByNameRequest request);
        Task<Result<List<GetMenuItemResponse>>> GetAvailableItemsAsync();
        Task<Result<GetDailyOrderCountPerMenuItemResponse>> GetDailyOrderCountAsync(GetDailyOrderCountPerMenuItemRequest request);
        Task<Result<List<GetMenuItemResponse>>> GetAllMenuItemsWithCategoryAsync(GetAllMenuItemsWithCategoryRequest request);
        public Task<Result<List<GetMenuItemResponse>>> UpdateAvailabilityBasedOnOrderCountAsync();
        public Task ResetAvailabilityDailyAsync();



    }
}
