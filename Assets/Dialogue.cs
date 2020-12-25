using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public List<string> Dialogues;
    public List<bool> IsChoice;

    [System.Serializable]
    public struct DialoguesLeadTo { public List<int> LeadTo; }
    public List<DialoguesLeadTo> dialogueLeadTo;

    public List<DialogueEvents> events;
    public List<DialogueConditions> conditions;

    public Canvas dialogueCanvas;
    public Text dialogueText;
    public GameObject dialogueMenu;
    public GameObject dialogueChoice;

    private int currentDialogue = 0;

    public void Start()
    {
        OnDialogueStart();
    }

    public void OnDialogueStart()
    {
        dialogueCanvas.enabled = true;
        dialogueText.text = Dialogues[0];
        SpawnDialogueChoices();
    }

    public void OnDialogueOver()
    {
        Time.timeScale = 1;
        dialogueCanvas.enabled = false;
    }

    public void NextDialogue(int index)
    {
        if (events[index] != null) events[index].ThisFunctionToCall.Invoke();

        if (dialogueLeadTo[index].LeadTo[0] > 0)
        {
            currentDialogue = index;
            SpawnDialogueChoices();
        } else
        {
            OnDialogueOver();
        }
    }

    public void SpawnDialogueChoices()
    {
        int loopcount = dialogueMenu.transform.childCount;
        if (loopcount > 0)
        {
            for (int i = 0; i < loopcount; i++)
            {
                Destroy(dialogueMenu.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < dialogueLeadTo[currentDialogue].LeadTo.Count; i++)
        {
            if (IsChoice[dialogueLeadTo[currentDialogue].LeadTo[i]])
            {
                GameObject temp = Instantiate(dialogueChoice, dialogueMenu.transform);
                temp.GetComponentInChildren<Text>().text = Dialogues[dialogueLeadTo[currentDialogue].LeadTo[i]];
                temp.GetComponent<DialogueChoice>().dialogue = this;
                temp.GetComponent<DialogueChoice>().Index = dialogueLeadTo[currentDialogue].LeadTo[i];

                if (conditions[dialogueLeadTo[currentDialogue].LeadTo[i]] != null)
                {
                    if (!conditions[dialogueLeadTo[currentDialogue].LeadTo[i]].IsConditionMet(-1))
                    {
                        temp.GetComponentInChildren<Text>().color = Color.grey;
                        temp.GetComponent<DialogueChoice>().IsConditionMet = false;
                    }
                }
            }
            else
            {
                dialogueText.text = Dialogues[dialogueLeadTo[currentDialogue].LeadTo[i]];
            }
        }
    }
}
