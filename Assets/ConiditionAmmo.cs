using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConiditionAmmo : DialogueConditions
{
    public override bool IsConditionMet(int Index)
    {
        int slotid;
        Inventory.InventoryData data;
        Inventory.inv.FetchInventoryByID(1, true, out slotid, out data);
        if (data.Amount < 300) return true; else return false;
    }
}
