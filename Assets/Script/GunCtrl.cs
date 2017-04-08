using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCtrl : MonoBehaviour {

	public Transform TaregetPonint;
	private Transform itizi;
	// Use this for initialization
	void Start () {
		itizi= TaregetPonint;

	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(TaregetPonint);
		//打つ方向に向けるだけのスクリプト
	}

	public void MoveRandSystem(float distance)
	{
		TaregetPonint.localPosition += new Vector3(Random.Range(-distance, distance), Random.Range(-distance, distance), 0.0f);
        
	}

    public void MoveReturn()
    {
        TaregetPonint.localPosition = new Vector3(0, -0.8f, 6);
    }
}
