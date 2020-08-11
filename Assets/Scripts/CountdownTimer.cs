using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountdownTimer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 10f;
    [SerializeField] Text countdownText;

    public override bool Equals(object obj)
    {
        return obj is CountdownTimer timer &&
               base.Equals(obj) &&
               EqualityComparer<Text>.Default.Equals(countdownText, timer.countdownText);
    }

    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        // if coundown hits 0 select random button
        if (currentTime <= 0)
        {
            currentTime = 0;
        }

    }
}
