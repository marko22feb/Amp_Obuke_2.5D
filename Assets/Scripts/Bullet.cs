using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public GameObject Owner;
    public GameObject BulletParticle;
    public StatComponent stat;

    public void Start()
    {
        GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Owner)
        {
            if (other.GetComponent<StatComponent>() != null)
            {
                stat = other.GetComponent<StatComponent>();
                EnemyHitCMD(other.gameObject);
            }
            OnImpact();
        }
    }

    [Command]
    public void EnemyHitCMD(GameObject target)
    {
        EnemyHitRPC(target);
    }

    [ClientRpc]
    public void EnemyHitRPC(GameObject target)
    {
  //      target.GetComponent<StatComponent>().ModifyHP(-10f);
        EnemyHit(target);
    }

    public void EnemyHit(GameObject target)
    {
        target.GetComponent<StatComponent>().ModifyHP(-10f);
    }

    [ClientRpc]
    public void OnImpactRPC()
    {
        Impact();
    }

    [Command]
    public void OnImpact()
    {
        OnImpactRPC();
    }

    public void Impact()
    {
        GameObject bulletParticle = Instantiate(BulletParticle, transform.position, transform.rotation);
        NetworkServer.Spawn(bulletParticle, gameObject);
        Destroy(gameObject);
    }
}
