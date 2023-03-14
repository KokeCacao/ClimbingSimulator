using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
  // Global Constants
  [HideInInspector] public const float ARM_LENGTH = 2.0f;
  [HideInInspector] public const float IK_SPEED = 100.0f;
  [HideInInspector] public const float IK_DRAG_BASE = 1.0f;
  [HideInInspector] public const float IK_DRAG = 100.0f;

  // Input Game Objects
  public float playerNumber = 0;
  [SerializeField] private GameManager _gameManager;
  [SerializeField] private GameObject _player;
  [SerializeField] private GameObject _leftAim;
  [SerializeField] private GameObject _rightAim;

  // These are automatically based on above
  [HideInInspector] private GameObject _leftHumerus;
  [HideInInspector] private GameObject _leftRadius;
  [HideInInspector] private GameObject _leftHand;
  [HideInInspector] private GameObject _rightHumerus;
  [HideInInspector] private GameObject _rightRadius;
  [HideInInspector] private GameObject _rightHand;
  [HideInInspector] private GameObject _body;

  [HideInInspector] private Rigidbody2D _worldRigidbody;
  [HideInInspector] private Rigidbody2D _leftHumerusRigidbody;
  [HideInInspector] private Rigidbody2D _leftRadiusRigidbody;
  [HideInInspector] private Rigidbody2D _leftHandRigidbody;
  [HideInInspector] private Rigidbody2D _rightHumerusRigidbody;
  [HideInInspector] private Rigidbody2D _rightRadiusRigidbody;
  [HideInInspector] private Rigidbody2D _rightHandRigidbody;


  // These component are automatically based on above
  [HideInInspector] private HingeJoint2D _leftHumerusBodyJoint;
  [HideInInspector] private HingeJoint2D _leftRadiusHumerusJoint;
  [HideInInspector] private HingeJoint2D _leftHandRadiusJoint;
  [HideInInspector] private HingeJoint2D _leftHandJoint;
  [HideInInspector] private HingeJoint2D _rightHumerusBodyJoint;
  [HideInInspector] private HingeJoint2D _rightRadiusHumerusJoint;
  [HideInInspector] private HingeJoint2D _rightHandRadiusJoint;
  [HideInInspector] private HingeJoint2D _rightHandJoint;

  // Vectors are calculated based on joint position
  [HideInInspector] private Vector2 _leftBody2HumerusPoint;
  [HideInInspector] private Vector2 _leftHumerus2RadiusPoint;
  [HideInInspector] private Vector2 _leftRadius2HandPoint;
  [HideInInspector] private Vector2 _rightBody2HumerusPoint;
  [HideInInspector] private Vector2 _rightHumerus2RadiusPoint;
  [HideInInspector] private Vector2 _rightRadius2HandPoint;

  // These are set by Controls
  [HideInInspector] public bool leftGrab;
  [HideInInspector] public bool rightGrab;
  [HideInInspector] public Vector2 leftStick;
  [HideInInspector] public Vector2 rightStick;

  // These are initialized directly
  [HideInInspector] private Controls controls;

  // Now start Event Handlers
  private void OnLeftGrabEvent(InputAction.CallbackContext context)
  {
    leftGrab = context.ReadValueAsButton();

    // update _leftAim with red color
    _leftAim.GetComponent<SpriteRenderer>().color = leftGrab ? Color.red : Color.gray;

    // if grabbing, add a hinge joint at hand position
    if (leftGrab)
    {
      if (_leftHandJoint == null) {
        _leftHandJoint = _leftHand.AddComponent<HingeJoint2D>();
      }
      _leftHandJoint.enabled = true;
      _leftHandJoint.connectedBody = _worldRigidbody;
      _leftHandJoint.anchor = Vector2.zero;
      _leftHandJoint.connectedAnchor = _leftHand.transform.position;
      _leftHandJoint.autoConfigureConnectedAnchor = false;
      _leftHandJoint.useLimits = false;
      _leftHandJoint.useMotor = false;
    }
    else
    {
      if (_leftHandJoint != null) {
        _leftHandJoint.enabled = false;
      }
    }
  }

  private void OnRightGrabEvent(InputAction.CallbackContext context)
  {
    rightGrab = context.ReadValueAsButton();

    // update _rightAim with red color
    _rightAim.GetComponent<SpriteRenderer>().color = rightGrab ? Color.red : Color.gray;
    
    // if grabbing, add a hinge joint at hand position
    if (rightGrab)
    {
      if (_rightHandJoint == null) {
        _rightHandJoint = _rightHand.AddComponent<HingeJoint2D>();
      }
      _rightHandJoint.enabled = true;
      _rightHandJoint.connectedBody = _worldRigidbody;
      _rightHandJoint.anchor = Vector2.zero;
      _rightHandJoint.connectedAnchor = _rightHand.transform.position;
      _rightHandJoint.autoConfigureConnectedAnchor = false;
      _rightHandJoint.useLimits = false;
      _rightHandJoint.useMotor = false;
    }
    else
    {
      if (_rightHandJoint != null) {
        _rightHandJoint.enabled = false;
      }
    }
  }

  private void OnLeftMoveEvent(InputAction.CallbackContext context)
  {
    leftStick = (Vector2) _leftBody2HumerusPoint + context.ReadValue<Vector2>() * ARM_LENGTH;

    // update _leftAim
    _leftAim.transform.position = new Vector3(leftStick.x, leftStick.y, 0);
  }

  private void OnRightMoveEvent(InputAction.CallbackContext context)
  {
    rightStick = (Vector2) _rightBody2HumerusPoint + context.ReadValue<Vector2>() * ARM_LENGTH;

    // update _rightAim
    _rightAim.transform.position = new Vector3(rightStick.x, rightStick.y, 0);
  }

  void Awake()
  {
    controls = new Controls();
    if (playerNumber == 0)
    {
      controls.ActionMap.LeftGrab.performed += ctx => OnLeftGrabEvent(ctx);
      controls.ActionMap.LeftGrab.canceled += ctx => OnLeftGrabEvent(ctx);
      controls.ActionMap.RightGrab.performed += ctx => OnRightGrabEvent(ctx);
      controls.ActionMap.RightGrab.canceled += ctx => OnRightGrabEvent(ctx);
      controls.ActionMap.LeftArm.performed += ctx => OnLeftMoveEvent(ctx);
      controls.ActionMap.LeftArm.canceled += ctx => OnLeftMoveEvent(ctx);
      controls.ActionMap.RightArm.performed += ctx => OnRightMoveEvent(ctx);
      controls.ActionMap.RightArm.canceled += ctx => OnRightMoveEvent(ctx);
    }
    else if (playerNumber == 1)
    {
      controls.ActionMap.LeftGrab1.performed += ctx => OnLeftGrabEvent(ctx);
      controls.ActionMap.LeftGrab1.canceled += ctx => OnLeftGrabEvent(ctx);
      controls.ActionMap.RightGrab1.performed += ctx => OnRightGrabEvent(ctx);
      controls.ActionMap.RightGrab1.canceled += ctx => OnRightGrabEvent(ctx);
      controls.ActionMap.LeftArm1.performed += ctx => OnLeftMoveEvent(ctx);
      controls.ActionMap.LeftArm1.canceled += ctx => OnLeftMoveEvent(ctx);
      controls.ActionMap.RightArm1.performed += ctx => OnRightMoveEvent(ctx);
      controls.ActionMap.RightArm1.canceled += ctx => OnRightMoveEvent(ctx);
    }
    else
    {
      Debug.LogError("Player number must be 1 or 0");
    }
  }


  void Start()
  {
    // assert not null
    Debug.Assert(_gameManager != null);
    Debug.Assert(_player != null);
    Debug.Assert(_leftAim != null);
    Debug.Assert(_rightAim != null);

    // get children game objects of _player
    _body = _player.transform.Find("Body").gameObject;
    _leftHumerus = _body.transform.Find("LeftHumerus").gameObject;
    _leftRadius = _body.transform.Find("LeftRadius").gameObject;
    _leftHand = _body.transform.Find("LeftHand").gameObject;
    _rightHumerus = _body.transform.Find("RightHumerus").gameObject;
    _rightRadius = _body.transform.Find("RightRadius").gameObject;
    _rightHand = _body.transform.Find("RightHand").gameObject;

    Debug.Assert(_body != null);
    Debug.Assert(_leftHumerus != null);
    Debug.Assert(_leftRadius != null);
    Debug.Assert(_leftHand != null);
    Debug.Assert(_rightHumerus != null);
    Debug.Assert(_rightRadius != null);
    Debug.Assert(_rightHand != null);

    // get components: hinge joint
    _leftHumerusBodyJoint = _leftHumerus.GetComponent<HingeJoint2D>();
    _leftRadiusHumerusJoint = _leftRadius.GetComponent<HingeJoint2D>();
    _leftHandRadiusJoint = _leftHand.GetComponent<HingeJoint2D>();
    _rightHumerusBodyJoint = _rightHumerus.GetComponent<HingeJoint2D>();
    _rightRadiusHumerusJoint = _rightRadius.GetComponent<HingeJoint2D>();
    _rightHandRadiusJoint = _rightHand.GetComponent<HingeJoint2D>();

    // get components: rigidbody
    _worldRigidbody = _gameManager.world.GetComponent<Rigidbody2D>();
    _leftHumerusRigidbody = _leftHumerus.GetComponent<Rigidbody2D>();
    _leftRadiusRigidbody = _leftRadius.GetComponent<Rigidbody2D>();
    _leftHandRigidbody = _leftHand.GetComponent<Rigidbody2D>();
    _rightHumerusRigidbody = _rightHumerus.GetComponent<Rigidbody2D>();
    _rightRadiusRigidbody = _rightRadius.GetComponent<Rigidbody2D>();
    _rightHandRigidbody = _rightHand.GetComponent<Rigidbody2D>();

    // set rigidbody angular drag
    _leftHumerusRigidbody.angularDrag = IK_DRAG;
    _leftRadiusRigidbody.angularDrag = IK_DRAG;
    _leftHandRigidbody.angularDrag = IK_DRAG;
    _rightHumerusRigidbody.angularDrag = IK_DRAG;
    _rightRadiusRigidbody.angularDrag = IK_DRAG;
    _rightHandRigidbody.angularDrag = IK_DRAG;
  }

  void Update()
  {

  }

  private void FixedUpdate()
  {
    // update vectors based on joint component positions
    _leftBody2HumerusPoint = _leftHumerus.transform.TransformPoint(_leftHumerusBodyJoint.anchor);
    _leftHumerus2RadiusPoint = _leftRadius.transform.TransformPoint(_leftRadiusHumerusJoint.anchor);
    _leftRadius2HandPoint = _leftHand.transform.TransformPoint(_leftHandRadiusJoint.anchor);

    // right arm
    _rightBody2HumerusPoint = _rightHumerus.transform.TransformPoint(_rightHumerusBodyJoint.anchor);
    _rightHumerus2RadiusPoint = _rightRadius.transform.TransformPoint(_rightRadiusHumerusJoint.anchor);
    _rightRadius2HandPoint = _rightHand.transform.TransformPoint(_rightHandRadiusJoint.anchor);
    

    // Calculate IK: gradient to _leftAim position

    // make [Body, Hand] vector to [Body, Aim] vector
    Vector2 leftBody2AimVector = ((Vector2)_leftAim.transform.position) - _leftBody2HumerusPoint;
    Vector2 leftBody2HumerusVector = ((Vector2)_leftAim.transform.position) - _leftRadius2HandPoint;
    float angle = Vector2.SignedAngle(leftBody2HumerusVector, leftBody2AimVector);
    _leftHumerusBodyJoint.GetComponent<Rigidbody2D>().AddTorque(-angle * IK_SPEED * Time.deltaTime);

    // right arm
    Vector2 rightBody2AimVector = ((Vector2)_rightAim.transform.position) - _rightBody2HumerusPoint;
    Vector2 rightBody2HumerusVector = ((Vector2)_rightAim.transform.position) - _rightRadius2HandPoint;
    angle = Vector2.SignedAngle(rightBody2HumerusVector, rightBody2AimVector);
    _rightHumerusBodyJoint.GetComponent<Rigidbody2D>().AddTorque(-angle * IK_SPEED * Time.deltaTime);

    // make [Humerus, Radius] vector to [Humerus, Aim] vector
    Vector2 leftHumerus2AimVector = ((Vector2)_leftAim.transform.position) - _leftHumerus2RadiusPoint;
    Vector2 leftHumerus2RadiusVector = ((Vector2)_leftAim.transform.position) - _leftRadius2HandPoint;
    angle = Vector2.SignedAngle(leftHumerus2RadiusVector, leftHumerus2AimVector);
    _leftRadiusHumerusJoint.GetComponent<Rigidbody2D>().AddTorque(-angle * IK_SPEED * Time.deltaTime);

    // right arm
    Vector2 rightHumerus2AimVector = ((Vector2)_rightAim.transform.position) - _rightHumerus2RadiusPoint;
    Vector2 rightHumerus2RadiusVector = ((Vector2)_rightAim.transform.position) - _rightRadius2HandPoint;
    angle = Vector2.SignedAngle(rightHumerus2RadiusVector, rightHumerus2AimVector);
    _rightRadiusHumerusJoint.GetComponent<Rigidbody2D>().AddTorque(-angle * IK_SPEED * Time.deltaTime);

    // make [Radius, Hand] vector to [Radius, Aim] vector
    // Vector2 leftRadius2AimVector = ((Vector2) _leftAim.transform.position) - _leftRadius2HandPoint;
    // Vector2 leftRadius2HandVector = ((Vector2) _leftAim.transform.position) - _leftHand2RadiusPoint;
    // angle = Vector2.SignedAngle(leftRadius2HandVector, leftRadius2AimVector);
    // _leftHandRadiusJoint.GetComponent<Rigidbody2D>().AddTorque(-angle * Mathf.Abs(angle) * IK_SPEED * Time.deltaTime);

    // update angular drag
    float distance = Vector2.Distance(_leftAim.transform.position, _leftHand.transform.position);
    float drag = Mathf.Exp(-distance) * IK_DRAG + IK_DRAG_BASE;
    _leftHumerusRigidbody.angularDrag = drag;
    _leftRadiusRigidbody.angularDrag = drag;
    _leftHandRigidbody.angularDrag = drag;

    // right arm
    distance = Vector2.Distance(_rightAim.transform.position, _rightHand.transform.position);
    drag = Mathf.Exp(-distance) * IK_DRAG + IK_DRAG_BASE;
    _rightHumerusRigidbody.angularDrag = drag;
    _rightRadiusRigidbody.angularDrag = drag;
    _rightHandRigidbody.angularDrag = drag;
  }

  private void LateUpdate()
  {

  }

  private void OnEnable()
  {
    controls.Enable();
  }

  private void OnDisable()
  {
    controls.Disable();
  }
}
