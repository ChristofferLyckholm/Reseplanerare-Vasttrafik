package md50cc9eaa2da138d8ad5f47480db51e1ac;


public class SearchFragment_SuggestionsAdapter
	extends android.support.v4.widget.CursorAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_newView:(Landroid/content/Context;Landroid/database/Cursor;Landroid/view/ViewGroup;)Landroid/view/View;:GetNewView_Landroid_content_Context_Landroid_database_Cursor_Landroid_view_ViewGroup_Handler\n" +
			"n_bindView:(Landroid/view/View;Landroid/content/Context;Landroid/database/Cursor;)V:GetBindView_Landroid_view_View_Landroid_content_Context_Landroid_database_Cursor_Handler\n" +
			"";
		mono.android.Runtime.register ("stops.SearchFragment+SuggestionsAdapter, stops", SearchFragment_SuggestionsAdapter.class, __md_methods);
	}


	public SearchFragment_SuggestionsAdapter (android.content.Context p0, android.database.Cursor p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == SearchFragment_SuggestionsAdapter.class)
			mono.android.TypeManager.Activate ("stops.SearchFragment+SuggestionsAdapter, stops", "Android.Content.Context, Mono.Android:Android.Database.ICursor, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public SearchFragment_SuggestionsAdapter (android.content.Context p0, android.database.Cursor p1, boolean p2)
	{
		super (p0, p1, p2);
		if (getClass () == SearchFragment_SuggestionsAdapter.class)
			mono.android.TypeManager.Activate ("stops.SearchFragment+SuggestionsAdapter, stops", "Android.Content.Context, Mono.Android:Android.Database.ICursor, Mono.Android:System.Boolean, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public SearchFragment_SuggestionsAdapter (android.content.Context p0, android.database.Cursor p1)
	{
		super (p0, p1);
		if (getClass () == SearchFragment_SuggestionsAdapter.class)
			mono.android.TypeManager.Activate ("stops.SearchFragment+SuggestionsAdapter, stops", "Android.Content.Context, Mono.Android:Android.Database.ICursor, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public android.view.View newView (android.content.Context p0, android.database.Cursor p1, android.view.ViewGroup p2)
	{
		return n_newView (p0, p1, p2);
	}

	private native android.view.View n_newView (android.content.Context p0, android.database.Cursor p1, android.view.ViewGroup p2);


	public void bindView (android.view.View p0, android.content.Context p1, android.database.Cursor p2)
	{
		n_bindView (p0, p1, p2);
	}

	private native void n_bindView (android.view.View p0, android.content.Context p1, android.database.Cursor p2);

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
