using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipCon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Tooltip tooltip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SingleTone<GameManager>.Instance.SetToolTip(this.name);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
    }
}
 