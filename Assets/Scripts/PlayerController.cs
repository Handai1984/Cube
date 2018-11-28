using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 角色控制.
/// </summary>
public class PlayerController : MonoBehaviour {
	#region 定义变量

	public int z = 3;
	private int x = 2;
	
	private Color colorOne = new Color(122 / 255f, 85 / 255f, 179 / 255f);
	private Color colorTwo = new Color(126 / 255f, 93 / 255f, 183 / 255f);
	
	private Transform m_Transform;
	
	private MapManager m_MapManager;
	private CameraFollow m_CameraFollow;
	private UIManager m_UIManager;
	
	public bool life = false;//改成公共的
	private int gemCount = 0;
	private int socreCount = 0;
	
	private GoogleAD interstital1;
	private GoogleAD interstital2;
	private List <GoogleAD> gad;

	private int count = 0;
	#endregion
	//private  ADUT ad;
#region 得分和金币以及保存数据

	private void AddGemCount()
	{
		gemCount++;
		//  Debug.Log("宝石数:" + gemCount);
		m_UIManager.UpdateData(socreCount, gemCount);
	}
	
	public void AddScoreCount()
	{
		socreCount++;
		//  Debug.Log("分数:" + socreCount);
		m_UIManager.UpdateData(socreCount, gemCount);
	}
	
	private void SaveData()
	{
		PlayerPrefs.SetInt("gem", gemCount);
		if (socreCount > PlayerPrefs.GetInt("score",0))
		{
			PlayerPrefs.SetInt("score", socreCount);
		}
		
	}
	#endregion

	void Start () {
		gad = new List<GoogleAD> ();

        gemCount = PlayerPrefs.GetInt("gem", 0);
		socreCount = PlayerPrefs.GetInt ("score", 0);
        m_Transform = gameObject.GetComponent<Transform>();

        m_MapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
        m_CameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        m_UIManager = GameObject.Find("UI Root").GetComponent<UIManager>();
		//查找广告脚本
//		interstital1 = GameObject.Find ("GoogleADOne").GetComponent<GoogleAD> ();
//		interstital2 = GameObject.Find ("GoogleADTwo").GetComponent <GoogleAD> ();
//
//		gad.Add (interstital1);
//		gad.Add (interstital2);
//		print (gad.Count);
		//初始化广告脚本
//		GADInit ();
		//ad
		//ad = GameObject.Find("MapManager").GetComponent<ADUT>();

	}

	private void GADInit()
	{
		for (int i = 0; i < gad.Count; i++) {
			gad [i].RequestInterstitial ();
		}
	}
	
    public void StartGame()
    {
		socreCount = 0;	
        SetPlayerPos();
        m_CameraFollow.startFollow = true;
        m_MapManager.StartTileDown();
		//interstital2.ShowInterstitial ();
    }

    #region 游戏更新移动

	void Update () {
		if(Input.GetKeyDown(KeyCode.M))
		{
			StartGame();
		}
		
		if (life)
		{
			PlayerControl();
		}
		
	}
	
	public void Left()
	{
		if (x != 0)
		{
			z++;
			AddScoreCount();
		}
		
		if (z % 2 == 1 && x != 0)
		{
			x--;
		}
		//  Debug.Log("Left:z:" + z + "--x:" + x);
		SetPlayerPos();
		CalcPosition();
	}
	
	public void Right()
	{
		if (x != 4 || z % 2 != 1)
		{
			z++;
			AddScoreCount();
		}
		
		if (z % 2 == 0 && x != 4)
		{
			x++;
		}
		//  Debug.Log("Right:z:" + z + "--x:" + x);
		SetPlayerPos();
		CalcPosition();
	}
	
	/// <summary>
	/// 角色移动控制.
	/// </summary>
	private void PlayerControl()
	{
		//left.
		if (Input.GetKeyDown(KeyCode.A))
		{
			Left();
		}
		
		//right.
		if (Input.GetKeyDown(KeyCode.D))
		{
			Right();
		}
	}
	#endregion

	#region 设置角色位置及粒子效果

	/// <summary>
	/// 设置角色位置,蜗牛痕迹生成.
	/// </summary>
	private void SetPlayerPos()
	{
		if (life==false) {
			return;
		}
		Transform playerPos = m_MapManager.mapList[z][x].GetComponent<Transform>();
		MeshRenderer normal_a2 = null;
		
		m_Transform.position = playerPos.position + new Vector3(0, 0.254f / 2, 0);
		m_Transform.rotation = playerPos.rotation;
		
		if (playerPos.tag == "Tile")
		{
			normal_a2 = playerPos.Find("normal_a2").GetComponent<MeshRenderer>();
		}
		else if (playerPos.tag == "Spikes")
		{
			normal_a2 = playerPos.Find("moving_spikes_a2").GetComponent<MeshRenderer>();
		}
		else if (playerPos.tag == "Sky_Spikes")
		{
			normal_a2 = playerPos.Find("smashing_spikes_a2").GetComponent<MeshRenderer>();
		}
		
		if(normal_a2 != null)
		{
			if (z % 2 == 0)
			{
				normal_a2.material.color = colorOne;
			}
			else
			{
				normal_a2.material.color = colorTwo;
			}
		}
		else
		{
			gameObject.AddComponent<Rigidbody>();
			StartCoroutine("GameOver", false);//把true改成false解决方块死亡移动的问题
		}
		
	}
	
	/// <summary>
	/// 计算位置.[角色在地图上的位置]
	/// </summary>
	private void CalcPosition()
	{
		if(m_MapManager.mapList.Count - z <= 12)
		{
			Debug.Log("动态生成新地图");
			m_MapManager.AddPR();
			float offsetZ = m_MapManager.mapList[m_MapManager.mapList.Count - 1][0].GetComponent<Transform>().position.z + m_MapManager.bottomLength / 2;
			m_MapManager.CreateMapItem(offsetZ);
		}
	}
	
	private void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "Spikes_Attack")
		{
			StartCoroutine("GameOver", false);
		}
		
		if(coll.tag == "Gem")
		{
			GameObject.Destroy(coll.gameObject.GetComponent<Transform>().parent.gameObject);
			AddGemCount();
		}
	}
	
	#endregion

    public IEnumerator GameOver(bool b)
    {
        if(b)
        {
            yield return new WaitForSeconds(0.5f);
        }
		if(life)
		{
			//Debug.Log("游戏结束");
			m_CameraFollow.startFollow = false;
			life = false;
			SaveData();
			//ad.SendAction ("showInterstitialAD", "");	//增加广告
//			bannerView.BannerDestory();
//			print ("banner is destory");

//			GADInterstitalShow ();
			StartCoroutine("ResetGame");

			//TODO:UI相关的交互.
		}

        //Time.timeScale = 0;
    }

    private void ResetPlayer()
    {
	GameObject.Destroy(gameObject.GetComponent<Rigidbody>());
        
        z = 3;
        x = 2;

       // life = true;

        socreCount = 0;
    }


    private IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(2);
        m_MapManager.ResetGameMap();
        m_CameraFollow.ResetCamera();
		m_UIManager.ResetUI();


		ResetPlayer();
    }

	private void GADInterstitalShow()
	{
		if (count >= gad.Count) {
			GADInit ();
			count = 0;


		} else {
			
			gad [count].ShowInterstitial ();
			print ("显示插屏"+count);
			count++;
		}
	}
		
}
