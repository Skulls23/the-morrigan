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
    [SerializeField]
    private float fallSpeed;

    [Header("COMBAT SYSTEM")]
    [Space(10)]
    [SerializeField]
    private float dashLockMovementTime;
    [SerializeField]
    private float attackLockMovementTime;
    [SerializeField]
    private float steeringTime;
    [SerializeField, Range(7, 30)]
    private float spearRange;

    [Header("Attack")]
    [SerializeField]
    private float damageOnFleshBase;
    [SerializeField]
    private float damageOnWeakpointBase;
    [SerializeField]
    private float damageOnFleshBravo;
    [SerializeField]
    private float damageOnWeakpointBravo;
    [SerializeField]
    private float damageOnFleshEcho;
    [SerializeField]
    private float damageOnWeakpointEcho;
    [SerializeField]
    private float damageOnFleshBravoAndEcho;
    [SerializeField]
    private float damageOnWeakpointBravoAndEcho;



    [Header("STAMINA MANAGEMENT")]
    [SerializeField, Range(0, 100)]
    private float staminaMax;
    [SerializeField]
    private float runStaminaPerSecond;
    [SerializeField]
    private float timeBeforeStartingRegenAgain;
    [SerializeField]
    private float staminaPerSecond;
    [SerializeField]
    private float minStaminaToRestartRunning;
    [SerializeField]
    private float dodgeStaminaCost;
    [SerializeField]
    private float attackStaminaCost;

    [Header("SLOPES")]
    [SerializeField]
    private float rayLength;
    [SerializeField]
    private float slopeForce;

    [SerializeField, Range(0, 3)]
    private float startGroundingDistance;
    [SerializeField, Range(0, 3)]
    private float startFallingDistance;
    [SerializeField, Range(-1, 0)]
    private float startAddingForceOnSlopeYVelocity;
    [SerializeField, Range(-3, 0)]
    private float startFallingYVelocity;
    [SerializeField, Range(0, 1)]
    private float startSlopingAngleDifference;
    [SerializeField, Range(0, 2)]
    private float startFallingAfterXSecondsOnAir;


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
    public float RunStaminaPerSecond { get => runStaminaPerSecond; set => runStaminaPerSecond = value; }
    public float TimeBeforeStartingRegenAgain { get => timeBeforeStartingRegenAgain; set => timeBeforeStartingRegenAgain = value; }
    public float StaminaPerSecond { get => staminaPerSecond; set => staminaPerSecond = value; }
    public float StaminaMax { get => staminaMax; set => staminaMax = value; }
    public float MinStaminaToRestartRunning { get => minStaminaToRestartRunning; set => minStaminaToRestartRunning = value; }
    public float FallSpeed { get => fallSpeed; set => fallSpeed = value; }
    public float StartGroundingDistance { get => startGroundingDistance; set => startGroundingDistance = value; }
    public float StartFallingDistance { get => startFallingDistance; set => startFallingDistance = value; }
    public float StartFallingYVelocity { get => startFallingYVelocity; set => startFallingYVelocity = value; }
    public float StartSlopingAngleDifference { get => startSlopingAngleDifference; set => startSlopingAngleDifference = value; }
    public float StartAddingForceOnSlopeYVelocity { get => startAddingForceOnSlopeYVelocity; set => startAddingForceOnSlopeYVelocity = value; }
    public float StartFallingAfterXSecondsOnAir { get => startFallingAfterXSecondsOnAir; set => startFallingAfterXSecondsOnAir = value; }
    public float SpearRange { get => spearRange; set => spearRange = value; }
    public float DamageOnFleshBase { get => damageOnFleshBase; set => damageOnFleshBase = value; }
    public float DamageOnWeakpointBase { get => damageOnWeakpointBase; set => damageOnWeakpointBase = value; }
    public float DamageOnFleshBravo { get => damageOnFleshBravo; set => damageOnFleshBravo = value; }
    public float DamageOnWeakpointBravo { get => damageOnWeakpointBravo; set => damageOnWeakpointBravo = value; }
    public float DamageOnFleshEcho { get => damageOnFleshEcho; set => damageOnFleshEcho = value; }
    public float DamageOnWeakpointEcho { get => damageOnWeakpointEcho; set => damageOnWeakpointEcho = value; }
    public float DamageOnFleshBravoAndEcho { get => damageOnFleshBravoAndEcho; set => damageOnFleshBravoAndEcho = value; }
    public float DamageOnWeakpointBravoAndEcho { get => damageOnWeakpointBravoAndEcho; set => damageOnWeakpointBravoAndEcho = value; }
}
