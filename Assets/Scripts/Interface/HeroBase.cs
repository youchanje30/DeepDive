using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;
using UnityEngine.UI;


public abstract class HeroBase : MonoBehaviour
{
    public Sprite sprite;
    public String text;
    public float survive_rate;
    public SwordInfo sword;


    // Rewards
    public Dictionary<int, int> earn_data = new Dictionary<int, int>();
    public int earn_coins;

    
    #region Get Data Functions
    public Sprite GetISprite()
    {
        return sprite;
    }


    #endregion
    
    public SwordInfo GetSword()// public BaseSword GetSword()
    {
        return sword;
    }

    public void SetSword(SwordInfo _sword)
    {
        sword = _sword;
        Debug.Log($"sword: {sword.Damage} , SurvivalRate:{sword.SurvivalRate} ,DestroyRate: {sword.DestroyRate}" );
    }

    public bool isDead()
    {
        Debug.Log(survive_rate);
        if (sword == null)
        {
            Debug.Log("here is Null");
            return true;
        }


        return !(UnityEngine.Random.Range(0f, 100f) < sword.SurvivalRate);
    }

    public void Battle()
    {
        if(isDead())
        {
            Debug.Log("I Dead" + this.ToString());
            return;
        }
        SingleTone<GameManager>.Instance.KillMonster(GetDamage(), this);
        
        // if Sword break, Add Need Sword
        if(UnityEngine.Random.Range(0f, 100f) < 10)//sword.GetBreakRate())
        {
            SingleTone<GameManager>.Instance.AddNeedSword(this);
            Debug.Log("Need !");
        }
    }

    public int GetDamage()
    {
        if (sword == null)
        {
            Debug.Log("Sword is Null Check Plz");
            return 0;
        }

        int killedMonster = Mathf.FloorToInt(sword.Damage);
        return killedMonster;
    }

    public float GetSurviveRate()
    {
        return survive_rate;
    }
}