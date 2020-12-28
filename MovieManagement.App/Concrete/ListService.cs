using MovieManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using MovieManagement.Domain.Helpers;
using MovieManagement.App.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Runtime.Serialization.Json;

namespace MovieManagement.App.Concrete
{
    public class ListService
    {
        string path = Path.Combine(Environment.CurrentDirectory, "movies.txt");
        string output;

        JsonSerializer serializer = new JsonSerializer();
        MovieService _movieService;

        public ListService()
        {
            _movieService = new MovieService();
        }

        public void SerializeToFile(List<Movie> list)
        {
            output = JsonConvert.SerializeObject(list);
            using StreamWriter sw = new StreamWriter(path);
            using JsonWriter writer = new JsonTextWriter(sw);

            serializer.Serialize(writer, list);

            sw.Close();
            writer.Close();
        }

        public List<Movie> DeserializeFromFile()
        {
            List<Movie> list = new List<Movie>();

            if (File.Exists(path))
            {
                output = File.ReadAllText(path);
                list = JsonConvert.DeserializeObject<List<Movie>>(output);
            }

            return list;
        }

    }
}
