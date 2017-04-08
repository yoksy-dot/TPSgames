using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class CharacterChoice : MonoBehaviour {
    //プレイヤー選択とシーン切り替え
    MovingScene Chenge;
    Toggle toggle;
    Slider slider;
    public GameObject ui,optionUI;
    public static int PlayerChoice = 1;
    public static bool network;

    public static float MouseSmooth = 2.0f;

    // Use this for initialization
    void Start () {
        Chenge = GetComponent<MovingScene>();
        toggle = ui.GetComponent<Toggle>();
        slider = optionUI.GetComponent<Slider>();

    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void Choice(int num)
    {
        PlayerChoice = num;
        if(!toggle.isOn)
            Chenge.ChengeScene(1);
        else if(toggle.isOn)
            Chenge.ChengeScene(2);

    }

    public void ExitSystem()
    {
        Application.Quit();
    }

    public void MouseSmoothSystem()
    {
        MouseSmooth = slider.value;
    }
}
