using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;

    bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && isAttacking)
        {
            isAttacking = false;
        }
    }

    public void Attack()
    {
        if(!isAttacking)
        {
            anim.ResetTrigger("Attack");
            anim.SetTrigger("Attack");
            isAttacking = true;
        }
    }

    public void SetDirection(int direction)
    {
        anim.SetInteger("Direction", direction);
    }
}
