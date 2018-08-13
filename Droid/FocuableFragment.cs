
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

namespace stops
{
	public abstract class FocuableFragment : Android.Support.V4.App.Fragment
	{
        public virtual void OnResumeCustom()
        {
        }

		public virtual void onFocus() {
		}
		public virtual void offFocus() {
		}

		public virtual void SlideAnimateIn() {


		}
		public virtual void RestoreViewPosition() {

		}

	}
}

