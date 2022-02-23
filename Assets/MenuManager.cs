using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameManager GM;

    void Update()
    {
        if(Input.GetButtonDown("Submit"))
        {
            GM.LoadScene(0);
        }
    }
}
