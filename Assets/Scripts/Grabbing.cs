using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour
{
    [SerializeField]
    float rockRadius = 0.5f;

    // Start is called before the first frame update
    bool GrabbingF(float handx, float handy){
        
        GameObject[ ] rocksInGame = GameObject.FindGameObjectsWithTag("rock");
        for (int i = 0; i < 18; i++){
            GameObject currRock = rocksInGame[i];
            float rockx = currRock.transform.position.x;
            float rocky = currRock.transform.position.y;
            Vector2 rockPos = new Vector2(rockx, rocky);
            Vector2 handPos = new Vector2(handx, handy);
            float dist = Vector2.Distance(rockPos,handPos);
            if (dist < rockRadius){
                return true;
            }
        }
        return false;
     
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
