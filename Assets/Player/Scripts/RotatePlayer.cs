using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public Vector2 dir;
    public float rotationSpeed = 10;
    public float animationSpeed = 0;
    public Vector3 targetDir;
    public GameObject middlePoint;
    public GameObject middlePoint2;

    private Transform parent;
    private CharacterMovement CM;
    private CameraController CamController;
    private Animator anim;

    public bool LockedOn;

    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {

        parent = GetComponentsInParent<Transform>()[1];
        CM = parent.GetComponent<CharacterMovement>();
        CamController = parent.GetComponent<CameraController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAnimatorMove()
    {
        if(CM.isRootMotionActive)
            CM.OnAnimatorMove();
    }

    private void FixedUpdate()
    {
        RotateTowardDirection();
        anim.SetFloat("attackAnimationSpeed", animationSpeed);
    }

    //Rotate the players towards his facing direction
    private void RotateTowardDirection()
    {

        if (LockedOn)
        {
            if (CamController.LockOnCamera2.activeInHierarchy)
            {
                middlePoint2.transform.position = new Vector3((CamController.LockStartPoint.transform.position.x + CamController.lockedEnemy.transform.position.x) / 2, (CamController.LockStartPoint.transform.position.y + CamController.lockedEnemy.transform.position.y) / 2, (CamController.LockStartPoint.transform.position.z + CamController.lockedEnemy.transform.position.z) / 2);
            }
            else
            {
                middlePoint.transform.position = new Vector3((CamController.LockStartPoint.transform.position.x + CamController.lockedEnemy.transform.position.x) / 2, (CamController.LockStartPoint.transform.position.y + CamController.lockedEnemy.transform.position.y) / 2, (CamController.LockStartPoint.transform.position.z + CamController.lockedEnemy.transform.position.z) / 2);
            }
            
            Vector3 dir = CamController.lockedEnemy.transform.position - parent.transform.position;          
            dir.Normalize();
            dir.y = 0;
            parent.transform.rotation = Quaternion.LookRotation(dir);
            return;
        }

        if (dir != Vector2.zero && !CM.isActing)
        {
            targetDir = cam.transform.forward * dir.y;
            targetDir += cam.transform.right * dir.x;
            targetDir.y = 0;
            targetDir.Normalize();
            Debug.DrawRay(transform.position, targetDir, Color.green);
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(GetComponent<Animator>().rootRotation, tr, Time.deltaTime * rotationSpeed);
            parent.transform.rotation = targetRotation;
        } 
    }
}
