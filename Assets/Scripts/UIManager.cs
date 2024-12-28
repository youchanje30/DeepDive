using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : SingleTone<UIManager>
{
    public Inventory obj;
    string clickname;
    public void SetIndex()
    {
        GameObject ClickedObj = EventSystem.current.currentSelectedGameObject;
        if(ClickedObj != null)
        {
            clickname = ClickedObj.name;
            obj.CheckID(clickname);
        }
    }

    public void ReZero()
    {
        obj.Clear();
    }

    public string GetIndex()
    {
        return clickname;
    }

    public void UpgrageFinish(BaseSword baseSword)
    {
        SingleTone<GameManager>.Instance.SetWeaponHero();
        SingleTone<GameManager>.Instance.ThankYou();
        if (baseSword != null)
        {

            Destroy(baseSword.gameObject);
        }
        
        
    }
}