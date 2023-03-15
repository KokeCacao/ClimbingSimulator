using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

  [SerializeField] public GameObject world;
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
  }

  void OnDisable()
  {
  }

  void Start()
  {
  }

  void Update()
  {

  }

  void FixedUpdate()
  {

  }

  void LateUpdate()
  {
  }
}
