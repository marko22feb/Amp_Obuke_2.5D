using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory inv;
    private ShootingLogic SL;

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
        public int MaxStack;
        public string Name;
        public string Description;
        public ItemType itemType;
        public EquipType equipType;
        public Sprite sprite;
        public Mesh armorMesh;
        public bool IsUnique;

        public ItemData(int itemID) { ItemID = itemID; MaxStack = 999; Name = ""; Description = ""; itemType = ItemType.Consumable; equipType = EquipType.Null ; sprite = Resources.Load("Assets/Sprites/22.png") as Sprite; armorMesh = null; IsUnique = false; }
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

    [Serializable]
    public struct ItemSets
    {
        public List<int> ItemIDs;
        public List<float> ChanceToDrop;
        public List<int> minAmount;
        public List<int> maxAmouunt;

        public ItemSets(List<int> itemIDs, List<float> chanceToDrop, List<int> MinAmount, List<int> MaxAmouunt)
        {
            ItemIDs = itemIDs; ChanceToDrop = chanceToDrop; minAmount = MinAmount; maxAmouunt = MaxAmouunt;
        }
    }


    public List<ItemData> itemData = new List<ItemData>();
    public List<EquipmentData> equipData = new List<EquipmentData>();
    public List<InventoryData> invData = new List<InventoryData>();
    public List<ItemSets> itemSets = new List<ItemSets>();

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

        invData[0] = new InventoryData(1, 85);

        
    }

    public void OnEnable()
    {
        SL = GameObject.FindWithTag("Player").GetComponent<ShootingLogic>();
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

    public void SetSlot(int slotID, int itemID, int newAmount)
    {
        invData[slotID] = new Inventory.InventoryData(itemID, newAmount);
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

    public void AddItem(int ItemID, int amount, out bool Sucess)
    {
        
        bool isSucess;
        bool hasSlot = false;
        ItemData item = GetItem(ItemID);
        Debug.Log(item.Name + " : " + amount);
        int slotID;
        InventoryData data;

        if (amount > 0)
        FetchInventoryByID(ItemID, true, out slotID, out data);
        else FetchInventoryByID(ItemID, false, out slotID, out data);

        if (slotID == -1) { Debug.Log("Can't add more Unique items."); Sucess = false; return; }

        if (amount > 0)
        {
            if (data.Amount > 0)
            {
                if (data.Amount + amount <= item.MaxStack)
                {
                    SetSlot(slotID, ItemID, data.Amount + amount);
                }
                else
                {
                    SetSlot(slotID, ItemID, item.MaxStack);
                    AddItem(ItemID, item.MaxStack - data.Amount + amount, out isSucess);
                }
            }
            else
            {
                for (int i = 0; i < invData.Count; i++)
                {
                    if (invData[i].ItemID == -1)
                    {
                        if (amount > item.MaxStack)
                        {
                            SetSlot(i, ItemID, item.MaxStack);
                            AddItem(ItemID, item.MaxStack - amount, out isSucess);
                        } else
                        {
                            SetSlot(i, ItemID, amount);
                        }
                        hasSlot = true;
                        break;
                    }
                }

                if (!hasSlot) { Debug.Log("InventoryFull");} else { hasSlot = false; }
            }
        }
        else
        {
            if (data.Amount > 0)
            {
                if (data.Amount + amount >= 1)
                {
                    SetSlot(slotID, ItemID, data.Amount + amount);
                }
                else
                {
                    SetSlot(slotID, -1, 0);
                    AddItem(ItemID, data.Amount + amount, out isSucess);
                }
            }
        }

        if (SL == null) SL = GameObject.FindWithTag("Player").GetComponent<ShootingLogic>();
        SL.RefreshAmounts();

        Sucess = !hasSlot;
    }

    public void FetchInventoryByID(int itemID, bool ShouldHaveSpace, out int slotID, out InventoryData data)
    {
        InventoryData tempInv = new InventoryData(-1, 0);
        int index = 0;
        bool CanAdd = true;
        ItemData item = GetItem(itemID);

        foreach (InventoryData inv in invData)
        {
            if (inv.ItemID == itemID)
            {
                bool check = true;
                if (ShouldHaveSpace)
                {
                    if (inv.Amount == item.MaxStack)
                    {
                        check = false;
                    }
                }
                if (check)
                {
                    tempInv = inv;
                    break;
                }
                else
                {
                    if (item.IsUnique)
                    {
                        CanAdd = false;
                        break;
                    }
                }
            }
            index++;
        }

        if (CanAdd)
        {
            slotID = index;
            data = tempInv;
        }
        else
        {
            slotID = -1;
            data = new InventoryData(-1, 0);
        }
    }
}
