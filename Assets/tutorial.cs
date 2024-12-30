using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tutorial : MonoBehaviour
{
    int cur_i = 0;
    public Sprite[] sprites;
    public Image main;

    void Start()
    {
        cur_i  = 0;
        main.sprite = sprites[cur_i];
    }

    public void Move(int i)
    {
        cur_i = Math.Clamp(cur_i + i, 0, 5);
        if (cur_i == 5)
        {
            SceneManager.LoadScene("TestGameScene");
            return;
        }
        main.sprite = sprites[cur_i];
    }

    
            // 
}
