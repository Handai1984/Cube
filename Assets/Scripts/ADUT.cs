using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class ADUT : MonoBehaviour {
	

	#if UNITY_IPHONE  
	[DllImport("__Internal")]
	private static extern bool ADAction(string title, string msg);  
	#endif
	/*
	 *  SendAction("hasLoadedInterstitialAD" , "" )
	 * 
	 *  SendAction("hasVideoADWithType" , "0" );
	 * 
	 * 
	 */


	public bool SendAction(string title, string msg)
	{
		#if UNITY_IPHONE
		print("one");
		return ADAction( title,  msg);
		#else
		print("two");
		return false;
		#endif
	}


}
