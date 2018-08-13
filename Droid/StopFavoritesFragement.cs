
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
using Core.Util;
using Ninject;

namespace stops
{
    public class StopFavoritesFragement : EventFragment
    {

      
        private Core.ViewModels.FavoritesViewModel _favoritesViewModel;
        private SearchFragment _searchFragment;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view =  inflater.Inflate(Resource.Layout.StopFavoritFragment, container, false);

            _favoritesViewModel = Stops.Setup.IoC.Container.Get<Core.ViewModels.FavoritesViewModel>();
            _favoritesViewModel.Load();

            _searchFragment = (SearchFragment)ChildFragmentManager.FindFragmentById(stops.Resource.Id.stopfavoritfragmentfrom);

            var mainMapFragmentButtonSearch = view.FindViewById<Button>(Resource.Id.mainMapFragmentButtonSearch);
            mainMapFragmentButtonSearch.Click += (sender, e) => {
                HandleSearchClicked();
            };

            _searchFragment.Init(null, "Sök på hållplats", false, false);
            _searchFragment.Events.OnEvent += (object source, EventArgs e) => {

                var evt = (EventClassArgs)e;

                //if (evt.Name == "OnQueryTextSubmit")
                //{
                //    HandleSearchClicked();
                //}

            };

            return view;
        }

        private void HandleSearchClicked()
        {
            if(_searchFragment.GetChoicedStop() == null) {
                return;
            }
            _favoritesViewModel.Add(new Core.Favorites.Favorite()
            {
                fromId = _searchFragment.GetChoicedStop().Id,
                fromName = _searchFragment.GetChoicedStop().Name,
                fromLat = _searchFragment.GetChoicedStop().Lat,
                fromLon = _searchFragment.GetChoicedStop().Lon
            });;

            this.Events.Trigger("finish");
        }
    }
}
