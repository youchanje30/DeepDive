using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : SingleTone<GameManager>
{
    private bool is_night;

    [SerializeField] private int monsters;
    [SerializeField] private int max_monsters;


    #region About Hero Datas
    public Queue<int> heros; // BaseHero
    public int hero_increase_day;
    [SerializeField] int hero_max_cnt; 
    private int cur_day = 0;
    public Queue<int> need_weapon_heros;
    #endregion

    void Start()
    {
        cur_day = 0;
        is_night = false;
        AddHero();
    }

    public void ChangeTime()
    {
        is_night = !is_night;

        if (is_night)
        {
            IncreaseMonsters();
            CheckClear();
            OpenShop();
        }
        else
        {
            ComeHero();
            OpenSell();
        }
    }




    #region About Hero Process
    private void ComeHero()
    {
        while(need_weapon_heros.Count > 0)
        {
            var need_hero = need_weapon_heros.Dequeue();
            // plz Make!
        }
    }

    private void AddHero()
    {
        var hero = GetHero();
        heros.Enqueue(hero);
    }

    private int GetHero()
    {
        return 0;
    }
    #endregion


    #region About Battle Process
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

    public void KillMonster(int val)
    {
        monsters -= val;
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
