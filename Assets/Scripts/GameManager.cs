using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool isGameStarted = false;
    public static bool isGameEnded = false;

    public void OnLevelStarted()
    {
        isGameStarted = true;
    }

    public void OnLevelEnded()
    {

    }

    public void OnLevelCompleted()
    {

    }

    public void OnLevelFailed()
    {

    }
}
