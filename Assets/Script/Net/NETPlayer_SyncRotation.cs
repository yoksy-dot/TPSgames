using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
//Listを使うためのライブラリ
using System.Collections.Generic;

public class NETPlayer_SyncRotation : NetworkBehaviour
{

    //Quaternion型からfloat型に修正
    //hook: SyncVar変数が変更された時に、SyncVar変数を引数にして指定したメソッドを実行
    [SyncVar(hook = "OnPlayerRotSynced")]
    private float syncPlayerRotation;
    [SyncVar(hook = "OnCamRotSynced")]
    private float syncCamRotation;

    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform camTransform;
    //Serialize Fieldを削除
    private float lerpRate = 17;
    //Quaternion型からfloat型(オイラー角。360度のやつ。)に変更
    private float lastPlayerRot;
    private float lastCamRot;
    //しきい値を1に変更
    private float threshold = 1;
    //角度保存用のList
    private List<float> syncPlayerRotList = new List<float>();
    private List<float> syncCamRotList = new List<float>();
    //HistoricalInterpolationで角度の判定に使用
    private float closeEnough = 0.4f;

    void Update()
    {
        LerpRotations();
    }

    void FixedUpdate()
    {
        TransmitRotations();
    }

    void LerpRotations()
    {
        if (!isLocalPlayer)
        {
            //UNETを使用した角度同期判定
            OrdinaryLerping();

        }
    }


    void OrdinaryLerping()
    {
        LerpPlayerRotation(syncPlayerRotation);
        LerpCamRotation(syncCamRotation);
    }

    //プレイヤーの現在角度を補間
    void LerpPlayerRotation(float rotAngle)
    {
        //引数のオイラー角を、Vector3の形で保存
        Vector3 playerNewRot = new Vector3(0, rotAngle, 0);
        //Lerp: 現在の角度と受け取った角度の補間値を求める
        //Euler: オイラー角をQuaternion角に変換してくれる
        playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, Quaternion.Euler(playerNewRot), lerpRate * Time.deltaTime);
    }

    //カメラの現在角度を補間
    void LerpCamRotation(float rotAngle)
    {
        Vector3 camNewRot = new Vector3(rotAngle, 0, 0);
        //カメラは子オブジェクトのため、親オブジェクトの向きを継承するlocalRotationを使う
        camTransform.localRotation = Quaternion.Lerp(camTransform.localRotation, Quaternion.Euler(camNewRot), lerpRate * Time.deltaTime);
    }

    [Command]
    //引数をfloat型(オイラー角)に修正
    void CmdProvideRotationsToServer(float playerRot, float camRot)
    {
        syncPlayerRotation = playerRot;
        syncCamRotation = camRot;
    }

    [Client]
    void TransmitRotations()
    {
        if (isLocalPlayer)
        {
            //			if (Quaternion.Angle (playerTransform.rotation, lastPlayerRot) > threshold ||
            //			   Quaternion.Angle (camTransform.rotation, lastCamRot) > threshold) {

            //localEulerAngles: Quaternion角をオイラー角(普通の360度)で回転量を表す
            if (CheckIfBeyondThreshold(playerTransform.localEulerAngles.y, lastPlayerRot) ||
                CheckIfBeyondThreshold(camTransform.localEulerAngles.x, lastCamRot))
            {
                //lastPlayerRotとlastCamRotを現在角度に更新
                lastPlayerRot = playerTransform.localEulerAngles.y;
                lastCamRot = camTransform.localEulerAngles.x;
                //現在角度に更新したlastPlayerRotとlastCamRotでメソッド実行
                CmdProvideRotationsToServer(lastPlayerRot, lastCamRot);
            }
        }
    }

    //現在角度と前フレームのオイラー角を比較し、threshold(1度)以上開きがあったらtrueを返す
    bool CheckIfBeyondThreshold(float rot1, float rot2)
    {
        //Mathf.Abs: 絶対値取得
        if (Mathf.Abs(rot1 - rot2) > threshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //syncPlayerRotation変数が変更された時に実行(hook)
    //Clientのみ実行
    [Client]
    void OnPlayerRotSynced(float latestPlayerRot)
    {
        //hookは自分で同期する必要がある
        syncPlayerRotation = latestPlayerRot;
        //Listに登録
        syncPlayerRotList.Add(syncPlayerRotation);
    }

    //syncCamRotation変数が変更された時実行(hook)
    //Clientのみ実行
    [Client]
    void OnCamRotSynced(float latestCamRot)
    {
        //hookは自分で同期する必要がある
        syncCamRotation = latestCamRot;
        //Listに登録
        syncCamRotList.Add(syncCamRotation);
    }
}