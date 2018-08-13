package md5439bf82a87c6f0fba97a3d8a4fa67c7f;


public class JazzyViewPager
	extends android.support.v4.view.ViewPager
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_addView:(Landroid/view/View;)V:GetAddView_Landroid_view_View_Handler\n" +
			"n_addView:(Landroid/view/View;I)V:GetAddView_Landroid_view_View_IHandler\n" +
			"n_addView:(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V:GetAddView_Landroid_view_View_Landroid_view_ViewGroup_LayoutParams_Handler\n" +
			"n_addView:(Landroid/view/View;II)V:GetAddView_Landroid_view_View_IIHandler\n" +
			"n_addView:(Landroid/view/View;ILandroid/view/ViewGroup$LayoutParams;)V:GetAddView_Landroid_view_View_ILandroid_view_ViewGroup_LayoutParams_Handler\n" +
			"n_onInterceptTouchEvent:(Landroid/view/MotionEvent;)Z:GetOnInterceptTouchEvent_Landroid_view_MotionEvent_Handler\n" +
			"n_onPageScrolled:(IFI)V:GetOnPageScrolled_IFIHandler\n" +
			"";
		mono.android.Runtime.register ("Jazzy.JazzyViewPager, JazzyViewPager", JazzyViewPager.class, __md_methods);
	}


	public JazzyViewPager (android.content.Context p0)
	{
		super (p0);
		if (getClass () == JazzyViewPager.class)
			mono.android.TypeManager.Activate ("Jazzy.JazzyViewPager, JazzyViewPager", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public JazzyViewPager (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == JazzyViewPager.class)
			mono.android.TypeManager.Activate ("Jazzy.JazzyViewPager, JazzyViewPager", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public void addView (android.view.View p0)
	{
		n_addView (p0);
	}

	private native void n_addView (android.view.View p0);


	public void addView (android.view.View p0, int p1)
	{
		n_addView (p0, p1);
	}

	private native void n_addView (android.view.View p0, int p1);


	public void addView (android.view.View p0, android.view.ViewGroup.LayoutParams p1)
	{
		n_addView (p0, p1);
	}

	private native void n_addView (android.view.View p0, android.view.ViewGroup.LayoutParams p1);


	public void addView (android.view.View p0, int p1, int p2)
	{
		n_addView (p0, p1, p2);
	}

	private native void n_addView (android.view.View p0, int p1, int p2);


	public void addView (android.view.View p0, int p1, android.view.ViewGroup.LayoutParams p2)
	{
		n_addView (p0, p1, p2);
	}

	private native void n_addView (android.view.View p0, int p1, android.view.ViewGroup.LayoutParams p2);


	public boolean onInterceptTouchEvent (android.view.MotionEvent p0)
	{
		return n_onInterceptTouchEvent (p0);
	}

	private native boolean n_onInterceptTouchEvent (android.view.MotionEvent p0);


	public void onPageScrolled (int p0, float p1, int p2)
	{
		n_onPageScrolled (p0, p1, p2);
	}

	private native void n_onPageScrolled (int p0, float p1, int p2);

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
