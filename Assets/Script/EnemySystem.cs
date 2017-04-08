using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour {
	//敵を一定間隔ごとに生み出すスクリプト
	public GameObject EnemyPrefab, Enemy2Prefab;

	public float interval = 10f;
	private float time = 0f;

	float x, z;

	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		x = Random.Range(-20.0f, 20.0f);
		z = Random.Range(-20.0f, 20.0f);
		
		if (time >= interval)
		{
			GameObject Enemy = (GameObject)Instantiate(EnemyPrefab, new Vector3(x, 3.0f, z), transform.rotation);

			Rigidbody EnemyRigidbody = Enemy.GetComponent<Rigidbody>();

			time = 0f;
		}
	}
}
