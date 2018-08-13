using System;
using Core.Interfaces;
using Ninject;
using System.Threading.Tasks;
using Core.Http;
using Core.Util;
using Core.Travel;

namespace Core.ViewModels
{
	public class MapViewModel : EventClass
	{	
		[Inject]
		public MapViewModel()
		{

			

		}

		public async Task<StoppingPlaces> GetNearestStops(double lat, double lon) {
			var http = new Http.API();

			var result = await http.GetNearestStops(lat,lon);
             result = await http.GetNearestStops(lat, lon);
			return (StoppingPlaces)result.Data;
		}
	}
}

