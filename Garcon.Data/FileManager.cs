using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System;


namespace Garcon.Data
{
    public static class FileManager
    {
        private static readonly string dataSourcePath = $"{Environment.CurrentDirectory}\\DataSource\\";


        /// <summary>
        /// Get entity from data souce based on the provided object type
        /// </summary>
        public static string ReadEntityFromDataSource<T>()
        {
            string filePath = GetFilePath(typeof(T).Name);
            var json = File.ReadAllText(filePath);
            return json;
        }


        /// <summary>
        /// Save the entity to data souce in json format
        /// </summary>
        public static void WriteEntityToDataSource<T>(ICollection<T> entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException("Entity value cannot be null.");
            }

            string filePath = GetFilePath(typeof(T).Name);
            using (var file = File.CreateText(filePath))
            {
                file.Write(JsonConvert.SerializeObject(entity, Formatting.Indented));
                file.Dispose();
            }
        }


        /// <summary>
        /// Get file path of the data source. This will create the said file if it does not exist.
        /// </summary>
        private static string GetFilePath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("FileName value cannot be empty");
            }

            string fullPath = $"{dataSourcePath}{fileName}.json";

            if (!File.Exists(fullPath))
            {
                File.WriteAllText(fullPath, "");
            }

            return fullPath;
        }
    }
}
