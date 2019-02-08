using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBalloon : MonoBehaviour {

    // Just check balloons y value
    void Update ()
    {
        if (gameObject.transform.position.y > 7)
        {
            Destroy(gameObject);
        }
    }
}
