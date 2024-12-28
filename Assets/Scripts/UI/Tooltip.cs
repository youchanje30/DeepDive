using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public TMP_Text Objname;
    public TMP_Text need_info;
    public TMP_Text cur_info;

    public void SetupTooltip(string name, string cur, string need = "")
    {
        Objname.text = name;
        need_info.text = cur;
        cur_info.text = need;
    }
}
