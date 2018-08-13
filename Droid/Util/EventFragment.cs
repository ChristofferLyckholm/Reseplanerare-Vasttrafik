
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Core.Util;

namespace stops
{
	[Activity (Label = "EventFragment")]			
	public class EventFragment : Android.Support.V4.App.Fragment
	{
		public EventClass Events = new EventClass();
	}
}

