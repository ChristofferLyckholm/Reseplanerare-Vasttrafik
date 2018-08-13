using System;
using Core.Travel;

namespace stops
{
    public class MapBridge : Core.Util.EventClass
    {
      

		private static MapBridge instance;

        public int windowHeight = 0;

		private MapBridge() { }

		public static MapBridge Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new MapBridge();
				}
				return instance;
			}
		}

        public  void FireStoppingPlaceSelcted(StoppingPlace stoppingplaceselxted) {

            Instance.Trigger("stoppingplaceselected", stoppingplaceselxted, 0);

        }  
    }
}
