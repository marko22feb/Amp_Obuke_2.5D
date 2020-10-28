using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory inv;

    public enum ItemType
    {
        Consumable, Armor, Weapon, Quest
    }

    [Serializable]
    public struct ItemData
    {
        public int ItemID;
        public string Name;
        public string Description;
        public ItemType itemType;
        public Sprite sprite;

        public ItemData(int itemID) { ItemID = itemID; Name = ""; Description = ""; itemType = ItemType.Consumable; sprite = Resources.Load("Assets/Sprites/22.png") as Sprite; }
    }

    [Serializable]
    public struct InventoryData
    {
        public int ItemID;
        public int Amount;

        public InventoryData(int itemID, int amount) { ItemID = itemID; Amount = amount; }
    }

    public List<ItemData> itemData = new List<ItemData>();
    public List<InventoryData> invData = new List<InventoryData>();

    private void Awake()
    {
        if (inv == null)
        {
            inv = this;
        }

        for (int i = 0; i < invData.Count; i++)
        {
            invData[i] = new InventoryData(-1, 0);
        }

        invData[15] = new InventoryData(1, 4);
        invData[22] = new InventoryData(2, 92);
    }

    public ItemData GetItem(int ItemID)
    {
        if (ItemID < 0)
        {
            ItemData temp = new ItemData(-1);
            return temp;
        }
        else
        {
            return itemData[0];
        }
    }

    public InventoryData GetSlot(int slotID)
    {
        return invData[slotID];
    }
}
