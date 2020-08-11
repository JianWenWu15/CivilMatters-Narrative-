using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MoveScene2D : MonoBehaviour
{
    [SerializeField] public string newLevel;
    public bool isAdditive = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isAdditive == false)
            {
                SceneManager.LoadScene(newLevel);
            }
            else
            {
                SceneManager.LoadScene(newLevel);
            }

        }
    }
}
