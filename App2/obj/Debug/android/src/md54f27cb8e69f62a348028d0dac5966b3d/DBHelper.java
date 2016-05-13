package md54f27cb8e69f62a348028d0dac5966b3d;


public class DBHelper
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("DBHelper, App2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DBHelper.class, __md_methods);
	}


	public DBHelper () throws java.lang.Throwable
	{
		super ();
		if (getClass () == DBHelper.class)
			mono.android.TypeManager.Activate ("DBHelper, App2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	java.util.ArrayList refList;
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
