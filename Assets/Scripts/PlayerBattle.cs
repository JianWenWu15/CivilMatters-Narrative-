using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    public  int maxTension = 100;
    public int currentTension;
    public TensionBar tensionBar;
    // Start is called before the first frame update
    void Start()
    {
        currentTension = maxTension;
        tensionBar.SetMaxTension(maxTension);
    }

    // Update is called once per frame
    void Update()
    {
        // add tension
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddTension(5);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            AddTension(10);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            AddTension(15);
        }
        //  remove tension
        if (Input.GetKeyDown(KeyCode.F))
        {
            RemoveTension(5);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            RemoveTension(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            RemoveTension(15);
        }
    }
    void RemoveTension(int damage)
    {
        currentTension -= damage;

        tensionBar.SetHealth(currentTension);
    }
    void AddTension(int damage)
    {
        currentTension += damage;

        tensionBar.SetHealth(currentTension);
    }
}
