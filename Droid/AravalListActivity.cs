
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
    [Activity(Label = "Sökresultat")]
    public class AravalListActivity : AppCompatActivity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            string fromID = Intent.GetStringExtra("fromId");
            string fromDate = Intent.GetStringExtra("fromDate");

            DateTime enteredDate = DateTime.Parse(fromDate);

            var AravalListFragment = new AravalListFragment();
            AravalListFragment.InitArivalList(fromID, null, enteredDate);
            AravalListFragment.Events.OnEvent += (object source, EventArgs e) => {

                var evt = (EventClassArgs)e;
                if (evt.Name == "finish")
                {
                    Finish();
                }
            };


            SetContentView(Resource.Layout.AravalListActivity);


            Android.Support.V4.App.FragmentTransaction transaction = SupportFragmentManager.BeginTransaction();
            transaction.SetTransition(Android.Support.V4.App.FragmentTransaction.TransitFragmentFade);
            transaction.Add(stops.Resource.Id.rootAravalListActivity, AravalListFragment);
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
