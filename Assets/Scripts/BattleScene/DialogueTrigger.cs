using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Dialogue narrative;
    public void TriggerBattle()
    {
        FindObjectOfType<BattleManager>().StartDialogue(dialogue);
    }

    public void TriggerNarrative()
    {
        FindObjectOfType<BattleManager>().StartNarrative(narrative);
    }
}
