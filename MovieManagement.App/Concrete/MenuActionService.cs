using MovieManagement.App.Common;
using MovieManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.App.Concrete
{
    public class MenuActionService : BaseService<MenuAction>
    {
        public MenuActionService()
        {
            Initialize();
        }

        public List<MenuAction> GetMenuActionsByMenuName(string menuName)
        {
            List<MenuAction> result = new List<MenuAction>();

            foreach(var menuAction in Movies)
            {
                if(menuAction.MenuName == menuName)
                {
                    result.Add(menuAction);
                }
            }
            return result;
        }
        private void  Initialize()
        {
            AddMovie(new MenuAction(1, "Add movie", "Main"));
            AddMovie(new MenuAction(2, "Rate movie", "Main"));
            AddMovie(new MenuAction(3, "Your movies", "Main"));
            AddMovie(new MenuAction(4, "Exit", "Main"));

            AddMovie(new MenuAction(1, "Action", "MovieType"));
            AddMovie(new MenuAction(2, "Comedy", "MovieType"));
            AddMovie(new MenuAction(3, "Drama", "MovieType"));
            AddMovie(new MenuAction(4, "Fantasy", "MovieType"));
            AddMovie(new MenuAction(5, "Horror", "MovieType"));
            AddMovie(new MenuAction(6, "Romance", "MovieType"));
            AddMovie(new MenuAction(7, "Thriller", "MovieType"));

            AddMovie(new MenuAction(1, "All movies", "DisplayMovies"));
            AddMovie(new MenuAction(2, "Movies to watch", "DisplayMovies"));
            AddMovie(new MenuAction(3, "Watched movies", "DisplayMovies"));

          
        }

    }
}
