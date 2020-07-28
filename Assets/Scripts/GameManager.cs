using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public enum GameState
    {
        INTRO,
        BERKLAND,
        BATTLE,
        RESULTS,
        ENDING
    }
    private GameState currentState = GameState.INTRO;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
        set { }
    }

    // Global Variables?!
<<<<<<< HEAD
    // private int howManyBattleWon = 0;
=======
    private int howManyBattleWon = 0;
>>>>>>> demo

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("[GameManager] There are multiple instances of GameManager.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        switch (currentState)
        {
            case GameState.INTRO:
                break;

            case GameState.BERKLAND:
                break;

            case GameState.BATTLE:
                break;
        }

    }

    public void SetState(GameState newState)
    {
        Debug.Log("[GameManager] ENTERING SCENE " + newState);
        currentState = newState;

        // which state we are going to
        switch (currentState)
        {
            case GameState.INTRO:
                SceneManager.LoadScene("Main");
                break;

            case GameState.BERKLAND:
                SceneManager.LoadScene("Berkland");
                break;

            case GameState.BATTLE:
                SceneManager.LoadScene("Battle");
                break;
        }
    }
}
