package md5439bf82a87c6f0fba97a3d8a4fa67c7f;


public class JazzyOutlineContainer
	extends android.widget.FrameLayout
	implements
		mono.android.IGCUserPeer,
		android.graphics.drawable.Animatable
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_dispatchDraw:(Landroid/graphics/Canvas;)V:GetDispatchDraw_Landroid_graphics_Canvas_Handler\n" +
			"n_isRunning:()Z:GetIsRunningHandler:Android.Graphics.Drawables.IAnimatableInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_start:()V:GetStartHandler:Android.Graphics.Drawables.IAnimatableInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_stop:()V:GetStopHandler:Android.Graphics.Drawables.IAnimatableInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Jazzy.JazzyOutlineContainer, JazzyViewPager", JazzyOutlineContainer.class, __md_methods);
	}


	public JazzyOutlineContainer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == JazzyOutlineContainer.class)
			mono.android.TypeManager.Activate ("Jazzy.JazzyOutlineContainer, JazzyViewPager", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public JazzyOutlineContainer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == JazzyOutlineContainer.class)
			mono.android.TypeManager.Activate ("Jazzy.JazzyOutlineContainer, JazzyViewPager", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public JazzyOutlineContainer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == JazzyOutlineContainer.class)
			mono.android.TypeManager.Activate ("Jazzy.JazzyOutlineContainer, JazzyViewPager", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public JazzyOutlineContainer (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == JazzyOutlineContainer.class)
			mono.android.TypeManager.Activate ("Jazzy.JazzyOutlineContainer, JazzyViewPager", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public void dispatchDraw (android.graphics.Canvas p0)
	{
		n_dispatchDraw (p0);
	}

	private native void n_dispatchDraw (android.graphics.Canvas p0);


	public boolean isRunning ()
	{
		return n_isRunning ();
	}

	private native boolean n_isRunning ();


	public void start ()
	{
		n_start ();
	}

	private native void n_start ();


	public void stop ()
	{
		n_stop ();
	}

	private native void n_stop ();

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
