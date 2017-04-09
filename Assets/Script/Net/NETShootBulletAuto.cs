using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class NETShootBulletAuto : NetworkBehaviour
{

    //弾作って飛ばすだけのスクリプト
    [Command]
    public void CmdShotOnly(GameObject qwe, GameObject Prefab, float Speed, int player, int Lv)
    //player=-1なら敵の弾
    {

        // プレファブから砲弾(Bullet1)オブジェクトを作成し、それをBullet1という名前の箱に入れる。
        GameObject Bullet1 = (GameObject)Instantiate(Prefab, qwe.transform.position, qwe.transform.rotation);
        NetworkServer.Spawn(Bullet1);
        // Rigidbodyの情報を取得し、それをBullet1Rigidbodyという名前の箱に入れる。
        Rigidbody Bullet1Rigidbody = Bullet1.GetComponent<Rigidbody>();
        /*射撃者特定用*/
        Bullet BulletInfo = Bullet1.GetComponent<Bullet>();
        BulletInfo.ShootPlayer = player;
        BulletInfo.ShootLv = Lv;

        // Bullet1Rigidbodyにz軸方向の力を加える。
        //Bullet1.transform.Rotate(90f, 0, 0);//なんか縦向きで飛んでいくので回転させる(カプセル型限定)
        Bullet1Rigidbody.AddForce(qwe.transform.forward * Speed);

    }
}