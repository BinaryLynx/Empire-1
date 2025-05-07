using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class YieldingPlant : Plant
{
    int hoursToYield = 24;
    int hoursInYield = 0;

    public override int Grow(int hoursPassed)
    {
        int remainder = base.Grow(hoursPassed);
        if (IsYieldReady() || remainder == 0)
        {
            return remainder;
        }
        int remainderInYield = hoursToYield - hoursInYield;
        int hoursAdded = Mathf.Min(remainderInYield, remainderInYield);
        hoursInYield += hoursAdded;
        remainder -= hoursAdded;

        return remainder;

    }

    bool IsYieldReady()
    {
        if (hoursInYield == hoursToYield)
        {
            return true;
        }
        return false;
    }

    public override List<InventoryItem> Harvest(float efficency_multiplier)
    {
        if (!IsYieldReady())
        {
            return null;
        }

        hoursInYield = 0;
        return yieldResult.yieldItems;

    }
}