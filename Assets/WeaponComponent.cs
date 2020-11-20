using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    Vector3 lastKnownPosition;
    public GameObject Owner;
    PawnController ControlledPawn;

    public void Start()
    {
        lastKnownPosition = transform.position;
        ControlledPawn = Owner.GetComponent<PawnController>();
    }

    public void FixedUpdate()
    {
        if (ControlledPawn.CanInflictDamage)
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, lastKnownPosition, out hit))
            {
                if (hit.collider.GetComponent<StatComponent>() != null)
                {
                    if (hit.collider.gameObject != Owner)
                    {
                        if (!ControlledPawn.AlreadyHit(hit.collider.gameObject) && hit.collider.GetComponent<StatComponent>().CurrentHP > 0)
                        {
                            ControlledPawn.TargetsHit.Add(hit.collider.gameObject);
                            hit.collider.GetComponent<StatComponent>().ModifyHP(-35);
                            hit.collider.GetComponent<PawnController>().GotHit();
                        }
                    }
                }
            }
        }

        lastKnownPosition = transform.position;
    }
}
