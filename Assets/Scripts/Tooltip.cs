using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public int ItemID;
    public Inventory.ItemData item;
    public NavigationItem navItem;
    public Text Name;
    public Text itemType;

    private void Awake()
    {
        Name = transform.parent.GetChild(0).GetComponent<Text>();
        itemType = transform.parent.GetChild(1).GetComponent<Text>();
    }

    private void Start()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void UpdateTooltip()
    {
        Inventory.InventoryData invData = Inventory.inv.GetSlot(navItem.GetComponent<InventorySlot>().SlotID);
        ItemID = invData.ItemID;
        item = Inventory.inv.GetItem(ItemID);
        Name.text = item.Name;
        itemType.text = item.itemType.ToString();
    }

    public void OnButtonClicked(int Command)
    {
        switch (Command)
        {
            case 1:
                Debug.Log("ItemUsed");
                break;
            case 2:
                Debug.Log("ItemDestroyed");
                break;
            case 3:
                OnEscape();
                break;
            default:
                break;
        }
    }

    public void OnEscape()
    {
        NavigationManager.navM.ClearSelection();
        NavigationManager.navM.NewSelection(GetComponent<NavigationLayoutObject>().prevNLO);
        transform.parent.gameObject.SetActive(false);
    }
}
