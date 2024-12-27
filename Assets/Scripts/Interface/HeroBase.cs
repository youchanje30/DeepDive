using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.UI;
using UnityEngine;


public abstract class HeroBase : MonoBehaviour
{
    public float survive_rate;
    public int sword; //BaseSword sword;
    
    
    public int GetSword()// public BaseSword GetSword()
    {
        return sword;
    }

    public void SetSword(int _sword)
    {
        sword = _sword;
    }

    public bool isDead()
    {
        Debug.Log(survive_rate);
        return !(Random.Range(0f, 100f) < survive_rate);
    }

    public void Battle()
    {
        if(isDead())
        {
            Debug.Log("I Dead" + this.ToString());
            return;
        }
        
        SingleTone<GameManager>.Instance.KillMonster(GetDamage(), this);
    }

    public int GetDamage()
    {
        return 1;
        //return sword.GetDamage();
    }

    public float GetSurviveRate()
    {
        return survive_rate;
    }
}