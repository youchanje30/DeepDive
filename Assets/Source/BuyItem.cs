using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    int itemcnt;
    public int ID;
    TMP_Text tmp;
    bool isbuy = false;
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(BuyItems);

    }
    private void Awake()
    {
        tmp = GetComponentInChildren<TMP_Text>();

    }
    private void OnEnable()
    {


        //Reroll();
    }
    public void InitCnt()
    {
        itemcnt = 0;
        isbuy = false;
    }
    void BuyItems()
    {
        // Audio
        if (!isbuy)
        {
            if (SingleTone<GameManager>.Instance.IsExistCoins(ID, itemcnt)&& itemcnt != 0)
            {
                SingleTone<GameManager>.Instance.AddMaterials(ID, itemcnt);
                SingleTone<GameManager>.Instance.UseCoins(ID, itemcnt);
            SingleTone<AudioManager>.Instance.PlaySfx(Sfx.buy);

                tmp.text = itemcnt.ToString();
                isbuy = true;
            }
            else
            {
                Debug.Log("no money");
            }
        }
    }
    public void UpItemCount()
    {
        if (!isbuy)
        {
            itemcnt++;
            tmp.text = itemcnt.ToString();
        }
    }
    public void DownItemCount()
    {
        if (!isbuy)
        {
            if (itemcnt > 0)
            {
                itemcnt--;
                tmp.text = itemcnt.ToString();
            }
        }
    }
    public void Reroll()
    {
        if (!isbuy)
        {
            itemcnt = 0;
            if (tmp != null)
            {
                tmp.text = itemcnt.ToString();
            }
            ID = 7 - Mathf.FloorToInt(Mathf.Sqrt(UnityEngine.Random.Range(0, 63 + 1)));
        }
    }
    public int GetID()
    {
        return ID;
    }
}
