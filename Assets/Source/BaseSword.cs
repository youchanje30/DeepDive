using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseSword : MonoBehaviour
{
    public Vector2[] UpgradeTarget;
    public List<Vector2> UpgradeTargets;
    public float AtkDamge;
    public float maxValue;//MaxUpgrade
    public float SurvivalRate = 30.0f;   
    public float DestroyRate = 10.0f;
    public bool UpgradeEnd = false;
    SwordInfo swordInfo;
    int UpgradeCnt = 0;
    public int TempCnt;
    public void Start()
    {
        TempCnt = UpgradeTarget.Length;
        for(int i = 0; i < TempCnt; i++)
        {
            GameObject gameObjects = new GameObject("T");
            gameObjects.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("UI_ 1");
            gameObjects.GetComponent<SpriteRenderer>().sortingOrder = 10;
            gameObjects.transform.localScale = new Vector2(1f, 1f);
            gameObjects.transform.localPosition = UpgradeTarget[i];
            gameObjects.transform.parent = gameObject.transform;
        }
    }
    public SwordInfo ReturnStat()
    {
        return swordInfo;
    }

    public void Upgrade(float value, Vector2 nearpos)
    {
        if(UpgradeCnt >= TempCnt)
        {
            Debug.Log("end upgrade");
            UpgradeEnd = true;
            SwordInfo swordInfo = new SwordInfo(AtkDamge, SurvivalRate, DestroyRate);
            SingleTone<GameManager>.Instance.GetSword(swordInfo);
            SingleTone<UIManager>.Instance.UpgrageFinish();
            return;
        }
        SingleTone<AudioManager>.Instance.PlaySfx(Sfx.hammer);
        UpgradeTargets.Add(nearpos);
        AtkDamge += value;
        UpgradeCnt++;
    }

    private const string BasePath = "Sword/";
    public static SwordType Create<SwordType>(Transform parent, int addpath) where SwordType : BaseSword
    {
        GameObject prefabobj = GameObject.Instantiate(Resources.Load(BasePath + addpath.ToString())) as GameObject;
        if (prefabobj != null)
        {
            Debug.Log("��");
        }
        else
            Debug.Log("��");
        SwordType sword = prefabobj.GetComponent<SwordType>();
        if(sword == null)
        {
            sword = prefabobj.AddComponent<SwordType>();
        }
        if (parent != null)
        {
            prefabobj.transform.SetParent(parent);
        }

        prefabobj.transform.localScale = Vector3.one;
        prefabobj.transform.localPosition = Vector3.zero;
        return sword;   
    }

}
