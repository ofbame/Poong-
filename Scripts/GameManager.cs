using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject[] BalloonPrefab;

    public Text scoreText;
    public Text wordText;
    public Text scoreTextPanel;
    public Text countDownPanel;
    public Text highScoreText;

    public GameObject EndPanel;

    public AudioSource audioSouce;
    public AudioClip btnClip;

    public static GameManager instance = null;

    public int curBalloonValue;
    public int score;
    public bool done;
    public float coolTime;
    public bool GamePaused;

    public string TargetWord;

    float timeLeft;
    bool start;
    string Alphabet = "ABCDEFGHIJKLNMOPQRSTUVWXYZ";

    //Setup GameManager as Instance
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Screen.SetResolution(1920, 1080, true);
        start = false;
        timeLeft = 30.0f;
    }

    //If this is word mode, there is time limits
    private void Update()
    {
        if (CheckScene() == "word")
        {
            if (start == true)
            {
                timeLeft -= Time.deltaTime;
                countDownPanel.text = "Time Left : " + Mathf.Round(timeLeft);
            }
            if (timeLeft < 0)
            {
                EndStage();
            }
        }
    }

    // Reset all values
    void ResetValues()
    {
        timeLeft = 30.0f;
        start = true;
        score = 0;
        curBalloonValue = 0;
        done = false;
        GamePaused = false;
        Time.timeScale = 1f;
        if(CheckScene() == "word")
            GetWordFromList();
        SetupScore();
    }

    // Set new word from list
    public void GetWordFromList()
    {
        TargetWord = GetComponent<Words>().WordList[Random.Range(0, GetComponent<Words>().WordList.Length)];
        SetupScore();
    }

    //Reset and start Coroutine
    public void StartBtn()
    {
        ResetValues();
        StartCoroutine(CreateBalloonPerSeconds()); 
    }

    //Setup regen Time Here
    public IEnumerator CreateBalloonPerSeconds()
    {
        coolTime = 0.6f;

        while( !done && !GamePaused)
        {
            if (IsWithin(score, 0, 9))
                coolTime = 0.5f;
            else if (IsWithin(score, 10, 19))
                coolTime = 0.4f;
            else if (IsWithin(score, 20, 29))
                coolTime = 0.3f;
            else if (IsWithin(score, 30, 39))
                coolTime = 0.2f;
            else
                coolTime = 0.2f;

            CreateBalloon();
            yield return new WaitForSecondsRealtime(coolTime);
        }
    }

    // Create Correct type balloons
    void CreateBalloon()
    {
        if (CheckScene() == "number")
        {
            GameObject temp = Instantiate(BalloonPrefab[0], new Vector3(Random.Range(-7, 7), -6, 0), Quaternion.identity);
            curBalloonValue++;
            temp.GetComponent<Balloon>().value = curBalloonValue;
        }
        else if(CheckScene() == "word")
        {
            if (TargetWord.Length == 0)
            {
                GetWordFromList();
                timeLeft += 5f;
            }

            GameObject temp = Instantiate(BalloonPrefab[1], new Vector3(Random.Range(-7, 7), -6, 0), Quaternion.identity);
            if( Random.Range(0,3) == 2)
            {
                if (curBalloonValue == TargetWord.Length)
                    curBalloonValue = 0;

                Debug.Log("curBalloonValue : " + curBalloonValue +" word : " + TargetWord);
                temp.GetComponent<WordBalloon>().value = TargetWord[curBalloonValue];
                curBalloonValue++;
            }
            else
            {
                temp.GetComponent<WordBalloon>().value = Alphabet[Random.Range(0, 26)];
            }
        }
    }
    
    //setup Showing Score on Main Screen and End Screen
    // Show Target Word Also
    public void SetupScore()
    {
        if( CheckScene() == "word")
            wordText.text = TargetWord;

        scoreText.text = "Score : " + score;
        scoreTextPanel.text = "You did\n " + score +" Poongs!";
        highScoreText.text = "HighScore : " + LoadData() + " poongs";
    }

    //End the stage and show end panel
    //Save data when it ends;
    public void EndStage()
    {
        start = false;
        done = true;
        EndPanel.SetActive(true);
        SaveData();
        SetupScore();
    }

    //Restart this mode
    public void Restart()
    {
        EndPanel.SetActive(false);
        ResetValues();
        StartBtn();
        SetupScore();
    }

    // return "number" when it is Numbers Mode 
    // return "word" when it is Words mode
    string CheckScene()
    {
        if (SceneManager.GetActiveScene().name == "CatchNumbers")
        {
            return "number";
        }
        else if (SceneManager.GetActiveScene().name == "PoongWords")
        {
            return "word";
        }
        else
            return null;
    }

    void SaveData()
    {
        if (LoadData() < score)
        {
            if (CheckScene() == "number")
                PlayerPrefs.SetInt("Num", score);
            else if (CheckScene() == "word")
                PlayerPrefs.SetInt("Word", score);
        }
    }

    int LoadData()
    {
        if (CheckScene() == "number")
            return PlayerPrefs.GetInt("Num", 0);
        else if (CheckScene() == "word")
            return PlayerPrefs.GetInt("Word", 0);
        else
            return 0;
    }

    /////// Util methods from here

    public bool IsWithin(int value, int min, int max)
    {
        return value >= min && value <= max;
    }

    public void ButtonSoundEffect()
    {
        audioSouce.clip = btnClip;
        audioSouce.Play();
    }
}
