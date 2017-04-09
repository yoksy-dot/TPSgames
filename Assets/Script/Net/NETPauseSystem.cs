using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public class NETPauseSystem : NetworkBehaviour
{
    public GameObject UI, UI2;//Pause時に表示したいUI
                              //public GameObject Player;
    public NETPlayerCtrl mmo;
    public float interval = 1.0f;//少し間隔を持たせる
    private GameObject canvas, aaaa, bbbb;//一時保管
    private float time = 0.0f;
    public bool GamePause;

    // Use this for initialization
    void Start()
    {
        GamePause = false;//Trueならポーズする
        canvas = GameObject.Find("Canvas").gameObject;
        //jjj = Player.transform.FindChild("CenterPoint/Camera").gameObject;
        //mmo = jjj.GetComponentInChildren<MMOPlayerCtrl>();

    }

    // Update is called once per frame
    void Update()
    {
        //if (!isLocalPlayer)
        //{
        //    return;
        //}
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
            bbbb.transform.parent = canvas.transform;
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

