using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TensionBar : MonoBehaviour
{ // Creating a public instance of Slider from Unity UI
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxTension(int tension)
    {
        slider.maxValue = tension;
        slider.value = tension;

        fill.color = gradient.Evaluate(1f);
    }
    public void SetTension(int tension)
    {
        slider.value = tension;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
