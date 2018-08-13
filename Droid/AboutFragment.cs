
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Database;
using Android.Provider;
using Android.Graphics;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using System.Threading.Tasks;
using Android.Graphics.Drawables;
using Android.Animation;

namespace stops
{
	public class AboutFragment : FocuableFragment
	{
		
		private View _view;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);


			// Create your fragment here
		}

		public override void OnViewCreated (View view, Bundle savedInstanceState)
		{
			base.OnViewCreated (view, savedInstanceState);

			showAnim ();
			HideAnim ();

		}

		async void showAnim() {


			await Task.Delay (1000);

			// previously invisible view
			View myView = _view.FindViewById(Resource.Id.test);

			var prms = myView.LayoutParameters;
			prms.Height = 300;
			myView.LayoutParameters = prms;

			await Task.Delay (1000);

			prms = myView.LayoutParameters;
			prms.Height = 100;
			myView.LayoutParameters = prms;

			// get the center for the clipping circle
			int cx = myView.Width / 2;
			int cy = myView.Height / 2;

			// get the final radius for the clipping circle
			float finalRadius = (float) Math.Sqrt(cx * cx + cy * cy);

			// create the animator for this view (the start radius is zero)
			Animator anim =
				ViewAnimationUtils.CreateCircularReveal(myView, cx, cy, 0, finalRadius);

			// make the view visible and start the animation
			myView.Visibility = ViewStates.Visible;
			anim.Start();
		}

		//http://pulse7.net/android/android-create-circular-reveal-animation-and-ripple-effect-like-whatsapp/
		async void HideAnim() {

			await Task.Delay (2000);

			// previously invisible view
			View myView = _view.FindViewById(Resource.Id.test);

			// get the center for the clipping circle
			int cx = myView.Width / 2;
			int cy = myView.Height / 2;

			// get the final radius for the clipping circle
			float finalRadius = (float) Math.Sqrt(cx * cx + cy * cy);

			// create the animator for this view (the start radius is zero)
			Animator anim =
				ViewAnimationUtils.CreateCircularReveal(myView, cx, cy, finalRadius,0);

			// make the view visible and start the animation
			myView.Visibility = ViewStates.Visible;
			anim.Start();

			anim.AnimationEnd += (object sender, EventArgs e) => {
				myView.Visibility = ViewStates.Invisible;
			};
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			_view =  inflater.Inflate(Resource.Layout.AboutFragmentCard, container, false);


		



			return _view;
		}







	}


}


