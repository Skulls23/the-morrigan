using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinematicCameraManager : MonoBehaviour
{
    public GameObject Timeline;
    public bool HasStart = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Timeline.SetActive(true);
            HasStart = true;
        }
    }
}
