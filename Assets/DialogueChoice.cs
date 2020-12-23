using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueChoice : MonoBehaviour, IPointerClickHandler
{
    public Dialogue dialogue;
    public int Index;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        dialogue.NextDialogue(Index);
    }
}
