using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public abstract class BaseSword : MonoBehaviour
{
    public Vector2[] UpgradeTarget;
    public float AtkDamge;
    public float maxValue;//MaxUpgrade
    public float maxDistance = 5.0f;

    public float ReturnStat()
    {
        return AtkDamge;
    }
    public void Upgrade(float value)
    {
        AtkDamge += value;
    }

}
