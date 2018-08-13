using System;
using Core.Travel;

namespace stops
{
    public class FavoriteBridge : Core.Util.EventClass
    {


        private static FavoriteBridge instance;

        private FavoriteBridge() { }

        public static FavoriteBridge Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FavoriteBridge();
                }
                return instance;
            }
        }

       
    }
}
