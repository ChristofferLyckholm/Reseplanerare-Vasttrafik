using System;
using System.Collections.Generic;

namespace stops.Util
{

	public static class LazyLoad {

		private static Dictionary<string,Android.Support.V4.App.Fragment> LazyLoadList = new Dictionary<string,Android.Support.V4.App.Fragment>();

		public static void Load(Android.Support.V4.App.Fragment fragment) {

			LazyLoadList [fragment.GetType ().Name] = fragment;

		}

		public static Android.Support.V4.App.Fragment GetFragment(string fragmentName) {

			return LazyLoadList [fragmentName];
		}

		public static bool HasLoaded(string fragmentName) {

			return LazyLoadList [fragmentName] != null;
		}
	}
}

