using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParicle : MonoBehaviour {

	ParticleSystem myParticleSystem, myParticleSystem2;
	GameObject chiled;
	public bool UseSub;

	// Use this for initialization
	void Start () {
		myParticleSystem = GetComponent<ParticleSystem>();

		if (UseSub)
		{
			chiled = transform.FindChild("SubEmitter0").gameObject;
			myParticleSystem2 = chiled.GetComponent<ParticleSystem>();
		}


	}
	
	// Update is called once per frame
	void Update () {
		if (!UseSub && myParticleSystem != null && myParticleSystem.particleCount == 0)
		{
			Destroy(gameObject);
			
		}
		if(UseSub&& myParticleSystem != null && myParticleSystem.particleCount == 0 && myParticleSystem2 != null && myParticleSystem2.particleCount == 0)
		{
			Destroy(gameObject);
			Destroy(chiled);
		}
	}
}
