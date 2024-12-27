using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MakeSword : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(CreateSword);
    }
    public string swordpath ;
    private void CreateSword()
    {
        swordpath = SingleTone<UIManager>.Instance.GetIndex();
        int Swordint;
        if(swordpath == null)
        {
            swordpath = "1";
        }
        if (int.TryParse(swordpath, out Swordint))
        {
            Debug.Log("transint :" + Swordint);
        }
        //if (SingleTone<GameManager>.Instance.CanBuySword(Swordint))
        //{
            Debug.Log(swordpath);
            BaseSword baseSword = GameObject.FindAnyObjectByType<BaseSword>();
            if (baseSword != null)
            {
                Debug.Log("check"+baseSword.name);
                Destroy(baseSword.gameObject);
            }
            WoodSword.Create<WoodSword>(transform.parent.parent, Swordint);
        //}
    }

    void Update()
    {
        
    }
}
