using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;


public class GoogleAD : MonoBehaviour {


		private BannerView bannerView;
		private InterstitialAd interstitial;
		
	public void Awake()
	{
		GoogleInite ();
	}

		public void Start()
		{
			//this.RequestBanner();
			//GoogleInite ();
		//this.RequestInterstitial ();
		}


	private void GoogleInite()
	{
		// These ad units are configured to always serve test ads.
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-3940256099942544/6300978111";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-3521489805936954~7342102274";
		#else
		string adUnitId = "unexpected_platform";
		#endif



		MobileAds.SetiOSAppPauseOnBackground(true);
		// Initialize the Google Mobile Ads SDK.
		MobileAds.Initialize(adUnitId);
	}

	private AdRequest CreateAdRequest()
	{
		return new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
			.AddTestDevice("e92f3f152355cf85f59a5c2d2d9a87e2")
			.Build();


	}

        public void RequestBanner()
		{
		// These ad units are configured to always serve test ads.
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-3940256099942544/6300978111";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-3521489805936954/2155457840";
		#else
		string adUnitId = "unexpected_platform";
		#endif
					
		if (this.bannerView != null)
		{
			this.bannerView.Destroy();
		}

		bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);


			// Create an empty ad request.
			//AdRequest request = new AdRequest.Builder().Build();

			// Load the banner with the request.
		this.bannerView.LoadAd(this.CreateAdRequest ());
		print ("我开始展示啦");
		}

	public  void  BannerDestory()
	{
		bannerView.Destroy ();
	}


	public void RequestInterstitial()
	{
		// These ad units are configured to always serve test ads.
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-3940256099942544/6300978111";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-3521489805936954/2037804703";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Clean up interstitial ad before creating a new one.
		if (this.interstitial != null)
		{
			this.interstitial.Destroy();
			print ("我来啦");
		}

		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(adUnitId);
		// Create an empty ad request.
		//AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		this.interstitial.LoadAd(this.CreateAdRequest ());
		print ("我是插屏");

	}

	public void ShowInterstitial()
	{
		if (this.interstitial.IsLoaded())
		{
			this.interstitial.Show();
			print("插屏显示");
		}
		else
		{
			MonoBehaviour.print("Interstitial is not ready yet");

		}
	}

}

