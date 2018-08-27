package md50cc9eaa2da138d8ad5f47480db51e1ac;


public abstract class FocuableFragment
	extends android.support.v4.app.Fragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("stops.FocuableFragment, stops", FocuableFragment.class, __md_methods);
	}


	public FocuableFragment ()
	{
		super ();
		if (getClass () == FocuableFragment.class)
			mono.android.TypeManager.Activate ("stops.FocuableFragment, stops", "", this, new java.lang.Object[] {  });
	}

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
