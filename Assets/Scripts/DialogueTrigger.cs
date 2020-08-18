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
        FindObjectOfType<BattleManager>().StartNarrative(narrative);
    }
}
