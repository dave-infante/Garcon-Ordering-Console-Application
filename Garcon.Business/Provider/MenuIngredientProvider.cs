using System.Collections.Generic;
using Garcon.Business.Model;
using Newtonsoft.Json.Linq;
using Garcon.Data.Helper;
using System;
using Garcon.Data;
using Garcon.Business.Provider.Interface;

namespace Garcon.Business.Provider
{
    public class MenuIngredientProvider : IProvider<MenuIngredient>
    {
        public List<MenuIngredient> GetEntityFromSource()
        {
            string json = FileManager.ReadEntityFromDataSource<MenuIngredient>();
            dynamic dict = JToken.Parse(json);
            List<MenuIngredient> list = new List<MenuIngredient>();

            foreach (var data in dict)
            {
                list.Add(new MenuIngredient(
                        Parse.ToInt(data.MenuId),
                        Parse.ToInt(data.IngredientItemId),
                        Parse.ToInt(data.RequiredQuantity)
                ));
            }

            return list;
        }

        public void SaveChanges(List<MenuIngredient> entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException("Entity cannot be null");
            }

            FileManager.WriteEntityToDataSource(entity);
        }
    }
}
