using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    int itemcnt;
    public int ID;
    TMP_Text tmp;
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(BuyItems);
        tmp = GetComponentInChildren<TMP_Text>();
        Reroll();
    }

    void BuyItems()
    {
        if (SingleTone<GameManager>.Instance.IsExistCoins(ID, itemcnt))
        {
            SingleTone<GameManager>.Instance.AddMaterials(ID, itemcnt);
            SingleTone<GameManager>.Instance.UseCoins(ID, itemcnt);

            itemcnt = 0;
            tmp.text = itemcnt.ToString();
        }
    }
    public void UpItemCount()
    {
        itemcnt++;
        tmp.text = itemcnt.ToString();
    }
    public void DownItemCount()
    {
        if (itemcnt > 0)
        {
            itemcnt--;
            tmp.text = itemcnt.ToString();
        }
    }
    public void Reroll()
    {
        itemcnt = 0;
        tmp.text = itemcnt.ToString();
        ID = 8 - Mathf.FloorToInt(Mathf.Sqrt(UnityEngine.Random.Range(0, 64 + 1)));
    }
    public int GetID()
    {
        return ID;
    }
}
