using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRandomization : MonoBehaviour
{
    [SerializeField]
    private GameObject Rock;
    
    // Start is called before the first frame update
    void Start()
    {
        float x0 = -35f;
        float xfinal = 35f;
        float x1 = x0;
        float x2 = x0;
        float colsize = 15f;
        while(x2 < xfinal){
            x2 = x1 + colsize;

            float y0 = -35f;
            float yfinal = 35f;
            float y1 = x0;
            float y2 = x0;
            float rowsize = 15f;

            while(y2 < yfinal){
                y2 = y1 + rowsize;
                Vector2 newpos = new Vector2((Random.Range(x0,x1)),(Random.Range(y0,y1)));
                GameObject newRock = Instantiate(Rock, newpos, Quaternion.identity);
            }
            
            x1 = x2;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
