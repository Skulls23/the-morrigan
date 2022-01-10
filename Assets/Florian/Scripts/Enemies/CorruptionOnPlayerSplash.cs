using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionOnPlayerSplash : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float delayBetweenCorruption;

    private bool CorruptionOverTime = false;

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
            CorruptionOverTime = true;
            InvokeRepeating("TakeCorruptionOverTime", 0f, delayBetweenCorruption);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CorruptionOverTime = false;
    }

    private void TakeCorruptionOverTime()
    {
        if (CorruptionOverTime == true)
        {
            player.GetComponent<Health>().addCorruptedHealth();
        }
    }
}
