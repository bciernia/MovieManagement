using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.App.Abstract
{
    public interface IService<T>
    {
        List<T> Movies { get; set; }

        List<T> GetAllMovies();
        T GetMovieById(int id);
        int AddMovie(T movie);
        int UpdateMovie(T movie);
        int ArchiveMovie(T movie);
    }
}   
