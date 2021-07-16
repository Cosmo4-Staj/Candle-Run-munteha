using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool isGameStarted = false;
    public static bool isGameEnded = false;
    public static bool isGameAlreadyOpen = false;
    public GameObject StartScreen, FinishScreen, FailedScreen;
    
    //SCORE
    public int currentFlame;
    [SerializeField] TextMeshProUGUI scoreText;

    //LEVEL
    public static int levelCount;
    //public TextMeshProUGUI levelText; 
    public List<GameObject> Levels = new List<GameObject>();
    public GameObject levelText;
    public GameObject StartPanel;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        currentFlame = 0;
    }

    void Start()
    {
        isGameStarted = false;
        isGameEnded = false;
        if (isGameAlreadyOpen == true)
        {
            StartPanel.SetActive(false);
            OnLevelStarted();
        }
            LoadLevel();
    }
    void Update()
    {
        levelText.GetComponent<TextMeshProUGUI>().SetText("Level: " + (levelCount + 1).ToString());
    }
    public void NextLevel()
    {
        levelCount++;
        PlayerPrefs.SetInt("LevelNo", levelCount);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadLevel()
    {
        levelCount = PlayerPrefs.GetInt("LevelNo", 0);

        if(levelCount >Levels.Count - 1 || levelCount < 0)
        {
            levelCount = 0;
            PlayerPrefs.SetInt("LevelNo",levelCount);
        }
        Instantiate(Levels[levelCount], Vector3.zero, Quaternion.identity);
        //Instantiate(Levels[PlayerPrefs.GetInt("LevelNo")], transform.position, Quaternion.identity);
    }

    public void OnLevelStarted()
    {
        isGameAlreadyOpen = true;
        isGameStarted = true;
        StartScreen.SetActive(false);
    }

    public void OnLevelCompleted()
    {
        FinishScreen.SetActive(true);
    }

    public void OnLevelFailed()
    {
        isGameEnded = true;
        FailedScreen.SetActive(true);
    }

    //SCORE

    public void AddFlame(int flameToAdd)
    {
        currentFlame += flameToAdd;
        scoreText.text = "Score: " + currentFlame;
    }
    public void RemoveFlame(int flameToRemove)
    {
        currentFlame -= Mathf.Abs(flameToRemove);

        if (currentFlame < 0)
        {
            //Reload level
            currentFlame = 0;
        }
        scoreText.text = "Score: " + currentFlame;
    }
}

