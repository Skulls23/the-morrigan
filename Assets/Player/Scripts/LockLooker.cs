using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockLooker : MonoBehaviour
{
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().LookAt(Enemy.transform.position);
    }
}
