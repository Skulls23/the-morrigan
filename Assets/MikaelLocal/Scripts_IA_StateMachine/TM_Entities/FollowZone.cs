using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowZone : MonoBehaviour
{
    public bool PlayerInZone => detectedPlayer != null;
    public float TimeToWaitBeforeReset;
    private GameObject detectedPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterMovement>())
        {
            detectedPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterMovement>())
        {
            StartCoroutine(ClearDetectedPlayerAfterDelay());
        }
    }

    private IEnumerator ClearDetectedPlayerAfterDelay()
    {
        yield return new WaitForSeconds(TimeToWaitBeforeReset);
        detectedPlayer = null;
    }
}
