using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform parent;
    private MeleeEnemy meleeEnemy;
    private Animator animator;
    void Start()
    {
        parent = GetComponentInParent<Transform>();
        meleeEnemy = GetComponentInParent<MeleeEnemy>();
        animator = GetComponentInParent<Animator>();
    }


    public void startSteering()
    {
        meleeEnemy.isSteering = true;
    }

    public void stopSteering()
    {
        meleeEnemy.isSteering = false;
    }

    public void startAddingMovement()
    {
        meleeEnemy.isAddingMovement = true;
    }

    public void stopAddingMovement()
    {
        meleeEnemy.isAddingMovement = false;
    }

    private void IsAttacking()
    {
        meleeEnemy.SetIsAttacking(true);
    }

    private void IsNotAttackingAnymore()
    {
        meleeEnemy.SetIsAttacking(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAnimatorMove()
    {
        if (animator.applyRootMotion)
        {
            meleeEnemy.OnAnimatorMove();
        }
    }

}
