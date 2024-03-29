using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndCondition : MonoBehaviour
{
    [SerializeField]
    private GameObject trophy;

    // [SerializeField]
    // private float trophyHeight = -10f;

    private bool trophyTouched = false;

    [SerializeField]
    private GameObject ferret;

    [SerializeField]
    private GameObject finishLine;

    [SerializeField]
    public GameManager gameManager;

    [SerializeField] public CanGrabTrophy canGrabTrophy;

    [SerializeField] 
    private float ascendrate = 0.3f;

    [SerializeField]
    private bool reachedTop = false;

    [SerializeField]
    private float heightDist = 10f;

    [SerializeField]
    private GameObject[] objectsList;

    [SerializeField]
    public bool touchedTrophy = false;

    [SerializeField]
    private GameObject trophyHalo;

    private GameObject trophyhalo = null;

    public GameObject newObj = null;

    private float halotimer = 0f;

    [SerializeField]
    private int haloSpinTime = 4; //seconds

    [SerializeField]
    public bool transformed = false;

    [SerializeField]
    private float timer = 0f; //overall game timer

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private TextMeshProUGUI endConditionText1;
    [SerializeField]
    private TextMeshProUGUI endConditionText2;
    [SerializeField]
    private TextMeshProUGUI endConditionText3;
    [SerializeField]
    private TextMeshProUGUI endConditionText4;

    private bool endConditionObject = false;
    public float gameOverLine = -15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public bool grabTrophy(){ // change code to if touched trophy
        if ((gameManager.players).Count > 0){
            PlayerManager player = gameManager.players[0];

            if (canGrabTrophy.leftGrabTrophy){
                player.OnLeftGrabEvent();
                return true;
            }
            if (canGrabTrophy.rightGrabTrophy ){
                player.OnRightGrabEvent();
                return true;
            }
        }
        return false;
    }

    public GameObject touchingTrophy(bool RightHand){
        if ((gameManager.players).Count > 0){
            PlayerManager player = gameManager.players[0];

            if (canGrabTrophy.leftGrabTrophy && !RightHand){ 
                player.ReleaseRightGrab();
                touchedTrophy = true;
                return trophy;
            }
            if (canGrabTrophy.rightGrabTrophy && RightHand){
                player.ReleaseLeftGrab();
                touchedTrophy = true;
                return trophy;
            }
        }
        return null;
    }
    

    // Update is called once per frame
    void Update()
    {
        if ((gameManager.players).Count > 0){
            ferret = (GameObject)(gameManager.players[0]._body);

            if (!touchedTrophy && (ferret.transform.position.y > -10)){
                timer += Time.deltaTime;
            }
            int minutes = (int)(timer / 60);
            int seconds = ((int)timer) % 60;
            int mins1 = (int)(minutes / 10);
            int mins2 = minutes %10;
            int secs1 = (int)(seconds / 10);
            int secs2 = seconds % 10;
            timerText.text = mins1.ToString() + mins2.ToString() + ":" + secs1.ToString() + secs2.ToString();//(seconds).ToString();
            
            touchingTrophy(true);
            touchingTrophy(false);
            if (touchedTrophy && !reachedTop){
                grabTrophy();
                trophy.transform.position = new Vector2 (trophy.transform.position.x, trophy.transform.position.y + ascendrate*Time.deltaTime);
                //gameManager.players[0]. move players position up
                if (trophy.transform.position.y >= (finishLine.transform.position.y + heightDist)){
                    reachedTop = true;
                    trophyhalo = Instantiate(trophyHalo);
                    trophyhalo.transform.position = trophy.transform.position;
                }
            } 
            

            if (reachedTop && !transformed){
                //spin halo
                halotimer += Time.deltaTime;
                int haloseconds = ((int)halotimer) % 60;
                trophyhalo.transform.Rotate (0,0,50*Time.deltaTime);
                trophyhalo.transform.localScale += new Vector3(Mathf.Sin(halotimer/(60*60*10)), Mathf.Sin(halotimer/(60*60*10)), 0);

                if (seconds >= haloSpinTime){
                    
                    int numObj = objectsList.Length;
                    int objectIndex = Random.Range(0, numObj);
                    GameObject obj = objectsList[objectIndex];
                    newObj = Instantiate(obj);
                    newObj.transform.position = ferret.transform.position;
                    newObj.GetComponent<Rigidbody2D>().AddTorque(1);
                    ferret.SetActive(false);
                    transformed = true;
                }
            }

            if (transformed) { 
                //gameManager.players[0]._camera.transform.position = newObj.transform.position;
                //choose new object to transform into
                
            }

            // text
            if (!transformed && (ferret.transform.position.y < gameOverLine)){
                //endConditionText.transform.position = new Vector2(0, gameManager.players[0].gameOverLine);
                endConditionText1.text = "YOU FAILED";
                endConditionText2.text = "Time taken";
                endConditionText3.text = timerText.text;
                endConditionText4.text = "Press \"esc\" to restart";
            } else {
                if ((newObj != null) && (newObj.transform.position.y < gameOverLine - 5) && !endConditionObject){
                    GameObject transformedobj = Instantiate(newObj);
                    transformedobj.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    transformedobj.transform.position = new Vector2(0,gameOverLine + 5);
                    endConditionText1.text = "YOU REINCARNATED";
                    //endConditionText1.transform.position = new Vector2(endConditionText1.transform.position.x, endConditionText1.transform.position.y + 10);
                    endConditionText2.text = "Time taken";
                    endConditionText3.text = timerText.text;
                    endConditionText4.text = "Press \"esc\" to restart";
                    endConditionObject = true;
                }
            }
        }  
       
    }


}
    
    
