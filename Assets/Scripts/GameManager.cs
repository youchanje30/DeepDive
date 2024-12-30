using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;

public class GameManager : SingleTone<GameManager>
{
    #region is shoud be removed
    public GameObject morning;
    public Slider bar;
    bool isTalking = false;
    bool isMaking = false;
    bool isWaiting = false;
    public GameObject talkingPanel;
    bool isThank = false;
    bool isBreak = false;

    public GameObject swordMaker;
    public GameObject hammer;

    void Update()
    {
        bar.value = monsters;


        if (Input.GetMouseButtonDown(0))
        {
            if (cutscene.IsWork() || is_night || isTalking || isMaking) return;

            if (survived_heros.Count == 0 && !isWaiting)
                ChangeTime();
            else
            {
                if (isThank)
                {
                    CheckRemainWeaponNeedHero();
                    isThank = false;
                    return;
                }
                if (isBreak)
                {
                    cutscene.NextScene();
                }
                else
                {
                    Debug.Log("1");
                    CheckRemainWeaponNeedHero();
                }
            }
        }

    }
    public void NextScenes(){
        cutscene.NextScene();
    }

    public Vector2 pos = Vector2.zero;
    public void HammerPos(Vector2 _pos)
    {
        pos = _pos;
    }

    public void Hammer()
    {
        GameObject ham = Instantiate(hammer);
        ham.transform.position = pos + new Vector2(-0.8f, 0.2f);
        Debug.Log(pos);
    }
    public GameObject Weddingd;
    public void Winner()
    {
        List<HeroBase> heroBases = new List<HeroBase>();
        while (survived_heros.Count > 0)
        {
            heroBases.Add(survived_heros.Dequeue());
        Debug.Log("0.1");
            Debug.Log(heroBases.Count);
        }
        Debug.Log("0.2");
        if (heroBases.Count > 0)
        {
            int random = UnityEngine.Random.Range(0, heroBases.Count);
            HeroBase winner = heroBases[random];
            if(winner != null)
            {
            Wedding(winner);

            }
            else
            {
                Debug.LogError("winner없음");
            }
        }
    }
    #endregion
    public void Wedding(HeroBase skin)
    {
        Weddingd.gameObject.SetActive(true);
        WeddingHero(skin);
    }
    private void WeddingHero(HeroBase hero)
    {
        GameObject parentObj = GameObject.Find("wedding/Ending Panel/Character3");
        if (parentObj != null)
        {
            parentObj.SetActive(true);
            foreach (Transform child in parentObj.transform)
            {
                string childname = child.name;
                Sprite randomSprite = hero.GetSprite(childname);
                if (randomSprite == null)
                {
                    child.gameObject.SetActive(false);
                    continue;
                }
                Image transimage = child.GetComponent<Image>();
                if (transimage != null)
                {
                    child.gameObject.SetActive(true);

                    transimage.sprite = randomSprite;
                    transimage.SetNativeSize();
                        GameObject princess = GameObject.Find("wedding/Ending Panel/Character1/body");
                    if (transimage.sprite.name.ToLower().Contains("Handsome"))
                    {
                        princess.GetComponent<Image>().sprite = Resources.Load<Sprite>("hime_yes_dummy");
                    }
                    else
                    {
                        princess.GetComponent<Image>().sprite = Resources.Load<Sprite>("hime_no_dummy");

                    }
                }
            }
        }
    }
    public void ViewChange()
    {
        Debug.Log("I Did");
        if(smithy.activeSelf)
        {
            CloseSmithy();
        }
        else
        {
            OpenSmithy();
        }
    }

    public void CloseSmithy()
    {
        smithy.gameObject.SetActive(false);
        anvilBG.SetActive(false);
        morning.SetActive(true);
        isMaking = false;

    }

    public void OpenSmithy()
    {
        // swordMaker.SetActive(true);
        smithy.gameObject.SetActive(true);
        anvilBG.SetActive(true);
        morning.SetActive(false);
        isMaking = true;
    }
    public AllScene cutscene;


    private bool is_night;

    [SerializeField] private int monsters;
    [SerializeField] private int max_monsters;
    [SerializeField] private int increase_monsters;
    [SerializeField] private float multiple_monsters;


