using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("COMPONENTS")]
    private Rigidbody rb;
    private Animator anim;
    private RotatePlayer rP;
    public Transform cameraFocus;
    
    [Header("VALUES")]
    public Vector2 movementValue;
    public Vector2 animationMovementValue;
    private Vector2 direction;
    private float animValue;
    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private bool runInput;
    [SerializeField]
    private bool isRunning;
    [SerializeField]
    private bool isLockedOn;

    [Header("Speeds")]
    [Header("GAME DESIGN")]
    
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float jogSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField, Range(0, 1)]
    private float startWalkingValue;
    [SerializeField, Range(0, 1)]
    private float startJogingValue;

    [SerializeField]
    private float rayLength;
    [SerializeField]
    private float slopeForce;

    [Header("Transitions")]
    [SerializeField]
    private float transitionSpeed;


    // Start is called before the first frame update
    void Start()
    {
        HashTable.Init();
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        rP = GetComponentInChildren<RotatePlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        animationMovementValue = ConvertMoveToAnimValues(movementValue);
        animValue = getGreaterAnimValue(animationMovementValue);
        currentSpeed = GetSpeedFromAnimValue(animValue);
        ApplyMovement();
    }

    //Event getting the values on controller left joystic, keyboard arrows and WASD
    public void OnMove(InputAction.CallbackContext context)
    {
        movementValue = CheckInput(context.ReadValue<Vector2>());
        direction = movementValue.normalized;
        rP.dir = direction;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        runInput = context.performed;
    }

    //Updates the animator movement layer and the player velocity
    private void ApplyMovement()
    {
        if (isLockedOn)
        {
            anim.SetFloat(HashTable.moveV, animationMovementValue.x, transitionSpeed, Time.fixedDeltaTime);
            anim.SetFloat(HashTable.moveH, animationMovementValue.y, transitionSpeed, Time.fixedDeltaTime);
        }
        else
        {
            anim.SetFloat(HashTable.moveV, animValue, transitionSpeed, Time.fixedDeltaTime);
            anim.SetFloat(HashTable.moveH, 0, 0, Time.fixedDeltaTime);
        }
        
        Vector3 targetVelocity = Vector3.zero;

        if (isLockedOn)
        {
            targetVelocity = rP.transform.forward * direction.y * currentSpeed;
            targetVelocity += rP.transform.right * direction.x * currentSpeed;
        }
        else
        {
            targetVelocity = rP.transform.forward * currentSpeed;
        }

        rb.velocity = targetVelocity;

        /*if (OnSlope())
        {
            rb.velocity = new Vector3(rb.velocity.x,SlopeManagement(),rb.velocity.z);
        }*/
    }

    private bool OnSlope()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.8f / 2 * rayLength))
            if (hit.normal != Vector3.up)
                return true;
        return false;
    }

    private float SlopeManagement()
    {
        if (currentSpeed!=0 && OnSlope())
        {
            return -1.8f / 2 * slopeForce * Time.deltaTime;
        }
        return rb.velocity.y;
    }

    private Vector2 CheckInput(Vector2 moveValue)
    {
        if (Mathf.Abs(moveValue.x) < startWalkingValue && Mathf.Abs(moveValue.y) < startWalkingValue)
            return Vector2.zero;
        return moveValue;
    }

    //Round values for game design purpose
    private Vector2 ConvertMoveToAnimValues(Vector2 moveValue)
    {
        Vector2 tempAnimValue = new Vector2(Mathf.Abs(moveValue.x), Mathf.Abs(moveValue.y));

        if (runInput && tempAnimValue != Vector2.zero)
        {
            isRunning = true;
            tempAnimValue.x = 1.5f;
        }
        else
        {
            isRunning = false;
            if (tempAnimValue.x > startWalkingValue)
                if (tempAnimValue.x > startJogingValue) tempAnimValue.x = 1;
                else tempAnimValue.x = 0.5f;

            if (tempAnimValue.y > startWalkingValue)
                if (tempAnimValue.y > startJogingValue) tempAnimValue.y = 1;
                else tempAnimValue.y = 0.5f;
        }
        return tempAnimValue;
    }

    private float getGreaterAnimValue(Vector2 animValue)
    {
        if (isRunning)
        {
            return 1.5f;
        }
        if (animValue.y >= animValue.x)
            return animValue.y;
        else
            return animValue.x;
    }

    private float GetSpeedFromAnimValue(float animValue)
    {
        if (animValue == 1.5f)
            return runSpeed * transform.localScale.x;
        if (animValue == 1)
            return jogSpeed * transform.localScale.x;
        if (animValue == 0.5f)
            return walkSpeed * transform.localScale.x;
        return 0;
    }
}
