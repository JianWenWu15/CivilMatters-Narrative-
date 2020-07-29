using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour

{
    public Animator transition;
    public float transitionTime = 1f;

    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        // wait
        yield return new WaitForSeconds(transitionTime);
        // add button to scene
        SceneManager.LoadScene(levelIndex);
    }
}