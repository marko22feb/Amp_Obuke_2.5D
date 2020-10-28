using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public int SlotID;

    private Image icon;
    private Text amountText;
    private int ItemID;
    private int Amount;
    private Sprite sprite;

    private void Awake()
    {
        icon = transform.GetChild(0).GetComponent<Image>();
        amountText = transform.GetChild(1).GetComponent<Text>();
    }

    private void Start()
    {
        Inventory.InventoryData invData = Inventory.inv.GetSlot(SlotID);
        ItemID = invData.ItemID;
        Amount = invData.Amount;

        Inventory.ItemData itemData = Inventory.inv.GetItem(ItemID);
        sprite = itemData.sprite;

        icon.sprite = sprite;
        if (Amount != 0) amountText.text = "x" + Amount; else amountText.text = "";
    }
}
