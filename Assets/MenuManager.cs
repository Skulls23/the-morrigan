using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameManager GM;
    public ScoreManager SM;

    void Update()
    {
        if(Input.GetButtonDown("Submit"))
        {
            GM.LoadScene(0);
            GM._IsTimeElapsing = true;
        }
    }
}
