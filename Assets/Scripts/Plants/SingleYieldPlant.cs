using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleYieldPlant : Plant
{
    public override List<InventoryItem> Harvest(float efficency_multiplier)
    {
        if (!isFullyGrown)
        {
            return null;
        }
        StartCoroutine(DestroyAfterReturn());
        return yieldResult.yieldItems;
    }

    private IEnumerator DestroyAfterReturn()
    {
        yield return null; // Wait 1 frame
        Destroy(gameObject); // Now safe to destroy
    }
}