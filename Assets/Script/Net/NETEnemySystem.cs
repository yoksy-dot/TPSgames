using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class NETEnemySystem : NetworkBehaviour
{
	public GameObject obj1;
    public float interval = 10f;
    public float Distance = 20f;
    private float x, z;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.frameCount % interval == 0)
        {
            CmdCreateCube(obj1, Distance);
        }
    }

	[Command]
	void CmdCreateCube(GameObject nanika, float dis)
	{
		x = Random.Range(-dis, dis);
		z = Random.Range(-dis, dis);
		GameObject Enemy = (GameObject)Instantiate(nanika, new Vector3(x, 3.0f, z), transform.rotation);
		NetworkServer.Spawn(Enemy);
	}
}
