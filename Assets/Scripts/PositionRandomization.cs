using UnityEngine;

public class PositionRandomization : MonoBehaviour
{
  [SerializeField]
  private GameObject Rock;

  [SerializeField]
  private GameObject background;
  public const string ROCK_TAG = "rock";

  public float ROCK_RADIUS = 0.8f; // this is good

  private float ROW_HEIGHT = 2f;

  private float COL_WIDTH = 2f;

  private float SPACING = 0.1f;

  private float BORDER_SIZE = 0.1f;

  private float x0 = 0f;

  private float y0 = -2f;

  private int N_PATHS = 3;

  private Vector2 backgroundDimension;

  [SerializeField] private Sprite[] rocks;


  GameObject InstantiateRock(float x1, float x2, float y1, float y2)
  {
    GameObject newRock = Instantiate(Rock);
    newRock.transform.parent = background.transform;
    print(backgroundDimension);
    newRock.transform.position = new Vector2((Random.Range(x1 + SPACING, x2 - SPACING)), (Random.Range(y1 + SPACING, y2 - SPACING)));
    newRock.transform.position -= new Vector3(backgroundDimension.x / 2f, 0f, 0f);
    newRock.GetComponent<SpriteRenderer>().sprite = rocks[Random.Range(0, rocks.Length)];
    return newRock;
  }

  public GameObject canGrab(Vector2 pos) {
    GameObject[] rocksInGame = GameObject.FindGameObjectsWithTag(ROCK_TAG);
    foreach (GameObject rock in rocksInGame) {
      float rocky = rock.transform.position.y;
      Vector2 rockPos = (Vector2) rock.transform.position;
      float dist = Vector2.Distance(rockPos,pos);
      if (dist < ROCK_RADIUS){
        return rock;
      }
    }
    return null;
  }

  public GameObject[] getRocks() {
    return GameObject.FindGameObjectsWithTag(ROCK_TAG);
  }

  private void OnEnable() {
    Debug.Assert(background != null, "Background is null");
    Debug.Assert(Rock != null, "Rock is null");

    print(background.GetComponent<SpriteRenderer>().sprite.bounds.size);
    print(background.transform.localScale);
    backgroundDimension = Vector3.Scale(background.GetComponent<SpriteRenderer>().sprite.bounds.size, background.transform.localScale);
  }

