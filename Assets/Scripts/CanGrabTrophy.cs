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

    // private void OnCollisionEnter2D(Collision2D collision){
    //     Debug.Log("OnCollisionEnter2D");
    //     if (collision.gameObject.tag == "leftHand")
    //     {
    //         leftGrabTrophy = true;
    //         Debug.Log("Left hand grabbed trophy");
    //     }
    //     if (collision.gameObject.tag == "rightHand")
    //     {
    //         rightGrabTrophy = true;
    //         Debug.Log("Right hand grabbed trophy");
    //     }
    // }
    //public GameObject canGrabTrophyFunc()

    // private void OnTriggerEnter2D(Collider2D collision){
    //     Debug.Log("OnCollisionEnter2D");
    //     if (collision.gameObject.tag == "leftHand")
    //     {
    //         leftGrabTrophy = true;
    //         Debug.Log("Left hand grabbed trophy");
    //     }
    //     if (collision.gameObject.tag == "rightHand")
    //     {
    //         rightGrabTrophy = true;
    //         Debug.Log("Right hand grabbed trophy");
    //     }
    // }

    private void OnTriggerStay2D(Collider2D collision){
        Debug.Log("OnCollisionStay2D");
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

    private void OnTriggerExit2D(Collider2D collision){
        Debug.Log("OnCollisionExit2D");
        if (collision.gameObject.tag == "leftHand")
        {
            leftGrabTrophy = false;
            Debug.Log("Left hand grabbed trophy");
        }
        if (collision.gameObject.tag == "rightHand")
        {
            rightGrabTrophy = false;
            Debug.Log("Right hand grabbed trophy");
        }
    }
}
