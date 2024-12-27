using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private BaseSword sword_data;
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
    public Queue<HeroBase> need_weapon_heros = new Queue<HeroBase>();
    #endregion

    void Start()
    {
        smithy.gameObject.SetActive(false);

        bar.maxValue = max_monsters;
        cur_day = 0;
        is_night = true;
        monsters = 10;
        AddRandomHero();
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
            // ComeHeroToGetWeapon();
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
        need_weapon_heros.Enqueue(hero);
    }

    private void ComeHeroToGetWeapon()
    {
        if(need_weapon_heros.Count > 0)
        {
            var need_hero = need_weapon_heros.Dequeue();
            SetImage(need_hero);
            cur_hero = need_hero;
            Debug.Log(need_hero);
        }
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
        // hero.SetSword(new SwordInfo(0f, 0f, 0f));
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

    public void KillMonster(int val, HeroBase hero)
    {
        monsters -= val;
        heros.Enqueue(hero);
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
    public itemData[] itemDatas;

    public void MakeItemList(int i, Dictionary<string, string> save_data)
    {
        itemDatas[i].data = save_data;
        itemDatas[i]._name = i.ToString();
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
        Debug.Log(need_weapon_heros.Count);
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
        if(need_weapon_heros.Count > 0)
        {
            var need_hero = need_weapon_heros.Dequeue();
            SetImage(need_hero);
            smithy.gameObject.SetActive(true);
            cur_hero = need_hero;
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
