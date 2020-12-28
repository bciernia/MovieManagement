using MovieManagement.App.Helpers;
using MovieManagement.Domain.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MovieManagement.Domain.Entity
{
    public class Movie : BaseEntity
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Category")]
        public MovieType Category { get; set; }
        [JsonProperty("Was_watched")]
        public bool IsWatched { get; set; }
        [JsonProperty("Rate")]
        public int Rate { get; set; }
        [JsonProperty("Release_year")]
        public int ReleaseYear { get; set; }
        [JsonProperty("Directors_name")]
        public string DirectorsName { get; set; }

        public Movie()
        {

        }

        public Movie(int id = 0, string name = "none", MovieType categoryId = MovieType.NonCategory, int releaseYear = 0000, string directorsName = "none")
        {
            Id = id;
            Name = name; 
            Category = categoryId;
            ReleaseYear = releaseYear;
            DirectorsName = directorsName;
        }
    }
}
