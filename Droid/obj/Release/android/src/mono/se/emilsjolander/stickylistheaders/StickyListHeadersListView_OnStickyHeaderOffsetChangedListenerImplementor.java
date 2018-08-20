package mono.se.emilsjolander.stickylistheaders;


public class StickyListHeadersListView_OnStickyHeaderOffsetChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		se.emilsjolander.stickylistheaders.StickyListHeadersListView.OnStickyHeaderOffsetChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onStickyHeaderOffsetChanged:(Lse/emilsjolander/stickylistheaders/StickyListHeadersListView;Landroid/view/View;I)V:GetOnStickyHeaderOffsetChanged_Lse_emilsjolander_stickylistheaders_StickyListHeadersListView_Landroid_view_View_IHandler:StickyListHeaders.StickyListHeadersListView/IOnStickyHeaderOffsetChangedListenerInvoker, StickyListHeaders\n" +
			"";
		mono.android.Runtime.register ("StickyListHeaders.StickyListHeadersListView+IOnStickyHeaderOffsetChangedListenerImplementor, StickyListHeaders", StickyListHeadersListView_OnStickyHeaderOffsetChangedListenerImplementor.class, __md_methods);
	}


	public StickyListHeadersListView_OnStickyHeaderOffsetChangedListenerImplementor ()
	{
		super ();
		if (getClass () == StickyListHeadersListView_OnStickyHeaderOffsetChangedListenerImplementor.class)
			mono.android.TypeManager.Activate ("StickyListHeaders.StickyListHeadersListView+IOnStickyHeaderOffsetChangedListenerImplementor, StickyListHeaders", "", this, new java.lang.Object[] {  });
	}


	public void onStickyHeaderOffsetChanged (se.emilsjolander.stickylistheaders.StickyListHeadersListView p0, android.view.View p1, int p2)
	{
		n_onStickyHeaderOffsetChanged (p0, p1, p2);
	}

	private native void n_onStickyHeaderOffsetChanged (se.emilsjolander.stickylistheaders.StickyListHeadersListView p0, android.view.View p1, int p2);

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
