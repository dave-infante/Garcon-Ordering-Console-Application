using Garcon.Business.Model;

namespace Garcon.Business.Repository.Interface
{
    public interface IIngredientItemRepository : IRepository<IngredientItem>
    {
        bool IsIngredientAvailable(int ingredientId, int requiredQuantity);
    }
}
