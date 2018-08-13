
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
using Android.Locations;
using Ninject;
using Core.Travel;
using Android.Support.Design.Widget;
using Android.Provider;
using System.Threading.Tasks;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Android.Graphics;
using Core.Util;

namespace stops
{
	public class MainMapFragment :  Android.Support.V4.App.Fragment,IOnMapReadyCallback, Android.Gms.Common.Apis.GoogleApiClient.IOnConnectionFailedListener, Android.Gms.Common.Apis.GoogleApiClient.IConnectionCallbacks,ILocationSource, Android.Gms.Location.ILocationListener 
	{
        private Clans.Fab.FloatingActionMenu _mainMapFragmentButtonFab;
        private Button _mainMapFragmentButtonSearch;

		private GoogleApiClient mGoogleApiClient;
		private static  LocationRequest REQUEST = LocationRequest.Create()
			.SetPriority(LocationRequest.PriorityBalancedPowerAccuracy);

        private bool _debugLocatoin = true;

		private ILocationSourceOnLocationChangedListener mMapLocationListener;

		public void OnConnectionFailed (Android.Gms.Common.ConnectionResult result)
		{
			// Do nothing
		}

		public void OnConnected (Bundle connectionHint)
		{
			LocationServices.FusedLocationApi.RequestLocationUpdates(
				mGoogleApiClient,
				REQUEST,
				this);  // LocationListener	
		}

		public void OnConnectionSuspended (int cause)
		{
			// Do nothing
		}

		public void OnLocationChanged (Location location)
		{

           
			var newLocation = location;
			// Not accurate enought
			if(newLocation.Accuracy >=1000) {
				return;
			}

			/*
			 * If we dont have a position or there is 200 meters from the last location we update the camera
			 * and the markers
			 */
			if(_currentLocation == null || _currentLocation.DistanceTo(newLocation) >=200) {
				_currentLocation = location;

				/*
					 * IF GPS was disabld initaly.
					 */
				if(_GPSSnackbar != null) {
					_GPSSnackbar.Dismiss();
					_GPSSnackbar = null;
				}

				PositionCamera(location);
				GetNearestStops ();
			}

			_currentLocation = location;

			if (mMapLocationListener != null) {
				mMapLocationListener.OnLocationChanged(location);
			}
		}

		public void Activate (ILocationSourceOnLocationChangedListener listener)
		{
			mMapLocationListener = listener;
		}

		public void Deactivate ()
		{
			mMapLocationListener = null;
		}
        private ViewTreeObserver vto;
	
		private View _snackBarView;

		private bool _useLastKnowLocation;

		private Location _currentLocation;

		private Snackbar _GPSSnackbar;

