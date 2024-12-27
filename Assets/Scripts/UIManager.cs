using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingleTone<UIManager>
{


    public void UpgrageFinish()
    {
        SingleTone<GameManager>.Instance.SetWeaponHero();
        SingleTone<GameManager>.Instance.CheckRemainWeaponNeedHero();
    }
}