    [Header("Shop Data")]
    #region About Shop Datas
    private HeroBase cur_hero;
    [SerializeField] TMP_Text hero_Text;
    [SerializeField] TMP_Text reward_Text;
    [SerializeField] GameObject reward_Panel;
    [SerializeField] int[] prices;
    #endregion
    [Space(10)]


    [Header("Hero Data")]
    #region About Hero Datas
    public Queue<HeroBase> heros = new Queue<HeroBase>(); // BaseHero
    public int hero_increase_day;
    [SerializeField] int hero_max_cnt;
    private int cur_day = 0;
    public Queue<HeroBase> survived_heros = new Queue<HeroBase>();
    #endregion


    #region  Material & Coins Data
    [Space(10)]
    [Header("Material and Coins Data")]
    public MaterialData materialData;
    [Serializable]
    public class MaterialData
    {
        public int coins;
        public int[] nums = new int[16];
        public int[] prices = new int[16];
    }
    #endregion


    #region Material & Coin Functions
    /// <summary>
    /// add Material with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="val"></param>
    public void AddMaterials(int id, int val) => materialData.nums[id] += val;

    /// <summary>
    /// use material with id, not except check
    /// </summary>
    /// <param name="id"></param>
    /// <param name="val"></param>
    public void UseMaterials(int id, int val) => materialData.nums[id] -= val;

    /// <summary>
    /// is exist material more val 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="val"></param>
    public bool IsExistMaterials(int id, int val = 0)
    {
        return materialData.nums[id] >= val;
    }

    public void CheckData(int id, Inventory obj)
    {
        for (int i = 0; i < 8; i++)
        {
            if (itemDatas[id].data[i] > 0)
            {
                obj.Move(i);
            }
        }

    }


    public void BuySword(int id)
    {
        EarnCoins(prices[id]);
        for (int i = 0; i < 8; i++)
        {
            materialData.nums[i] -= itemDatas[id].data[i];
        }
    }

    public bool CanBuySword(int id)
    {
        for (int i = 0; i < 8; i++)
        {
            // Debug.Log(i);
            if (!IsExistMaterials(i, itemDatas[id].data[i])) return false;
        }
        return true;
    }

    public void GetMaterials(int earn_coins, Dictionary<int, int> earn_data)
    {
        EarnCoins(earn_coins);
        foreach (var item in earn_data)
        {
            AddMaterials(item.Key, item.Value);
        }
    }


    public bool IsExistCoins(int id, int val)
    {
        return materialData.coins > materialData.prices[id] * val;
    }
    public void UseCoins(int id, int val) => materialData.coins -= materialData.prices[id] * val;
    public void EarnCoins(int val) => materialData.coins += val;
    #endregion
    public GameObject shop;
    void Start()
    {
        Weddingd.gameObject.SetActive(false);
        smithy.gameObject.SetActive(false);
        shop.gameObject.SetActive(false);
        bar.maxValue = max_monsters;
        cur_day = 0;
        is_night = true;
        // monsters = 10;
        ChangeTime();
    }

    public void ChangeTime()
    {
        // except process
        if (!is_night && survived_heros.Count > 0) return;


        is_night = !is_night;
        // Debug.Log(heros.Count);

        morning.SetActive(!is_night);
        if (is_night)
        {
            Combat();
            IncreaseMonsters();
            CheckClear();
            OpenShop();
            // Debug.Log("?");
        }
        else
        {
            shop.SetActive(false);
            cur_day++;
            ComeNewHero();
            CheckRemainWeaponNeedHero();
        }
    }




    #region About Hero Process

    private void ComeNewHero()
    {
        if (!(cur_day >= hero_increase_day && heros.Count < hero_max_cnt)) return;

        cur_day = 0;
        AddRandomHero();
    }

    public void AddNeedSword(HeroBase hero)
    {
        // survived_heros.Enqueue(hero);
    }


    private void AddRandomHero()
    {
        var hero = GetHero();
        AddHero(hero);
        survived_heros.Enqueue(hero);
        // AddNeedSword(hero);
    }

