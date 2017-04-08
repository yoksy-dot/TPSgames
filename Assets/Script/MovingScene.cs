using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MovingScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChengeScene(int num)
	{
		switch (num)
		{
			case 0:
				SceneManager.LoadScene("CharacterChoice");
				break;
			case 1:
				SceneManager.LoadScene("test_stage");
				break;
			case 2:
				SceneManager.LoadScene("Net_stage");
				break;
		}
	}
}
