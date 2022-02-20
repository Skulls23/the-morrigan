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
    private Player player;

    public Transform GroundRayStart;
    public UI_Player_Stats_Manager UIPSManager;

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
    public bool isAttacking;
    [SerializeField]
    public bool isLockedOn;
    private Vector3 ResetMoveVelocity;
    [SerializeField]
    private bool isGrounded = true;
    private bool isFalling = false;
    public LayerMask GroundMask;

    [SerializeField]
    public bool canMove = true;
    [SerializeField]
    public bool canRotate = true;

    public bool isRootMotionActive;


    private float FallTimeTimer;

    [Header("CustomLockedMovements")]
    [SerializeField]
    private float transitionSpeed;

    public float timeRotateCam = 1;
    public float timerRotateCam = 0;
    private float lastDirX;
    public Transform transFollow;
    public Transform startMovingTrans;


    // Start is called before the first frame update
    void Start()
    {
        HashTable.Init();
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        rP = GetComponentInChildren<RotatePlayer>();
        SM = GetComponent<StaminaManager>();
        player = GetComponent<Player>();

        //GroundRay
        //GroundRayStart.localPosition = new Vector3(0, 0, -GetComponent<CapsuleCollider>().radius);

        ResetMoveVelocity = new Vector3(0, rb.velocity.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        isRootMotionActive = anim.applyRootMotion;
        LockedCustomMovements();
    }

    private void LateUpdate()
    {
        
    }

    private void LockedCustomMovements()
    {
        //Cam Logic
        if (lastDirX > 0 && movementValue.x <= 0 || lastDirX < 0 && movementValue.x >= 0 || rb.velocity == Vector3.zero)
        {
            transFollow = startMovingTrans;

            if (GetComponent<CameraController>().lockedEnemy != null)
            {
                startMovingTrans.LookAt(GetComponent<CameraController>().lockedEnemy.transform);
            }
            timerRotateCam = 0;
        }

        lastDirX = direction.x;

        if (timerRotateCam > timeRotateCam && canMove)
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
        if (canMove && !isFalling)
        {
            ApplyMovement();
        }

        CheckGround();
        ResetMoveVelocity = new Vector3(0, rb.velocity.y, 0);
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

        if (context.started && SM.HasEnoughStamina(player.MinStaminaToRestartRunning))
        {
            SM.canRun = true;
        }

        if (SM.canRun)
        {
            anim.SetBool("isRunning", runInput);
        }
        else
        {
            if(anim.GetBool("isRunning"))
                anim.SetBool("isRunning", false);
        }
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (context.performed && canMove && SM.HasEnoughStamina(player.DashStaminaCost))
        {
            
            Debug.Log("Started");
            //GetInput
            dodgeInput = context.performed;

            //Set Values
            canRotate = false;
            canMove = false;
            anim.applyRootMotion = true;
            anim.SetTrigger(HashTable.dodged);
            anim.SetBool("isDodging", true);
            SM.UseStamina(player.DashStaminaCost);
            
            //
            timerRotateCam = 0;
            transFollow = startMovingTrans;
            startMovingTrans.position = transform.position;
            if (GetComponent<CameraController>().lockedEnemy)
            {
                startMovingTrans.LookAt(GetComponent<CameraController>().lockedEnemy.transform);
            }
            //

            Vector3 dashDir = new Vector3(direction.x, 0, direction.y);
            
            if (isLockedOn)
            {
                anim.SetFloat(HashTable.dirX, movementValue.x, 0, Time.fixedDeltaTime);
                anim.SetFloat(HashTable.dirZ, movementValue.y, 0, Time.fixedDeltaTime);
            }
            else
            {
                //transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
                anim.SetFloat(HashTable.dirX, 0, 0, Time.fixedDeltaTime);
                anim.SetFloat(HashTable.dirZ, 1, 0, Time.fixedDeltaTime);
            }

            StopCoroutine("IELockMovementTimer");
            StartCoroutine(IELockMovementTimer(player.DashLockMovementTime));

            //TO DO Multiplicator of speed for GD purpose
        }
        
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            anim.SetBool("hasAttacked", true);
            if (SM.HasEnoughStamina(player.AttackStaminaCost))
            {
                if (canMove)
                {
                    canMove = false;
                    isAttacking = true;
                    attackInput = context.performed;
                    rb.velocity = ResetMoveVelocity;
                    anim.SetTrigger(HashTable.attacked);
                    StartCoroutine(AttackRoutine(player.SteeringTime));
                }
            }
        }
    }

    IEnumerator AttackRoutine(float time)
    {
        yield return new WaitForSeconds(player.SteeringTime);
        rb.velocity = ResetMoveVelocity;
        anim.applyRootMotion = true;
        canRotate = false;
        StartCoroutine(IELockMovementTimer(player.AttackLockMovementTime));
    }


    IEnumerator IELockMovementTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("can Move Again !");
        anim.applyRootMotion = false;
        canMove = true;
        canRotate = true;
        isAttacking = false;
        anim.SetBool("isDodging", false);
        anim.SetBool("hasAttacked", false);
    }

    //Updates the animator movement layer and the player velocity
    private void ApplyMovement()
    {
        Vector3 targetVelocity = ResetMoveVelocity;

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

        targetVelocity.y = rb.velocity.y;
        rb.velocity = targetVelocity;
    }

    private void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(GroundRayStart.position, Vector3.down, out hit, player.RayLength, GroundMask))
        {
            Debug.DrawLine(GroundRayStart.position, hit.point, Color.red, 1);
            if (hit.distance <= player.StartGroundingDistance)
            {
                if(((hit.normal.x >= player.StartSlopingAngleDifference || hit.normal.z >= player.StartSlopingAngleDifference) || (hit.normal.x <= -player.StartSlopingAngleDifference || hit.normal.z <= -player.StartSlopingAngleDifference)) && rb.velocity.y <= player.StartAddingForceOnSlopeYVelocity)
                {
                    rb.AddForce(-transform.up * player.SlopeForce * Time.deltaTime, ForceMode.VelocityChange);
                }
                if(isGrounded == false)
                {
                    canRotate = true;
                    canMove = true;
                }
                isGrounded = true;
                isFalling = false;
                return;
            }
            else
            {
                if(isFalling)
                    rb.velocity = new Vector3(0, rb.velocity.y, 0);

                if (hit.distance >= player.StartFallingDistance)
                {
                    if (rb.velocity.y <= -player.StartFallingYVelocity)
                    {
                        FallTimeTimer += Time.deltaTime;
                        if (FallTimeTimer > player.StartFallingAfterXSecondsOnAir && !isFalling)
                        {
                            isGrounded = false;
                            canRotate = false;
                            canMove = false;
                            isFalling = true;
                            rb.velocity = new Vector3(0, rb.velocity.y, 0);
                            return;
                        }
                    }
                }
                else
                {
                    FallTimeTimer = 0;
                }

                rb.AddForce(-transform.up * player.SlopeForce * Time.deltaTime, ForceMode.VelocityChange);
            }
        } 
    }

    private Vector2 CheckInput(Vector2 moveValue)
    {
        if (Mathf.Abs(moveValue.x) < player.StartWalkingValue && Mathf.Abs(moveValue.y) < player.StartWalkingValue)
            return Vector2.zero;
        return moveValue;
    }

    //Round values for game design purpose
    private Vector2 ConvertMoveToAnimValues(Vector2 moveValue)
    {
        float xSign = Mathf.Sign(moveValue.x);
        float ySign = Mathf.Sign(moveValue.y);
        Vector2 tempAnimValue = new Vector2(Mathf.Abs(moveValue.x), Mathf.Abs(moveValue.y));

        if (runInput && tempAnimValue != Vector2.zero && SM.canRun)
        {
            isRunning = true;
            tempAnimValue.x = 1.5f;
        }
        else
        {
            isRunning = false;
            if (tempAnimValue.x > player.StartWalkingValue)
                if (tempAnimValue.x > player.StartJogingValue) tempAnimValue.x = 1;
                else tempAnimValue.x = 0.5f;

            if (tempAnimValue.y > player.StartWalkingValue)
                if (tempAnimValue.y > player.StartJogingValue) tempAnimValue.y = 1;
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
            return player.RunSpeed * transform.localScale.x;
        if (isLockedOn)
        {
            if (animValue == 1)
                return player.StrafeJogSpeed * transform.localScale.x;
            if (animValue == 0.5f)
                return player.StrafeWalkSpeed * transform.localScale.x;
        }
        else
        {
            if (animValue == 1)
                return player.JogSpeed * transform.localScale.x;
            if (animValue == 0.5f)
                return player.WalkSpeed * transform.localScale.x;
        }
        
        return 0;
    }

    public bool GetIsRunning()
    {
        return isRunning;
    }

    public bool GetIsFalling()
    {
        return isFalling;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AttackCollider")
        {
            GetHit();
            UIPSManager.PlayerGetHit();
        }
    }

    void GetHit()
    {
        canMove = false;
        canRotate = false;
        rb.velocity = ResetMoveVelocity;
        anim.SetTrigger("getHit");
        StartCoroutine(IELockMovementTimer(player.DamageLockMovementTimer));
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
