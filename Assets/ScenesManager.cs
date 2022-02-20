using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    [SerializeField]
    private int _nbEnnemies;
    [SerializeField]
    private bool _isChunkDeliver;

    private bool _launchPortalOverture = false;


    private void Update()
    {
        if(_nbEnnemies == 0 && !_launchPortalOverture)
        {

        }
    }

    private void OpenPortal()
    {
        // get portal anim controller
    }
}
