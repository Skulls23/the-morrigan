using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public Vector2 dir;
    private Vector2 currentDirection;
    public float rotationSpeed = 10;
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
            currentDirection = dir;
            Quaternion tr = Quaternion.LookRotation(new Vector3(currentDirection.x, 0, currentDirection.y));
            Quaternion targetRotation = Quaternion.Slerp(GetComponent<Animator>().rootRotation, tr, Time.deltaTime * rotationSpeed);
            transform.rotation = targetRotation;
        } 
    }
}
