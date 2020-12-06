﻿using MovieManagement.App.Concrete;
using MovieManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MovieManagement.App.Managers
{
    public class MovieManager
    {
        private readonly MenuActionService _actionService;
        private MovieService _movieService;

        public MovieManager(MenuActionService actionService)
        {
            _movieService = new MovieService();
            _actionService = actionService;
        }

        public int AddNewMovie()
        {
            Console.Write("Enter name of movie: ");
            var movieName = Console.ReadLine();
            Console.WriteLine("Select category of movie:");
            var movieTypesMenu = _actionService.GetMenuActionsByMenuName("MovieType");
            for (int i = 0; i < movieTypesMenu.Count; i++)
            {
                Console.WriteLine($"{movieTypesMenu[i].Id}. {movieTypesMenu[i].Name}");
            }
            var option = Console.ReadKey();
            int categoryId = 0;
            Int32.TryParse(option.KeyChar.ToString(), out categoryId);
            if (categoryId > 1 || categoryId < 7)
            {
                categoryId = 8;
            }
            var lastId = _movieService.GetLastId();
            lastId++;

            Movie movie = new Movie(lastId, movieName, categoryId);
            _movieService.AddMovie(movie);

            Console.Clear();
            return movie.Id;
        }
        //ocenianie filmu
        public void ArchiveMovie()
        {
            Console.WriteLine("Which movie did you watch?");

            var watchedMovies = _movieService.GetAllMovies();

            for (int i = 0; i < watchedMovies.Count; i++)
            {
                if (watchedMovies[i].IsWatched == false)
                {
                    Console.WriteLine($"{watchedMovies[i].Id}. {watchedMovies[i].Name}, {watchedMovies[i].CategoryId}");
                }
            }
            var movieToArchive = Console.ReadKey();
            int id;
            Int32.TryParse(movieToArchive.KeyChar.ToString(), out id);
            Console.Write("\nRate movie from 1 to 10: ");
            var option = Console.ReadKey();
            var rate = 0;
            Int32.TryParse(option.KeyChar.ToString(), out rate);
            if (rate < 1 || rate > 10)
            {
                rate = 0;
            }
            var movie = _movieService.GetMovieById(id);
            movie.Rate = rate;
            movie.IsWatched = true;
            _movieService.ArchiveMovie(movie);
            Console.Clear();
        }

        public void DisplayMovieList(MenuActionService actionService)
        {
            Console.Clear();
            Console.WriteLine("Which list you want to see?");

            var displayMovieMenu = actionService.GetMenuActionsByMenuName("DisplayMovies");
            for (int i = 0; i < displayMovieMenu.Count; i++)
            {
                Console.WriteLine($"{displayMovieMenu[i].Id}. {displayMovieMenu[i].Name}");
            }
            var option = Console.ReadKey();
            var watchedMovies = _movieService.GetAllMovies();
            Console.Clear();
            switch (option.KeyChar)
            {
                case '1':
                    for (int i = 0; i < watchedMovies.Count; i++)
                    {
                        if (watchedMovies[i].IsWatched == false)
                        {
                            Console.WriteLine($"{watchedMovies[i].Id}. '{watchedMovies[i].Name}', {watchedMovies[i].CategoryId}");
                        }
                        else
                        {
                            Console.WriteLine($"{watchedMovies[i].Id}. '{watchedMovies[i].Name}', {watchedMovies[i].CategoryId}, Rate: {watchedMovies[i].Rate}");
                        }
                    }
                    break;
                case '2':
                    for (int i = 0; i < watchedMovies.Count; i++)
                    {
                        if (watchedMovies[i].IsWatched == false)
                        {
                            Console.WriteLine($"{watchedMovies[i].Id}. '{watchedMovies[i].Name}', {watchedMovies[i].CategoryId}");
                        }
                    }
                    break;
                case '3':
                    for (int i = 0; i < watchedMovies.Count; i++)
                    {
                        if (watchedMovies[i].IsWatched == true)
                        {
                            Console.WriteLine($"{watchedMovies[i].Id}. '{watchedMovies[i].Name}', {watchedMovies[i].CategoryId}, Rate: {watchedMovies[i].Rate}");
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Wrong option, try again.");
                    break;
            }
            Console.ReadKey();
            Console.Clear();
        }
    }
}
