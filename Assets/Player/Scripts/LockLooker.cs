using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockLooker : MonoBehaviour
{
    private GameObject enemy;
    private Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy != null)
        {
            trans.LookAt(enemy.transform.position);
        }
    }

    public void SetEnemy(GameObject _enemy)
    {
        enemy = _enemy;
    }
}
