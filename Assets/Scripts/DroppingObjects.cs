using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingObjects : MonoBehaviour
{
    [SerializeField]
    private GameObject background;

    [SerializeField]
    private GameObject ferret;

    [SerializeField]
    private GameObject DropObjectS;

    [SerializeField]
    private GameObject DropObjectM;

    [SerializeField]
    private GameObject DropObjectL;

    [SerializeField] 
    private Sprite[] smallObjects;

    [SerializeField] 
    private Sprite[] mediumObjects;

    [SerializeField] 
    private Sprite[] bigObjects;

    [SerializeField]
    private float droppingSpacing;

    [SerializeField]
    private GameObject finishLine;

    private bool wonGame = false;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // float backgroundheight = background.GetComponent<SpriteRenderer>().sprite.rect.height;  
        // float backgroundwidth = background.GetComponent<SpriteRenderer>().sprite.rect.width;
        Vector3 backgroundDimension = Vector3.Scale(background.GetComponent<SpriteRenderer>().sprite.bounds.size, background.transform.lossyScale);
        float backgroundwidth = backgroundDimension.x;
        float backgroundheight = backgroundDimension.y;

        int numSmallObj = smallObjects.Length;
        int numMediumObj = mediumObjects.Length;
        int numBigObj = bigObjects.Length;
        float ferretY = ferret.transform.position.y;
        if (ferretY >= finishLine.transform.position.y){
            wonGame = true;
        }

        if (((int)ferretY != 0) && ((int)ferretY % (int)droppingSpacing == 0) && (!wonGame)){
            if (ferretY < backgroundheight*(1f/3f)){
                //do we drop object where ferret is or random??
                GameObject dropObj = Instantiate(DropObjectS);
                dropObj.transform.position = new Vector2((Random.Range(-backgroundwidth/2, backgroundwidth/2)), 50);
                dropObj.GetComponent<SpriteRenderer>().sprite = smallObjects[Random.Range(0, numSmallObj)];
            }
            if ((backgroundheight*(1f/3f) < ferretY) && (ferretY < backgroundheight*(2f/3f))){
                //do we drop object where ferret is or random??
                GameObject dropObj = Instantiate(DropObjectM);
                dropObj.transform.position = new Vector2((Random.Range(-backgroundwidth/2, backgroundwidth/2)), 50);
                dropObj.GetComponent<SpriteRenderer>().sprite = mediumObjects[Random.Range(0, numMediumObj)];
            }
            if (backgroundheight*(2f/3f) < ferretY){
                //do we drop object where ferret is or random??
                GameObject dropObj = Instantiate(DropObjectL);
                dropObj.transform.position = new Vector2((Random.Range(-backgroundwidth/2, backgroundwidth/2)), 50);
                dropObj.GetComponent<SpriteRenderer>().sprite = bigObjects[Random.Range(0, numBigObj)];
            }

        }

        
        
        
    }
}
