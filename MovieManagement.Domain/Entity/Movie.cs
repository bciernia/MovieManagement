using MovieManagement.App.Helpers;
using MovieManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Domain.Entity
{
    public class Movie : BaseEntity
    {
        public string Name { get; set; }
        public MovieType CategoryId { get; set; }
        public bool IsWatched { get; set; }
        public int Rate { get; set; }
        public int ReleaseYear { get; set; }
        public string DirectorsName { get; set; }

        public Movie(int id, string name, MovieType categoryId, int releaseYear, string directorsName)
        {
            Id = id;
            Name = name; 
            CategoryId = categoryId;
            ReleaseYear = releaseYear;
            DirectorsName = directorsName;
        }
    }
}
