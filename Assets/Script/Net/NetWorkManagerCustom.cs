using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class NetWorkManagerCustom : NetworkManager
{
    private NETCreatPlayer creator;
    private GameObject hoge;//インスタンス
    private NETPlayerCtrl Controller;
    private bool first;
    public GameObject CenterPoint;//プレハブ

	// Use this for initialization
	void Start () {
        //creator = GameObject.Find("EnemySystem").GetComponent<NETCreatPlayer>();
        first = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnServerConnect(NetworkConnection conn)
    {
    //    creator = GameObject.Find("EnemySystem").GetComponent<NETCreatPlayer>();

    //    creator.makingCP = true;
    //    Debug.Log("asd");
    }

    //public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    //{
    //    var player = (GameObject)GameObject.Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity);
    //    NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

    //    creator = GameObject.Find("EnemySystem").GetComponent<NETCreatPlayer>();
    //    if (first)
    //    {
    //        first = false;
    //    }
    //    else
    //    {
    //        hoge = (GameObject)Instantiate(CenterPoint, transform.position, transform.rotation);//カメラ作成
    //        NetworkServer.Spawn(hoge);
    //    }
        

    //}
}
