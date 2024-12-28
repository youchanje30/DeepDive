using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : SingleTone<TooltipManager>
{
    public Tooltip tooltip;

    private void Update()
    {
        tooltip.GetComponent<RectTransform>().SetAsLastSibling();
        tooltip.transform.position = new Vector3(Input.mousePosition.x - 140, Input.mousePosition.y + (Input.mousePosition.y > 500 ? -160 : 160));
    }

    public void SetText(int val, string name)
    {
        tooltip.SetupTooltip(name, val.ToString());
    }

    public void SetSwordText(string a, string b = "", string c = "")
    {
        tooltip.SetupSwordTooltip(a, b, c);
    }
}
