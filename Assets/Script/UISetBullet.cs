using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UISetBullet : MonoBehaviour {
	Slider _slider;


	// Use this for initialization
	void Start () {
		
		


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetUISysytem(string name, float Maxnum ,float Nownum)//スライダー関係は全部これで
	{
		_slider = GameObject.Find(name).GetComponent<Slider>();//スライダー取得
		_slider.maxValue = Maxnum;
		_slider.value = Nownum;

	}

}
