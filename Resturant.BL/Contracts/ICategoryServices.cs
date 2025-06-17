using Resturant.BL.Features.Categories.Requests;
using Resturant.BL.Features.Categories.Responses;
using Resturant.BL.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.BL.Contracts
{
    public interface ICategoryServices
    {
        Task<Result<AddCategoryResponse>> AddCategoryAsync(AddCategoryRequest request);
        Task<Result<DeleteCategoryResponse>> DeleteCategoryAsync(DeleteCategoryRequest request);
        Task<Result<List<GetAllCategoriesResponse>>> GetAllCategoriesAsync();
        Task<Result<List<GetAllCategoriesResponse>>> GetAllCategoriesWithActiveMenuItemsAsync();
        Task<Result<GetCategoryByIdResponse>> GetCategoryByIdAsync(GetCategoryByIdRequest request);
        Task<Result<GetCategoryByNameResponse>> GetCategoryByNameAsync(GetCategoryByNameRequest request);
        Task<Result<UpdateCategoryResponse>> UpdateCategoryAsync(UpdateCategoryRequest request);
    }
}
