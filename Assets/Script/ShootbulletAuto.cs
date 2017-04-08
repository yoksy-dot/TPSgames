using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootbulletAuto : MonoBehaviour {

	//弾作って飛ばすだけのスクリプト

	public void ShotOnly(Transform qwe, GameObject Prefab, float Speed,int player,int Lv)
	//player=-1なら敵の弾
	{

		// プレファブから砲弾(Bullet1)オブジェクトを作成し、それをBullet1という名前の箱に入れる。
		GameObject Bullet1 = (GameObject)Instantiate(Prefab, qwe.position, qwe.rotation);

		// Rigidbodyの情報を取得し、それをBullet1Rigidbodyという名前の箱に入れる。
		Rigidbody Bullet1Rigidbody = Bullet1.GetComponent<Rigidbody>();
		/*射撃者特定用*/
		Bullet BulletInfo = Bullet1.GetComponent<Bullet>();
		BulletInfo.ShootPlayer = player;
		BulletInfo.ShootLv = Lv;

		// Bullet1Rigidbodyにz軸方向の力を加える。
		//Bullet1.transform.Rotate(90f, 0, 0);//なんか縦向きで飛んでいくので回転させる(カプセル型限定)
		Bullet1Rigidbody.AddForce(qwe.forward * Speed);

	}

	//public void PutParticle(Transform qwe, GameObject Prefab)
	////パーティクル等置くだけならこちら
	//{
	//	GameObject Particle = (GameObject)Instantiate(Prefab, qwe.position, qwe.rotation);
	//}
}
