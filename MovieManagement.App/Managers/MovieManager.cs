using MovieManagement.App.Concrete;
using MovieManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MovieManagement.App.Helpers;
using MovieManagement.App.Abstract;
using MovieManagement.Domain.Helpers;

namespace MovieManagement.App.Managers
{
    public class MovieManager
    {
        private readonly MenuActionService _actionService;
        private InformationProvider _informationProvider;
        private ListService _listService;
        public MovieManager(MenuActionService actionService, IService<Movie> movieService, InformationProvider informationProvider)
        {
            _informationProvider = new InformationProvider();
            _actionService = actionService;
            _listService = new ListService();
        }

        public int AddNewMovie(MovieService movieService)
        {
            _informationProvider.ShowSingleMessage("Enter name of movie: ");
            var movieName = _informationProvider.GetInputString();
            _informationProvider.ShowSingleMessage("Select category of movie: ");

            var movieTypesMenu = _actionService.GetMenuActionsByMenuName("MovieType");
            for (int i = 0; i < movieTypesMenu.Count; i++)
            {
                _informationProvider.ShowSingleMessage($"{movieTypesMenu[i].Id}. {movieTypesMenu[i].Name}");
            }

            var categoryId = _informationProvider.GetNumericInputKey();

            if (categoryId < 1 || categoryId > 7)
            {
                categoryId = 8;
            }
            var lastId = movieService.GetLastId();
            lastId++;

            _informationProvider.ShowSingleMessage("\nEnter the release year:");
            var releaseYear = _informationProvider.GetNumericValue();

            _informationProvider.ShowSingleMessage("Enter name of director: ");
            string directorsName = _informationProvider.GetInputString();
            MovieType movieType = new MovieType();
            movieType = (MovieType)categoryId;

            var movie = new Movie(lastId, movieName, movieType, releaseYear, directorsName);
            movieService.AddMovie(movie);

            _listService.SerializeToFile(movieService.Items);

            Console.Clear();
            return movie.Id;
        }

        public void RemoveMovieById(int id, MovieService movieService)
        {
            var movie = movieService.GetMovieById(id);
            movieService.RemoveMovie(movie);
        }

        public void ArchiveMovieById(MovieService movieService)
        {
            bool isMovieToArchive = false;
            _informationProvider.ShowSingleMessage("Which movie did you watch?");

            var watchedMovies = movieService.GetAllMovies();

            for (int i = 0; i < watchedMovies.Count; i++)
            {
                if (watchedMovies[i].IsWatched == false)
                {
                    _informationProvider.ShowSingleMessage($"{watchedMovies[i].Id}. {watchedMovies[i].Name}, {watchedMovies[i].Category}");
                }
            }

            var movieToArchive = _informationProvider.GetNumericInputKey();

            foreach (var number in movieService.Items)
            {
                if (movieToArchive == number.Id)
                {
                    isMovieToArchive = true;
                }
            }

            if (isMovieToArchive == false)
            {
                Console.Clear();
                return;
            }

            _informationProvider.ShowSingleMessage("\nRate movie from 1 to 10: ");

            var rate = _informationProvider.GetNumericValue();

            if (rate < 1)
            {
                rate = 0;
            }
            else if(rate > 10)
            {
                rate = 10;
            }
            var movie = movieService.GetMovieById(movieToArchive);

            movie.Rate = rate;
            movie.IsWatched = true;
            movieService.ArchiveMovie(movie);
            _listService.SerializeToFile(movieService.Items);

            Console.Clear();
        }

        public void DisplayMovieList(MovieService movieService, MenuActionService actionService)
        {
            Console.Clear();
            _informationProvider.ShowSingleMessage("Which list you want to see?");

            var displayMovieMenu = actionService.GetMenuActionsByMenuName("DisplayMovies");
            for (int i = 0; i < displayMovieMenu.Count; i++)
            {
                _informationProvider.ShowSingleMessage($"{displayMovieMenu[i].Id}. {displayMovieMenu[i].Name}");
            }
            var option = _informationProvider.GetNumericInputKey();
            var watchedMovies = movieService.GetAllMovies();
            Console.Clear();
            switch (option)
            {
                case 1:
                    for (int i = 0; i < watchedMovies.Count; i++)
                    {
                        if (watchedMovies[i].IsWatched == false)
                        {
                            _informationProvider.ShowSingleMessage($"{watchedMovies[i].Id}. '{watchedMovies[i].Name}'");
                        }
                        else
                        {
                            _informationProvider.ShowSingleMessage($"{watchedMovies[i].Id}. '{watchedMovies[i].Name}', Rate: {watchedMovies[i].Rate}");
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < watchedMovies.Count; i++)
                    {
                        if (watchedMovies[i].IsWatched == false)
                        {
                            _informationProvider.ShowSingleMessage($"{watchedMovies[i].Id}. '{watchedMovies[i].Name}'");
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < watchedMovies.Count; i++)
                    {
                        if (watchedMovies[i].IsWatched == true)
                        {
                            _informationProvider.ShowSingleMessage($"{watchedMovies[i].Id}. '{watchedMovies[i].Name}', Rate: {watchedMovies[i].Rate}");
                        }
                    }
                    break;
                default:
                    _informationProvider.ShowSingleMessage("Wrong option, try again.");
                    break;
            }
            Console.ReadKey();
            Console.Clear();
        }
        public void DisplayMovieDetails(MovieService movieService)
        {
            var watchedMovies = movieService.GetAllMovies();
            for (int i = 0; i < watchedMovies.Count; i++)
            {
                _informationProvider.ShowSingleMessage($"{watchedMovies[i].Id}. '{watchedMovies[i].Name}'");
            }
            _informationProvider.ShowSingleMessage("\nWhich movie's details you want to see?");
            var selectedMovie = _informationProvider.GetNumericInputKey();
            var movie = movieService.GetMovieById(selectedMovie);
            if (movie == null)
            {
                Console.Clear();
                return;
            }
            Console.Clear();
            _informationProvider.ShowSingleMessage($"Number of movie in data base: {movie.Id}\n" +
                $"Movie title: {movie.Name}\n" +
                $"Movie type: {movie.Category}\n" +
                $"Year of release: {movie.ReleaseYear}\n" +
                $"Director's name: {movie.DirectorsName}");
            if (movie.IsWatched == true)
            {
                _informationProvider.ShowSingleMessage($"Your rate: {movie.Rate}");
            }
            Console.ReadKey();
            Console.Clear();
        }
    }
}
