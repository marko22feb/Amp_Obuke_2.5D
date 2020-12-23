using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfAfterDelay : NetworkBehaviour
{
    public float flightDestroy = 3f;

    public void Start()
    {
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(flightDestroy);
        ServerDestroy();
    }

 //   [ClientRpc]
    public void ServerDestroy()
    {
        Destroy(gameObject);
    }
}
