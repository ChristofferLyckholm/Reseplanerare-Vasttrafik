
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
using Ninject;
using Core.ViewModels;
using Core.Util;
using Core.Travel;
using stops.util;
using Android.Graphics;
using System.Threading.Tasks;
using Android.Animation;
using Core.Favorites;

namespace stops
{		
	public class StoppingPlacesFragment : FocuableFragment
	{
		private View _view;
		private ViewGroup _root;

		private ValueAnimator _HeightAnimator;



		private StoppingPlacesViewModel _stoppingPlacesViewModel;
        private Core.ViewModels.FavoritesViewModel _favoritesViewModel;

		private SearchFragment _searchFromFragment;
		private SearchFragment _searchToFragment;
		private DateFragment _dateFragment;

		private bool listenToStoppingPlaceSelectedEvent = true;

        private int stoppingPlaceInt = 0;
		
        #region implemented abstract members of FocuableFragment

		public override void onFocus ()
		{
			_root.DescendantFocusability = DescendantFocusability.AfterDescendants;
		}

		public override void offFocus ()
		{
			_root.DescendantFocusability = DescendantFocusability.BlockDescendants;
		}

		public override void RestoreViewPosition ()
		{	
			if (_HeightAnimator != null) {
				_HeightAnimator.Cancel ();
			}
			var prms = _root.LayoutParameters;
			prms.Height = Util.Util.dpToPixel (113); //170  standard - 113 just two rows visisble
			_root.LayoutParameters = prms;
		}

		public override void SlideAnimateIn ()
		{
			postSponSlideIn ();



		}

		public async void postSponSlideIn() {
			await Task.Delay (500);

		 	_HeightAnimator = Util.Util.AnimateHeight(_root,Util.Util.dpToPixel (113), Util.Util.dpToPixel (170));
			_HeightAnimator.SetDuration (150);
			_HeightAnimator.Start ();



		}

	

		#endregion

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			_view = inflater.Inflate(Resource.Layout.StoppingPlacesCard, container, false);
			_stoppingPlacesViewModel = Stops.Setup.IoC.Container.Get<Core.ViewModels.StoppingPlacesViewModel> ();
            _favoritesViewModel = Stops.Setup.IoC.Container.Get<Core.ViewModels.FavoritesViewModel>();
            _favoritesViewModel.Load();

			_root = (ViewGroup)_view.FindViewById (stops.Resource.Id.stoppingPlacesCardRoot);
			 
		    
            FavoriteBridge.Instance.OnEvent += (source, e) => {


                var evt = (EventClassArgs)e;
                if (evt.Name == "FavoriteSelected")
                {
                    var data = (Favorite)evt.Data;

                    if (data.fromName != null && data.toName != null)
                    {
                        _searchFromFragment.SetChoicedStop(new StoppingPlace()
                        {
                            Name = data.fromName,
                            Id = data.fromId,
                            Lat = data.fromLat,
                            Lon = data.fromLon
                        });

                        _searchToFragment.SetChoicedStop(new StoppingPlace()
                        {
                            Name = data.toName,
                            Id = data.toId,
                            Lat = data.toLat,
                            Lon = data.toLon
                        });
                    }
                }


            };

			/*
			 *  Handle Date for search
			 */
			_dateFragment = (DateFragment)ChildFragmentManager.FindFragmentById (stops.Resource.Id.stoppingPlacesCardDate);
			_dateFragment.Events.OnEvent += (object source, EventArgs e) => {

				var evt = (EventClassArgs)e;
                if(evt.Name == "favoritebuttonclicked") {
                    HandleAddFavorite();
				}

			};
			_dateFragment.Init(true);


			/*
			 *  Handle stop search from
			 */
			_searchFromFragment = (SearchFragment)ChildFragmentManager.FindFragmentById (stops.Resource.Id.stoppingPlacesCardSearchFromStops);
			_searchFromFragment.Events.OnEvent += (object source, EventArgs e) => {

				var evt = (EventClassArgs)e;
				if(evt.Name == "itemclicked") {
					//PerformSearch();
				}
				else if(evt.Name == "locationClick") {
					ShowMapFragment();
				}
                if (evt.Name == "OnQueryTextSubmit")
                {
                    HandleSearchClicked();
                }

			};
			_searchFromFragment.Init ("StoppingPlacesFromLastStop", "Från", false, true);

