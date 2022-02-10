using System.Collections;
using UnityEngine;

public class PlayerDetector : MonoBehaviour // NOTE : Does not handle multiple beast entering/exiting
{
    public bool PlayerInRange => detectedPlayer != null;
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
            detectedPlayer = null;
        }
    }

    public Transform GetPlayerTranform()
    {
        return detectedPlayer.transform;
    }

    /*public Vector3 GetNearestBeastPosition()
    {
        return _detectedBeast?.transform.position ?? Vector3.zero;
    }*/
}