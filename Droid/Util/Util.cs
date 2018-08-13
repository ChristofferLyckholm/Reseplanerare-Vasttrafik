using System;
using Android.Views;
using Android.Animation;
using Android.Locations;
using Android.App;
using Android.Content;
using Android.Views.Animations;

namespace stops.Util
{
	public static class Util
	{
		public static int PxToDP (int pixelsValue)
		{
			return (int)(pixelsValue / Android.App.Application.Context.Resources.DisplayMetrics.Density);
		}

		public static int dpToPixel(int px) {

			return (int)( px * Android.App.Application.Context.Resources.DisplayMetrics.Density);
		}

		public static ValueAnimator AnimateHeight(View view,int start, int end) {

			ValueAnimator animator = ValueAnimator.OfInt(start, end);
			animator.SetInterpolator(new LinearInterpolator());
			animator.Update +=
				(object sender, ValueAnimator.AnimatorUpdateEventArgs e) => { 

				var value = (int)animator.AnimatedValue;
				ViewGroup.LayoutParams layoutParams = view.LayoutParameters;
				layoutParams.Height = value;
				view.LayoutParameters=layoutParams;

			}; 

			return animator;
		}

      



		public static ValueAnimator AnimateAlpha(View view,int start, int end) {

			ValueAnimator animator = ValueAnimator.OfInt(start, end);
			animator.SetInterpolator(new LinearInterpolator());
			animator.Update +=
				(object sender, ValueAnimator.AnimatorUpdateEventArgs e) => { 

				var value = (int)animator.AnimatedValue;
				view.Alpha = value;
			}; 

			return animator;
		}



		public static bool isGpsPositionEnabled() {

			LocationManager mgr = (LocationManager)Application.Context.GetSystemService (Context.LocationService);
		
			string gpsProvider = LocationManager.GpsProvider;

			return mgr.IsProviderEnabled (gpsProvider);
		}

		public static bool isNetworkPositionEnabled() {

			LocationManager mgr = (LocationManager)Application.Context.GetSystemService (Context.LocationService);

			string networkProvider = LocationManager.NetworkProvider;

			return mgr.IsProviderEnabled (networkProvider);
		}

		public static DateTime ChangeTime(this DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
		{
			return new DateTime(
				dateTime.Year,
				dateTime.Month,
				dateTime.Day,
				hours,
				minutes,
				seconds,
				milliseconds,
				dateTime.Kind);
		}



		public static bool isPositionEnabled() {

			LocationManager mgr = (LocationManager)Application.Context.GetSystemService (Context.LocationService);

			string networkProvider = LocationManager.NetworkProvider;
			string gpsProvider = LocationManager.GpsProvider;

			return mgr.IsProviderEnabled (networkProvider) || mgr.IsProviderEnabled (gpsProvider);
		}

		public static Location GetLastKnownLocation() {



			/*
			 * Get Last Know location
			 */
			LocationManager mgr = (LocationManager)Application.Context.GetSystemService (Context.LocationService);

			string networkProvider = LocationManager.NetworkProvider;
			string gpsProvider = LocationManager.GpsProvider;

			Location gpsLoacation = null, networkLocation = null;

			if(mgr.IsProviderEnabled(networkProvider)) {
				networkLocation = mgr.GetLastKnownLocation (networkProvider);
			}
			if(mgr.IsProviderEnabled(gpsProvider)) {
				gpsLoacation = mgr.GetLastKnownLocation (gpsProvider);

			}

			/*
			 * Determn best location
			 */
			Location bestLocation = null;
			if (networkLocation != null && gpsLoacation != null) {

				if (networkLocation.Accuracy < gpsLoacation.Accuracy) {
					bestLocation = networkLocation;
				} else {
					bestLocation = gpsLoacation;
				}

			} else if (gpsLoacation != null || networkLocation != null) {
				if (gpsLoacation != null) {
					bestLocation = gpsLoacation;
				} else if (networkLocation != null) {
					bestLocation = networkLocation;
				}
			} else {
				return null;
			}	


			return bestLocation;
		}

		public static void ToogleView(View myView, bool show, Func<int> param = null) {


			// get the center for the clipping circle
			int cx = myView.Width / 2;
			int cy = myView.Height / 2;

			// get the final radius for the clipping circle
			float finalRadius = (float) Math.Sqrt(cx * cx + cy * cy);

			// create the animator for this view (the start radius is zero)
			Animator anim;
			if (show) {
				anim = ViewAnimationUtils.CreateCircularReveal (myView, cx, cy, 0, finalRadius);
			} else {
				anim = ViewAnimationUtils.CreateCircularReveal (myView, cx, cy, finalRadius, 0);
			}
			anim.SetDuration (500);
			anim.AnimationEnd += (object sender, EventArgs e) => {
				if(param != null) {
					param();
				}

				if(!show) {
					myView.Visibility = ViewStates.Invisible;
				}
			};
			// make the view visible and start the animation
			myView.Visibility = ViewStates.Visible;
			anim.Start();
		}

	}
}

