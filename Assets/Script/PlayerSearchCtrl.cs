using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSearchCtrl : MonoBehaviour
{
    /*プレイヤー用敵捜索スクリプト*/
    private GameObject parent;

    public bool FindEnemy;
    public bool FindPlayer;
    public GameObject SearchedCharP, SearchedCharE;

    // Use this for initialization
    void Start()
    {
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        SearchedCharP = null;
        SearchedCharE = null;
        FindPlayer = false;
    }



    void OnTriggerStay(Collider coll)
    {
        
        if (coll.gameObject.tag == "Enemy")
        {
            FindEnemy = true;
            transform.parent.LookAt(coll.gameObject.transform);//範囲内ならプレイヤーの方向を向く
        }

        if (coll.gameObject.tag == "Player")
        {
            SearchedCharP = coll.gameObject;
            FindPlayer = true;
        }

    }

    void OnTriggerExit(Collider coll)
    {
        
        if (coll.gameObject.tag == "Enemy")
        {

            FindEnemy = false;
        }

        if (coll.gameObject.tag == "Player")
        {
            SearchedCharP = coll.gameObject;
            FindPlayer = false;
        }
    }
}
