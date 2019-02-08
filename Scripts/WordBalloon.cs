using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Balloon On wordPoong Mode
public class WordBalloon : MonoBehaviour
{
    public Splatter splatter;
    public char value;
    bool found;
    string word;

    //Show balloon's value on screen
    private void Start()
    {
        GetComponentInChildren<TextMesh>().text = value.ToString();
        found = false;
    }

    // Check Balloon Position
    private void Update()
    {
        if( gameObject.transform.position.y > 7)
            Destroy(gameObject);

        if (GameManager.instance.done == true)
            Destroy(gameObject);

    }

    // When the balloon is touched, Remove it
    // Play effect Sound, effect Splatter With SoundAndGone
    private void OnMouseDown()
    {
        word = GameManager.instance.TargetWord;
        found = false;
        if (GameManager.instance.done == false)
        {
            for( int i = 0; i < GameManager.instance.TargetWord.Length; i++)
            {
                if(GameManager.instance.TargetWord [i]== value)
                {
                    found = true;
                    word = word.Replace(value, ' ');
                    word = word.Replace(" ", string.Empty);
                    GameManager.instance.TargetWord = word;
                    GameManager.instance.curBalloonValue = 0;
                    StartCoroutine(SoundAndGone());
                }
            }
            if( found == false)
                GameManager.instance.EndStage();
        }
    }
    
    IEnumerator SoundAndGone()
    {
        Instantiate(splatter, this.transform.position, Quaternion.identity);
        GetComponent<AudioSource>().Play();
        GameManager.instance.score++;
        GameManager.instance.SetupScore();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        Destroy(gameObject);
    }

}
