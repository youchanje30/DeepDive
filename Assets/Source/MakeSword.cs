using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MakeSword : MonoBehaviour
{
    public bool isGen = false;
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(CreateSword);
    }
    public string swordpath;
    WoodSword sword;
    private void OnEnable(){
        isGen = false;
    }
    private void CreateSword()
    {
        swordpath = SingleTone<UIManager>.Instance.GetIndex();
        int Swordint;
        if (swordpath == null)
        {
            swordpath = "1";
        }
        if (int.TryParse(swordpath, out Swordint))
        {
            Debug.Log("transint :" + Swordint);
        }
        if (SingleTone<GameManager>.Instance.CanBuySword(Swordint) && !isGen)
        {
            SingleTone<GameManager>.Instance.BuySword(Swordint);
            Debug.Log(swordpath);
            BaseSword baseSword = GameObject.FindAnyObjectByType<BaseSword>();
            if (baseSword != null)
            {
                Debug.Log("check" + baseSword.name);
                Destroy(baseSword.gameObject);
            }
            sword = WoodSword.Create<WoodSword>(transform.parent.parent, Swordint);
            isGen = true;
        }
        if (isGen && BaseSword.getUpgrade())
        {
            sword.UseBtn();

            isGen = false;
        }
    }

    void Update()
    {

    }
}
