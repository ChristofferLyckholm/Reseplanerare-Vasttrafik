
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Core.Favorites;
using Ninject;
using stops.util;

namespace stops
{
	public class WidgetFragment : FocuableFragment
	{

        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        PhotoAlbumAdapter mAdapter;
       
        private Core.ViewModels.FavoritesViewModel _favoritesViewModel;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.NotificationCard, container, false);

            _favoritesViewModel = Stops.Setup.IoC.Container.Get<Core.ViewModels.FavoritesViewModel>();
            _favoritesViewModel.Load();


            Clans.Fab.FloatingActionMenu fabbutton = view.FindViewById<Clans.Fab.FloatingActionMenu>(Resource.Id.mainMapFragmentButtonFab);

            Clans.Fab.FloatingActionButton stop = view.FindViewById<Clans.Fab.FloatingActionButton>(Resource.Id.stop);

            stop.Click += (sender, e) =>
            {

                var second = new Intent(Application.Context, typeof(RootStopFavoritesActivty)); ;
                StartActivity(second);

                fabbutton.Close(true);

            };


            var layout = view.FindViewById<FrameLayout>(Resource.Id.notificationcardroot);


            var prms = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            prms.Height = MapBridge.Instance.windowHeight;
            layout.LayoutParameters = prms;




            /*
             * Create Recyckleview
             */
            // mPhotoAlbum = new PhotoAlbum();

            mRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);

            // Plug in the linear layout manager:
            mLayoutManager = new LinearLayoutManager(Application.Context);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            // Plug in my adapter:
            mAdapter = new PhotoAlbumAdapter(_favoritesViewModel.GetList());

            mAdapter.ItemClick += (sender, e) =>
            {
                int photoNum = e;
                FavoriteBridge.Instance.Trigger("FavoriteSelected", _favoritesViewModel.GetList()[photoNum]);

                //mAdapter.NotifyDataSetChanged();
            };

            mAdapter.LongClick += (object sender, int e) => {

                _favoritesViewModel.RemoveInList(e);
                _favoritesViewModel.Load();
                mAdapter.UpdateList(_favoritesViewModel.GetList());
                mAdapter.NotifyDataSetChanged();

            };
          

            mRecyclerView.SetAdapter(mAdapter);

            return view;
		}

        /*
         * Update the list
         */
        public override void OnResumeCustom()
        {
            _favoritesViewModel.Load();
            mAdapter.UpdateList(_favoritesViewModel.GetList());
            mAdapter.NotifyDataSetChanged();
        }

	}



    public class PhotoViewHolder : RecyclerView.ViewHolder
    {
       // public ImageView Image { get; private set; }
        public TextView Caption { get; private set; }

        public PhotoViewHolder(View itemView, Action<int> listener, Action<int> listenerlongClick) : base(itemView)
        {
            // Locate and cache view references:
            //Image = itemView.FindViewById<ImageView>(Resource.Id.header);
            Caption = itemView.FindViewById<TextView>(Resource.Id.textView);

            itemView.Click += (sender, e) => listener(base.LayoutPosition);
            itemView.LongClick += (sender, e) => listenerlongClick(base.LayoutPosition);
        }


    }

    public class PhotoViewHolderMultiLine : RecyclerView.ViewHolder
    {
        public TextView From { get; private set; }
        public TextView To { get; private set; }

        public PhotoViewHolderMultiLine(View itemView, Action<int> listener, Action<int> listenerlongClick) : base(itemView)
        {
            // Locate and cache view references:
            To = itemView.FindViewById<TextView>(Resource.Id.textViewTo);
            From = itemView.FindViewById<TextView>(Resource.Id.textViewFrom);

            itemView.Click += (sender, e) => listener(base.LayoutPosition);
            itemView.LongClick += (sender, e) => listenerlongClick(base.LayoutPosition);
        }


    }

    public class PhotoAlbumAdapter : RecyclerView.Adapter
    {
        public List<Favorite> mPhotoAlbum;

        public event EventHandler<int> ItemClick;


        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }


        public event EventHandler<int> LongClick;


        void OnLongClick(int position)
        {
            if (LongClick != null)
                LongClick(this, position);
        }



        public void UpdateList(List<Favorite> photoAlbum)
        {
            mPhotoAlbum = photoAlbum;
        }

        public PhotoAlbumAdapter(List<Favorite> photoAlbum)
        {
            mPhotoAlbum = photoAlbum;
        }

        public override int ItemCount
        {
            get { return mPhotoAlbum.Count(); }
        }

        public override int GetItemViewType(int position)
        {

            if (mPhotoAlbum[position].fromName != null && mPhotoAlbum[position].toName != null)
            {
                return 1;
            }
            return base.GetItemViewType(position);
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            

            // Load the photo image resource from the photo album:
            //vh.Image.SetImageResource(mPhotoAlbum[position].PhotoID);

            // Load the photo caption from the photo album:



            if(mPhotoAlbum[position].fromName !=null && mPhotoAlbum[position].toName != null) {
                PhotoViewHolderMultiLine vh = holder as PhotoViewHolderMultiLine;
                vh.To.Text = mPhotoAlbum[position].toName;
                vh.From.Text = mPhotoAlbum[position].fromName;
            }
            else if (mPhotoAlbum[position].fromName != null)
            {
                PhotoViewHolder vh = holder as PhotoViewHolder;
                vh.Caption.Text = mPhotoAlbum[position].fromName;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView;
            if(viewType == 0) {
                itemView = LayoutInflater.From(parent.Context).
                                         Inflate(Resource.Layout.FavoritesRow, parent, false);

                PhotoViewHolder vh = new PhotoViewHolder(itemView, OnClick, OnLongClick);

                return vh;
            }
            else {
                itemView = LayoutInflater.From(parent.Context).
                                         Inflate(Resource.Layout.FavoritsListMultiLIneView, parent, false);

                PhotoViewHolderMultiLine vh = new PhotoViewHolderMultiLine(itemView, OnClick, OnLongClick);

                return vh;
            }

            // Create a ViewHolder to hold view references inside the CardView:

        }
    }
}

