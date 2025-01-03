using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneShutter : Scenes
{

    private void OnEnable()
    {
        img = GetComponent<Image>();
        ShutterDown();
    }
    void ShutterDown()
    {
        DOTween.Kill(this);
        isEnd = true;
        img.transform.DOLocalMoveY(0, 1.2f).SetEase(Ease.InOutQuart).OnComplete(() => 
        { 
            SingleTone<GameManager>.Instance.ViewChange();    
            Invoke("ShutterUp",0.5f); 
        });

    }
    void ShutterUp()
    {
        img.transform.DOLocalMoveY(1080, 1.2f).SetEase(Ease.InOutQuart).OnComplete(() => {
            gameObject.SetActive(false); 
            isEnd = false;
        });
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
