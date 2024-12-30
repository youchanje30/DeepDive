using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class OpeningController : MonoBehaviour
{
    public TMP_Text[] texts;
    public SpriteRenderer img;

    public TMP_Text touchTO;

    public GameObject Tuto;

    bool is_fin = false;

    private void Update() {
        if(!is_fin) return;

        if(Input.anyKeyDown) 
        {
            Tuto.SetActive(true);
            is_fin = !is_fin;
            // SceneManager.LoadScene("TestGameScene");
        }
    }

    void Start()
    {
        DOTween.To(() => "", x=>texts[0].text=x, "유일한 대장장이이자 초 절정 미녀 공주가", 2.1f).OnComplete(() => Function1());
    }

    void Function1()
    {   
        texts[0].DOFade(0f, 0.5f);
        DOTween.To(() => "", x=>texts[1].text=x, "어릴적에 마왕을 꼬셨었다고?!?!!!!!!!", 2.1f).OnComplete(() => Function2());
    }
    
    void Function2()
    {
        texts[1].DOFade(0f, 0.5f);
        DOTween.To(() => "", x=>texts[2].text=x, "결혼을 걸고서라도 왕국을 지켜야 해!!", 2.1f).OnComplete(() => Function3());
    }
    
    void Function3()
    {
        texts[2].DOFade(0f, 0.5f);
        DOTween.To(() => "", x=>texts[3].text=x, "그 말 지켜야한다????", 2f).OnComplete(() => Function4());
    }


    void Function4()
    {
        texts[3].DOFade(1f, 0.5f).OnComplete(() => 
        texts[3].DOFade(0f, 0.5f)).OnComplete(() => Function5());
    }

    void Function5()
    {
        img.DOFade(1f, 1f).OnComplete(() => 
        {
            touchTO.gameObject.SetActive(true);
            is_fin = true;
        });
    }
}