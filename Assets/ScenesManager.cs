using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameManager GM;

    [SerializeField]
    private Transform _spawnPoint;
    [SerializeField]
    private GameObject _ycPrefab;
    [SerializeField]
    private bool _isTesting;

    [SerializeField]
    private Text _sceneText;
    [SerializeField]
    private string _nbScene;

    public void Start()
    {
        _sceneText.text = _nbScene;
        
        if (_isTesting)
        {
            Instantiate(_ycPrefab, _spawnPoint.position, Quaternion.identity);
        }
        else
        {
            if (GameObject.Find("Chara_YoungCeltGroup001(Clone)"))
            {
                GameObject.Find("Chara_YoungCeltGroup001(Clone)").transform.GetChild(0).transform.position = _spawnPoint.position;
                SetGameManager();
            }
            else if (GameObject.Find("Chara_YoungCeltGroup001"))
            {
                GameObject.Find("Chara_YoungCeltGroup001").transform.GetChild(0).transform.position = _spawnPoint.position;
                SetGameManager();
            }
            
        }
    }

    public void SetGameManager()
    {
        Debug.Log("a");
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Debug.Log("b");
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
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Player") || other.gameObject.name == "JeuneCelte")
        {
            SetGameManager();
            GM.LoadScene(_nextSceneIndex);
        }
    }
}
