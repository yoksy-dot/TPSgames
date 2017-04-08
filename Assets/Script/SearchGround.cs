using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchGround : MonoBehaviour
{

    public bool StayGround;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Stage")
        {
            StayGround = true;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Stage")
        {
            StayGround = false;
        }
    }
}
