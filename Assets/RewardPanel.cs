using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{
    public UnityEngine.UI.Image[] images;
    public TMP_Text[] texts;

    



    public void Setting(int earn_coins, Dictionary<int, int> earn_data)
    {
        List<int> data = new List<int>();
        texts[0].text = earn_coins.ToString();


        int i = 1;
        foreach (var item in earn_data)
        {
            if (item.Value <= 0) continue;

            images[i].sprite = SingleTone<GameManager>.Instance.GetSpriteOfID(item.Key);
            texts[i++].text = item.Value.ToString();
        }
        
        for (; i < 3; ++i)
        {
            images[i].gameObject.SetActive(false);
        }
        
    }
}