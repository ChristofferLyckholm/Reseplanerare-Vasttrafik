
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
using Android.Graphics;
using Ninject;

namespace stops
{
	public class DateFragment : EventFragment,Android.App.DatePickerDialog.IOnDateSetListener, Android.App.TimePickerDialog.IOnTimeSetListener
	{
		private View _view;

        private Core.ViewModels.FavoritesViewModel _favoritesViewModel;

        private ImageButton _buttonFavorite;
		/*
		 * Holds the search date
		 */
		private DateTime _searchDate;
		private stops.util.Time _currentTime;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}


		public void Init(bool showDeparture = false) {

			/*
			 *  Date picker field
			 */	
            _favoritesViewModel = Stops.Setup.IoC.Container.Get<Core.ViewModels.FavoritesViewModel>();
            _favoritesViewModel.Load();


			SetCurrentTime (true);
			var datePicker = _view.FindViewById<FrameLayout> (Resource.Id.dtmDate);
			datePicker.Click += (o, s) => {

				//
				// Start with time picker
				//
				var dialog = new TimePickerDialogFragment (Activity, _searchDate, this);
				dialog.Show (FragmentManager, null);
			};

			datePicker.LongClick += (object sender, View.LongClickEventArgs e) => {
				SetCurrentTime ();
				return;
			};

            _buttonFavorite = _view.FindViewById<ImageButton>(Resource.Id.btnFavorite);
            SetFavoritButtonDark();
            _buttonFavorite.Click += (sender, e) => {

                if((int)_buttonFavorite.Tag == Resource.Drawable.star_dark) {
                    SetFavoritButtonGold();
                }
                else {
                    SetFavoritButtonDark();
                }
                this.Events.Trigger("favoritebuttonclicked", (int)_buttonFavorite.Tag == Resource.Drawable.star_dark ? false : true);
            };

			/*
			 * Show depature spinner
			 */
			var dateFragmentTimeFrame = _view.FindViewById<FrameLayout> (Resource.Id.DateFragmentTimeFrame);
			var dateFragmentDepartureFrame = _view.FindViewById<FrameLayout> (Resource.Id.DateFragmentDepartureFrame);
			if (showDeparture) {
				
				var prms = (LinearLayout.LayoutParams)dateFragmentTimeFrame.LayoutParameters;
				prms.Weight = 0.65f;
				dateFragmentTimeFrame.LayoutParameters = prms;

				prms = (LinearLayout.LayoutParams)dateFragmentDepartureFrame.LayoutParameters;
				prms.Weight = 0.35f;
				dateFragmentDepartureFrame.LayoutParameters = prms;

				var spinner = _view.FindViewById<Spinner> (Resource.Id.DateFragmentDepartureSpinner);

				spinner.ItemSelected += (sender, e) => {

				};
				var adapter = ArrayAdapter.CreateFromResource (
					Application.Context, Resource.Array.when_array, Resource.Layout.SpinnerText);

				adapter.SetDropDownViewResource (Resource.Layout.Spinner);
				spinner.Adapter = adapter;
			}





		}

        public void SetFavoritButtonDark() {

            _buttonFavorite.Tag = Resource.Drawable.star_dark;
            _buttonFavorite.SetImageResource(Resource.Drawable.star_dark);
        }

        public void SetFavoritButtonGold() {
            _buttonFavorite.Tag = Resource.Drawable.star;
            _buttonFavorite.SetImageResource(Resource.Drawable.star);
        }

        public void UpdateFavoriteDataModel() {
            _favoritesViewModel.Load();


        }

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			_view = inflater.Inflate(Resource.Layout.DateFragment, container, false);
			return _view;
		}


		public void OnTimeSet (TimePicker view, int hourOfDay, int minute)
		{

			_currentTime.hourOfDay = hourOfDay;
			_currentTime.minute = minute;

			var dialog = new DatePickerDialogFragment(Activity,_searchDate,this);
			dialog.Show(FragmentManager, null);
		}

		public void OnDateSet (DatePicker view, int year, int monthOfYear, int dayOfMonth)
		{
			_currentTime.year = year;
			_currentTime.monthOfYear = monthOfYear;
			_currentTime.dayOfMonth = dayOfMonth;
			UpdateTime ();

		}
			
		public void SetCurrentTime(bool silent = false) {

			_searchDate = DateTime.Now;
			_searchDate = _searchDate.ToLocalTime ();

			var timeField = _view.FindViewById<EditText> (Resource.Id.fromAliasText);
			timeField.Text = "Idag " + _searchDate.ToString("HH:mm");

			_currentTime = new stops.util.Time () {
				hourOfDay = _searchDate.Hour,
				minute = _searchDate.Minute,
				year = _searchDate.Year,
				monthOfYear = _searchDate.Month,
				dayOfMonth = _searchDate.Day,
			};

			if(!silent) {
				this.Events.Trigger ("Dateupdated", _searchDate);
			}
		}

		public DateTime GetDate() {
			return _searchDate;
		}

		public void UpdateTime() {

			var todaysDate = DateTime.Now.ToString("yy-MM-dd");
			_searchDate = new DateTime (_currentTime.year, _currentTime.monthOfYear+1, _currentTime.dayOfMonth, _currentTime.hourOfDay, _currentTime.minute, 0);
			var newDateTime = _searchDate.ToString("yy-MM-dd");
			var timeField = _view.FindViewById<EditText> (Resource.Id.fromAliasText);

			this.Events.Trigger ("Dateupdated", _searchDate);

			//Todays date
			if (todaysDate == newDateTime) {
				timeField.Text = "Idag " + _searchDate.ToString ("HH:mm");
			} else {
				timeField.Text = _searchDate.ToString ("yy-MM-dd HH:mm");
			}

		}


	}
}

