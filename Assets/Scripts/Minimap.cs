using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


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

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject canvasMinimap;

    private float lowerOffset = 100f;



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
        Vector3 backgroundDimension = Vector3.Scale(Background.GetComponent<SpriteRenderer>().sprite.bounds.size, Background.transform.lossyScale);
        float backgroundwidth = backgroundDimension.x;
        float backgroundheight = backgroundDimension.y;

        float canvasMinimapH = canvasMinimap.transform.lossyScale.y;

        float head1y = Player1._body.transform.position.y;//*(minimapheight/backgroundheight);
        //float head2y = Player2.transform.position.y*(minimapheight/backgroundheight);
        Vector2 Head1pos = new Vector2 (head_xpos, head1y);
        //Vector2 Head2pos = new Vector2 (Head2.transform.position.x, head2y);
        Head.transform.position = Head1pos;

        scoreText.text = ((int)((head1y/backgroundheight)*100)).ToString() + "%";
        float textposy = (head1y/backgroundheight)*canvasMinimapH + lowerOffset;
        Vector2 textpos = new Vector2 (scoreText.transform.position.x, textposy);
        scoreText.transform.position = textpos;
        //Head2.transform.position = Head2pos;
    }
}
