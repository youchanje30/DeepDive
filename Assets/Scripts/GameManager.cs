using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingleTone<GameManager>
{
    #region is shoud be removed

    public Slider bar;
    void Update()
    {
        bar.value = monsters;
    }
    #endregion




    private bool is_night;

    [SerializeField] private int monsters;
    [SerializeField] private int max_monsters;


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
    }

    public void ChangeTime()
    {
        is_night = !is_night;


        int cnt = heros.Count;
        while(cnt-->0)
        {
            Debug.Log("Hero is Here");
        }

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

    private void ComeHero()
    {
        while(need_weapon_heros.Count > 0)
        {
            var need_hero = need_weapon_heros.Dequeue();
            // plz Make!
        }
    }

    private void AddRandomHero()
    {
        var hero = GetHero();
        AddHero(hero);
    }

    public void AddHero(HeroBase hero)
    {
        heros.Enqueue(hero);
    }

    private HeroBase GetHero()
    {
        GameObject obj = new GameObject("noob");
        HeroBase hero = obj.AddComponent<NoobHero>();
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


    // void Update()
    // {
        
    // }
}
