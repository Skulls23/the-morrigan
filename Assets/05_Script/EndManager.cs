using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    private ScoreManager SM;
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
            SM = GameObject.Find("Game Manager").transform.GetChild(0).gameObject.GetComponent<ScoreManager>();
            SM._nbUnusedLiquor = GameObject.Find("PlayerStats").GetComponent<UI_Player_Stats_Manager>()._nbRemainingLiquors;
            GameObject.Find("Game Manager").transform.GetChild(0).gameObject.SetActive(true);
            SM._timeElapsedInGame =  GameObject.Find("Game Manager").GetComponent<GameManager>()._timeElapsedInGame;
            SM.DisplayScore();
        }
    }
}
