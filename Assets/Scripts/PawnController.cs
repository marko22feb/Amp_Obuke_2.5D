using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Mirror;

public class PawnController : NetworkBehaviour
{
    Animator anim;
    public ShootingLogic SL;
    public bool InputEnabled = true;
    public bool CanInflictDamage = false;
    public bool IsLogicEnabled = true;
    public bool CanChainCombo = false;
    public bool ComboChained = false;
    public bool AttackSpammed = false;
    public List<GameObject> TargetsHit;
    public GameObject bulletPrefab;
    public GameObject gunMuzzle;
    public Camera mainPlayerCamera;



    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        SL = GetComponent<ShootingLogic>();
    }

    [Command]
    public void cmdAttack(Vector3 vel, GameObject ow)
    {
        GameObject tempBullet = NetworkManager.Instantiate(bulletPrefab, gunMuzzle.transform.position, gunMuzzle.transform.rotation);
        tempBullet.GetComponent<Rigidbody>().velocity = vel;
        tempBullet.GetComponent<Bullet>().Owner = ow;

        NetworkServer.Spawn(tempBullet, gameObject);
       rpcAttack(tempBullet, vel, ow);
    }

    [ClientRpc]
    public void rpcAttack(GameObject bullet, Vector3 vel, GameObject ow)
    {
        bullet.GetComponent<Rigidbody>().velocity = vel;
        bullet.GetComponent<Bullet>().Owner = ow;
    }

    public void Attack()
    {
        anim.SetTrigger("ExecuteAttack");
        SL.SubstractAmmo(1);

        Ray ray = mainPlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 impactPoint;
        impactPoint = ray.GetPoint(10000);
        Vector3 vel = (impactPoint - gunMuzzle.transform.position).normalized * 30f;

        cmdAttack(vel, this.gameObject);
    }

    [ClientRpc]
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

    [ClientRpc]
    public void GotHit()
    {
        if(GetComponent<StatComponent>().CurrentHP > 0)
        anim.SetTrigger("GotHit");
    }
}
