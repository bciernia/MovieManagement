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
        public List<T> Items { get; set; }
        public BaseService()
        {
            Items = new List<T>();
        }

        public int AddMovie(T movie)
        {
            Items.Add(movie);
            return movie.Id;
        }

        public int GetLastId()
        {
            int lastId;
            if(Items.Any())
            {
                lastId = Items.OrderBy(x => x.Id).LastOrDefault().Id;
            }    
            else
            {
                lastId = 0;
            }

            return lastId;
        }

        public int ArchiveMovie(T movie)
        {
            var entity = Items.FirstOrDefault(x => x.Id == movie.Id);
            if (entity != null)
            {
                entity = movie;
            }
            return entity.Id;
        }

        public List<T> GetAllMovies()
        {
            return Items;
        }

        public int UpdateMovie(T movie)
        {
            var entity = Items.FirstOrDefault(x => x.Id == movie.Id);
            if(entity != null)
            {
                entity = movie;
            }
            return entity.Id;
        }

        public T GetMovieById(int id)
        {
            var entity = Items.FirstOrDefault(x => x.Id == id);
            return entity;
        }

        public void RemoveMovie(T movie)
        {
            Items.Remove(movie);
        }
    }
}
