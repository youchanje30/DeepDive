using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Cutscene : CutScenes
{
    public Image img;

    float a = 0;


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    public void Run()
    {
        img = GameObject.Find("Production/Panel").GetComponent<Image>();
        Debug.Log("ÀÛµ¿Áß?");
        Black();

    }
    public void Update()
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, a);
    }

    [ContextMenu("Black")]
    public void Black()
    {
        DOTween.To(() => a, x=> a = x, 1f, 2f).OnComplete(() => White());
        
    }

    [ContextMenu("White")]
    public void White()
    {
        DOTween.To(() => a, x=> a = x, 0f, 0.5f); // .OnComplete(() => Black());
    }




}
