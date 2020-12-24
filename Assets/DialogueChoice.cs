using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueChoice : MonoBehaviour, IPointerClickHandler
{
    public Dialogue dialogue;
    public int Index;
    public bool IsConditionMet = true;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (IsConditionMet)
        dialogue.NextDialogue(Index);
    }
}
