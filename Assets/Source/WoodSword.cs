using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WoodSword : BaseSword
{
    float tolerance = 1f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UpgradeEnd)
        {
            SelectUpgrade();
        }
    }
    
    private void SelectUpgrade()
    {
        
        Vector3 mousePos = Input.mousePosition;
        Vector3 WorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        WorldPos.z = 0f;
        RaycastHit2D hit = Physics2D.Raycast(WorldPos, Vector2.zero);

        float randomOffsetx = Random.Range(-tolerance, tolerance);
        float randomOffsety = Random.Range(-tolerance, tolerance);
        if(hit.collider != null)
        {
            Vector2 MousePos2D = new Vector2(WorldPos.x + randomOffsetx, WorldPos.y + randomOffsety);

            Vector2 nearPos = Vector2.zero;
            float maxWeight = float.MinValue;
            for (int i = 0; i < TempCnt; i++)
            {
                if (!UpgradeTargets.Contains(UpgradeTarget[i])){
                    float distance = Vector2.Distance(MousePos2D, UpgradeTarget[i]);

                    float weight = distance > 0 ? Mathf.Min(1 / distance, maxValue) : maxValue;
                    if (weight > maxWeight)
                    {
                        maxWeight = weight;//near Distance weight
                        nearPos = UpgradeTarget[i];//near Target

                    }
                }
                
            }
            if (getTarget())
                maxWeight = 1;
            if (maxWeight != float.MinValue)
            {
                DisableCloset(nearPos);
                Upgrade(maxWeight,nearPos);
            }
            Debug.Log(maxWeight);
        }
    }
    void DisableCloset(Vector2 nearpos)
    {
        Transform[] ChildObjects = GetComponentsInChildren<Transform>();
        GameObject closetobj = null;
        float closetDistance = float.MaxValue;
        foreach(Transform child in ChildObjects)
        {
            if(child.name == "T") {
                {
                    float distance = Vector2.Distance(nearpos,(Vector2)child.position);
                    if (distance < closetDistance)
                    {
                        closetDistance = distance;
                        closetobj = child.gameObject;
                    }
                } 
            }
        }
        if(closetobj != null)
        {
            closetobj.transform.GetComponent<Renderer>().material.DOFade(0, 1).OnComplete(() => { closetobj.SetActive(false); });
        }
    }
    bool getTarget()
    {
        if (UpgradeTargets.Count != UpgradeTarget.Length)
            return false;
        for (int i = 0; i < UpgradeTarget.Length; i++)
        {
            if (!UpgradeTargets.Contains(UpgradeTarget[i]))
            {
                return false;
            }
        }
        return true;
    }

}
