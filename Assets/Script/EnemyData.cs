using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour {

	public Transform ShootPoint,SearchArea;
	public GameObject BulletPrefab;

	private GameObject Chiled;
	private bool Fire = false;
	ShootbulletAuto AutoShooter;//射撃スクリプト
	ExpSystem _ExpSystem;//経験値システムのスクリプト
	EnemyMoveCtrl moving;

	public float HP = 1;//開始時の体力
	public float nowHP = 1;//現在の体力
	public float ATK = 1;//攻撃力
	public bool canShot;//弾を打つかどうか
	//public bool UseBomb;
	public float interval = 10f;//弾の発射間隔
	public float ShootSpeed = 10.0f;
	public float Exp = 1.0f;//所有経験値
	public bool canMove;//動けるかどうか
	public bool canFly;//飛ぶやつかどうか

	private float time = 0f;
	private int playNUM;

	// Use this for initialization
	void Start () {
		nowHP = HP;


		//射撃用スクリプトのコンポーネントをとってくる
		AutoShooter = GetComponent<ShootbulletAuto>();
		//経験値システムのスクリプトのコンポーネントをとってくる
		_ExpSystem = GameObject.Find("EnemySystem").GetComponent<ExpSystem>();
		//子取得
		if(canShot)
			Chiled = gameObject.transform.FindChild("SearchArea").gameObject;
		/*射撃しないやつや子がない奴は実行時にエラーが出てる*/
	}
	
	// Update is called once per frame
	void Update () {

		time += Time.deltaTime;

		if (nowHP <= 0)
		//力尽きたなら
		{
			_ExpSystem.InputExp(playNUM, Exp);
			Destroy(gameObject);
		}

		if (canShot && time >= interval && Chiled.GetComponent<EnemySearchCtrl>().FindPlayer)//射撃判定
		{
			Fire = true;
			Chiled.GetComponent<EnemySearchCtrl>().FindPlayer = false;

			time = 0;
		}

		if (canMove)
		{
			//moving.
		}
		
	}

	void FixedUpdate()
	{
		if (Fire)
		{
			AutoShooter.ShotOnly(ShootPoint.transform, BulletPrefab, ShootSpeed, -1, 0);
			Fire = false;
		}
	}


	void OnCollisionEnter(Collision coll)//普通用
	{
		if (coll.gameObject.tag == "Bullet")
		{
			nowHP -= coll.gameObject.GetComponent<Bullet>().ATK + coll.gameObject.GetComponent<Bullet>().ShootLv * 0.5f;
			//レベル1につき0.5だけダメージが増える
			playNUM = coll.gameObject.GetComponent<Bullet>().ShootPlayer;

		}
	}


	void OnTriggerEnter(Collider coll)//isTrigar使用
	{
		if (coll.gameObject.tag == "Bullet")//主に近接系
		{
			nowHP -= coll.gameObject.GetComponent<Bullet>().ATK + coll.gameObject.GetComponent<Bullet>().ShootLv * 1.2f;
			playNUM = coll.gameObject.GetComponent<Bullet>().ShootPlayer;

		}
		if (coll.gameObject.tag == "Bomb" && coll.gameObject.GetComponent<BombSystem>()._ShootPlayer >= 0)
		//主に爆風
		//0以上ならプレイヤーからの攻撃判定
		{
			nowHP -= coll.gameObject.GetComponent<BombSystem>().bombATK + coll.gameObject.GetComponent<BombSystem>()._ShootLv * 0.4f;
			playNUM = coll.gameObject.GetComponent<BombSystem>()._ShootPlayer;
		}
	}

}
