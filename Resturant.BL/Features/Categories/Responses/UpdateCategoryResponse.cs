using Resturant.Core.Entities.Models;


namespace Resturant.BL.Features.Categories.Responses
{
   public record UpdateCategoryResponse(int Id ,string Name, List<int>? menuItemsIds);
   
}
