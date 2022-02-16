using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlessingNS : MonoBehaviour
{
    [SerializeField]
    public Blessing blessing;
    [SerializeField]
    private string _id;

    public bool isEquipped = false;
    public int weight;

    public void Awake()
    {
        this._id = blessing._ID;
        this.weight = blessing.weight;
    }

    public abstract void OnEnter();
    public abstract void OnExit();

}