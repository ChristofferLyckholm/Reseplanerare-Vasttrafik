
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Util;
using System.Threading.Tasks;
using Stops;
using Stops.Setup;
using Ninject;
using stops.util;

namespace Stops
{
	[Activity(Label = "Reseplaneraren", Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
	public class SplashActivity : Activity
	{
		static readonly string TAG = "X:" + typeof(SplashActivity).Name;

		private Core.ViewModels.InitViewModel _initViewModel;

		public static Activity Activity;

	

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SplashActivity.Activity = this;

			Task startupWork = new Task(() => {

				/*
			 	* Init Session
			 	*/ 
				Stops.Setup.IoC.Container = new Ninject.StandardKernel(new NinjectLoader());
				_initViewModel = Stops.Setup.IoC.Container.Get<Core.ViewModels.InitViewModel> ();

				var intent = new Intent(Application.Context, typeof(MainActivity));
				intent.AddFlags(ActivityFlags.ClearTop);

				StartActivity(intent);

			});
			startupWork.Start();
		}
			
		protected override void OnResume()
		{
			base.OnResume();


		}


	}
}

