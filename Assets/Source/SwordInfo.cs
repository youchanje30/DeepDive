using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInfo 
{
    public float Damage;
    public float SurvivalRate;
    public float DestroyRate;

    public SwordInfo(float damage, float survivalRate, float destroyRate)
    {
        Damage = damage;
        SurvivalRate = survivalRate;
        DestroyRate = destroyRate;
    }
}
