using Android.App;
using Android.Widget;
using Android.OS;
using Stops.Setup;
using stops;
using Ninject;
using Core.ViewModels;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Content;
using stops.util;
using System.Threading;
using System.Threading.Tasks;
using Android.Support.V4.View;
using Android.Views;
using com.refractored;
using Android.Graphics;
using Android.Support.V4.App;
using Android.Graphics.Drawables;
using Android.Util;
using System;
using Android.Content.PM;
using System.Collections.Generic;
using stops.Util;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Provider;
using Core.Util;
using Core.Favorites;

namespace Stops
{
	//Activity.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);

    [Activity (Label = "Reseplaneraren", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/Theme.Main", MainLauncher = false, Icon = "@mipmap/icon", WindowSoftInputMode = SoftInput.StateHidden|SoftInput.AdjustPan)]
	public class MainActivity : AppCompatActivity, IOnTabReselectedListener
	{
		private MainMapFragment _mapFragment;

		protected int LayoutResource {
			get {
				return stops.Resource.Layout.Main;
			}
		}

        private LazyLoadFragment prevFrag = null;

		private MyPagerAdapter adapter;
		private Drawable oldBackground = null;
		private int currentColor;
		private ViewPager pager;
		private PagerSlidingTabStrip tabs;

		public override void OnBackPressed ()
		{

			SplashActivity.Activity.Finish ();
			Finish();
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (stops.Resource.Layout.Main);

			InitAppAndToolbar ();
			InitTabs ();
			InitMap ();

			LazyLoad ();
		}

		protected override void OnResume ()
		{
			base.OnResume ();

			if (_mapFragment != null) {
				_mapFragment.EnableAPI ();
			}

			GetPosAsync ();

		}

		protected override void OnPause ()
		{
			base.OnPause ();

			if (_mapFragment != null) {
				_mapFragment.DisableAPI ();
			}

		}

		async void GetPosAsync() {

			await Task.Delay (500);
			_mapFragment.GetPos();
			await Task.Delay (500);
			_mapFragment.GetPos();
			await Task.Delay (1000);
			_mapFragment.GetPos ();
			await Task.Delay (500);
			_mapFragment.GetPos ();
		}

		async void LazyLoad() {

			await Task.Delay (400);
			stops.Util.LazyLoad.Load (new stops.MapFragment ());


//			await Task.Delay (2500);
//			var bar = Snackbar.Make (FindViewById<ViewGroup> (stops.Resource.Id.mapCointatiner), 
//				          "", Snackbar.LengthIndefinite);
//					
//			bar.SetText("Tips!\n ");
//			bar.Show();
//			await Task.Delay (1500);
//			bar.SetText("Håll inne klicket för att rensa, t.ex hållplats-/datum-fälltet");
//			bar.SetAction("Visa !nte igen", (view) => {
//
//			});
//			bar.Show();
//			bar.SetActionTextColor(Color.ParseColor("#2B9CF5"));
//
//			await Task.Delay (4000);
//			bar.Dismiss ();

		}


		void InitAppAndToolbar() {

			/*
			 * Set up support action bar and title and subtitle.
			 */
            this.Title = "Reseplaneraren Väst";      
			SupportActionBar.SetDisplayShowHomeEnabled(true);
			SupportActionBar.SetIcon(stops.Resource.Drawable.smallicon);
			tabs = FindViewById<PagerSlidingTabStrip> (stops.Resource.Id.tabs);

			ChangeColor (Color.ParseColor("#2B9CF5"));



			/*
			 * Hides toolbar on low res phones so we can show map instead.
			 */
			var metrics = Util.PxToDP(Resources.DisplayMetrics.HeightPixels);
			if (metrics < 500) {
				SupportActionBar.Hide();
			}
				
			/*
			 * Set up recent apps title
			 */
			Bitmap bm = BitmapFactory.DecodeResource (Resources, stops.Resource.Id.icon);
			Android.App.ActivityManager.TaskDescription taskDesc = 			  
                new Android.App.ActivityManager.TaskDescription("Reseplaneraren Väst", bm, Color.White);
			this.SetTaskDescription (taskDesc);

		}

        void InitTabs()
        {

            adapter = new MyPagerAdapter(SupportFragmentManager);


            pager = new CustomPager(this);


            pager.Id = 0x1000;
            tabs = FindViewById<PagerSlidingTabStrip>(stops.Resource.Id.tabs);
            var pagerWrapper = FindViewById<FrameLayout>(stops.Resource.Id.viewPagerWrapper);

            pagerWrapper.AddView(pager);
            pager.Adapter = adapter;
            tabs.SetViewPager(pager);

            var pageMargin = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 4, Resources.DisplayMetrics);
            pager.PageMargin = pageMargin;
            tabs.OnTabReselectedListener = this;

            // pager.SetCurrentItem(2, true);

            pager.OffscreenPageLimit = 55;


            FavoriteBridge.Instance.OnEvent += (source, e) => {
                

                var evt = (EventClassArgs)e;
                if (evt.Name == "FavoriteSelected")
                {
                    var data = (Favorite)evt.Data;

                    if (data.fromName != null && data.toName != null) {
                        pager.SetCurrentItem(1, true);
                    }
                    else if(data.fromName != null){
                        pager.SetCurrentItem(0, true);
                    }
                }


            };


			pager.PageSelected += (object sender, ViewPager.PageSelectedEventArgs e) => {
				var pos = e.Position;

				if(prevFrag == null) {
					prevFrag = ((LazyLoadFragment)adapter.Frags[0]);
				}

				var currentFrag = ((LazyLoadFragment)adapter.Frags[pos]);

				/*
				 * Animate frag slide in on viewpager change. So we avoid the 
				 */
				if(currentFrag.Loaded) {
					currentFrag.Frag.SlideAnimateIn();
				}

				/*
				 * Makes the prev layout ready for slide in animation 
				 */
				if(prevFrag != null && prevFrag.Loaded) {
					prevFrag.Frag.RestoreViewPosition();
				}

				((LazyLoadFragment)adapter.Frags[pos]).Create();
				if(prevFrag != null) {
					prevFrag.ClearFocus();
				}
				if(((LazyLoadFragment)adapter.Frags[pos]).Loaded ){
					((LazyLoadFragment)adapter.Frags[pos]).SetFocus();
				}
				prevFrag = ((LazyLoadFragment)adapter.Frags[pos]);
			};

		}

        protected override void OnResumeFragments()
        {

            foreach (var lazyLoadFragment in adapter.Frags) {

                ((LazyLoadFragment)lazyLoadFragment).OnResumeCustom();
            }

            base.OnResumeFragments();
        }
		void InitMap() {

			_mapFragment = new MainMapFragment ().Init (FindViewById<ViewGroup>(stops.Resource.Id.mapCointatiner));


			Android.Support.V4.App.FragmentTransaction transaction = SupportFragmentManager.BeginTransaction ();
			transaction.SetTransition(Android.Support.V4.App.FragmentTransaction.TransitFragmentFade);
			transaction.Add(stops.Resource.Id.mapCointatiner, _mapFragment);
			transaction.Commit();

		}


		public override bool OnCreateOptionsMenu (IMenu menu)
		{

			MenuInflater.Inflate(stops.Resource.Menu.MyMenu, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			Intent intent;
			try { //http://findmyfbid.com
				intent = new Intent (Intent.ActionView, Android.Net.Uri.Parse ("fb://page/" + "244485372277349"));
				StartActivity (intent);
			}
			catch(Exception) {
				intent = new Intent (Intent.ActionView, Android.Net.Uri.Parse ("https://www.facebook.com/" + "244485372277349"));
				StartActivity (intent);
			}


			return base.OnOptionsItemSelected (item);
		}

	

		#region IOnTabReselectedListener implementation
		public void OnTabReselected (int position)
		{

		}
		#endregion

		private void ChangeColor(Color newColor) {
			tabs.SetBackgroundColor(newColor);

			// change ActionBar color just if an ActionBar is available
			Drawable colorDrawable = new ColorDrawable(newColor);
			Drawable bottomDrawable = new ColorDrawable(Resources.GetColor(Android.Resource.Color.Transparent));
			LayerDrawable ld = new LayerDrawable(new Drawable[]{colorDrawable, bottomDrawable});
			if (oldBackground == null) {
				SupportActionBar.SetBackgroundDrawable(ld);
			} else {
				TransitionDrawable td = new TransitionDrawable(new Drawable[]{oldBackground, ld});
				SupportActionBar.SetBackgroundDrawable(td);
				td.StartTransition(200);
			}

			oldBackground = ld;
			currentColor = newColor;
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{
			base.OnSaveInstanceState (outState);
			outState.PutInt ("currentColor", currentColor);
		}

		protected override void OnRestoreInstanceState (Bundle savedInstanceState)
		{
			base.OnRestoreInstanceState (savedInstanceState);
			currentColor = savedInstanceState.GetInt ("currentColor");
			ChangeColor (new Color (currentColor));
		}

//		public void OnMapReady (GoogleMap googleMap)
//		{
//			_supportMapFragment.Map.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(57.724215, 11.970159), 12.0f));
//			//57.724215, 11.970159
//		}
	}

	/**
 * Created by vihaan on 1/9/15.
 */
	public class CustomPager :  Android.Support.V4.View.ViewPager, Android.Support.V4.View.ViewPager.IOnPageChangeListener {

		private View mCurrentView;

		public CustomPager (Context context) :base(context){
			

        }
		public CustomPager (Context context, Android.Util.IAttributeSet attrs):base(context,attrs){
			
		}



		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{
			if (mCurrentView == null) {
				base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
				return;
			}
			int height = 0;
			mCurrentView.Measure(widthMeasureSpec, MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified));
			int h = mCurrentView.MeasuredHeight;
			if (h > height) height = h;
			heightMeasureSpec = MeasureSpec.MakeMeasureSpec(height, MeasureSpecMode.Exactly);

			base.OnMeasure(widthMeasureSpec, heightMeasureSpec);




		}


		public void measureCurrentView(View currentView) {
			mCurrentView = currentView;
			RequestLayout();
		}

		public int measureFragment(View view) {
			if (view == null)
				return 0;

			view.Measure(0, 0);
			return view.MeasuredHeight;
		}

        public void OnPageScrollStateChanged(int state)
        {
            
        }

        private int oldIndex = 0;
        void IOnPageChangeListener.OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            base.OnPageScrolled(position,positionOffset, positionOffsetPixels);

            if(position != oldIndex) {
                MapBridge.Instance.Trigger("ViewPagerPosition",position,0);
            }

            oldIndex = position;
        }

        public void OnPageSelected(int position)
        {
       
        }
    }

	public class MyPagerAdapter : FragmentPagerAdapter{
		private  string[] Titles = {"Nästa tur", "Favoriter"};

		private int mCurrentPosition = -1;
		public List<Android.Support.V4.App.Fragment> Frags = new List<Android.Support.V4.App.Fragment> (4);

		public MyPagerAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
		{
		}

	
		public override void SetPrimaryItem (ViewGroup container, int position, Java.Lang.Object objectValue)
		{
			base.SetPrimaryItem (container, position, objectValue);
			if (position != mCurrentPosition) {
				Android.Support.V4.App.Fragment fragment = (Android.Support.V4.App.Fragment) objectValue;
				CustomPager pager = (CustomPager) container;
				if (fragment != null && fragment.View != null) {
					mCurrentPosition = position;
					pager.measureCurrentView(fragment.View);
				}
			}
		}
	

		public override Java.Lang.ICharSequence GetPageTitleFormatted (int position)
		{
			return new Java.Lang.String (Titles [position]);
		}
		#region implemented abstract members of PagerAdapter
		public override int Count {
			get {
				return Titles.Length;
			}
		}
		#endregion
		#region implemented abstract members of FragmentPagerAdapter
		public override Android.Support.V4.App.Fragment GetItem (int position)
		{
			/*
			 * IMPORTANT: This is the point. We create a RootFragment acting as
			 * a container for other fragments
			 */ 
			Android.Support.V4.App.Fragment frag;
			switch (position) {
				case 0:
					frag = new RootStoppingPlaceFragment ();
					break;
				case 1:
                    frag = new RootWidgetFragment();
					break;
			 default:
					frag = new RootWidgetFragment ();
					break;
//				default :
//					frag = new RootSettingFragment ();
//					break;
			}
			Frags.Add (frag);
			return frag;
		}
		#endregion
	}





}


