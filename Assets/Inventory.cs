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

    public enum EquipType
    {
        Null, Helmet, Chestguard, Legguards, OneHandedWeapon, TwoHandedWeapon, Bow, Shield, Arrow
    }

    [Serializable]
    public struct ItemData
    {
        public int ItemID;
        public string Name;
        public string Description;
        public ItemType itemType;
        public EquipType equipType;
        public Sprite sprite;
        public Mesh armorMesh;

        public ItemData(int itemID) { ItemID = itemID; Name = ""; Description = ""; itemType = ItemType.Consumable; equipType = EquipType.Null ; sprite = Resources.Load("Assets/Sprites/22.png") as Sprite; armorMesh = null; }
    }

    [Serializable]
    public struct InventoryData
    {
        public int ItemID;
        public int Amount;

        public InventoryData(int itemID, int amount) { ItemID = itemID; Amount = amount; }
    }

    [Serializable]
    public struct EquipmentData
    {
        public EquipType equipType;
        public int ItemID;
        public int Amount;

        public EquipmentData(EquipType equip_Type, int itemID, int amount) {equipType = equip_Type; ItemID = itemID; Amount = amount; }
    }


    public List<ItemData> itemData = new List<ItemData>();
    public List<EquipmentData> equipData = new List<EquipmentData>();
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
        ItemData temp = new ItemData(-1);

        if (ItemID < 0)
        {
            return temp;
        }
        else
        {
            foreach (ItemData data in itemData)
            {
                if (data.ItemID == ItemID) { temp = data; break; }
            }
            return temp;
        }
    }

    public InventoryData GetSlot(int slotID)
    {
        return invData[slotID];
    }

    public EquipmentData GetEquip(EquipType type)
    {
        EquipmentData equip = new EquipmentData(EquipType.Null, -1, 0);

        foreach (EquipmentData eData in equipData)
        {
            if (eData.equipType == type) equip = eData;
        }

        return equip;
    }

    public void SetEquippedItem()
    {

    }
}
