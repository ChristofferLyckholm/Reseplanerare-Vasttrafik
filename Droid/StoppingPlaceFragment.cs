
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
using stops.Adapters;
using Core.Travel;
using Ninject;
using Core.Http;
using Core.Util;
using Android.Support.V7.Widget;
using Core.ViewModels;
using stops.util;
using System.Threading.Tasks;
using Android.Animation;
using Core.Favorites;

namespace stops
{
	public class StoppingPlaceFragment : FocuableFragment
	{
		private View _view;
		private ViewGroup _root;

		private DateFragment _dateFragment;
		private SearchFragment _searchFragment;

		private StoppingPlaceViewModel _stoppingPlaceViewModel;
        private Core.ViewModels.FavoritesViewModel _favoritesViewModel;

        private bool listenToStoppingPlaceSelectedEvent = true;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		}

		public override void OnViewCreated (View view, Bundle savedInstanceState)
		{
			base.OnViewCreated (view, savedInstanceState);
		}
			
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			/*
			 * Load view model
			 */
 			_view = inflater.Inflate (Resource.Layout.StoppingPlaceCard, container, false);
			_stoppingPlaceViewModel = Stops.Setup.IoC.Container.Get<Core.ViewModels.StoppingPlaceViewModel> ();
            _favoritesViewModel = Stops.Setup.IoC.Container.Get<Core.ViewModels.FavoritesViewModel>();
            _favoritesViewModel.Load();

			/*
			 * Setup listview
			 */
			_root = (ViewGroup)_view.FindViewById (stops.Resource.Id.layout_container);
//
//			((FrameLayout)headerview.Parent).RemoveView(headerview);
//
			var recyclerView = _view.FindViewById<RecyclerView>(Resource.Id.recyclerview);
			recyclerView.SetLayoutManager(new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false));
			recyclerView.HasFixedSize = true;
			

			// items
			var elements = new List<string>(0);
			for (int i = 0; i < 0; i++)
			{
				elements.Add("row " + i);
			}
			recyclerView.SetAdapter(new SimpleRecyclerAdapter(Activity, elements));



            FavoriteBridge.Instance.OnEvent += (source, e) => {


                var evt = (EventClassArgs)e;
                if (evt.Name == "FavoriteSelected")
                {
                    var data = (Favorite)evt.Data;

                    if (data.fromName != null && data.toName == null)
                    {
                        _searchFragment.SetChoicedStop(new StoppingPlace()
                        {
                            Name = data.fromName,
                            Id = data.fromId,
                            Lat = data.fromLat,
                            Lon = data.fromLon
                        });
                    }
                }


            };

			/*
			 *  Handle stop search
			 */
			_searchFragment = (SearchFragment)ChildFragmentManager.FindFragmentById (stops.Resource.Id.stoppingPlaceCardSearchStop);
			_searchFragment.Events.OnEvent += (object source, EventArgs e) => {

				var evt = (EventClassArgs)e;
				if(evt.Name == "itemclicked" || evt.Name == "initWithStop") {
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


			/*
			 *  Handle Date for search
			 */
			_dateFragment = (DateFragment)ChildFragmentManager.FindFragmentById (stops.Resource.Id.stoppingPlaceCardDateStop);
			_dateFragment.Events.OnEvent += (object source, EventArgs e) => {

				var evt = (EventClassArgs)e;
				if(evt.Name == "Dateupdated") {
					//PerformSearch();
				}
			};          
			_dateFragment.Init ();
			_searchFragment.Init ("StoppingPlaceLastStop", "Hållplats", false, false, true);


            MapBridge.Instance.OnEvent += (source, e) =>
            {
                var evt = ((EventClassArgs)e);
                if(evt.Name == "stoppingplaceselected"){
                    var data = (StoppingPlace)evt.Data;

                    if(listenToStoppingPlaceSelectedEvent) {
                        _searchFragment.SetChoicedStop(data);   
                    }
                }
                if (evt.Name == "ViewPagerPosition")
                {
                    var data = (int)evt.Data;
                    if(data == 0 ) {
                        listenToStoppingPlaceSelectedEvent = true;
                    }
                    else {
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

        private void HandleSearchClicked()
        {
            if (listenToStoppingPlaceSelectedEvent && _searchFragment.GetChoicedStop() !=null){

                var second = new Intent(Application.Context, typeof(AravalListActivity));
                second.PutExtra("fromId", _searchFragment.GetChoicedStop().Id);
                second.PutExtra("fromDate", _dateFragment.GetDate().ToString());

                var date = _dateFragment.GetDate().ToString();
                StartActivity(second);

            }
        }

        private void ShowMapFragment() {

			var map = (ViewGroup)_view.FindViewById (Resource.Id.stoppingPlaceCardMap);
			var mapFragment = (MapFragment)this.ChildFragmentManager.FindFragmentById(Resource.Id.stoppingPlaceCardMap);


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

        #region handle favorit button

        public void InitFavoritButton() {

            _dateFragment.Events.OnEvent += (object source, EventArgs e) => {

                var evt = (EventClassArgs)e;
                if(evt.Name == "favoritebuttonclicked") {
                    var data = (bool)evt.Data;


                }
            };          

        }

        #endregion


        #region implemented abstract members of FocuableFragment

        public override void onFocus ()
		{
			_root.DescendantFocusability = DescendantFocusability.AfterDescendants;


		}

		public override void offFocus ()
		{
			_root.DescendantFocusability = DescendantFocusability.BlockDescendants;


		}

		#endregion




        public override void OnResumeCustom()
        {
            _favoritesViewModel.Load();
            _dateFragment.UpdateFavoriteDataModel();
        }


		private class SimpleRecyclerAdapter : RecyclerView.Adapter
		{
			private readonly Context context;
			private readonly List<string> elements;

			public SimpleRecyclerAdapter(Context context, List<string> elements)
			{
				this.context = context;
				this.elements = elements;
			}

			public override int ItemCount
			{
				get { return elements.Count; }
			}

			public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup viewGroup, int i)
			{
				LayoutInflater layoutInflater = LayoutInflater.From(context);
                View view = layoutInflater.Inflate(Resource.Layout.StopRow, viewGroup, false);
				return new SimpleViewHolder(this, elements, view);
			}

			public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int i)
			{
				var holder = (SimpleViewHolder) viewHolder;
				holder.SetText(elements[i]);
			}

			private class SimpleViewHolder : RecyclerView.ViewHolder
			{
				private readonly TextView textView;

				public SimpleViewHolder(SimpleRecyclerAdapter adapter, List<string> elements, View itemView)
					: base(itemView)
				{
					//textView = (TextView) itemView.FindViewById(Android.Resource.Id.Text1);

					//textView.Click += delegate
					//{
					//	var pos = AdapterPosition;
					//	elements.Insert(pos, "(+) row " + pos);
					//	adapter.NotifyItemInserted(pos);
					//};
					//textView.LongClick += delegate
					//{
					//	var pos = AdapterPosition;
					//	elements.RemoveAt(pos);
					//	adapter.NotifyItemRemoved(pos);
					//};
				}

				public void SetText(string text)
				{
					textView.Text = text;
				}
			}
		}
	}
}
