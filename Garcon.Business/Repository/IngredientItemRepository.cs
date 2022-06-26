using Garcon.Business.Repository.Interface;
using Garcon.Business.Provider.Interface;
using Garcon.Business.Model;
using System.Linq;
using System;


namespace Garcon.Business.Repository
{
    public class IngredientItemRepository : Repository<IngredientItem>, IIngredientItemRepository
    {
        public IngredientItemRepository(IProvider<IngredientItem> provider) : base(provider) { }

        public bool IsIngredientAvailable(int ingredientId, int requiredQuantity)
            => base.GetEntityList().FirstOrDefault(i => i.Id == ingredientId)?.SupplyCount >= requiredQuantity;

        public override void UpdateEntity(IngredientItem Entity)
        {
            if (Entity is null)
            {
                throw new ArgumentNullException("Entity cannot be null");
            }

            entity.ToList().ForEach(e => { if (e.Id != Entity.Id) e = Entity; });
        }
    }
}