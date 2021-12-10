using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public Vector2 dir;
    public float rotationSpeed = 10;
    public Vector3 targetDir;

    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardDirection();
    }

    //Rotate the players towards his facing direction
    private void RotateTowardDirection()
    {
        if (dir != Vector2.zero)
        {
            targetDir = cam.transform.forward *  dir.y;
            targetDir += cam.transform.right * dir.x;
            targetDir.y = 0;
            targetDir.Normalize();
            Debug.DrawRay(transform.position, targetDir, Color.green);
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(GetComponent<Animator>().rootRotation, tr, Time.deltaTime * rotationSpeed);
            transform.rotation = targetRotation;
        } 
    }
}
