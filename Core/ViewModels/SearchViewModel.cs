using System;
using Core.Interfaces;
using Ninject;
using System.Threading.Tasks;
using Core.Http;
using Core.Travel;
using Core.Util;
using System.Collections.Generic;

namespace Core.ViewModels
{
	public class SearchViewModel : EventClass
	{	
		private IPlatform _uniqId;

		private iFileSystem _filesystem;


		private StoppingPlaces latestChociedStop;

		[Inject]
		public SearchViewModel (IPlatform uniqId, iFileSystem filesystem)
		{
			_uniqId = uniqId;
			_filesystem = filesystem;
		}

		public void Init(string savedLatestChoiceFileName) {
            latestChociedStop = new StoppingPlaces(savedLatestChoiceFileName == null ? null : savedLatestChoiceFileName + ".xml");

			// Get saved last choiced stop (choiced in dropdown)
			var items = latestChociedStop.Load (_filesystem);
			if (items !=null && items.Count == 1) {
				Trigger ("savedLatstChoicedStop",items);
			}
		}

		public bool HasLatestCLickeStop() {
			return latestChociedStop.GetList ().Count == 1;
		}

		public StoppingPlace GetLatestCLickeStop() {
			return latestChociedStop.GetList () [0];
		}


		public void clearLastChociedStop() {
			latestChociedStop.Clear ();
			latestChociedStop.Save (_filesystem);
		}

		public void SaveLastChoicedStop(Core.Travel.StoppingPlace stop) {
			latestChociedStop.Clear ();
			latestChociedStop.Add(stop);
			latestChociedStop.Save (_filesystem);
		}

		public async Task<HttpResult> GetStops(string text) {
			var http = new Http.API();
			return await http.GetStops(text);
		}
	}
}

