using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public GameObject Owner;
    public GameObject BulletParticle;
    public StatComponent stat;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Owner)
        {
            if (other.GetComponent<StatComponent>() != null)
            {
                stat = other.GetComponent<StatComponent>();
                EnemyHit();
            }
            OnImpact();
        }
    }

    [ClientRpc]
    public void EnemyHit()
    {
        stat.ModifyHP(-10f);
    }

    [ClientRpc]
    public void OnImpact()
    {
        GameObject bulletParticle = Instantiate(BulletParticle, transform.position, transform.rotation);
        Destroy(gameObject);
        NetworkServer.Spawn(bulletParticle, gameObject);
    }
}
