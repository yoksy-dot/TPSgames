using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class netPlayerSetUp : NetworkBehaviour
{

    public CharacterController charctrl;
    public NETPlayerCtrl playerctrl;
    public Camera maincamera;
    public AudioListener audioListener;
    public SpriteRenderer reder;
    public NETShootbullet shooter;
    public GunCtrl gun;
    public Animator anim;
    public NETAnimationCtrl animctrl;

    // Use this for initialization
    void Start () {
        if (isLocalPlayer)//自分のやつなら
        {
            charctrl.enabled = true;
            playerctrl.enabled = true;

            maincamera.enabled = true;
            audioListener.enabled = true;
            reder.enabled = true;

            shooter.enabled = true;
            gun.enabled = true;

            anim.enabled = true;
            animctrl.enabled = true;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
