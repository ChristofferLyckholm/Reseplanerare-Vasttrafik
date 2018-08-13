
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
using Ninject;

namespace stops
{
    public class StopsFavoritesFragement : EventFragment
    {
        private Core.ViewModels.FavoritesViewModel _favoritesViewModel;
        private SearchFragment _searchFromFragment;
        private SearchFragment _searchToFragment;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.StopsFavoritFragment, container, false);

            _favoritesViewModel = Stops.Setup.IoC.Container.Get<Core.ViewModels.FavoritesViewModel>();
            _favoritesViewModel.Load();


            _searchFromFragment = (SearchFragment)ChildFragmentManager.FindFragmentById(stops.Resource.Id.stopsfavoritfragmentfrom);
            _searchToFragment = (SearchFragment)ChildFragmentManager.FindFragmentById(stops.Resource.Id.stopsfavoritfragmentto);
            _searchFromFragment.Init(null, "Sök från hållplats", false, false);
            _searchToFragment.Init(null, "Sök till hållplats", false, false);
           

            var mainMapFragmentButtonSearch = view.FindViewById<Button>(Resource.Id.mainMapFragmentButtonSearch);
            mainMapFragmentButtonSearch.Click += (sender, e) => {
                HandleSearchClicked();
            };

            return view;
        }

        private void HandleSearchClicked()
        {
            if (_searchToFragment.GetChoicedStop() == null && _searchFromFragment.GetChoicedStop() == null)
            {
                return;
            }
            _favoritesViewModel.Add(new Core.Favorites.Favorite()
            {
                fromId = _searchFromFragment.GetChoicedStop().Id,
                fromName = _searchFromFragment.GetChoicedStop().Name,
                fromLat = _searchFromFragment.GetChoicedStop().Lat,
                fromLon = _searchFromFragment.GetChoicedStop().Lon,

                toId = _searchToFragment.GetChoicedStop().Id,
                toName = _searchToFragment.GetChoicedStop().Name,
                toLat = _searchToFragment.GetChoicedStop().Lat,
                toLon = _searchToFragment.GetChoicedStop().Lon
            });

            this.Events.Trigger("finish");
        }
    }
}
