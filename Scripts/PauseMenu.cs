using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For Pause menu
public class PauseMenu : MonoBehaviour {

    public GameObject PauseMenuUi;

    public void Pause()
    {
        PauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameManager.instance.GamePaused = true;
    }

    public void Resume()
    {
        PauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameManager.instance.GamePaused = false;
        StartCoroutine(GameManager.instance.CreateBalloonPerSeconds());
    }
}
