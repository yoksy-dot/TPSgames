using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSystem : MonoBehaviour {

	public Transform ShootPoint, SearchArea;
	public GameObject Bullet1Prefab;

	private GameObject Chiled,Parent;

	//public bool useShoot;
	public float shotSpeed;
	public AudioClip shotSound;
	private float time = 0f;
	public float interval = 10f;//弾の発射間隔

	ShootbulletAuto AutoShooter;
	PlayerData Data;

	// Use this for initialization
	void Start () {

		AutoShooter = GetComponent<ShootbulletAuto>();
		//子取得
		Chiled = gameObject.transform.FindChild("SearchArea").gameObject;
		//親及びコンポーネント
		Parent = gameObject.transform.parent.gameObject;
		Data = Parent.GetComponent<PlayerData>();
	}

	// Update is called once per frame
	void Update()
	{
		time += Time.deltaTime;
		if (time >= interval && Chiled.GetComponent<PlayerSearchCtrl>().FindEnemy)
		{
			AutoShooter.ShotOnly(ShootPoint.transform, Bullet1Prefab, shotSpeed,Data.NUM,Data.Level);/*ファンネル装備は4のみ*/
			time = 0f;
			Chiled.GetComponent<PlayerSearchCtrl>().FindEnemy = false;
		}
		
	}
}
