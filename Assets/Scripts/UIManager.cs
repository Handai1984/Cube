using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// UI管理器.
/// </summary>
public class UIManager : MonoBehaviour
{
#region 定义变量

	private GameObject m_StartUI;
	private GameObject m_GameUI;
	
	private UILabel m_ScoreLabel;
	private UILabel m_GemLabel;
	private UILabel m_GameScoreLabel;
	private UILabel m_GameGemLabel;
	
	private GameObject m_PlayButton;
	
	private GameObject m_Left;
	private GameObject m_Right;
	
	private PlayerController m_PlayerController;
	
	public GameObject[] ad;
	private List<GoogleAD> gad;
	#endregion
	//	private  ADUT ad;
	//
	//	void Awake()
	//	{
	//		ad = GameObject.Find("MapManager").GetComponent<ADUT>();
	//	}

	void Start ()
	{
		gad = new List<GoogleAD> ();
		m_StartUI = GameObject.Find ("Start_UI");
		m_GameUI = GameObject.Find ("Game_UI");

		m_ScoreLabel = GameObject.Find ("Score_Label").GetComponent<UILabel> ();
		m_GemLabel = GameObject.Find ("Gem_Label").GetComponent<UILabel> ();
		m_GameScoreLabel = GameObject.Find ("GameScoreLabel").GetComponent<UILabel> ();
		m_GameGemLabel = GameObject.Find ("GameGemLabel").GetComponent<UILabel> ();

		m_PlayerController = GameObject.Find ("cube_books").GetComponent<PlayerController> ();	

		m_PlayButton = GameObject.Find ("play_btn");
		UIEventListener.Get (m_PlayButton).onClick = PlayButtonClick;

		m_Left = GameObject.Find ("Left");
		UIEventListener.Get (m_Left).onClick = Left;

		m_Right = GameObject.Find ("Right");
		UIEventListener.Get (m_Right).onClick = Right;
		for (int i = 0; i < ad.Length; i++) {
			print (ad.Length);
			print (ad [i].name);
			GoogleAD temp = ad [i].GetComponent <GoogleAD> ();
			print (temp);
			gad.Add (temp);
			gad [i].RequestBanner ();
			gad [i].BannerHide ();
		}


		Init ();

//		ad.SendAction("showBannerAD", "");
		m_GameUI.SetActive (false);
	}


	private void Init ()
	{
		//PlayerPrefs.DeleteAll ();
		m_ScoreLabel.text = PlayerPrefs.GetInt ("score", 0) + "";
		m_GemLabel.text = PlayerPrefs.GetInt ("gem", 0) + "/100";
		m_GameScoreLabel.text = PlayerPrefs.GetInt ("score", 0) + "";	
		m_GameGemLabel.text = PlayerPrefs.GetInt ("gem", 0) + "/100";

		//初始化广告开始展示广告
		StartCoroutine ("StartBanner");

	}

	public void UpdateData (int score, int gem)
	{
		m_GemLabel.text = gem + "/100";
		m_GameScoreLabel.text = score.ToString ();
		m_GameGemLabel.text = gem + "/100";
	}

	private void PlayButtonClick (GameObject go)
	{
		Debug.Log ("游戏开始啦.");
		m_StartUI.SetActive (false);
		m_GameUI.SetActive (true);
		m_PlayerController.life = true;//游戏点击开始才可以运动
		m_PlayerController.StartGame ();
	}

	private void Left (GameObject go)
	{
		m_PlayerController.Left ();
	}

	private void Right (GameObject go)
	{
		m_PlayerController.Right ();
	}

	public void ResetUI ()
	{
		m_StartUI.SetActive (true);
		m_GameUI.SetActive (false);
		m_ScoreLabel.text = PlayerPrefs.GetInt ("score", 0) + "";

	}

	private  IEnumerator  StartBanner ()
	{
		for (int i = 0; i < gad.Count; i++) {
			gad [i].BannerShow ();
			print ("banner show" + i);
			yield return new WaitForSeconds (10);
			gad [i].BannerHide ();
			if (i == gad.Count - 1) {
				i = -1;
			}
		}
	}
}
