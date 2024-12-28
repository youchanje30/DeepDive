using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusInFace : Scenes
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        img = GetComponent<Image>();
        FocusFaceIn();
    }
    void FocusFaceIn()
    {
        isEnd = true;

        transform.DOScale(1, 2).OnComplete(() =>
        {
            Invoke("FocusIn", 1);
        });
    }
    void FocusIn()
    {
        img.GetComponent<RectTransform>().sizeDelta = Vector3.zero;
        SingleTone<GameManager>.Instance.OpenSmithy();
        Invoke("FocusFaceOut", 0.5f);

    }
    void FocusFaceOut()
    {
        img.GetComponent<RectTransform>().sizeDelta = new Vector3(300,300);
        transform.DOScale(11, 2).OnComplete(() =>
        {
            gameObject.SetActive(false);
            isEnd = false;

        });
    }
}
