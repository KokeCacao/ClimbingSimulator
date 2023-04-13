using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
  [SerializeField] private GameObject dropletPrefab;
  [SerializeField] private float waterHeight = 10.0f;
  [SerializeField] private float waterWidth = 20.0f;
  [SerializeField] private int waterAmount = 200;
  [SerializeField] private Vector3 waterPosition = new Vector3(0.0f, 0.0f, 0.0f);
  [SerializeField] private float buoyancyDistance = 0.5f; // distance to completely sink
  [SerializeField] private float buoyancyForce = 200.0f;
  [SerializeField] private float waterDrag = 0.5f;
  [SerializeField] private List<Rigidbody2D> floater;
  [SerializeField] private List<Rigidbody2D> sinker;
  // WARNING: if an object sinked, it cannot be made to be above water level again for now

  [HideInInspector] private int topWaterCount = 10;
  [HideInInspector] public float waterLevel;

  [HideInInspector] private List<GameObject> waterList;
  [HideInInspector] private GameObject waterHolder;

  [HideInInspector] private List<float> floaterOriginalDrag;
  [HideInInspector] private List<float> sinkerOriginalDrag;

  void Awake()
  {
    waterList = new List<GameObject>();
    floaterOriginalDrag = new List<float>();
    sinkerOriginalDrag = new List<float>();
  }
  void Enable()
  {
    foreach (Rigidbody2D rb in floater)
    {
      floaterOriginalDrag.Add(rb.drag);
    }
    foreach (Rigidbody2D rb in sinker)
    {
      sinkerOriginalDrag.Add(rb.drag);
    }
  }
  void Start()
  {
    // start a game object to hold water
    waterHolder = new GameObject("WaterHolder");
    waterHolder.transform.parent = transform;

    
    for (int i = 0; i < waterAmount; i++)
    {
      GameObject water = Instantiate(dropletPrefab);
      water.transform.position = new Vector3(Random.Range(-waterWidth / 2, waterWidth / 2), Random.Range(0, waterHeight), 0) + waterPosition;
      water.transform.parent = waterHolder.transform;
      water.layer = LayerMask.NameToLayer("Water");
      waterList.Add(water);
    }
  }

  void SortDropletBasedOnYPosition()
  {
    waterList.Sort((a, b) => b.transform.position.y.CompareTo(a.transform.position.y));
  }

  void UpdateWaterLevel()
  {
    SortDropletBasedOnYPosition();
    Vector3 waterHolderPosition = new Vector3(0, 0, 0);
    // print("list length "  + waterList.Count + "water count " + topWaterCount);
    if (topWaterCount <= waterAmount){
      for (int i = 0; i < topWaterCount; i++)
      {
        GameObject water = waterList[i];
        waterHolderPosition += water.transform.position;
      }
      waterHolderPosition /= topWaterCount;
    }
    
    waterLevel = waterHolderPosition.y;
  }

  public void AddFloater(Rigidbody2D rb)
  {
    floater.Add(rb);
    floaterOriginalDrag.Add(rb.drag);
    // rb.gameObject.layer = LayerMask.NameToLayer("Floater");
  }

  public void RemoveFloater(Rigidbody2D rb)
  {
    int index = floater.IndexOf(rb);
    floater.RemoveAt(index);
    floaterOriginalDrag.RemoveAt(index);
  }

  public void AddSinker(Rigidbody2D rb)
  {
    sinker.Add(rb);
    sinkerOriginalDrag.Add(rb.drag);
    // rb.gameObject.layer = LayerMask.NameToLayer("Sinker");
  }

  public void RemoveSinker(Rigidbody2D rb)
  {
    int index = sinker.IndexOf(rb);
    sinker.RemoveAt(index);
    sinkerOriginalDrag.RemoveAt(index);
  }

  // Update is called once per frame
  void Update()
  {
    UpdateWaterLevel();

    // Debug.Log("Water Level: " + waterLevel);

    // floater logics
    for (int i = 0; i < floater.Count; i++)
    {
      Rigidbody2D rb = floater[i];
      float distance = rb.transform.position.y - waterLevel;
      // Debug.Log("Floater: " + rb.name + " is " + distance + " units away from water level");
      // Debug.Log("Floater: " + rb.name + " has position: " + rb.transform.position.y);
      if (distance > 0)
      {
        rb.drag = floaterOriginalDrag[i];
      }
      else
      {
        rb.drag = waterDrag;
        rb.AddForce(new Vector2(0, buoyancyForce * Mathf.Clamp(-distance, 0, buoyancyDistance)));
        // Debug.Log("Floater: " + rb.name + " is floating");
      }
    }

    foreach (Rigidbody2D rb in sinker)
    {
      rb.AddForce(new Vector2(0, -1));
    }
  }
}