    public void AddHero(HeroBase hero)
    {
        heros.Enqueue(hero);
    }
    private void ChangeHero(HeroBase hero)
    {
        GameObject parentObj = GameObject.Find("Main/BgCanvas/Character");
        if (parentObj != null)
        {
            parentObj.SetActive(true);
            foreach (Transform child in parentObj.transform)
            {
                string childname = child.name;
                Sprite randomSprite = hero.GetSprite(childname);
                if (randomSprite == null) continue;
                Image transimage = child.GetComponent<Image>();
                if (transimage != null)
                {
                    transimage.sprite = randomSprite;
                    transimage.SetNativeSize();
                }
            }
        }
    }
    private HeroBase GetHero()
    {
        GameObject obj = new GameObject("noob");
        HeroBase hero = obj.AddComponent<NoobHero>();

        GameObject parentObj = GameObject.Find("Main/BgCanvas/Character");

        var path = UnityEngine.Random.Range(1, 10 + 1) <= 3 ? "Handsome/" : "Character/";

        if (parentObj != null)
        {
            parentObj.SetActive(true);
            // Debug.Log("parent있음");
            foreach (Transform child in parentObj.transform)
            {
                // Debug.Log("자식찾는중...");

                string childName = child.name;
                Sprite[] sprites = Resources.LoadAll<Sprite>(path + childName);

                if (sprites == null || sprites.Length == 0)
                {
                    Debug.Log("폴더가 없거나 파일이 없음");

                    continue;
                }
                Sprite RandomSprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
                Image transimg = child.GetComponent<Image>();
                if (transimg != null)
                {
                    // Debug.Log("스프라이트 바꿈");

                    transimg.sprite = RandomSprite;
                    // Handsome guys size diffrent
                    // transimg.SetNativeSize();
                    transimg.gameObject.GetComponent<Image>().SetNativeSize();
                    hero.SetSprites(transimg.sprite);
                }
            }
            // Debug.Log(parentObj.name);
        }

        hero.text = "Hug Me Please";
        return hero;
    }

    #endregion


    #region About Battle Process
    private void Combat()
    {
        int cnt = heros.Count;
        while (cnt-- > 0)
        {
            var cur_hero = heros.Dequeue();
            cur_hero.Battle();
        }
    }
    private void CheckClear()
    {
        if (monsters >= max_monsters)
        {
            MonsterEnd();
        }
    }

    public bool Try_Get_Item()
    {
        // Get Item 20%
        return UnityEngine.Random.Range(1, 101) <= 20;
    }

    public void KillMonster(int val, HeroBase hero)
    {
        monsters -= val;
        Debug.Log(monsters + "몹 수");

        if (monsters <= 0)
        {
            survived_heros.Enqueue(hero);
            Winner();
            return;
            //monsters = 100;
            Debug.Log("I Killed!" + hero.name + hero.sword.Damage.ToString());
        }

        int item_cnt = 0;
        while (val-- > 0)
        {
            if (item_cnt < 2 && Try_Get_Item())
            {
                item_cnt++;

                int id = 8 - Mathf.FloorToInt(Mathf.Sqrt(UnityEngine.Random.Range(0, 64 + 1)));
                hero.earn_data[id] = 1;
            }
            else
            {
                hero.earn_coins += UnityEngine.Random.Range(0, 11);
            }
        }
        survived_heros.Enqueue(hero);
    }

    private void MonsterEnd()
    {
        Debug.Log("You Marry Monster");
    }

    private void HeroEnd(int hero)
    {
        Debug.Log("You Marry Hero");
        // get hero's face
        // set wedding base queen's Money

    }

    [Serializable]
    public class itemData
    {
        public string _name;
        public Dictionary<int, int> data = new Dictionary<int, int>();
    }
    public itemData[] itemDatas = new itemData[16];

    public void MakeItemList(int i, Dictionary<int, int> save_data)
    {
        itemDatas[i].data = save_data;
        itemDatas[i]._name = i.ToString();
    }

    private void IncreaseMonsters()
    {
        // 단순히 2배 늘어남
        monsters = Mathf.FloorToInt(multiple_monsters * monsters) + increase_monsters;
        if(monsters >= max_monsters)
        {
            GameObject go = new GameObject("go");
            HeroBase Mawang = go.AddComponent<NoobHero>();
            Mawang.Mawang();
            Wedding(Mawang);
        }
    }
    #endregion

    public void SetWeaponHero()
    {
        cutscene.NextScene();
        if (cur_hero == null)
        {
            Debug.Log("Something Wrong");
            Debug.Log("It Shoudn't be Null");
            return;
        }
        if (tempinfo != null)
        {
            cur_hero.SetSword(tempinfo);
            isBreak = false;
        }
        else
        {
            Debug.Log("it's Bug. Report to Programmer");
        }

    }

