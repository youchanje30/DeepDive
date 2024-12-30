using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetSwordMaterial : MonoBehaviour
{

    public Sprite[] sprites;
    public GameObject obj;

    public Queue<GameObject> remove_obj = new Queue<GameObject>();

    public void SetSwordData(int id)
    {
        ClearChildren();
        // SingleTone<GameManager>.Instance.CheckData(id, this);
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
        var ob = Instantiate(obj);
        var text = obj.GetComponentInChildren<TMP_Text>();
        UnityEngine.UI.Image icon = obj.GetComponentInChildren<UnityEngine.UI.Image>();
        icon.sprite = sprites[id];
        text.text = nums.ToString() + " / " + data_val.ToString();
        
        ob.GetComponent<RectTransform>().SetParent(this.GetComponent<RectTransform>());
        ob.transform.localScale = Vector3.one;
        remove_obj.Enqueue(ob);
    }
    
}
