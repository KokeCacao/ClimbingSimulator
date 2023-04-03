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

  [HideInInspector] private int topWaterCount = 10;
  [HideInInspector] public float waterLevel;

  private List<GameObject> waterList = new List<GameObject>();
  private GameObject waterHolder;


  // Start is called before the first frame update
  void Start()
  {
    // start a game object to hold water
    waterHolder = new GameObject("WaterHolder");
    waterHolder.transform.parent = transform;


    for (int i = 0; i < waterAmount; i++)
    {
      GameObject water = Instantiate(dropletPrefab);
      water.transform.position = new Vector3(Random.Range(-waterWidth/2, waterWidth/2), Random.Range(0, waterHeight), 0) + waterPosition;
      water.transform.parent = waterHolder.transform;
      water.layer = LayerMask.NameToLayer("Water");
      waterList.Add(water);
    }

  }

  void UpdateWaterLevel() {
    Vector3 waterHolderPosition = new Vector3(0, 0, 0);
    for (int i = 0; i < topWaterCount; i++)
    {
      GameObject water = waterList[i];
      waterHolderPosition += water.transform.position;
    }
    waterHolderPosition /= topWaterCount;
    waterLevel = waterHolderPosition.y;
  }

  // Update is called once per frame
  void Update()
  {
    UpdateWaterLevel();
  }
}