    SwordInfo tempinfo;
    public void GetSword(SwordInfo sword)
    {
        if (sword != null)
        {
            tempinfo = sword;
        }
    }
    public GameObject smithy;
    public void CheckRemainWeaponNeedHero()
    {
        talkingPanel.SetActive(false);
        reward_Panel.SetActive(false);
        Debug.Log("cur Survived : " + survived_heros.Count.ToString());

        isWaiting = survived_heros.Count > 0;
        if (survived_heros.Count > 0)
        {
            var hero = survived_heros.Dequeue();
            ChangeHero(hero);
            if (hero.earn_coins > 0)
            {
                reward_Panel.SetActive(true);
                reward_Panel.GetComponent<RewardPanel>().Setting(hero.earn_coins, hero.earn_data);
                ViewGetMaterials();

                GetMaterials(hero.earn_coins, hero.earn_data);
            }

            isTalking = true;
            Invoke("TryText", 0.4f);

            if (hero.GetSword() == null)
            {
                isBreak = true;
                cur_hero = hero;
            }
            else
            {
                isBreak = false;
                // Debug.Log("같은 친구 옴");
                heros.Enqueue(hero);
            }
        }
        else
        {
            //hero_Image.sprite = nullImg;
            GameObject parentObj = GameObject.Find("Main/BgCanvas/Character");
            if (parentObj != null)
            {
                parentObj.SetActive(false);
            }
            smithy.gameObject.SetActive(false);
            isMaking = false;
            isTalking = false;
        }
    }

    private void ViewGetMaterials()
    {
        isTalking = true;
        DOTween.To(() => "", x => reward_Text.text = x, GiftText(), 0.5f).OnComplete(() => isTalking = false);
    }

    public string[] giftText;
    public String GiftText()
    {
        return giftText[UnityEngine.Random.Range(0, giftText.Length)];
    }

    public string[] thankText;
    public String ThankText()
    {
        return thankText[UnityEngine.Random.Range(0, thankText.Length)];
    }

    public void OpenShop()
    {
        shop.gameObject.SetActive(true);

        /*
        ChangeView()
        select_materials 3
        buy_selected_materials
        ChangTime()
        */
    }



    #region About Shop Process
    void SetImage(HeroBase hero)
    {
        //hero_Image.sprite = hero.sprite;
        // hero_Text.text = hero.text;
    }
    #endregion




    public GameObject anvilBG;
    #region About Chat
    public void TryText()
    {
        talkingPanel.SetActive(true);
        isTalking = true;

        SetText(RandomString());
    }

    public String[] helloText;

    public String RandomString()
    {
        return helloText[UnityEngine.Random.Range(0, helloText.Length)];

    }

    public void GetText(HeroBase hero)
    {

    }
    public void ThankYou()
    {
        SingleTone<UIManager>.Instance.ReZero();
        isThank = true;
        SetText(ThankText());
    }

    public void SetText(String text)
    {
        isTalking = true;
        DOTween.To(() => "", x => hero_Text.text = x, text, 1f).OnComplete(() => isTalking = false);
    }
    #endregion


    public String[] textOfID;
    public string GetTextOfID(int id)
    {
        return textOfID[id];
    }

    public Sprite[] spriteOfID;
    public Sprite GetSpriteOfID(int id)
    {
        return spriteOfID[id];
    }
    public void SetToolTip(string name)
    {
        int i = int.Parse(name);
        if(i==8)
            SingleTone<TooltipManager>.Instance.SetText(materialData.coins, "코인");
        else
            SingleTone<TooltipManager>.Instance.SetText(materialData.nums[i], textOfID[i]);
    }

    public void SetWeaponToolTip(string name)
    {
        int i = int.Parse(name);

        List<String> strings = new List<string>();

        for (int id = 0; id < 8; id++)
        {
            if (itemDatas[i].data[id] > 0)
            {
                // i == item, id == material
                String str = GetTextOfID(id) + " " + itemDatas[i].data[id].ToString();
                // Debug.Log(str);
                strings.Add(str);
            }
        }

        SingleTone<TooltipManager>.Instance.SetSwordText((strings.Count > 0 ? strings[0] : ""), (strings.Count > 1 ? strings[1] : ""), (strings.Count > 2 ? strings[2] : ""));
    }
}
