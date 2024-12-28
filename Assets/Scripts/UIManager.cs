using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : SingleTone<UIManager>
{
    public GetSwordMaterial obj;
    string clickname;
    public void SetIndex()
    {
        GameObject ClickedObj = EventSystem.current.currentSelectedGameObject;
        if(ClickedObj != null)
        {
            clickname = ClickedObj.name;
            obj.SetSwordData(int.Parse(clickname));
        }
    }
    public string GetIndex()
    {
        return clickname;
    }

    public void UpgrageFinish()
    {
        BaseSword baseSword = GameObject.FindAnyObjectByType<BaseSword>();
        if (baseSword != null)
        {

            Destroy(baseSword.gameObject);
        }
        SingleTone<GameManager>.Instance.SetWeaponHero();
        SingleTone<GameManager>.Instance.ThankYou();
        
    }
}