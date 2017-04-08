using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CreatPlayer : MonoBehaviour {
	public GameObject Char0, Char1, Char2, Char3, Char4, Char5;//キャラ用プレハブ
    public GameObject CP;//センターポイント
	private GameObject Player,Center;
	private GameObject childObject;
	private float time = 0.0f,time2 = 0.0f;
	private bool First = true;
    private float mousespeed;
	public int NUM;
	public float StandBy = 5.0f;

	UISetBullet CreatSlider;//スライダー関係
	Shootbullet bulletnum;//Bullet用
	PlayerData Data;
	TextCtrl _text;
	PauseSystem Pause;
    MMOPlayerCtrl ctrl;

    // Use this for initialization
    void Start () {
		CreatSlider = GetComponent<UISetBullet>();
		
		NUM = CharacterChoice.PlayerChoice;//保存用
        mousespeed = CharacterChoice.MouseSmooth;//マウス感度
        _text = GameObject.Find("Canvas/TextMes").GetComponent<TextCtrl>();//メッセージ用

		Pause = GameObject.Find("EnemySystem").GetComponent<PauseSystem>();

		Cursor.visible = false;//カーソルを非表示にする

        Center = (GameObject)Instantiate(CP, transform.position, transform.rotation);//カメラ作成

    }
	
	// Update is called once per frame
	void Update () {
		_text.textCtrl(0, "");//何もないなら文字を表示しない
			
		if (StandBy > 0)//初回なら  なくしてもいいかも
		{
			StandBy -= Time.deltaTime;
			//_text.textCtrl(0, "Ready");
		}
		else if(StandBy <= 0 && First)
		{
			PlayerMaker(NUM);  
			First = false;
		}

		if (!First)
		{
			if (Data.nowHP <= 0)//死んでいるなら
			{
				time += Time.deltaTime;
				if (Data.ReSpornTime >= time)//リスポーン待ち
				{
					_text.textCtrl(0, Mathf.Ceil(time / Data.ReSpornTime * 100) + "%");//復活までの時間
				}
				else//復活
				{
					Pause.mmo.CanMove = true;
					Destroy(Player.gameObject);//プレイヤーの完全消去
					time = 0;
					PlayerMaker(NUM);

				}
			}
		}
	}

	void PlayerMaker(int PlayNumber)//プレイヤー作る関数
	{
		switch (PlayNumber)
		{
			/*MMO...のcharacterに先に設定されたプレイヤープレハブを設定していく*/
			case 0:
				Player = (GameObject)Instantiate(Char0, transform.position, transform.rotation);
				childObject = Player.gameObject.transform.FindChild("wepon_rendo/shootbullet1").gameObject;//少なくなる
				Data = Player.GetComponent<PlayerData>();//直下になる
				break;
			case 1:
				Player = (GameObject)Instantiate(Char1, transform.position, transform.rotation);//プレイヤー作成
				childObject = Player.gameObject.transform.FindChild("weapon/shootbullet1").gameObject;
                Data = Player.GetComponent<PlayerData>();
                break;
			case 2:
				Player = (GameObject)Instantiate(Char2, transform.position, transform.rotation);
				childObject = Player.gameObject.transform.FindChild("rance_Main/shootbullet2").gameObject;
                Data = Player.GetComponent<PlayerData>();
                break;
			case 3:
				Player = (GameObject)Instantiate(Char3, transform.position, transform.rotation);
				childObject = Player.gameObject.transform.FindChild("weapon/shootbullet1").gameObject;
                Data = Player.GetComponent<PlayerData>();
                break;
			case 4:
				Player = (GameObject)Instantiate(Char4, transform.position, transform.rotation);
				childObject = Player.gameObject.transform.FindChild("WeaponALL/shootbullet1").gameObject;
                Data = Player.GetComponent<PlayerData>();
                break;
			case 5:
				Player = (GameObject)Instantiate(Char5, transform.position, transform.rotation);
				childObject = Player.gameObject.transform.FindChild("waepon/shootbullet1").gameObject;
                Data = Player.GetComponent<PlayerData>();
                break;
		}
		bulletnum = childObject.GetComponent<Shootbullet>();
		CreatSlider.SetUISysytem("Canvas/BulletBarUI1", bulletnum.BulletMax, bulletnum.BulletMax);
        ctrl = Player.GetComponent<MMOPlayerCtrl>();//普通のに
        ctrl.centerPoint = Center.transform;//代入
        ctrl.Playercm = Center.transform.FindChild("Camera");
        ctrl.mouseSmooth = mousespeed;
        childObject.GetComponent<GunCtrl>().TaregetPonint = Center.transform.FindChild("TaregetPoint");
        Pause.mmo = ctrl;//ポーズ用オブジェクト代入
		_text.textCtrl(0, "GO!");
	}
}
