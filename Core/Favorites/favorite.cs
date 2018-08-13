using System;
namespace Core.Favorites
{
    public class Favorite
    {
        public string fromName { get; set; }
        public string fromId { get; set; }
        public double fromLat { get; set; }
        public double fromLon { get; set; }

        public string toName { get; set; }
        public string toId { get; set; }
        public double toLat { get; set; }
        public double toLon { get; set; }
    }
}
