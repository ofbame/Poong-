using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadData : MonoBehaviour {

    public Text numHighText;
    public Text wordHighText;

    int numHigh;
    int wordHigh;

	void Start ()
    {
        LoadHighscoreData();

        numHighText.text = numHigh + " Poongs!";
        wordHighText.text = wordHigh + " Poongs!";
    }

    void LoadHighscoreData()
    {
        numHigh = PlayerPrefs.GetInt("Num", 0);
        wordHigh = PlayerPrefs.GetInt("Word", 0);
    }
}
