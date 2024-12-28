using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : SingleTone<TooltipManager>
{
    public Tooltip tooltip;

    private void Update()
    {
        tooltip.GetComponent<RectTransform>().SetAsLastSibling();
        tooltip.transform.position = new Vector3(Input.mousePosition.x - 140, Input.mousePosition.y + 160);
    }

    public void SetText(int val, string name)
    {
        tooltip.SetupTooltip(name, val.ToString());
    }
}
