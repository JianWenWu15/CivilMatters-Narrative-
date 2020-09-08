using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    public int maxTension = 100;
    public int currentTension;
    public TensionBar tensionBar;

    void Start()
    {
        currentTension = maxTension / 2;
        tensionBar.SetMaxTension(maxTension);
    }
    void Update()
    {
        // add tension
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddTension(5);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RemoveTension(5);
        }
    }
    void RemoveTension(int damage)
    {
        currentTension -= damage;
        tensionBar.SetTension(currentTension);
    }
    void AddTension(int damage)
    {
        currentTension += damage;
        tensionBar.SetTension(currentTension);
    }
}
