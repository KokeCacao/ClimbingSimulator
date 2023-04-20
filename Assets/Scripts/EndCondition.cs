using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCondition : MonoBehaviour
{
    [SerializeField]
    private GameObject Trophy;

    [SerializeField]
    private float trophyHeight = -10f;

    private bool trophyTouched = false;

    [SerializeField]
    private GameObject ferret;

    [SerializeField]
    private GameObject finishLine;

    [SerializeField]
    public GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool touchedTrophy(){ // change code to if touched trophy
        if ((gameManager.players).Count > 0){
            ferret = (GameObject)(gameManager.players[0]._body);
            if (ferret.transform.position.y >= finishLine.transform.position.y){
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {

        if ((gameManager.players).Count > 0){
            ferret = (GameObject)(gameManager.players[0]._body);
            //if grabbed trophy
            if (touchedTrophy()){
                
            } 
        }  
       
    }


}
    
    
