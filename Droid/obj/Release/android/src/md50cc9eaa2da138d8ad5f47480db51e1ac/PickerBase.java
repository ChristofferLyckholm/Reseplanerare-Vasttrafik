package md50cc9eaa2da138d8ad5f47480db51e1ac;


public class PickerBase
	extends android.support.v4.app.DialogFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("stops.PickerBase, stops", PickerBase.class, __md_methods);
	}


	public PickerBase ()
	{
		super ();
		if (getClass () == PickerBase.class)
			mono.android.TypeManager.Activate ("stops.PickerBase, stops", "", this, new java.lang.Object[] {  });
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
