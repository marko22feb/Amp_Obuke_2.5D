using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public List<int> SetID;

    public List<int> ItemID;
    public List<int> Amount;

    public void Start()
    {
        GenerateLoot();
    }
    public void GenerateLoot()
    {
        for (int a = 0; a < SetID.Count; a++)
        {
            Inventory.ItemSets set = Inventory.inv.itemSets[SetID[a]];

            int Index = -1;
            float accumulatedPercentage = 0;

            for (int i = 0; i < set.ChanceToDrop.Count; i++)
            {
                if (set.ChanceToDrop[i] > 0)
                {
                    float rollPercent = Random.Range(1, 100 - accumulatedPercentage);
                    if (rollPercent <= set.ChanceToDrop[i])
                    {
                        Index = i;
                        break;
                    }
                    else
                    {
                        accumulatedPercentage += set.ChanceToDrop[i];
                    }
                }
            }

            ItemID.Add(set.ItemIDs[Index]);
            Amount.Add(Random.Range(set.minAmount[Index], set.maxAmouunt[Index]));
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");

        bool temp = false;

        for (int i = 0; i < ItemID.Count; i++)
        {
            if (ItemID[i] != -1)
            {
                Inventory.inv.AddItem(ItemID[i], Amount[i], out temp);
                if (temp)
                {
                    ItemID[i] = -1;
                }
            }
        }

        temp = false;

        foreach (int i in ItemID)
        {
            if (i != -1) { temp = true; break; }
        }

        if (!temp)
        {
            Destroy(gameObject);
        }
    }
}
