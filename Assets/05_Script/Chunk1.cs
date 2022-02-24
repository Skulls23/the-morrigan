using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk1 : MonoBehaviour
{

    public BlessingManager BM;
    private bool security;


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
        if (!security)
        {
            BM = GameObject.Find("BlessingManager").GetComponent<BlessingManager>();
            security = true;
            BM.StartChunk001();
        }
        
    }
}
