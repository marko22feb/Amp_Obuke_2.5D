using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationOnUseData : MonoBehaviour
{
    public void OnInventorySlotUse(NavigationItem nav)
    {
        GetComponent<NavigationLayoutObject>().tooltip.transform.parent.gameObject.SetActive(true);
        GetComponent<NavigationLayoutObject>().tooltip.navItem = nav;
        GetComponent<NavigationLayoutObject>().tooltip.UpdateTooltip();
        GetComponent<NavigationLayoutObject>().tooltip.GetComponent<NavigationLayoutObject>().prevNLO = NavigationManager.navM.navObj;
        NavigationManager.navM.ClearSelection();
        NavigationManager.navM.NewSelection(GetComponent<NavigationLayoutObject>().tooltip.GetComponent<NavigationLayoutObject>());
    }
}
