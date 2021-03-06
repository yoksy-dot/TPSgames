﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class NETShootbullet : NetworkBehaviour
{
    /*後々rootに移してNetworkIdentity消したい*/
    public GameObject Bullet1Prefab, SpecialPrefab;
    GameObject oyaoya;
    private bool Fire1, Fire2;

    public int ShootPlayer = 0;//設定したが親(PlayerData)で代入できるようにしました
    public int ShootLv = 1;//同上
    public bool useShoot;

    public float shotSpeed;//発射速度

    public AudioClip shotSound;//音
    private float time = 0f, time2 = 0f, time3 = 0f;

    public float BulletMax = 100;//装填弾数
    public float nowBullet;//残弾数
    public float ReloadTime = 4;//リロード時間
    private float RemainingTime;//リロード時間の残り
    public float interval = 10f;//弾の発射間隔
    public float Bure = 0.0f;//どれくらいぶれるか
    private bool CanShoot = true;//リロード中に打てないようにする(trueなら打てる)
    public bool rere = false;

    /*Fire2関連*/
    public bool UseFire2;//デバッグ用最後には消したい
    /*タイプはキャラで固定するので不要*/
    public float SPMax = 3.0f;//残弾数
    public float nowSP = 0;//残りの量 スタート時0から
    public float ReloadTimeSP = 0.01f;//SPリロード時間
    public float Effect = 5.0f;//効果量等
    public float EffectTime = 10.0f;//効果量等
    public float Distance = 5.0f;//有効距離等
    public float interval2 = 1.0f;//間隔
    public int ExATK = 0;//追加攻撃力分 レベルに加算
    private bool SpON = false;//スペシャルが起動しているかどうか
    private float beforespeed;//元の速度の保持

    UISetBullet ForUI;
    TextCtrl _text, _text2;
    //ShootbulletAuto AutoShooter;
    GunCtrl tar;
    NetPlayerData data;
    NETPlayerCtrl sss;
    void Start()
    {
        Fire1 = Fire2 = false;

        //UI(Slider)のコンポーネント
        ForUI = GetComponent<UISetBullet>();
        //テキストのコンポーネント
        _text = GameObject.Find("Canvas/Text").GetComponent<TextCtrl>();
        _text2 = GameObject.Find("Canvas/TextSP").GetComponent<TextCtrl>();//SP用
        
        nowBullet = BulletMax;
        RemainingTime = 0;
        tar = GetComponent<GunCtrl>();
        oyaoya = gameObject.transform.root.gameObject;//Charオブジェクト情報
        data = oyaoya.GetComponent<NetPlayerData>();//上のコンポーネント

        //速度変化用コンポーネント
        sss = oyaoya.GetComponentInChildren<NETPlayerCtrl>();
        beforespeed = sss.moveSpeed;
    }

    void Update()
    {
        
        //if (!isLocalPlayer)
        //{
        //    return;
        //}
        time3 += Time.deltaTime;
        time2 += Time.deltaTime;
        time += Time.deltaTime;
        _text.textCtrl(0, nowBullet + "/" + BulletMax);//弾の最大値と残弾の表示
        _text2.textCtrl(0, nowSP + "/" + SPMax);//SP表示
        if (rere)//リロード用カウント
        {
            RemainingTime += Time.deltaTime;
            float aaa = (RemainingTime / ReloadTime) * 100;
            ForUI.SetUISysytem("Canvas/BulletBarUI1", 100, aaa);//リロード中ゲージが伸びていく
            _text.textCtrl(0, "リロード中");
        }
        /*SPスライダー*/
        float bbb = (nowSP / SPMax) * 100;
        ForUI.SetUISysytem("Canvas/SpBarUI", 100, bbb);
        _text2.textCtrl(0, Mathf.Floor(nowSP) + "/" + SPMax);

        if (nowSP <= SPMax && nowSP >= 0)//SPを増やす
        {
            nowSP += ReloadTimeSP * Time.deltaTime;
        }
        else if (nowSP > SPMax)
        {
            nowSP = SPMax;
        }

        if (nowBullet <= 0)//残弾数が0なら撃たせない
        {
            CanShoot = false;
        }
        
        // もしも「Fire1」というボタンが押されたら（条件）
        if ((Input.GetButton("Fire1") || Input.GetMouseButton(0)) && time >= interval && CanShoot && sss.CanMove) 
        {
            //Debug.Log("fire1");
            tar.MoveReturn();//ブレ修正
            tar.MoveRandSystem(Bure);//ブレ発生
            // ①Shotという名前の関数（命令ブロック）を実行する。
            Fire1 = true;
            CmdShotOnly();

            // ②効果音を再生する。
            //AudioSource.PlayClipAtPoint(shotSound, transform.position);
            time = 0f;
            nowBullet -= 1;
            ForUI.SetUISysytem("Canvas/BulletBarUI1", BulletMax, nowBullet);//スライダー更新
        }
        else if ((Input.GetButton("Fire2") || Input.GetMouseButton(1)) && time2 >= interval2 && UseFire2 && nowSP >= 1 && sss.CanMove)
        //必殺枠(右クリック)
        {
            Debug.Log("fire2");
            Fire2 = true;
            nowSP--;
            SpON = true;
            time2 = 0f;
            time3 = 0f;
        }
        if (time3 >= EffectTime)//スペシャル終了
        {
            ExATK = 0;
            SpON = false;
            sss.moveSpeed = beforespeed;
        }
        if (data.NUM == 4 && SpON)//獣人SP用ステUP
        {
            if (data.nowHP > 0 && data.nowHP <= data.HP)//死んでるやつにはしない Maxにもしない
            {
                data.nowHP += Effect + ((data.Level - 1) * 1.0f) * Time.deltaTime;
                if (data.nowHP >= data.HP)//超えたらMaxにする
                    data.nowHP = data.HP;
            }
            ExATK = Mathf.FloorToInt(Effect) * 3;
        }
        else if (data.NUM == 3 && SpON)//重火器兵SP用速度UP
        {
            if (data.nowHP > 0 && data.nowHP <= data.HP)
            {
                sss.moveSpeed = Effect + ((data.Level - 1) * 1.0f);//速度変化
            }
        }

        if (Input.GetKey(KeyCode.R) && !rere && sss.CanMove)//リロード
        {
            CanShoot = false;
            rere = true;//カウント開始
            ForUI.SetUISysytem("Canvas/BulletBarUI1", 100, 0);//百分率にするための操作
        }

        if (ReloadTime <= RemainingTime)//リロード時間経過なら
        {
            CanShoot = true;
            nowBullet = BulletMax;
            RemainingTime = 0;//元に戻すこと
            rere = false;//元に戻すこと
            ForUI.SetUISysytem("Canvas/BulletBarUI1", BulletMax, BulletMax);//スライダー更新

        }
    }

    void FixedUpdate()
    {
        if (Fire1)
        {
            //AutoShooter.ShotOnly(transform, Bullet1Prefab, shotSpeed, ShootPlayer, ShootLv + ExATK);
            //CmdShotOnly();
            Fire1 = false;
        }
        if (Fire2)
        {
            CmdSPATK();
            Fire2 = false;
        }
    }

    //[Command]
    void CmdSPATK()
    {
        //スペシャル用switch文
        GameObject Special = (GameObject)Instantiate(SpecialPrefab, transform.position, transform.rotation);//ここでパーティクル等を
        NetworkServer.Spawn(Special);
        switch (data.NUM)
        {
            case 0://支援範囲攻撃
                /*効果量等は関係なし*/
                Special.transform.localRotation = Quaternion.Euler(0, 0, 0);
                SideAttackSystem arrow = Special.GetComponent<SideAttackSystem>();
                arrow.NUM = data.NUM;
                arrow.Lv = data.Level;
                break;
            case 1://範囲回復
                Special.transform.localRotation = Quaternion.Euler(0, 0, 0);
                StatsUper heal = Special.GetComponent<StatsUper>();
                heal.Lv = data.Level;
                heal.NUM = data.NUM;
                heal.interval = interval2;
                heal.Effect = Effect;
                heal.EffectTime = EffectTime;
                heal.Distance = Distance;
                break;
            case 2://近接範囲攻撃
                Rigidbody Bullet1Rigidbody = Special.GetComponent<Rigidbody>();
                Bullet BulletInfo = Special.GetComponent<Bullet>();
                BulletInfo.ShootPlayer = ShootPlayer;
                BulletInfo.ShootLv = ShootLv;
                break;
            case 3://速度UP
                Special.transform.localRotation = Quaternion.Euler(0, 0, 0);
                Special.transform.parent = oyaoya.transform;//親に設定
                //MMOPlayerCtrl
                break;
            case 4://自ステUP
                Special.transform.localRotation = Quaternion.Euler(0, 0, 0);
                Special.transform.parent = oyaoya.transform;//親に設定
                StatsUper StatsUp = Special.GetComponent<StatsUper>();
                StatsUp.Lv = data.Level;
                StatsUp.NUM = data.NUM;
                StatsUp.interval = interval2;
                StatsUp.Effect = Effect;
                StatsUp.EffectTime = EffectTime;
                StatsUp.Distance = Distance;
                break;
            case 5://手榴弾
                Rigidbody Bullet2Rigidbody = Special.GetComponent<Rigidbody>();
                Bullet bullet = Special.GetComponent<Bullet>();
                bullet.ShootPlayer = data.NUM;
                bullet.ShootLv = data.Level;
                Bullet2Rigidbody.AddForce(Special.transform.forward * Distance);//仕方ないのでDistance
                break;
        }
    }

    //弾作って飛ばすだけのスクリプト
    //[Command]
    public void CmdShotOnly()
    //player=-1なら敵の弾
    {

        // プレファブから砲弾(Bullet1)オブジェクトを作成し、それをBullet1という名前の箱に入れる。
        GameObject Bullet1 = (GameObject)Instantiate(Bullet1Prefab, transform.position, transform.rotation);
        NetworkServer.Spawn(Bullet1);
        // Rigidbodyの情報を取得し、それをBullet1Rigidbodyという名前の箱に入れる。
        Rigidbody Bullet1Rigidbody = Bullet1.GetComponent<Rigidbody>();
        /*射撃者特定用*/
        Bullet BulletInfo = Bullet1.GetComponent<Bullet>();
        BulletInfo.ShootPlayer = ShootPlayer;
        BulletInfo.ShootLv = ShootLv + ExATK;

        // Bullet1Rigidbodyにz軸方向の力を加える。
        Bullet1Rigidbody.AddForce(transform.forward * shotSpeed);

    }
}