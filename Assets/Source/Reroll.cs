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
        for (int i = 0; i < items.Length; i++)
        {
            items[i].InitCnt();
        }
        Reset();
    }
    private void Reset()
    {
        // Audio
        SingleTone<AudioManager>.Instance.PlaySfx(Sfx.reroll);

        List<int> idList = new List<int>();
        for (int i = 0; i < items.Length; i++)
        {
            items[i].Reroll();
        }
        for (int i = 0; i < items.Length; i++)
        {
            int ID = items[i].GetID();
            if (idList.Contains(ID))
            {
                int attempts = 0; 
                int maxAttempts = 100;

                Debug.Log($"Duplicate ID found :{ID}");
                while (idList.Contains(ID))
                {if(attempts >= maxAttempts)
                    {
                        break;
                    }
                    items[i].Reroll();
                    ID = items[i].GetID();
                    attempts++;
                }
            }
            texts[i].text = GetTextOfID(items[i].GetID());
            idList.Add(ID);
            
        }
    }



    private string GetTextOfID(int id)
    {
        return SingleTone<GameManager>.Instance.GetTextOfID(id);
    }
}
