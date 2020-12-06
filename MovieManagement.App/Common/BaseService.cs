using MovieManagement.App.Abstract;
using MovieManagement.Domain.Common;
using MovieManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieManagement.App.Common
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        public List<T> Movies { get; set; }
        public BaseService()
        {
            Movies = new List<T>();
        }

        public int AddMovie(T movie)
        {
            Movies.Add(movie);
            return movie.Id;
        }

        public int GetLastId()
        {
            int lastId;
            if(Movies.Any())
            {
                lastId = Movies.OrderBy(x => x.Id).LastOrDefault().Id;
            }    
            else
            {
                lastId = 0;
            }

            return lastId;
        }

        public int ArchiveMovie(T movie)
        {
            var entity = Movies.FirstOrDefault(x => x.Id == movie.Id);
            if (entity != null)
            {
                entity = movie;
            }
            return entity.Id;
        }

        public List<T> GetAllMovies()
        {
            return Movies;
        }

        public int UpdateMovie(T movie)
        {
            var entity = Movies.FirstOrDefault(x => x.Id == movie.Id);
            if(entity != null)
            {
                entity = movie;
            }
            return entity.Id;
        }

        public T GetMovieById(int id)
        {
            var movie = Movies.FirstOrDefault(x => x.Id == id);
            return movie;
        }
    }
}
