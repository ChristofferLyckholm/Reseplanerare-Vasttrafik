package md50cc9eaa2da138d8ad5f47480db51e1ac;


public class StoppingPlacesFragment_MyShadowBuilder
	extends android.view.View.DragShadowBuilder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onProvideShadowMetrics:(Landroid/graphics/Point;Landroid/graphics/Point;)V:GetOnProvideShadowMetrics_Landroid_graphics_Point_Landroid_graphics_Point_Handler\n" +
			"n_onDrawShadow:(Landroid/graphics/Canvas;)V:GetOnDrawShadow_Landroid_graphics_Canvas_Handler\n" +
			"";
		mono.android.Runtime.register ("stops.StoppingPlacesFragment+MyShadowBuilder, stops", StoppingPlacesFragment_MyShadowBuilder.class, __md_methods);
	}


	public StoppingPlacesFragment_MyShadowBuilder (android.view.View p0)
	{
		super (p0);
		if (getClass () == StoppingPlacesFragment_MyShadowBuilder.class)
			mono.android.TypeManager.Activate ("stops.StoppingPlacesFragment+MyShadowBuilder, stops", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onProvideShadowMetrics (android.graphics.Point p0, android.graphics.Point p1)
	{
		n_onProvideShadowMetrics (p0, p1);
	}

	private native void n_onProvideShadowMetrics (android.graphics.Point p0, android.graphics.Point p1);


	public void onDrawShadow (android.graphics.Canvas p0)
	{
		n_onDrawShadow (p0);
	}

	private native void n_onDrawShadow (android.graphics.Canvas p0);

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
