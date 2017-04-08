using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NETAnimationCtrl : MonoBehaviour
{
    public NETShootbullet Shooter;
    private Animator anim;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))//移動中
        {
            anim.SetTrigger("MiniJump");
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }


        if (Input.GetButton("Fire1") || Input.GetMouseButton(0))//マウス右クリ
        {
            anim.SetBool("Shoot", true);
        }
        else
        {
            anim.SetBool("Shoot", false);
        }

        if (Input.GetMouseButton(1))//マウス左クリ
        {
            anim.SetTrigger("Special");
        }


        if (Input.GetKeyDown(KeyCode.R))//リロード
        {
            anim.SetBool("Reload", true);
        }
        else if (Shooter.rere)
        {
            anim.SetBool("Reload", true);
        }
        else
        {
            anim.SetBool("Reload", false);
        }
    }
}
