using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject BombPrefab, Particle;
    private GameObject Bomb,Part;

    //private EnemyData data;

    public int ShootPlayer,ShootLv;
    public float ATK = 1.0f;
    public float Breaktime = 5.0f;//消滅時間
    //public float Breakdistance = 0.0f;//射程

    public bool bomb, useParticle, Rebound;

    private float count = 0.0f;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ShootLv);
        count += Time.deltaTime;
        if (count > Breaktime && this.gameObject.tag == "Bullet")/*プレハブごとにcountを初期化していく*/
        {
            Destroy(gameObject);
            if (bomb)
            {
                Bomb = (GameObject)Instantiate(BombPrefab, transform.position, transform.rotation);
                /*射撃者特定用*/
                BombSystem BulletInfo = Bomb.GetComponent<BombSystem>();
                BulletInfo._ShootPlayer = ShootPlayer;
                BulletInfo._ShootLv = ShootLv;
            }
            count = 0;

            if (useParticle)
            {
                Part = (GameObject)Instantiate(Particle, transform.position, transform.rotation);//パーティクル発生
                Part.transform.Rotate(90f, 0, 0);
            }
        }

        if(count > Breaktime && this.gameObject.tag == "EnemyBullet")//敵の弾のとき
        {
            Destroy(gameObject);
            if (bomb)
            {
                Bomb = (GameObject)Instantiate(BombPrefab, transform.position, transform.rotation);
                /*射撃者特定用*/
                BombSystem BulletInfo = Bomb.GetComponent<BombSystem>();
                BulletInfo._ShootPlayer = -1;//敵は-1
                //BulletInfo._ShootLv = ShootLv;
            }
            count = 0;
        }
    }

    void OnCollisionEnter(Collision coll)//普通の弾用
    {
        if (coll.gameObject.tag == "Stage" && !Rebound)//ステージに当たった時
        {
            Destroy(gameObject);
            if (bomb)//爆発する弾なら
            {
                Bomb = (GameObject)Instantiate(BombPrefab, transform.position, transform.rotation);//プレハブ作成
                /*射撃者特定用*/
                BombSystem BulletInfo = Bomb.GetComponent<BombSystem>();
                BulletInfo._ShootPlayer = ShootPlayer;
                BulletInfo._ShootLv = ShootLv;
                Destroy(gameObject);//削除
            }
        }
        else if (coll.gameObject.tag == "Enemy")//敵に当たった
        {

            Destroy(gameObject);
            if (bomb)//爆発する弾なら
            {
                Bomb = (GameObject)Instantiate(BombPrefab, transform.position, transform.rotation);//プレハブ作成
                /*射撃者特定用*/
                BombSystem BulletInfo = Bomb.GetComponent<BombSystem>();
                BulletInfo._ShootPlayer = ShootPlayer;
                BulletInfo._ShootLv = ShootLv;
                Destroy(gameObject);//削除
             }

        }
        else if (coll.gameObject.tag == "Castle" /*&& !Rebound*/)
        {

        }

        if (coll.gameObject.tag == "Player" /*&& !Rebound*/)//敵の弾がプレイヤーに当たった時
        {
            Destroy(gameObject);
        }

    }

}
