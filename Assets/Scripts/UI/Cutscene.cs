using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Cutscene : Scenes
{

    float a = 0;


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>

    private void OnEnable()
    {
        Black();

    }
    public void Update()
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, a);
    }

    [ContextMenu("Black")]
    public void Black()
    {
        isEnd = true;

        DOTween.To(() => a, x=> a = x, 1f, 2f).OnComplete(() =>
        {
            SingleTone<GameManager>.Instance. ViewChange();
            White();
        });
        
    }

    [ContextMenu("White")]
    public void White()
    {
        DOTween.To(() => a, x => a = x, 0f, 0.5f).OnComplete(() => { gameObject.SetActive(false);
        isEnd = false;
        }); // .OnComplete(() => Black());
    }




}
