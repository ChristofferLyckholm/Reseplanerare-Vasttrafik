
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Core.Travel;
using Core.ViewModels;
using Ninject;

namespace stops
{
    public class AravalListFragment : EventFragment
    {
        private NextStopingPlacesViewModel _nextStopingPlacesViewModel;

        public override void OnDestroy()
        {
            base.OnDestroy();
            _destoryed = true;
           
        }

        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        ArivalAdapter mAdapter;

        private View _view;
        private bool _destoryed = false;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            _view = inflater.Inflate(Resource.Layout.AravalListFragment, container, false);

           

            return _view;
        }

        public void InitArivalList(string fromId, string toId, DateTime date) {

            _nextStopingPlacesViewModel = Stops.Setup.IoC.Container.Get<Core.ViewModels.NextStopingPlacesViewModel>();


            GetArivalsList(fromId, toId, date);

            UpdateList(fromId, toId, date);
        }


        private async void UpdateList(string fromId, string toId, DateTime date) {

            if(_destoryed) {
                return;
            }

            await Task.Delay(10 * 1000);


            GetArivalsList(fromId, toId, date);

            UpdateList(fromId, toId, date);
        } 

        async void GetArivalsList(string fromId, string toId, DateTime date)
        {

            StoppingTimeManager stoppingTimeManager = await _nextStopingPlacesViewModel.GetArivalsList(new Core.Travel.StoppingPlace()
            {
                Id = fromId
            }, null, date);

            mRecyclerView = _view.FindViewById<RecyclerView>(Resource.Id.recyclerView);

            // Plug in the linear layout manager:
            mLayoutManager = new LinearLayoutManager(Application.Context);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            // Plug in my adapter:
            mAdapter = new ArivalAdapter(stoppingTimeManager.GetList());



            mRecyclerView.SetAdapter(mAdapter);



        }
    }

    public class ArivalViewHolder : RecyclerView.ViewHolder
    {
        public TextView Header { get; private set; }
        public TextView Caption { get; private set; }
        public TextView Time { get; private set; }

        public ArivalViewHolder(View itemView, Action<int> listener, Action<int> listenerlongClick) : base(itemView)
        {
            // Locate and cache view references:
            Header = itemView.FindViewById<TextView>(Resource.Id.header);
            Caption = itemView.FindViewById<TextView>(Resource.Id.textView);
            Time = itemView.FindViewById<TextView>(Resource.Id.time);

            itemView.Click += (sender, e) => listener(base.LayoutPosition);
            itemView.LongClick += (sender, e) => listenerlongClick(base.LayoutPosition);
        }


    }

    public class ArivalAdapter : RecyclerView.Adapter
    {
        public List<TravelTime> mPhotoAlbum;

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



        public void UpdateList(List<TravelTime> photoAlbum)
        {
            mPhotoAlbum = photoAlbum;
        }

        public ArivalAdapter(List<TravelTime> photoAlbum)
        {
            mPhotoAlbum = photoAlbum;
        }

        public override int ItemCount
        {
            get { return mPhotoAlbum.Count(); }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ArivalViewHolder vh = holder as ArivalViewHolder;

            // Load the photo image resource from the photo album:
            vh.Header.Text = mPhotoAlbum[position].name;
            // vh.Time.Text = 

            DateTime startTime = mPhotoAlbum[position].Date;
            DateTime endTime = DateTime.Now;

            TimeSpan span = startTime.Subtract(endTime);
            var totalminues = Math.Round(span.TotalMinutes);

            vh.Time.Text = totalminues.ToString();  



            // Load the photo caption from the photo album:

            if (mPhotoAlbum[position].direction != null)
            {
                vh.Caption.Text = mPhotoAlbum[position].direction;
            }
        }


        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.FavoritsListView, parent, false);

            // Create a ViewHolder to hold view references inside the CardView:
            ArivalViewHolder vh = new ArivalViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }
    }
}
