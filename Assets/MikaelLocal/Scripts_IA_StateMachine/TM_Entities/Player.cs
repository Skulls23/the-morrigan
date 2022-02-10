using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("SPEEDS")]

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
    [SerializeField, Range(0, 1)]
    private float startWalkingValue;
    [SerializeField, Range(0, 1)]
    private float startJogingValue;

    [Header("COMBAT SYSTEM")]
    [Space(10)]
    [SerializeField]
    private float dashLockMovementTime;
    [SerializeField]
    private float attackLockMovementTime;
    [SerializeField]
    private float steeringTime;


    [Header("STAMINA MANAGEMENT")]
    [SerializeField]
    private float dodgeStaminaCost;
    [SerializeField]
    private float attackStaminaCost;

    [Header("SLOPES")]
    [SerializeField]
    private float rayLength;
    [SerializeField]
    private float slopeForce;


    public float WalkSpeed { get => walkSpeed; set => walkSpeed = value; }
    public float JogSpeed { get => jogSpeed; set => jogSpeed = value; }
    public float RunSpeed { get => runSpeed; set => runSpeed = value; }
    public float StrafeWalkSpeed { get => strafeWalkSpeed; set => strafeWalkSpeed = value; }
    public float StrafeJogSpeed { get => strafeJogSpeed; set => strafeJogSpeed = value; }
    public float DashSpeed { get => dashSpeed; set => dashSpeed = value; }
    public float DashLockMovementTime { get => dashLockMovementTime; set => dashLockMovementTime = value; }
    public float AttackLockMovementTime { get => attackLockMovementTime; set => attackLockMovementTime = value; }
    public float SteeringTime { get => steeringTime; set => steeringTime = value; }
    public float StartWalkingValue { get => startWalkingValue; set => startWalkingValue = value; }
    public float StartJogingValue { get => startJogingValue; set => startJogingValue = value; }
    public float RayLength { get => rayLength; set => rayLength = value; }
    public float SlopeForce { get => slopeForce; set => slopeForce = value; }
    public float DashStaminaCost { get => dodgeStaminaCost; set => dodgeStaminaCost = value; }
    public float AttackStaminaCost { get => attackStaminaCost; set => attackStaminaCost = value; }
}
