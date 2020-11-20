using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatComponent : MonoBehaviour
{
    public float CurrentHP;
    public float MaxHP;

    public Slider HP_Bar;

    public void ModifyHP(float amount)
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

    public void Death()
    {
        GetComponent<Animator>().SetTrigger("IsDead");
        GetComponent<PawnController>().IsLogicEnabled = false;
        gameObject.layer = 9;
        if (HP_Bar != null)
        HP_Bar.transform.parent.GetComponent<Canvas>().enabled = false;
    }
}
