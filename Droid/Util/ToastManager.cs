using System;
using Android.Widget;
using Android.Content;
using System.Threading.Tasks;
using Android.Support.Design.Widget;

namespace stops.util
{
	public static class ToastManager
	{
		private static Toast _toast;
		

		public async static void show(string text, int delay = 0) {

			if (delay > 0) {
				await Task.Delay(delay);
				show (text);
				return;
			}

			if (_toast == null) {
				_toast = Toast.MakeText (Android.App.Application.Context, "Init", ToastLength.Long);
				TextView v = (TextView) _toast.View.FindViewById(global::Android.Resource.Id.Message);
				if (v != null) {
					v.Gravity = Android.Views.GravityFlags.Center;
				}
			}


			_toast.SetText(text);
			_toast.Show();

		}
	}
}

