

namespace Resturant.BL.Features.Categories.Responses
{
    public record GetAllCategoriesResponse(int Id ,string Name,List<int>? menuItemsIds);
}
  
