using MovieManagement.App.Common;
using MovieManagement.Domain;
using MovieManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.App.Concrete
{
    public class MovieService : BaseService<Movie>
    {
           public MovieService()
           {
              Items = new List<Movie>();
           }
    }
}
