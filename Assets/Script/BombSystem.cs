using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSystem : MonoBehaviour {
	//public GameObject BombPrefab;
	//private GameObject Bomb;
	
	public int _ShootPlayer,_ShootLv;
	public float bombATK = 3.0f;//爆風の攻撃力
	public float bombdistance = 2.0f;//爆風の最大の大きさ
	public float Speed = 0.2f;
	private float i = 0.8f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (i < bombdistance)
		{
			this.transform.localScale = new Vector3(i, i, i);
			i += Speed * Time.deltaTime;
		}
		else
			Destroy(gameObject);//削除
	}
}
