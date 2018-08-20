package md50cc9eaa2da138d8ad5f47480db51e1ac;


public class PhotoViewHolderMultiLine
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("stops.PhotoViewHolderMultiLine, stops", PhotoViewHolderMultiLine.class, __md_methods);
	}


	public PhotoViewHolderMultiLine (android.view.View p0)
	{
		super (p0);
		if (getClass () == PhotoViewHolderMultiLine.class)
			mono.android.TypeManager.Activate ("stops.PhotoViewHolderMultiLine, stops", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
