using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private string[] scenesName;

    private ScenesManager SM;

    private int sceneIndex = 0;

    public float _timeElapsedInGame = 0f;
    public bool _IsTimeElapsing = false;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1)
        {
            Destroy(GameObject.FindGameObjectsWithTag("GameManager")[0]);
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        //GameObject.Find("SceneManager").GetComponent<ScenesManager>().SetGameManager();
    }

    private void Update()
    {
        if (_IsTimeElapsing)
        {
            _timeElapsedInGame += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SM = GameObject.Find("SceneManager").GetComponent<ScenesManager>();
            SM._nbEnnemies = 0;
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            GameObject.Find("JeuneCelte").GetComponent<CharacterMovement>().isInvincible = !GameObject.Find("JeuneCelte").GetComponent<CharacterMovement>().isInvincible;
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {
            RestartGame();
        }

    }

    public void RestartGame()
    {
        GameObject.Destroy(GameObject.Find("WwiseGlobal").gameObject);
        GameObject.Destroy(GameObject.Find("AudioManager").gameObject);
        GameObject.Destroy(GameObject.Find("Chara_YoungCeltGroup001(Clone)").gameObject);
        _timeElapsedInGame = 0;
        _IsTimeElapsing = false;
        SceneManager.LoadScene(0);
    }

    IEnumerator LoadAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1);
        SM = GameObject.Find("SceneManager").GetComponent<ScenesManager>();
        SM.SetGameManager();
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsyncScene(scenesName[sceneIndex]));
    }
}
