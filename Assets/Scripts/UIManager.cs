﻿using UnityEngine;
using System.Collections;

/// <summary>
/// UI管理器.
/// </summary>
public class UIManager : MonoBehaviour {

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

	private GoogleAD ad;
//	private  ADUT ad;
//
//	void Awake()
//	{
//		ad = GameObject.Find("MapManager").GetComponent<ADUT>();
//	}

	void Start () {
        m_StartUI = GameObject.Find("Start_UI");
        m_GameUI = GameObject.Find("Game_UI");

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

		ad = GameObject.Find ("GoogleAD").GetComponent<GoogleAD> ();

        Init();

//		ad.SendAction("showBannerAD", "");
        m_GameUI.SetActive(false);
	}
	

    private void Init()
    {
		//PlayerPrefs.DeleteAll ();
		m_ScoreLabel.text = PlayerPrefs.GetInt("score", 0) + "";
        m_GemLabel.text = PlayerPrefs.GetInt("gem", 0) + "/100";
		m_GameScoreLabel.text = PlayerPrefs.GetInt("score", 0) + "";	
        m_GameGemLabel.text = PlayerPrefs.GetInt("gem", 0) + "/100";
		ad.RequestBanner();

    }

    public void UpdateData(int score, int gem)
    {
        m_GemLabel.text = gem + "/100";
        m_GameScoreLabel.text = score.ToString();
		m_GameGemLabel.text = gem + "/100";
    }

    private void PlayButtonClick(GameObject go)
    {
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
