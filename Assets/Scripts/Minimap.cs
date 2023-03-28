using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{

    [SerializeField]
    private GameObject Head;

    // [SerializeField]
    // private GameObject Head2;
    [SerializeField]
    private float head_xpos = 25.29557f;

    [SerializeField]
    private GameObject Background;

    [SerializeField]
    private GameObject MinimapO;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void MinimapUpdate(PlayerManager Player1)
    {
    
        //float backgroundheight = Background.GetComponent<SpriteRenderer>().sprite.rect.height;  
        //float backgroundwidth = Background.GetComponent<SpriteRenderer>().sprite.rect.width;
        //float minimapheight = MinimapO.GetComponent<SpriteRenderer>().sprite.rect.height;  
        //float minimapwidth = Minimap.GetComponent<SpriteRenderer>().sprite.rect.width;
        float head1y = Player1._body.transform.position.y;//*(minimapheight/backgroundheight);
        //float head2y = Player2.transform.position.y*(minimapheight/backgroundheight);
        Vector2 Head1pos = new Vector2 (head_xpos, head1y);
        //Vector2 Head2pos = new Vector2 (Head2.transform.position.x, head2y);
        Head.transform.position = Head1pos;
        //Head2.transform.position = Head2pos;
    }
}
