using Resturant.Core.Entities.Models;


namespace Resturant.BL.Features.Categories.Responses
{
    public record GetAllCategoriesWithMenuItemsResponse(int Id, string Name, List<int>? menuItemsIds);
  
}