			/*
			 *  Handle stop search to
			 */
			_searchToFragment = (SearchFragment)ChildFragmentManager.FindFragmentById (stops.Resource.Id.stoppingPlacesCardSearchToStops);
			_searchToFragment.Events.OnEvent += (object source, EventArgs e) => {

				var evt = (EventClassArgs)e;
				if(evt.Name == "itemclicked") {
					//PerformSearch();
				}
				else if(evt.Name == "locationClick") {
					//ShowMapFragment();
				}
                if (evt.Name == "OnQueryTextSubmit")
                {
                    HandleSearchClicked();
                }

			};
			_searchToFragment.Init ("StoppingPlacesToLastStop", "Till", true,true);

			/*
			 * Handle arrival spinner
			 */
//			var spinner = (Spinner)_view.FindViewById (stops.Resource.Id.stoppingPlacesCardDepartureSpinner);
//			spinner.ItemSelected += (sender, e) => {
//				
//			};
//			var adapter = ArrayAdapter.CreateFromResource (
//				Android.App.Application.Context, Resource.Array.when_array,Resource.Layout.SpinnerText);
//
//			adapter.SetDropDownViewResource (Resource.Layout.Spinner);
//			spinner.Adapter = adapter;
//

			SetUpDragAndDrop ();

			//PerformSearch ();


			MapBridge.Instance.OnEvent += (source, e) =>
			{
				var evt = ((EventClassArgs)e);
				if (evt.Name == "stoppingplaceselected")
				{
					var data = (StoppingPlace)evt.Data;

					if (listenToStoppingPlaceSelectedEvent)
					{
                        if (stoppingPlaceInt == 0)
                            _searchFromFragment.SetChoicedStop(data);
                        else 
                            _searchToFragment.SetChoicedStop(data);

                        stoppingPlaceInt++;
                        if(stoppingPlaceInt > 1) {
                            stoppingPlaceInt = 0;
                        }
					}
				}
				if (evt.Name == "ViewPagerPosition")
				{
					var data = (int)evt.Data;
					if (data == 1)
					{
						listenToStoppingPlaceSelectedEvent = true;
					}
					else
					{
						listenToStoppingPlaceSelectedEvent = false;
					}
				}
                if (evt.Name == "SearchButtonClicked")
                {
                    HandleSearchClicked();
                }
			};

			return _view;
		}

        private void HandleAddFavorite()
        {
            throw new NotImplementedException();
        }

        private void HandleSearchClicked()
        {
            if (listenToStoppingPlaceSelectedEvent && _searchFromFragment.GetChoicedStop() !=null && _searchToFragment.GetChoicedStop()!=null) {
                var second = new Intent(Application.Context, typeof(AravalListActivity));
                second.PutExtra("fromId", _searchFromFragment.GetChoicedStop().Id);
                second.PutExtra("toId", _searchToFragment.GetChoicedStop().Id);
                StartActivity(second);
            }
        }

        private void ShowMapFragment() {

			var map = (View)_view.FindViewById (Resource.Id.stoppingPlacesCardMap);
			var mapFragment = (MapFragment)this.ChildFragmentManager.FindFragmentById(Resource.Id.stoppingPlacesCardMap);


			mapFragment.Show ();
			Util.Util.ToogleView (map, true,delegate { 

				mapFragment.Init();
				mapFragment.StartSerach();

				mapFragment.Events.OnEvent += (object source, EventArgs e) => {

					var evt = (EventClassArgs)e;
					if(evt.Name == "close") {

						Util.Util.ToogleView (map, false,delegate { 
							mapFragment.Hide();

							return 1;
						});
					}

				};


				return 1;
			});
		}





