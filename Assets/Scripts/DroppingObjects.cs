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
    private float distFromFerret = 15f;
    
    // [SerializeField]
    // private float droppingSpacing;

    [SerializeField]
    private float timer = 0f;

    private int prevseconds = 0;

    [SerializeField]
    private GameObject finishLine;

    private bool wonGame = false;

    [SerializeField]
    public GameManager gameManager;

    private List<int> dropPositions = new List<int>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //once ferret is at certain height dropping small objects every 1 min/30secs
        //once 1/3 start droping medium objects, once 2/3 drop large objects
        //drop more often as getr higher
        if ((gameManager.players).Count > 0){

            ferret = (GameObject)(gameManager.players[0]._body);
            // float backgroundheight = background.GetComponent<SpriteRenderer>().sprite.rect.height;  
            // float backgroundwidth = background.GetComponent<SpriteRenderer>().sprite.rect.width;
            Vector3 backgroundDimension = Vector3.Scale(background.GetComponent<SpriteRenderer>().sprite.bounds.size, background.transform.lossyScale);
            float backgroundwidth = backgroundDimension.x;
            float backgroundheight = backgroundDimension.y;

            int numSmallObj = smallObjects.Length;
            int numMediumObj = mediumObjects.Length;
            int numBigObj = bigObjects.Length;
            float ferretY = ferret.transform.position.y;
            float ferretX = ferret.transform.position.x;
            if (ferretY >= finishLine.transform.position.y){
                wonGame = true;
            }
            
            if (((int)ferretY != 0) && (!wonGame)){
                timer += Time.deltaTime;
                int seconds = ((int)timer) % 60;

                dropPositions.Add((int)ferretY);
                if ((ferretY < backgroundheight*(1f/3f)) && (seconds % 30 == 0) && (prevseconds != seconds)){
                    //do we drop object where ferret is or random??
                    GameObject dropObj = Instantiate(DropObjectS);
                    float leftS = Mathf.Max((ferretX - distFromFerret), (-backgroundwidth/2 + 5));
                    float rightS = Mathf.Min((ferretX + distFromFerret), (backgroundwidth/2 - 5));
                    dropObj.transform.position = new Vector2((Random.Range(leftS, rightS)), 50);
                    dropObj.GetComponent<SpriteRenderer>().sprite = smallObjects[Random.Range(0, numSmallObj)];
                }
                if ((backgroundheight*(1f/3f) < ferretY) && (ferretY < backgroundheight*(2f/3f))  && (seconds % 20 == 0) && (prevseconds != seconds)){
                    //do we drop object where ferret is or random??
                    GameObject dropObj = Instantiate(DropObjectM);
                    float leftS = Mathf.Max((ferretX - distFromFerret), (-backgroundwidth/2 + 5));
                    float rightS = Mathf.Min((ferretX + distFromFerret), (backgroundwidth/2 - 5));
                    dropObj.transform.position = new Vector2((Random.Range(leftS, rightS)), 50);
                    dropObj.GetComponent<SpriteRenderer>().sprite = mediumObjects[Random.Range(0, numMediumObj)];
                }
                if ((backgroundheight*(2f/3f) < ferretY )&& (seconds % 10 == 0) && (prevseconds != seconds)){
                    //do we drop object where ferret is or random??
                    GameObject dropObj = Instantiate(DropObjectL);
                    float leftS = Mathf.Max((ferretX - distFromFerret), (-backgroundwidth/2 + 5));
                    float rightS = Mathf.Min((ferretX + distFromFerret), (backgroundwidth/2 - 5));
                    dropObj.transform.position = new Vector2((Random.Range(leftS, rightS)), 50);
                    dropObj.GetComponent<SpriteRenderer>().sprite = bigObjects[Random.Range(0, numBigObj)];
                }

                prevseconds = seconds;

            }

        }

        
        
        
    }
}
