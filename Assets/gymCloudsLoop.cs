using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gymCloudsLoop : MonoBehaviour
{
    public float loopSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+=new Vector3(-loopSpeed*Time.deltaTime, 0);

        if(transform.position.x<-20.59)
        {
            transform.position=new Vector3(20.59f, transform.position.y);
        }
    }
}
