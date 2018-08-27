package md50cc9eaa2da138d8ad5f47480db51e1ac;


public class EventFragment
	extends android.support.v4.app.Fragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("stops.EventFragment, stops", EventFragment.class, __md_methods);
	}


	public EventFragment ()
	{
		super ();
		if (getClass () == EventFragment.class)
			mono.android.TypeManager.Activate ("stops.EventFragment, stops", "", this, new java.lang.Object[] {  });
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
