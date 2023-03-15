using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{

    [SerializeField]
    private GameObject Head1;

    // [SerializeField]
    // private GameObject Head2;

    [SerializeField]
    private GameObject Background;

    [SerializeField]
    private GameObject MinimapO;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void MinimapUpdate(GameObject Player1, GameObject Head1)
    {
        float backgroundheight = Background.GetComponent<SpriteRenderer>().sprite.rect.height;  
        //float backgroundwidth = Background.GetComponent<SpriteRenderer>().sprite.rect.width;
        float minimapheight = MinimapO.GetComponent<SpriteRenderer>().sprite.rect.height;  
        //float minimapwidth = Minimap.GetComponent<SpriteRenderer>().sprite.rect.width;
        float head1y = Player1.transform.position.y*(minimapheight/backgroundheight);
        //float head2y = Player2.transform.position.y*(minimapheight/backgroundheight);
        Vector2 Head1pos = new Vector2 (Head1.transform.position.x, head1y);
        //Vector2 Head2pos = new Vector2 (Head2.transform.position.x, head2y);
        Head1.transform.position = Head1pos;
        //Head2.transform.position = Head2pos;
    }
}
