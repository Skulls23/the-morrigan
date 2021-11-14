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
    
    [Header("VALUES")]
    private Vector2 movementValue;
    private Vector2 direction;
    private float animValue;
    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private bool runInput;
    [SerializeField]
    private bool isRunning;

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
        animValue = ConvertMoveToAnimValues(movementValue);
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
        anim.SetFloat(HashTable.moveH, animValue, transitionSpeed, Time.fixedDeltaTime);
        rb.velocity = new Vector3(direction.x * currentSpeed, rb.velocity.y, direction.y * currentSpeed);
    }

    private Vector2 CheckInput(Vector2 moveValue)
    {
        if (Mathf.Abs(moveValue.x) < startWalkingValue && Mathf.Abs(moveValue.y) < startWalkingValue)
            return Vector2.zero;
        return moveValue;
    }

    //Round values for game design purpose
    private float ConvertMoveToAnimValues(Vector2 moveValue)
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
        float greaterValue = tempAnimValue.x <= tempAnimValue.y ? tempAnimValue.y : tempAnimValue.x;
        return greaterValue;
    }

    private float GetSpeedFromAnimValue(float animValue)
    {
        if (animValue == 1.5f)
            return runSpeed;
        if (animValue == 1)
            return jogSpeed;
        if (animValue == 0.5f)
            return walkSpeed;
        return 0;
    }
}
