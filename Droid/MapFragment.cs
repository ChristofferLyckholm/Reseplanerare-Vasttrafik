
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
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Views.Animations;
using Ninject;
using Core.ViewModels;
using Android.Locations;
using Core.Util;
using Core.Travel;

namespace stops
{
	public class MapFragment : Android.Support.V4.App.Fragment,IOnMapReadyCallback
	{
		private SupportMapFragment _supportMapFragment;
        private GoogleMap _googleMap;
		private View _view;

		public EventClass Events = new EventClass();
		private bool isCreated = false;
		private RelativeLayout _mapFragmentRoot;

		private MapViewModel _mapViewModel;

		private bool isMapReady = false;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public void Hide() {
			var parms = _mapFragmentRoot.LayoutParameters;
			parms.Height = 0;
			_mapFragmentRoot.LayoutParameters = parms;
		}

		public void Show() {
			var parms = _mapFragmentRoot.LayoutParameters;
			parms.Height = WindowManagerLayoutParams.MatchParent;
			_mapFragmentRoot.LayoutParameters = parms;
		}

		public void Init() {

			if (isCreated) {
				return;
			}

			isCreated = true;
			_supportMapFragment = new SupportMapFragment ();

			//Android.Support.V4.App.Fragment
			Android.Support.V4.App.FragmentTransaction transaction = FragmentManager.BeginTransaction ();
			/*
			 * When this container fragment is created, we fill it with our first
			 * "real" fragment
			 */

			//_mapFragmentRoot.Alpha = 0.01f;

	
		
			transaction.Add(Resource.Id.mapFragmentMapCointatiner, _supportMapFragment);
			transaction.SetTransition (Android.Support.V4.App.FragmentTransaction.TransitFragmentFade);
			transaction.Commit();

			_supportMapFragment.GetMapAsync (this);

		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			_view = (View)inflater.Inflate(Resource.Layout.MapFragment, container, false);

			_mapViewModel = Stops.Setup.IoC.Container.Get<Core.ViewModels.MapViewModel> ();

			_mapFragmentRoot = (RelativeLayout) _view.FindViewById (Resource.Id.mapFragmentMapCointatiner);

			var view = (Button)_view.FindViewById (Resource.Id.mapFragmentCloseBtn);
			view.Click += (object sender, EventArgs e) => {
				Events.Trigger("close");
			};

           


			return _view;
		}

		public void StartSerach () {

			if (!isMapReady) {
				return;
			}

            _googleMap.Clear ();

			/*
			 *  googleMap.AddMarker (new MarkerOptions ()
				.SetPosition (new LatLng (0, 0))
				.SetTitle ("Marker"));

			 */
			Location location = null;
			if((location = Util.Util.GetLastKnownLocation()) != null) {

				GetNearestStops (location);

			} else {

				AlertDialog.Builder alert = new AlertDialog.Builder (Activity);

				alert.SetTitle ("Gick inte att hämta position");
				alert.SetMessage ("Vill du försöka igen?");
				alert.SetPositiveButton ("Ja", (senderAlert, args) => {
					StartSerach();
				} );

				alert.SetNegativeButton ("Nej", (senderAlert, args) => {
					Events.Trigger("close");
				} );
				//run the alert in UI thread to display in the screen
				Dialog dialog = alert.Create();
				dialog.Show();

			}

			






		}

		async void GetNearestStops(Location location) {

			try {
				
				var data = await _mapViewModel.GetNearestStops (location.Latitude, location.Longitude);
				if(data.GetList().Count >0) {

					/*
					 * Zoom in on first result
					 */
					var stopList = data.GetList();
					var firstStop = stopList[0];
                    _googleMap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(firstStop.Lat, firstStop.Lon), 14.0f));
				
					/*
					 * Create markers
					 */
					foreach(StoppingPlace stop in stopList) {
                        _googleMap.AddMarker (new MarkerOptions ()
							.SetPosition (new LatLng (stop.Lat, stop.Lon))
							.SetTitle (stop.Name));
					}
						
				}

			}
			catch(Exception) {

				AlertDialog.Builder alert = new AlertDialog.Builder (Activity);

				alert.SetTitle ("Gick inte att hämta Hållplatser");
				alert.SetMessage ("Vill du försöka igen?");
				alert.SetPositiveButton ("Ja", (senderAlert, args) => {
					StartSerach();
				} );

				alert.SetNegativeButton ("Nej", (senderAlert, args) => {
					Events.Trigger("close");
				} );
				//run the alert in UI thread to display in the screen
				Dialog dialog = alert.Create();
				dialog.Show();

			}
		}

		public void OnMapReady (GoogleMap googleMap)
		{
            _googleMap = googleMap;
			isMapReady = true;
			StartSerach ();
		}


	}
}

