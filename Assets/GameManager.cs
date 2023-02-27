using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // Global Settings
    [HideInInspector] public const float ARM_LENGTH = 5.0f;

    // Input Game Objetcs
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _test;

    // These are automatically based on above
    private GameObject _leftArm;
    private GameObject _rightArm;
    private GameObject _body;

    // These are set by Controls
    public bool leftGrab;
    public bool rightGrab;
    public Vector2 leftStick;
    public Vector2 rightStick;

    // These are initialized dirrectly
    private Controls controls;

    void Awake() {
      // register input
      controls = new Controls();
      // BUG: related to gamepad, not update if not going back to zero
      controls.ActionMap.LeftGrab.performed += ctx => leftGrab = ctx.ReadValueAsButton();
      controls.ActionMap.LeftGrab.canceled += ctx => leftGrab = false;
      controls.ActionMap.RightGrab.performed += ctx => rightGrab = ctx.ReadValueAsButton();
      controls.ActionMap.RightGrab.canceled += ctx => rightGrab = false;
      controls.ActionMap.LeftArm.performed += ctx => leftStick = ctx.ReadValue<Vector2>();
      controls.ActionMap.LeftArm.canceled += ctx => leftStick = Vector2.zero;
      controls.ActionMap.RightArm.performed += ctx => rightStick = ctx.ReadValue<Vector2>();
      controls.ActionMap.RightArm.canceled += ctx => rightStick = Vector2.zero;
    }

    void OnEnable() {
      controls.Enable();
    }

    void OnDisable() {
      controls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        // get game children game objects of _player
        GameObject[] children = new GameObject[_player.transform.childCount];
        for (int i = 0; i < _player.transform.childCount; i++)
        {
            children[i] = _player.transform.GetChild(i).gameObject;

            if (children[i].name == "LeftArm")
            {
                _leftArm = children[i];
            }
            if (children[i].name == "RightArm")
            {
                _rightArm = children[i];
            }
            if (children[i].name == "Body")
            {
                _body = children[i];
            }
        }
        
        // assert none of game objects are null
        Debug.Assert(_player != null);
        Debug.Assert(_test != null);
        Debug.Assert(_leftArm != null);
        Debug.Assert(_rightArm != null);
        Debug.Assert(_body != null);
    }

    // Update is called once per frame
    void Update()
    {
      // update controls
      // BUG: hot fix
      float horizontal = Input.GetAxis("Horizontal");
      float vertical = Input.GetAxis("Vertical");
      leftStick = new Vector2(horizontal, vertical);

      // print controls
      Debug.Log("leftGrab: " + leftGrab);
      Debug.Log("rightGrab: " + rightGrab);
      Debug.Log("leftStick: " + leftStick);
      Debug.Log("rightStick: " + rightStick);

      // move tmp
      _test.transform.position = new Vector3(leftStick.x, leftStick.y, 0);

      // move left arm
      TransformCapsule(_leftArm, new Vector2(0.0f, 0.0f), leftStick * ARM_LENGTH);
      TransformCapsule(_rightArm, new Vector2(0.0f, 0.0f), rightStick * ARM_LENGTH);
    }

    void TransformCapsule(GameObject go, Vector2 origin, Vector2 direction) {
      Vector2 center = (origin + direction) / 2;
      float angle = Vector2.SignedAngle(Vector2.up, direction);
      float length = Vector2.Distance(origin, direction);
      go.transform.position = center;
      go.transform.rotation = Quaternion.Euler(0, 0, angle);
      go.transform.localScale = new Vector3(1, length, 1);
    }
}
