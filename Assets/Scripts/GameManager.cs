using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool isGameStarted = false;
    public static bool isGameEnded = false;
    public GameObject StartScreen, FinishScreen, FailedScreen;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        OnLevelStarted();
        
    }

    public void OnLevelStarted()
    {
        isGameStarted = true;
        StartScreen.SetActive(false);
    }

    public void OnLevelCompleted()
    {
        isGameEnded = true;
        FinishScreen.SetActive(true);
    }

    public void OnLevelFailed()
    {
        isGameEnded = true;
        FailedScreen.SetActive(true);
    }
}
