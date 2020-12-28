using MovieManagement.App.Concrete;
using MovieManagement.App.Managers;
using MovieManagement.Domain.Entity;
using MovieManagement.Domain.Helpers;
using System;
using System.Collections.Generic;

namespace MovieManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            bool turnOff = false;

            MenuActionService actionService = new MenuActionService();
            MovieService movieService = new MovieService();
            InformationProvider informationProvider = new InformationProvider();
            MovieManager movieManager = new MovieManager(actionService, movieService, informationProvider);
            ListService listService = new ListService();

            movieService.Items = listService.DeserializeFromFile();

            Console.WriteLine("Welcome to your movies app!");

            while (turnOff == false)
            {
                Console.WriteLine("Please tell me, what do you want to do?");
                var mainMenu = actionService.GetMenuActionsByMenuName("Main");
                for (int i = 0; i < mainMenu.Count; i++)
                {
                    Console.WriteLine($"{mainMenu[i].Id}. {mainMenu[i].Name}");
                }

                var choice = Console.ReadKey();
                Console.Clear();

                switch (choice.KeyChar)
                {
                    case '1':
                        var id = movieManager.AddNewMovie(movieService);
                        break;
                    case '2':
                        movieManager.ArchiveMovieById(movieService);
                        break;
                    case '3':
                        movieManager.DisplayMovieDetails(movieService);
                        break;
                    case '4':
                        movieManager.DisplayMovieList(movieService, actionService);
                        break;
                    case '5':
                        Console.WriteLine("See you next time!");
                        turnOff = true;
                        break;
                    default:
                        Console.WriteLine("Wrong option, try again.");
                        break;
                }
            }
        }
        
    }
}