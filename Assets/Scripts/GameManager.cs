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

    public GameObject sdfsdf;
    private HeroBase GetHero()
    {
        GameObject obj = new GameObject("noob");
        HeroBase hero = obj.AddComponent<NoobHero>();
        GameObject sword = Instantiate(sdfsdf);
        hero.SetSword(sword.GetComponent<BaseSword>());
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
    }

    private void HeroEnd(int hero)
    {
        // get hero's face
        // set wedding base queen's Money

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
            Debug.Log("Something Wrong");
            Debug.Log("It Shoudn't be Null");
            return;
        
        // It need Change;
        GameObject sword = Instantiate(sdfsdf);
        sword_data = sword.GetComponent<BaseSword>();
        cur_hero.SetSword(sword_data);
    }

    public void CheckRemainWeaponNeedHero()    
    {
        if(need_weapon_heros.Count > 0)
        {
            var need_hero = need_weapon_heros.Dequeue();
            SetImage(need_hero);
        }
        else
        {
            SetImage(null);
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
