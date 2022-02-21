using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    [SerializeField]
    public int _nbEnnemies;
    [SerializeField]
    private bool _isChunkDeliver;

    [SerializeField]
    public Animator PortalAnimator;
    private bool _hasPortalBeenLaunched = false;

    [SerializeField]
    private int _nextSceneIndex;

    private GameManager GM;

    public void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }



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
        PortalAnimator.SetBool("LaunchDoor", true);
    }

    public void ChunkDelivering()
    {
        if (_isChunkDeliver)
        {
            // chunk management
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GM.LoadScene(_nextSceneIndex);
        }
    }
}
