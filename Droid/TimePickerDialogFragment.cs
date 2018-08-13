
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
	public class TimePickerDialogFragment : PickerBase
	{
		private readonly Context _context;
		private  DateTime _date;
		private readonly Android.App.TimePickerDialog.IOnTimeSetListener _listener;

		public TimePickerDialogFragment(Context context, DateTime date, Android.App.TimePickerDialog.IOnTimeSetListener listener  )
		{
			_context = context;
			_date = date;
			_listener = listener;
		}

		public override Dialog OnCreateDialog(Bundle savedState)
		{
			var dialog = new Android.App.TimePickerDialog(_context, _listener, _date.Hour, _date.Minute, true);
			return dialog;
		}
	}
}
