using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Reroll : MonoBehaviour
{
    public BuyItem[] items;
    public TMP_Text[] texts;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(Reset);
    }
    private void OnEnable()
    {
        Debug.Log("리로?");
        Reset();
    }
    private void Reset()
    {
        // Audio
        SingleTone<AudioManager>.Instance.PlaySfx(Sfx.reroll);

        List<int> idList = new List<int>();
        for (int i = 0; i < items.Length; i++)
        {
            Debug.Log("확인");
            items[i].Reroll();
            Debug.Log("aaa확인");
        }
        for (int i = 0; i < items.Length; i++)
        {
            Debug.Log("확인aan");
            int ID = items[i].GetID();
            if (idList.Contains(ID))
            {
                Debug.Log($"Duplicate ID found :{ID}");
                while (idList.Contains(ID))
                {
                    Debug.Log("���ư�����-----" + ID);
                    items[i].Reroll();
                    ID = items[i].GetID();
                }
            }
            Debug.Log("확인nn");
            texts[i].text = GetTextOfID(items[i].GetID());
            idList.Add(ID);
            
        }
    }



    public String[] textOfID;
    private string GetTextOfID(int id)
    {
        return textOfID[id];
    }
}
