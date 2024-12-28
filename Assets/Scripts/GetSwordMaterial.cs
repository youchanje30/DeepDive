using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetSwordMaterial : MonoBehaviour
{

    public Sprite[] sprites;
    public GameObject obj;

    public Queue<GameObject> remove_obj = new Queue<GameObject>();

    public void SetSwordData(int id)
    {
        ClearChildren();
        SingleTone<GameManager>.Instance.CheckData(id, this);
    }

    

    void ClearChildren()
    {
        var cnt = remove_obj.Count;
        while (cnt-->0)
        {
            Destroy(remove_obj.Dequeue());
        }
    }

    public void AddData(int data_val, int nums, int id = 0)
    {
        // Debug.Log($"{data_val} , {}");
        // material's id need data val, i have nums
        var ob = Instantiate(obj);
        var text = obj.GetComponentInChildren<TMP_Text>();
        UnityEngine.UI.Image icon = obj.GetComponentInChildren<UnityEngine.UI.Image>();
        icon.sprite = sprites[id];
        text.text = data_val.ToString() + " / " + nums.ToString();
        
        ob.GetComponent<RectTransform>().SetParent(this.GetComponent<RectTransform>());
        ob.transform.localScale = Vector3.one;
        remove_obj.Enqueue(ob);
    }
    
}