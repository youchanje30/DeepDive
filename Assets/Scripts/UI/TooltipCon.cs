using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipCon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Tooltip tooltip;

    public bool is_material;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (is_material)
            SingleTone<GameManager>.Instance.SetToolTip(this.name);
        else
            SingleTone<GameManager>.Instance.SetWeaponToolTip(this.name);
        tooltip.gameObject.SetActive(true);
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
    }
}
 