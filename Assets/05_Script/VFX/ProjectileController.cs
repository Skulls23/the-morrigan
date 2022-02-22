using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Transform targetToFollow;
    public float startFollow;
    public float lifetime;
    private float lifetimeTimer;

    public float FollowSpeed;
    // Start is called before the first frame update
    void Start()
    {
        targetToFollow = GameObject.Find("JeuneCelte").transform;
    }

    // Update is called once per frame
    void Update()
    {
        lifetimeTimer += Time.deltaTime;
        if (lifetimeTimer >= startFollow)
        {
            GetComponent<Rigidbody>().AddForce((((targetToFollow.position + new Vector3(0,3,0) - transform.position).normalized * FollowSpeed)), ForceMode.Acceleration);
        }
        if(lifetimeTimer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
