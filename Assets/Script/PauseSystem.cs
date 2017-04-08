using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseSystem : MonoBehaviour {
	public GameObject UI,UI2;//Pause時に表示したいUI
	//public GameObject Player;
	public MMOPlayerCtrl mmo;
	public float interval = 1.0f;//少し間隔を持たせる
	private GameObject canvas,aaaa, bbbb;//一時保管
	private float time = 0.0f;
	public bool GamePause;

	// Use this for initialization
	void Start () {
		GamePause = false;//Trueならポーズする
		canvas = GameObject.Find("Canvas").gameObject;
		

	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (Input.GetKey(KeyCode.Escape) && time >= interval)//ESCキーを押すと
		{
			
			PauseFunc();
			time = 0;
		}

	}

	void PauseFunc()//ポーズ用関数
	{
		if (GamePause)
		{
			Cursor.visible = true;
			aaaa = Instantiate(UI);
			bbbb = Instantiate(UI2);
			aaaa.transform.parent = canvas.transform;
			aaaa.transform.localPosition = new Vector3(0, 30, 0);
			bbbb.transform.parent = canvas.transform;
			bbbb.transform.localPosition = new Vector3(0, -30, 0);
		}
		else
		{
			Cursor.visible = false;
			Destroy(aaaa);
			Destroy(bbbb);
		}
		GamePause = !GamePause;
		mmo.CanMove = GamePause;
	}
}
