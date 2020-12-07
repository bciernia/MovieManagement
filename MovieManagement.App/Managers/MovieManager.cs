using MovieManagement.App.Concrete;
using MovieManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MovieManagement.App.Helpers;

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
            if (categoryId < 1 || categoryId > 7)
            {
                categoryId = 8;
            }
            var lastId = _movieService.GetLastId();
            lastId++;

            Console.WriteLine("\nEnter the release year: ");
            string year = Console.ReadLine();
            int releaseYear;
            Int32.TryParse(year, out releaseYear);

            Console.WriteLine("Enter director's name: ");
            string directorsName = Console.ReadLine();
            MovieType movieType = new MovieType();
            movieType = (MovieType)categoryId;


            Movie movie = new Movie(lastId, movieName, movieType, releaseYear, directorsName);
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
            var option = Console.ReadLine();
            var rate = 0;
            Int32.TryParse(option, out rate);
            if (rate < 1)
            {
                rate = 0;
            }
            else if(rate > 10)
            {
                rate = 10;
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
                            Console.WriteLine($"{watchedMovies[i].Id}. '{watchedMovies[i].Name}'");
                        }
                        else
                        {
                            Console.WriteLine($"{watchedMovies[i].Id}. '{watchedMovies[i].Name}', Rate: {watchedMovies[i].Rate}");
                        }
                    }
                    break;
                case '2':
                    for (int i = 0; i < watchedMovies.Count; i++)
                    {
                        if (watchedMovies[i].IsWatched == false)
                        {
                            Console.WriteLine($"{watchedMovies[i].Id}. '{watchedMovies[i].Name}'");
                        }
                    }
                    break;
                case '3':
                    for (int i = 0; i < watchedMovies.Count; i++)
                    {
                        if (watchedMovies[i].IsWatched == true)
                        {
                            Console.WriteLine($"{watchedMovies[i].Id}. '{watchedMovies[i].Name}', Rate: {watchedMovies[i].Rate}");
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
        public void DisplayMovieDetails()
        {
            var watchedMovies = _movieService.GetAllMovies();
            for (int i = 0; i < watchedMovies.Count; i++)
            {
                Console.WriteLine($"{watchedMovies[i].Id}. '{watchedMovies[i].Name}'");
            }
            Console.WriteLine("\nWhich movie's details you want to see?");
            var option = Console.ReadKey();
            int selectedMovie;
            Int32.TryParse(option.KeyChar.ToString(), out selectedMovie);
            var movie = _movieService.GetMovieById(selectedMovie);
            if (movie == null)
            {
                Console.Clear();
                return;
            }
            Console.Clear();
            Console.WriteLine($"Number of movie in data base: {movie.Id}\n" +
                $"Movie title: {movie.Name}\n" +
                $"Movie type: {movie.CategoryId}\n" +
                $"Year of release: {movie.ReleaseYear}\n" +
                $"Director's name: {movie.DirectorsName}");
            if (movie.IsWatched == true)
            {
                Console.WriteLine($"Your rate: {movie.Rate}");
            }
            Console.ReadKey();
            Console.Clear();
        }
    }
}
