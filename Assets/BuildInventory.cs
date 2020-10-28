using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildInventory : MonoBehaviour
{
    public int SizeX;
    public int SizeY;
    public GameObject invSlot;

    private void OnEnable()
    {
        ConstructInventory();
    }

    public void ConstructInventory()
    {
        int index = 0;
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                GameObject temp = Instantiate(invSlot, transform);
                temp.GetComponent<InventorySlot>().SlotID = index;
                index++;
            }
        }
    }
}
