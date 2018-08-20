package md5439bf82a87c6f0fba97a3d8a4fa67c7f;


public class JazzyOutlineContainer_OutlineInterpolator
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.animation.Interpolator,
		android.animation.TimeInterpolator
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getInterpolation:(F)F:GetGetInterpolation_FHandler:Android.Views.Animations.IInterpolatorInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Jazzy.JazzyOutlineContainer+OutlineInterpolator, JazzyViewPager", JazzyOutlineContainer_OutlineInterpolator.class, __md_methods);
	}


	public JazzyOutlineContainer_OutlineInterpolator ()
	{
		super ();
		if (getClass () == JazzyOutlineContainer_OutlineInterpolator.class)
			mono.android.TypeManager.Activate ("Jazzy.JazzyOutlineContainer+OutlineInterpolator, JazzyViewPager", "", this, new java.lang.Object[] {  });
	}


	public float getInterpolation (float p0)
	{
		return n_getInterpolation (p0);
	}

	private native float n_getInterpolation (float p0);

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
