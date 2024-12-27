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
        if(swordpath == null)
        {
            swordpath = "1";
        }
        Debug.Log(swordpath);
        BaseSword baseSword = GameObject.FindAnyObjectByType<BaseSword>();
        if (baseSword != null)
        {
            Debug.Log("»Æ¿Œ"+baseSword.name);

            Destroy(baseSword.gameObject);
        }
        WoodSword woodSword = WoodSword.Create<WoodSword>(transform.parent.parent, swordpath);

    }

    void Update()
    {
        
    }
}
