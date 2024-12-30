using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ClickTo : MonoBehaviour
{

    TMP_Text text;
    
    void OnEnable()
    {
        text = GetComponent<TMP_Text>();

        text.DOFade(0.7f, 0.8f).OnComplete(() => Fade(1f));
    }

    void Fade(float val)
    {
        text.DOFade(val, 0.8f).OnComplete(() => Fade(1.7f - val));
    }

    
}
