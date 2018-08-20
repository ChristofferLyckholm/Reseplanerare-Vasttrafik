package se.emilsjolander.stickylistheaders;


public class StickyListHeadersListView_IOnItemLongClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.widget.AdapterView.OnItemLongClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onItemLongClick:(Landroid/widget/AdapterView;Landroid/view/View;IJ)Z:GetOnItemLongClick_Landroid_widget_AdapterView_Landroid_view_View_IJHandler:Android.Widget.AdapterView/IOnItemLongClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("StickyListHeaders.StickyListHeadersListView+IOnItemLongClickListenerImplementor, StickyListHeaders", StickyListHeadersListView_IOnItemLongClickListenerImplementor.class, __md_methods);
	}


	public StickyListHeadersListView_IOnItemLongClickListenerImplementor ()
	{
		super ();
		if (getClass () == StickyListHeadersListView_IOnItemLongClickListenerImplementor.class)
			mono.android.TypeManager.Activate ("StickyListHeaders.StickyListHeadersListView+IOnItemLongClickListenerImplementor, StickyListHeaders", "", this, new java.lang.Object[] {  });
	}


	public boolean onItemLongClick (android.widget.AdapterView p0, android.view.View p1, int p2, long p3)
	{
		return n_onItemLongClick (p0, p1, p2, p3);
	}

	private native boolean n_onItemLongClick (android.widget.AdapterView p0, android.view.View p1, int p2, long p3);

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
