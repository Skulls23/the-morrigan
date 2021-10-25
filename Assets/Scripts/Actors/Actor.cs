using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    private bool isDead;
    private bool isGrounded;
    [SerializeField]
    private float lifePoints;

    public bool IsDead
    {
        get
        {
            return isDead;
        }
        set
        {
            this.isDead = value;
        }
    }

    public bool IsGrounded { get; set; }

    public float LifePoints 
    { 
        get
        {
            return this.lifePoints;
        }
        set
        {
            this.lifePoints = value;
        }
    }

    public void OnSpawn() { }

    public void OnDeath() { }

    public void OnHit() { }
}
