using Garcon.Business.Repository.Interface;
using Garcon.Business.Provider.Interface;
using System.Collections.Generic;
using Garcon.Business.Helper;
using Garcon.Business.Model;
using System.Linq;
using System;


namespace Garcon.Business.Repository
{
    public class MenuIngredientRepository : Repository<MenuIngredient>, IMenuIngredientRepository
    {
        private IIngredientItemRepository _ingredientItemRepository;

        public MenuIngredientRepository(IProvider<MenuIngredient> provider, IIngredientItemRepository ingredientItemRepository) : base(provider)
        {
            if (ingredientItemRepository is null)
            {
                throw new ArgumentNullException("Entity cannot be null");
            }

            this._ingredientItemRepository = ingredientItemRepository;
        }

        public override ICollection<MenuIngredient> GetEntityList()
            => Mapper.IngredientItemToMenuIngredients(base.GetEntityList(), _ingredientItemRepository.GetEntityList());

        public override void UpdateEntity(MenuIngredient Entity)
        {
            if (Entity is null)
            {
                throw new ArgumentNullException("Entity cannot be null");
            }

            entity.ToList().ForEach(e => { if (e.MenuId == Entity.MenuId && e.IngredientItemId == Entity.IngredientItemId) e = Entity; });
        }
    }
}