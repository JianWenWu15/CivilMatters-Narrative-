using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HidePanel : MonoBehaviour
{
    public GameObject DecisionPanel;
    int counter = 0;
    // Start is called before the first frame update
    public void showHidePanel()
    {
        counter++;
        if (counter % 2 == 1)
        {
            DecisionPanel.gameObject.SetActive(false);
        }
        else
        {
            DecisionPanel.gameObject.SetActive(true);
        }

    }
}
