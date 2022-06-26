using System.Collections.Generic;
using Garcon.Business.Model;
using Newtonsoft.Json.Linq;
using Garcon.Data.Helper;
using System;
using Garcon.Data;
using Garcon.Business.Provider.Interface;

namespace Garcon.Business.Provider
{
    public class IngredientItemProvider : IProvider<IngredientItem>
    {
        public List<IngredientItem> GetEntityFromSource()
        {
            string json = FileManager.ReadEntityFromDataSource<IngredientItem>();
            dynamic dict = JToken.Parse(json);
            List<IngredientItem> list = new List<IngredientItem>();

            foreach (var data in dict)
            {
                list.Add(new IngredientItem(Parse.ToInt(data.Id))
                {
                    Name = Parse.ToString(data.Name),
                    SupplyCount = Parse.ToInt(data.SupplyCount)
                });
            }

            return list;
        }

        public void SaveChanges(List<IngredientItem> entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException("Entity cannot be null");
            }

            FileManager.WriteEntityToDataSource(entity);
        }
    }
}
