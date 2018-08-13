
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
using System.Threading.Tasks;

namespace stops
{
    public abstract class LazyLoadFragment : Android.Support.V4.App.Fragment
    {

        public LazyLoadFragment()
        {


        }

        public virtual int Delay
        {
            get { return 250; }
        }

        public bool Loaded
        {
            get;
            private set;
        } = false;

        // Area is a read-only property - only a get accessor is needed:
        public abstract FocuableFragment Frag
        {
            get;
            set;
        }




        public abstract int ResourceId
        {
            get;
        }

        public abstract void SetFragment();


        public void ClearFocus()
        {
            Frag.offFocus();
        }

        public void SetFocus()
        {
            SendOnFocus();

        }

        async void SendOnFocus()
        {
            await Task.Delay(500);
            Frag.onFocus();
        }

        public void OnResumeCustom() {

            if (Frag != null)
            {
                Frag.OnResumeCustom();
            }

        }
       

		public async void Create() {

			if (Loaded) {
				return;
			}
			Loaded = true;


			await Task.Delay (Delay);
			SetFragment ();
			//Android.Support.V4.App.Fragment
			Android.Support.V4.App.FragmentTransaction transaction = FragmentManager.BeginTransaction ();
			/*
			 * When this container fragment is created, we fill it with our first
			 * "real" fragment
			 */

			transaction.Replace(ResourceId, Frag);
			transaction.SetTransition (Android.Support.V4.App.FragmentTransaction.TransitFragmentFade);
			transaction.Commit();
		}

	}
}

