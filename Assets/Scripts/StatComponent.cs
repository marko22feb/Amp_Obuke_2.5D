using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class StatComponent : NetworkBehaviour
{
    public float CurrentHP;
    public float MaxHP;

    public Slider HUD_HP_Bar;
    public Slider Floating_HP_Bar;

    private Slider HP_Bar;

    public void Start()
    {
        if (hasAuthority)
        {
            HUD_HP_Bar = GameObject.Find("HUD_HP_Slider").GetComponent<Slider>();
            HP_Bar = HUD_HP_Bar;
        }
        else
        {
            HP_Bar = Floating_HP_Bar;
            Floating_HP_Bar.transform.parent.GetComponent<Canvas>().enabled = true;
        }

        GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent< NetworkIdentity > ().connectionToClient);
    }

    [Command]
    public void ModifyHP(float amount)
    {
        ModifyHPRPC(amount);
    }

    [ClientRpc]
    public void ModifyHPRPC(float amount)
    {
        ModifyStat(amount);
    }

    public void ModifyStat(float amount)
    {
        CurrentHP += amount;
        if (CurrentHP <= 0)
        {
            Death();
        }
        else if (CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }

        if (HP_Bar != null)
        {
            HP_Bar.value = CurrentHP / MaxHP;
        }
    }


    [ClientRpc]
    public void Death()
    {
        GetComponent<Animator>().SetTrigger("IsDead");
        GetComponent<PawnController>().IsLogicEnabled = false;
        gameObject.layer = 9;
        if (Floating_HP_Bar != null)
            Floating_HP_Bar.transform.parent.GetComponent<Canvas>().enabled = false;
    }
}
