using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueEvents : MonoBehaviour
{

    public UnityEvent ThisFunctionToCall;

    public void AddAmmo(int amount)
    {
        bool temp;
        Inventory.inv.AddItem(1, amount, out temp);
    }

    public void FillMaxAmmo()
    {

    }
}
