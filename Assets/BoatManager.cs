using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    public Transform Target;
    public float speed = 2f;
    public CinematicCameraManager CCM;

    // Update is called once per frame
    void Update()
    {
        if(CCM.HasStart)
            transform.position = Vector3.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
    }
}
