using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "YieldResult", menuName = "Scriptable Objects/YieldResult")]
public class YieldResult : ScriptableObject
{
    public List<InventoryItem> yieldItems;
}
