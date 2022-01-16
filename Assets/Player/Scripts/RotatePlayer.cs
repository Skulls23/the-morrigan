using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public Vector2 dir;
    public float rotationSpeed = 10;
    public Vector3 targetDir;
    public GameObject lockPoint;
    public GameObject lockPoint2;
    public GameObject middlePoint;
    public GameObject middlePoint2;

    private Transform parent;
    private CharacterMovement CM;
    private CameraController CamController;
    private Animator anim;
    public bool LockedOn;
    public bool ennemy2;

    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {

        parent = GetComponentsInParent<Transform>()[1];
        CM = parent.GetComponent<CharacterMovement>();
        CamController = parent.GetComponent<CameraController>();
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
    }

    //Rotate the players towards his facing direction
    private void RotateTowardDirection()
    {

        if (LockedOn)
        {
            middlePoint.transform.position = new Vector3((CamController.LockStartPoint.transform.position.x + lockPoint.transform.position.x) / 2, (CamController.LockStartPoint.transform.position.y + lockPoint.transform.position.y) / 2, (CamController.LockStartPoint.transform.position.z + lockPoint.transform.position.z) / 2);
            middlePoint2.transform.position = new Vector3((CamController.LockStartPoint.transform.position.x + lockPoint2.transform.position.x) / 2, (CamController.LockStartPoint.transform.position.y + lockPoint2.transform.position.y) / 2, (CamController.LockStartPoint.transform.position.z + lockPoint2.transform.position.z) / 2);
            Vector3 dir;
            if (!ennemy2)
            {
                dir = lockPoint.transform.position - parent.transform.position;
            }
            else
            {
                dir = lockPoint2.transform.position - parent.transform.position;
            }
            
            dir.Normalize();
            dir.y = 0;
            parent.transform.rotation = Quaternion.LookRotation(dir);
            return;
        }

        if (dir != Vector2.zero && !CM.isDodging)
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
