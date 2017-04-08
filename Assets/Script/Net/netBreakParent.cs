using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class netBreakParent : MonoBehaviour {
    private bool first;

	// Use this for initialization
	void Start () {
        transform.DetachChildren();
        //Destroy(this);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