  // Start is called before the first frame update
  void Start()
  {

    Vector2 widthHeight = backgroundDimension / new Vector2(COL_WIDTH, ROW_HEIGHT) - new Vector2(BORDER_SIZE, BORDER_SIZE);
    int numrows = (int)widthHeight.y + 1;
    int numcols = (int)widthHeight.x;

    int[] path_prev_col = new int[N_PATHS];

    //evenly spread out in first row
    for (int p = 0; p < N_PATHS; p++)
    {
      float y1 = y0;
      float y2 = y1 + ROW_HEIGHT;

      path_prev_col[p] = (int)(p * ((float)numcols / (float)N_PATHS));///column number 
      print(numcols / N_PATHS);
      print(path_prev_col[p]);

      float x1 = x0 + (float)path_prev_col[p] * COL_WIDTH;
      float x2 = x1 + COL_WIDTH;
      GameObject newRock = InstantiateRock(x1, x2, y1, y2);
      // var cubeRenderer = newRock.GetComponent<Renderer>();
      // cubeRenderer.material.SetColor("_Color", Color.red);

    }

    //rest of rows create path from initial point
    for (int i = 1; i < numrows; i++)
    {

      float y1 = y0 + i * ROW_HEIGHT;
      float y2 = y1 + ROW_HEIGHT;

      int[] grids_filled = new int[numcols];  //place in grid, mark 1 if filled
      for (int p = 0; p < N_PATHS; p++)
      {
        int oldp = path_prev_col[p]; // index of prev location
        int newp = 0;
        print("oldp" + oldp);
        if (oldp == 0)
        { //leftmost
          if (grids_filled[0] == 1 && grids_filled[1] == 1)
          {
            newp = 1;  //not instantiate //not use col 0
            path_prev_col[p] = newp;
          }
          else if (grids_filled[0] == 1)
          {
            newp = 1;
            path_prev_col[p] = newp;
            grids_filled[newp] = 1;
            float x1 = x0 + newp * COL_WIDTH;
            float x2 = x1 + COL_WIDTH;
            GameObject newRock = InstantiateRock(x1, x2, y1, y2);
          }
          else if (grids_filled[1] == 1)
          {
            newp = 1;  //not instantiate //not use col 0
            path_prev_col[p] = newp;
          }
          else
          {
            newp = 1;//(int)Random.Range((int)(0),(int)(2)); //int random is exclusive
            path_prev_col[p] = newp;
            grids_filled[newp] = 1;
            float x1 = x0 + newp * COL_WIDTH;
            float x2 = x1 + COL_WIDTH;
            GameObject newRock = InstantiateRock(x1, x2, y1, y2);
          }
        }
        else if (oldp == numcols - 1)
        { // last column 
          if (grids_filled[oldp] == 1 && grids_filled[oldp - 1] == 1)
          {
            newp = (int)Random.Range((int)(oldp), (int)(oldp + 1));  //not instantiate
            path_prev_col[p] = newp;
          }
          else if (grids_filled[oldp] == 1)
          {
            newp = oldp - 1;
            path_prev_col[p] = newp;
            grids_filled[newp] = 1;
            float x1 = x0 + newp * COL_WIDTH;
            float x2 = x1 + COL_WIDTH;
            GameObject newRock = InstantiateRock(x1, x2, y1, y2);
          }
          else if (grids_filled[oldp - 1] == 1)
          {
            newp = oldp - 1;
            path_prev_col[p] = newp;
            grids_filled[newp] = 1;
            //float x1 = x0 + newp*colwidth;  //not instantiate
            //float x2 = x1+ colwidth;
            //GameObject newRock = InstantiateRock(x1,x2,y1,y2);
          }
          else
          {
            newp = (int)(oldp - 1);//(int)Random.Range((int)(oldp-1),(int)(oldp+1)); //int random is exclusive
            path_prev_col[p] = newp;
            grids_filled[newp] = 1;
            float x1 = x0 + newp * COL_WIDTH;
            float x2 = x1 + COL_WIDTH;
            GameObject newRock = InstantiateRock(x1, x2, y1, y2);
          }
        }
        else if (grids_filled[oldp - 1] == 1 && grids_filled[oldp] == 1 && grids_filled[oldp + 1] == 1)
        {
          newp = (int)Random.Range((int)(oldp), (int)(oldp + 2));
          path_prev_col[p] = newp; // if all filled do NOT instantiate new rock
        }
        else if (grids_filled[oldp - 1] == 1 && grids_filled[oldp] == 1)
        {
          newp = oldp + 1;
          path_prev_col[p] = newp;
          grids_filled[newp] = 1;
          float x1 = x0 + newp * COL_WIDTH;
          float x2 = x1 + COL_WIDTH;
          GameObject newRock = InstantiateRock(x1, x2, y1, y2);
        }
        else if (grids_filled[oldp] == 1 && grids_filled[oldp + 1] == 1)
        {
          newp = oldp - 1;
          path_prev_col[p] = newp;
          grids_filled[newp] = 1;
          float x1 = x0 + newp * COL_WIDTH;
          float x2 = x1 + COL_WIDTH;
          GameObject newRock = InstantiateRock(x1, x2, y1, y2);
        }
        else if (grids_filled[oldp - 1] == 1 && grids_filled[oldp + 1] == 1)
        {
          newp = oldp;
          path_prev_col[p] = newp;
          grids_filled[newp] = 1;
          float x1 = x0 + newp * COL_WIDTH;
          float x2 = x1 + COL_WIDTH;
          GameObject newRock = InstantiateRock(x1, x2, y1, y2);
        }
        else if (grids_filled[oldp - 1] == 1)
        {
          newp = (int)(Random.Range((int)(oldp), (int)(oldp + 2)));
          path_prev_col[p] = newp;
          grids_filled[newp] = 1;
          float x1 = x0 + newp * COL_WIDTH;
          float x2 = x1 + COL_WIDTH;
          GameObject newRock = InstantiateRock(x1, x2, y1, y2);
        }
        else if (grids_filled[oldp] == 1)
        {
          int randval = (int)(Random.Range((int)(0), (int)(2)));
          if (randval == 0)
          {
            newp = oldp - 1;
          }
          else if (randval == 1)
          {
            newp = oldp + 1;
          }
          path_prev_col[p] = newp;
          grids_filled[newp] = 1;
          float x1 = x0 + newp * COL_WIDTH;
          float x2 = x1 + COL_WIDTH;
          GameObject newRock = InstantiateRock(x1, x2, y1, y2);
        }
        else if (grids_filled[oldp + 1] == 1)
        {
          newp = (int)(Random.Range((int)(oldp - 1), (int)(oldp + 1)));
          path_prev_col[p] = newp;
          grids_filled[newp] = 1;
          float x1 = x0 + newp * COL_WIDTH;
          float x2 = x1 + COL_WIDTH;
          GameObject newRock = InstantiateRock(x1, x2, y1, y2);
        }
        else
        {
          newp = (Random.Range((int)(oldp - 1), (int)(oldp + 2)));
          print("newp" + newp);
          path_prev_col[p] = newp;
          grids_filled[newp] = 1;
          float x1 = x0 + newp * COL_WIDTH;
          float x2 = x1 + COL_WIDTH;
          GameObject newRock = InstantiateRock(x1, x2, y1, y2);
        }
      }

      for (int n = 0; n < numcols; n++)
      {
        grids_filled[n] = 0;
      }
    }
  }
}

