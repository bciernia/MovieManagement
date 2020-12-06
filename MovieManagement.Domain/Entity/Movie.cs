using MovieManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Domain.Entity
{
    public class Movie : BaseEntity
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public bool IsWatched { get; set; }
        public int Rate { get; set; }

        public Movie(int id, string name, int categoryId)
        {
            Id = id;
            Name = name; 
            CategoryId = categoryId;
        }
    }
}
