using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpSystem : MonoBehaviour {

	private GameObject qwe;
	private PlayerData Player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InputExp(int PlayerNUM, float exp)//非ネット化
	{
		switch (PlayerNUM)
		{
			case 0:
				qwe = GameObject.Find("PlayerCrossbow_ver2(Clone)");
				break;
			case 1:
				qwe = GameObject.Find("PlayerMagic_ver2(Clone)");
				break;
			case 2:
				qwe = GameObject.Find("PlayerLance_ver2(Clone)");
				break;
			case 3:
				qwe = GameObject.Find("PlayerGatling_ver2(Clone)");
				break;
			case 4:
				qwe = GameObject.Find("PlayerForces_ver2(Clone)");
				break;
			case 5:
				qwe = GameObject.Find("PlayerRifle_ver2(Clone)");
				break;
			default:
				Debug.Log("経験値関連がおかしいです");
				break;

		}
		Player = qwe.GetComponent<PlayerData>();
		Player.nowExperience += exp;
	}
}
