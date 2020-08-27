using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ilumisoft.VisualStateMachine;

public class StateTrigger : MonoBehaviour
{
    [SerializeField]
    StateMachine stateMachine = null;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            stateMachine.Trigger("Battle Scene");
        }
    }
}
