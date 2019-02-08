using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Balloon On numPoong Mode
public class Balloon : MonoBehaviour{

    public Splatter splatter;
    public int value;

    //Show balloon's value on screen
    private void Start()
    {
        GetComponentInChildren<TextMesh>().text = value.ToString();
    }

    // Check Balloon Position
    private void Update()
    {
        if( gameObject.transform.position.y > 7 )
        {
            GameManager.instance.EndStage();
            Destroy(gameObject);
        }

        if( GameManager.instance.done == true)
        {
            Destroy(gameObject);
        }
    }

    // When the balloon is touched, Remove it
    // Play effect Sound, effect Splatter With SoundAndGone
    private void OnMouseDown()
    {
        // click is not act when game ends
        if (GameManager.instance.done == false && GameManager.instance.score == (value-1) )
        {
            StartCoroutine(SoundAndGone());
        }
    }

    IEnumerator SoundAndGone()
    {
        //Splatter splatterObj = (Splatter)Instantiate(splatter, this.transform.position, Quaternion.identity);
        Instantiate(splatter, this.transform.position, Quaternion.identity);
        GetComponent<AudioSource>().Play();
        GameManager.instance.score++;
        GameManager.instance.SetupScore();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        Destroy(gameObject);
    }

}
