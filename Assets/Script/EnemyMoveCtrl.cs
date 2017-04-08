using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveCtrl : MonoBehaviour {

	public int Pattern;
	public float interval = 0.0f;
	public float moveMax;
	public float moveMin;
	public float roteMax;
	public float roteMin;
	public int RoteNum;//何回に一回回転するか

	private float time,qqq = 1;
	private int aaa;

	Rigidbody MyBody;
	SearchGround Moving;

	// Use this for initialization
	void Start () {
		MyBody = GetComponent<Rigidbody>();
		Moving = GetComponentInChildren<SearchGround>();
		time = 0;
		aaa = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		time += Time.deltaTime;

		if (Moving.StayGround && GetComponentInParent<EnemyData>().canMove && !GetComponentInParent<EnemyData>().canFly && time >= interval)//接地してるなら
		{
			aaa++;//周回回数
			switch (Pattern)
			{
				case 0:/*完全ランダム*/
					MyBody.AddForce(transform.forward * Random.Range(moveMin, moveMax));
					if (RoteNum >= aaa)
					{
						transform.eulerAngles = new Vector3(0.0f, Random.Range(roteMin, roteMax), 0.0f);
						aaa = 0;
					}
					break;
				case 1:/*円を時計回りに回り続ける*/
					if (1 >= aaa)
						MyBody.AddForce(transform.forward * moveMin);
					else
					{
						transform.eulerAngles += new Vector3(0.0f, roteMin, 0.0f);
						aaa = 0;
					}
					break;
				default:
					Debug.Log("敵移動パターンがおかしいです");
					break;
			}
			time = 0.0f;
		}
		else if (GetComponentInParent<EnemyData>().canFly)
		{
			
			aaa++;//周回回数
			//飛行なら
			switch (Pattern)
			{
				case 0://円状に周回
					if (RoteNum >= aaa)
						MyBody.AddForce(transform.forward * moveMin);
					else
					{
						MyBody.velocity = transform.up * 0.0f;
						transform.eulerAngles += new Vector3(0.0f, roteMin, 0.0f);
						aaa = 0;
					}
					break;
				case 1://上下に動くだけ(初回上昇)
					if (RoteNum >= aaa)
					{
						MyBody.AddForce(transform.up * moveMin * qqq);
					}
					else
					{
						MyBody.AddForce(transform.up * moveMin * -1);
						aaa = 0;
						qqq *= -1;
						MyBody.velocity = transform.up * 0.0f;

					}
					break;
				default:
					Debug.Log("敵(飛行)移動パターンがおかしいです");
					break;
			}
			time = 0.0f;
		}
		
	}


}
