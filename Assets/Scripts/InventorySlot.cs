using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler

{
    public int SlotID;

    private Image icon;
    private Text amountText;
    private int ItemID;
    private int Amount;
    private Sprite sprite;
    
    public Inventory.EquipType equipType = Inventory.EquipType.Null;


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (ItemID == -1) return;
        UpdateNavigation();
        transform.GetChild(0).SetParent(transform.parent.parent);
        icon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (ItemID == -1) return;
        icon.transform.parent.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<InventorySlot>().ItemID == -1) return;

        int currentItemId = ItemID;
        int currentAmount = Amount;
        int currentSlotID = SlotID;
        Inventory.EquipType currentEquipType = equipType;

        int draggedSlotID = eventData.pointerDrag.GetComponent<InventorySlot>().SlotID;
        int draggedItemID = -1;
        int draggedAmount = 0;
        Inventory.EquipType draggedEquipType = Inventory.EquipType.Null;

        if (eventData.pointerDrag.GetComponent<InventorySlot>().equipType == Inventory.EquipType.Null)
        {
            Inventory.InventoryData draggedInvData = Inventory.inv.GetSlot(draggedSlotID);

            draggedItemID = draggedInvData.ItemID;
            draggedAmount = draggedInvData.Amount;
        }
        else
        {
            Inventory.EquipmentData draggedInvData = Inventory.inv.GetEquip(eventData.pointerDrag.GetComponent<InventorySlot>().equipType);

            draggedItemID = draggedInvData.ItemID;
            draggedAmount = draggedInvData.Amount;
            draggedEquipType = draggedInvData.equipType;
        }

        if (eventData.pointerDrag.GetComponent<InventorySlot>().equipType == Inventory.EquipType.Null)
        {
            Inventory.inv.invData[draggedSlotID] = new Inventory.InventoryData(currentItemId, currentAmount);
        }
        else
        {
            Inventory.inv.equipData[draggedSlotID] = new Inventory.EquipmentData(currentEquipType, currentItemId, currentAmount);
        }

        if (equipType == Inventory.EquipType.Null)
        {
            Inventory.inv.invData[currentSlotID] = new Inventory.InventoryData(draggedItemID, draggedAmount);
        }
        else
        {
            Inventory.inv.equipData[currentSlotID] = new Inventory.EquipmentData(currentEquipType, draggedItemID, draggedAmount);
        }

        eventData.pointerDrag.GetComponent<InventorySlot>().RestorePosition();

        UpdateVisuals();
        UpdateNavigation();
        eventData.pointerDrag.GetComponent<InventorySlot>().UpdateVisuals();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<InventorySlot>().ItemID == -1) return;
        RestorePosition();
    }

    public void RestorePosition()
    {
        icon.transform.parent.SetParent(transform);
        icon.raycastTarget = true;
        icon.transform.parent.localPosition = new Vector3(0, 0, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UpdateNavigation();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ItemID == -1) return;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ItemID == -1) return;
    }

    private void Awake()
    {
        icon = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        amountText = transform.GetChild(0).GetChild(1).GetComponent<Text>();
    }

    private void Start()
    {
        UpdateVisuals();
        GetComponent<NavigationItem>().FunctionToCallOnEnter.AddListener(OnUse);
    }

    public void OnUse()
    {
        if (ItemID > 0)
      transform.parent.GetComponent<NavigationOnUseData>().OnInventorySlotUse(GetComponent<NavigationItem>());
    }

    public void UpdateNavigation()
    {
        NavigationManager nm = GameController.control.GetComponent<NavigationManager>();
        nm.selectedNavItem.DeSelect();
        nm.selectedNavItem = GetComponent<NavigationItem>();
        nm.selectedNavItem.Select();
    }

    public void UpdateVisuals()
    {
        if (equipType == Inventory.EquipType.Null)
        {
            Inventory.InventoryData invData = Inventory.inv.GetSlot(SlotID);
            ItemID = invData.ItemID;
            Amount = invData.Amount;
        }
        else
        {
            Inventory.EquipmentData equipData = Inventory.inv.GetEquip(equipType);
            ItemID = equipData.ItemID;
            Amount = equipData.Amount;
        }

        Inventory.ItemData itemData = Inventory.inv.GetItem(ItemID);
        sprite = itemData.sprite;

        icon.sprite = sprite;
        if (Amount != 0) amountText.text = "x" + Amount; else amountText.text = "";
    }
}
