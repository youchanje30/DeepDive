using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;
using System.Runtime;
using Unity.IO.LowLevel.Unsafe;

public class Inventory : MonoBehaviour
{
    [SerializeField] Transform[] inventory;
    
    public void CheckID(string clickname)
    {
        Clear();
        SingleTone<GameManager>.Instance.CheckData(int.Parse(clickname), this);
    }

    public void Clear()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i].position.y == -4f) continue;
            
            DOTween.Kill(inventory[i]);
            inventory[i].DOMoveY(-4f, 0.5f).SetEase(Ease.OutCirc);
        }
    }

    public void Move(int id)
    {
        DOTween.Kill(inventory[id]);
        inventory[id].DOMoveY(-2f, 1f).SetEase(Ease.OutCirc);
    }
}
