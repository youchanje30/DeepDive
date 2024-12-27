using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public abstract class BaseSword : MonoBehaviour
{
    public Vector2[] UpgradeTarget;
    public float AtkDamge;
    public float maxValue;//MaxUpgrade
    public float maxDistance = 5.0f;
    public float SurvivalRate = 30.0f;   
    public float DestroyRate = 10.0f;
    public bool UpgradeEnd = false;
    SwordInfo swordInfo;
    int UpgradeCnt = 0;
    private void Start()
    {
    }
    public SwordInfo ReturnStat()
    {
        return swordInfo;
    }
    public void Upgrade(float value)
    {
        if(UpgradeCnt >= UpgradeTarget.Length)
        {
            Debug.Log("end upgrade");
            UpgradeEnd = true;
            SwordInfo swordInfo = new SwordInfo(AtkDamge, SurvivalRate, DestroyRate);
            SingleTone<GameManager>.Instance.GetSword(swordInfo);
            return;
        }
        AtkDamge += value;
        UpgradeCnt++;
    }
}
