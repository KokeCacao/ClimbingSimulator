using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRandomization : MonoBehaviour
{
    [SerializeField]
    private GameObject Rock;

    [SerializeField]
    private GameObject Background;
    
    private float totalheight = 1900f;
    
    private float rowheight = 75f;
    
    private float totalwidth = 420f;
    
    private float colwidth = 70f;

    private float spacing = 25f;
    
    private float x0 = 20f;
    
    private float y0 = 0f;
    
    private int numpaths = 4;
    //background: 1080x4213
    //random rock
    //random rotation

    [SerializeField]
    private Sprite rock1;
    [SerializeField]
    private Sprite rock2;
    [SerializeField]
    private Sprite rock3;
    [SerializeField]
    private Sprite rock4;
    [SerializeField]
    private Sprite rock5;
    [SerializeField]
    private Sprite rock6;
    [SerializeField]
    private Sprite rock7;
    [SerializeField]
    private Sprite rock8;
    [SerializeField]
    private Sprite rock9;
    [SerializeField]
    private Sprite rock10;
    [SerializeField]
    private Sprite rock11;
    [SerializeField]
    private Sprite rock12;
    [SerializeField]
    private Sprite rock13;
    [SerializeField]
    private Sprite rock14;
    [SerializeField]
    private Sprite rock15;
    [SerializeField]
    private Sprite rock16;
    [SerializeField]
    private Sprite rock17;
    [SerializeField]
    private Sprite rock18;

    
    GameObject InstantiateRock(float x1, float x2, float y1, float y2){
        Vector2 newpos = new Vector2((Random.Range(x1+spacing,x2-spacing)),(Random.Range(y1+spacing,y2-spacing)));
        GameObject newRock = Instantiate(Rock);
        newRock.transform.position = newpos;
        int randomRock = Random.Range(1,18);
        switch(randomRock)
        {
            case 1:
                newRock.GetComponent<SpriteRenderer>().sprite = rock1;
                break;
            case 2:
                newRock.GetComponent<SpriteRenderer>().sprite = rock2;
                break;
            case 3:
                newRock.GetComponent<SpriteRenderer>().sprite = rock3;
                break;
            case 4:
                newRock.GetComponent<SpriteRenderer>().sprite = rock4;
                break;
            case 5:
                newRock.GetComponent<SpriteRenderer>().sprite = rock5;
                break;
            case 6:
                newRock.GetComponent<SpriteRenderer>().sprite = rock6;
                break;
            case 7:
                newRock.GetComponent<SpriteRenderer>().sprite = rock7;
                break;
            case 8:
                newRock.GetComponent<SpriteRenderer>().sprite = rock8;
                break;
            case 9:
                newRock.GetComponent<SpriteRenderer>().sprite = rock9;
                break;
            case 10:
                newRock.GetComponent<SpriteRenderer>().sprite = rock10;
                break;
            case 11:
                newRock.GetComponent<SpriteRenderer>().sprite = rock11;
                break;
            case 12:
                newRock.GetComponent<SpriteRenderer>().sprite = rock12;
                break;
            case 13:
                newRock.GetComponent<SpriteRenderer>().sprite = rock13;
                break;
            case 14:
                newRock.GetComponent<SpriteRenderer>().sprite = rock14;
                break;
            case 15:
                newRock.GetComponent<SpriteRenderer>().sprite = rock15;
                break;
            case 16:
                newRock.GetComponent<SpriteRenderer>().sprite = rock16;
                break;
            case 17:
                newRock.GetComponent<SpriteRenderer>().sprite = rock17;
                break;
            case 18:
                newRock.GetComponent<SpriteRenderer>().sprite = rock18;
                break;
            default:
                newRock.GetComponent<SpriteRenderer>().sprite = rock1;
                break;
        }

        return newRock;
    }

    // Start is called before the first frame update
    void Start()
    {
        totalheight = Background.GetComponent<SpriteRenderer>().sprite.bounds.size.y*50 - 40;  // *scale -spacing on sides
        totalwidth = Background.GetComponent<SpriteRenderer>().sprite.bounds.size.x*50 - 40;
        print("totalheight" + totalheight);
        print("totalwidth" + totalwidth);
        int[] path_prev_col = new int[numpaths];

        int numrows  = (int)(totalheight/rowheight) +1;
        int numcols = (int)(totalwidth/colwidth) ;
        print("numrows" + numrows);
        print("numcols" + numcols);

        //evenly spread out in first row
        for (int p = 0; p < numpaths; p++)
        {
            float y1 = y0;
            float y2 = y1+rowheight;

            path_prev_col[p] = (int)(p*((float)numcols/(float)numpaths));///column number 
            print(numcols/numpaths);
            print(path_prev_col[p]);

            float x1 = x0 + (float)path_prev_col[p]*colwidth;
            float x2 = x1 + colwidth;
            GameObject newRock = InstantiateRock(x1,x2,y1,y2);
            // var cubeRenderer = newRock.GetComponent<Renderer>();
            // cubeRenderer.material.SetColor("_Color", Color.red);

        }

        //rest of rows create path from initial point
        for (int i = 1; i < numrows; i++)
        {

            float y1 = y0 + i*rowheight;
            float y2 = y1 + rowheight;

            int[] grids_filled = new int[numcols];  //place in grid, mark 1 if filled
            for (int p = 0; p < numpaths; p++){
                int oldp  =  path_prev_col[p]; // index of prev location
                int newp = 0;
                print("oldp" + oldp);
                if (oldp == 0){ //leftmost
                    if (grids_filled[0] == 1 && grids_filled[1] == 1){
                        newp = 1;  //not instantiate //not use col 0
                        path_prev_col[p] = newp;
                    }
                    else if (grids_filled[0]== 1){
                        newp = 1;
                        path_prev_col[p] = newp;
                        grids_filled[newp] = 1;
                        float x1 = x0 + newp*colwidth;
                        float x2 = x1+ colwidth;
                        GameObject newRock = InstantiateRock(x1,x2,y1,y2);
                    }
                    else if (grids_filled[1]== 1){
                        newp = 1;  //not instantiate //not use col 0
                        path_prev_col[p] = newp;
                    }
                    else {
                        newp = 1;//(int)Random.Range((int)(0),(int)(2)); //int random is exclusive
                        path_prev_col[p] = newp;
                        grids_filled[newp] = 1;
                        float x1 = x0 + newp*colwidth;
                        float x2 = x1+ colwidth;
                        GameObject newRock = InstantiateRock(x1,x2,y1,y2);
                    }
                }
                else if (oldp == numcols-1){ // last column 
                    if (grids_filled[oldp] == 1 && grids_filled[oldp-1] == 1){
                        newp = (int)Random.Range((int)(oldp),(int)(oldp+1));  //not instantiate
                        path_prev_col[p] = newp;
                    }
                    else if (grids_filled[oldp] == 1){
                        newp = oldp-1;
                        path_prev_col[p] = newp;
                        grids_filled[newp] = 1;
                        float x1 = x0 + newp*colwidth;
                        float x2 = x1+ colwidth;
                        GameObject newRock = InstantiateRock(x1,x2,y1,y2);
                    }
                    else if (grids_filled[oldp-1] == 1){
                        newp = oldp-1;
                        path_prev_col[p] = newp;
                        grids_filled[newp] = 1;
                        //float x1 = x0 + newp*colwidth;  //not instantiate
                        //float x2 = x1+ colwidth;
                        //GameObject newRock = InstantiateRock(x1,x2,y1,y2);
                    }
                    else{
                        newp = (int)(oldp-1);//(int)Random.Range((int)(oldp-1),(int)(oldp+1)); //int random is exclusive
                        path_prev_col[p] = newp;
                        grids_filled[newp] = 1;
                        float x1 = x0 + newp*colwidth;
                        float x2 = x1+ colwidth;
                        GameObject newRock = InstantiateRock(x1,x2,y1,y2);
                    }
                }
                else if (grids_filled[oldp-1] == 1 && grids_filled[oldp] == 1 && grids_filled[oldp+1] == 1){
                    newp = (int)Random.Range((int)(oldp),(int)(oldp + 2));
                    path_prev_col[p] = newp; // if all filled do NOT instantiate new rock
                }
                else if (grids_filled[oldp-1] == 1 && grids_filled[oldp] == 1){
                    newp = oldp + 1;
                    path_prev_col[p] = newp;
                    grids_filled[newp] = 1;
                    float x1 = x0 + newp*colwidth;
                    float x2 = x1+ colwidth;
                    GameObject newRock = InstantiateRock(x1,x2,y1,y2);
                }
                else if (grids_filled[oldp] == 1 && grids_filled[oldp + 1] == 1){
                    newp = oldp -1;
                    path_prev_col[p] = newp;
                    grids_filled[newp] = 1;
                    float x1 = x0 + newp*colwidth;
                    float x2 = x1+ colwidth;
                    GameObject newRock = InstantiateRock(x1,x2,y1,y2);
                }
                else if (grids_filled[oldp-1] == 1 && grids_filled[oldp + 1] == 1){
                    newp = oldp;
                    path_prev_col[p] = newp;
                    grids_filled[newp] = 1;
                    float x1 = x0 + newp*colwidth;
                    float x2 = x1+ colwidth;
                    GameObject newRock = InstantiateRock(x1,x2,y1,y2);
                }
                else if (grids_filled[oldp-1] == 1){
                    newp = (int)(Random.Range((int)(oldp),(int)(oldp+2)));
                    path_prev_col[p] = newp;
                    grids_filled[newp] = 1;
                    float x1 = x0 + newp*colwidth;
                    float x2 = x1+ colwidth;
                    GameObject newRock = InstantiateRock(x1,x2,y1,y2);
                }
                else if (grids_filled[oldp] == 1){
                    int randval = (int)(Random.Range((int)(0), (int)(2)));
                    if (randval == 0){
                        newp = oldp-1;
                    }
                    else if (randval == 1){
                        newp = oldp+1;
                    }
                    path_prev_col[p] = newp;
                    grids_filled[newp] = 1;
                    float x1 = x0 + newp*colwidth;
                    float x2 = x1+ colwidth;
                    GameObject newRock = InstantiateRock(x1,x2,y1,y2);
                }
                else if (grids_filled[oldp+1] == 1){
                    newp = (int)(Random.Range((int)(oldp-1),(int)(oldp+1)));
                    path_prev_col[p] = newp;
                    grids_filled[newp] = 1;
                    float x1 = x0 + newp*colwidth;
                    float x2 = x1+ colwidth;
                    GameObject newRock = InstantiateRock(x1,x2,y1,y2);
                }
                else{
                    newp = (Random.Range((int)(oldp-1),(int)(oldp+2)));
                    print("newp" + newp);
                    path_prev_col[p] = newp;
                    grids_filled[newp] = 1;
                    float x1 = x0 + newp*colwidth;
                    float x2 = x1+ colwidth;
                    GameObject newRock = InstantiateRock(x1,x2,y1,y2);
                }
                
                
            }

            for(int n = 0; n < numcols; n++){
                    grids_filled[n] = 0;    
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }  
}

