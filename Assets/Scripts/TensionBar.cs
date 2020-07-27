using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TensionBar : MonoBehaviour
{ // Creating a public instance of Slider from Unity UI
    public Slider slider;
    public Gradient gradient;
    public Image fill; 
    public void SetTention(int tention)
    {
        slider.value =  tention;

    }

    public void SetMaxTention(int tention)
    {
        slider.maxValue = tention;
        slider.value = tention;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int tention)
    {
        slider.value = tention;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
