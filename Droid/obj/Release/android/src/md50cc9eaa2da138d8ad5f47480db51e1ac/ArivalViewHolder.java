package md50cc9eaa2da138d8ad5f47480db51e1ac;


public class ArivalViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("stops.ArivalViewHolder, stops", ArivalViewHolder.class, __md_methods);
	}


	public ArivalViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == ArivalViewHolder.class)
			mono.android.TypeManager.Activate ("stops.ArivalViewHolder, stops", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
