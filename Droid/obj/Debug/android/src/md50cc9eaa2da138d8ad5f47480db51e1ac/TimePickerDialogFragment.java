package md50cc9eaa2da138d8ad5f47480db51e1ac;


public class TimePickerDialogFragment
	extends md50cc9eaa2da138d8ad5f47480db51e1ac.PickerBase
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateDialog:(Landroid/os/Bundle;)Landroid/app/Dialog;:GetOnCreateDialog_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("stops.TimePickerDialogFragment, stops", TimePickerDialogFragment.class, __md_methods);
	}


	public TimePickerDialogFragment ()
	{
		super ();
		if (getClass () == TimePickerDialogFragment.class)
			mono.android.TypeManager.Activate ("stops.TimePickerDialogFragment, stops", "", this, new java.lang.Object[] {  });
	}


	public android.app.Dialog onCreateDialog (android.os.Bundle p0)
	{
		return n_onCreateDialog (p0);
	}

	private native android.app.Dialog n_onCreateDialog (android.os.Bundle p0);

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
