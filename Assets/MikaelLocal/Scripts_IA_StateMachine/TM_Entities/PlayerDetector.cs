using System.Collections;
using UnityEngine;

public class PlayerDetector : MonoBehaviour // NOTE : Does not handle multiple beast entering/exiting
{
    public bool PlayerInRange => detectedPlayer != null;

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
            StartCoroutine(ClearDetectedBeastAfterDelay());
        }
    }

    private IEnumerator ClearDetectedBeastAfterDelay()
    {
        yield return new WaitForSeconds(TimeToWaitBeforeReset);
        detectedPlayer = null;
    }

    public Vector3 GetPlayerPosition()
    {
        return detectedPlayer.transform.position;
    }

    /*public Vector3 GetNearestBeastPosition()
    {
        return _detectedBeast?.transform.position ?? Vector3.zero;
    }*/
}