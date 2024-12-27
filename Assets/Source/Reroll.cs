using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reroll : MonoBehaviour
{
    public BuyItem[] items;
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(Reset);
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
        for(int i = 0; i < items.Length; i++)
        {
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
                break;
            }
            idList.Add(ID);
        }
    }

}
