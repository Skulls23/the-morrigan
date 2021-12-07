using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneHandler : MonoBehaviour
{
    public List<CheckpointsGuard> listGuard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(CheckpointsGuard guard in listGuard)
            {
                guard.zoneColliderAlert(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (CheckpointsGuard guard in listGuard)
            {
                guard.zoneColliderAlert(false);
            }
        }
    }
}
