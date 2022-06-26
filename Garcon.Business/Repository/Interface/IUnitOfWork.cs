namespace Garcon.Business.Repository.Interface
{
    public interface IUnitOfWork
    {
        IMenuIngredientRepository MenuIngredients { get; }
        IIngredientItemRepository IngredientItems { get; }
        IMenuItemRepository MenuItems { get; }

        void SaveEntities();
    }
}
