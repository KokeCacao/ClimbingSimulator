using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanGrabTrophy : MonoBehaviour
{
    public bool leftGrabTrophy = false;
    public bool rightGrabTrophy = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "leftHand")
        {
            leftGrabTrophy = true;
            Debug.Log("Left hand grabbed trophy");
        }
        if (collision.gameObject.tag == "rightHand")
        {
            rightGrabTrophy = true;
            Debug.Log("Right hand grabbed trophy");
        }
    }
}
