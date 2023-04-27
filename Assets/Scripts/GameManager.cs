using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

  [SerializeField] public GameObject world;
  [SerializeField] public GameObject indicatorPrefab;
  [SerializeField] public Camera startingCamera;

  [SerializeField] public PositionRandomization positionRandomization;
  [SerializeField] public Minimap minimap;
  [SerializeField] public WaterManager waterManager;
  private bool playerJoined = false;

  public List<PlayerManager> players;

  void Awake()
  {
  }

  

  public void OnPlayerJoined(PlayerInput playerInput)
  {
    Debug.Log("Player joined: " + playerInput.playerIndex);
    // Perform additional tasks when a player joins
  }

  public void OnPlayerLeft(PlayerInput playerInput)
  {
    Debug.Log("Player left: " + playerInput.playerIndex);
    // Perform additional tasks when a player leaves
  }


  void OnEnable()
  {
    Debug.Assert(world != null, "World is null");
    Debug.Assert(positionRandomization != null, "PositionRandomization is null");
    Debug.Assert(indicatorPrefab != null, "IndicatorPrefab is null");
    players = new List<PlayerManager>();
  }

  void OnDisable()
  {
  }

  void Start()
  {
  }

  void Update()
  {
    if (players.Count > 0 && !playerJoined){
      Debug.Log("player.Count > 0");
      // gameObject ferret = players[0]._body;
      // gameObject nearestRock = getNearestRock(ferret);
      PlayerManager player = players[0];
      player.justJoined = true;
      //player.OnLeftGrabEvent();
      playerJoined = true;
    }
    GameObject[] rocks = positionRandomization.getRocks();
      foreach (PlayerManager player in players)
      {
        Vector2 leftHandPos = player.getHandPosition(true);
        Vector2 rightHandPos = player.getHandPosition(false); 
        
        GameObject leftRock = positionRandomization.canGrab(leftHandPos);
        GameObject rightRock = positionRandomization.canGrab(rightHandPos);
        if (player.justJoined){
          leftRock = rocks[20];
        }
        if (leftRock != null) {
          if (player.leftIndicator == null) {
            player.leftIndicator = Instantiate(indicatorPrefab);
            player.leftIndicator.transform.parent = world.transform;
          }
          player.leftIndicator.transform.position = leftRock.transform.position;
          player.leftIndicator.SetActive(true);
          player.OnLeftGrabEvent(); // refresh, since player may hold the key
        } else {
          if (player.leftIndicator != null) {
            player.leftIndicator.SetActive(false);
          }
        }
        if (rightRock != null) {
          if (player.rightIndicator == null) {
            player.rightIndicator = Instantiate(indicatorPrefab);
            player.rightIndicator.transform.parent = world.transform;
          }
          player.rightIndicator.transform.position = rightRock.transform.position;
          player.rightIndicator.SetActive(true);
          player.OnRightGrabEvent(); // refresh, since player may hold the key
        } else {
          if (player.rightIndicator != null) {
            player.rightIndicator.SetActive(false);
          }
        }
        minimap.MinimapUpdate(player);
      }
    
  }

  void FixedUpdate()
  {

  }

  void LateUpdate()
  {
  }
}
