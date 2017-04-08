using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class netPlayerSetUp : NetworkBehaviour
{


    public Camera maincamera;
    public AudioListener audioListener;
    public SpriteRenderer reder;
    [SerializeField]
    NETShootbullet shooter;
    [SerializeField]
    GunCtrl gun;

    // Use this for initialization
    void Start () {
        if (isLocalPlayer)//自分のやつなら
        {
            GetComponent<CharacterController>().enabled = true;
            GetComponent<NETPlayerCtrl>().enabled = true;

            maincamera.enabled = true;
            audioListener.enabled = true;
            reder.enabled = true;

            shooter.enabled = true;
            gun.enabled = true;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
