
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Core.Util;

namespace stops
{
    [Activity(Label = "Lägg till hållplats som favorit")]
    public class RootStopFavoritesActivty : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var favoritesFragment = new StopFavoritesFragement();

            SetContentView(Resource.Layout.RootStopFavorites);

            var _favoritFragment = new StopFavoritesFragement();
            _favoritFragment.Events.OnEvent += (object source, EventArgs e) => {

                var evt = (EventClassArgs)e;
                if (evt.Name == "finish") {
                    Finish();
                }
            };

            Android.Support.V4.App.FragmentTransaction transaction = SupportFragmentManager.BeginTransaction();
            transaction.SetTransition(Android.Support.V4.App.FragmentTransaction.TransitFragmentFade);
            transaction.Add(stops.Resource.Id.rootstopfavortesmain, _favoritFragment);
            transaction.Commit();



            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            // Create your application here
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}
