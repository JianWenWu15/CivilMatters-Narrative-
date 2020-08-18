using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleManager : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 10f;
    [SerializeField] Text countdownText;
    public Animator beginAnimator;
    public Animator dialogueAnimator;
    public Animator narrativeAnimator;
    public Animator decisionAnimator;
    public Animator optionsAnimator;
    public Text nameText;
    public Text dialogueText;
    private Queue<string> sentences;
    void Start()
    {
        sentences = new Queue<string>();
        currentTime = startingTime;
        beginAnimator.SetBool("begin", true);
        decisionAnimator.SetBool("DecisionTime", true);
    }
    public void StartDialogue(Dialogue dialogue)
    {
        beginAnimator.SetBool("begin", false);
        dialogueAnimator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
    }
    public void StartNarrative(Dialogue narrative)
    {
        narrativeAnimator.SetBool("narrativeOpen", true);
        nameText.text = narrative.name;

        sentences.Clear();

        foreach (string sentence in narrative.sentences)
        {
            sentences.Enqueue(sentence);
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        Debug.Log(sentence);
    }
    public void EndDialogue()
    {
        Debug.Log("End of conversation");
        dialogueAnimator.SetBool("IsOpen", false);
        narrativeAnimator.SetBool("narrativeOpen", false);
        decisionAnimator.SetBool("DecisionTime", false);
    }
    void update()
    {
        //if  dicisionAnimator.SetBool("decisionTime") = falses
        // {
        //     currentTime -= 1 * Time.deltaTime;
        //     countdownText.text = currentTime.ToString();
        // }

    }
}
