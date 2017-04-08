using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUper : MonoBehaviour {
	GameObject area;
	PlayerSearchCtrl aaaa;
	PlayerData Char;

	public int Lv;
	public int NUM;
	public float interval;
	public float Effect;
	public float EffectTime = 5.0f;
	public float Distance;

	private float time,time2;
	// Use this for initialization
	void Start () {
		area = gameObject.transform.FindChild("SearchArea").gameObject;//操作範囲のオブジェクト
		aaaa = area.GetComponent<PlayerSearchCtrl>();//コンポーネント

		area.transform.localScale = new Vector3(Distance, Distance, Distance);
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;//ステアップ間隔用
		time2 += Time.deltaTime;//消滅判定用

		if(time2 >= EffectTime)
		{
			Destroy(gameObject);
		}

		if (aaaa.FindPlayer && aaaa.SearchedCharP != null && time >= interval)//キャラがいるなら
		{
			switch (NUM)
			{
				case 1:
					//魔法使い(回復)
					//範囲HP回復
					if(aaaa.SearchedCharP.tag == "Player" )
					{
						Char = aaaa.SearchedCharP.GetComponent<PlayerData>();
						if(Char.nowHP > 0 && Char.nowHP <= Char.HP)//死んでるやつにはしない Maxにもしない
						{
							Char.nowHP += Effect + ( (Lv -1) * 0.3f);
							if (Char.nowHP >= Char.HP)//超えたらMaxにする
								Char.nowHP = Char.HP;
							time = 0;
						}
					}
					break;
				//case 4:
				//	//獣人(対人)
				//	//自分HP自動回復+攻撃UP(ShootBullet.cs)
				//	if (aaaa.SearchedCharP.tag == "Player")
				//	{
				//		//this.gameObject.transform.parent = aaaa.SearchedCharP.transform;
				//		Char = aaaa.SearchedCharP.GetComponent<PlayerData>();
				//		if (Char.nowHP > 0 && Char.nowHP <= Char.HP)//死んでるやつにはしない Maxにもしない
				//		{
				//			Char.nowHP += Effect + ((Lv - 1) * 1.0f);
				//			if (Char.nowHP >= Char.HP)//超えたらMaxにする
				//				Char.nowHP = Char.HP;
				//			time = 0;
				//		}
				//	}
				//	break;
			}
		}
	}
}
