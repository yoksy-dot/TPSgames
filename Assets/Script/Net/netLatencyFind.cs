using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public class netLatencyFind : NetworkBehaviour
{
    
    
    private NetworkClient nClient;
	private int latency; //遅延時間
	private Text latencyText; //遅延時間表示用テキスト

	// Use this for initialization
	void Start () {
		nClient = GameObject.Find("Starter").GetComponent<NetworkManager>().client;
		latencyText = GameObject.Find("Latency Text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        ShowLatency();

    }

    void ShowLatency()
    {
        if (isLocalPlayer)
        {
            //latencyを取得
            latency = nClient.GetRTT();
            //latencyを表示
            latencyText.text = latency.ToString();
        }
    }
}
