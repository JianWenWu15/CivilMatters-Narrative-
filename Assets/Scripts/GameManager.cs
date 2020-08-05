using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public enum GameState { Menu, NewsIntro, Berkland, BattleScene, Results, Transitions }
    private GameState currentState = GameState.Menu;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
        set { }
    }

    // Global Variables?!
    // private int aggressiveChoices = 0;
    // public float tention = 100f;
    // private int howManyBattleWon = 0;

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
            case GameState.Menu:
                break;

            case GameState.NewsIntro:
                break;

            case GameState.Berkland:
                break;

            case GameState.BattleScene:
                print("in battle scene");
                break;

            case GameState.Transitions:
                break;

            case GameState.Results:
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
            case GameState.Menu:

                SceneManager.LoadScene("Menu");
                break;

            case GameState.NewsIntro:
                SceneManager.LoadScene("NewsIntro");
                break;


            case GameState.Berkland:
                SceneManager.LoadScene("Berkland");
                break;

            case GameState.BattleScene:
                SceneManager.LoadScene("BattleScene");
                break;
            case GameState.Results:
                SceneManager.LoadScene("Results");
                break;
        }
    }
}
