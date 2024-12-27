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
            for (int i = 0; i < UpgradeTarget.Length; i++)
            {
                float distance = Vector2.Distance(MousePos2D, UpgradeTarget[i]);

                float weight = distance > 0 ? Mathf.Min(1 / distance, maxValue) : maxValue;
                if (weight > maxWeight)
                {
                    maxWeight = weight;//near Distance weight
                    nearPos = UpgradeTarget[i];//near Target
                }
            }
            if(maxWeight != float.MinValue)
            {
                Upgrade(maxWeight);
            }
        }
    }

}
