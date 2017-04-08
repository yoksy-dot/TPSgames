using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextCtrl : MonoBehaviour {

	Text myText;

	/*     ＜使い方＞
	 *  使う場所のStartに下記のスクリプトを書く
	 *  aaaa = GameObject.Find("Canvas/OOOO").GetComponent<TextCtrl>();
	 *  このスクリプトをTextのコンポーネントにして
	 *  textCtrlに引数入れて呼ぶだけ
	 */

	// Use this for initialization
	void Start () {
		myText = GetComponent<Text>();
		myText.text = "未設定";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void textCtrl(int num,string str)
		/*書きたい文字をstrに*/
	{
		switch (num)
		{
			case 0:
				myText.text = str;
				break;
			case 1:
				break;

			default:
				myText.text = "テキスト設定がおかしいです";
				break;
		}
	}
}
