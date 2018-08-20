package se.emilsjolander.stickylistheaders;


public class ExpandableStickyListHeadersListView_AnimExecutor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		se.emilsjolander.stickylistheaders.ExpandableStickyListHeadersListView.IAnimationExecutor
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_executeAnim:(Landroid/view/View;I)V:GetExecuteAnim_Landroid_view_View_IHandler:StickyListHeaders.ExpandableStickyListHeadersListView/IAnimationExecutorInvoker, StickyListHeaders\n" +
			"";
		mono.android.Runtime.register ("StickyListHeaders.ExpandableStickyListHeadersListView+AnimExecutor, StickyListHeaders", ExpandableStickyListHeadersListView_AnimExecutor.class, __md_methods);
	}


	public ExpandableStickyListHeadersListView_AnimExecutor ()
	{
		super ();
		if (getClass () == ExpandableStickyListHeadersListView_AnimExecutor.class)
			mono.android.TypeManager.Activate ("StickyListHeaders.ExpandableStickyListHeadersListView+AnimExecutor, StickyListHeaders", "", this, new java.lang.Object[] {  });
	}


	public void executeAnim (android.view.View p0, int p1)
	{
		n_executeAnim (p0, p1);
	}

	private native void n_executeAnim (android.view.View p0, int p1);

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
