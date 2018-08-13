using System;
using Core.Interfaces;
using System.Net.NetworkInformation;
using Android.Telephony;
using Android.App;
using Android.Content;
using Java.Util;
using Core.Util;

namespace Stops.Implementations
{


	public class Platform : IPlatform
		{



		public string GetId()
			{
			var telephonyDeviceID = string.Empty;
			var telephonySIMSerialNumber = string.Empty;
			TelephonyManager telephonyManager = (TelephonyManager)Application.Context.GetSystemService(Context.TelephonyService);
			if (telephonyManager != null)
			{
				if(!string.IsNullOrEmpty(telephonyManager.DeviceId))
					telephonyDeviceID = telephonyManager.DeviceId;
				if(!string.IsNullOrEmpty(telephonyManager.SimSerialNumber))
					telephonySIMSerialNumber = telephonyManager.SimSerialNumber;
			}

			var androidID = Android.Provider.Settings.Secure.GetString(Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
			var deviceUuid = new UUID(androidID.GetHashCode(), ((long)telephonyDeviceID.GetHashCode() << 32) | telephonySIMSerialNumber.GetHashCode());
			var deviceID = deviceUuid.ToString();

			return deviceID;
		}

	}
	
}

