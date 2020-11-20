using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : PawnController
{
    GameObject Target = null;
    public LayerMask layer = default;

    public void Update()
    {
        if (Target == null)
        {
            Collider[] OverLappedActors = Physics.OverlapSphere(transform.position, 3f, layer);

            foreach (Collider col in OverLappedActors)
            {
                if (col.gameObject.tag == "Player")
                {
                    Target = col.gameObject;
                    break;
                }
            }
        }
        else
        {
            float distance = Vector3.Distance(this.transform.position, Target.transform.position);
            if (distance < 1.55f)
            {
                Attack();
            } else
            {
                GetComponent<NavMeshAgent>().Move(Target.transform.position);
            }
        }
    }
}
