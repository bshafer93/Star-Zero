using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Handles player movement logic. 
/// Keeps track of ....
/// PlayerInfo
/// Main Camera
/// Near By Targets
/// Current target
/// If it's currently locked on to any thing.
/// TODO - This is a doozy of a class that I think we can parse down to the bare essentials and move other logic to their own class or another script.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField]  PlayerInfo playerInfo;
    [SerializeField] Vector3 directionVector;

    private Vector2 axisPos;

    private Rigidbody rb;

    [SerializeField] Camera mainCamera;

    private bool isDodging;

    PlayerInputAsset controls;

    [SerializeField] 
    bool isLockedOn = false;

    [SerializeField] 
    public RythymManager rm;

    Queue<GameObject> nearbyTargets;

    [SerializeField] Transform CurrentTarget;

    public GameObject enemyContainer;

    public GameObject lockOnReticlePrefab;

    [SerializeField] GameObject lockOnReticle;

    [SerializeField] 
    public GameObject rythymManager;

    public float[] button_hit_times;

    [SerializeField] bool isStrafing = false;
    Vector3 cardinalDirection;
    void OnEnable()
    {
        controls.Player.Enable();
    }
    void OnDisable()
    {
        controls.Player.Disable();
    }

    void Awake()
    {
        button_hit_times = new float[3]; 
        controls = new PlayerInputAsset();
        controls.Player.Move.performed += GetAxis;
        controls.Player.Move.canceled += (context) => { axisPos.Set(0, 0); };
        controls.Player.Dodge.started += (context) => { 
            isDodging = true;
            StartCoroutine(activateDodge(0.1f));
        };
        controls.Player.Dodge.canceled += (context) => { isDodging = false; };
        controls.Player.LockOn.started += LockOn;

        controls.Player.EquipLeft.started += (context) => { playerInfo.playerInventory.changeInstrument(0); };
        controls.Player.EquipRight.started += (context) => { playerInfo.playerInventory.changeInstrument(1); };

        controls.Player.Fire.started += (context) => { playerInfo.fireInstrument(); };
        controls.Player.Strafe.started += (context) => { 
            isStrafing = true; 
            float x = Mathf.Abs(axisPos.x);
            float z = Mathf.Abs(axisPos.y);
            cardinalDirection = (x > z) ? new Vector3(Mathf.Sign(axisPos.x), 0.0f, 0) : new Vector3(0, 0.0f, Mathf.Sign(axisPos.y));
            cardinalDirection = mainCamera.transform.TransformDirection(cardinalDirection);
            cardinalDirection.y = 0.0f;
        };
        controls.Player.Strafe.canceled += (context) => { isStrafing = false; };
        controls.Player.ForceStartSong.started += StartRythymSong;
        controls.Player.NoteA.started += NoteHit;
        controls.Player.NoteX.started += NoteHit;
        controls.Player.NoteY.started += NoteHit;
        controls.Player.ExitGame.started += (context) => { Application.Quit(); };
        

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        nearbyTargets = new Queue<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {


        MovePlayer();


    }

    void FixedUpdate() {


    }

    IEnumerator activateDodge(float seconds) {

        rb.AddForce(playerInfo.dodgeStrength * directionVector.normalized, ForceMode.Impulse);
        yield return new WaitForSeconds(seconds);
        rb.velocity = new Vector3(0f,0f,0f);
    }

    void MovePlayer()
    {
        directionVector = new Vector3(axisPos.x, 0.0f, axisPos.y);
        directionVector = mainCamera.transform.TransformDirection(directionVector);
        directionVector.y = 0.0f;
        rb.MovePosition(transform.position + directionVector.normalized * Time.deltaTime * playerInfo.getSpeed());

        if (directionVector != Vector3.zero || axisPos != Vector2.zero)
        {
            
            if (isLockedOn && CurrentTarget != null )
            {
                directionVector = CurrentTarget.position - transform.position;
                directionVector.y = 0.0f;
                rb.MoveRotation(Quaternion.LookRotation(directionVector));
            }
            else if (isStrafing)
            {
                rb.MoveRotation(Quaternion.LookRotation(cardinalDirection));
            }
            else
            {
                rb.MoveRotation(Quaternion.LookRotation(directionVector));
            }

        }
        else {
            rb.angularVelocity = new Vector3(0, 0, 0);
        }
    }

    void StartRythymSong(InputAction.CallbackContext context) {
       rm.StartSong();
    }

    void GetAxis(InputAction.CallbackContext context)
    {
        Vector2 axisPosition = context.ReadValue<Vector2>();
        if (axisPosition.magnitude >= 0.3f)
        {
            axisPos = axisPosition;
        }
        else {
            axisPos = new Vector2(0,0);
        }
    }
    void LockOn(InputAction.CallbackContext context){

        if (isLockedOn == false && PlayerInventory.hasRythym)
        {
            nearbyTargets = getNearbyEnemies();
            if (nearbyTargets.Count != 0)
            {
                CurrentTarget = nearbyTargets.Dequeue().transform;
                isLockedOn = true;
                rythymManager.SetActive(true);
                lockOnReticle = Instantiate<GameObject>(lockOnReticlePrefab, CurrentTarget);
            }

        }
        else if (PlayerInventory.hasRythym)
        {

            if (nearbyTargets.Count != 0)
            {
                Destroy(lockOnReticle);
                CurrentTarget = nearbyTargets.Dequeue().transform;
                lockOnReticle = Instantiate<GameObject>(lockOnReticlePrefab, CurrentTarget);
            }
            else
            {
                CurrentTarget = null;
                isLockedOn = false;
                rythymManager.SetActive(false);
                Destroy(lockOnReticle);
            }

        }
        else {
            Debug.Log("You need to unlock the Thunder Guitar!");
        
        }
       
    }



    Queue<GameObject> getNearbyEnemies()
    {
        //UpdatePosition...
        playerInfo.checkArea();


        Transform[] allEnemyPositions =  DungeonManager.enemy_containers[(int)PlayerInfo.player_area].GetComponentsInChildren<Transform>();
        List<GameObject> allEnemies = new List<GameObject>();

        for(int i = 0; i < allEnemyPositions.Length; i++){
            if (allEnemyPositions[i].gameObject.tag.Equals( "Enemy"))
            {
                allEnemies.Add(allEnemyPositions[i].gameObject);
            }
            
        }

        allEnemies.Sort((e1, e2) => {
            float e1Dist = Vector3.Distance(transform.position, e1.transform.position);
            float e2Dist = Vector3.Distance(transform.position, e2.transform.position);
            return e1Dist.CompareTo(e2Dist);
        });
        if (PlayerInfo.player_area == AREA_NAME.RM5) {

            allEnemies.Add(DungeonManager.Boss_Door_Static);
        }

        return new Queue<GameObject>(allEnemies);
    }

    public void NoteHit(InputAction.CallbackContext context){

        if(isLockedOn && rythymManager.activeInHierarchy){

            
                if( context.action == controls.Player.NoteA)
                {
                    //button_hit_times[0] = rm.song.songPosition;
                    rm.CheckNoteHit("A",rm.song.songPosition);
                }

                if (context.action == controls.Player.NoteX)
                {
                    //button_hit_times[1] = rm.song.songPosition;
                    rm.CheckNoteHit("X",rm.song.songPosition);
                }

                if ( context.action == controls.Player.NoteY)
                {
                    //button_hit_times[2] = rm.song.songPosition;
                    rm.CheckNoteHit("Y",rm.song.songPosition);
                }

            
        }else{
            button_hit_times[0] =0f;
            button_hit_times[1] =0f;
            button_hit_times[2] =0f;
        }


    }

    public void InputDeviceChanged(InputDevice device, InputDeviceChange change)
    {
        //Method from https://aidantakami.com/2021/02/02/detecting-the-players-controller-type-with-the-unity-input-system/
        switch (change)
        {
            //New device added
            case InputDeviceChange.Added:
                Debug.Log("New device added");
                
                //Checks if is Playstation Controller
                if (device.description.manufacturer == "Sony Interactive Entertainment")
                {
                    //Sets UI scheme
                    Debug.Log("Playstation Controller Detected");
                    /*
                    currentImageScheme.SetImagesToPlaystation();
                    _currentController = CurrentControllerType.PlayStation;
                    controllerTypeChange.Invoke();
                    */
                }
                //Else, assumes Xbox controller
                //device.description.manufacturer for Xbox returns empty string
                else if(device.description.manufacturer != "Sony Interactive Entertainment")
                {
                    Debug.Log("Xbox Controller Detected");
                    /*
                    currentImageScheme.SetImagesToXbox();
                    _currentController = CurrentControllerType.Xbox;
                    controllerTypeChange.Invoke();
                    */
                }
                break;
               
            //Device disconnected
            case InputDeviceChange.Disconnected:
            /*
                controllerDisconnected.Invoke();
                _currentController = CurrentControllerType.Other;
                */
                Debug.Log("Device disconnected");
                break;
            
            //Familiar device connected
            case InputDeviceChange.Reconnected:
            /*
                controllerReconnected.Invoke();
                */
                Debug.Log("Device reconnected");
                
                //Checks if is Playstation Controller
                if (device.description.manufacturer == "Sony Interactive Entertainment")
                {
                    //Sets UI scheme
                    Debug.Log("Playstation Controller Detected");
                    /*
                    currentImageScheme.SetImagesToPlaystation();
                    _currentController = CurrentControllerType.PlayStation;
                    controllerTypeChange.Invoke();
                    */
                }
                //Else, assumes Xbox controller
                //device.description.manufacturer for Xbox returns empty string
                else if(device.description.manufacturer != "Sony Interactive Entertainment")
                {
                    Debug.Log("Xbox Controller Detected");
                    /*
                    currentImageScheme.SetImagesToXbox();
                    _currentController = CurrentControllerType.Xbox;
                    controllerTypeChange.Invoke();
                    */
                }
                break;
                
            //Else
            default:
                break;
        }
    }

}
