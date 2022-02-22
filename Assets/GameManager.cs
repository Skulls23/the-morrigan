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

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        //GameObject.Find("SceneManager").GetComponent<ScenesManager>().SetGameManager();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SM = GameObject.Find("SceneManager").GetComponent<ScenesManager>();
            SM._nbEnnemies = 0;
        }

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
