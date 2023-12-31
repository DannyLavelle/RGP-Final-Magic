using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent (typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    //I recommend 7 for the move speed, and 1.2 for the force damping
    InputAction Movement,Aiming;
    public Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 forceToApply;
    public Vector2 MoveInput;
    public float forceDamping;
    public Camera Cam;
    private Vector2 AimPos;
    public GameObject pointArrow;
    private PlayersInput playerControls;
    private PlayerInput playerInput;
    private bool isGamepad;
    public float ControllerDeadzone = 0.1f;
    public float rotationSmoothing= 1000f;
    private void Awake()
    {
        playerControls = new PlayersInput();
        playerInput = new PlayerInput();
    }
    private void Start()
    {
        //GameManager.InputManager.inputActions.Player.Enable();
        ////Cam = Camera.main;
        //GameManager.InputManager.inputActions.Player.Movement.Enable();
        ////Debug.Log(GameManager.InputManager.inputActions.Player.Movement);
        //GameManager.InputManager.inputActions.Player.Aiming.Enable();
    }
    private void OnEnable()
    {
      playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    void Update()
    {
        ManageInput();
        //Debug.Log("Movement Action: " + GameManager.InputManager.inputActions.Player.Movement);
        //Debug.Log(GameManager.InputManager.inputActions.Player.Movement);
        //MoveInput = Movement.ReadValue<Vector2>();
        GetAim();
    }
    void FixedUpdate()
    {
        GetMovement();
    }


    void GetAim()
    {
        if(isGamepad)
        {
            if(Mathf.Abs(AimPos.x) > ControllerDeadzone)
            {
                //Vector2 aimDirection = Vector2.right*AimPos.x + Vector2.up*AimPos.y;
                //Vector2 aimDirection = new Vector2(AimPos.x, AimPos.y).normalized;
                Vector2 aimDirection = new Vector2(AimPos.x, AimPos.y).normalized;
                if (aimDirection.sqrMagnitude >0.0f)
                {
                    float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg -90f;
                    Quaternion newrotation = Quaternion.Euler(0f,0f,(angle+90f));
                    //pointArrow.transform.rotation = Quaternion.RotateTowards(pointArrow.transform.rotation, newrotation, rotationSmoothing*Time.deltaTime);
                    rb.rotation = angle;
                }
            }
        }
        else
        {
            //Vector2 aimDirection = Vector2.right*AimPos.x + Vector2.up*AimPos.y;
            Vector2 aimDirection = new Vector2(AimPos.x, AimPos.y).normalized;
            if (aimDirection.sqrMagnitude > 0.0f)
            {
                float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
                Quaternion newrotation = Quaternion.Euler(0f, 0f, angle);
                //pointArrow.transform.rotation = Quaternion.RotateTowards(pointArrow.transform.rotation, newrotation, rotationSmoothing * Time.deltaTime);
                rb.rotation = angle;

            }
        }
        
       

    }
    void GetMovement()
    {
        Vector2 moveForce = MoveInput * moveSpeed;
        moveForce += forceToApply;
        forceToApply /= forceDamping;
        if (Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
        {
            forceToApply = Vector2.zero;
        }
        rb.velocity = moveForce;
    }
    void ManageInput()
    {
        MoveInput = playerControls.Player.Movement.ReadValue<Vector2>();

        if (isGamepad)
        {
            //Debug.Log(playerControls.Player.Aiming.ReadValue<Vector2>());
            AimPos = playerControls.Player.Aiming.ReadValue<Vector2>();
        }
        else
        {
            //Debug.Log(Cam.ScreenToWorldPoint(playerControls.Player.Aiming.ReadValue<Vector2>()));
            AimPos = Cam.ScreenToWorldPoint(playerControls.Player.Aiming.ReadValue<Vector2>()) - transform.position;

        }
    }

    public void OnDeviceChange(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Controller")? true:false;
        //Debug.Log("Change " + isGamepad);
       
    }
}
