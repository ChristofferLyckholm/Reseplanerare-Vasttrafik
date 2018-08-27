package md50cc9eaa2da138d8ad5f47480db51e1ac;


public class SearchFragment
	extends md50cc9eaa2da138d8ad5f47480db51e1ac.EventFragment
	implements
		mono.android.IGCUserPeer,
		android.support.v7.widget.SearchView.OnQueryTextListener,
		android.support.v7.widget.SearchView.OnSuggestionListener,
		android.view.ActionMode.Callback,
		android.view.View.OnLayoutChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onCreateView:(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;:GetOnCreateView_Landroid_view_LayoutInflater_Landroid_view_ViewGroup_Landroid_os_Bundle_Handler\n" +
			"n_onQueryTextChange:(Ljava/lang/String;)Z:GetOnQueryTextChange_Ljava_lang_String_Handler:Android.Support.V7.Widget.SearchView/IOnQueryTextListenerInvoker, Xamarin.Android.Support.v7.AppCompat\n" +
			"n_onQueryTextSubmit:(Ljava/lang/String;)Z:GetOnQueryTextSubmit_Ljava_lang_String_Handler:Android.Support.V7.Widget.SearchView/IOnQueryTextListenerInvoker, Xamarin.Android.Support.v7.AppCompat\n" +
			"n_onSuggestionClick:(I)Z:GetOnSuggestionClick_IHandler:Android.Support.V7.Widget.SearchView/IOnSuggestionListenerInvoker, Xamarin.Android.Support.v7.AppCompat\n" +
			"n_onSuggestionSelect:(I)Z:GetOnSuggestionSelect_IHandler:Android.Support.V7.Widget.SearchView/IOnSuggestionListenerInvoker, Xamarin.Android.Support.v7.AppCompat\n" +
			"n_onActionItemClicked:(Landroid/view/ActionMode;Landroid/view/MenuItem;)Z:GetOnActionItemClicked_Landroid_view_ActionMode_Landroid_view_MenuItem_Handler:Android.Views.ActionMode/ICallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onCreateActionMode:(Landroid/view/ActionMode;Landroid/view/Menu;)Z:GetOnCreateActionMode_Landroid_view_ActionMode_Landroid_view_Menu_Handler:Android.Views.ActionMode/ICallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onDestroyActionMode:(Landroid/view/ActionMode;)V:GetOnDestroyActionMode_Landroid_view_ActionMode_Handler:Android.Views.ActionMode/ICallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onPrepareActionMode:(Landroid/view/ActionMode;Landroid/view/Menu;)Z:GetOnPrepareActionMode_Landroid_view_ActionMode_Landroid_view_Menu_Handler:Android.Views.ActionMode/ICallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onLayoutChange:(Landroid/view/View;IIIIIIII)V:GetOnLayoutChange_Landroid_view_View_IIIIIIIIHandler:Android.Views.View/IOnLayoutChangeListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("stops.SearchFragment, stops", SearchFragment.class, __md_methods);
	}


	public SearchFragment ()
	{
		super ();
		if (getClass () == SearchFragment.class)
			mono.android.TypeManager.Activate ("stops.SearchFragment, stops", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public android.view.View onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2)
	{
		return n_onCreateView (p0, p1, p2);
	}

	private native android.view.View n_onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2);


	public boolean onQueryTextChange (java.lang.String p0)
	{
		return n_onQueryTextChange (p0);
	}

	private native boolean n_onQueryTextChange (java.lang.String p0);


	public boolean onQueryTextSubmit (java.lang.String p0)
	{
		return n_onQueryTextSubmit (p0);
	}

	private native boolean n_onQueryTextSubmit (java.lang.String p0);


	public boolean onSuggestionClick (int p0)
	{
		return n_onSuggestionClick (p0);
	}

	private native boolean n_onSuggestionClick (int p0);


	public boolean onSuggestionSelect (int p0)
	{
		return n_onSuggestionSelect (p0);
	}

	private native boolean n_onSuggestionSelect (int p0);


	public boolean onActionItemClicked (android.view.ActionMode p0, android.view.MenuItem p1)
	{
		return n_onActionItemClicked (p0, p1);
	}

	private native boolean n_onActionItemClicked (android.view.ActionMode p0, android.view.MenuItem p1);


	public boolean onCreateActionMode (android.view.ActionMode p0, android.view.Menu p1)
	{
		return n_onCreateActionMode (p0, p1);
	}

	private native boolean n_onCreateActionMode (android.view.ActionMode p0, android.view.Menu p1);


	public void onDestroyActionMode (android.view.ActionMode p0)
	{
		n_onDestroyActionMode (p0);
	}

	private native void n_onDestroyActionMode (android.view.ActionMode p0);


	public boolean onPrepareActionMode (android.view.ActionMode p0, android.view.Menu p1)
	{
		return n_onPrepareActionMode (p0, p1);
	}

	private native boolean n_onPrepareActionMode (android.view.ActionMode p0, android.view.Menu p1);


	public void onLayoutChange (android.view.View p0, int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8)
	{
		n_onLayoutChange (p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	private native void n_onLayoutChange (android.view.View p0, int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8);

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
