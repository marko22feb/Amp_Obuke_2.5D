using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ShootingLogic : NetworkBehaviour
{
    public int currentAmmo = 60;
    public int maximumAmmo = 200;
    public int currentClipAmmo = 0;
    public int clipMaxAmmo = 30;
    public Text AmmoText;

    public void Start()
    {
        if (!hasAuthority) return;
        AmmoText = GameObject.Find("AmmunitionText").GetComponent<Text>();
        GetAmmoAmount();
        UpdateUI(); 
    }

    public void GetAmmoAmount()
    {
        int slotId;
        Inventory.InventoryData invdata;
        Inventory.inv.FetchInventoryByID(1, false, out slotId, out invdata);
        currentAmmo = invdata.Amount;
        Reload();
    }

    public void PickUpAmmo(int amount)
    {

    }

    public void SubstractAmmo(int amount)
    {
        currentClipAmmo -= amount;
        currentClipAmmo = Mathf.Clamp(currentClipAmmo, 0, clipMaxAmmo);
        
        UpdateUI();
    }

    public void Reload()
    {
        currentAmmo -= clipMaxAmmo - currentClipAmmo;
        int newClipAmmo = clipMaxAmmo;

        if (currentAmmo < 0) { newClipAmmo = clipMaxAmmo + currentAmmo; }

        currentClipAmmo = newClipAmmo;

        currentAmmo = Mathf.Clamp(currentAmmo, 0, maximumAmmo);

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (!hasAuthority) return;
        AmmoText.text = currentClipAmmo + " / " + currentAmmo;
    }
}
