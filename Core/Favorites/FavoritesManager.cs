using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Core.Travel;

namespace Core.Favorites
{
    public class FavoritesManager
    {
        private List<Favorite> _favorites;

        private string _filePath;

        public FavoritesManager()
        {
            _favorites = new List<Favorite>();

            _filePath = "favoritesList";
        }

        public List<Favorite> Load()
        {

            XmlSerializer serializer = new XmlSerializer(typeof(List<Favorite>));
            List<Favorite> items = (List<Favorite>)Core.Util.Injected.Instance.Filesystem.deserilaze(serializer, _filePath);

            if (items != null)
            {
                _favorites = items;
                return _favorites;
            }
            return null;



        }

        public void Add(Favorite stop)
        {
            _favorites.Add(stop);
        }

        public void Clear()
        {
            _favorites.Clear();
        }

        public List<Favorite> GetList()
        {
            return _favorites;
        }

        public void Save()
        {


            List<Favorite> yourlist = _favorites;
            XmlSerializer serializer = new XmlSerializer(typeof(List<Favorite>));
            Core.Util.Injected.Instance.Filesystem.serilaze(serializer, _filePath, yourlist);

        }
    }
}
