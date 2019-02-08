using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On Title screen, create some balloons for active screen
public class TitleController : MonoBehaviour {

    float coolTime;
    public GameObject BalloonPrefab;

    public AudioSource audioSouce;
    public AudioClip btnClip;

	void Start ()
    {
        Time.timeScale = 1f;
        StartCoroutine(CreateBalloonPerSeconds());
    }

    public IEnumerator CreateBalloonPerSeconds()
    {
        coolTime = 2f;

        while (true)
        {
            CreateBalloon();
            yield return new WaitForSecondsRealtime(coolTime);
        }
    }

    void CreateBalloon()
    {
        Instantiate(BalloonPrefab, new Vector3(Random.Range(-7, 7), -6, 0), Quaternion.identity);
    }

    public void ButtonSoundEffect()
    {
        audioSouce.clip = btnClip;
        audioSouce.Play();
    }
}
