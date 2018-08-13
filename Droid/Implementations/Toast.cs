using System;
using Core.Interfaces;
using System.Net.NetworkInformation;
using Android.Telephony;
using Android.App;
using Android.Content;
using Java.Util;
using System.Threading;
using System.Threading.Tasks;

namespace Stops.Implementations
{


	public class Toast : iToast
	{
		public void show (string text, int delay = 0)
		{
			if (delay > 0) {
				delayToast (text);
			} else {
				stops.util.ToastManager.show (text);
			}
		}

		private async void delayToast(string text) {
			await Task.Delay(1000);
			stops.util.ToastManager.show (text);
		}

		


	}
	
}

