using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class NETCreatPlayer : NetworkBehaviour
{
    public GameObject Char0, Char1, Char2, Char3, Char4, Char5;//キャラ用プレハブ
    public GameObject CP;//センターポイント
    public NetWorkManagerCustom netmane;

    public bool makingCP = false;

    private GameObject Player, Center/*,ChengeCenter*/;
    private GameObject childObject, childObject2;
    //private GameObject FuncCenterPoint;
    private float time = 0.0f;
    private bool First = true;
    private float mousespeed;
    public int NUM;
    //public float StandBy = 5.0f;

    UISetBullet CreatSlider;//スライダー関係
    NETShootbullet bulletnum;//Bullet用
    NetPlayerData Data;
    TextCtrl _text;
    NETPauseSystem Pause;
    NETPlayerCtrl ctrl;
    NETAnimationCtrl animctrl;
    netPlayerSetUp setup;
    // Use this for initialization
    void Start()
    {
        CreatSlider = GetComponent<UISetBullet>();

        //NUM = CharacterChoice.PlayerChoice;//保存用
        mousespeed = CharacterChoice.MouseSmooth;//マウス感度
        _text = GameObject.Find("Canvas/TextMes").GetComponent<TextCtrl>();//メッセージ用

        Pause = gameObject.GetComponent<NETPauseSystem>();
        animctrl = gameObject.GetComponent<NETAnimationCtrl>();
        Cursor.visible = false;//カーソルを非表示にする

        //CmdMakeCnter();
        //PlayerMaker(NUM);
        //Debug.Log("hi!");
        makingCP = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (makingCP)
        //{
        //    CmdMakeCnter();
        //    ChengeCenter = Center;
        //    //ChengeCenter = Center.GetComponent<NETPlayerCtrl>();//普通のに
        //    //ChengeCenter.centerPoint = Center.transform;
        //    //ChengeCenter.Playercm = Center.transform.FindChild("Camera");
        //    //ChengeCenter.mouseSmooth = mousespeed;
        //    makingCP = false;
        //}
        _text.textCtrl(0, "");//邪魔になるかも

        //if (!isLocalPlayer)
        //{
        //    return;
        //}
        

        

        if (First)
        {
            //CmdMakeCnter();
            PlayerMaker(NUM);
            First = false;
        }

        if (!First)
        {
            if (Data.nowHP <= 0)//死んでいるなら
            {
                time += Time.deltaTime;
                if (Data.ReSpornTime >= time)//リスポーン待ち
                {
                    _text.textCtrl(0, Mathf.Ceil(time / Data.ReSpornTime * 100) + "%");//復活までの時間
                }
                else//復活
                {
                    Pause.mmo.CanMove = true;
                    Destroy(Player.gameObject);//プレイヤーの完全消去
                    time = 0;
                    PlayerMaker(NUM);

                }
            }
        }
    }


    void PlayerMaker(int PlayNumber)//プレイヤー作る関数
    {
        switch (PlayNumber)
        {
            /*MMO...のcharacterに先に設定されたプレイヤープレハブを設定していく*/
            case 0:
                //Player = (GameObject)Instantiate(Char0, transform.position, transform.rotation);
                //NetworkServer.Spawn(Player);
                Player = Char0;
                childObject = Player.gameObject.transform.FindChild("Player").gameObject;
                childObject2 = childObject.gameObject.transform.FindChild("wepon_rendo/shootbullet1").gameObject;
                Data = childObject.GetComponent<NetPlayerData>();//直下になる
                break;
            case 1:
                //Player = Char1;
                //childObject = Player.gameObject.transform.FindChild("weapon/shootbullet1").gameObject;
                //Data = Player.GetComponent<NetPlayerData>();

                Player = Char1;
                childObject = Player.gameObject.transform.FindChild("Player").gameObject;
                childObject2 = childObject.gameObject.transform.FindChild("weapon/shootbullet1").gameObject;
                Data = childObject.GetComponent<NetPlayerData>();
                break;
            case 2:
                //Player = (GameObject)Instantiate(Char2, transform.position, transform.rotation);
                //NetworkServer.Spawn(Player);
                Player = Char2;
                childObject = Player.gameObject.transform.FindChild("rance_Main/shootbullet2").gameObject;
                Data = Player.GetComponent<NetPlayerData>();
                break;
            case 3:
                //Player = (GameObject)Instantiate(Char3, transform.position, transform.rotation);
                //NetworkServer.Spawn(Player);
                Player = Char3;
                childObject = Player.gameObject.transform.FindChild("weapon/shootbullet1").gameObject;
                Data = Player.GetComponent<NetPlayerData>();
                break;
            case 4:
                //Player = (GameObject)Instantiate(Char4, transform.position, transform.rotation);
                //NetworkServer.Spawn(Player);
                Player = Char4;
                childObject = Player.gameObject.transform.FindChild("WeaponALL/shootbullet1").gameObject;
                Data = Player.GetComponent<NetPlayerData>();
                break;
            case 5:
                //Player = (GameObject)Instantiate(Char5, transform.position, transform.rotation);
                //NetworkServer.Spawn(Player);
                Player = Char5;
                childObject = Player.gameObject.transform.FindChild("waepon/shootbullet1").gameObject;
                Data = Player.GetComponent<NetPlayerData>();
                break;
        }
        bulletnum = Player.GetComponent<NETShootbullet>();
        /*MMOCtrl関係*/
        ctrl = Player.GetComponent<NETPlayerCtrl>();
        
        
        //ctrl.centerPoint = Center.transform;
        //ctrl.Playercm = Center.transform.FindChild("Camera");
        //ctrl.mouseSmooth = mousespeed;
        /*ポーズ*/
        Pause.mmo = ctrl;
        /*GunCtrl*/
        Player.GetComponent<GunCtrl>().TaregetPonint = Player.transform.FindChild("NETCenterPoint/TaregetPoint");
        /*SetUp関係*/
        //setup = childObject.GetComponent<netPlayerSetUp>();
        //setup.maincamera = ctrl.Playercm.GetComponent<Camera>();
        //setup.audioListener = ctrl.Playercm.GetComponent<AudioListener>();
        //setup.reder = ctrl.centerPoint.GetComponentInChildren<SpriteRenderer>();
        /*PlayerPrefabs代入*/
        netmane.playerPrefab = Player;
        /*UI*/
        CreatSlider.SetUISysytem("Canvas/BulletBarUI1", bulletnum.BulletMax, bulletnum.BulletMax);
        _text.textCtrl(0, "GO!"); 
    }

    //GameObject FindCenterPoint()
    //{
    //    int num = 1;
    //    for(num = 1; num < 5; num++)
    //    {
    //        FuncCenterPoint = GameObject.Find("NETCenterPoint"+num).gameObject;
    //        Debug.Log(num);
    //        if (FuncCenterPoint.GetComponent<netHowUse>().Use == false)
    //        {
    //            FuncCenterPoint.GetComponent<netHowUse>().Use = true;
    //            break;
    //        }
    //    }

    //    return FuncCenterPoint;
    //}

    [Command]
    public void CmdMakeCnter()
    {
        Center = (GameObject)Instantiate(CP, transform.position, transform.rotation);//カメラ作成
        NetworkServer.Spawn(Center);
        //NetWorkManagerCustomにもある
    }


}