		private SupportMapFragment _supportMapFragment;
		private Core.ViewModels.MapViewModel _mapViewModel;
		private GoogleMap _googleMap;
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);


		}


		public MainMapFragment Init(View snackBarView) {

			_snackBarView = snackBarView;


			return this;
		}

        private View view;

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			view =  inflater.Inflate(Resource.Layout.MainMapFragment, container, false);

			_mapViewModel = Stops.Setup.IoC.Container.Get<Core.ViewModels.MapViewModel> ();

			/*
			 * Create the map
			 */
			_supportMapFragment = new SupportMapFragment ();
			Android.Support.V4.App.FragmentTransaction transaction = FragmentManager.BeginTransaction ();

			transaction.Add(Resource.Id.mainMapFragmentMapWrapper, _supportMapFragment);
			transaction.SetTransition (Android.Support.V4.App.FragmentTransaction.TransitFragmentFade);
			transaction.Commit();

            //TODO

            vto = view.ViewTreeObserver;
            vto.GlobalLayout += GlobalLayoutHandler;

			_supportMapFragment.GetMapAsync (this);

            _mainMapFragmentButtonSearch = (Button)view.FindViewById(Resource.Id.mainMapFragmentButtonSearch);
            _mainMapFragmentButtonFab = view.FindViewById<Clans.Fab.FloatingActionMenu>(Resource.Id.mainMapFragmentButtonFab);



           

            _mainMapFragmentButtonSearch.Click += (sender, e) => {
                MapBridge.Instance.Trigger("SearchButtonClicked");
            };

			return view;
		}

        private void GlobalLayoutHandler(object sender, EventArgs e)
        {
            try
            {
                vto.GlobalLayout -= GlobalLayoutHandler;
            }
            catch {}

            int statusbarheight = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 24, Application.Context.Resources.DisplayMetrics);
            int actiobbarHeight = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 56, Application.Context.Resources.DisplayMetrics);
            int pagertabstripheight = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 56, Application.Context.Resources.DisplayMetrics);

            MapBridge.Instance.windowHeight = view.Height - actiobbarHeight;;
        }

		public void EnableAPI() {

			if (mGoogleApiClient != null && !mGoogleApiClient.IsConnected) {
				mGoogleApiClient.Connect ();
			}
		}


		public void DisableAPI() {
			if (mGoogleApiClient != null && mGoogleApiClient.IsConnected) {
				mGoogleApiClient.Disconnect ();
			}
		}

		public void GetPos() {

			if (!_useLastKnowLocation) {
				return;
			}



			var location = Util.Util.GetLastKnownLocation ();
			if (_currentLocation == null && location != null) {
				_currentLocation = location;
				_useLastKnowLocation = false;
				PositionCamera(_currentLocation);
				GetNearestStops ();
			}
		}

        public async void PositionCamera() {

            await Task.Delay(3000);
            if (_debugLocatoin)
            {
                PositionCamera(null);
            }
        }

		public void OnMapReady (GoogleMap googleMap)
		{
            _googleMap = googleMap;

            PositionCamera();


			_googleMap.MyLocationEnabled = true;
			_googleMap.UiSettings.MapToolbarEnabled = false;
			_googleMap.UiSettings.CompassEnabled = false;
			_googleMap.UiSettings.MyLocationButtonEnabled = false;
			_googleMap.UiSettings.TiltGesturesEnabled = false;



			mGoogleApiClient = new GoogleApiClient.Builder(Application.Context)
				.AddApi(LocationServices.API)
				.AddConnectionCallbacks(this)
				.AddOnConnectionFailedListener(this)
				.Build();

			mGoogleApiClient.Connect ();

			_googleMap.SetLocationSource(this);
			/*
			 *  Init GPS posistioning
			 */
			var location = PositionCamera (Util.Util.GetLastKnownLocation ());
			ShowGPSMessage (false);

			/*
			 * If we have a fix we show the markers
			 */
			if (_debugLocatoin || location != null) {
				GetNearestStops ();
			}
		}

		Location PositionCamera(Location location) {

            if (location != null && !_debugLocatoin) {
				_googleMap.AnimateCamera (CameraUpdateFactory.NewLatLngZoom (new LatLng (location.Latitude + 0.0012, location.Longitude), 15.0f));
				_currentLocation = location;
			}  else {
				_googleMap.AnimateCamera (CameraUpdateFactory.NewLatLngZoom (new LatLng (57.724215, 11.970159), 12.0f));
			}


			return location;
		}

		async void ShowGPSMessage(bool dontShowSnackBar = true) {

			await Task.Delay (250); // For smother animation on startup

			if (dontShowSnackBar == false && Util.Util.isGpsPositionEnabled ()) {

				//Snackbar.Make (_snackBarView, "Hämtar de närmaste hållplatserna!", Snackbar.LengthLong).Show ();
			}  else if(dontShowSnackBar == false) {

				_GPSSnackbar = Snackbar.Make (_snackBarView, "Slå på GPS:en (Lågenergi-läge används!)", Snackbar.LengthIndefinite)
					.SetAction ("GPS-inställningar", (view) => {
						_currentLocation = null;
						_useLastKnowLocation = true;
						StartActivity (new Intent (Settings.ActionLocationSourceSettings));
					});


				_GPSSnackbar.SetActionTextColor(Color.ParseColor("#2B9CF5"));

				_GPSSnackbar.Show ();

				DismissSnackbar();

			}
		}

		async void DismissSnackbar() {
			await Task.Delay (6000);
			if (_GPSSnackbar != null) {
				_GPSSnackbar.Dismiss ();
			}
		}

		async void GetNearestStops() {

			try {
                StoppingPlaces data = null;
                if(!_debugLocatoin) {
                    data = await _mapViewModel.GetNearestStops(_currentLocation.Latitude, _currentLocation.Longitude);
                }
                else {
                    data = await _mapViewModel.GetNearestStops(57.724215, 11.970159);
                }
				 
				if(data.GetList().Count >0) {

					/*
					 * Zoom in on first result
					 */
					_googleMap.Clear (); 
					var stopList = data.GetList();
					var firstStop = stopList[0];
					PositionCamera(_currentLocation);


                    _googleMap.InfoWindowClick += (sender, e) => {
                  

                         var stopPlace = stopList.Find(x => x.Name == e.Marker.Title);
                         MapBridge.Instance.FireStoppingPlaceSelcted(stopPlace);
                    };

                

					/*
					 * Create markers
					 */
					foreach(StoppingPlace stop in stopList) {




                       
						_googleMap.AddMarker(new MarkerOptions()
							.SetPosition(new LatLng (stop.Lat, stop.Lon))
							.SetTitle(stop.Name)
							.SetIcon(BitmapDescriptorFactory.FromResource(stops.Resource.Drawable.marker)));
					}
				}
			}
			catch(Exception) {

				Snackbar.Make (_snackBarView, "Gick inte att hämta hållplatser", Snackbar.LengthIndefinite)
				.SetAction ("Fösök igen", (view) => { 
					GetNearestStops();
				}).Show ();
			}
		}

       
       

       
    }
}
