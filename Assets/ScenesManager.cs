using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    [SerializeField]
    private int _nbEnnemies;
    [SerializeField]
    private bool _isChunkDeliver;

    [SerializeField]
    public Animator PortalAnimator;
    private bool _hasPortalBeenLaunched = false;



    private void Update()
    {
        if(_nbEnnemies == 0 && !_hasPortalBeenLaunched)
        {
            OpenPortal();
            _hasPortalBeenLaunched = true;
        }
    }

    private void OpenPortal()
    {
        // get portal anim controller
    }

    public void ChunkDelivering()
    {
        if (_isChunkDeliver)
        {

        }
    }
}
