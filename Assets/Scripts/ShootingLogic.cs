using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ShootingLogic : NetworkBehaviour
{
    public int currentAmmo = 60;
    public int currentAmmoToDisplay = 30;
    public int maximumAmmo = 300;
    public int currentClipAmmo = 0;
    public int clipMaxAmmo = 30;
    public Text AmmoText;

    public void Start()
    {
        if (!hasAuthority) return;
        AmmoText = GameObject.Find("AmmunitionText").GetComponent<Text>();
        GetAmmoAmount(true);
        UpdateUI(); 
    }

    public void GetAmmoAmount(bool Initialize)
    {
        int slotId;
        Inventory.InventoryData invdata;
        Inventory.inv.FetchInventoryByID(1, false, out slotId, out invdata);
        currentAmmo = invdata.Amount;
        if (Initialize)Reload();
        currentAmmoToDisplay = currentAmmo - currentClipAmmo;
    }

    public void RefreshAmounts()
    {
        GetAmmoAmount(false);
        UpdateUI();
    }

    public void SubstractAmmo(int amount)
    {
        currentClipAmmo -= amount;
        currentClipAmmo = Mathf.Clamp(currentClipAmmo, 0, clipMaxAmmo);
        bool temp;
        Inventory.inv.AddItem(1, -1, out temp);

        UpdateUI();
    }

    public void Reload()
    {
        currentAmmoToDisplay -= clipMaxAmmo - currentClipAmmo;
        int newClipAmmo = clipMaxAmmo;

        if (currentAmmoToDisplay < 0) { newClipAmmo = clipMaxAmmo + currentAmmoToDisplay; }

        currentClipAmmo = newClipAmmo;

        currentAmmoToDisplay = Mathf.Clamp(currentAmmoToDisplay, 0, maximumAmmo);

      //  currentAmmoToDisplay = currentAmmo - currentClipAmmo;

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (!hasAuthority) return;
        AmmoText.text = currentClipAmmo + " / " + currentAmmoToDisplay;
    }
}
