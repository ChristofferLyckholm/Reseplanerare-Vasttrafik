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
	public class StoppingPlaceViewModel : EventClass
	{	
		private IPlatform _uniqId;
		private iFileSystem _filesystem;

		[Inject]
		public StoppingPlaceViewModel (IPlatform uniqId, iFileSystem filesystem)
		{
			_uniqId = uniqId;
			_filesystem = filesystem;
		}


		public async Task<SortedDictionary<string,StoppingTimeManager>> GetArivalsList(StoppingPlace stop, DateTime date) {
			var http = new Http.API();
			var stoppingTimeManager = (HttpResult)await http.GetStop(stop, date);
			var sortedList = ((StoppingTimeManager)stoppingTimeManager.Data).Sort ();

			return sortedList;
		}



	
	}
}

