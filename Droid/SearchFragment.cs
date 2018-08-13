
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
using Android.Support.V7.Widget;
using Core.Travel;
using stops.Adapters;
using Ninject;
using Core.Http;
using Android.Views.InputMethods;

namespace stops
{
	public class SearchFragment : EventFragment,Android.Support.V7.Widget.SearchView.IOnQueryTextListener,
	Android.Support.V7.Widget.SearchView.IOnSuggestionListener, ActionMode.ICallback, View.IOnLayoutChangeListener
	{

		static readonly string[] COLUMNS = new string[] {
			BaseColumns.Id,
			SearchManager.SuggestColumnText1,
		};

		private SuggestionsAdapter mSuggestionsAdapter;


		private Core.ViewModels.SearchViewModel _stoppingPlaceViewModel;

		/// <summary>
		/// Holds the selected stop - if the stop is beeing editing this will not be null on int.. TODO
		/// </summary
		private StoppingPlace _choicedStop = null;

		private Android.Support.V7.Widget.SearchView _searchView;
		private ArrayAdapter _stopAutoCompleteAdapter;
		private List<StopsAdapter> _stopAutoCompleteOptions = new List<StopsAdapter> ();

		private View _view;
		private List<StoppingPlace> _stopServerList;

		private string _oldFromText = null;

		/*
		 * On startup we load the latest choiced stop
		 * For a beter UI experices we delete the choiced stop text on
		 * the autocomplete text..
		 */
		private bool _ignoreAutoCompleteTextChange = false;
		private bool _deleteAllTextOnClick = false;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

		//https://philio.me/styling-the-searchview-with-appcompat-v21/
		//https://code.google.com/p/android/issues/detail?id=70754
		public void Init(string searchIdName, string textFieldHint, bool topLine, bool serachLine, bool bottomline = false) {

			_stoppingPlaceViewModel = Stops.Setup.IoC.Container.Get<Core.ViewModels.SearchViewModel> ();


			/*
			 * Load last selected on startup
			 */
			_searchView = _view.FindViewById<Android.Support.V7.Widget.SearchView> (Resource.Id.SearchFragmentSearch);
			_searchView.QueryHint = textFieldHint;
			_searchView.SetOnQueryTextListener (this);
			_searchView.SetOnSuggestionListener (this);

			((EditText)_searchView.FindViewById (Resource.Id.search_src_text)).CustomSelectionActionModeCallback 
			= this;

			if (mSuggestionsAdapter == null) {
				MatrixCursor cursor = new MatrixCursor (COLUMNS);
				mSuggestionsAdapter = new SuggestionsAdapter (Android.App.Application.Context, cursor);
			}

			_searchView.SuggestionsAdapter = mSuggestionsAdapter;


			if(!topLine) {
				var topLineView = _view.FindViewById<FrameLayout> (Resource.Id.SearchFragmentTopLine);
				topLineView.Visibility = topLine ? ViewStates.Visible : ViewStates.Gone;
			}

			if(!serachLine) {
				var view = _view.FindViewById<FrameLayout> (Resource.Id.SearchFragmentDragLine);
				view.Visibility = serachLine ? ViewStates.Visible : ViewStates.Gone;
			}
            if (!bottomline)
            {
                var view = _view.FindViewById<FrameLayout>(Resource.Id.SearchFragmentBottomLine);
                view.Visibility = serachLine ? ViewStates.Visible : ViewStates.Gone;
            }

			int searchEditTextId = Resource.Id.search_src_text; // for AppCompat

			// get AutoCompleteTextView from SearchView
			AutoCompleteTextView searchEditText = (AutoCompleteTextView) _searchView.FindViewById(searchEditTextId);
			View dropDownAnchor = _searchView.FindViewById(searchEditText.DropDownAnchor);
			dropDownAnchor.AddOnLayoutChangeListener (this);

			searchEditText.LongClick += (object sender, View.LongClickEventArgs e) => {
				SetChoicedStop (null); 
			};

			/*
			 * Init the view model and set the last chocied stop
			 */
			_stoppingPlaceViewModel.Init (searchIdName);
			if (_stoppingPlaceViewModel.HasLatestCLickeStop ()) {

				_choicedStop = _stoppingPlaceViewModel.GetLatestCLickeStop ();
				_ignoreAutoCompleteTextChange = true;

				_searchView.SetQuery (_choicedStop.Name, true);
				Events.Trigger ("initWithStop", _choicedStop, 100);

			}

			var btnPos = _view.FindViewById<ImageButton> (Resource.Id.btnPos);

			btnPos.Click +=	 (object sender, EventArgs e) => {
				Events.Trigger ("locationClick", null);
				return;
			};

		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			_view = inflater.Inflate(Resource.Layout.SearchFragment, container, false);

			return _view;
		}

		private void ClearChoicedStop() {
			_choicedStop = null;
			_stoppingPlaceViewModel.clearLastChociedStop();
		}

		public StoppingPlace GetChoicedStop() {
			return _choicedStop;
		}

		public void SetChoicedStop(StoppingPlace place) {

			if (place == null) {
				_searchView.SetQuery ("", false);
				ClearChoicedStop ();
				return;
			}
			_stoppingPlaceViewModel. SaveLastChoicedStop(place);
			_choicedStop = place;
			_ignoreAutoCompleteTextChange = true;
			_searchView.SetQuery (_choicedStop.Name, true);

		}

		private async void getFromStops(string text){

			try {
				var result = (HttpResult)await _stoppingPlaceViewModel.GetStops(text);
				if (result.Result) {
					var list = ((StoppingPlaces)result.Data).GetList ();
					populatFromAutoComplete (list);;
				} 
			}
			catch(Exception) {
				stops.util.ToastManager.show ("Gick inte att hämta resor");
			}
		}	

		void populatFromAutoComplete (List<StoppingPlace> fromList) {

			MatrixCursor cursor = new MatrixCursor (COLUMNS);
			Converter<string, Java.Lang.Object> func = s => new Java.Lang.String (s);

			_stopServerList = fromList;

			foreach (var stoppingPlace in fromList) {
				cursor.AddRow (Array.ConvertAll<string,Java.Lang.Object> (new string[] { stoppingPlace.Id, stoppingPlace.Name }, func));
			}

			mSuggestionsAdapter.SwapCursor (cursor);

		}


		public bool OnQueryTextChange (string newText)
		{
			if (newText.Length > 3 && !_ignoreAutoCompleteTextChange) {
				getFromStops (newText);
			} else if(newText.Length < 3) {
				ClearChoicedStop ();
			}
			_ignoreAutoCompleteTextChange = false;

			return true;
		}

        //private bool _hasIgnoredFirstOnQueryTextSubmit = false;
		public bool OnQueryTextSubmit (string query)
		{
            //if(!_hasIgnoredFirstOnQueryTextSubmit) {
            //    _hasIgnoredFirstOnQueryTextSubmit = true;
            //    return true;
            //}

            //Events.Trigger("OnQueryTextSubmit");

            try
            {
                InputMethodManager inputManager = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);

                inputManager.HideSoftInputFromWindow(Activity.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);

            }
            catch {
                
            }


			return true;
		}

