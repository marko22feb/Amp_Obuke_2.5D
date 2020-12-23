using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueConditions : MonoBehaviour
{
    public virtual bool IsConditionMet()
    {
        return true;
    }
}
