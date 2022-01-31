using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneHandler : MonoBehaviour
{
    public List<CheckpointsGuard> listGuard;

    private void OnTriggerEnter(Collider other)
    {
        if(listGuard.Count != 0)
            if (other.CompareTag("Player"))
                foreach(CheckpointsGuard guard in listGuard)
                    guard.ZoneColliderAlert(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (listGuard.Count != 0)
            if (other.CompareTag("Player"))
                foreach (CheckpointsGuard guard in listGuard)
                    guard.ZoneColliderAlert(false);
    }
}
