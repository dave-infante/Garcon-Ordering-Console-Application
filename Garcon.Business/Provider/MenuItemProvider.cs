using System.Collections.Generic;
using Garcon.Business.Model;
using Newtonsoft.Json.Linq;
using Garcon.Business.Enum;
using Garcon.Data.Helper;
using System;
using Garcon.Data;
using Garcon.Business.Provider.Interface;

namespace Garcon.Business.Provider
{
    public class MenuItemProvider : IProvider<MenuItem>
    {

        public List<MenuItem> GetEntityFromSource()
        {
            string json = FileManager.ReadEntityFromDataSource<MenuItem>();
            dynamic dict = JToken.Parse(json);
            List<MenuItem> list = new List<MenuItem>();

            foreach (var data in dict)
            {
                list.Add(new MenuItem(Parse.ToInt(data.Id))
                {
                    Name = Parse.ToString(data.Name),
                    Classification = Parse.ToEnum<MenuItemClassification>(data.Classification),
                    PrepTimeInMins = Parse.ToDecimal(data.PrepTimeInMins),
                    CookTimeInMins = Parse.ToDecimal(data.CookTimeInMins),
                    Price = Parse.ToDecimal(data.Price),
                    IsChefRecommended = Parse.ToBool(data.IsChefRecommended)
                });
            }
            return list;
        }

        public void SaveChanges(List<MenuItem> entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException("Entity cannot be null");
            }

            FileManager.WriteEntityToDataSource(entity);
        }
    }
}
