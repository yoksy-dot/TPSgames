using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemySearchCtrl : MonoBehaviour {
	/*プレイヤーには使えないです*/
	private GameObject parent;
	private float time;
	public bool FindPlayer;
	//public bool FindEnemy;

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject;
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	void OnTriggerStay(Collider coll)
	{
		
		//プレイヤーが接触している間trueを返す
		if(coll.gameObject.tag == "Player")
		{
			FindPlayer = true;
			transform.parent.LookAt(coll.gameObject.transform);//範囲内ならプレイヤーの方向を向く
			/*LookAtなんかやばい 回転させたほうがいいかもしれない*/
		}

	}

	void OnTriggerExit(Collider coll)
	{
		if (coll.gameObject.tag == "Player")
		{

			FindPlayer = false;
		}


	}
}
