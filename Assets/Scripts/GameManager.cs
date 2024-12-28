using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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

    void Update()
    {
        bar.value = monsters;


        if (Input.GetMouseButtonDown(0))
        {
            if(is_night) return;
            if (isTalking) return;
            if (isMaking) return;
            
            if (survived_heros.Count == 0 && !isWaiting)
                ChangeTime();
            else
            {
                // cutscene.Black
                if(isBreak)
                {
                    OpenSmithy();
                }
                else
                {
                    CheckRemainWeaponNeedHero();
                }
                if(isThank)
                {
                    OpenSell();
                    isThank = false;
                }
            }
        }
    }
    #endregion
    public void OpenSmithy()
    {
        smithy.gameObject.SetActive(true);
        anvilBG.SetActive(true);
        isMaking = true;
    }
    public Scenes cutscene;

    
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

    public void CheckData(int id, GetSwordMaterial obj)
    {
        for (int i = 0; i < 8; i++)
        {
            if(itemDatas[id].data[i] > 0)
            {
                obj.AddData(itemDatas[id].data[i], materialData.nums[i], i);
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
    public void UseCoins(int id,int val) => materialData.coins -= materialData.prices[id] * val;
    public void EarnCoins(int val) => materialData.coins += val;
    #endregion
    public GameObject shop;
    void Start()
    {
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
        if(!is_night && survived_heros.Count > 0) return;


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
            OpenSell();
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
        survived_heros.Enqueue(hero);
    }


    private void AddRandomHero()
    {
        var hero = GetHero();
        AddHero(hero);
        AddNeedSword(hero);
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

        var path = UnityEngine.Random.Range(1, 10+1) <= 3 ? "Handsome/" : "Character/";

        if (parentObj != null)
        {
        parentObj.SetActive(true);
            // Debug.Log("parent있음");
            foreach(Transform child in parentObj.transform)
            {
                // Debug.Log("자식찾는중...");

                string childName = child.name;
                Sprite[] sprites = Resources.LoadAll<Sprite>(path+childName);

                if(sprites == null|| sprites.Length == 0)
                {
                    Debug.Log("폴더가 없거나 파일이 없음");

                    continue;
                }
                Sprite RandomSprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
                Image transimg = child.GetComponent<Image>();
                if(transimg != null)
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
        while(cnt-->0)
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
        int item_cnt = 0;
        while (val-->0)
        {
            if(item_cnt < 2 && Try_Get_Item())
            {
                item_cnt++;
                
                int id = 8 - Mathf.FloorToInt(Mathf.Sqrt(UnityEngine.Random.Range(0, 64+1)));
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
        // view wedding
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
        monsters  = Mathf.FloorToInt(multiple_monsters * monsters) + increase_monsters;
    }
    #endregion


    public void OpenSell()
    {
        /*
        ChangeView()
        Come_Hero()
        
        ChangeTime()
        */
        //
        CheckRemainWeaponNeedHero();
    }
    public void SetWeaponHero()
    {
        if(cur_hero == null)
        {
            Debug.Log("Something Wrong");
            Debug.Log("It Shoudn't be Null");
            return;
        }
            
        
        if(tempinfo != null)
        {
            cur_hero.SetSword(tempinfo);
        }
        else
        {
            Debug.Log("it's Bug. Report to Programmer");
        }
    }

    SwordInfo tempinfo;
    public void GetSword(SwordInfo sword) {
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
            if(hero.earn_coins > 0)
            {
                reward_Panel.SetActive(true);
                ViewGetMaterials();
                GetMaterials(hero.earn_coins, hero.earn_data);
            }
                
            isTalking = true;
            Invoke("TryText", 0.4f);
            
            if(hero.GetSword() == null)
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
        DOTween.To(()=> "", x=>reward_Text.text = x, "당신을 위한 선물이에요", 0.5f).OnComplete(() => isTalking = false);
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
        anvilBG.SetActive(false);
        isMaking = false;
        isThank = true;
        SetText("고마워요! 다음에봐요 공주님"); 
    }

    public void SetText(String text)
    {
        isTalking = true;
        DOTween.To(()=> "", x=>hero_Text.text = x, text, 1f).OnComplete(() => isTalking = false);
    }
    #endregion
}
