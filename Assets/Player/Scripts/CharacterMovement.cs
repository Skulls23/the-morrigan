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
    private StaminaManager SM;

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
    private bool dodgeInput;
    [SerializeField]
    private bool attackInput;
    [SerializeField]
    private bool isRunning;
    [SerializeField]
    public bool isLockedOn;
    [SerializeField]
    public bool isActing;

    [Header("Speeds")]
    [Header("GAME DESIGN")]
    
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float jogSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float strafeWalkSpeed;
    [SerializeField]
    private float strafeJogSpeed;
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashLockMovementTime;
    [SerializeField]
    private float attackLockMovementTime;
    [SerializeField, Range(0, 1)]
    private float startWalkingValue;
    [SerializeField, Range(0, 1)]
    private float startJogingValue;

    [SerializeField]
    private float rayLength;
    [SerializeField]
    private float slopeForce;

    public bool isRootMotionActive;

    

    [Header("Transitions")]
    [SerializeField]
    private float transitionSpeed;

    public float timeRotateCam = 1;
    public float timerRotateCam = 0;
    private float lastDirX;
    public Transform transFollow;
    public Transform startMovingTrans;

    [Header("Stamina Costs")]
    [SerializeField]
    private float dodgeStaminaCost;
    [SerializeField]
    private float attackStaminaCost;


    // Start is called before the first frame update
    void Start()
    {
        HashTable.Init();
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        rP = GetComponentInChildren<RotatePlayer>();
        SM = GetComponent<StaminaManager>();
    }

    // Update is called once per frame
    void Update()
    {
        isRootMotionActive = anim.applyRootMotion;
        
        //Cam Logic
        if (lastDirX > 0 && movementValue.x <= 0 || lastDirX < 0 && movementValue.x >= 0 || rb.velocity == Vector3.zero)
        {
            transFollow = startMovingTrans;
            
            if(GetComponent<CameraController>().lockedEnemy != null)
            {
                startMovingTrans.LookAt(GetComponent<CameraController>().lockedEnemy.transform);
            }
            timerRotateCam = 0;
        }

        lastDirX = direction.x;

        if (timerRotateCam > timeRotateCam && !isActing)
        {
            transFollow = rP.transform;
        }
        else
        {
            startMovingTrans.position = transform.position;
            transFollow = startMovingTrans;
        }
        timerRotateCam += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        animationMovementValue = ConvertMoveToAnimValues(movementValue);
        animValue = getGreaterAnimValue(animationMovementValue);
        currentSpeed = GetSpeedFromAnimValue(animValue);
        if (!isActing)
        {
            ApplyMovement();
        }
    }

    public void OnAnimatorMove()
    {
        transform.position += anim.deltaPosition;
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
        anim.SetBool("isRunning", runInput);
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (context.performed && !isActing && SM.UseStamina(dodgeStaminaCost))
        {
            //
            timerRotateCam = 0;
            transFollow = startMovingTrans;
            startMovingTrans.position = transform.position;
            startMovingTrans.LookAt(GetComponent<CameraController>().lockedEnemy.transform);
            //

            Vector3 dashDir = new Vector3(direction.x, 0, direction.y);
            anim.applyRootMotion = true;
            isActing = true;
            dodgeInput = context.performed;
            anim.SetTrigger(HashTable.dodged);
            anim.SetBool("isDodging", true);
            if (isLockedOn)
            {
                anim.SetFloat(HashTable.dirX, movementValue.x, 0, Time.fixedDeltaTime);
                anim.SetFloat(HashTable.dirZ, movementValue.y, 0, Time.fixedDeltaTime);
            }
            else
            {
                anim.SetFloat(HashTable.dirX, 0, 0, Time.fixedDeltaTime);
                anim.SetFloat(HashTable.dirZ, 1, 0, Time.fixedDeltaTime);
            }
            
            StartCoroutine(IELockMovementTimer(dashLockMovementTime));

            //TO DO Multiplicator of speed for GD purpose
        }
        
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            anim.SetBool("hasAttacked", true);
            if (SM.UseStamina(attackStaminaCost))
            {
                if (!isActing)
                {
                    rb.velocity = Vector3.zero;
                    anim.applyRootMotion = true;
                    isActing = true;
                    attackInput = context.performed;
                    anim.SetTrigger(HashTable.attacked);
                    StartCoroutine(IELockMovementTimer(attackLockMovementTime));
                }
            }
        }
    }

    IEnumerator IELockMovementTimer(float time)
    {
        yield return new WaitForSeconds(time);
        anim.applyRootMotion = false;
        isActing = false;
        anim.SetBool("isDodging", false);
        anim.SetBool("hasAttacked", false);
    }

    //Updates the animator movement layer and the player velocity
    private void ApplyMovement()
    {
        Vector3 targetVelocity = Vector3.zero;

        if (isLockedOn && !isRunning)
        {
            anim.SetFloat(HashTable.moveV, animationMovementValue.y, transitionSpeed, Time.fixedDeltaTime);
            anim.SetFloat(HashTable.moveH, animationMovementValue.x, transitionSpeed, Time.fixedDeltaTime);
            targetVelocity = rP.transform.forward * direction.y * currentSpeed;
            targetVelocity += transFollow.right * direction.x * currentSpeed;
        }
        else
        {
            anim.SetFloat(HashTable.moveV, animValue, transitionSpeed, Time.fixedDeltaTime);
            anim.SetFloat(HashTable.moveH, 0, 0, Time.fixedDeltaTime);
            targetVelocity = rP.transform.forward * currentSpeed;
        }

        rb.velocity = targetVelocity;

        if (OnSlope())
        {
            rb.velocity = new Vector3(rb.velocity.x,SlopeManagement(),rb.velocity.z);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
    }

    private bool OnSlope()
    {
        RaycastHit hit;
        //camera focus
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.8f / 2 * rayLength))
            if (hit.normal != Vector3.up && hit.distance > 4.1f)
                return true;
        return false;
    }

    private float SlopeManagement()
    {
        return -slopeForce;
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
        float xSign = Mathf.Sign(moveValue.x);
        float ySign = Mathf.Sign(moveValue.y);
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

        if (isLockedOn)
        {
            tempAnimValue.x *= xSign;
            tempAnimValue.y *= ySign;
        }

        return tempAnimValue;
    }  

    private float getGreaterAnimValue(Vector2 animValue)
    {
        animValue.x = Mathf.Abs(animValue.x);
        animValue.y = Mathf.Abs(animValue.y);
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
        if (isLockedOn)
        {
            if (animValue == 1)
                return strafeJogSpeed * transform.localScale.x;
            if (animValue == 0.5f)
                return strafeWalkSpeed * transform.localScale.x;
        }
        else
        {
            if (animValue == 1)
                return jogSpeed * transform.localScale.x;
            if (animValue == 0.5f)
                return walkSpeed * transform.localScale.x;
        }
        
        return 0;
    }

    public bool GetIsRunning()
    {
        return isRunning;
    }
}
