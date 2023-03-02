using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRandomization : MonoBehaviour
{
    [SerializeField]
    private GameObject Rock;
    
    private float totalheight = 421.3f;
    
    private float rowheight = 20f;
    
    private float totalwidth = 108.0f;
    
    private float colwidth = 13f;
    
    private float x0 = -50f;
    
    private float y0 = -80f;
    
    private int numpaths = 3;
    //background: 1080x4213
    //random rock
    //random rotation
    //random color

    
    // Start is called before the first frame update
    void Start()
    {
        int[] path_prev_col = new int[numpaths];

        int numrows  = (int)(totalheight/rowheight) + 1;
        int numcols = (int)(totalwidth/colwidth) + 1;
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
            Vector2 newpos = new Vector2((Random.Range(x1,x2)),(Random.Range(y1,y2)));
            GameObject newRock = Instantiate(Rock);
            newRock.transform.position = newpos;
            var cubeRenderer = newRock.GetComponent<Renderer>();
            cubeRenderer.material.SetColor("_Color", Color.red);

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
                if (oldp == 0){
                    if (grids_filled[0] == 1 && grids_filled[1] == 1){
                        newp = (int)Random.Range((int)(1),(int)(2));  //not instantiate
                        path_prev_col[p] = newp;
                    }
                    else if (grids_filled[0]== 1){
                        newp = 1;
                        path_prev_col[p] = newp;
                        grids_filled[newp] = 1;
                        float x1 = x0 + newp*colwidth;
                        float x2 = x1+ colwidth;
                        Vector2 newpos = new Vector2((Random.Range(x1,x2)),(Random.Range(y1,y2)));
                        GameObject newRock = Instantiate(Rock);
                        newRock.transform.position = newpos;
                    }
                    else if (grids_filled[1]== 1){
                        newp = 0;
                        path_prev_col[p] = newp;
                        grids_filled[newp] = 1;
                        float x1 = x0 + newp*colwidth;
                        float x2 = x1+ colwidth;
                        Vector2 newpos = new Vector2((Random.Range(x1,x2)),(Random.Range(y1,y2)));
                        GameObject newRock = Instantiate(Rock);
                        newRock.transform.position = newpos;
                    }
                    else {
                        newp = (int)Random.Range((int)(0),(int)(2)); //int random is exclusive
                        path_prev_col[p] = newp;
                        grids_filled[newp] = 1;
                        float x1 = x0 + newp*colwidth;
                        float x2 = x1+ colwidth;
                        Vector2 newpos = new Vector2((Random.Range(x1,x2)),(Random.Range(y1,y2)));
                        GameObject newRock = Instantiate(Rock);
                        newRock.transform.position = newpos;
                    }
                }
                else if (oldp == numcols-1){ // too large
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
                        Vector2 newpos = new Vector2((Random.Range(x1,x2)),(Random.Range(y1,y2)));
                        GameObject newRock = Instantiate(Rock);
                        newRock.transform.position = newpos;
                    }
                    else if (grids_filled[oldp-1] == 1){
                        newp = oldp;
                        path_prev_col[p] = newp;
                        grids_filled[newp] = 1;
                        float x1 = x0 + newp*colwidth;
                        float x2 = x1+ colwidth;
                        Vector2 newpos = new Vector2((Random.Range(x1,x2)),(Random.Range(y1,y2)));
                        GameObject newRock = Instantiate(Rock);
                        newRock.transform.position = newpos;
                    }
                    else{
                        newp = (int)Random.Range((int)(oldp-1),(int)(oldp+1)); //int random is exclusive
                        path_prev_col[p] = newp;
                        grids_filled[newp] = 1;
                        float x1 = x0 + newp*colwidth;
                        float x2 = x1+ colwidth;
                        Vector2 newpos = new Vector2((Random.Range(x1,x2)),(Random.Range(y1,y2)));
                        GameObject newRock = Instantiate(Rock);
                        newRock.transform.position = newpos;
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
                    Vector2 newpos = new Vector2((Random.Range(x1,x2)),(Random.Range(y1,y2)));
                    GameObject newRock = Instantiate(Rock);
                    newRock.transform.position = newpos;
                }
                else if (grids_filled[oldp] == 1 && grids_filled[oldp + 1] == 1){
                    newp = oldp -1;
                    path_prev_col[p] = newp;
                    grids_filled[newp] = 1;
                    float x1 = x0 + newp*colwidth;
                    float x2 = x1+ colwidth;
                    Vector2 newpos = new Vector2((Random.Range(x1,x2)),(Random.Range(y1,y2)));
                    GameObject newRock = Instantiate(Rock);
                    newRock.transform.position = newpos;
                }
                else if (grids_filled[oldp-1] == 1 && grids_filled[oldp + 1] == 1){
                    newp = oldp;
                    path_prev_col[p] = newp;
                    grids_filled[newp] = 1;
                    float x1 = x0 + newp*colwidth;
                    float x2 = x1+ colwidth;
                    Vector2 newpos = new Vector2((Random.Range(x1,x2)),(Random.Range(y1,y2)));
                    GameObject newRock = Instantiate(Rock);
                    newRock.transform.position = newpos;
                }
                else if (grids_filled[oldp-1] == 1){
                    newp = (int)(Random.Range((int)(oldp),(int)(oldp+2)));
                    path_prev_col[p] = newp;
                    grids_filled[newp] = 1;
                    float x1 = x0 + newp*colwidth;
                    float x2 = x1+ colwidth;
                    Vector2 newpos = new Vector2((Random.Range(x1,x2)),(Random.Range(y1,y2)));
                    GameObject newRock = Instantiate(Rock);
                    newRock.transform.position = newpos;
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
                    Vector2 newpos = new Vector2((Random.Range(x1,x2)),(Random.Range(y1,y2)));
                    GameObject newRock = Instantiate(Rock);
                    newRock.transform.position = newpos;
                }
                else if (grids_filled[oldp+1] == 1){
                    newp = (int)(Random.Range((int)(oldp-1),(int)(oldp+1)));
                    path_prev_col[p] = newp;
                    grids_filled[newp] = 1;
                    float x1 = x0 + newp*colwidth;
                    float x2 = x1+ colwidth;
                    Vector2 newpos = new Vector2((Random.Range(x1,x2)),(Random.Range(y1,y2)));
                    GameObject newRock = Instantiate(Rock);
                    newRock.transform.position = newpos;
                }
                else{
                    newp = (Random.Range((int)(oldp-1),(int)(oldp+2)));
                    print("newp" + newp);
                    path_prev_col[p] = newp;
                    grids_filled[newp] = 1;
                    float x1 = x0 + newp*colwidth;
                    float x2 = x1+ colwidth;
                    Vector2 newpos = new Vector2((Random.Range(x1,x2)),(Random.Range(y1,y2)));
                    GameObject newRock = Instantiate(Rock);
                    newRock.transform.position = newpos;
                }
                
                for(int n = 0; n < numcols; n++){
                    grids_filled[n] = 0;    
                }
            }
        }
    }
    
    //old random position function 
    // void Start()
    // {

    //     firstRock.transform.position = new Vector3(0,0,0);
    //     float x0 = -35f;
    //     float xfinal = 35f;
    //     float x1 = x0;
    //     float x2 = x0;
    //     float colsize = 15f;
        
    //     while(x2 < xfinal){
    //         x2 = x1 + colsize;

    //         float y0 = -20f;
    //         float yfinal = 20f;
    //         float y1 = x0;
    //         float y2 = x0;
    //         float rowsize = 10f;

    //         while(y2 < yfinal){
    //             y2 = y1 + rowsize;
    //             Vector2 newpos = new Vector2((Random.Range(x1,x2)),(Random.Range(y1,y2)));
    //             GameObject newRock = Instantiate(Rock);
    //             newRock.transform.position = newpos;
    //             y1 = y2;
    //         }
            
    //         x1 = x2;
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
    
    }  
}
