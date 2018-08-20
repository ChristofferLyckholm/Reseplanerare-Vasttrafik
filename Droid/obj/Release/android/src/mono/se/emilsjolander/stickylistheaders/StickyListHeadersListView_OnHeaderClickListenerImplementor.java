package mono.se.emilsjolander.stickylistheaders;


public class StickyListHeadersListView_OnHeaderClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		se.emilsjolander.stickylistheaders.StickyListHeadersListView.OnHeaderClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onHeaderClick:(Lse/emilsjolander/stickylistheaders/StickyListHeadersListView;Landroid/view/View;IJZ)V:GetOnHeaderClick_Lse_emilsjolander_stickylistheaders_StickyListHeadersListView_Landroid_view_View_IJZHandler:StickyListHeaders.StickyListHeadersListView/IOnHeaderClickListenerInvoker, StickyListHeaders\n" +
			"";
		mono.android.Runtime.register ("StickyListHeaders.StickyListHeadersListView+IOnHeaderClickListenerImplementor, StickyListHeaders", StickyListHeadersListView_OnHeaderClickListenerImplementor.class, __md_methods);
	}


	public StickyListHeadersListView_OnHeaderClickListenerImplementor ()
	{
		super ();
		if (getClass () == StickyListHeadersListView_OnHeaderClickListenerImplementor.class)
			mono.android.TypeManager.Activate ("StickyListHeaders.StickyListHeadersListView+IOnHeaderClickListenerImplementor, StickyListHeaders", "", this, new java.lang.Object[] {  });
	}


	public void onHeaderClick (se.emilsjolander.stickylistheaders.StickyListHeadersListView p0, android.view.View p1, int p2, long p3, boolean p4)
	{
		n_onHeaderClick (p0, p1, p2, p3, p4);
	}

	private native void n_onHeaderClick (se.emilsjolander.stickylistheaders.StickyListHeadersListView p0, android.view.View p1, int p2, long p3, boolean p4);

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
