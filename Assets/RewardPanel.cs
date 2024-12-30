using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{

    public GameObject[] objs;

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

        for (int ci = 0; ci < 3; ci++)
        {
            objs[ci].SetActive(true);
        }
        
        for (; i < 3; ++i)
        {
            objs[i].SetActive(false);
        }
        
    }
}
