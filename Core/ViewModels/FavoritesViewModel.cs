using System;
using System.Collections.Generic;
using Core.Favorites;
using Core.Interfaces;
using Ninject;

namespace Core.ViewModels
{
    public class FavoritesViewModel
    {
        private FavoritesManager favorites;
        [Inject]
        public FavoritesViewModel()
        {
            favorites = new FavoritesManager();
        }

        public void Load() {
            favorites.Load();
        }

        public void Add(Favorite favorite)
        {
            favorites.Add(favorite);
            favorites.Save();
        }


        public List<Favorite> GetList()
        {
            return favorites.GetList();
        }

        public void RemoveInList(int position)
        {
            favorites.GetList().RemoveAt(position);
            favorites.Save();
        }
    }
}
