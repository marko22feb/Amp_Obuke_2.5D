using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PawnController : MonoBehaviour
{
    Animator anim;
    public bool InputEnabled = true;
    public bool CanInflictDamage = false;
    public bool IsLogicEnabled = true;
    public bool CanChainCombo = false;
    public bool ComboChained = false;
    public bool AttackSpammed = false;
    public List<GameObject> TargetsHit;

    public virtual void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    public void Attack()
    {
        anim.SetTrigger("ExecuteAttack");
        if (CanChainCombo) ComboChained = true;
        if (ComboChained) AttackSpammed = true;
    }

    public void ChainCombo()
    {
        if (ComboChained && !AttackSpammed)
        {
            anim.SetTrigger("Combo");
            ComboChained = false;
        }

        AttackSpammed = false;
    }

    public void EndAttack()
    {
        anim.SetTrigger("AttackIsOver");
        InputEnabled = true;
    }

    public void UpdateSpeedVariable(float speed)
    {
        anim.SetFloat("Speed", speed);
    }

    public void ResetAttackTrigger()
    {
        anim.ResetTrigger("ExecuteAttack");
        InputEnabled = false;
    }

    public void DamageON()
    {
        TargetsHit.Clear();
        CanInflictDamage = true;
    }

    public void DamageOFF()
    {
        CanInflictDamage = false;
    }

    public void ComboON()
    {
        CanChainCombo = true;
    }

    public void ComboOFF()
    {
        CanChainCombo = false;
    }

    public void InputEnabledON()
    {
        InputEnabled = true;
    }

    public void InputEnabledOFF()
    {
        InputEnabled = false;
    }

    public bool AlreadyHit(GameObject target)
    {
        bool temp = false;
        foreach (GameObject gob in TargetsHit)
        {
            if (gob == target) { temp = true; }
        }
        return temp;
    }

    public void GotHit()
    {
        if(GetComponent<StatComponent>().CurrentHP > 0)
        anim.SetTrigger("GotHit");
    }
}
