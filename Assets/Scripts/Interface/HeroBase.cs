using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;


public abstract class HeroBase : MonoBehaviour
{
    public Sprite sprite;
    public String text;
    public float survive_rate;
    public BaseSword sword;
    
    #region Get Data Functions
    public Sprite GetISprite()
    {
        return sprite;
    }


    #endregion
    
    public BaseSword GetSword()// public BaseSword GetSword()
    {
        return sword;
    }

    public void SetSword(BaseSword _sword)
    {
        if(sword != null) Destroy(sword);

        sword = _sword;
    }

    public bool isDead()
    {
        Debug.Log(survive_rate);
        return !(UnityEngine.Random.Range(0f, 100f) < survive_rate);
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
        float damage = sword.ReturnStat();
        int killedMonster = Mathf.FloorToInt(damage);
        return killedMonster;
    }

    public float GetSurviveRate()
    {
        return survive_rate;
    }
}