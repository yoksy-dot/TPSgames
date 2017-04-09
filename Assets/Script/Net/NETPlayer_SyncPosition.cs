using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic; //Listを使う時のライブラリ

[NetworkSettings(channel = 0, sendInterval = 0.033f)]
public class NETPlayer_SyncPosition : NetworkBehaviour
{

    //hook: SyncVar変数が変更された時、指定メソッドを実行するようサーバーから全クライアントへ命令を出す
    [SyncVar(hook = "SyncPositionValues")]
    private Vector3 syncPos;

    [SerializeField]
    Transform myTransform;

    float lerpRate;
    float normalLerpRate = 15;
    float fasterLerpRate = 25;

    private Vector3 lastPos;
    private float threshold = 0.5f;
    private NetworkClient nClient;
    private int latency;
    private Text latencyText;

    //Position同期用のList
    private List<Vector3> syncPosList = new List<Vector3>();

    //HistoricalLerpingメソッドを使う時はtrueにする
    //SerializeFieldなのでInspectorビューから変更可能
    [SerializeField]
    private bool useHistoricalLerping = false;

    //2点間の距離を判定する時に使う
    private float closeEnough = 0.1f;

    void Start()
    {
        nClient = GameObject.Find("Starter").GetComponent<NetworkManager>().client;
        latencyText = GameObject.Find("Latency Text").GetComponent<Text>();
        lerpRate = normalLerpRate;
    }

    void Update()
    {
        LerpPosition();
        ShowLatency();
    }

    void FixedUpdate()
    {
        TransmitPosition();
    }

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
                //通常の補間メソッド
                OrdinaryLerping();


        }
    }

    [Command]
    void CmdProvidePositionToServer(Vector3 pos)
    {
        syncPos = pos;
    }

    [ClientCallback]
    void TransmitPosition()
    {
        if (isLocalPlayer && Vector3.Distance(myTransform.position, lastPos) > threshold)
        {
            CmdProvidePositionToServer(myTransform.position);
            lastPos = myTransform.position;
        }
    }

    //クライアントのみ有効
    [Client]
    //hookで指定されたメソッド　全クライアントが実行
    void SyncPositionValues(Vector3 latestPos)
    {
        syncPos = latestPos;
        //ListにPosition追加
        syncPosList.Add(syncPos);
    }

    void ShowLatency()
    {
        if (isLocalPlayer)
        {
            latency = nClient.GetRTT();
            latencyText.text = latency.ToString();
        }
    }

    //通常使われる補間メソッド
    void OrdinaryLerping()
    {
        myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
    }

}