		void SetUpDragAndDrop() {


			/*
			 * Init Drag
			 */
			var dragA = _view.FindViewById<FrameLayout> (Resource.Id.stoppingPlacesCardDragOne);
			var dragB = _view.FindViewById<FrameLayout> (Resource.Id.stoppingPlacesCardDragTwo);

			var dropA = _view.FindViewById<FrameLayout> (Resource.Id.stoppingPlacesCardDropOne);
			var dropB = _view.FindViewById<FrameLayout> (Resource.Id.stoppingPlacesCardDropTwo);


			var dragAShadow = new MyShadowBuilder (dragA);
			var dragBShadow = new MyShadowBuilder (dragB);

			/*
			 * Handle Drag
			 */
			dragA.Touch += (object sender, View.TouchEventArgs e) => {

				var evt = e.Event.Action;
				var point = new Point((int) e.Event.RawX, (int)e.Event.RawY);

				switch(evt){
					case MotionEventActions.Down:
						dragAShadow.lastTouch = point;
						dragA.StartDrag (ClipData.NewPlainText ("from", "A"), dragAShadow,null,0);
						FillLayout(dropB);
						offFocus();
						HideLayoutInvisible(dragA);
						break;
					case MotionEventActions.Move:
						dragAShadow.lastTouch = point;
						break;
					case MotionEventActions.Up :
						RestoreDragLayout(dragA,dragB,dragAShadow,dragBShadow);
						break;
				}


			};

			dragB.Touch += (object sender, View.TouchEventArgs e) => {

				var evt = e.Event.Action;
				var point = new Point((int) e.Event.RawX, (int)e.Event.RawY);

				switch(evt){
					case MotionEventActions.Down:
						dragBShadow.lastTouch = point;
						dragB.StartDrag (ClipData.NewPlainText ("from", "B"), dragBShadow,null,0);
						FillLayout(dropA);
						offFocus();
						HideLayoutInvisible(dragB);
						break;
					case MotionEventActions.Move:
						dragBShadow.lastTouch = point;
						break;
					case MotionEventActions.Up :
						RestoreDragLayout(dragA,dragB,dragAShadow,dragBShadow);
						break;
				}
			};






			_view.Drag += (object sender, View.DragEventArgs e) => {
				var evt = e.Event;
				switch (evt.Action) {
				case DragAction.Drop:
					RestoreDragLayout(dragA,dragB,dragAShadow,dragBShadow);
					e.Handled = true;
					break;
				}
			};

			/*
			 * Handle Drop
			 */
			//var dragB = _view.FindViewById<FrameLayout> (Resource.Id.stoppingPlacesCardDragTwo);
			dropA.Drag += (object sender, View.DragEventArgs e) => {

				var evt = e.Event;
				switch (evt.Action) {
				case DragAction.Drop:
					e.Handled = true;

					if(e.Event.ClipData.GetItemAt (0).Text == "B") {

						ChangeDirection();
						HideLayout(dropA);
						FillLayout(dragB);
					}

					break;
				case DragAction.Ended :
					RestoreDragLayout(dragA,dragB,dragAShadow,dragBShadow);
					break;
				}

			};

			dropB.Drag += (object sender, View.DragEventArgs e) => {

				var evt = e.Event;
				switch (evt.Action) {
				case DragAction.Drop:
					e.Handled = true;

					if(e.Event.ClipData.GetItemAt (0).Text == "A") {

						ChangeDirection();
						HideLayout(dropB);
						FillLayout(dragA);
					}

					break;
				case DragAction.Ended :
					RestoreDragLayout(dragA,dragB,dragAShadow,dragBShadow);
					break;
				}

			};


		}

		async void RestoreDragLayout(ViewGroup dragA, ViewGroup dragB, MyShadowBuilder dragShadowA, MyShadowBuilder dragShadowB) {
			FillLayout(dragA, 100);
			FillLayout(dragB, 100);
			FillLine ();
			await Task.Delay (500);
			onFocus ();
		}

		async void FillLine() {
			await Task.Delay (100);
			var line = _view.FindViewById<FrameLayout> (Resource.Id.stoppingPlacesCardStopline);
			line.Visibility = ViewStates.Visible;
		}
			
		void HideLayoutInvisible(ViewGroup view) {
			view.Visibility = ViewStates.Invisible;
			var line = _view.FindViewById<FrameLayout> (Resource.Id.stoppingPlacesCardStopline);
			line.Visibility = ViewStates.Invisible;
		}

		void HideLayout(ViewGroup view) {
			view.Visibility = ViewStates.Gone;
		}

		void ChangeDirection() {

			var choicedStopTo = _searchToFragment.GetChoicedStop();
			var choicedStopFrom = _searchFromFragment.GetChoicedStop();
			_searchToFragment.SetChoicedStop(choicedStopFrom);
			_searchFromFragment.SetChoicedStop(choicedStopTo);

		}

		async void FillLayout(ViewGroup view, int delay = 0) {

			if (delay >0) {
				await Task.Delay (delay);
				FillLayout (view);
			}
			view.Visibility = ViewStates.Visible;
		}

        public override void OnResumeCustom()
        {
            _favoritesViewModel.Load();
            _dateFragment.UpdateFavoriteDataModel();
        }
		

		class MyShadowBuilder : View.DragShadowBuilder
		{

			public Point lastTouch;

			public MyShadowBuilder (View baseView) : base (baseView)
			{
			}
				
			public override void OnProvideShadowMetrics (Point shadowSize, Point shadowTouchPoint)
			{
				base.OnProvideShadowMetrics (shadowSize, shadowTouchPoint);

				if (lastTouch!= null) {
					shadowTouchPoint.X = lastTouch.X;
				}
			}

			public override void OnDrawShadow (Canvas canvas)
			{
				base.OnDrawShadow (canvas);
			}
		}
	}
}