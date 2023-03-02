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

  // These are initialized directly
  private Controls controls;

  void Awake()
  {
    // register input
    controls = new Controls();
    // BUG: related to gamepad, not update if not going back to zero
    controls.ActionMap.LeftGrab.performed += ctx => OnLeftGrab(ctx);
    controls.ActionMap.LeftGrab.canceled += ctx => OnLeftRelease(ctx);
    controls.ActionMap.RightGrab.performed += ctx => rightGrab = ctx.ReadValueAsButton();
    controls.ActionMap.RightGrab.canceled += ctx => rightGrab = false;
    controls.ActionMap.LeftArm.performed += ctx => OnMoveLeft(ctx);
    controls.ActionMap.LeftArm.canceled += ctx => leftStick = Vector2.zero;
    controls.ActionMap.RightArm.performed += ctx => rightStick = ctx.ReadValue<Vector2>();
    controls.ActionMap.RightArm.canceled += ctx => rightStick = Vector2.zero;
  }

  void OnMoveLeft(InputAction.CallbackContext context)
  {
    leftStick = context.ReadValue<Vector2>();
    // update Anchor Position of HingeJoint2D
    HingeJoint2D[] joints = _leftArm.GetComponents<HingeJoint2D>();
    foreach (HingeJoint2D joint in joints)
    {
      if (joint.connectedBody == _body.GetComponent<Rigidbody2D>())
      {
        joint.anchor = new Vector2(0, -(leftStick.magnitude * ARM_LENGTH) / 2.0f);
      }
    }
  }

  void OnLeftGrab(InputAction.CallbackContext context)
  {
    if (leftGrab == false)
    {
      // only execute on first press
      Grab(_leftArm, leftStick * ARM_LENGTH);
    }
    leftGrab = context.ReadValueAsButton();
  }

  void OnLeftRelease(InputAction.CallbackContext context)
  {
    if (leftGrab == true)
    {
      // only execute on first release
      Release(_leftArm);
    }
    leftGrab = false;
  }

  void OnEnable()
  {
    controls.Enable();
  }

  void OnDisable()
  {
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

    // set initial values
    // Grab(_leftArm, new Vector2(0.0f, 0.0f));
  }

  // Update is called once per frame
  void Update()
  {
    // move tmp
    // _test.transform.position = new Vector3(leftStick.x, leftStick.y, 0);

  }

  void FixedUpdate() {
    // move left arm
    TransformGameObject(_leftArm, new Vector2(_leftArm.transform.position.x, _leftArm.transform.position.y) - 0.5f * (leftStick * ARM_LENGTH), leftStick * ARM_LENGTH);
    // TransformGameObject(_rightArm, new Vector2(_rightArm.transform.position.x, _rightArm.transform.position.y) - 0.5f * (rightStick * ARM_LENGTH), rightStick * ARM_LENGTH);
    
  }
  
  // late update (after physics): camera
  // fixed update (physics): movement, collisions, etc.

  void LateUpdate() {
  }

  void TransformGameObject(GameObject go, Vector2 origin, Vector2 direction)
  {
    print(go.transform.rotation.eulerAngles.z);

    Vector2 center = (origin + direction) / 2;
    float angle = Vector2.SignedAngle(Vector2.up, direction);
    float length = direction.magnitude;
    go.transform.position = center;
    // go.transform.rotation = Quaternion.AxisAngle(Vector3.forward, angle * Mathf.Deg2Rad);
    go.transform.rotation = Quaternion.Euler(0, 0, angle);

    // get the SpriteRenderer of GameObject
    SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
    spriteRenderer.drawMode = SpriteDrawMode.Tiled;
    spriteRenderer.size = new Vector2(1, length);
  }

  void Grab(GameObject go, Vector2 position)
  {
    // add HingeJoint2D to GameObject
    HingeJoint2D joint = go.AddComponent<HingeJoint2D>();
    joint.connectedBody = _test.GetComponent<Rigidbody2D>();
    joint.autoConfigureConnectedAnchor = false; // IMPORTANT
    joint.anchor = new Vector2(0, Vector2.Distance(new Vector2(0.0f, 0.0f), leftStick * ARM_LENGTH) / 2); // This should be the vector from center of [go] to left top of [go]
    joint.connectedAnchor = position; // This should be 
    joint.enabled = true;

    _leftArm.GetComponent<Rigidbody2D>().gravityScale = 1.0f;

  }

  void Release(GameObject go)
  {
    // remove HingeJoint2D from GameObject
    HingeJoint2D[] joints = go.GetComponents<HingeJoint2D>();
    foreach (HingeJoint2D joint in joints)
    {
      if (joint.connectedBody == _test.GetComponent<Rigidbody2D>())
      {
        joint.enabled = false;
        Destroy(joint);
        break;
      }
    }
  }
}
