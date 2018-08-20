package mono.se.emilsjolander.stickylistheaders;


public class StickyListHeadersListView_OnStickyHeaderChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		se.emilsjolander.stickylistheaders.StickyListHeadersListView.OnStickyHeaderChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onStickyHeaderChanged:(Lse/emilsjolander/stickylistheaders/StickyListHeadersListView;Landroid/view/View;IJ)V:GetOnStickyHeaderChanged_Lse_emilsjolander_stickylistheaders_StickyListHeadersListView_Landroid_view_View_IJHandler:StickyListHeaders.StickyListHeadersListView/IOnStickyHeaderChangedListenerInvoker, StickyListHeaders\n" +
			"";
		mono.android.Runtime.register ("StickyListHeaders.StickyListHeadersListView+IOnStickyHeaderChangedListenerImplementor, StickyListHeaders", StickyListHeadersListView_OnStickyHeaderChangedListenerImplementor.class, __md_methods);
	}


	public StickyListHeadersListView_OnStickyHeaderChangedListenerImplementor ()
	{
		super ();
		if (getClass () == StickyListHeadersListView_OnStickyHeaderChangedListenerImplementor.class)
			mono.android.TypeManager.Activate ("StickyListHeaders.StickyListHeadersListView+IOnStickyHeaderChangedListenerImplementor, StickyListHeaders", "", this, new java.lang.Object[] {  });
	}


	public void onStickyHeaderChanged (se.emilsjolander.stickylistheaders.StickyListHeadersListView p0, android.view.View p1, int p2, long p3)
	{
		n_onStickyHeaderChanged (p0, p1, p2, p3);
	}

	private native void n_onStickyHeaderChanged (se.emilsjolander.stickylistheaders.StickyListHeadersListView p0, android.view.View p1, int p2, long p3);

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
