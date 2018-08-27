package md58df294765f92b03de8924417d1780c2b;


public class MyPagerAdapter
	extends android.support.v4.app.FragmentPagerAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_setPrimaryItem:(Landroid/view/ViewGroup;ILjava/lang/Object;)V:GetSetPrimaryItem_Landroid_view_ViewGroup_ILjava_lang_Object_Handler\n" +
			"n_getPageTitle:(I)Ljava/lang/CharSequence;:GetGetPageTitle_IHandler\n" +
			"n_getCount:()I:GetGetCountHandler\n" +
			"n_getItem:(I)Landroid/support/v4/app/Fragment;:GetGetItem_IHandler\n" +
			"";
		mono.android.Runtime.register ("Stops.MyPagerAdapter, stops", MyPagerAdapter.class, __md_methods);
	}


	public MyPagerAdapter (android.support.v4.app.FragmentManager p0)
	{
		super (p0);
		if (getClass () == MyPagerAdapter.class)
			mono.android.TypeManager.Activate ("Stops.MyPagerAdapter, stops", "Android.Support.V4.App.FragmentManager, Xamarin.Android.Support.Fragment", this, new java.lang.Object[] { p0 });
	}


	public void setPrimaryItem (android.view.ViewGroup p0, int p1, java.lang.Object p2)
	{
		n_setPrimaryItem (p0, p1, p2);
	}

	private native void n_setPrimaryItem (android.view.ViewGroup p0, int p1, java.lang.Object p2);


	public java.lang.CharSequence getPageTitle (int p0)
	{
		return n_getPageTitle (p0);
	}

	private native java.lang.CharSequence n_getPageTitle (int p0);


	public int getCount ()
	{
		return n_getCount ();
	}

	private native int n_getCount ();


	public android.support.v4.app.Fragment getItem (int p0)
	{
		return n_getItem (p0);
	}

	private native android.support.v4.app.Fragment n_getItem (int p0);

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
