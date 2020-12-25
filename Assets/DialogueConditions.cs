using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueConditions : MonoBehaviour
{
    public virtual bool IsConditionMet(int Index)
    {
        switch (Index)
        {
            case -1:
                return true;
            case 22:
                int slotid;
                Inventory.InventoryData data;
                Inventory.inv.FetchInventoryByID(1, true, out slotid, out data);
                if (data.Amount < 300) return true; else return false;
            default:
                return true;
        }
    }
}
