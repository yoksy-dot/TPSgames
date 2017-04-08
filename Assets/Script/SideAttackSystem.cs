using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideAttackSystem : MonoBehaviour {
	public GameObject ArrowPrehab;
	GameObject ChildShoot;
	ShootbulletAuto Auto;
	private bool fire = false;
	public int ChildrenNum;//子の数
	public int ShootNum = 1;//発射数
	public float DelayTime = 1.0f;//発生までの遅延
	public float ShootSpeed = 10000;

	public int NUM, Lv;//スクリプトで設定

	private float time,time2;
	private bool Did;
	private int Shooted,number=0;//撃った数(周回回数把握)
	private float interval;
	// Use this for initialization
	void Start () {
		Auto = GetComponent<ShootbulletAuto>();
        time = time2 = 0.0f;
		Did = false;
		Shooted = 0;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		interval = Random.Range(0, 0.01f);
        if (DelayTime <= time && !Did)
		{
			//時間が過ぎたらオンにする
			Did = true;
		}
		if (Did)
		{
            time2 += Time.deltaTime;
			if(Shooted < ShootNum && time2 >= interval)//子の数だけループ
			{
                
				if (number >= ChildrenNum) number = 0;
				ChildShoot = gameObject.transform.FindChild("Shooter (" + number + ")").gameObject;//名前注意
				fire = true;
				Shooted++;number++;
				time2 = 0.0f;
			}
			else if(Shooted >= ShootNum)//規定数打ったら終わり
				Destroy(gameObject);
		}
	}

	void FixedUpdate()
	{
		if (fire)
		{
			Auto.ShotOnly(ChildShoot.transform, ArrowPrehab, ShootSpeed, NUM, Lv);
			fire = false;
		}
	}
}
