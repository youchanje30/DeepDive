using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : SingleTone<UIManager>
{
    string clickname;
    public void SetIndex()
    {
        GameObject ClickedObj = EventSystem.current.currentSelectedGameObject;
        if(ClickedObj != null)
        {
            clickname = ClickedObj.name;
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
            Debug.Log("»Æ¿Œ" + baseSword.name);

            Destroy(baseSword.gameObject);
        }
        SingleTone<GameManager>.Instance.SetWeaponHero();
        SingleTone<GameManager>.Instance.CheckRemainWeaponNeedHero();
        
    }
}