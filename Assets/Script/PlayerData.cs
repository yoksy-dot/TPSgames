using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerData : MonoBehaviour {
	//GameObject Parent;
	//public bool useFire2;

	public int NUM;
	public float HP = 100;
	public float nowHP;
	public float ReSpornTime = 5.0f;
	
	public int Level = 1;
	public float nowExperience = 0;
	private float[] ExpTable = new float[11] {1,5,9,13,17,21,25,29,33,38,44 };
	/*  経験値テーブル入れ とりま５つ
	 *  攻撃力加算式はEnemyDataに入ってる(Lv.1につき+0.2f)
	 */

	UISetBullet ForUI;
	TextCtrl _text, _text2, _text3;
	Shootbullet sss;

	// Use this for initialization
	void Start () {
		nowHP = HP;
		
		Level = 1;
		nowExperience = 0;

		//Sliderのコンポーネント
		ForUI = GetComponent<UISetBullet>();
		//テキストのコンポーネント
		_text = GameObject.Find("Canvas/TextHP").GetComponent<TextCtrl>();//HPテキスト
		_text2 = GameObject.Find("Canvas/TextLv").GetComponent<TextCtrl>();//Lvテキスト
		_text3 = GameObject.Find("Canvas/TextExp").GetComponent<TextCtrl>();//Expテキスト

		ForUI.SetUISysytem("Canvas/HPBarUI", HP, HP);//HP初期化
		ForUI.SetUISysytem("Canvas/ExpBarUI", 100, 0);//経験値?初期化

		sss = gameObject.GetComponentInChildren<Shootbullet>();//しました
		sss.ShootPlayer = NUM;//射撃者ナンバー
		sss.ShootLv = Level;

		//Parent = gameObject.transform.root.gameObject;//一番上の親
	}
	
	// Update is called once per frame
	void Update () {
		//レベルアップ判定
		if (nowExperience >= ExpTable[Level - 1])
		{
			Level++;
			nowExperience = 0;
			sss.ShootLv = Level;
		}
		//Debug.Log(Level);
		float aaa = (nowHP / HP) * 100;//HP用100分率
		float bbb = (nowExperience / ExpTable[Level-1]) * 100;//経験値100分率
		_text.textCtrl(0, Mathf.Ceil(aaa) +"%" );
		_text2.textCtrl(0, "Lv." + Level);
		_text3.textCtrl(0, Mathf.Ceil(bbb) + "%");

		if(nowHP <= 0)//死んだとき
		{
			Destroy(gameObject);

		}
		

		//スライダー更新
		ForUI.SetUISysytem("Canvas/HPBarUI", HP, nowHP);
		ForUI.SetUISysytem("Canvas/ExpBarUI", ExpTable[Level - 1], nowExperience);
	}

	void OnCollisionEnter(Collision coll)//普通用
	{
		if (coll.gameObject.tag == "EnemyBullet")
		{
			nowHP -= coll.gameObject.GetComponent<Bullet>().ATK;
			

		}
	}

	void OnTriggerEnter(Collider coll)//isTrigar使用
	{
		if (coll.gameObject.tag == "Bomb" && coll.gameObject.GetComponent<BombSystem>()._ShootPlayer < 0)
		//主に爆風
		//-1なら敵からの弾
		{
			nowHP -= coll.gameObject.GetComponent<BombSystem>().bombATK;
		}
	}
}
