using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
  // Global Constants
  [HideInInspector] public const bool DEBUG = false;
  [HideInInspector] public const bool CHEATING = true;
  [HideInInspector] public const float ARM_LENGTH = 2.0f;
  [HideInInspector] public const float IK_SPEED = 100.0f;
  [HideInInspector] public const float IK_DRAG_BASE = 1.0f;
  [HideInInspector] public const float IK_DRAG = 10.0f;
  [HideInInspector] public const float IK_GRAVITY = 1.0f;
  [HideInInspector] public const float IK_MASS_BODY = 1.5f;
  [HideInInspector] public const float IK_MASS_ARM = 0.8f;
  [HideInInspector] public const float IK_ANGULAR_DRAG_ARM = 0.05f;
  [HideInInspector] public const float IK_ANGULAR_DRAG_BODY = 0.35f;

  // Input Game Objects
  [SerializeField] private GameObject _player;
  [SerializeField] private GameObject _leftAim;
  [SerializeField] private GameObject _rightAim;
  [SerializeField] private Vector2 _virtualLeftAim;
  [SerializeField] private Vector2 _virtualRightAim;
  [SerializeField] public Camera _camera;

  // Sprites
  [SerializeField] private Sprite[] _handReleaseSpriteLeft;
  [SerializeField] private Sprite[] _handGrabSpriteLeft;
  [SerializeField] private Sprite[] _handReleaseSpriteRight;
  [SerializeField] private Sprite[] _handGrabSpriteRight;
  [SerializeField] private Sprite[] _humerusSprite;
  [SerializeField] private Sprite[] _radiusSprite;
  [SerializeField] private Sprite[] _playerTreeSprite;

  //Sprite Heads for minimap
  [SerializeField] public GameObject[] _playerHeadMinimap;

  // These are automatically based on above
  [HideInInspector] private MainMenu _mainMenu;
  [HideInInspector] private GameManager _gameManager;
  [HideInInspector] private GameObject _leftHumerus;
  [HideInInspector] private GameObject _leftRadius;
  [HideInInspector] private GameObject _leftHand;
  [HideInInspector] private GameObject _rightHumerus;
  [HideInInspector] private GameObject _rightRadius;
  [HideInInspector] private GameObject _rightHand;
  [HideInInspector] public GameObject _body;
  [HideInInspector] public GameObject _trophy;

  [HideInInspector] private Rigidbody2D _worldRigidbody;
  [HideInInspector] private Rigidbody2D _leftHumerusRigidbody;
  [HideInInspector] private Rigidbody2D _leftRadiusRigidbody;
  [HideInInspector] private Rigidbody2D _leftHandRigidbody;
  [HideInInspector] private Rigidbody2D _rightHumerusRigidbody;
  [HideInInspector] private Rigidbody2D _rightRadiusRigidbody;
  [HideInInspector] private Rigidbody2D _rightHandRigidbody;
  [HideInInspector] private Rigidbody2D _trophyRigidbody;


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
  [HideInInspector] public Vector2 leftControlerInput;
  [HideInInspector] public Vector2 rightControlerInput;

  // These are initialized directly
  [HideInInspector] private Controls controls;
  [HideInInspector] public int playerIndex;
  [HideInInspector] private int deviceIndex;

  // These are set by GameManager
  [HideInInspector] public GameObject leftIndicator;
  [HideInInspector] public GameObject rightIndicator;

  public EndCondition endCondition;
  

  [HideInInspector] public bool  justJoined;

  private Vector3 Divide(Vector3 a, Vector3 b)
  {
    return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
  }

  public void OnControlsChanged(PlayerInput playerInput)
  {
    // if (_gameManager.startingCamera != null)
    // {
    //   _camera = _gameManager.startingCamera;
    // }
    // playerInput.camera = _camera;
    playerInput.camera.cullingMask = (1 << LayerMask.NameToLayer("Default"))
    | (1 << LayerMask.NameToLayer("UI"))
    | (1 << LayerMask.NameToLayer("Floater"))
    | (1 << LayerMask.NameToLayer("Sinker"))
    | (1 << LayerMask.NameToLayer("PlayerArm"))
    | (1 << LayerMask.NameToLayer("Player"));
    if (controls == null)
    {
      controls = new Controls();
    }

    Debug.Log("Player " + playerInput.playerIndex + " joined");
    playerIndex = playerInput.playerIndex; 
    deviceIndex = -1;

    controls.ActionMap.LeftGrab.performed += ctx => OnLeftGrabEvent(ctx, playerInput.playerIndex);
    controls.ActionMap.LeftGrab.canceled += ctx => OnLeftGrabEvent(ctx, playerInput.playerIndex);
    controls.ActionMap.RightGrab.performed += ctx => OnRightGrabEvent(ctx, playerInput.playerIndex);
    controls.ActionMap.RightGrab.canceled += ctx => OnRightGrabEvent(ctx, playerInput.playerIndex);
    controls.ActionMap.LeftArm.performed += ctx => OnLeftMoveEvent(ctx, playerInput.playerIndex);
    controls.ActionMap.LeftArm.canceled += ctx => OnLeftMoveEvent(ctx, playerInput.playerIndex);
    controls.ActionMap.RightArm.performed += ctx => OnRightMoveEvent(ctx, playerInput.playerIndex);
    controls.ActionMap.RightArm.canceled += ctx => OnRightMoveEvent(ctx, playerInput.playerIndex);
    controls.ActionMap.Start.performed += ctx => _mainMenu.DisableMenu();
    controls.ActionMap.Back.performed += ctx => _mainMenu.EnableMenu();
    controls.Enable();
    
  }

  private bool isValidInput(InputAction.CallbackContext context)
  {
    if (deviceIndex == -1)
    {
      Debug.Log("New device detected");
      deviceIndex = context.control.device.deviceId;
      return true;
    }
    
    if (endCondition.touchedTrophy){
      return false;
    }
    return deviceIndex == context.control.device.deviceId;
  }

  // private GameObject getNearestRock(){
    
  // }

  // Now start Event Handlers
  public void ReleaseLeftGrab()
  {
    leftGrab = false;
    OnLeftGrabEvent();
  }
  public void OnLeftGrabEvent(InputAction.CallbackContext context, int playerIndex)
  {
    if (!isValidInput(context)) return;
    leftGrab = context.ReadValueAsButton();
    OnLeftGrabEvent();
  }
  public void OnLeftGrabEvent()
  {
    // update _leftAim with red color
    _leftAim.GetComponent<SpriteRenderer>().color = leftGrab ? Color.red : Color.gray;
    _leftHand.GetComponentInChildren<SpriteRenderer>().sprite = leftGrab ? _handGrabSpriteLeft[playerIndex % _handGrabSpriteLeft.Length] : _handReleaseSpriteLeft[playerIndex % _handReleaseSpriteLeft.Length];

    // if grabbing, add a hinge joint at hand position
    GameObject grabbed = _gameManager.positionRandomization.canGrab(_leftHand.transform.position);
    GameObject trophy = endCondition.touchingTrophy(false);
    if (trophy != null){
      grabbed = trophy;
      // grabbed_trophy
    }
    if (justJoined){
      GameObject[] rocks = _gameManager.positionRandomization.getRocks();
      int nearestRock = _gameManager.positionRandomization.getNearestRock();
      leftGrab = true;
      grabbed = rocks[nearestRock];
    }
    
    if ((leftGrab && grabbed != null)||(grabbed == trophy && grabbed != null))
    {
      if (_leftHandJoint == null)
      {
        _leftHandJoint = _leftHand.AddComponent<HingeJoint2D>();
      }
      Vector3 grabbingPosition;
      if (grabbed.tag == "rock"){
        grabbingPosition = CHEATING ? grabbed.transform.position : _leftHand.transform.position;
      }
      else {
        Debug.Assert(grabbed.tag == "trophy");
        grabbingPosition = _leftHand.transform.position;
      }
      
      _leftHandJoint.enabled = true;
      if (grabbed.tag == "trophy"){
        _leftHandJoint.connectedBody = _trophyRigidbody;
        _leftHandJoint.anchor = Vector2.zero;
        _leftHandJoint.connectedAnchor = Divide((grabbingPosition - _trophyRigidbody.transform.position), _trophyRigidbody.transform.lossyScale);
      }
      else{
        _leftHandJoint.connectedBody = _worldRigidbody;
        _leftHandJoint.anchor = Vector2.zero;
        _leftHandJoint.connectedAnchor = Divide((grabbingPosition - _worldRigidbody.transform.position), _worldRigidbody.transform.lossyScale);
      }

      _leftHandJoint.autoConfigureConnectedAnchor = false;
      _leftHandJoint.useLimits = false;
      _leftHandJoint.useMotor = false;
    }
    else
    {
      if (_leftHandJoint != null)
      {
        _leftHandJoint.enabled = false;
      }
    }
  }

  public void ReleaseRightGrab()
  {
    rightGrab = false;
    OnRightGrabEvent();
  }
  public void OnRightGrabEvent(InputAction.CallbackContext context, int playerIndex)
  {
    if (!isValidInput(context)) return;
    rightGrab = context.ReadValueAsButton();
    OnRightGrabEvent();
  }
  public void OnRightGrabEvent()
  {

    // update _rightAim with red color
    _rightAim.GetComponent<SpriteRenderer>().color = rightGrab ? Color.red : Color.gray;
    _rightHand.GetComponentInChildren<SpriteRenderer>().sprite = rightGrab ? _handGrabSpriteRight[playerIndex % _handGrabSpriteRight.Length] : _handReleaseSpriteRight[playerIndex % _handReleaseSpriteRight.Length];

    // if grabbing, add a hinge joint at hand position
    GameObject grabbed = _gameManager.positionRandomization.canGrab(_rightHand.transform.position);
    GameObject trophy = endCondition.touchingTrophy(true);
    if (trophy != null){
      grabbed = trophy;
    }

    if ((rightGrab && grabbed != null)||(grabbed == trophy && grabbed != null))
    {
      if (justJoined){
        justJoined = false;
      }
      if (_rightHandJoint == null)
      {
        _rightHandJoint = _rightHand.AddComponent<HingeJoint2D>();
      }
      Vector3 grabbingPosition;
      if (grabbed.tag == "rock"){
        grabbingPosition = CHEATING ? grabbed.transform.position : _rightHand.transform.position;
      }
      else {
        Debug.Assert(grabbed.tag == "trophy");
        grabbingPosition = _rightHand.transform.position;
      }
      
      _rightHandJoint.enabled = true;

      if (grabbed.tag == "trophy"){
        _rightHandJoint.connectedBody = _trophyRigidbody;
        _rightHandJoint.anchor = Vector2.zero;
        _rightHandJoint.connectedAnchor = Divide((grabbingPosition - _trophyRigidbody.transform.position), _trophyRigidbody.transform.lossyScale);
      }
      else{
        _rightHandJoint.connectedBody = _worldRigidbody;
        _rightHandJoint.anchor = Vector2.zero;
        _rightHandJoint.connectedAnchor = Divide((grabbingPosition - _worldRigidbody.transform.position), _worldRigidbody.transform.lossyScale);
      }
      _rightHandJoint.autoConfigureConnectedAnchor = false;
      _rightHandJoint.useLimits = false;
      _rightHandJoint.useMotor = false;
    }
    else
    {
      if (_rightHandJoint != null)
      {
        _rightHandJoint.enabled = false;
      }
    }
  }

  public void OnLeftMoveEvent(InputAction.CallbackContext context, int playerIndex)
  {
    if (!isValidInput(context)) return;
    leftControlerInput = context.ReadValue<Vector2>();
    leftStick = (Vector2)_leftBody2HumerusPoint + leftControlerInput * ARM_LENGTH;
  }

  public void OnRightMoveEvent(InputAction.CallbackContext context, int playerIndex)
  {
    if (!isValidInput(context)) return;
    rightControlerInput = context.ReadValue<Vector2>();
    rightStick = (Vector2)_rightBody2HumerusPoint + rightControlerInput * ARM_LENGTH;
  }

  void Awake()
  {
    _leftAim.SetActive(DEBUG);
    _rightAim.SetActive(DEBUG);

    // assert not null
    Debug.Assert(_gameManager == null);
    _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    Debug.Assert(_player != null);
    Debug.Assert(_leftAim != null);
    Debug.Assert(_rightAim != null);
    Debug.Assert(_handGrabSpriteLeft != null);
    Debug.Assert(_handReleaseSpriteLeft != null);
    Debug.Assert(_handGrabSpriteRight != null);
    Debug.Assert(_handReleaseSpriteRight != null);
    Debug.Assert(_virtualLeftAim != null);
    Debug.Assert(_virtualRightAim != null);

    // get children game objects of _player
    _mainMenu = Resources.FindObjectsOfTypeAll<MainMenu>()[0].gameObject.GetComponent<MainMenu>();
    endCondition = Resources.FindObjectsOfTypeAll<EndCondition>()[0].gameObject.GetComponent<EndCondition>();
    _trophy = GameObject.Find("trophy");
    _body = _player.transform.Find("Body").gameObject;
    _leftHumerus = _body.transform.Find("LeftHumerus").gameObject;
    _leftRadius = _body.transform.Find("LeftRadius").gameObject;
    _leftHand = _body.transform.Find("LeftHand").gameObject;
    _rightHumerus = _body.transform.Find("RightHumerus").gameObject;
    _rightRadius = _body.transform.Find("RightRadius").gameObject;
    _rightHand = _body.transform.Find("RightHand").gameObject;

    //_playerHead = _player.transform.Find("Player Head").gameObject;

    Debug.Assert(_mainMenu != null);
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

    Debug.Assert(_leftHumerusBodyJoint != null);
    Debug.Assert(_leftRadiusHumerusJoint != null);
    Debug.Assert(_leftHandRadiusJoint != null);
    Debug.Assert(_rightHumerusBodyJoint != null);
    Debug.Assert(_rightRadiusHumerusJoint != null);
    Debug.Assert(_rightHandRadiusJoint != null);

    // get components: rigidbody
    _worldRigidbody = _gameManager.world.GetComponent<Rigidbody2D>();
    _leftHumerusRigidbody = _leftHumerus.GetComponent<Rigidbody2D>();
    _leftRadiusRigidbody = _leftRadius.GetComponent<Rigidbody2D>();
    _leftHandRigidbody = _leftHand.GetComponent<Rigidbody2D>();
    _rightHumerusRigidbody = _rightHumerus.GetComponent<Rigidbody2D>();
    _rightRadiusRigidbody = _rightRadius.GetComponent<Rigidbody2D>();
    _rightHandRigidbody = _rightHand.GetComponent<Rigidbody2D>();
    _trophyRigidbody = _trophy.GetComponent<Rigidbody2D>();

    //add tags to hands (for collisions)
    _leftHand.tag = "leftHand";
    _rightHand.tag = "rightHand";

    Debug.Assert(_worldRigidbody != null);
    Debug.Assert(_leftHumerusRigidbody != null);
    Debug.Assert(_leftRadiusRigidbody != null);
    Debug.Assert(_leftHandRigidbody != null);
    Debug.Assert(_rightHumerusRigidbody != null);
    Debug.Assert(_rightRadiusRigidbody != null);
    Debug.Assert(_rightHandRigidbody != null);

    // set rigidbody angular drag
    _leftHumerusRigidbody.angularDrag = IK_DRAG;
    _leftRadiusRigidbody.angularDrag = IK_DRAG;
    _leftHandRigidbody.angularDrag = IK_DRAG;
    _rightHumerusRigidbody.angularDrag = IK_DRAG;
    _rightRadiusRigidbody.angularDrag = IK_DRAG;
    _rightHandRigidbody.angularDrag = IK_DRAG;

    // set ridigbody mass
    Rigidbody2D[] rigidBodies = GetComponentsInChildren<Rigidbody2D>(true);
    foreach (Rigidbody2D rigidBody in rigidBodies)
    {
      rigidBody.gravityScale = IK_GRAVITY;
      rigidBody.mass = IK_MASS_ARM;
      rigidBody.angularDrag = IK_ANGULAR_DRAG_ARM;
    }

    // change player sprite
    GameObject bodySprite = _body.transform.Find("BodySprite").gameObject;
    GameObject playerTree = bodySprite.transform.Find("PlayerTree").gameObject;
    playerTree.GetComponent<SpriteRenderer>().sprite = _playerTreeSprite[playerIndex % _playerTreeSprite.Length];
    _leftHumerus.GetComponentInChildren<SpriteRenderer>().sprite = _humerusSprite[playerIndex % _humerusSprite.Length];
    _leftRadius.GetComponentInChildren<SpriteRenderer>().sprite = _radiusSprite[playerIndex % _radiusSprite.Length];
    _leftHand.GetComponentInChildren<SpriteRenderer>().sprite = _handReleaseSpriteLeft[playerIndex % _handReleaseSpriteLeft.Length];
    _rightHumerus.GetComponentInChildren<SpriteRenderer>().sprite = _humerusSprite[playerIndex % _humerusSprite.Length];
    _rightRadius.GetComponentInChildren<SpriteRenderer>().sprite = _radiusSprite[playerIndex % _radiusSprite.Length];
    _rightHand.GetComponentInChildren<SpriteRenderer>().sprite = _handReleaseSpriteRight[playerIndex % _handReleaseSpriteRight.Length];

    rigidBodies = playerTree.GetComponentsInChildren<Rigidbody2D>(true);
    foreach (Rigidbody2D rigidBody in rigidBodies)
    {
      rigidBody.gravityScale = IK_GRAVITY;
      rigidBody.mass = IK_MASS_BODY;
      rigidBody.angularDrag = IK_ANGULAR_DRAG_BODY;

      //_gameManager.waterManager.AddFloater(rigidBody);
    }

    // add itself to GameManager
    _gameManager.players.Add(this);
  }

  public Vector2 getHandPosition(bool isLeft)
  {
    if (isLeft)
    {
      return _leftHand.transform.position;
    }
    else
    {
      return _rightHand.transform.position;
    }
  }


  void Start()
  {
  }

  void Update()
  {
    leftStick = (Vector2)_leftBody2HumerusPoint + leftControlerInput * ARM_LENGTH;
    rightStick = (Vector2)_rightBody2HumerusPoint + rightControlerInput * ARM_LENGTH;

    // virtual aim for knowing what force to apply
    _virtualLeftAim = leftStick;
    _virtualRightAim = rightStick;

    float r2ov2 = Mathf.Sqrt(2.0f)/2.0f;

    if (leftGrab && leftControlerInput.y < -r2ov2)
    {
      leftStick = (Vector2)_leftBody2HumerusPoint + (new Vector2(leftControlerInput.x > 0 ? r2ov2 : -r2ov2, -r2ov2)) * ARM_LENGTH;
      _virtualLeftAim = leftStick;
    }
    if (rightGrab && rightControlerInput.y < -r2ov2)
    {
      rightStick = (Vector2)_rightBody2HumerusPoint + (new Vector2(rightControlerInput.x > 0 ? r2ov2 : -r2ov2, -r2ov2)) * ARM_LENGTH;
      _virtualRightAim = rightStick;
    }

    // aim indicator
    // if (!leftGrab)
    // {
    _leftAim.transform.position = new Vector3(leftStick.x, leftStick.y, 0);
    // }
    // if (!rightGrab)
    // {
    _rightAim.transform.position = new Vector3(rightStick.x, rightStick.y, 0);
    // }

    // camera follow
    if (!endCondition.transformed && !(_body.transform.position.y < endCondition.gameOverLine)){
      _camera.transform.position = new Vector3(0, _body.transform.position.y, endCondition.gameOverLine);
    } else{
      if ((endCondition.newObj != null) && (!(endCondition.newObj.transform.position.y < endCondition.gameOverLine))){
        _camera.transform.position = new Vector3(0,endCondition.newObj.transform.position.y, endCondition.gameOverLine);
      }
    }
    _camera.orthographicSize = 7.0f;
  }

  private void FixedUpdate()
  {
    // for water: if trying to float up, no collision, otherwise there is
    float leftHandCollisionProbability = Vector2.Dot(_leftHandRigidbody.velocity, Vector2.right);
    float rightHandCollisionProbability = Vector2.Dot(_rightHandRigidbody.velocity, Vector2.right);
    _leftHand.GetComponent<CircleCollider2D>().enabled = Random.Range(0f, 1f) < leftHandCollisionProbability;
    _rightHand.GetComponent<CircleCollider2D>().enabled = Random.Range(0f, 1f) < rightHandCollisionProbability;


    leftStick = (Vector2)_leftBody2HumerusPoint + leftControlerInput * ARM_LENGTH;
    rightStick = (Vector2)_rightBody2HumerusPoint + rightControlerInput * ARM_LENGTH;

    // update vectors based on joint component positions
    _leftBody2HumerusPoint = _leftHumerus.transform.TransformPoint(_leftHumerusBodyJoint.anchor);
    _leftHumerus2RadiusPoint = _leftRadius.transform.TransformPoint(_leftRadiusHumerusJoint.anchor);
    _leftRadius2HandPoint = _leftHand.transform.TransformPoint(_leftHandRadiusJoint.anchor);

    // right arm
    _rightBody2HumerusPoint = _rightHumerus.transform.TransformPoint(_rightHumerusBodyJoint.anchor);
    _rightHumerus2RadiusPoint = _rightRadius.transform.TransformPoint(_rightRadiusHumerusJoint.anchor);
    _rightRadius2HandPoint = _rightHand.transform.TransformPoint(_rightHandRadiusJoint.anchor);


    // Calculate IK: gradient to _leftAim position
    if (leftControlerInput.magnitude != 0)
    {
      float boost = Vector2.Distance(_leftRadius2HandPoint, _virtualLeftAim); // for boosting torque at parallel angle
      // make [Body, Hand] vector to [Body, Aim] vector
      Vector2 leftBody2AimVector = _virtualLeftAim - _leftBody2HumerusPoint;
      Vector2 leftBody2HumerusVector = _virtualLeftAim - _leftRadius2HandPoint;
      float angle = Vector2.SignedAngle(leftBody2HumerusVector, leftBody2AimVector);
      _leftHumerusBodyJoint.GetComponent<Rigidbody2D>().AddTorque(-angle * IK_SPEED * Time.deltaTime * boost);

      // make [Humerus, Radius] vector to [Humerus, Aim] vector
      Vector2 leftHumerus2AimVector = _virtualLeftAim - _leftHumerus2RadiusPoint;
      Vector2 leftHumerus2RadiusVector = _virtualLeftAim - _leftRadius2HandPoint;
      angle = Vector2.SignedAngle(leftHumerus2RadiusVector, leftHumerus2AimVector);
      _leftRadiusHumerusJoint.GetComponent<Rigidbody2D>().AddTorque(-angle * IK_SPEED * Time.deltaTime * boost);

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

    }

    if (rightControlerInput.magnitude != 0)
    {
      float boost = Vector2.Distance(_rightRadius2HandPoint, _virtualRightAim); // for boosting torque at parallel angle
      // right arm
      Vector2 rightBody2AimVector = _virtualRightAim - _rightBody2HumerusPoint;
      Vector2 rightBody2HumerusVector = _virtualRightAim - _rightRadius2HandPoint;
      float angle = Vector2.SignedAngle(rightBody2HumerusVector, rightBody2AimVector);
      _rightHumerusBodyJoint.GetComponent<Rigidbody2D>().AddTorque(-angle * IK_SPEED * Time.deltaTime * boost);

      // right arm
      Vector2 rightHumerus2AimVector = _virtualRightAim - _rightHumerus2RadiusPoint;
      Vector2 rightHumerus2RadiusVector = _virtualRightAim - _rightRadius2HandPoint;
      angle = Vector2.SignedAngle(rightHumerus2RadiusVector, rightHumerus2AimVector);
      _rightRadiusHumerusJoint.GetComponent<Rigidbody2D>().AddTorque(-angle * IK_SPEED * Time.deltaTime * boost);

      // right arm
      float distance = Vector2.Distance(_rightAim.transform.position, _rightHand.transform.position);
      float drag = Mathf.Exp(-distance) * IK_DRAG + IK_DRAG_BASE;
      _rightHumerusRigidbody.angularDrag = drag;
      _rightRadiusRigidbody.angularDrag = drag;
      _rightHandRigidbody.angularDrag = drag;
    }
  }

  private void LateUpdate()
  {

  }

  private void OnEnable()
  {
  }

  private void OnDisable()
  {
    if (controls != null)
    {
      controls.Disable();
    }
  }

  public void Hit() {
    if (leftGrab && rightGrab) {
      if (Random.Range(0, 2) == 0) {
        ReleaseLeftGrab();
        Debug.Log("left release");
        _leftHandRigidbody.AddForce(Vector2.down * 100);
      } else {
        ReleaseRightGrab();
        Debug.Log("right release");
        _rightHandRigidbody.AddForce(Vector2.down * 100);
      }
    }
  }
}
