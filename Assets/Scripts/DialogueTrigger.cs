using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Dialogue narrative;
    public void TriggerDialogue()
    {
        FindObjectOfType<BattleManager>().StartDialogue(dialogue);
        // if dialouge ends
    }

    public void TriggerNarrative()
    {
        FindObjectOfType<BattleManager>().StartNarrative(narrative);
    }
}
