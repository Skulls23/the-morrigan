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
    Vector2 animValue;
    [SerializeField]
    float currentSpeed;

    [Header("Speeds")]
    [Header("GAME DESIGN")]
    
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float jogSpeed;
    [SerializeField]
    private float runSpeed;

    [Header("Transitions")]
    [SerializeField, Range(0,1)]
    private float startWalkingValue;
    [SerializeField, Range(0, 1)]
    private float startJogingValue;
    [SerializeField]
    private float animationTransitionTime;


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
        ApplyMovement();
    }

    //Event getting the values on controller left joystic, keyboard arrows and WASD
    public void OnMove(InputAction.CallbackContext context)
    {
        movementValue = CheckInput(context.ReadValue<Vector2>());
        direction = movementValue.normalized;
        rP.dir = direction;
        animValue = ConvertMoveToAnimValues(movementValue);
        currentSpeed = GetSpeedFromAnimValue(animValue);
    }

    //Updates the animator movement layer and the player velocity
    private void ApplyMovement()
    {
        anim.SetFloat(HashTable.moveH, animValue.x <= animValue.y ? animValue.y : animValue.x, animationTransitionTime, Time.fixedDeltaTime);
        rb.velocity = new Vector3(direction.x * currentSpeed, rb.velocity.y, direction.y * currentSpeed);
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

        if (tempAnimValue.x > startWalkingValue)
            if (tempAnimValue.x > startJogingValue) tempAnimValue.x = 1;
            else tempAnimValue.x = 0.5f;

        if (tempAnimValue.y > startWalkingValue)
            if (tempAnimValue.y > startJogingValue) tempAnimValue.y = 1;
            else tempAnimValue.y = 0.5f;

        return tempAnimValue;
    }

    private float GetSpeedFromAnimValue(Vector2 animValue)
    {
        if (animValue.x == 1 || animValue.y == 1)
            return jogSpeed;
        if (animValue.x == 0.5f || animValue.y == 0.5f)
            return walkSpeed;
        return 0;
    }
}
