using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using Unity.Mathematics;
using Unity.VisualScripting;

public class GameManager : SingleTone<GameManager>
{
    #region is shoud be removed
    public GameObject morning;
    public Slider bar;
    void Update()
    {
        bar.value = monsters;
    }
    #endregion



    
    private bool is_night;

    [SerializeField] private int monsters;
    [SerializeField] private int max_monsters;


    [Header("Shop Data")]
    #region About Shop Datas
    private HeroBase cur_hero;
    public Sprite nullImg;
    public Sprite[] imgs;
    [SerializeField] Image hero_Image;
    [SerializeField] TMP_Text hero_Text;
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

    public void GetMaterials(int earn_coins, Dictionary<int, int> earn_data)
    {
        EarnCoins(earn_coins);
        foreach (var item in earn_data)
        {
            AddMaterials(item.Key, item.Value);
        }
    }


    public void UseCoins(int val) => materialData.coins -= val;
    public void EarnCoins(int val) => materialData.coins += val;
    #endregion

    void Start()
    {
        smithy.gameObject.SetActive(false);

        bar.maxValue = max_monsters;
        cur_day = 0;
        is_night = true;
        monsters = 10;
        ChangeTime();
    }

    public void ChangeTime()
    {
        is_night = !is_night;
        Debug.Log(heros.Count);

        morning.SetActive(!is_night);
        if (is_night)
        {
            Combat();
            IncreaseMonsters();
            CheckClear();
            OpenShop();
        }
        else
        {
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

    private HeroBase GetHero()
    {
        GameObject obj = new GameObject("noob");
        HeroBase hero = obj.AddComponent<NoobHero>();
        hero.sprite = imgs[UnityEngine.Random.Range(0, imgs.Length)];
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

    private void CheckMonsterDead(int hero)
    {
        if (monsters <= 0)
        {
            HeroEnd(hero);
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

    public class itemData
    {
        public string _name;
        public Dictionary<string, string> data;
    }
    public itemData[] itemDatas = new itemData[10];

    public void MakeItemList(int i, Dictionary<string, string> save_data)
    {
        foreach(var item in save_data)
        {
            Debug.Log(item);
        }
        // itemDatas[i].data = save_data;
        // itemDatas[i]._name = i.ToString();

        // foreach(var item in save_data)
        // {
        //     Debug.Log(item);
        // }
    }

    private void IncreaseMonsters()
    {
        // 단순히 2배 늘어남
        monsters *= 2;
    }
    #endregion



    public void OpenSell()
    {
        /*
        ChangeView()
        Come_Hero()
        
        ChangeTime()
        */
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
        Debug.Log("cur Survived : " + survived_heros.Count.ToString());
        if(survived_heros.Count > 0)
        {
            var hero = survived_heros.Dequeue();
            GetMaterials(hero.earn_coins, hero.earn_data);
            SetImage(hero);
            if(hero.GetSword() == null)
            {
                smithy.gameObject.SetActive(true);
                cur_hero = hero;
            }
            else
            {
                heros.Enqueue(hero);
            }
        }
        else
        {
            hero_Image.sprite = nullImg;
            smithy.gameObject.SetActive(false);
        }
    }


    public void OpenShop()
    {
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
        hero_Image.sprite = hero.sprite;
        hero_Text.text = hero.text;
    }
    #endregion

    // void Update()
    // {
        
    // }
}
