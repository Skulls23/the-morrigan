using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPopup : MonoBehaviour
{
    [SerializeField]
    private GameObject _popupSprite;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _popupSprite.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _popupSprite.SetActive(false);
        }
    }
}
