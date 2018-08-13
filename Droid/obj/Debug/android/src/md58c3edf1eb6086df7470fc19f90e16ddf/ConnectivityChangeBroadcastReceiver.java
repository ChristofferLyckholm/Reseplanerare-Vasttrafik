package md58c3edf1eb6086df7470fc19f90e16ddf;


public class ConnectivityChangeBroadcastReceiver
	extends android.content.BroadcastReceiver
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onReceive:(Landroid/content/Context;Landroid/content/Intent;)V:GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler\n" +
			"";
		mono.android.Runtime.register ("Cheesebaron.MvxPlugins.Connectivity.Droid.ConnectivityChangeBroadcastReceiver, Cheesebaron.MvxPlugins.Connectivity.Droid", ConnectivityChangeBroadcastReceiver.class, __md_methods);
	}


	public ConnectivityChangeBroadcastReceiver ()
	{
		super ();
		if (getClass () == ConnectivityChangeBroadcastReceiver.class)
			mono.android.TypeManager.Activate ("Cheesebaron.MvxPlugins.Connectivity.Droid.ConnectivityChangeBroadcastReceiver, Cheesebaron.MvxPlugins.Connectivity.Droid", "", this, new java.lang.Object[] {  });
	}


	public void onReceive (android.content.Context p0, android.content.Intent p1)
	{
		n_onReceive (p0, p1);
	}

	private native void n_onReceive (android.content.Context p0, android.content.Intent p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
