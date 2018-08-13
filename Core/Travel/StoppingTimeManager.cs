using System;
using System.Collections.Generic;

namespace Core.Travel
{
	public class StoppingTimeManager
	{
		private List<TravelTime> _stopTimes = new List<TravelTime> ();

		public StoppingTimeManager ()
		{
		}

		public void Add(TravelTime stop) {
			_stopTimes.Add(stop);
		}

		public void Clear() {
			_stopTimes.Clear ();
		}

		public List<TravelTime> GetList() {
			return _stopTimes;	
		}

		public StoppingTimeManager SortOnDate() {

			_stopTimes.Sort((x, y) => x.Date.CompareTo(y.Date));
			return this;
		}

		public SortedDictionary<string,StoppingTimeManager> Sort() {

			/*
			 * Sort on line numbers > orgin
			 */
			var stoppingTimeList = new SortedDictionary<string,StoppingTimeManager> ();
			foreach(var stoppingTime in _stopTimes) {


				StoppingTimeManager found;
				if (!stoppingTimeList.TryGetValue (stoppingTime.name + stoppingTime.direction, out found)) {
					stoppingTimeList.Add (stoppingTime.name + stoppingTime.direction, new StoppingTimeManager ());
				} 
				stoppingTimeList [stoppingTime.name + stoppingTime.direction].Add (stoppingTime);
					
			}

			/*
			 * Sort on date
			 */
			var stoppingTimeListSortedOndate = new SortedDictionary<string,StoppingTimeManager> ();
			foreach (var item in stoppingTimeList) {
				stoppingTimeListSortedOndate.Add (item.Key, ((StoppingTimeManager)item.Value).SortOnDate ());
			}

			return stoppingTimeListSortedOndate;

		}

	}
}

