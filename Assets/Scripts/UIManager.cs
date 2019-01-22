using UnityEngine;
using System.Collections;

/// <summary>
/// UI管理器.
/// </summary>
public class UIManager : MonoBehaviour {

    private GameObject m_StartUI;
    private GameObject m_GameUI;
	private GameObject m_storeUI;//商店节点
	private GameObject m_gemUI;//金币节点
	private GameObject m_store_offUI;//商店关闭按钮
    private UILabel m_ScoreLabel;
    private UILabel m_GemLabel;
    private UILabel m_GameScoreLabel;
    private UILabel m_GameGemLabel;

    private GameObject m_PlayButton;

    private GameObject m_Left;
    private GameObject m_Right;

    private PlayerController m_PlayerController;

	void Start () {
        m_StartUI = GameObject.Find("Start_UI");
        m_GameUI = GameObject.Find("Game_UI");
		m_storeUI = GameObject.Find ("store");//查找商店
		m_gemUI = GameObject.Find("gem_button");//查找金币节点
		m_store_offUI = GameObject.Find("sotre_cut");//查找商店关闭按钮
        m_ScoreLabel = GameObject.Find("Score_Label").GetComponent<UILabel>();
        m_GemLabel = GameObject.Find("Gem_Label").GetComponent<UILabel>();
        m_GameScoreLabel = GameObject.Find("GameScoreLabel").GetComponent<UILabel>();
        m_GameGemLabel = GameObject.Find("GameGemLabel").GetComponent<UILabel>();

        m_PlayerController = GameObject.Find("cube_books").GetComponent<PlayerController>();	

        m_PlayButton = GameObject.Find("play_btn");
        UIEventListener.Get(m_PlayButton).onClick = PlayButtonClick;

        m_Left = GameObject.Find("Left");
        UIEventListener.Get(m_Left).onClick = Left;

        m_Right = GameObject.Find("Right");
        UIEventListener.Get(m_Right).onClick = Right;
		m_storeUI.SetActive (false);
        Init();

        m_GameUI.SetActive(false);
	}

	//商店开启
	public void StoreOpen(){
		
		m_storeUI.SetActive (true);
		m_PlayButton.SetActive (false);
	}

	//商店关闭
	public void StoreOff(){
		m_storeUI.SetActive (false);
		m_PlayButton.SetActive (true);
	}

	public void Addgem_one(){
		m_PlayerController.AddGemCount (60);

	}

	public void Addgem_two(){
		m_PlayerController.AddGemCount (300);

	}

	public void Addgem_three(){
		m_PlayerController.AddGemCount (980);

	}

	public void Addgem_four(){
		m_PlayerController.AddGemCount (1980);

	}

	public void Addgem_five(){
		m_PlayerController.AddGemCount (3280);

	}

	public void Addgem_six(){
		m_PlayerController.AddGemCount (6480);

	}

    private void Init()
    {
		var num = PlayerPrefs.GetInt ("gem", 0);
		if (num == m_PlayerController.gemCount) {
			m_PlayerController.gemCount = 100;
			PlayerPrefs.SetInt ("gem", m_PlayerController.gemCount);
		}
		print ("haha " + num);
		//PlayerPrefs.DeleteAll ();
		m_ScoreLabel.text = PlayerPrefs.GetInt("score", 0) + "";
        m_GemLabel.text = PlayerPrefs.GetInt("gem", 0) + "";
		m_GameScoreLabel.text = PlayerPrefs.GetInt("score", 0) + "";	
        m_GameGemLabel.text = PlayerPrefs.GetInt("gem", 0) + "";
		m_PlayerController.gemCount = PlayerPrefs.GetInt ("gem", 0);
    }

    public void UpdateData(int score, int gem)
    {
        m_GemLabel.text = gem + "";
        m_GameScoreLabel.text = score.ToString();
		m_GameGemLabel.text = gem + "";
    }

    private void PlayButtonClick(GameObject go)
    {
		if (m_PlayerController.gemCount < 20) {
			StoreOpen ();
			return;
		}
		m_PlayerController.SubGemCount (20);
        Debug.Log("游戏开始啦.");
        m_StartUI.SetActive(false);
        m_GameUI.SetActive(true);
		m_PlayerController.life = true;//游戏点击开始才可以运动
        m_PlayerController.StartGame();
    }

    private void Left(GameObject go)
    {
        m_PlayerController.Left();
    }

    private void Right(GameObject go)
    {
        m_PlayerController.Right();
    }

    public void ResetUI()
    {
        m_StartUI.SetActive(true);
        m_GameUI.SetActive(false);
		m_ScoreLabel.text = PlayerPrefs.GetInt("score", 0) + "";

    }
}