		public bool OnSuggestionClick (int position)
		{
			var place = _stopServerList[position];

			_ignoreAutoCompleteTextChange = true;
			_searchView.SetQuery (place.Name, true);
			_stoppingPlaceViewModel. SaveLastChoicedStop(place);
			_choicedStop = place;

			return true;
		}

		public bool OnSuggestionSelect (int position)
		{
			return true;
		}

		private class SuggestionsAdapter : Android.Support.V4.Widget.CursorAdapter
		{

			public SuggestionsAdapter (Context context, ICursor c) 
				: base (context, c, 0)
			{
			}


			public override View NewView (Context context, ICursor cursor, ViewGroup parent)
			{
				LayoutInflater inflater = LayoutInflater.From (context);
				View v = inflater.Inflate (Resource.Layout.Spinner, parent, false);
				return v;
			}

			public override void BindView (View view, Context context, ICursor cursor)
			{
				TextView tv = (TextView)view;
				int textIndex = cursor.GetColumnIndex (SearchManager.SuggestColumnText1);
				tv.Text = cursor.GetString (textIndex);
			}
		}

		public bool OnActionItemClicked (ActionMode mode, IMenuItem item)
		{
			return false;
		}

		public bool OnCreateActionMode (ActionMode mode, IMenu menu)
		{
			SetChoicedStop (null); 
			return false;
		}

		public void OnDestroyActionMode (ActionMode mode)
		{

		}

		public bool OnPrepareActionMode (ActionMode mode, IMenu menu)
		{
			return false;
		}

		public void OnLayoutChange (View v, int left, int top, int right, int bottom, int oldLeft, int oldTop, int oldRight, int oldBottom)
		{



		
		}
	}
}