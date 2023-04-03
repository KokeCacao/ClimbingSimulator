using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCondition : MonoBehaviour
{
    [SerializeField]
    private GameObject lid;

    [SerializeField]
    private float lidClosingTime = 4f;

    [SerializeField]
    private float initLidX = -10f;

    private bool lidClosed = false;
    
    [SerializeField]
    private GameObject BlackMask;

    [SerializeField]
    private GameObject ferret;

    [SerializeField]
    private GameObject finishLine;

    [SerializeField]
    public GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        BlackMask.GetComponent<SpriteRenderer>().color = new Color (0,0,0,0);
    }

    void CloseLid(){
        float lidClosingRate = Time.deltaTime*lidClosingTime;
        float lidX = lid.transform.position.x + lidClosingRate;
        float lidY = lid.transform.position.y;
        lid.transform.position = new Vector2 (lidX, lidY);
        if (lidX >= 0f){
            lidClosed = true;
        }
        float alphaChange = Mathf.Abs(1/(((float)initLidX)/((float)lidClosingRate)));
        Color newColor = BlackMask.GetComponent<SpriteRenderer>().color + new Color (0, 0, 0, alphaChange);
        BlackMask.GetComponent<SpriteRenderer>().color = newColor;
    }

    void SewerEndCondition(){
        if ((gameManager.players).Count > 0){
            ferret = (GameObject)(gameManager.players[0]._body);
            if (ferret.transform.position.y >= finishLine.transform.position.y || Input.GetKey("down")){
                if (!lidClosed){
                    CloseLid();
                }
            } 
        }  
    }

    // Update is called once per frame
    void Update()
    {

        SewerEndCondition();
       
    }


}
    
    
